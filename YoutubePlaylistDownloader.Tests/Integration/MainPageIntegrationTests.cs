using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using System.Windows;
using Xunit;
using YoutubePlaylistDownloader.Services;

namespace YoutubePlaylistDownloader.Tests.Integration
{
    public class MainPageIntegrationTests
    {
        [WpfFact]
        public async Task MainPage_ShouldLoadAndDisplayCorrectly()
        {
            // Arrange
            await Application.Current.Dispatcher.InvokeAsync(() =>
            {
                var services = ServiceConfiguration.ConfigureServices();
                var mainPage = new MainPage();

                // Act
                mainPage.Load();

                // Assert
                Assert.NotNull(mainPage.DataContext);
                Assert.IsType<MainPageViewModel>(mainPage.DataContext);
            });
        }
    }
} 