using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using Eternity.Core.Settings;
using Eternity.Core.Utilities.Helpers;

namespace Eternity.Core.Screenshot
{
    public class ScreenshotUtility
    {
        public static void TakeScreenshot()
        {
            // Determine the size of the "virtual screen", which includes all monitors.
            var screenLeft = SystemInformation.VirtualScreen.Left;
            var screenTop = SystemInformation.VirtualScreen.Top;
            var screenWidth = SystemInformation.VirtualScreen.Width;
            var screenHeight = SystemInformation.VirtualScreen.Height;

            // Create a bitmap of the appropriate size to receive the screenshot.
            using (var bmp = new Bitmap(screenWidth, screenHeight))
            {
                // Draw the screenshot into our bitmap.
                using (var g = Graphics.FromImage(bmp))
                {
                    g.CopyFromScreen(screenLeft, screenTop, 0, 0, bmp.Size);
                }

                var jpgEncoder = GetEncoder(ImageFormat.Jpeg);
                var encoder = Encoder.Quality;
                
                var encoderParameters = new EncoderParameters(1);
                var encoderParameter = new EncoderParameter(encoder, (int)EternitySettings.Current.ScreenshotQuality);
                encoderParameters.Param[0] = encoderParameter;
                
                // Do something with the Bitmap here, like save it to a file:
                bmp.Save(ScreenshotPath(DateTime.Now), jpgEncoder, encoderParameters);
            }
        }

        /// <summary>
        /// Returns either the user configured screenshot directory or the default AppData directory
        /// </summary>
        public static string ScreenshotDirectory =>
                EternitySettings.Current.ScreenshotPath
                    .Or(AppDataHelper.FilePath("", directory: "screenshots"));

        /// <summary>
        /// Generates a Date based screenshot directory (non-existent directories are created)
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string ScreenshotPath(DateTime date)
        {
            var directory = Path.Combine(ScreenshotDirectory, $"{date:yyyy-MM-dd}");

            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            return Path.Combine(directory, $"{date:HH-mm-ss}.jpg");
        }

        private static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            return ImageCodecInfo.GetImageDecoders()
                .FirstOrDefault(c => c.FormatID == format.Guid);
        }

        /// <summary>
        /// Clears screenshots older that the configurable history length of time
        /// </summary>
        public static void CleanScreenshots()
        {
            // TODO add lock when browsing screenshots
            var removalDate = DateTime.Now.Add(-EternitySettings.Current.ScreenshotLifespan);

            var screenshotDirectories = Directory.GetDirectories(ScreenshotDirectory);

            foreach (var screenshotDirectory in screenshotDirectories)
            {
                var dateString = screenshotDirectory
                                    .Split('\\').Last();
                var screenshotDate = DateTime.ParseExact(dateString, "yyyy-MM-dd", null);

                if (screenshotDate <= removalDate)
                {
                    try
                    {
                        Directory.Delete(screenshotDirectory, true);
                    }
                    catch (Exception ex)
                    {
                        // TODO log
                    }
                }
            }
        }
    }
}
