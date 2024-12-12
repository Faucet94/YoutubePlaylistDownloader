using System;
using System.Windows.Controls;
using YoutubePlaylistDownloader.Interfaces;
using System.Runtime.Versioning;

namespace YoutubePlaylistDownloader.Models
{
    [SupportedOSPlatform("windows")]
    public class QueuedDownload : IDisposable
    {
        private readonly Interfaces.IDownload _download;
        private readonly Grid _displayGrid;
        private bool disposedValue;

        public QueuedDownload(Interfaces.IDownload download)
        {
            _download = download;
            _displayGrid = CreateDisplayGrid();
        }

        public Grid GetDisplayGrid() => _displayGrid;

        private Grid CreateDisplayGrid()
        {
            var grid = new Grid();
            // Implementação do grid...
            return grid;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _download?.Dispose();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
} 