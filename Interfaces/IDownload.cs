using System;
using System.Threading.Tasks;
using System.Windows;

namespace YoutubePlaylistDownloader.Interfaces
{
    public interface IDownload : IDisposable
    {
        string ImageUrl { get; }
        string Title { get; set; }
        string TotalDownloaded { get; set; }
        int TotalVideos { get; }
        int CurrentProgressPercent { get; set; }
        string CurrentDownloadSpeed { get; set; }
        string CurrentTitle { get; set; }
        string CurrentStatus { get; set; }
        Task<bool> Cancel();
        void OpenFolder_Click(object sender, RoutedEventArgs e);
        void Exit();
    }
} 