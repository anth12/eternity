
using FluentAssertions;
using Xunit;

namespace Eternity.Core.Tests.Extensions
{
    public class StringExtensionTests
    {
        [Theory]
        [InlineData(@"c:\some folder\another_directory\example-file.jpg", "example-file")]
        [InlineData(@"c:\some folder\another.directory\example.file.jpg", "example.file")]
        [InlineData(@"c:\example file.jpg", "example file")]
        public void Can_parse_file_name_from_path(string path, string expectedFileName)
        {
            var fileName = path.PathFileName();

            fileName.Should().Be(expectedFileName);
        }
    }
}
