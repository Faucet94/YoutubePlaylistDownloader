using FluentAssertions;
using Moq;
using System.Threading.Tasks;
using Xunit;
using YoutubePlaylistDownloader.Services;
using YoutubePlaylistDownloader.ViewModels;

namespace YoutubePlaylistDownloader.Tests.ViewModels
{
    public class MainPageViewModelTests
    {
        private readonly Mock<ConfigurationService> _mockConfig;
        private readonly Mock<DownloadService> _mockDownload;
        private readonly MainPageViewModel _sut;

        public MainPageViewModelTests()
        {
            _mockConfig = new Mock<ConfigurationService>();
            _mockDownload = new Mock<DownloadService>();
            _sut = new MainPageViewModel(_mockConfig.Object, _mockDownload.Object);
        }

        [Fact]
        public void CanExecuteDownload_WhenUrlIsEmpty_ShouldReturnFalse()
        {
            // Arrange
            _sut.PlaylistUrl = string.Empty;

            // Act
            var canExecute = ((RelayCommand)_sut.DownloadCommand).CanExecute(null);

            // Assert
            canExecute.Should().BeFalse();
        }

        [Fact]
        public void CanExecuteDownload_WhenUrlIsValid_ShouldReturnTrue()
        {
            // Arrange
            _sut.PlaylistUrl = "https://youtube.com/watch?v=test";
            _sut.IsDownloading = false;

            // Act
            var canExecute = ((RelayCommand)_sut.DownloadCommand).CanExecute(null);

            // Assert
            canExecute.Should().BeTrue();
        }
    }
} 