using Xunit;
using System.Threading.Tasks;
using YoutubePlaylistDownloader.Services;
using Microsoft.Extensions.DependencyInjection;

namespace YoutubePlaylistDownloader.Tests
{
    public class IntegrationTests
    {
        [Fact]
        public async Task CompleteDownloadFlow_ShouldWork()
        {
            // Arrange
            var services = ServiceConfiguration.ConfigureServices();
            var serviceProvider = services.BuildServiceProvider();
            var cacheService = serviceProvider.GetRequiredService<ISecureCacheService>();

            var settings = new DownloadSettings(
                "mp3", false, YoutubeHelpers.High720,
                false, false, false, false, "192",
                false, "en", false, false, 0, 0,
                false, true, false, true, 4, "$title", false, "mkv");

            var downloadPage = new DownloadPage(settings, Path.GetTempPath(), true);

            try
            {
                // Act
                await downloadPage.StartDownload("https://www.youtube.com/watch?v=test");

                // Assert
                Assert.True(downloadPage.CurrentProgressPercent >= 0);
                Assert.NotNull(downloadPage.CurrentTitle);
                Assert.NotNull(downloadPage.CurrentStatus);
            }
            finally
            {
                await downloadPage.Cancel();
                downloadPage.Dispose();
            }
        }
    }
} 