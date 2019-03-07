using CCP.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CCP.Utils
{
    internal class HelpTextBuilder
    {
        public static string BuildHelpText(Assembly operationsAssembly, Exception? includingError = null)
        {
            StringBuilder sbHelp = new StringBuilder();

            sbHelp.AppendLine($"{operationsAssembly.GetName().Name} v{operationsAssembly.GetName().Version}");
            sbHelp.AppendLine();

            if(includingError != null)
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

            foreach(TypeInfo operationType in operationTypes)
            {
                sbHelp.AppendLine(operationType.Name);

                var properties = operationType.DeclaredProperties    
                                                   .OrderBy(p => p.Name)
                                                   .Select(p => new
                                                   {
                                                       p.Name,
                                                       Required = p.CustomAttributes.Any(a => a.AttributeType == typeof(RequiredAttribute)),
                                                       TypeName = p.PropertyType.GenericTypeArguments.Length > 0 ? 
                                                                  p.PropertyType.GenericTypeArguments[0].Name : p.PropertyType.Name
                                                   });

                foreach (var property in properties)
                {
                    sbHelp.Append($"\t{property.Name}=<{property.TypeName}>");
                    if (property.Required) { sbHelp.Append(" [REQUIRED]"); }
                    sbHelp.AppendLine();
                }

                sbHelp.AppendLine();
            }            

            return sbHelp.ToString();
        }
    }
}
