using System;
using System.Collections.Generic;

namespace Eternity.Core.Models
{
    public class SimpleWindowsApplicationRecord
    {
        public DateTime Time { get; set; }
        public List<SimpleWindowsApplication> Items { get; set; }  = new List<SimpleWindowsApplication>();
    }
}
