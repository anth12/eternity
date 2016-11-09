using System.Collections.Generic;
using Eternity.Core.Models;
using Eternity.Core.Repositories;

namespace Eternity.Core.Tests.Mock
{
    public class MockProjectRepository : ProjectRepository
    {
        private static List<Project> projects;

        public new List<Project> GetAll()
        {
            if (projects != null)
                return projects;

            return projects = new List <Project>
            {
                new Project
                {
                    Key = "SMP-1",
                    Name = "Sample One",
                    Color = "#fff",
                    ProjectAssociations =
                    {
                        new ProjectAssociation(false, "devenv.exe", "Sample.One"),
                        new ProjectAssociation(false, "chrome.exe", "Sample One"),
                    }
                },

                new Project
                {
                    Key = "SMP-2",
                    Name = "Sample 2",
                    Color = "#333",
                    ProjectAssociations =
                    {
                        new ProjectAssociation(false, "devenv.exe", "Sample.Two"),
                        new ProjectAssociation(false, "Code.exe", "sample-two"),
                    }
                }

            };
        }
    }
}
