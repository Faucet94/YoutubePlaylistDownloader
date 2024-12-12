using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using YoutubePlaylistDownloader.Services.Interfaces;
using YoutubePlaylistDownloader.Preview;
using YoutubePlaylistDownloader.ViewModels;
using YoutubePlaylistDownloader.Data;
using System.Runtime.Versioning;
using YoutubePlaylistDownloader.Services;

namespace YoutubePlaylistDownloader.Services
{
    [SupportedOSPlatform("windows")]
    public static class ServiceConfiguration
    {
        public static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            // Verificar se está rodando no Windows
            if (!OperatingSystem.IsWindows())
            {
                throw new PlatformNotSupportedException("Esta aplicação só pode ser executada no Windows.");
            }

            // Configurar Serilog
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.File("logs/app-.log", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            // Serviços comuns
            services.AddMemoryCache();
            services.AddLogging(builder =>
            {
                builder.AddSerilog(dispose: true);
            });

            // Registrar serviços base
            services.AddSingleton<IWindowsStorageService, LocalStorageService>();
            services.AddSingleton<IStorageService>(sp => sp.GetRequiredService<IWindowsStorageService>());
            services.AddSingleton<ConfigurationService>();
            services.AddSingleton<CacheService>();
            services.AddSingleton<SecureCacheService>();

            if (App.IsPreviewMode)
            {
                services.AddSingleton<IDownloadService, MockDownloadService>();
            }
            else
            {
                services.AddSingleton<IDownloadService, DownloadService>();
                services.AddSingleton<TelemetryService>();
                services.AddSingleton<AnalyticsService>();
            }

            services.AddTransient<MainPageViewModel>();

            return services.BuildServiceProvider();
        }
    }
} 