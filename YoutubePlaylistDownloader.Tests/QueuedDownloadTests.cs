using Xunit;
using Moq;
using YoutubePlaylistDownloader.Models;
using YoutubePlaylistDownloader.Interfaces;

namespace YoutubePlaylistDownloader.Tests
{
    public class QueuedDownloadTests
    {
        [Fact]
        public void QueuedDownload_Constructor_ShouldInitializeCorrectly()
        {
            // Arrange
            var mockDownload = new Mock<IDownload>();
            mockDownload.Setup(x => x.Title).Returns("Test Video");
            mockDownload.Setup(x => x.CurrentProgressPercent).Returns(0);

            // Act
            var queuedDownload = new QueuedDownload(mockDownload.Object);

            // Assert
            Assert.NotNull(queuedDownload);
            Assert.NotNull(queuedDownload.GetDisplayGrid());
        }

        [Fact]
        public void QueuedDownload_Dispose_ShouldDisposeUnderlyingDownload()
        {
            // Arrange
            var mockDownload = new Mock<IDownload>();
            var disposed = false;
            mockDownload.Setup(x => x.Dispose()).Callback(() => disposed = true);
            var queuedDownload = new QueuedDownload(mockDownload.Object);

            // Act
            queuedDownload.Dispose();

            // Assert
            Assert.True(disposed);
            mockDownload.Verify(x => x.Dispose(), Times.Once);
        }
    }
} 