<<<<<<< HEAD
﻿using System.Threading;
using System.Threading.Tasks;
=======
﻿using System.Timers;
>>>>>>> origin/master

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

<<<<<<< HEAD
            Running = true;
            Task.Run(()=> Main());
=======
            Timer.Elapsed += Main;
>>>>>>> origin/master
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
