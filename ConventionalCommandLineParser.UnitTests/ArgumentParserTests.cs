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

            Operation[] Operations = ArgumentsParser.Parse(args);

            Assert.AreEqual(0, Operations.Length);
        }

        [TestMethod]
        public void When_SingleOperationWithNoArguments_Returns_CorrectOperationName()
        {
            string[] args = new string[] { "TestOperation" };

            Operation[] Operations = ArgumentsParser.Parse(args);

            Assert.AreEqual(1, Operations.Length);
            Assert.AreEqual("TestOperation", Operations[0].Name);
        }

        [TestMethod]
        public void When_SingleOperationWithNoArguments_Returns_OperationWithNoArguments()
        {
            string[] args = new string[] { "TestOperation" };

            Operation[] Operations = ArgumentsParser.Parse(args);

            Assert.AreEqual(0, Operations[0].Arguments.Length);
        }

        [TestMethod]
        public void When_MultipleOperationWithNoArguments_Returns_CorrectOperationNames()
        {
            string[] args = new string[] { "TestOperation1", "TestOperation2", "TestOperation3" };

            Operation[] Operations = ArgumentsParser.Parse(args);

            Assert.AreEqual(3, Operations.Length);
            Assert.AreEqual("TestOperation1", Operations[0].Name);
            Assert.AreEqual("TestOperation2", Operations[1].Name);
            Assert.AreEqual("TestOperation3", Operations[2].Name);
        }

        [TestMethod]
        public void When_MultipleOperationsWithNoArguments_Returns_OperationsWithNoArguments()
        {
            string[] args = new string[] { "TestOperation1", "TestOperation2", "TestOperation3" };

            Operation[] Operations = ArgumentsParser.Parse(args);

            Assert.AreEqual(0, Operations[0].Arguments.Length);
            Assert.AreEqual(0, Operations[1].Arguments.Length);
            Assert.AreEqual(0, Operations[2].Arguments.Length);
        }

        [TestMethod]
        public void When_SingleOperationWithSingleArgument_Returns_ArgumentWithCorrectName()
        {
            string[] args = new string[] { "TestOperation", "Arg1=123" };

            Operation[] Operations = ArgumentsParser.Parse(args);

            Assert.AreEqual(1, Operations[0].Arguments.Length);
            Assert.AreEqual("Arg1", Operations[0].Arguments[0].Name);
        }

        [TestMethod]
        public void When_SingleOperationWithSingleArgument_Returns_ArgumentWithCorrectValue()
        {
            string[] args = new string[] { "TestOperation", "Arg1=123" };

            Operation[] Operations = ArgumentsParser.Parse(args);

            Assert.AreEqual(1, Operations[0].Arguments.Length);
            Assert.AreEqual("123", Operations[0].Arguments[0].Value);
        }

        [TestMethod]
        public void When_SingleOperationWithMultipleArguments_Returns_ArgumentsWithCorrectNames()
        {
            string[] args = new string[] { "TestOperation", "Arg1=123", "Arg2=aaaa", "Arg3=\"aaa bbbb\"" };

            Operation[] Operations = ArgumentsParser.Parse(args);

            Assert.AreEqual(3, Operations[0].Arguments.Length);
            Assert.AreEqual("Arg1", Operations[0].Arguments[0].Name);
            Assert.AreEqual("Arg2", Operations[0].Arguments[1].Name);
            Assert.AreEqual("Arg3", Operations[0].Arguments[2].Name);
        }

        [TestMethod]
        public void When_SingleOperationWithMultipleArguments_Returns_ArgumentsWithCorrectValues()
        {
            string[] args = new string[] { "TestOperation", "Arg1=123", "Arg2=aaaa", "Arg3=\"aaa bbbb\"" };

            Operation[] Operations = ArgumentsParser.Parse(args);

            Assert.AreEqual(3, Operations[0].Arguments.Length);
            Assert.AreEqual("123", Operations[0].Arguments[0].Value);
            Assert.AreEqual("aaaa", Operations[0].Arguments[1].Value);
            Assert.AreEqual("\"aaa bbbb\"", Operations[0].Arguments[2].Value);
        }

        [TestMethod]
        public void When_MultipleOperationsWithMultipleArguments_Returns_ArgumentsWithCorrectNames()
        {
            string[] args = new string[] { "TestOperation", "Arg1=123", "Arg2=aaaa", "Arg3=\"aaa bbbb\"", "TestOperation2", "Arg4=xxx", "Arg5={test:123}" };

            Operation[] Operations = ArgumentsParser.Parse(args);

            Assert.AreEqual(3, Operations[0].Arguments.Length);
            Assert.AreEqual("Arg1", Operations[0].Arguments[0].Name);
            Assert.AreEqual("Arg2", Operations[0].Arguments[1].Name);
            Assert.AreEqual("Arg3", Operations[0].Arguments[2].Name);

            Assert.AreEqual(2, Operations[1].Arguments.Length);
            Assert.AreEqual("Arg4", Operations[1].Arguments[0].Name);
            Assert.AreEqual("Arg5", Operations[1].Arguments[1].Name);           
        }

        [TestMethod]
        public void When_MultipleOperationsWithMultipleArguments_Returns_ArgumentsWithCorrectValues()
        {
            string[] args = new string[] { "TestOperation", "Arg1=123", "Arg2=aaaa", "Arg3=\"aaa bbbb\"", "TestOperation2", "Arg4=xxx", "Arg5={test:123}" };

            Operation[] Operations = ArgumentsParser.Parse(args);

            Assert.AreEqual(3, Operations[0].Arguments.Length);
            Assert.AreEqual("123", Operations[0].Arguments[0].Value);
            Assert.AreEqual("aaaa", Operations[0].Arguments[1].Value);
            Assert.AreEqual("\"aaa bbbb\"", Operations[0].Arguments[2].Value);

            Assert.AreEqual(2, Operations[1].Arguments.Length);
            Assert.AreEqual("xxx", Operations[1].Arguments[0].Value);
            Assert.AreEqual("{test:123}", Operations[1].Arguments[1].Value);
        }

        [TestMethod]
        public void When_ArgumentIsString_IsCorrectlyIdentified()
        {
            string[] args = new string[] { "TestOperation", "Arg1=123", "Arg2=aaaa", "Arg3=\"aaa bbbb\"" };

            Operation[] Operations = ArgumentsParser.Parse(args);

            Assert.IsTrue(Operations[0].Arguments[2].IsLiteralString);
        }

        [TestMethod]
        public void When_ArgumentIsNotString_IsCorrectlyIdentified()
        {
            string[] args = new string[] { "TestOperation", "Arg1=123", "Arg2=aaaa", "Arg3=\"aaa bbbb\"" };

            Operation[] Operations = ArgumentsParser.Parse(args);

            Assert.IsFalse(Operations[0].Arguments[1].IsLiteralString);
        }

        [TestMethod]
        public void When_ArgumentIsPotentialJson_IsCorrectlyIdentified()
        {
            string[] args = new string[] { "TestOperation", "Arg1=123", "Arg2=aaaa", "Arg3={test:123}" };

            Operation[] Operations = ArgumentsParser.Parse(args);

            Assert.IsTrue(Operations[0].Arguments[2].IsPotentialJson);
        }

        [TestMethod]
        public void When_ArgumentIsNotPotentialJson_IsCorrectlyIdentified()
        {
            string[] args = new string[] { "TestOperation", "Arg1=123", "Arg2=aaaa", "Arg3=\"aaa bbbb\"" };

            Operation[] Operations = ArgumentsParser.Parse(args);

            Assert.IsFalse(Operations[0].Arguments[1].IsPotentialJson);
        }
    }
}
