<<<<<<< HEAD
﻿using Eternity.Core.Tasks;
using FluentAssertions;
using System.Threading;
=======
﻿using System.Threading;
using Eternity.Core.Tasks;
using FluentAssertions;
>>>>>>> origin/master
using Xunit;

namespace Eternity.Core.Tests.BackgroundTasks
{
    public class TaskSchedulingTests
    {
        [Fact]
<<<<<<< HEAD
        public void Can_exwecute_task()
=======
        public void Can_execute_task()
>>>>>>> origin/master
        {
            var task = new MockBackgroundTask();
            task.Start();

<<<<<<< HEAD
            Thread.Sleep(55);

            task.Stop();

            task.Executions.Should().Be(3);
=======
            Thread.Sleep(505);

            task.Stop();

            task.Executions.Should().Be(9);
>>>>>>> origin/master
        }
    }

    internal class MockBackgroundTask : BaseBackgroundTask
    {
<<<<<<< HEAD
        protected override int TickInterval => 20;
=======
        protected override int TickInterval => 50;
>>>>>>> origin/master

        public int Executions;
        protected override void Run()
        {
            Executions += 1;
        }
    }
}
