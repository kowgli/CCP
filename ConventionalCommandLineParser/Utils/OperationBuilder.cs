using CCP.Attributes;
using CCP.Exceptions;
using CCP.Models;
using System;
using System.Linq;
using System.Reflection;

namespace CCP.Utils
{
    internal class OperationBuilder
    {
        private ValueBuilder valueBuilder;

        public OperationBuilder(Options options)
        {
            options = options ?? throw new ArgumentNullException(nameof(options));

            valueBuilder = new ValueBuilder(options.Locale, options.DateFormat, options.ArrayElementSeparator);
        }

        public IOperation CreateInstance(TypeInfo[] operationTypes, Operation operation)
        {
            operationTypes = operationTypes ?? throw new ArgumentNullException(nameof(operationTypes));
            operation = operation ?? throw new ArgumentNullException(nameof(operation));
            TypeInfo operationType = FindOperationTypeWithValidation(operationTypes, operation);

            ValidateRequiredProperties(operationType, operation);

            PropertyInfo[] availableProperties = FindPropertiesWithValidation(operationType, operation);

            IOperation instance = (IOperation)Activator.CreateInstance(operationType.AsType());

            SetPropertiesOnInstance(instance, operation, availableProperties);

            return instance;
        }

        private void ValidateRequiredProperties(TypeInfo operationType, Operation command)
        {
            var requiredProperties = operationType.DeclaredProperties
                                                   .Where(p => p.CustomAttributes.Any(a => a.AttributeType == typeof(RequiredAttribute)))
                                                   .Select(p => p.Name);

            var missingRequired = requiredProperties.FirstOrDefault(p => !command.Arguments.Any(a => a.Name.ToLowerInvariant() == p.ToLowerInvariant()));

            if (missingRequired != null)
            {
                throw new MissingRequiredArgumentException(command.Name, missingRequired);
            }
        }

        private TypeInfo FindOperationTypeWithValidation(TypeInfo[] operationTypes, Operation operation)
        {
            TypeInfo[] filteredOperationTypes = operationTypes
                                                      .Where(t => 
                                                            t.Name.ToLowerInvariant() == operation.Name.ToLowerInvariant()
                                                            ||
                                                            t.GetCustomAttributes<AliasAttribute>().Any(a => 
                                                                a.Name.ToLowerInvariant() == operation.Name.ToLowerInvariant()
                                                                || $"-{a.Name.ToLowerInvariant()}" == operation.Name.ToLowerInvariant()
                                                            )
                                                       )
                                                      .ToArray();

            if (filteredOperationTypes.Length == 0)
            {
                throw new OperationNotFoundException(operationName: operation.Name);
            }
            else if (filteredOperationTypes.Length > 1)
            {
                throw new MultipleOperationsFoundException(operationName: operation.Name);
            }

            return filteredOperationTypes[0];
        }

        private PropertyInfo[] FindPropertiesWithValidation(TypeInfo typeInfo, Operation operation)
        {
            PropertyInfo[] availableProperties = typeInfo.DeclaredProperties
                                                         .Where(p => p.CanWrite && p.SetMethod?.IsPublic == true)
                                                         .ToArray();

            foreach (Argument argument in operation.Arguments)
            {
                int count = availableProperties.Where(p => 
                                                        p.Name.ToLowerInvariant() == argument.Name.ToLowerInvariant()
                                                        || p.GetCustomAttributes<AliasAttribute>().Any(a => a.Name.ToLowerInvariant() == argument.Name.ToLowerInvariant())
                                                     ).Count();

                if (count == 0)
                {
                    throw new ArgumentNotFoundException(operationName: operation.Name, argumentName: argument.Name);
                }
                else if (count > 1)
                {
                    throw new MultipleArgumentsFoundException(operationName: operation.Name, argumentName: argument.Name);
                }
            }

            return availableProperties;
        }

        private void SetPropertiesOnInstance(IOperation instance, Operation command, PropertyInfo[] availableProperties)
        {
            foreach (Argument argument in command.Arguments)
            {
                PropertyInfo property = availableProperties.First(p => 
                    p.Name.ToLowerInvariant() == argument.Name.ToLowerInvariant()
                    || p.GetCustomAttributes<AliasAttribute>().Any(a => a.Name.ToLowerInvariant() == argument.Name.ToLowerInvariant())
                );
                property.SetValue(instance, valueBuilder.GetValue(property.PropertyType, argument));
            }
        }
    }
}