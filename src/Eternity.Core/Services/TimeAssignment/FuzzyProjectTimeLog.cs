using System;
using System.Collections.Generic;
using Eternity.Core.Models;

namespace Eternity.Core.Services.TimeAssignment
{
    public class FuzzyProjectTimeLog
    {
        public DateTime Time { get; set; }
        public List<Project> Projects { get; set; }
        public SimpleWindowsApplication Application { get; set; }
    }
}
