using System.Collections.Generic;
using System.Linq;

namespace Eternity.Core.Models
{
    public class SimpleWindowsApplication
    {
        public SimpleWindowsApplication()
        {
            
        }

        public SimpleWindowsApplication(bool hasFocus, string processName, string windowName)
        {
            HasFocus = hasFocus;
            ProcessName = processName;
            WindowName = windowName;
        }

        public bool HasFocus { get; set; }
        public string ProcessName { get; set; }
        public string WindowName { get; set; }

        public Project BestMatch(List<Project> projects)
        {
            if (HasFocus)
            {
                var projectsRequiringFocus =
                    projects.Where(
                        project =>
                            project.ProjectAssociations.Any(
                                association => association.MustBeTopMost && association.MatchesApplication(this)))
                    .ToList();

                if (projectsRequiringFocus.Any())
                    return projectsRequiringFocus.First();
            }

            // Perhaps use history or adidtional priority setting to break a tie
            return
                projects.FirstOrDefault(
                    project => project.ProjectAssociations.Any(association => association.MatchesApplication(this)));
        }
    }
}
