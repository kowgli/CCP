using System;

namespace CCP.Attributes
{
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public sealed class RequiredAttribute : Attribute
    {
    }
}