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

        public static string ScreenshotDirectory =>
                EternitySettings.Current.ScreenshotPath
                    .Or(AppDataHelper.FilePath("", directory: "screenshots"));

        public static string ScreenshotPath(DateTime date)
        {
            return Path.Combine(ScreenshotDirectory, $"{date:yyyy-MM-dd HH-mm-ss}.jpg");
        }
        private static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            return ImageCodecInfo.GetImageDecoders()
                .FirstOrDefault(c => c.FormatID == format.Guid);
        }

        public static void CleanScreenshots()
        {
            // TODO add lock when browsing screenshots
            var removalDate = DateTime.Now.Add(-EternitySettings.Current.ScreenshotLifespan);

            var screenshotPaths = Directory.GetFiles(ScreenshotDirectory);

            foreach (var screenshotPath in screenshotPaths)
            {
                var dateString = screenshotPath
                                    .Split('\\').Last()
                                    .Split('.').First();
                var screenshotDate = DateTime.ParseExact(dateString, "yyyy-MM-dd HH-mm-ss", null);

                if (screenshotDate <= removalDate)
                {
                    try
                    {
                        File.Delete(screenshotPath);
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
