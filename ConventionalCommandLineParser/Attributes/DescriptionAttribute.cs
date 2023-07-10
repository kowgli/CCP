using System;

namespace CCP.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public class DescriptionAttribute : Attribute
    {
        public DescriptionAttribute()
        {
        }

        public DescriptionAttribute(string description)
        {
            Description = description;
        }

        public string Description { get; set; } = "";
    }
}
