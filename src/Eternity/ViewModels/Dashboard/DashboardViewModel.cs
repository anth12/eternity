using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Eternity.ViewModels.Home;
using Eternity.ViewModels.Projects;
using Eternity.ViewModels.Settings;

namespace Eternity.ViewModels.Dashboard
{
    internal class DashboardViewModel : BaseViewModel<DashboardModel>
    {
        protected readonly HomeViewModel Parent;

        public DashboardViewModel(HomeViewModel parent)
        {
            Parent = parent;
        }

        public ICommand CurrentDayCommand { get; set; }
        public ICommand CurrentWeekCommand { get; set; }
        public ICommand ProjectsCommand { get; set; }
        public ICommand SettingsCommand { get; set; }

        public void CurrentDay()
        {
            MessageBox.Show("Current Day");
        }

        public void CurrentWeek()
        {
            MessageBox.Show("Current Week");
        }

        public void Projects()
        {

            //Parent.Navigate<ProjectsViewModel>();
        }

        public void Settings()
        {
            Parent.Navigate<SettingsViewModel>();
        }
    }
}
