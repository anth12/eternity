using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Eternity.Core.Settings;

namespace Eternity.Core.Screenshot
{
    public class ScreenshotService
    {
        /// <summary>
        /// Retrieves a list of Dates that contain at least screenshot
        /// </summary>
        public List<DateTime> GetDays()
        {
            return GetDates()
                        .GroupBy(d => d.Date)
                        .Select(g => g.Key)
                        .ToList();
        }

        public List<DateTime> GetDates()
        {
            if (!Directory.Exists(EternitySettings.Current.ScreenshotPath))
            {
                return new List<DateTime>();
            }

            var files = Directory.GetFiles(EternitySettings.Current.ScreenshotPath);

            return files.Select(f => DateTime.ParseExact(f.Split('\\').Last().Split('.').First(), "yyyy-MM-dd HH-mm-ss", null))
                .ToList();
        }

        public byte[] Get(DateTime dateTime)
        {
            var screenshotPath = ScreenshotUtility.ScreenshotPath(dateTime);

            if (!File.Exists(screenshotPath))
                return null;

            return File.ReadAllBytes(screenshotPath);
        }
    }
}
