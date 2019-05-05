using System;

namespace CCP.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, Inherited = true, AllowMultiple = true)]
    public class AliasAttribute : Attribute
    {
        public string Name { get; set; } = "";
    }
}
