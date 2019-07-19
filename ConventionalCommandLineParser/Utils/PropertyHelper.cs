using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CCP.Utils
{
    public static class PropertyHelper
    {
        public static List<PropertyInfo> GetPropertiesOfTypeRecusive(TypeInfo typeInfo)
        {
            List<PropertyInfo> result = new List<PropertyInfo>();

            PropertyInfo[] availableProperties = typeInfo.DeclaredProperties
                                                         .Where(p => p.CanWrite && p.SetMethod?.IsPublic == true)
                                                         .ToArray();

            result.AddRange(availableProperties);

            if (typeInfo.BaseType != typeof(object))
            {
                List<PropertyInfo> baseProperties = GetPropertiesOfTypeRecusive(typeInfo.BaseType.GetTypeInfo());

                result.AddRange(baseProperties);
            }

            return result;
        }
    }
}
