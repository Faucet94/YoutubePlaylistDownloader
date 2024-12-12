using FluentAssertions;
using Moq;
using System.Threading.Tasks;
using Xunit;
using YoutubePlaylistDownloader.Models;
using YoutubePlaylistDownloader.Services;

namespace YoutubePlaylistDownloader.Tests.Services
{
    public class ConfigurationServiceTests
    {
        private readonly Mock<LocalStorageService> _mockLocalStorage;
        private readonly ConfigurationService _sut;

        public ConfigurationServiceTests()
        {
            _mockLocalStorage = new Mock<LocalStorageService>();
            _sut = new ConfigurationService(_mockLocalStorage.Object);
        }

        [Fact]
        public async Task GetPreferences_WhenNoPreferencesExist_ShouldReturnDefaultValues()
        {
            // Arrange
            _mockLocalStorage.Setup(x => x.GetAsync<UserPreferences>(It.IsAny<string>()))
                .ReturnsAsync((UserPreferences)null);

            // Act
            var result = await _sut.GetPreferencesAsync();

            // Assert
            result.Should().NotBeNull();
            result.EnableGlassMorphism.Should().BeTrue();
            result.GlassOpacity.Should().Be(0.1);
            result.PreferredFormat.Should().Be("mp4");
        }

        [Fact]
        public async Task SavePreferences_ShouldPersistPreferences()
        {
            // Arrange
            var preferences = new UserPreferences
            {
                EnableGlassMorphism = true,
                GlassOpacity = 0.5,
                PreferredFormat = "mp3"
            };

            // Act
            await _sut.SavePreferencesAsync(preferences);

            // Assert
            _mockLocalStorage.Verify(x => x.SaveAsync(
                It.IsAny<string>(),
                It.Is<UserPreferences>(p => 
                    p.EnableGlassMorphism == preferences.EnableGlassMorphism &&
                    p.GlassOpacity == preferences.GlassOpacity &&
                    p.PreferredFormat == preferences.PreferredFormat)
            ), Times.Once);
        }
    }
} 