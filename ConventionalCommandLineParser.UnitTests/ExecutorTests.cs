using ConventionalCommandLineParser.UnitTests.MockExecutors;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConventionalCommandLineParser.UnitTests
{
    [TestClass]
    public class ExecutorTests
    {
        [TestMethod]
        public void When_NoArguments_BuildsCorrectExecutable()
        {
            string[] args = new string[] { };

            IExecutable[] executables = Executor.BuildExecutables(args, typeof(ExecutorTests).Assembly);

            Assert.AreEqual(0, executables.Length);
        }

        [TestMethod]
        public void When_CommandWithNoArgs_BuildsCorrectExecutable()
        {
            string[] args = new string[] { "CommandWithNoArgs" };

            IExecutable[] executables = Executor.BuildExecutables(args, typeof(ExecutorTests).Assembly);

            Assert.AreEqual(1, executables.Length);
            Assert.IsTrue(executables[0] is CommandWithNoArgs);
        }

        [TestMethod]
        public void When_CommandWithArgs_ProperyValuesAreSet()
        {
            string[] args = new string[] { "CommandWithSimpleArgs", "Arg1=test", "Arg2=3", "Arg3=123.45" };

            IExecutable[] executables = Executor.BuildExecutables(args, typeof(ExecutorTests).Assembly);

            Assert.AreEqual(1, executables.Length);
            Assert.IsTrue(executables[0] is CommandWithSimpleArgs);

            CommandWithSimpleArgs command = (CommandWithSimpleArgs) executables[0];

            Assert.AreEqual("test", command.Arg1);
            Assert.AreEqual(3, command.Arg2);
            Assert.AreEqual(123.45M, command.Arg3);
        }
    }
}