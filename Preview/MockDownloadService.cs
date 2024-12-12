using YoutubePlaylistDownloader.Services.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;

namespace YoutubePlaylistDownloader.Preview
{
    public class MockDownloadService : IDownloadService
    {
        private readonly Random _random = new();

        public event EventHandler<Services.Interfaces.DownloadProgressEventArgs> ProgressChanged;

        public async Task<Stream> DownloadVideoAsync(string videoId, string outputPath)
        {
            // Simula download com progresso
            for (int i = 0; i <= 100; i += 5)
            {
                await Task.Delay(200); // Simula delay de rede
                OnProgressChanged(i);
            }

            return Stream.Null;
        }
        
        protected virtual void OnProgressChanged(int progress)
        {
            ProgressChanged?.Invoke(this, new Services.Interfaces.DownloadProgressEventArgs(progress));
        }
    }
} 