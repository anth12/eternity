using Eternity.Core.Tasks;
using FluentAssertions;
using System.Threading;
using Xunit;

namespace Eternity.Core.Tests.BackgroundTasks
{
    public class TaskSchedulingTests
    {
        [Fact]
        public void Can_exwecute_task()
        {
            var task = new MockBackgroundTask();
            task.Start();

            Thread.Sleep(55);

            task.Stop();

            task.Executions.Should().Be(3);
        }
    }

    internal class MockBackgroundTask : BaseBackgroundTask
    {
        protected override int TickInterval => 20;

        public int Executions;
        protected override void Run()
        {
            Executions += 1;
        }
    }
}
