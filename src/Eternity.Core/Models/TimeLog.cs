
using System;
using PropertyChanged;

namespace Eternity.Core.Models
{
    [ImplementPropertyChanged]
    public class TimeLog
    {
        public Project Project { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public TimeSpan TotalTime => EndTime - StartTime;
    }
}
