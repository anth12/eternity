using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Autofac;
using Eternity.ViewModels.Dashboard;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Eternity.ViewModels.Settings;

namespace Eternity.ViewModels.Home
{
    internal class HomeViewModel : BaseViewModel
    {
        public void Init()
        {
            Views = new ObservableCollection<IViewModel>
            {
                App.Container.Resolve<DashboardViewModel>(),
                App.Container.Resolve<SettingsViewModel>()
            };
            
            Navigate<DashboardViewModel>();
        }

        public ObservableCollection<IViewModel> Views { get; set; } 
        public IViewModel ActiveView { get; set; }
        

        #region Commands
        public ICommand AboutCommand { get; set; }
        public ICommand HelpCommand { get; set; }
        #endregion

        #region Event handlers
        
        protected void About()
        {
            var assembly = Assembly.GetExecutingAssembly();

            ((MetroWindow)Application.Current.MainWindow).ShowMessageAsync($"Time Tracker Version: {assembly.GetName().Version}", "By Anthony Halliday");
        }

        protected void Help()
        {
            Process.Start("https://github.com/anth12/Eternity/wiki");
        }
        
        #endregion

        public void Navigate<TViewModel>()
            where TViewModel : IViewModel
        {
            ActiveView = Views.OfType<TViewModel>().Single();
        }
    }
}
