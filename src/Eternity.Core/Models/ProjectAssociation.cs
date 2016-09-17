using PropertyChanged;

namespace Eternity.Core.Models
{
    [ImplementPropertyChanged]
    public class ProjectAssociation
    {
        public bool MustBeTopMost { get; set; }
        public string ProcessName { get; set; }
        public string WindowTitle { get; set; }
    }
}