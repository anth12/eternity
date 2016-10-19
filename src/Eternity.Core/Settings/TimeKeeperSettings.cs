using System;
using PropertyChanged;
using Eternity.Core.Settings.Models;

namespace Eternity.Core.Settings
{
    [ImplementPropertyChanged]
    public class EternitySettings
    {
        public static EternitySettings Current { get; set; }

        #region Screenshot
        public bool ScreenshotsEnabled { get; set; }
        public TimeSpan ScreenshotFrequency { get; set; }
        public string ScreenshotPath { get; set; }
        public ScreenshotQuality ScreenshotQuality { get; set; }
        public TimeSpan ScreenshotLifespan { get; set; }
        #endregion

        public DayOfWeek[] WorkingDays { get; set; }
        public TimeSpan WorkingTime { get; set; }
        public TimeSpan LunchBreak { get; set; }

        /// <summary>
        /// Active driver
        /// </summary>
        public string Driver { get; set; }
    }
}
