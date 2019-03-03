using ConventionalCommandLineParser.UnitTests.Executors;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;

namespace ConventionalCommandLineParser.UnitTests
{
    [TestClass]
    public class ExecutorTests
    {
        [TestMethod]
        public void When_NoArguments_BuildsCorrectExecutable()
        {
            string[] args = new string[] { };

            IExecutable[] executables = Executor.BuildExecutables(args, typeof(ExecutorTests).Assembly, FormattingOptions.Default);

            Assert.AreEqual(0, executables.Length);
        }

        [TestMethod]
        public void When_CommandWithNoArgs_BuildsCorrectExecutable()
        {
            string[] args = new string[] { "CommandWithNoArgs" };

            IExecutable[] executables = Executor.BuildExecutables(args, typeof(ExecutorTests).Assembly, FormattingOptions.Default);

            Assert.AreEqual(1, executables.Length);
            Assert.IsTrue(executables[0] is CommandWithNoArgs);
        }

        [TestMethod]
        public void When_CommandWithArgs_PropertyValuesAreSet()
        {
            string[] args = new string[] { "CommandWithSimpleArgs", "Arg1=test", "Arg2=3", "Arg3=123.45" };

            IExecutable[] executables = Executor.BuildExecutables(args, typeof(ExecutorTests).Assembly, FormattingOptions.Default);

            Assert.AreEqual(1, executables.Length);
            Assert.IsTrue(executables[0] is CommandWithSimpleArgs);

            CommandWithSimpleArgs command = (CommandWithSimpleArgs) executables[0];

            Assert.AreEqual("test", command.Arg1);
            Assert.AreEqual(3, command.Arg2);
            Assert.AreEqual(123.45M, command.Arg3);
        }

        [TestMethod]
        public void When_CommandWithArgsInDifferentLocale_PropertyValuesAreSet()
        {
            string[] args = new string[] { "CommandWithSimpleArgs", "Arg1=test", "Arg2=3", "Arg3=123,45" };

            var formattingOptions = FormattingOptions.Default;
            formattingOptions.Locale = new CultureInfo("pl-PL");

            IExecutable[] executables = Executor.BuildExecutables(args, typeof(ExecutorTests).Assembly, formattingOptions);

            Assert.AreEqual(1, executables.Length);
            Assert.IsTrue(executables[0] is CommandWithSimpleArgs);

            CommandWithSimpleArgs command = (CommandWithSimpleArgs)executables[0];

            Assert.AreEqual("test", command.Arg1);
            Assert.AreEqual(3, command.Arg2);
            Assert.AreEqual(123.45M, command.Arg3);
        }

        [TestMethod]
        public void When_ComplexTypeJson_PropertyValuesAreSet()
        {
            string json = @"{
                IntValue: 123,
                StringValue: ""hello world"",
                BoolValue: true
            }";

            string[] args = new string[] { "CommandWithComplexArg", $"Arg1={json}" };

            IExecutable[] executables = Executor.BuildExecutables(args, typeof(ExecutorTests).Assembly, FormattingOptions.Default);

            Assert.AreEqual(1, executables.Length);
            Assert.IsTrue(executables[0] is CommandWithComplexArg);

            CommandWithComplexArg command = (CommandWithComplexArg)executables[0];

            Assert.IsNotNull(command.Arg1);
            Assert.AreEqual(123, command.Arg1.IntValue, "int value incorrect");
            Assert.AreEqual("hello world", command.Arg1.StringValue, "string value incorrect");
            Assert.IsTrue(command.Arg1.BoolValue, "bool value incorrect");

        }
    }
}