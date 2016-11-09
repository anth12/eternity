using PropertyChanged;

namespace Eternity.Core.Models
{
    [ImplementPropertyChanged]
    public class ProjectAssociation
    {
        public ProjectAssociation()
        {
            
        }

        public ProjectAssociation(bool mustBeTopMost, string processName, string windowTitle)
        {
            MustBeTopMost = mustBeTopMost;
            ProcessName = processName;
            WindowTitle = windowTitle;
        }

        public bool MustBeTopMost { get; set; }
        public string ProcessName { get; set; }
        public string WindowTitle { get; set; }

        public bool MatchesApplication(SimpleWindowsApplication application)
        {
            return application.ProcessName == ProcessName
                   && application.WindowName.ToLower().Contains(WindowTitle.ToLower())
                   && (!MustBeTopMost || application.HasFocus);
        }

    }
}