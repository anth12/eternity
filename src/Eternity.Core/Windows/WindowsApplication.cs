using System;
using Eternity.Core.Models;

namespace Eternity.Core.Windows
{
    public class WindowsApplication : SimpleWindowsApplication
    {
        public int ProcessId { get; set; }

        public DateTime StarTime { get; set; }
    }
    
}
