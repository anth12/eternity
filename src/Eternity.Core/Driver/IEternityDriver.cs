using System.Collections.Generic;
using System.Threading.Tasks;
using Eternity.Core.Models;

namespace Eternity.Core.Driver
{
    public interface IEternityDriver
    {
        Task InsertTimeLogAsync(TimeLog timeLog);

        Task<List<Project>> GetProjectsAsync();
    }
}
