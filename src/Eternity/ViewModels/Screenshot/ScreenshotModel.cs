using System;
using System.Collections.Generic;
using Eternity.Models;

namespace Eternity.ViewModels.Screenshot
{
    public class ScreenshotModel : BaseModel
    {
        public string CurreentImagePath { get; set; }
        public List<DateTime> Days { get; set; }
        public List<DateTime> Screenshots { get; set; }

        public DateTime SelectedDate { get; set; } = DateTime.Now;
    }
}
