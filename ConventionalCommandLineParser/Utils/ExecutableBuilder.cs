using ConventionalCommandLineParser.Attributes;
using ConventionalCommandLineParser.Exceptions;
using ConventionalCommandLineParser.Models;
using System;
using System.Linq;
using System.Reflection;

namespace ConventionalCommandLineParser.Utils
{
    internal class ExecutableBuilder
    {
        private ValueBuilder valueBuilder;

        public ExecutableBuilder(FormattingOptions formatingOptions)
        {
            formatingOptions = formatingOptions ?? throw new ArgumentNullException(nameof(formatingOptions));

            valueBuilder = new ValueBuilder(formatingOptions.Locale, formatingOptions.DateFormat);
        }

        public IExecutable CreateInstance(TypeInfo[] executableTypes, Command command)
        {
            executableTypes = executableTypes ?? throw new ArgumentNullException(nameof(executableTypes));
            command = command ?? throw new ArgumentNullException(nameof(command));
            TypeInfo executableType = FindExecutableTypeWithValidation(executableTypes, command);

            ValidateRequiredProperties(executableType, command);

            PropertyInfo[] availableProperties = FindPropertiesWithValidation(executableType, command);

            IExecutable instance = (IExecutable)Activator.CreateInstance(executableType.AsType());

            SetPropertiesOnInstance(instance, command, availableProperties);

            return instance;
        }

        private void ValidateRequiredProperties(TypeInfo executableType, Command command)
        {
            var requiredProperties = executableType.DeclaredProperties
                                                   .Where(p => p.CustomAttributes.Any(a => a.AttributeType == typeof(RequiredAttribute)))
                                                   .Select(p => p.Name);

            var missingRequired = requiredProperties.FirstOrDefault(p => !command.Arguments.Any(a => a.Name.ToLowerInvariant() == p.ToLowerInvariant()));

            if (missingRequired != null)
            {
                throw new MissingRequiredParameterException("Missing value for required property", command.Name, missingRequired);
            }
        }

        private TypeInfo FindExecutableTypeWithValidation(TypeInfo[] executableTypes, Command command)
        {
            TypeInfo[] filteredExecutableTypes = executableTypes
                                                      .Where(t => t.Name.ToLowerInvariant() == command.Name.ToLowerInvariant())
                                                      .ToArray();

            if (filteredExecutableTypes.Length == 0)
            {
                throw new ExecutableNotFoundException(message: "Not found", executableName: command.Name);
            }
            else if (filteredExecutableTypes.Length > 1)
            {
                throw new MultipleExecutablesFoundException(message: "Not found", executableName: command.Name);
            }

            return filteredExecutableTypes[0];
        }

        private PropertyInfo[] FindPropertiesWithValidation(TypeInfo typeInfo, Command command)
        {
            PropertyInfo[] availableProperties = typeInfo.DeclaredProperties
                                                         .Where(p => p.CanWrite && p.SetMethod?.IsPublic == true)
                                                         .ToArray();

            foreach (Argument argument in command.Arguments)
            {
                int count = availableProperties.Where(p => p.Name.ToLowerInvariant() == argument.Name.ToLowerInvariant()).Count();

                if (count == 0)
                {
                    throw new PropertyNotFoundException(message: "Not found", propertyName: argument.Name);
                }
                else if (count > 1)
                {
                    throw new MultiplePropertiesFoundException(message: "Multiple found", propertyName: argument.Name);
                }
            }

            return availableProperties;
        }

        private void SetPropertiesOnInstance(IExecutable instance, Command command, PropertyInfo[] availableProperties)
        {
            foreach (Argument argument in command.Arguments)
            {
                PropertyInfo property = availableProperties.First(p => p.Name.ToLowerInvariant() == argument.Name.ToLowerInvariant());
                property.SetValue(instance, valueBuilder.GetValue(property.PropertyType, argument));
            }
        }
    }
}