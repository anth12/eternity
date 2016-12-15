using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Input;
using Eternity.Core.Driver;
using Eternity.Core.Screenshot;
using Eternity.Core.Settings;
using Eternity.Core.Settings.Models;
using Eternity.ViewModels.Dashboard;
using Eternity.ViewModels.Home;

namespace Eternity.ViewModels.Settings
{
    internal class SettingsViewModel : HomeChildScreen<EternitySettings>
    {

        private readonly DriverService _driverService;
        private readonly ScreenshotBackgroundTask _screenshotBackgroundTask;

        public SettingsViewModel(HomeViewModel parent, DriverService driverService, ScreenshotBackgroundTask screenshotBackgroundTask) : base(parent)
        {
            _driverService = driverService;
            _screenshotBackgroundTask = screenshotBackgroundTask;

            // When IsActive is changed, reload the settings- this will 
            // revert any unsaved changes when closing the settings
            // OnPropertyChanged(nameof(IsActive), Load);
            PropertyChanged += (sender, args) =>
            {
                // TODO auto-save
            };
            ScreenshotQualitiesList = Enum.GetValues(typeof(ScreenshotQuality)).OfType<ScreenshotQuality>();
            Load();
            PopulateDriversList();
        }
        

        public IEnumerable<ScreenshotQuality> ScreenshotQualitiesList { get; set; }
        public ObservableCollection<string> DriversList { get; set; } = new ObservableCollection<string>();

        #region Commands
        public ICommand SaveAndCloseCommand { get; set; }
        public ICommand ScreenshotFolderPickerCommand { get; set; }


        public ICommand AddDriverCommand { get; set; }
        public ICommand RemoveDriverCommand { get; set; }

        #region Event handlers
        protected void SaveAndClose()
        {
            // Ensure the Screenshot util is started/stopped
            if(EternitySettings.Current.ScreenshotsEnabled)
                _screenshotBackgroundTask.Start();
            else
                _screenshotBackgroundTask.Stop();

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

        protected void AddDriver()
        {
            var filePicker = new OpenFileDialog();

            var result = filePicker.ShowDialog();

            if (result == DialogResult.OK && filePicker.FileName.IsNotBlank())
            {
                var error = _driverService.AddDriver(filePicker.FileName);
                if (error.IsBlank())
                {
                    PopulateDriversList();
                    Model.Driver = filePicker.FileName.PathFileName();
                    return;
                }

                MessageBox.Show(error); // TODO use metro theme
            }
        }

        protected void RemoveDriver()
        {
            if (Model.Driver.IsNotBlank())
            {
                _driverService.RemoveDriver(Model.Driver);
                PopulateDriversList();
                Model.Driver = "";
            }
        }

        #endregion

        #endregion

        public void Load()
        {
            SettingsBootstrapper.Setup();
            Model = EternitySettings.Current;
        }

        private void PopulateDriversList()
        {
            DriversList.Clear();

            
        }
    }
}
