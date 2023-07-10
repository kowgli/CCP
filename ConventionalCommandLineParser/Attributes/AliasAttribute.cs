using System;

namespace CCP.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, Inherited = true, AllowMultiple = true)]
    public class AliasAttribute : Attribute
    {
        public AliasAttribute()
        {            
        }

        public AliasAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; set; } = "";
    }
}
