using System.Collections.Generic;
using System.Linq;
using Eternity.Core.Settings;
using Eternity.Core.Tasks;
using Microsoft.Win32;

namespace Eternity.Core
{
    public class Bootstrapper
    {
        private readonly IEnumerable<IBackgroundTask> _backgroundTasks;

        public Bootstrapper(IEnumerable<IBackgroundTask> backgroundTasks)
        {
            _backgroundTasks = backgroundTasks;
        }

        public void Setup()
        {
            /*
             * Setup the application
             */
            SettingsBootstrapper.Setup();
            

            // Start the background tasks
            foreach (var backgroundTask in _backgroundTasks)
            {
                backgroundTask.Start();
            }
            
            SystemEvents.SessionSwitch += SystemEvents_SessionSwitch; ;
        }

        private void SystemEvents_SessionSwitch(object sender, Microsoft.Win32.SessionSwitchEventArgs e)
        {
            if (e.Reason == SessionSwitchReason.SessionLock)
            {
                _backgroundTasks
                    .OfType<IPauseOnLock>()
                    .ToList()
                    .ForEach(backgrounddTask=> backgrounddTask.Stop());
            }
            else if(e.Reason == SessionSwitchReason.SessionUnlock)
            {
                _backgroundTasks
                    .OfType<IPauseOnLock>()
                    .ToList()
                    .ForEach(backgrounddTask => backgrounddTask.Start());
            }
        }
    }
}
