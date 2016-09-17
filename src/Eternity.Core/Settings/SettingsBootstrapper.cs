using System;
using Eternity.Core.Settings.Models;
using Eternity.Core.Utilities.Helpers;

namespace Eternity.Core.Settings
{
    public class SettingsBootstrapper
    {
        private const string SettingsFileName = "Settings.json";

        public static void Setup()
        {
            var settings = AppDataHelper.LoadJson<EternitySettings>(SettingsFileName);

            if (settings == null)
            {
                settings = new EternitySettings
                {
                    ScreenshotsEnabled = true,
                    ScreenshotFrequency = TimeSpan.FromMinutes(2),
                    ScreenshotQuality = ScreenshotQuality.Low
                };

                AppDataHelper.SaveJson(SettingsFileName, settings);
            }

            EternitySettings.Current = settings;
        }

        public static void Persist()
        {
            
            AppDataHelper.SaveJson(SettingsFileName, EternitySettings.Current);
        }
    }
}
