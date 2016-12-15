using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using Eternity.Core.Settings;

namespace Eternity.Core.Screenshot
{
    public class ScreenshotService
    {
        /// <summary>
        /// Retrieves a list of Dates that contain screenshots
        /// </summary>
        public List<DateTime> GetDays()
        {
            if (!Directory.Exists(EternitySettings.Current.ScreenshotPath))
            {
                return new List<DateTime>();
            }

            var dateDirectories = Directory.GetDirectories(EternitySettings.Current.ScreenshotPath);

            return dateDirectories.Select(f => DateTime.ParseExact(f.Split('\\').Last(), "yyyy-MM-dd", null))
                .ToList();
        }

        /// <summary>
        /// Retrieves a list of screenshots for a given date
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public List<DateTime> GetScreenshots(DateTime date)
        {
            if (!Directory.Exists(EternitySettings.Current.ScreenshotPath))
            {
                return new List<DateTime>();
            }

            var screenshotDirectory = Path.Combine(EternitySettings.Current.ScreenshotPath, $"{date:yyyy-MM-dd}");
            var files = Directory.GetFiles(screenshotDirectory);

            return files.Select(f => DateTime.ParseExact(f.Split('\\').Last().Split('.').First(), "HH-mm-ss", null))
                .ToList();
        }

        /// <summary>
        /// Gets an image for a given time
        /// </summary>
        /// <returns></returns>
        public byte[] GetImageBytes(DateTime dateTime)
        {
            var screenshotPath = ScreenshotUtility.ScreenshotPath(dateTime);

            if (!File.Exists(screenshotPath))
                return null;

            return File.ReadAllBytes(screenshotPath);
        }

        /// <summary>
        /// Gets an image for a given time
        /// </summary>
        /// <returns></returns>
        public Image GetImage(DateTime dateTime)
        {
            var screenshotPath = ScreenshotUtility.ScreenshotPath(dateTime);

            if (!File.Exists(screenshotPath))
                return null;

            return Image.FromFile(screenshotPath);
        }

        /// <summary>
        /// Generates an image path for a screenshot time
        /// </summary>
        /// <returns></returns>
        public string GetImagePath(DateTime dateTime)
        {
            return ScreenshotUtility.ScreenshotPath(dateTime);
        }
    }
}
