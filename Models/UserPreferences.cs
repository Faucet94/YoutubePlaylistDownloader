using System;

namespace YoutubePlaylistDownloader.Models
{
    public class UserPreferences
    {
        public string DefaultDownloadPath { get; set; }
        public string PreferredQuality { get; set; }
        public string PreferredFormat { get; set; }
        public bool AutomaticallyDownloadSubtitles { get; set; }
        public string PreferredLanguage { get; set; }
        public bool EnableGlassMorphism { get; set; }
        public double GlassOpacity { get; set; }
        public int MaxConcurrentDownloads { get; set; }
        public DateTime LastUpdateCheck { get; set; }
    }
} 