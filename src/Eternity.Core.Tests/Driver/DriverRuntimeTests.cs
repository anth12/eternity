using System;
using System.IO;
using System.Linq;
using Eternity.Core.Driver;
using FluentAssertions;
using Xunit;

namespace Eternity.Core.Tests.Driver
{
    public class DriverRuntimeTests
    {
        private readonly DriverService _driverService = new DriverService();

        [Fact]
        public void Can_load_driver()
        {
            // 1. Test the driver is not already loaded
            AppDomain.CurrentDomain.GetAssemblies()
                .Count(a => a.FullName.StartsWith("Eternity.ExampleDriver,"))
                .Should().Be(0);

            // 2. Ensure the driver is registered
            var driverPath = Path.Combine(Directory.GetCurrentDirectory(), "../../../Eternity.ExampleDriver/bin/Debug/Eternity.ExampleDriver.dll");
            var result = _driverService.AddDriver(driverPath);
            
            // 3. Test the driver is now loaded in the App Domain
            var driverAssembly = AppDomain.CurrentDomain.GetAssemblies()
                .Single(a => a.FullName.StartsWith("Eternity.ExampleDriver,"));

            driverAssembly
                .GetTypes()
                .Count(t => t.GetInterfaces().Any(i => i.Name == nameof(IEternityDriver)))
                .Should().BeGreaterOrEqualTo(1);

            var drivers = _driverService.GetDrivers();
            drivers.Count().Should().Be(1);
        }

        public void Can_update_driver()
        {
            // TODO drivers need to be removed from AppDomain before added to prevent duplicates
        }
    }
}
