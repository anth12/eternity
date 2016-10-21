using Eternity.Core.Tasks;

namespace Eternity.Core.Tests.BackgroundTasks
{
    public class TaskSchedulingTests
    {
        public void Can_exwecute_task
    }

    internal class MockBackgroundTask : BaseBackgroundTask
    {
        protected override int TickInterval => 300;

        public int Executions;
        protected override void Run()
        {
            Executions += 1;
        }
    }
}
