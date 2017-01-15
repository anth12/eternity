using System.Collections.Generic;
using System.Threading.Tasks;
using Eternity.Core.Driver;
using Eternity.Core.Models;

namespace Eternity.ExampleDriver
{
    public class ExampleJsonEternityDriver : IEternityDriver
    {
        public async Task InsertTimeLogAsync(TimeLog timeLog)
        {
            await Task.Delay(1000);
        }

        public async Task<List<Project>> GetProjectsAsync()
        {
            await Task.Delay(1000);

            return new List<Project>
            {
                new Project {Name = "Example Project 1"},
                new Project {Name = "Example Project 2"},
                new Project {Name = "Example Project 3"}
            };
        }
    }
}
