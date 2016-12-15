using System.Collections.Generic;
using System.Windows.Input;
using Eternity.Core.Extensions;
using Eternity.Core.Screenshot;
using Eternity.Core.Settings;
using Eternity.Core.Settings.Models;
using Eternity.ViewModels.Home;

namespace Eternity.ViewModels.Screenshot
{
    internal class ScreenshotViewModel : HomeChildScreen<ScreenshotModel>
    {
        private readonly ScreenshotService _screenshotService;

        public ScreenshotViewModel(HomeViewModel parent, ScreenshotService screenshotService) : base(parent)
        {
            _screenshotService = screenshotService;
            Model = new ScreenshotModel();

            RefreshModel();
        }
        

        public IEnumerable<ScreenshotQuality> ScreenshotQualities { get; set; }

        #region Commands

        public ICommand PreviousCommand { get; set; }
        public ICommand NextCommand { get; set; }

        #region Event handlers

        public void Previous()
        {
            Model.SelectedDate = Model.Screenshots.Previous(Model.SelectedDate);
            LoadImage();
        }

        public void Next()
        {
            Model.SelectedDate = Model.Screenshots.Previous(Model.SelectedDate);
            LoadImage();
        }

        #endregion

        #endregion

        private void RefreshModel()
        {
            Model.Days = _screenshotService.GetDays();
            Model.Screenshots = _screenshotService.GetScreenshots(Model.SelectedDate);

            // If the selected Date/Time is not available, select the closest one
            if (!Model.Screenshots.Contains(Model.SelectedDate))
            {
                Model.SelectedDate = Model.Screenshots.ClosestTo(Model.SelectedDate);
            }

            LoadImage();
        }

        private void LoadImage()
        {
            Model.CurreentImagePath = _screenshotService.GetImagePath(Model.SelectedDate);
        }
    }
}
