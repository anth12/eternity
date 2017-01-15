using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Eternity.Core.Models;
using Eternity.Core.Utilities.Helpers;
using Newtonsoft.Json;

namespace Eternity.Core.Repositories
{
    public class BaseRepository<TType>
        where TType : BaseModel
    {
        protected BaseRepository()
        {
            var name = typeof(TType).Name;
            FileName = AppDataHelper.FilePath($"{name}.json", "data");

            if (File.Exists(FileName))
            {
                // Load the data
                Load();
            }
            else
            {
                File.Create(FileName);
            }
        }

        protected virtual bool SplitOnDate { get; }

        private readonly string FileName;
        private readonly IDictionary<Guid, TType> Collection = new ConcurrentDictionary<Guid, TType>();

        public List<TType> GetAll()
        {
            return Collection.Values.ToList();
        }

        public void Save()
        {
            var json = JsonConvert.SerializeObject(GetAll());
            File.WriteAllText(FileName, json);
        }

        public void Load()
        {
            Collection.Clear();

            var file = File.ReadAllText(FileName);

            JsonConvert.DeserializeObject<List<TType>>(file)
                .ForEach(item=> Collection[item.Id] = item);
        }

        public void Add(List<TType> data)
        {
            
            AppDataHelper.SaveJson(FileName, data);
        }
    }
}