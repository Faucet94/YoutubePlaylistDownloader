using Xunit;
using YoutubePlaylistDownloader.Services;
using Microsoft.Extensions.DependencyInjection;

namespace YoutubePlaylistDownloader.Tests
{
    public class ServiceConfigurationTests
    {
        [Fact]
        public void ConfigureServices_ShouldRegisterRequiredServices()
        {
            // Arrange
            var services = ServiceConfiguration.ConfigureServices();

            // Act & Assert
            Assert.NotNull(services);
            var serviceProvider = services.BuildServiceProvider();
            
            // Verify essential services are registered
            Assert.NotNull(serviceProvider.GetService<ISecureCacheService>());
            Assert.NotNull(serviceProvider.GetService<MainPageViewModel>());
        }

        [Fact]
        public void ConfigureServices_ShouldConfigureLogging()
        {
            // Arrange & Act
            var services = ServiceConfiguration.ConfigureServices();
            var serviceProvider = services.BuildServiceProvider();

            // Assert
            var logger = serviceProvider.GetService<ILogger<ServiceConfigurationTests>>();
            Assert.NotNull(logger);
        }
    }
} 