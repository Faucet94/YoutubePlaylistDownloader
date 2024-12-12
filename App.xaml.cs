using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using YoutubePlaylistDownloader.Services;
using YoutubePlaylistDownloader.Data;
using YoutubePlaylistDownloader.Models;
using System.IO;
using System.Runtime.Versioning;

namespace YoutubePlaylistDownloader
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    [SupportedOSPlatform("windows")]
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; }
        public static bool IsPreviewMode { get; private set; }

        public App()
        {
            DispatcherUnhandledException += App_DispatcherUnhandledException;
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            try
            {
                if (!OperatingSystem.IsWindows())
                {
                    System.Windows.MessageBox.Show(
                        "Esta aplicação só pode ser executada no Windows.", 
                        "Erro",
                        MessageBoxButton.OK, 
                        MessageBoxImage.Error);
                    Current.Shutdown();
                    return;
                }

                ServiceProvider = ServiceConfiguration.ConfigureServices();
                
                if (e.Args.Contains("--preview"))
                {
                    IsPreviewMode = true;
                    InitializePreviewMode();
                }

                ConfigureGlobalStyles();
                InitializeDatabase();

                var mainWindow = new MainWindow();
                mainWindow.Show();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(
                    $"Erro durante a inicialização: {ex.Message}", 
                    "Erro", 
                    MessageBoxButton.OK, 
                    MessageBoxImage.Error);
                Current.Shutdown();
            }
        }

        private void ConfigureGlobalStyles()
        {
            var configService = ServiceProvider.GetRequiredService<ConfigurationService>();
            var preferences = configService.GetPreferencesAsync().GetAwaiter().GetResult();

            // Adicionar dicionários de recursos globais
            Resources.MergedDictionaries.Add(new ResourceDictionary
            {
                Source = new Uri("/Styles/GlassMorphism.xaml", UriKind.Relative)
            });

            // Configurar tema baseado nas preferências
            if (preferences.EnableGlassMorphism)
            {
                // Aplicar configurações do Glass Morphism
            }
        }

        private void InitializeDatabase()
        {
            try
            {
                using var scope = ServiceProvider.CreateScope();
                using var context = new DownloadContext();
                context.Database.EnsureCreated();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(
                    $"Erro ao inicializar banco de dados: {ex.Message}", 
                    "Erro", 
                    MessageBoxButton.OK, 
                    MessageBoxImage.Error);
            }
        }

        private void InitializePreviewMode()
        {
            try
            {
                var previewSettings = new UserPreferences
                {
                    EnableGlassMorphism = true,
                    GlassOpacity = 0.2,
                    PreferredFormat = "mp4",
                    DefaultDownloadPath = Path.Combine(Environment.CurrentDirectory, "PreviewDownloads")
                };

                var configService = ServiceProvider.GetRequiredService<ConfigurationService>();
                configService.SavePreferencesAsync(previewSettings).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Erro ao inicializar modo preview: {ex.Message}", "Erro", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            
            // Limpar recursos
            if (ServiceProvider is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }

        async void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            await GlobalConsts.ShowMessage((string)FindResource("Error"), (string)FindResource("ErrorMessage"));
            await GlobalConsts.Log($"{e.Exception}", "Unhandled exception");

            // Don't crash at the moment of truth >.<
#if !DEBUG
            e.Handled = true;
#endif
        }
    }
}
