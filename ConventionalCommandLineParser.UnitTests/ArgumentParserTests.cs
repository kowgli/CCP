using CCP.Models;
using CCP.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConventionalOperationLineParser.UnitTests
{
    [TestClass]
    public class ArgumentParserTests
    {
        [TestMethod]
        public void When_EmptyArgs_Returns_NoOperations()
        {
            string[] args = new string[0];

            Operation[] operations = ArgumentsParser.Parse(args);

            Assert.AreEqual(0, operations.Length);
        }

        [TestMethod]
        public void When_SingleOperationWithNoArguments_Returns_CorrectOperationName()
        {
            string[] args = new string[] { "TestOperation" };

            Operation[] operations = ArgumentsParser.Parse(args);

            Assert.AreEqual(1, operations.Length);
            Assert.AreEqual("TestOperation", operations[0].Name);
        }

        [TestMethod]
        public void When_SingleOperationWithNoArguments_Returns_OperationWithNoArguments()
        {
            string[] args = new string[] { "TestOperation" };

            Operation[] operations = ArgumentsParser.Parse(args);

            Assert.AreEqual(0, operations[0].Arguments.Length);
        }

        [TestMethod]
        public void When_MultipleOperationWithNoArguments_Returns_CorrectOperationNames()
        {
            string[] args = new string[] { "TestOperation1", "TestOperation2", "TestOperation3" };

            Operation[] operations = ArgumentsParser.Parse(args);

            Assert.AreEqual(3, operations.Length);
            Assert.AreEqual("TestOperation1", operations[0].Name);
            Assert.AreEqual("TestOperation2", operations[1].Name);
            Assert.AreEqual("TestOperation3", operations[2].Name);
        }

        [TestMethod]
        public void When_MultipleOperationsWithNoArguments_Returns_OperationsWithNoArguments()
        {
            string[] args = new string[] { "TestOperation1", "TestOperation2", "TestOperation3" };

            Operation[] operations = ArgumentsParser.Parse(args);

            Assert.AreEqual(0, operations[0].Arguments.Length);
            Assert.AreEqual(0, operations[1].Arguments.Length);
            Assert.AreEqual(0, operations[2].Arguments.Length);
        }

        [TestMethod]
        public void When_SingleOperationWithSingleArgument_Returns_ArgumentWithCorrectName()
        {
            string[] args = new string[] { "TestOperation", "Arg1=123" };

            Operation[] operations = ArgumentsParser.Parse(args);

            Assert.AreEqual(1, operations[0].Arguments.Length);
            Assert.AreEqual("Arg1", operations[0].Arguments[0].Name);
        }

        [TestMethod]
        public void When_SingleOperationWithSingleArgument_Returns_ArgumentWithCorrectValue()
        {
            string[] args = new string[] { "TestOperation", "Arg1=123" };

            Operation[] operations = ArgumentsParser.Parse(args);

            Assert.AreEqual(1, operations[0].Arguments.Length);
            Assert.AreEqual("123", operations[0].Arguments[0].Value);
        }

        [TestMethod]
        public void When_SingleOperationWithMultipleArguments_Returns_ArgumentsWithCorrectNames()
        {
            string[] args = new string[] { "TestOperation", "Arg1=123", "Arg2=aaaa", "Arg3=\"aaa bbbb\"" };

            Operation[] operations = ArgumentsParser.Parse(args);

            Assert.AreEqual(3, operations[0].Arguments.Length);
            Assert.AreEqual("Arg1", operations[0].Arguments[0].Name);
            Assert.AreEqual("Arg2", operations[0].Arguments[1].Name);
            Assert.AreEqual("Arg3", operations[0].Arguments[2].Name);
        }

        [TestMethod]
        public void When_SingleOperationWithMultipleArguments_Returns_ArgumentsWithCorrectValues()
        {
            string[] args = new string[] { "TestOperation", "Arg1=123", "Arg2=aaaa", "Arg3=\"aaa bbbb\"" };

            Operation[] operations = ArgumentsParser.Parse(args);

            Assert.AreEqual(3, operations[0].Arguments.Length);
            Assert.AreEqual("123", operations[0].Arguments[0].Value);
            Assert.AreEqual("aaaa", operations[0].Arguments[1].Value);
            Assert.AreEqual("\"aaa bbbb\"", operations[0].Arguments[2].Value);
        }

        [TestMethod]
        public void When_MultipleOperationsWithMultipleArguments_Returns_ArgumentsWithCorrectNames()
        {
            string[] args = new string[] { "TestOperation", "Arg1=123", "Arg2=aaaa", "Arg3=\"aaa bbbb\"", "TestOperation2", "Arg4=xxx", "Arg5={test:123}" };

            Operation[] operations = ArgumentsParser.Parse(args);

            Assert.AreEqual(3, operations[0].Arguments.Length);
            Assert.AreEqual("Arg1", operations[0].Arguments[0].Name);
            Assert.AreEqual("Arg2", operations[0].Arguments[1].Name);
            Assert.AreEqual("Arg3", operations[0].Arguments[2].Name);

            Assert.AreEqual(2, operations[1].Arguments.Length);
            Assert.AreEqual("Arg4", operations[1].Arguments[0].Name);
            Assert.AreEqual("Arg5", operations[1].Arguments[1].Name);           
        }

        [TestMethod]
        public void When_MultipleOperationsWithMultipleArguments_Returns_ArgumentsWithCorrectValues()
        {
            string[] args = new string[] { "TestOperation", "Arg1=123", "Arg2=aaaa", "Arg3=\"aaa bbbb\"", "TestOperation2", "Arg4=xxx", "Arg5={test:123}" };

            Operation[] operations = ArgumentsParser.Parse(args);

            Assert.AreEqual(3, operations[0].Arguments.Length);
            Assert.AreEqual("123", operations[0].Arguments[0].Value);
            Assert.AreEqual("aaaa", operations[0].Arguments[1].Value);
            Assert.AreEqual("\"aaa bbbb\"", operations[0].Arguments[2].Value);

            Assert.AreEqual(2, operations[1].Arguments.Length);
            Assert.AreEqual("xxx", operations[1].Arguments[0].Value);
            Assert.AreEqual("{test:123}", operations[1].Arguments[1].Value);
        }

        [TestMethod]
        public void When_ArgumentIsString_IsCorrectlyIdentified()
        {
            string[] args = new string[] { "TestOperation", "Arg1=123", "Arg2=aaaa", "Arg3=\"aaa bbbb\"" };

            Operation[] operations = ArgumentsParser.Parse(args);

            Assert.IsTrue(operations[0].Arguments[2].IsLiteralString);
        }

        [TestMethod]
        public void When_ArgumentIsNotString_IsCorrectlyIdentified()
        {
            string[] args = new string[] { "TestOperation", "Arg1=123", "Arg2=aaaa", "Arg3=\"aaa bbbb\"" };

            Operation[] operations = ArgumentsParser.Parse(args);

            Assert.IsFalse(operations[0].Arguments[1].IsLiteralString);
        }

        [TestMethod]
        public void When_ArgumentIsPotentialJson_IsCorrectlyIdentified()
        {
            string[] args = new string[] { "TestOperation", "Arg1=123", "Arg2=aaaa", "Arg3={test:123}" };

            Operation[] operations = ArgumentsParser.Parse(args);

            Assert.IsTrue(operations[0].Arguments[2].IsPotentialJson);
        }

        [TestMethod]
        public void When_ArgumentIsNotPotentialJson_IsCorrectlyIdentified()
        {
            string[] args = new string[] { "TestOperation", "Arg1=123", "Arg2=aaaa", "Arg3=\"aaa bbbb\"" };

            Operation[] operations = ArgumentsParser.Parse(args);

            Assert.IsFalse(operations[0].Arguments[1].IsPotentialJson);
        }

        [TestMethod]
        public void When_ArgumentIsArray_Returns_ArgumentsWithCorrectValues()
        {
            string[] args = new string[] { "TestOperation", "Arg1=aaa;bbb;ccc" };

            Operation[] operations = ArgumentsParser.Parse(args);

            Assert.AreEqual(3, operations[0].Arguments[0].Values.Length);
            Assert.AreEqual("aaa", operations[0].Arguments[0].Values[0]);
            Assert.AreEqual("bbb", operations[0].Arguments[0].Values[1]);
            Assert.AreEqual("ccc", operations[0].Arguments[0].Values[2]);
        }

        [TestMethod]
        public void When_ArgumentIsStringArrayWithSplitChar_Returns_ArgumentsWithCorrectValues()
        {
            string[] args = new string[] { "TestOperation", "Arg1=\"aaa;aaa\";bbb;\"ccc\"" };

            Operation[] operations = ArgumentsParser.Parse(args);

            Assert.AreEqual(3, operations[0].Arguments[0].Values.Length);
            Assert.AreEqual("\"aaa;aaa\"", operations[0].Arguments[0].Values[0]);
            Assert.AreEqual("bbb", operations[0].Arguments[0].Values[1]);
            Assert.AreEqual("\"ccc\"", operations[0].Arguments[0].Values[2]);
        }
    }
}
