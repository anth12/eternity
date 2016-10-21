using System;
using Eternity.Core.Settings;
using Eternity.Core.Tasks;

namespace Eternity.Core.Screenshot
{
    public class ScreenshotBackgroundTask : BaseBackgroundTask, IPauseOnLock
    {
        protected override bool ShouldRun => EternitySettings.Current.ScreenshotsEnabled;
        protected override int TickInterval => EternitySettings.Current.ScreenshotFrequency.Milliseconds;
        
        private static DateTime LastCleanedScreenshots = DateTime.MinValue;

        protected override void Run()
        {
            ScreenshotUtility.TakeScreenshot();

            if (LastCleanedScreenshots.Add(TimeSpan.FromHours(1)) >= DateTime.Now)
            {
                // Clean old screenshots every hour
                ScreenshotUtility.CleanScreenshots();
                LastCleanedScreenshots = DateTime.Now;
            }
        }

    }
}
