using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Eternity.Core.Driver;
using Eternity.Core.Models;

namespace Eternity.CsvDriver
{
    public class CSVEternityDriver : IEternityDriver
    {
        private static string ProjectsPath =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments), "Eternity\\Projects.csv");

        private static string TimeLogPath =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments), "Eternity\\TimeLog.csv");


        public async Task InsertTimeLogAsync(TimeLog timeLog)
        {
            if (!File.Exists(TimeLogPath))
                File.Create(TimeLogPath);

            var line = string.Join(",", timeLog.Project.Key, timeLog.StartTime, timeLog.EndTime, timeLog.TotalTime);

            File.AppendAllLines(TimeLogPath, new [] { line });
        }

        public async Task<List<Project>> GetProjectsAsync()
        {
            if (!File.Exists(ProjectsPath))
                return new List<Project>();

            var lines = File.ReadAllLines(ProjectsPath);

            var result = new List<Project>();
            foreach (var line in lines)
            {
                var columns = line.Split(',');

                result.Add(
                    new Project
                    {
                        Key = columns[0],
                        Name = columns[1],
                        Color = columns.Length >= 2 ? columns[2] : null,
                    }
                );
            }

            return result;
        }
    }
}
