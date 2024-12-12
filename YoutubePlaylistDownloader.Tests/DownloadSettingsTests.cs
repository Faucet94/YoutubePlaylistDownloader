using Xunit;
using YoutubePlaylistDownloader.Models;
using System.IO;

namespace YoutubePlaylistDownloader.Tests
{
    public class DownloadSettingsTests
    {
        [Fact]
        public void DownloadSettings_DefaultValues_ShouldBeValid()
        {
            // Arrange & Act
            var settings = new DownloadSettings(
                "mp3", false, YoutubeHelpers.High720, 
                false, false, false, false, "192", 
                false, "en", false, false, 0, 0, 
                false, true, false, true, 4, "$title", false, "mkv");

            // Assert
            Assert.Equal("mp3", settings.SaveFormat);
            Assert.Equal("192", settings.Bitrate);
            Assert.Equal("$title", settings.FilenamePattern);
            Assert.False(settings.AudioOnly);
            Assert.False(settings.DownloadCaptions);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("   ")]
        public void DownloadSettings_InvalidBitrate_ShouldUseDefault(string invalidBitrate)
        {
            // Arrange & Act
            var settings = new DownloadSettings(
                "mp3", false, YoutubeHelpers.High720,
                false, false, true, true, invalidBitrate,
                false, "en", false, false, 0, 0,
                false, true, false, true, 4, "$title", false, "mkv");

            // Assert
            Assert.Equal("192", settings.Bitrate); // Should use default
        }

        [Theory]
        [InlineData(-1, 10)]
        [InlineData(10, 5)]
        [InlineData(-5, -1)]
        public void DownloadSettings_InvalidSubsetIndexes_ShouldThrowException(int start, int end)
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentException>(() => new DownloadSettings(
                "mp3", false, YoutubeHelpers.High720,
                false, false, false, false, "192",
                false, "en", true, false, start, end,
                false, true, false, true, 4, "$title", false, "mkv"));
        }
    }
} 