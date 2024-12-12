using System;
using System.Threading.Tasks;
using YoutubePlaylistDownloader.Models;
using YoutubePlaylistDownloader.Services.Interfaces;

namespace YoutubePlaylistDownloader.Services
{
    public class ConfigurationService
    {
        private readonly IStorageService _storage;
        private readonly string PREFERENCES_KEY = "user_preferences";
        private UserPreferences _cachedPreferences;

        public ConfigurationService(IStorageService storage)
        {
            _storage = storage;
        }

        public async Task<UserPreferences> GetPreferencesAsync()
        {
            if (_cachedPreferences != null)
                return _cachedPreferences;

            _cachedPreferences = await _storage.GetAsync<UserPreferences>(PREFERENCES_KEY);
            
            if (_cachedPreferences == null)
            {
                _cachedPreferences = GetDefaultPreferences();
                await SavePreferencesAsync(_cachedPreferences);
            }

            return _cachedPreferences;
        }

        public async Task SavePreferencesAsync(UserPreferences preferences)
        {
            _cachedPreferences = preferences;
            await _storage.SaveAsync(PREFERENCES_KEY, preferences);
        }

        private UserPreferences GetDefaultPreferences()
        {
            return new UserPreferences
            {
                DefaultDownloadPath = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos),
                PreferredQuality = "1080p",
                PreferredFormat = "mp4",
                AutomaticallyDownloadSubtitles = false,
                PreferredLanguage = "en",
                EnableGlassMorphism = true,
                GlassOpacity = 0.1,
                MaxConcurrentDownloads = 3,
                LastUpdateCheck = DateTime.Now
            };
        }
    }
} 