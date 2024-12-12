using YoutubePlaylistDownloader.Services.Interfaces;
using YoutubePlaylistDownloader.Models;

namespace YoutubePlaylistDownloader.Preview
{
    public class MockStorageService : IStorageService
    {
        private readonly UserPreferences _defaultPreferences;
        private readonly Dictionary<string, object> _storage = new();

        public MockStorageService(UserPreferences defaultPreferences)
        {
            _defaultPreferences = defaultPreferences;
        }

        public Task SaveAsync<T>(string key, T data)
        {
            _storage[key] = data;
            return Task.CompletedTask;
        }

        public Task<T> GetAsync<T>(string key)
        {
            if (_storage.TryGetValue(key, out var value))
                return Task.FromResult((T)value);

            if (typeof(T) == typeof(UserPreferences))
                return Task.FromResult((T)(object)_defaultPreferences);

            return Task.FromResult<T>(default);
        }

        public bool Exists(string key) => _storage.ContainsKey(key);

        public void Remove(string key) => _storage.Remove(key);
    }
} 