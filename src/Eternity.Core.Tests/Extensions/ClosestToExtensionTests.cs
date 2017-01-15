using System;
using System.Collections.Generic;
using Eternity.Core.Extensions;
using FluentAssertions;
using Xunit;

namespace Eternity.Core.Tests.Extensions
{
    public class ClosestToExtensionTests
    {
        [Fact]
        public void Can_find_next_closest_date()
        {
            var sameple = new List<DateTime>
            {
                new DateTime(2018, 1, 1, 12, 5, 0),
                new DateTime(2018, 1, 1, 11, 50, 0),
                new DateTime(2018, 1, 1, 11, 54, 59),
                new DateTime(2018, 1, 1, 12, 5, 1),
            };

            var closestDate = sameple.ClosestTo(new DateTime(2018, 1, 1, 12, 0, 0));

            closestDate.Should().Be(new DateTime(2018, 1, 1, 12, 5, 0));
        }

        [Fact]
        public void Can_find_previous_closest_date()
        {
            var sameple = new List<DateTime>
            {
                new DateTime(2018, 1, 1, 12, 5, 1),
                new DateTime(2018, 1, 1, 11, 50, 0),
                new DateTime(2018, 1, 1, 11, 55, 0),
                new DateTime(2018, 1, 1, 11, 54, 59),
            };

            var closestDate = sameple.ClosestTo(new DateTime(2018, 1, 1, 12, 0, 0));

            closestDate.Should().Be(new DateTime(2018, 1, 1, 11, 55, 0));
        }

    }
}
