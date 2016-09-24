using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Input;
using Eternity.Core.Screenshot;
using Eternity.Core.Settings;
using Eternity.Core.Settings.Models;
using Eternity.ViewModels.Dashboard;
using Eternity.ViewModels.Home;

namespace Eternity.ViewModels.Settings
{
    internal class SettingsViewModel : HomeChildScreen<EternitySettings>
    {
        public SettingsViewModel(HomeViewModel parent) : base(parent)
        {
            // When IsActive is changed, reload the settings- this will 
            // revert any unsaved changes when closing the settings
            // OnPropertyChanged(nameof(IsActive), Load);
            PropertyChanged += (sender, args) =>
            {
                // TODO auto-save
            };
            ScreenshotQualities = Enum.GetValues(typeof(ScreenshotQuality)).OfType<ScreenshotQuality>();
        }
        

        public IEnumerable<ScreenshotQuality> ScreenshotQualities { get; set; }

        #region Commands
        public ICommand SaveAndCloseCommand { get; set; }
        public ICommand ScreenshotFolderPickerCommand { get; set; }

        #region Event handlers
        protected void SaveAndClose()
        {
            // Ensure the Screenshot util is started/stopped
            ScreenshotBackgroundTask.EnsureRunning();
            SettingsBootstrapper.Persist();
            Parent.Navigate<DashboardViewModel>();
        }

        protected void ScreenshotFolderPicker()
        {
            var folderPicker = new FolderBrowserDialog
            {
                SelectedPath = Model.ScreenshotPath
            };

            var result = folderPicker.ShowDialog();

            if (result == DialogResult.OK)
            {
                Model.ScreenshotPath = folderPicker.SelectedPath;
            }
        }
        #endregion

        #endregion

        public void Load()
        {
            SettingsBootstrapper.Setup();
            Model = EternitySettings.Current;
        }
    }
}
