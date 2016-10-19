using System;
using System.Threading;
using System.Threading.Tasks;
using Eternity.Core.Settings;

namespace Eternity.Core.Screenshot
{
    public class ScreenshotBackgroundTask
    {
        public static bool Running { get; private set; }

        public static void EnsureRunning()
        {
            if (EternitySettings.Current.ScreenshotsEnabled)
            {
                if (Running)
                    return;

                Running = true;
                Task.Run(() => Tick());
            }
            else
            {
                Running = false;
            }
        }

        public static void Stop()
        {
            Running = false;
        }

        private static DateTime LastCleanedScreenshots = DateTime.MinValue;

        private static void Tick()
        {
            while (Running)
            {
                ScreenshotUtility.TakeScreenshot();

                if (LastCleanedScreenshots.Add(TimeSpan.FromHours(1)) >= DateTime.Now)
                {
                    // Clean old screenshots every hour
                    ScreenshotUtility.CleanScreenshots();
                    LastCleanedScreenshots = DateTime.Now;
                }

                Thread.Sleep(EternitySettings.Current.ScreenshotFrequency);
            }
        }
    }
}
