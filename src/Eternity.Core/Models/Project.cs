using System.Collections.Generic;
using PropertyChanged;

namespace Eternity.Core.Models
{
    [ImplementPropertyChanged]
    public class Project : BaseModel
    {
        public string Key { get; set; }
        public string Name { get; set; }

        public string Color { get; set; }

        public List<ProjectAssociation> ProjectAssociations { get; set; } = new List<ProjectAssociation>();

    }
}
