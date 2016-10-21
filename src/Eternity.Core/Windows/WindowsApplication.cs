using System;
using System.Diagnostics;

namespace Eternity.Core.Windows
{
    public class WindowsApplication
    {
        public int ProcessId { get; set; }
        public string ProcessName { get; set; }
        public string WindowName { get; set; }

        public DateTime StarTime { get; set; }
        public Process Process { get; set; }
    }
}
