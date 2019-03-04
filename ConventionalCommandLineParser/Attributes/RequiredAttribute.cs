using System;

namespace ConventionalCommandLineParser.Attributes
{
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = false)]
    internal sealed class RequiredAttribute : Attribute
    {
    }
}