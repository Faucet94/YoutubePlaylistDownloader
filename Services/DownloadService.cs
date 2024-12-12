using System;
using System.Threading.Tasks;
using Polly;
using YoutubeExplode;
using System.IO;
using YoutubeExplode.Exceptions;
using YoutubePlaylistDownloader.Services.Interfaces;
using DownloadEventArgs = YoutubePlaylistDownloader.Services.Interfaces.DownloadProgressEventArgs;

namespace YoutubePlaylistDownloader.Services
{
    public class DownloadService : IDownloadService
    {
        private readonly IAsyncPolicy<Stream> _retryPolicy;
        
        public event EventHandler<DownloadEventArgs> ProgressChanged;

        public DownloadService()
        {
            _retryPolicy = Policy<Stream>
                .Handle<HttpRequestException>()
                .Or<YoutubeExplodeException>()
                .WaitAndRetryAsync(3, retryAttempt => 
                    TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        }

        public async Task<Stream> DownloadVideoAsync(string videoId, string outputPath)
        {
            return await _retryPolicy.ExecuteAsync(async () =>
            {
                // Lógica de download existente
                OnProgressChanged(0, "Iniciando download...");
                // ... implementar download real aqui
                OnProgressChanged(100, "Download concluído");
                return await Task.FromResult(Stream.Null);
            });
        }

        protected virtual void OnProgressChanged(int progress, string status = null)
        {
            ProgressChanged?.Invoke(this, new DownloadEventArgs(progress, status));
        }
    }
} 