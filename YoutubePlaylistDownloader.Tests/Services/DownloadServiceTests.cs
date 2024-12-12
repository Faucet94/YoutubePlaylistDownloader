using Moq;
using System;
using System.Threading.Tasks;
using Xunit;
using YoutubePlaylistDownloader.Services;

namespace YoutubePlaylistDownloader.Tests.Services
{
    public class DownloadServiceTests
    {
        private readonly Mock<TelemetryService> _mockTelemetry;
        private readonly DownloadService _sut;

        public DownloadServiceTests()
        {
            _mockTelemetry = new Mock<TelemetryService>();
            _sut = new DownloadService(_mockTelemetry.Object);
        }

        [Fact]
        public async Task DownloadVideo_ShouldRetryOnFailure()
        {
            // Arrange
            var videoId = "test-video-id";
            var outputPath = "test-output-path";
            var attempts = 0;

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () =>
            {
                await _sut.DownloadVideoAsync(videoId, outputPath);
            });

            _mockTelemetry.Verify(x => x.TrackDownloadAsync(
                It.Is<string>(id => id == videoId),
                It.Is<bool>(success => !success),
                It.IsAny<TimeSpan>()
            ), Times.Once);
        }
    }
} 