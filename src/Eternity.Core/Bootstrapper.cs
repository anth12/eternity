using System.Collections.Generic;
using Eternity.Core.Settings;
using Eternity.Core.Tasks;

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
        }
    }
}
