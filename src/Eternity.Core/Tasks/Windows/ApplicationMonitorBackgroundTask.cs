namespace Eternity.Core.Tasks.Windows
{
    public class ApplicationMonitorBackgroundTask : BaseBackgroundTask, IPauseOnLock
    {
        protected override int TickInterval => 5000; // Every 5 seconds

        protected override void Run()
        {

        }
        
    }
}
