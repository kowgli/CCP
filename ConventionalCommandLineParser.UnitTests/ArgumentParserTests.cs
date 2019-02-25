using ConventionalCommandLineParser.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConventionalCommandLineParser.UnitTests
{
    [TestClass]
    public class ArgumentParserTests
    {
        [TestMethod]
        public void When_EmptyArgs_Returns_NoCommands()
        {
            string[] args = new string[0];

            Command[] commands = ArgumentsParser.Parse(args);

            Assert.AreEqual(0, commands.Length);
        }

        [TestMethod]
        public void When_SingleCommandWithNoArguments_Returns_CorrectCommandName()
        {
            string[] args = new string[] { "TestCommand" };

            Command[] commands = ArgumentsParser.Parse(args);

            Assert.AreEqual(1, commands.Length);
            Assert.AreEqual("TestCommand", commands[0].Name);
        }

        [TestMethod]
        public void When_SingleCommandWithNoArguments_Returns_CommandWithNoArguments()
        {
            string[] args = new string[] { "TestCommand" };

            Command[] commands = ArgumentsParser.Parse(args);

            Assert.AreEqual(0, commands[0].Arguments.Length);
        }

        [TestMethod]
        public void When_MultipleCommandWithNoArguments_Returns_CorrectCommandNames()
        {
            string[] args = new string[] { "TestCommand1", "TestCommand2", "TestCommand3" };

            Command[] commands = ArgumentsParser.Parse(args);

            Assert.AreEqual(3, commands.Length);
            Assert.AreEqual("TestCommand1", commands[0].Name);
            Assert.AreEqual("TestCommand2", commands[1].Name);
            Assert.AreEqual("TestCommand3", commands[2].Name);
        }

        [TestMethod]
        public void When_MultipleCommandsWithNoArguments_Returns_CommandsWithNoArguments()
        {
            string[] args = new string[] { "TestCommand1", "TestCommand2", "TestCommand3" };

            Command[] commands = ArgumentsParser.Parse(args);

            Assert.AreEqual(0, commands[0].Arguments.Length);
            Assert.AreEqual(0, commands[1].Arguments.Length);
            Assert.AreEqual(0, commands[2].Arguments.Length);
        }

        [TestMethod]
        public void When_SingleCommandWithSingleArgument_Returns_ArgumentWithCorrectName()
        {
            string[] args = new string[] { "TestCommand", "Arg1=123" };

            Command[] commands = ArgumentsParser.Parse(args);

            Assert.AreEqual(1, commands[0].Arguments.Length);
            Assert.AreEqual("Arg1", commands[0].Arguments[0].Name);
        }

        [TestMethod]
        public void When_SingleCommandWithSingleArgument_Returns_ArgumentWithCorrectValue()
        {
            string[] args = new string[] { "TestCommand", "Arg1=123" };

            Command[] commands = ArgumentsParser.Parse(args);

            Assert.AreEqual(1, commands[0].Arguments.Length);
            Assert.AreEqual("123", commands[0].Arguments[0].Value);
        }

        [TestMethod]
        public void When_SingleCommandWithMultipleArguments_Returns_ArgumentsWithCorrectNames()
        {
            string[] args = new string[] { "TestCommand", "Arg1=123", "Arg2=aaaa", "Arg3=\"aaa bbbb\"" };

            Command[] commands = ArgumentsParser.Parse(args);

            Assert.AreEqual(3, commands[0].Arguments.Length);
            Assert.AreEqual("Arg1", commands[0].Arguments[0].Name);
            Assert.AreEqual("Arg2", commands[0].Arguments[1].Name);
            Assert.AreEqual("Arg3", commands[0].Arguments[2].Name);
        }

        [TestMethod]
        public void When_SingleCommandWithMultipleArguments_Returns_ArgumentsWithCorrectValues()
        {
            string[] args = new string[] { "TestCommand", "Arg1=123", "Arg2=aaaa", "Arg3=\"aaa bbbb\"" };

            Command[] commands = ArgumentsParser.Parse(args);

            Assert.AreEqual(3, commands[0].Arguments.Length);
            Assert.AreEqual("123", commands[0].Arguments[0].Value);
            Assert.AreEqual("aaaa", commands[0].Arguments[1].Value);
            Assert.AreEqual("\"aaa bbbb\"", commands[0].Arguments[2].Value);
        }

        [TestMethod]
        public void When_MultipleCommandsWithMultipleArguments_Returns_ArgumentsWithCorrectNames()
        {
            string[] args = new string[] { "TestCommand", "Arg1=123", "Arg2=aaaa", "Arg3=\"aaa bbbb\"", "TestCommand2", "Arg4=xxx", "Arg5={test:123}" };

            Command[] commands = ArgumentsParser.Parse(args);

            Assert.AreEqual(3, commands[0].Arguments.Length);
            Assert.AreEqual("Arg1", commands[0].Arguments[0].Name);
            Assert.AreEqual("Arg2", commands[0].Arguments[1].Name);
            Assert.AreEqual("Arg3", commands[0].Arguments[2].Name);

            Assert.AreEqual(2, commands[1].Arguments.Length);
            Assert.AreEqual("Arg4", commands[1].Arguments[0].Name);
            Assert.AreEqual("Arg5", commands[1].Arguments[1].Name);           
        }

        [TestMethod]
        public void When_MultipleCommandsWithMultipleArguments_Returns_ArgumentsWithCorrectValues()
        {
            string[] args = new string[] { "TestCommand", "Arg1=123", "Arg2=aaaa", "Arg3=\"aaa bbbb\"", "TestCommand2", "Arg4=xxx", "Arg5={test:123}" };

            Command[] commands = ArgumentsParser.Parse(args);

            Assert.AreEqual(3, commands[0].Arguments.Length);
            Assert.AreEqual("123", commands[0].Arguments[0].Value);
            Assert.AreEqual("aaaa", commands[0].Arguments[1].Value);
            Assert.AreEqual("\"aaa bbbb\"", commands[0].Arguments[2].Value);

            Assert.AreEqual(2, commands[1].Arguments.Length);
            Assert.AreEqual("xxx", commands[1].Arguments[0].Value);
            Assert.AreEqual("{test:123}", commands[1].Arguments[1].Value);
        }

        [TestMethod]
        public void When_ArgumentIsString_IsCorrectlyIdentified()
        {
            string[] args = new string[] { "TestCommand", "Arg1=123", "Arg2=aaaa", "Arg3=\"aaa bbbb\"" };

            Command[] commands = ArgumentsParser.Parse(args);

            Assert.IsTrue(commands[0].Arguments[2].IsLiteralString);
        }

        [TestMethod]
        public void When_ArgumentIsNotString_IsCorrectlyIdentified()
        {
            string[] args = new string[] { "TestCommand", "Arg1=123", "Arg2=aaaa", "Arg3=\"aaa bbbb\"" };

            Command[] commands = ArgumentsParser.Parse(args);

            Assert.IsFalse(commands[0].Arguments[1].IsLiteralString);
        }

        [TestMethod]
        public void When_ArgumentIsPotentialJson_IsCorrectlyIdentified()
        {
            string[] args = new string[] { "TestCommand", "Arg1=123", "Arg2=aaaa", "Arg3={test:123}" };

            Command[] commands = ArgumentsParser.Parse(args);

            Assert.IsTrue(commands[0].Arguments[2].IsPotentialJson);
        }

        [TestMethod]
        public void When_ArgumentIsNotPotentialJson_IsCorrectlyIdentified()
        {
            string[] args = new string[] { "TestCommand", "Arg1=123", "Arg2=aaaa", "Arg3=\"aaa bbbb\"" };

            Command[] commands = ArgumentsParser.Parse(args);

            Assert.IsFalse(commands[0].Arguments[1].IsPotentialJson);
        }
    }
}
