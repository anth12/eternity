using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Input;
using Eternity.Core.Settings;
using Eternity.Core.Settings.Models;

namespace Eternity.ViewModels.Settings
{
    internal class SettingsViewModel : BaseViewModel<EternitySettings>
    {
        public SettingsViewModel()
        { 
            // When IsActive is changed, reload the settings- this will 
            // revert any unsaved changes when closing the settings
            OnPropertyChanged(nameof(IsActive), Load);

            ScreenshotQualities = Enum.GetValues(typeof(ScreenshotQuality)).OfType<ScreenshotQuality>();
        }

        public bool IsActive { get; set; }
        public IEnumerable<ScreenshotQuality> ScreenshotQualities { get; set; }

        #region Commands
        public ICommand SaveAndCloseCommand { get; set; }
        public ICommand ScreenshotFolderPickerCommand { get; set; }

        #region Event handlers
        protected void SaveAndClose()
        {
            SettingsBootstrapper.Persist();
            IsActive = false;
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
