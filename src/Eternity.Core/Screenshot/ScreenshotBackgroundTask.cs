
using System;
using System.Threading;
using System.Threading.Tasks;
using Eternity.Core.Settings;

namespace Eternity.Core.Screenshot
{
    public class ScreenshotBackgroundTask
    {
        public static void Run()
        {
            if (EternitySettings.Current.ScreenshotsEnabled)
            {
                Task.Run(()=> Tick());
            }
        }

        private static DateTime LastCleanedScreenshots = DateTime.MinValue;

        private static void Tick()
        {
            ScreenshotUtility.TakeScreenshot();

            if (LastCleanedScreenshots.Add(TimeSpan.FromHours(1)) >= DateTime.Now)
            {
                // Clean old screenshots every hour
                ScreenshotUtility.CleanScreenshots();
                LastCleanedScreenshots = DateTime.Now;
            }

            Thread.Sleep(EternitySettings.Current.ScreenshotFrequency);

            Tick();
        }
    }
}
