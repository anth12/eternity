using System;
using System.Collections.Generic;
using Eternity.Core.Models;
using Eternity.Core.Services.TimeAssignment;
using Eternity.Core.Tests.Mock;
using Xunit;

namespace Eternity.Core.Tests.Services.TimeAssignment
{
    public class TimeAssignmentServiceTests
    {
        private static readonly MockProjectRepository _mockProjectRepository = new MockProjectRepository();
        private static readonly TimeAssignmentService _timeAssignmentService = new TimeAssignmentService(_mockProjectRepository);

        [Fact]
        public void Can()
        {
            var projects = _mockProjectRepository.GetAll();
            var sample1 = projects[0];
            var sample2 = projects[1];


            var result = _timeAssignmentService.CreateTimeLog(MockWindowRecords());

        }

        private List<SimpleWindowsApplicationRecord> MockWindowRecords()
        {
            var projects = _mockProjectRepository.GetAll();
            var sample1 = projects[0];
            var sample2 = projects[1];

            var chromeWithFocus = new SimpleWindowsApplication(true, "chrome.EXE", "New Tab - Google Chrome");
            var chromeWithoutFocus = new SimpleWindowsApplication(false, "chrome.EXE", "Home | Sample One | Lorem ipsum loro sit amet");

            var outlookWithFocus = new SimpleWindowsApplication(true, "OUTLOOK.EXE", "Inbox - example@example.com - Outlooks");
            var outlookWithoutFocus = new SimpleWindowsApplication(false, "OUTLOOK.EXE", "Inbox - example@example.com - Outlooks");

            var sample1VsWithFocus = new SimpleWindowsApplication(true, "devenv.exe", "Sample.One - Microsoft Visual Studio (Administrator)");
            var sample1VsWithoutFocus = new SimpleWindowsApplication(false, "devenv.exe", "Sample.One - Microsoft Visual Studio (Administrator)");

            var sample2VsWithFocus = new SimpleWindowsApplication(true, "devenv.exe", "Sample.Two - Microsoft Visual Studio (Administrator)");
            var sample2VsWithoutFocus = new SimpleWindowsApplication(false, "devenv.exe", "Sample.Two - Microsoft Visual Studio (Administrator)");

            var sample2VsCodeWithFocus = new SimpleWindowsApplication(true, "Code.exe", "Example.js - sample-two - Visual Studio Code");
            var sample2VsCodeWithoutFocus = new SimpleWindowsApplication(false, "Code.exe", "Example.js - sample-two - Visual Studio Code");

            var spotifyWithFocus = new SimpleWindowsApplication(true, "Spotify.exe", "Kiasmos - Swept");
            var spotifyWithoutFocus = new SimpleWindowsApplication(false, "Spotify.exe", "Ólafur Arnalds - Erla's Waltz");

            return new List<SimpleWindowsApplicationRecord>
            {
                new SimpleWindowsApplicationRecord
                {
                    Time = new DateTime(2016, 6, 0, 9, 0, 0),
                    Items =
                    {
                        chromeWithFocus,
                        outlookWithoutFocus, spotifyWithoutFocus, sample1VsWithoutFocus, sample2VsCodeWithoutFocus
                    }
                },
                new SimpleWindowsApplicationRecord
                {
                    Time = new DateTime(2016, 6, 0, 9, 0, 30),
                    Items =
                    {
                        outlookWithFocus,
                        chromeWithoutFocus, spotifyWithoutFocus, sample1VsWithoutFocus, sample2VsCodeWithoutFocus
                    }
                },
                new SimpleWindowsApplicationRecord
                {
                    Time = new DateTime(2016, 6, 0, 9, 1, 0),
                    Items =
                    {
                        sample1VsWithFocus,
                        chromeWithoutFocus, outlookWithFocus, spotifyWithoutFocus, sample2VsCodeWithoutFocus
                    }
                },
                new SimpleWindowsApplicationRecord
                {
                    Time = new DateTime(2016, 6, 0, 9, 1, 0),
                    Items =
                    {
                        sample1VsWithFocus,
                        chromeWithoutFocus, outlookWithFocus, spotifyWithoutFocus, sample2VsCodeWithoutFocus
                    }
                },
                new SimpleWindowsApplicationRecord
                {
                    Time = new DateTime(2016, 6, 0, 9, 0, 0),
                    Items =
                    {
                        chromeWithFocus,
                        outlookWithoutFocus, spotifyWithoutFocus, sample1VsWithoutFocus, sample2VsCodeWithoutFocus
                    }
                },
            };
        }
    }
}
