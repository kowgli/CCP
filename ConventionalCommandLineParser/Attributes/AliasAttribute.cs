using System;

namespace CCP.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class AliasAttribute : Attribute
    {
        public string Name { get; set; } = "";
    }
}
