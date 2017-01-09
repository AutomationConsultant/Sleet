﻿using System.IO;
using System.Threading.Tasks;
using NuGet.Test.Helpers;
using Xunit;

namespace Sleet.Test
{
    public class InitCommandTests
    {
        [Fact]
        public async Task InitCommand_BasicAsync()
        {
            using (var target = new TestFolder())
            using (var cache = new LocalCache())
            {
                // Arrange
                var log = new TestLogger();
                var fileSystem = new PhysicalFileSystem(cache, UriUtility.CreateUri(target.Root));
                var settings = new LocalSettings();

                var indexJsonOutput = new FileInfo(Path.Combine(target.Root, "index.json"));
                var settingsOutput = new FileInfo(Path.Combine(target.Root, "sleet.settings.json"));
                var autoCompleteOutput = new FileInfo(Path.Combine(target.Root, "autocomplete", "query"));
                var catalogOutput = new FileInfo(Path.Combine(target.Root, "catalog", "index.json"));
                var searchOutput = new FileInfo(Path.Combine(target.Root, "search", "query"));
                var packageIndexOutput = new FileInfo(Path.Combine(target.Root, "sleet.packageindex.json"));

                // Act
                var exitCode = await InitCommand.RunAsync(settings, fileSystem, log);

                // Assert
                Assert.True(exitCode);
                Assert.True(indexJsonOutput.Exists);
                Assert.True(settingsOutput.Exists);
                Assert.True(autoCompleteOutput.Exists);
                Assert.True(catalogOutput.Exists);
                Assert.True(searchOutput.Exists);
                Assert.True(packageIndexOutput.Exists);
            }
        }
    }
}