using System.Runtime.Versioning;

namespace YoutubePlaylistDownloader.Services.Interfaces
{
    [SupportedOSPlatform("windows")]
    public interface IWindowsStorageService : IStorageService
    {
        void SaveSecureValue(string key, string value);
        string GetSecureValue(string key);
    }
} 