using System.IO;
using Eternity.Core.Driver;
using FluentAssertions;
using Xunit;

namespace Eternity.Core.Tests.Driver
{
    public class DriverRegistrationTests
    {
        private readonly DriverService _driverService = new DriverService();

        [Fact]
        public void Can_validate_invalid_driver_path()
        {
            var result = _driverService.AddDriver("C:\\test\\12345678\\My.Driver.dll");
            result.Should().Contain("could not be found");
        }

        [Fact]
        public void Can_validate_file_type()
        {
            var driverPath = Path.Combine(Directory.GetCurrentDirectory(), "../../../Eternity.ExampleDriver/bin/Debug/Eternity.ExampleDriver.pdb");
            var result = _driverService.AddDriver(driverPath);
            result.Should().Contain("Unrecognised file type `pdb`");
        }

        [Fact]
        public void Can_validate_implements_interface()
        {
            var driverPath = Path.Combine(Directory.GetCurrentDirectory(), "../../../Eternity.ExampleDriver/bin/Debug/Newtonsoft.Json.dll");
            var result = _driverService.AddDriver(driverPath);
            result.Should().Contain("Could not locate any implementations of `IEternityDriver`");
        }

        [Fact]
        public void Can_load_valid_driver()
        {
            var driverPath = Path.Combine(Directory.GetCurrentDirectory(), "../../../Eternity.ExampleDriver/bin/Debug/Eternity.ExampleDriver.dll");
            var result = _driverService.AddDriver(driverPath);
            result.Should().BeNull();
        }
    }
}
