﻿using System.Threading;

namespace Eternity.Core.Tasks
{
    public abstract class BaseBackgroundTask : IBackgroundTask
    {
        public bool Running { get; private set; }

        public void Start()
        {
            if(Running)
                return;

            Running = true;
            Main();
        }

        public void Stop()
        {
            if(!Running)
                return;

            Running = false;
        }

        protected void Main()
        {
            while (Running)
            {
                
                Thread.Sleep(TickInterval);
            }
        }

        protected virtual bool ShouldRun => true;
        protected abstract int TickInterval { get; }
        protected abstract void Run();
    }
}