using CCP.Attributes;
using System;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CCP.Utils
{
    internal class HelpTextBuilder
    {
        public static string BuildHelpText(Assembly operationsAssembly, Exception? includingError = null)
        {
            StringBuilder sbHelp = new StringBuilder();

            sbHelp.AppendLine($"{operationsAssembly.GetName().Name} v{operationsAssembly.GetName().Version}");
            sbHelp.AppendLine();

            if (includingError != null)
            {
                sbHelp.AppendLine("ERROR");
                sbHelp.AppendLine("-----");
                sbHelp.AppendLine(includingError.Message);
                sbHelp.AppendLine();
            }

            sbHelp.AppendLine("POSSIBLE PARAMETERS");
            sbHelp.AppendLine("-------------------");

            TypeInfo[] operationTypes = operationsAssembly.DefinedTypes
                                                           .Where(x => x.ImplementedInterfaces.Contains(typeof(IOperation)))
                                                           .OrderBy(x => x.Name)
                                                           .ToArray();

            foreach (TypeInfo operationType in operationTypes)
            {
                var dscAttribute = operationType.GetCustomAttributes<DescriptionAttribute>().FirstOrDefault();
                if (dscAttribute != null)
                {
                    sbHelp.Append($"[{dscAttribute.Description}]");
                    sbHelp.Append(Environment.NewLine);
                }

                sbHelp.AppendLine(operationType.Name);
                foreach (AliasAttribute alias in operationType.GetCustomAttributes<AliasAttribute>())
                {
                    sbHelp.AppendLine($"\tAlias: {alias.Name} or -{alias.Name}");
                }

                var properties = PropertyHelper.GetPropertiesOfTypeRecusive(operationType)
                                               .OrderBy(p => p.Name)
                                               .Select(p => new
                                               {
                                                   p.Name,
                                                   HasRequiredAttribute = p.CustomAttributes.Any(a => a.AttributeType == typeof(RequiredAttribute)),
                                                   HasDescriptionAttribute = p.CustomAttributes.Any(a => a.AttributeType == typeof(DescriptionAttribute)),
                                                   TypeName = p.PropertyType.GenericTypeArguments.Length > 0 ?
                                                              p.PropertyType.GenericTypeArguments[0].Name : p.PropertyType.Name,
                                                   DescriptionAttribute = p.CustomAttributes.Any(a => a.AttributeType == typeof(DescriptionAttribute)) ?
                                                                          p.GetCustomAttributes<DescriptionAttribute>().First() : null
                                               });

                foreach (var property in properties)
                {
                    if (property.HasDescriptionAttribute)
                    {                        
                        sbHelp.Append($"\t  [{property.DescriptionAttribute.Description}]");
                        sbHelp.Append(Environment.NewLine);
                    }

                    sbHelp.Append($"\t* {property.Name} <{property.TypeName}>");
                    
                    if (property.HasRequiredAttribute) { sbHelp.Append(" [REQUIRED]"); }                    

                    sbHelp.AppendLine();
                }

                sbHelp.AppendLine();
            }

            string helpText = sbHelp.ToString();

            return helpText;
        }
    }
}
