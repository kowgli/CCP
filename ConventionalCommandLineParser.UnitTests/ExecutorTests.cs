using ConventionalCommandLineParser.Exceptions;
using ConventionalCommandLineParser.UnitTests.Executors;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
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
            string[] args = new string[] { nameof(CommandWithNoProps) };

            IExecutable[] executables = Executor.BuildExecutables(args, typeof(ExecutorTests).Assembly, FormattingOptions.Default);

            Assert.AreEqual(1, executables.Length);
            Assert.IsTrue(executables[0] is CommandWithNoProps);
        }

        [TestMethod]
        public void When_CommandWithArgs_PropertyValuesAreSet()
        {
            string[] args = new string[] { nameof(CommandWithSimpleProps), "Arg1=test", "Arg2=3", "Arg3=123.45" };

            IExecutable[] executables = Executor.BuildExecutables(args, typeof(ExecutorTests).Assembly, FormattingOptions.Default);

            Assert.AreEqual(1, executables.Length);
            Assert.IsTrue(executables[0] is CommandWithSimpleProps);

            CommandWithSimpleProps command = (CommandWithSimpleProps) executables[0];

            Assert.AreEqual("test", command.Arg1);
            Assert.AreEqual(3, command.Arg2);
            Assert.AreEqual(123.45M, command.Arg3);
        }

        [TestMethod]
        public void When_CommandWithArgsInDifferentLocale_PropertyValuesAreSet()
        {
            string[] args = new string[] { nameof(CommandWithSimpleProps), "Arg1=test", "Arg2=3", "Arg3=123,45" };

            var formattingOptions = FormattingOptions.Default;
            formattingOptions.Locale = new CultureInfo("pl-PL");

            IExecutable[] executables = Executor.BuildExecutables(args, typeof(ExecutorTests).Assembly, formattingOptions);

            Assert.AreEqual(1, executables.Length);
            Assert.IsTrue(executables[0] is CommandWithSimpleProps);

            CommandWithSimpleProps command = (CommandWithSimpleProps)executables[0];

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

            string[] args = new string[] { nameof(CommandWithComplexProp), $"Arg1={json}" };

            IExecutable[] executables = Executor.BuildExecutables(args, typeof(ExecutorTests).Assembly, FormattingOptions.Default);

            Assert.AreEqual(1, executables.Length);
            Assert.IsTrue(executables[0] is CommandWithComplexProp);

            CommandWithComplexProp command = (CommandWithComplexProp)executables[0];

            Assert.IsNotNull(command.Arg1);
            Assert.AreEqual(123, command.Arg1.IntValue, "int value incorrect");
            Assert.AreEqual("hello world", command.Arg1.StringValue, "string value incorrect");
            Assert.IsTrue(command.Arg1.BoolValue, "bool value incorrect");
        }

        [TestMethod]
        public void When_DateTimeWithDefaultFormat_PropertyValuesAreSet()
        {
            DateTime dateTime = DateTime.Now;

            string dateTimeString = dateTime.ToShortDateString();

            string[] args = new string[] { nameof(CommandWithDateTimeProp), $"Arg1={dateTimeString}" };

            IExecutable[] executables = Executor.BuildExecutables(args, typeof(ExecutorTests).Assembly, FormattingOptions.Default);

            Assert.AreEqual(1, executables.Length);
            Assert.IsTrue(executables[0] is CommandWithDateTimeProp);

            CommandWithDateTimeProp command = (CommandWithDateTimeProp)executables[0];

            Assert.AreEqual(dateTime.Year, command.Arg1.Year, "year value incorrect");
            Assert.AreEqual(dateTime.Month, command.Arg1.Month, "month value incorrect");
            Assert.AreEqual(dateTime.Day, command.Arg1.Day, "day value incorrect");           
        }

        [TestMethod]
        public void When_DateTimeWithSpecifiedFormat_PropertyValuesAreSet()
        {
            string dateTimeString = "2015-02.12";

            string[] args = new string[] { nameof(CommandWithDateTimeProp), $"Arg1={dateTimeString}" };

            FormattingOptions formattingOptions = new FormattingOptions
            {
                DateFormat = "yyyy-MM.dd"
            };

            IExecutable[] executables = Executor.BuildExecutables(args, typeof(ExecutorTests).Assembly, formattingOptions);

            Assert.AreEqual(1, executables.Length);
            Assert.IsTrue(executables[0] is CommandWithDateTimeProp);

            CommandWithDateTimeProp command = (CommandWithDateTimeProp)executables[0];

            Assert.AreEqual(2015, command.Arg1.Year, "year value incorrect");
            Assert.AreEqual(2, command.Arg1.Month, "month value incorrect");
            Assert.AreEqual(12, command.Arg1.Day, "day value incorrect");
        }

        [TestMethod]
        public void When_InvalidValue_ThrowsValueParsingException()
        {
            string[] args = new string[] { nameof(CommandWithDateTimeProp), $"Arg1=not_a_date" };

            Assert.ThrowsException<ValueParsingException>(() => Executor.BuildExecutables(args, typeof(ExecutorTests).Assembly, FormattingOptions.Default));
        }

        [TestMethod]
        public void When_MissingRequiredProperty_ThrowsException()
        {
            string[] args = new string[] { nameof(CommandWithRequiredProp), $"Arg1=1" };

            try
            {
                Executor.BuildExecutables(args, typeof(ExecutorTests).Assembly, FormattingOptions.Default);

                Assert.Fail("Should have thrown an exception");
            }
            catch(MissingRequiredParameterException ex)
            {
                Assert.AreEqual("CommandWithRequiredProp", ex.ExecutableName);
                Assert.AreEqual("Arg2", ex.ParameterName);
            }
        }

        [TestMethod]
        public void When_Asked_ExecutorIsExecuted()
        {
            string[] args = new string[] { nameof(CommandForExecutionTest1), nameof(CommandForExecutionTest1), nameof(CommandForExecutionTest2) };

            Executor.ExecuteFromArgs(args, typeof(ExecutorTests).Assembly);

            Assert.AreEqual(2, CommandForExecutionTest1.RunCount);
            Assert.AreEqual(1, CommandForExecutionTest2.RunCount);
        }
    }
}