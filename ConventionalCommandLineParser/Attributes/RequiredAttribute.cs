using System;

namespace ConventionalCommandLineParser.Attributes
{
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = false)]
    public sealed class RequiredAttribute : Attribute
    {
    }
}