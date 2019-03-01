using ConventionalCommandLineParser.Exceptions;
using ConventionalCommandLineParser.Models;
using System;
using System.Linq;
using System.Reflection;

namespace ConventionalCommandLineParser.Utils
{
    internal static class ExecutableBuilder
    {
        public static IExecutable CreateInstance(TypeInfo[] executableTypes, Command command)
        {
            executableTypes = executableTypes ?? throw new ArgumentNullException(nameof(executableTypes));
            command = command ?? throw new ArgumentNullException(nameof(command));
            TypeInfo executableType = FindExecutableTypeWithValidation(executableTypes, command);

            PropertyInfo[] availableProperties = FindPropertiesWithValidation(executableType, command);

            IExecutable instance = (IExecutable)Activator.CreateInstance(executableType.AsType());

            SetPropertiesOnInstance(instance, command, availableProperties);

            return instance;
        }

        private static TypeInfo FindExecutableTypeWithValidation(TypeInfo[] executableTypes, Command command)
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

        private static PropertyInfo[] FindPropertiesWithValidation(TypeInfo typeInfo, Command command)
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

        private static void SetPropertiesOnInstance(IExecutable instance, Command command, PropertyInfo[] availableProperties)
        {
            foreach (Argument argument in command.Arguments)
            {
                PropertyInfo property = availableProperties.First(p => p.Name.ToLowerInvariant() == argument.Name.ToLowerInvariant());
                property.SetValue(instance, ValueBuilder.GetValueFromString(property.PropertyType, argument.Value));
            }
        }
    }
}