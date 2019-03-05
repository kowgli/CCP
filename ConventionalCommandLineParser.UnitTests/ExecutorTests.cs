using CCP;
using CCP.Exceptions;
using CCP.UnitTests.Executors;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;

namespace ConventionalOperationLineParser.UnitTests
{
    [TestClass]
    public class ExecutorTests
    {
        [TestMethod]
        public void When_NoArguments_BuildsCorrectExecutable()
        {
            string[] args = new string[] { };

            IOperation[] executables = Executor.BuildOperations(args, typeof(ExecutorTests).Assembly, FormattingOptions.Default);

            Assert.AreEqual(0, executables.Length);
        }

        [TestMethod]
        public void When_OperationWithNoArgs_BuildsCorrectExecutable()
        {
            string[] args = new string[] { nameof(OperationWithNoProps) };

            IOperation[] executables = Executor.BuildOperations(args, typeof(ExecutorTests).Assembly, FormattingOptions.Default);

            Assert.AreEqual(1, executables.Length);
            Assert.IsTrue(executables[0] is OperationWithNoProps);
        }

        [TestMethod]
        public void When_OperationWithArgs_PropertyValuesAreSet()
        {
            string[] args = new string[] { nameof(OperationWithSimpleProps), "Arg1=test", "Arg2=3", "Arg3=123.45" };

            IOperation[] executables = Executor.BuildOperations(args, typeof(ExecutorTests).Assembly, FormattingOptions.Default);

            Assert.AreEqual(1, executables.Length);
            Assert.IsTrue(executables[0] is OperationWithSimpleProps);

            OperationWithSimpleProps Operation = (OperationWithSimpleProps) executables[0];

            Assert.AreEqual("test", Operation.Arg1);
            Assert.AreEqual(3, Operation.Arg2);
            Assert.AreEqual(123.45M, Operation.Arg3);
        }

        [TestMethod]
        public void When_OperationWithArgsInDifferentLocale_PropertyValuesAreSet()
        {
            string[] args = new string[] { nameof(OperationWithSimpleProps), "Arg1=test", "Arg2=3", "Arg3=123,45" };

            var formattingOptions = FormattingOptions.Default;
            formattingOptions.Locale = new CultureInfo("pl-PL");

            IOperation[] executables = Executor.BuildOperations(args, typeof(ExecutorTests).Assembly, formattingOptions);

            Assert.AreEqual(1, executables.Length);
            Assert.IsTrue(executables[0] is OperationWithSimpleProps);

            OperationWithSimpleProps Operation = (OperationWithSimpleProps)executables[0];

            Assert.AreEqual("test", Operation.Arg1);
            Assert.AreEqual(3, Operation.Arg2);
            Assert.AreEqual(123.45M, Operation.Arg3);
        }

        [TestMethod]
        public void When_ComplexTypeJson_PropertyValuesAreSet()
        {
            string json = @"{
                IntValue: 123,
                StringValue: ""hello world"",
                BoolValue: true
            }";

            string[] args = new string[] { nameof(OperationWithComplexProp), $"Arg1={json}" };

            IOperation[] executables = Executor.BuildOperations(args, typeof(ExecutorTests).Assembly, FormattingOptions.Default);

            Assert.AreEqual(1, executables.Length);
            Assert.IsTrue(executables[0] is OperationWithComplexProp);

            OperationWithComplexProp Operation = (OperationWithComplexProp)executables[0];

            Assert.IsNotNull(Operation.Arg1);
            Assert.AreEqual(123, Operation.Arg1.IntValue, "int value incorrect");
            Assert.AreEqual("hello world", Operation.Arg1.StringValue, "string value incorrect");
            Assert.IsTrue(Operation.Arg1.BoolValue, "bool value incorrect");
        }

        [TestMethod]
        public void When_DateTimeWithDefaultFormat_PropertyValuesAreSet()
        {
            DateTime dateTime = DateTime.Now;

            string dateTimeString = dateTime.ToShortDateString();

            string[] args = new string[] { nameof(OperationWithDateTimeProp), $"Arg1={dateTimeString}" };

            IOperation[] executables = Executor.BuildOperations(args, typeof(ExecutorTests).Assembly, FormattingOptions.Default);

            Assert.AreEqual(1, executables.Length);
            Assert.IsTrue(executables[0] is OperationWithDateTimeProp);

            OperationWithDateTimeProp Operation = (OperationWithDateTimeProp)executables[0];

            Assert.AreEqual(dateTime.Year, Operation.Arg1.Year, "year value incorrect");
            Assert.AreEqual(dateTime.Month, Operation.Arg1.Month, "month value incorrect");
            Assert.AreEqual(dateTime.Day, Operation.Arg1.Day, "day value incorrect");           
        }

        [TestMethod]
        public void When_DateTimeWithSpecifiedFormat_PropertyValuesAreSet()
        {
            string dateTimeString = "2015-02.12";

            string[] args = new string[] { nameof(OperationWithDateTimeProp), $"Arg1={dateTimeString}" };

            FormattingOptions formattingOptions = new FormattingOptions
            {
                DateFormat = "yyyy-MM.dd"
            };

            IOperation[] executables = Executor.BuildOperations(args, typeof(ExecutorTests).Assembly, formattingOptions);

            Assert.AreEqual(1, executables.Length);
            Assert.IsTrue(executables[0] is OperationWithDateTimeProp);

            OperationWithDateTimeProp Operation = (OperationWithDateTimeProp)executables[0];

            Assert.AreEqual(2015, Operation.Arg1.Year, "year value incorrect");
            Assert.AreEqual(2, Operation.Arg1.Month, "month value incorrect");
            Assert.AreEqual(12, Operation.Arg1.Day, "day value incorrect");
        }

        [TestMethod]
        public void When_InvalidValue_ThrowsValueParsingException()
        {
            string[] args = new string[] { nameof(OperationWithDateTimeProp), $"Arg1=not_a_date" };

            Assert.ThrowsException<ValueParsingException>(() => Executor.BuildOperations(args, typeof(ExecutorTests).Assembly, FormattingOptions.Default));
        }

        [TestMethod]
        public void When_MissingRequiredProperty_ThrowsException()
        {
            string[] args = new string[] { nameof(OperationWithRequiredProp), $"Arg1=1" };

            try
            {
                Executor.BuildOperations(args, typeof(ExecutorTests).Assembly, FormattingOptions.Default);

                Assert.Fail("Should have thrown an exception");
            }
            catch(MissingRequiredArgumentException ex)
            {
                Assert.AreEqual("OperationWithRequiredProp", ex.ExecutableName);
                Assert.AreEqual("Arg2", ex.ParameterName);
            }
        }

        [TestMethod]
        public void When_Asked_ExecutorIsExecuted()
        {
            string[] args = new string[] { nameof(OperationForExecutionTest1), nameof(OperationForExecutionTest1), nameof(OperationForExecutionTest2) };

            Executor.ExecuteFromArgs(args, typeof(ExecutorTests).Assembly);

            Assert.AreEqual(2, OperationForExecutionTest1.RunCount);
            Assert.AreEqual(1, OperationForExecutionTest2.RunCount);
        }
    }
}