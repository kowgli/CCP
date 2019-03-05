using System;

namespace CCP.Attributes
{
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = false)]
    public sealed class RequiredAttribute : Attribute
    {
    }
}