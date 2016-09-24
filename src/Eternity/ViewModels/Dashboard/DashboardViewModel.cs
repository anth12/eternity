using System.Windows;
using System.Windows.Input;
using Eternity.ViewModels.Home;
using Eternity.ViewModels.Screenshot;
using Eternity.ViewModels.Settings;

namespace Eternity.ViewModels.Dashboard
{
    internal class DashboardViewModel : HomeChildScreen<DashboardModel>
    {
        public DashboardViewModel(HomeViewModel parent)
            : base(parent)
        {
        }

        public ICommand ScreenshotsCommand { get; set; }
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
        public void Screenshots()
        {
            Parent.Navigate<ScreenshotViewModel>();
        }

        public void Settings()
        {
            Parent.Navigate<SettingsViewModel>();
        }
    }
}
