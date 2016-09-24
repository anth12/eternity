using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using Autofac;
using Eternity.ViewModels.Dashboard;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace Eternity.ViewModels.Home
{
    internal class HomeViewModel : BaseViewModel
    {

        public void Init()
        {
            var childViewModels = App.Container.Resolve<IEnumerable<IHomeChildScreen>>();
            Views = new ObservableCollection<IViewModel>(childViewModels);

            Navigate<DashboardViewModel>();
        }

        public ObservableCollection<IViewModel> Views { get; set; } 
        public IViewModel ActiveView { get; set; }
        

        #region Commands
        public ICommand AboutCommand { get; set; }
        public ICommand HelpCommand { get; set; }
        public ICommand PixelForgeCommand { get; set; }
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

        protected void PixelForge()
        {
            Process.Start("http://thepixelforge.co.uk");
        }

        #endregion

        public void Navigate<TViewModel>()
            where TViewModel : IViewModel
        {
            ActiveView = Views.OfType<TViewModel>().Single();
        }
    }
}
