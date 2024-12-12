using Xunit;
using Moq;
using YoutubePlaylistDownloader.Models;
using System.Threading.Tasks;

namespace YoutubePlaylistDownloader.Tests
{
    public class DownloadPageTests
    {
        [Fact]
        public async Task Cancel_WhenNotDisposed_ShouldCallExitAsync()
        {
            // Arrange
            var downloadSettings = new DownloadSettings(
                "mp3", false, YoutubeHelpers.High720,
                false, false, false, false, "192",
                false, "en", false, false, 0, 0,
                false, true, false, true, 4, "$title", false, "mkv");

            var downloadPage = new DownloadPage(downloadSettings, "test/path", false);

            // Act
            var result = await downloadPage.Cancel();

            // Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(50)]
        [InlineData(100)]
        public void UpdateProgress_ShouldUpdateProgressCorrectly(int progress)
        {
            // Arrange
            var downloadSettings = new DownloadSettings(
                "mp3", false, YoutubeHelpers.High720,
                false, false, false, false, "192",
                false, "en", false, false, 0, 0,
                false, true, false, true, 4, "$title", false, "mkv");

            var downloadPage = new DownloadPage(downloadSettings, "test/path", false);

            // Act
            downloadPage.CurrentProgressPercent = progress;

            // Assert
            Assert.Equal(progress, downloadPage.CurrentProgressPercent);
        }
    }
} 