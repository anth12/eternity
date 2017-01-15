using System;
using System.Linq;
using Eternity.Core.Models;
using Eternity.Core.Repositories;
using Eternity.Core.Windows;

namespace Eternity.Core.Tasks.Windows
{
    public class ApplicationMonitorBackgroundTask : BaseBackgroundTask, IPauseOnLock
    {
        private readonly ApplicationRepository _repository;

        public ApplicationMonitorBackgroundTask(ApplicationRepository repository)
        {
            _repository = repository;
        }

        protected override int TickInterval => 10000; // Every 10 seconds

        protected override void Run()
        {
            _repository.Date = DateTime.Today;
            
            var applications = ApplicationHelper.GetOpenApplications().Select(a=> new SimpleWindowsApplication
            {
                HasFocus = a.HasFocus,
                WindowName = a.WindowName,
                ProcessName = a.ProcessName
            });

            _repository.Append(applications);
        }
        
    }
}
