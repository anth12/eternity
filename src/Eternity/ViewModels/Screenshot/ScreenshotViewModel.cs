using System.Collections.Generic;
using System.Windows.Input;
using Eternity.Core.Screenshot;
using Eternity.Core.Settings;
using Eternity.Core.Settings.Models;
using Eternity.ViewModels.Home;

namespace Eternity.ViewModels.Screenshot
{
    internal class ScreenshotViewModel : HomeChildScreen<EternitySettings>
    {
        private readonly ScreenshotService _screenshotService;

        public ScreenshotViewModel(HomeViewModel parent, ScreenshotService screenshotService) : base(parent)
        {
            _screenshotService = screenshotService;
        }
        

        public IEnumerable<ScreenshotQuality> ScreenshotQualities { get; set; }

        #region Commands

        public ICommand PreviousCommand { get; set; }
        public ICommand NextCommand { get; set; }

        #region Event handlers

        public void Previous()
        {
            
        }

        public void Next()
        {
            
        }

        #endregion

        #endregion
        
    }
}
