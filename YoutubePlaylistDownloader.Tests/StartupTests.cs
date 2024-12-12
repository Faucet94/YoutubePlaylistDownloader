using Xunit;
using System.Windows;
using YoutubePlaylistDownloader.Services;
using Microsoft.Extensions.DependencyInjection;

namespace YoutubePlaylistDownloader.Tests
{
    public class StartupTests
    {
        [Fact]
        public void CriticalComponents_ShouldBeAvailable()
        {
            // Verifica se os componentes principais existem e são acessíveis
            Assert.NotNull(typeof(MainWindow));
            Assert.NotNull(typeof(MainPage));
            Assert.NotNull(typeof(DownloadPage));
            Assert.NotNull(typeof(GlobalConsts));
        }

        [Fact]
        public void ServiceConfiguration_ShouldInitializeCorrectly()
        {
            // Verifica se a configuração de serviços está correta
            var services = ServiceConfiguration.ConfigureServices();
            var serviceProvider = services.BuildServiceProvider();

            // Verifica serviços essenciais
            Assert.NotNull(serviceProvider.GetService<ISecureCacheService>());
            Assert.NotNull(serviceProvider.GetService<MainPageViewModel>());
        }

        [Fact]
        public void GlobalConsts_ShouldHaveValidPaths()
        {
            // Verifica se os caminhos críticos estão configurados
            Assert.NotNull(GlobalConsts.CurrentDir);
            Assert.NotNull(GlobalConsts.TempFolderPath);
            Assert.NotNull(GlobalConsts.FFmpegFilePath);
            Assert.True(Directory.Exists(GlobalConsts.CurrentDir));
        }

        [Fact]
        public void UserInterface_ShouldHaveRequiredControls()
        {
            // Verifica se os controles da UI estão presentes
            var mainWindowType = typeof(MainWindow);
            var mainPageType = typeof(MainPage);
            var downloadPageType = typeof(DownloadPage);

            Assert.True(mainWindowType.IsSubclassOf(typeof(Window)));
            Assert.True(mainPageType.IsSubclassOf(typeof(UserControl)));
            Assert.True(downloadPageType.IsSubclassOf(typeof(UserControl)));
        }

        [Fact]
        public void Dependencies_ShouldBeResolved()
        {
            // Verifica se todas as dependências necessárias estão presentes
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var requiredAssemblies = new[]
            {
                "Microsoft.Extensions.DependencyInjection",
                "Microsoft.Extensions.Logging",
                "Serilog",
                "MaterialDesignThemes.Wpf"
            };

            foreach (var required in requiredAssemblies)
            {
                Assert.Contains(assemblies, a => a.GetName().Name.StartsWith(required));
            }
        }

        [Fact]
        public void ViewModels_ShouldBeProperlyConfigured()
        {
            // Verifica se os ViewModels estão configurados corretamente
            var services = ServiceConfiguration.ConfigureServices();
            var serviceProvider = services.BuildServiceProvider();

            var mainPageViewModel = serviceProvider.GetService<MainPageViewModel>();
            Assert.NotNull(mainPageViewModel);
        }
    }
} 