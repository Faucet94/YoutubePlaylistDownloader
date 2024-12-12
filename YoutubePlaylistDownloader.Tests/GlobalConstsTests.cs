using Xunit;
using System.IO;

namespace YoutubePlaylistDownloader.Tests
{
    public class GlobalConstsTests
    {
        [Fact]
        public void GlobalConsts_Initialization_ShouldSetupCorrectPaths()
        {
            // Assert
            Assert.NotNull(GlobalConsts.CurrentDir);
            Assert.NotNull(GlobalConsts.TempFolderPath);
            Assert.NotNull(GlobalConsts.FFmpegFilePath);
            Assert.NotNull(GlobalConsts.VERSION);
        }

        [Fact]
        public void DownloadSettings_ShouldNotBeNull()
        {
            // Act
            var settings = GlobalConsts.DownloadSettings;

            // Assert
            Assert.NotNull(settings);
            Assert.NotNull(settings.SaveFormat);
            Assert.NotNull(settings.Bitrate);
            Assert.NotNull(settings.FilenamePattern);
        }

        [Fact]
        public void OppositeTheme_ShouldToggleCorrectly()
        {
            // Arrange
            GlobalConsts.settings.Theme = "Light";

            // Act & Assert
            Assert.Equal("Dark", GlobalConsts.OppositeTheme);

            // Change theme
            GlobalConsts.settings.Theme = "Dark";
            Assert.Equal("Light", GlobalConsts.OppositeTheme);
        }
    }
} 