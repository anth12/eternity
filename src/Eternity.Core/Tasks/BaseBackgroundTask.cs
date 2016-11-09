using System.Timers;

namespace Eternity.Core.Tasks
{
    public abstract class BaseBackgroundTask : IBackgroundTask
    {
        protected BaseBackgroundTask()
        {
            Timer = new Timer
            {
                Interval = TickInterval
            };

            Timer.Elapsed += Main;
        }

        protected void Main(object sender, ElapsedEventArgs e)
        {
            Timer.Stop();

            Run();

            Timer.Start();
        }

        protected Timer Timer { get; set; }

        public bool Running => Timer.Enabled;

        public void Start()
        {
            Timer.Start();
        }

        public void Stop()
        {
            Timer.Stop();
        }
        

        protected virtual bool ShouldRun => true;
        protected abstract int TickInterval { get; }
        protected abstract void Run();
    }
}
