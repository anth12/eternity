
using System;
using System.Collections.Generic;
using System.Linq;
using Eternity.Core.Models;
using Eternity.Core.Repositories;

namespace Eternity.Core.Services.TimeAssignment
{
    public class TimeAssignmentService
    {
        private readonly ProjectRepository projectRepository;

        public TimeAssignmentService(ProjectRepository projectRepository)
        {
            this.projectRepository = projectRepository;
        }

        private static TimeSpan ChangeThreashhold = TimeSpan.FromMinutes(1);

        public List<TimeLog> CreateTimeLog(List<SimpleWindowsApplicationRecord> applicationRecords)
        {

            var fuzzyMatches = GetFuzzyWorkingProjects(applicationRecords);

            var activeProjects = RefineFuzzyMatches(fuzzyMatches);

            return AssignProjectsToTimeLog(activeProjects);
        }

        #region

        /// <summary>
        /// Maps the open applications to any possible matching projects
        /// </summary>
        /// <param name="applicationRecords"></param>
        /// <returns></returns>
        private List<FuzzyProjectTimeLog> GetFuzzyWorkingProjects(
            List<SimpleWindowsApplicationRecord> applicationRecords)
        {
            var projects = projectRepository.GetAll();

            return (from applicationRecordSet in applicationRecords
                    from simpleWindowsApplication in applicationRecordSet.Items
                    let activeProjects = projects
                                                .Where(project => project.ProjectAssociations.Any(association => association.MatchesApplication(simpleWindowsApplication))).ToList()
                    where activeProjects.Any()
                    select new FuzzyProjectTimeLog
                    {
                        Time = applicationRecordSet.Time,
                        Projects = activeProjects,
                        Application = simpleWindowsApplication
                    }).ToList();
        }

        #endregion

        #region OLD- Private methods

        //private List<FuzzyProjectTimeLog> GetFuzzyWorkingProjects(
        //    List<SimpleWindowsApplicationRecord> applicationRecords)
        //{
        //    var projects = projectRepository.GetAll();

        //    return (from applicationRecordSet in applicationRecords
        //        from simpleWindowsApplication in applicationRecordSet.Items
        //            let activeProjects = projects
        //                                        .Where(project => project.ProjectAssociations.Any(association => association.MatchesApplication(simpleWindowsApplication))).ToList()
        //        where activeProjects.Any()
        //        select new FuzzyProjectTimeLog
        //        {
        //            Time = applicationRecordSet.Time,
        //            Projects = activeProjects,
        //            Application = simpleWindowsApplication
        //        }).ToList();
        //}
        
        private Dictionary<DateTime, Project> RefineFuzzyMatches(List<FuzzyProjectTimeLog> fuzzyMatches)
        {
            var result = new Dictionary<DateTime, Project>();

            foreach (var fuzzyProjectTimeLog in fuzzyMatches)
            {
                var matchingProject = fuzzyProjectTimeLog.Application.BestMatch(fuzzyProjectTimeLog.Projects);

                result.Add(fuzzyProjectTimeLog.Time, matchingProject);
            }

            return result;
        }

        private List<TimeLog> AssignProjectsToTimeLog(Dictionary<DateTime, Project> activeProjects)
        {
            // Remove entries of the same project
            var keys = activeProjects.Keys.ToList();
            var keysToRemove = new List<DateTime>();

            var index = 0;
            foreach (var key in keys)
            {
                if(index <= 1)
                    continue;

                var previousKey = keys[index - 1];

                if (activeProjects[key].Id == activeProjects[previousKey].Id)
                {
                    keysToRemove.Add(previousKey);
                }
                index ++;
            }

            // Remove the unnecessary keys
            keysToRemove.ForEach(key => activeProjects.Remove(key));


            // Map to the Time log
            var result = new List<TimeLog>();
            // TODO - use Change Threshold
            foreach (var projectLog in activeProjects)
            {
                result.Add(new TimeLog
                {
                    Project = projectLog.Value,
                    StartTime = projectLog.Key
                });
            }

            return result;
        }

        #endregion

    }
}
