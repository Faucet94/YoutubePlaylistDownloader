namespace YoutubePlaylistDownloader.Services.Interfaces
{
    public interface IStorageService
    {
        Task SaveAsync<T>(string key, T data);
        Task<T> GetAsync<T>(string key);
        bool Exists(string key);
        void Remove(string key);
    }
} 