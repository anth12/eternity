using System.Collections.Generic;
using Eternity.Core.Utilities.Helpers;

namespace Eternity.Core.Repositories
{
    public class BaseRepository<TType>
    {
        protected BaseRepository()
        {
            var name = typeof(TType).Name;
            FileName = $"data/{name}.json";
        }

        private readonly string FileName;

        public List<TType> Get()
        {
            var data = AppDataHelper.LoadJson<List<TType>>(FileName);
            return data ?? new List<TType>();
        }

        public void Save(List<TType> data)
        {
            AppDataHelper.SaveJson(FileName, data);
        }
    }
}