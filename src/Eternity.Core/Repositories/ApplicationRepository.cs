using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Eternity.Core.Extensions;
using Eternity.Core.Models;
using Eternity.Core.Utilities.Helpers;

namespace Eternity.Core.Repositories
{
    public class ApplicationRepository
    {
        public ApplicationRepository()
        {
            Date = DateTime.Today;
        }

        public ApplicationRepository(DateTime date)
        {
            Date = date;
        }

        public DateTime Date;

        public List<SimpleWindowsApplicationRecord> GetApplications()
        {
            var result = new List<SimpleWindowsApplicationRecord>();
            using (var fileStream = File.OpenText(FilePath()))
            {
                string line;
                while ((line = fileStream.ReadLine()) != null)
                {
                    result.Add(line.ParseJson<SimpleWindowsApplicationRecord>());
                }
            }
            
            return result;
        } 

        public void Append(IEnumerable<SimpleWindowsApplication> applications)
        {
            var item = new SimpleWindowsApplicationRecord {Time = DateTime.Now, Items = applications.ToList()};
            var jsonData = item.ToJson();
            File.AppendAllLines(FilePath(), new [] { jsonData });
        }

        #region

        private string FilePath()
        {
            return AppDataHelper.FilePath($"{Date.ToString("yy-MM-dd")}.json", "repo\\applications");
        }

        #endregion
    }
}
