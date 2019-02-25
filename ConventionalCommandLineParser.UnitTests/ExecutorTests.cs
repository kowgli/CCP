using ConventionalCommandLineParser.UnitTests.MockExecutors;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConventionalCommandLineParser.UnitTests
{
    [TestClass]
    public class ExecutorTests
    {
        [TestMethod]
        public void When_NoArguments_DoesNotRun()
        {
            Executor.ExecuteFromArgs(typeof(ExecutorTests).Assembly, new string[] { });

            Assert.AreEqual(0, ExecutionState.Executions.Count);
        }

        [TestMethod]
        public void When_MethodWithNoArgs_DoesExecute()
        {
            Executor.ExecuteFromArgs(typeof(ExecutorTests).Assembly, new string[] { nameof(CommandWithNoArgs) });

            var executions = ExecutionState.Executions.ToArray();

            Assert.AreEqual(1, executions.Length);
            Assert.AreEqual(nameof(CommandWithNoArgs), executions[0].Command);
        }
    }
}