using System;
using System.IO;
using System.Threading.Tasks;

namespace YoutubePlaylistDownloader.Services.Interfaces
{
    public interface IDownloadService
    {
        Task<Stream> DownloadVideoAsync(string videoId, string outputPath);
        event EventHandler<DownloadProgressEventArgs> ProgressChanged;
    }

    public class DownloadProgressEventArgs : EventArgs
    {
        public int Progress { get; }
        public string Status { get; }

        public DownloadProgressEventArgs(int progress, string status = null)
        {
            Progress = progress;
            Status = status;
        }
    }
} 