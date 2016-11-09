using System.Threading;
using Eternity.Core.Tasks;
using FluentAssertions;
using Xunit;

namespace Eternity.Core.Tests.BackgroundTasks
{
    public class TaskSchedulingTests
    {
        [Fact]
        public void Can_execute_task()
        {
            var task = new MockBackgroundTask();
            task.Start();

            Thread.Sleep(505);

            task.Stop();

            task.Executions.Should().Be(9);
        }
    }

    internal class MockBackgroundTask : BaseBackgroundTask
    {
        protected override int TickInterval => 50;

        public int Executions;
        protected override void Run()
        {
            Executions += 1;
        }
    }
}
