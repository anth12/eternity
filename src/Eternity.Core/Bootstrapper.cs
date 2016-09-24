using Eternity.Core.Screenshot;
using Eternity.Core.Settings;

namespace Eternity.Core
{
    public class Bootstrapper
    {
        public static void Setup()
        {
            /*
             * Setup the application
             */
            SettingsBootstrapper.Setup();

            /*
             * Begin background tasks
             */
             ScreenshotBackgroundTask.EnsureRunning();
        }
    }
}
