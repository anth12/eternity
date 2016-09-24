using System.Collections.Generic;
using Eternity.Core.Settings;
using Eternity.Core.Settings.Models;
using Eternity.ViewModels.Home;

namespace Eternity.ViewModels.Screenshot
{
    internal class ScreenshotViewModel : HomeChildScreen<EternitySettings>
    {
        public ScreenshotViewModel(HomeViewModel parent) : base(parent)
        {

        }
        

        public IEnumerable<ScreenshotQuality> ScreenshotQualities { get; set; }

        #region Commands


        #region Event handlers
        

        #endregion

        #endregion
        
    }
}
