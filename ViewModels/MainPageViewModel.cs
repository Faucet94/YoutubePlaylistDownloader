using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using YoutubePlaylistDownloader.Services;
using YoutubePlaylistDownloader.Commands;
using System.Threading.Tasks;
using System.Windows.Threading;
using System;

namespace YoutubePlaylistDownloader.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private readonly ConfigurationService _configService;
        private readonly DownloadService _downloadService;
        private string _playlistUrl;
        private bool _isDownloading;
        private string _currentDownloadStatus;
        private double _downloadProgress;
        private double _glassOpacity;

        public string PlaylistUrl
        {
            get => _playlistUrl;
            set
            {
                _playlistUrl = value;
                OnPropertyChanged();
            }
        }

        public bool IsDownloading
        {
            get => _isDownloading;
            set
            {
                _isDownloading = value;
                OnPropertyChanged();
            }
        }

        public string CurrentDownloadStatus
        {
            get => _currentDownloadStatus;
            set
            {
                _currentDownloadStatus = value;
                OnPropertyChanged();
            }
        }

        public double DownloadProgress
        {
            get => _downloadProgress;
            set
            {
                _downloadProgress = value;
                OnPropertyChanged();
            }
        }

        public double GlassOpacity
        {
            get => _glassOpacity;
            set
            {
                _glassOpacity = value;
                OnPropertyChanged();
            }
        }

        public ICommand DownloadCommand { get; }

        public MainPageViewModel(ConfigurationService configService, DownloadService downloadService)
        {
            _configService = configService;
            _downloadService = downloadService;
            DownloadCommand = new RelayCommand(ExecuteDownload, CanExecuteDownload);
            LoadPreferences();
        }

        private async void LoadPreferences()
        {
            var preferences = await _configService.GetPreferencesAsync();
            GlassOpacity = preferences.GlassOpacity;
        }

        private bool CanExecuteDownload()
        {
            return !string.IsNullOrWhiteSpace(PlaylistUrl) && !IsDownloading;
        }

        private async void ExecuteDownload()
        {
            IsDownloading = true;
            CurrentDownloadStatus = "Iniciando download...";
            DownloadProgress = 0;

            try
            {
                await Task.Run(async () =>
                {
                    // Simular progresso do download
                    for (int i = 0; i <= 100; i += 10)
                    {
                        await Task.Delay(500); // Simular trabalho
                        await Dispatcher.CurrentDispatcher.InvokeAsync(() =>
                        {
                            DownloadProgress = i;
                            CurrentDownloadStatus = $"Baixando... {i}%";
                        });
                    }
                });
            }
            catch (Exception ex)
            {
                CurrentDownloadStatus = $"Erro: {ex.Message}";
            }
            finally
            {
                IsDownloading = false;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
} 