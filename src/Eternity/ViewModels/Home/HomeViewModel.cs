using System.Diagnostics;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using Autofac;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Eternity.Models.Home;
using Eternity.ViewModels.Settings;

namespace Eternity.ViewModels.Home
{
    internal class HomeViewModel : BaseViewModel<HomeModel>
    {
        public HomeViewModel()
        {
            Model = new HomeModel();
            Settings = App.Container.Resolve<SettingsViewModel>();
        }

        public SettingsViewModel Settings { get; set; }

        #region Commands
        public ICommand OpenSettingsCommand { get; set; }
        
        public ICommand AboutCommand { get; set; }
        public ICommand HelpCommand { get; set; }
        #endregion

        #region Event handlers
        protected void OpenSettings()
        {
            Settings.IsActive = !Settings.IsActive;
        }

        protected void About()
        {
            var assembly = Assembly.GetExecutingAssembly();

            ((MetroWindow)Application.Current.MainWindow).ShowMessageAsync($"Time Tracker Version: {assembly.GetName().Version}", "By Anthony Halliday");
        }

        protected void Help()
        {
            Process.Start("https://github.com/anth12/ReVersion/wiki");
        }
        
        #endregion
    }
}
