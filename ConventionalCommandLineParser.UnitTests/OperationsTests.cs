using CCP;
using CCP.Exceptions;
using CCP.UnitTests.Operations;
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

            IOperation[] operations = Executor.BuildOperations(args, typeof(ExecutorTests).Assembly, Options.Default);

            Assert.AreEqual(0, operations.Length);
        }

        [TestMethod]
        public void When_OperationWithNoArgs_BuildsCorrectExecutable()
        {
            string[] args = new string[] { nameof(OperationWithNoProps) };

            IOperation[] operations = Executor.BuildOperations(args, typeof(ExecutorTests).Assembly, Options.Default);

            Assert.AreEqual(1, operations.Length);
            Assert.IsTrue(operations[0] is OperationWithNoProps);
        }

        [TestMethod]
        public void When_OperationWithArgs_PropertyValuesAreSet()
        {
            string[] args = new string[] { nameof(OperationWithSimpleProps), "Arg1=test", "Arg2=3", "Arg3=123.45" };

            IOperation[] operations = Executor.BuildOperations(args, typeof(ExecutorTests).Assembly, Options.Default);

            Assert.AreEqual(1, operations.Length);
            Assert.IsTrue(operations[0] is OperationWithSimpleProps);

            OperationWithSimpleProps Operation = (OperationWithSimpleProps) operations[0];

            Assert.AreEqual("test", Operation.Arg1);
            Assert.AreEqual(3, Operation.Arg2);
            Assert.AreEqual(123.45M, Operation.Arg3);
        }

        [TestMethod]
        public void When_OperationWithArgsInDifferentLocale_PropertyValuesAreSet()
        {
            string[] args = new string[] { nameof(OperationWithSimpleProps), "Arg1=test", "Arg2=3", "Arg3=123,45" };

            var formattingOptions = Options.Default;
            formattingOptions.Locale = new CultureInfo("pl-PL");

            IOperation[] operations = Executor.BuildOperations(args, typeof(ExecutorTests).Assembly, formattingOptions);

            Assert.AreEqual(1, operations.Length);
            Assert.IsTrue(operations[0] is OperationWithSimpleProps);

            OperationWithSimpleProps Operation = (OperationWithSimpleProps)operations[0];

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

            IOperation[] operations = Executor.BuildOperations(args, typeof(ExecutorTests).Assembly, Options.Default);

            Assert.AreEqual(1, operations.Length);
            Assert.IsTrue(operations[0] is OperationWithComplexProp);

            OperationWithComplexProp Operation = (OperationWithComplexProp)operations[0];

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

            IOperation[] operations = Executor.BuildOperations(args, typeof(ExecutorTests).Assembly, Options.Default);

            Assert.AreEqual(1, operations.Length);
            Assert.IsTrue(operations[0] is OperationWithDateTimeProp);

            OperationWithDateTimeProp Operation = (OperationWithDateTimeProp)operations[0];

            Assert.AreEqual(dateTime.Year, Operation.Arg1.Year, "year value incorrect");
            Assert.AreEqual(dateTime.Month, Operation.Arg1.Month, "month value incorrect");
            Assert.AreEqual(dateTime.Day, Operation.Arg1.Day, "day value incorrect");           
        }

        [TestMethod]
        public void When_DateTimeWithSpecifiedFormat_PropertyValuesAreSet()
        {
            string dateTimeString = "2015-02.12";

            string[] args = new string[] { nameof(OperationWithDateTimeProp), $"Arg1={dateTimeString}" };

            Options formattingOptions = new Options
            {
                DateFormat = "yyyy-MM.dd"
            };

            IOperation[] operations = Executor.BuildOperations(args, typeof(ExecutorTests).Assembly, formattingOptions);

            Assert.AreEqual(1, operations.Length);
            Assert.IsTrue(operations[0] is OperationWithDateTimeProp);

            OperationWithDateTimeProp Operation = (OperationWithDateTimeProp)operations[0];

            Assert.AreEqual(2015, Operation.Arg1.Year, "year value incorrect");
            Assert.AreEqual(2, Operation.Arg1.Month, "month value incorrect");
            Assert.AreEqual(12, Operation.Arg1.Day, "day value incorrect");
        }

        [TestMethod]
        public void When_InvalidValue_ThrowsValueParsingException()
        {
            string[] args = new string[] { nameof(OperationWithDateTimeProp), $"Arg1=not_a_date" };

            Assert.ThrowsException<ValueParsingException>(() => Executor.BuildOperations(args, typeof(ExecutorTests).Assembly, Options.Default));
        }

        [TestMethod]
        public void When_MissingRequiredProperty_ThrowsException()
        {
            string[] args = new string[] { nameof(OperationWithRequiredProp), $"Arg1=1" };

            try
            {
                Executor.BuildOperations(args, typeof(ExecutorTests).Assembly, Options.Default);

                Assert.Fail("Should have thrown an exception");
            }
            catch(MissingRequiredArgumentException ex)
            {
                Assert.AreEqual("OperationWithRequiredProp", ex.OperationName);
                Assert.AreEqual("Arg2", ex.ArgumentName);
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

        [TestMethod]
        public void When_StringArrayProperty_PropertyValuesAreSet()
        {
            string[] args = new string[] { nameof(OperationWithArrayProps), $"StringArray=aaa;bbb;ccc" };

            IOperation[] operations = Executor.BuildOperations(args, typeof(ExecutorTests).Assembly, Options.Default);

            Assert.AreEqual(1, operations.Length);
            Assert.IsInstanceOfType(operations[0], typeof(OperationWithArrayProps));

            OperationWithArrayProps operation = (OperationWithArrayProps)operations[0];

            Assert.AreEqual("aaa", operation.StringArray[0]);
            Assert.AreEqual("bbb", operation.StringArray[1]);
            Assert.AreEqual("ccc", operation.StringArray[2]);
        }

        [TestMethod]
        public void When_IntArrayProperty_PropertyValuesAreSet()
        {
            string[] args = new string[] { nameof(OperationWithArrayProps), $"IntArray=1;2;3" };

            IOperation[] operations = Executor.BuildOperations(args, typeof(ExecutorTests).Assembly, Options.Default);

            Assert.AreEqual(1, operations.Length);
            Assert.IsInstanceOfType(operations[0], typeof(OperationWithArrayProps));

            OperationWithArrayProps operation = (OperationWithArrayProps)operations[0];

            Assert.AreEqual(1, operation.IntArray[0]);
            Assert.AreEqual(2, operation.IntArray[1]);
            Assert.AreEqual(3, operation.IntArray[2]);
        }

        [TestMethod]
        public void When_IntArrayProperty_WithCustomSeparator_PropertyValuesAreSet()
        {
            string[] args = new string[] { nameof(OperationWithArrayProps), $"IntArray=1$2$3" };

            var options = Options.Default;
            options.ArrayElementSeparator = '$';
            IOperation[] operations = Executor.BuildOperations(args, typeof(ExecutorTests).Assembly, options);

            Assert.AreEqual(1, operations.Length);
            Assert.IsInstanceOfType(operations[0], typeof(OperationWithArrayProps));

            OperationWithArrayProps operation = (OperationWithArrayProps)operations[0];

            Assert.AreEqual(1, operation.IntArray[0]);
            Assert.AreEqual(2, operation.IntArray[1]);
            Assert.AreEqual(3, operation.IntArray[2]);
        }

        [TestMethod]
        public void When_ComplextArrayProperty_PropertyValuesAreSet()
        {
            string json = @"{
                IntValue: 123,
                StringValue: ""hello world"",
                BoolValue: true
            }";

            string[] args = new string[] { nameof(OperationWithArrayProps), $"ComplexArray={json};{json};{json}" };

            IOperation[] operations = Executor.BuildOperations(args, typeof(ExecutorTests).Assembly, Options.Default);

            Assert.AreEqual(1, operations.Length);
            Assert.IsInstanceOfType(operations[0], typeof(OperationWithArrayProps));

            OperationWithArrayProps operation = (OperationWithArrayProps)operations[0];

            Assert.AreEqual(123, operation.ComplexArray[0].IntValue);
            Assert.AreEqual("hello world", operation.ComplexArray[1].StringValue);
            Assert.AreEqual(true, operation.ComplexArray[2].BoolValue);
        }


        [TestMethod]
        public void When_ArgumentIsGuid_Is_CorrectlyParsed()
        {
            string[] args = new string[] { nameof(OperationWithGuidProp), "Id={17AA4272-9DA4-431E-9958-5069D4B34C2F}" };

            IOperation[] operations = Executor.BuildOperations(args, typeof(ExecutorTests).Assembly, Options.Default);

            Assert.AreEqual(1, operations.Length);
            Assert.IsInstanceOfType(operations[0], typeof(OperationWithGuidProp));

            OperationWithGuidProp operation = (OperationWithGuidProp)operations[0];

            Assert.AreEqual(new Guid("17AA4272-9DA4-431E-9958-5069D4B34C2F"), operation.Id);
        }

        [TestMethod]
        public void When_ArgumentIsNullable_Is_CorrectlyParsed()
        {
            string[] args = new string[] { nameof(OperationWithNullableProps), "SomeInt=123", "SomeGuid=27AA4272-9DA4-431E-9958-5069D4B34C2F" };

            IOperation[] operations = Executor.BuildOperations(args, typeof(ExecutorTests).Assembly, Options.Default);

            Assert.AreEqual(1, operations.Length);
            Assert.IsInstanceOfType(operations[0], typeof(OperationWithNullableProps));

            OperationWithNullableProps operation = (OperationWithNullableProps)operations[0];

            Assert.IsNotNull(operation.SomeInt);
            Assert.AreEqual(123, operation.SomeInt);
            Assert.IsNotNull(operation.SomeGuid);
            Assert.AreEqual(new Guid("27AA4272-9DA4-431E-9958-5069D4B34C2F"), operation.SomeGuid);
        }

        [TestMethod]
        public void When_ArgumentIsNullable_AndNotSet_IsLeftAsNull()
        {
            string[] args = new string[] { nameof(OperationWithNullableProps), "SomeInt=123", "SomeGuid=27AA4272-9DA4-431E-9958-5069D4B34C2F" };

            IOperation[] operations = Executor.BuildOperations(args, typeof(ExecutorTests).Assembly, Options.Default);

            Assert.AreEqual(1, operations.Length);
            Assert.IsInstanceOfType(operations[0], typeof(OperationWithNullableProps));

            OperationWithNullableProps operation = (OperationWithNullableProps)operations[0];

            Assert.IsNull(operation.SomeDate);           
        }

        [TestMethod]
        public void When_ArgumentIsNullableDate_Is_CorrectlyParsed()
        {
            string[] args = new string[] { nameof(OperationWithNullableProps), "SomeDate=2019-05-05" };

            IOperation[] operations = Executor.BuildOperations(args, typeof(ExecutorTests).Assembly, Options.Default);

            Assert.AreEqual(1, operations.Length);
            Assert.IsInstanceOfType(operations[0], typeof(OperationWithNullableProps));

            OperationWithNullableProps operation = (OperationWithNullableProps)operations[0];

            Assert.IsNotNull(operation.SomeDate);
            Assert.AreEqual(new DateTime(2019, 05, 05), operation.SomeDate);
        }

        [TestMethod]
        public void When_OperationHasAlias_Is_CorrectlyIdentified()
        {
            string[] args = new string[] { "a" };

            IOperation[] operations = Executor.BuildOperations(args, typeof(ExecutorTests).Assembly, Options.Default);

            Assert.AreEqual(1, operations.Length);
            Assert.IsInstanceOfType(operations[0], typeof(OperationWithAliases));
        }

        [TestMethod]
        public void When_OperationHasAlternateAlias_Is_CorrectlyIdentified()
        {
            string[] args = new string[] { "some_alias" };

            IOperation[] operations = Executor.BuildOperations(args, typeof(ExecutorTests).Assembly, Options.Default);

            Assert.AreEqual(1, operations.Length);
            Assert.IsInstanceOfType(operations[0], typeof(OperationWithAliases));
        }

        [TestMethod]
        public void When_OperationHasAlias_UsedWithDash_Is_CorrectlyIdentified()
        {
            string[] args = new string[] { "-a" };

            IOperation[] operations = Executor.BuildOperations(args, typeof(ExecutorTests).Assembly, Options.Default);

            Assert.AreEqual(1, operations.Length);
            Assert.IsInstanceOfType(operations[0], typeof(OperationWithAliases));
        }

        [TestMethod]
        public void When_OperationHasAlternateAlias_UsedWithDash_Is_CorrectlyIdentified()
        {
            string[] args = new string[] { "-some_alias" };

            IOperation[] operations = Executor.BuildOperations(args, typeof(ExecutorTests).Assembly, Options.Default);

            Assert.AreEqual(1, operations.Length);
            Assert.IsInstanceOfType(operations[0], typeof(OperationWithAliases));
        }

        [TestMethod]
        public void When_PropertyHasAlias_Is_CorrectlyIdentifiedAndSet()
        {
            string[] args = new string[] { "OperationWithAliasedProps", "nr=123" };

            IOperation[] operations = Executor.BuildOperations(args, typeof(ExecutorTests).Assembly, Options.Default);

            Assert.AreEqual(1, operations.Length);
            Assert.IsInstanceOfType(operations[0], typeof(OperationWithAliasedProps));

            OperationWithAliasedProps operation = (OperationWithAliasedProps)operations[0];

            Assert.AreEqual(123, operation.Number);
        }

        [TestMethod]
        public void When_PropertyHasAlternateAlias_Is_CorrectlyIdentifiedAndSet()
        {
            string[] args = new string[] { "OperationWithAliasedProps", "nmbr=123" };

            IOperation[] operations = Executor.BuildOperations(args, typeof(ExecutorTests).Assembly, Options.Default);

            Assert.AreEqual(1, operations.Length);
            Assert.IsInstanceOfType(operations[0], typeof(OperationWithAliasedProps));

            OperationWithAliasedProps operation = (OperationWithAliasedProps)operations[0];

            Assert.AreEqual(123, operation.Number);
        }

        [TestMethod]
        public void When_PropertyAlias_IsRequired__Is_CorrectlyIdentifiedAndSet()
        {
            string[] args = new string[] { "OperationWithAliasedRequiredProp", "nr=123" };

            IOperation[] operations = Executor.BuildOperations(args, typeof(ExecutorTests).Assembly, Options.Default);

            Assert.AreEqual(1, operations.Length);
            Assert.IsInstanceOfType(operations[0], typeof(OperationWithAliasedRequiredProp));

            OperationWithAliasedRequiredProp operation = (OperationWithAliasedRequiredProp)operations[0];

            Assert.AreEqual(123, operation.Number);
        }
    }
}