using YoutubePlaylistDownloader.Models;

namespace YoutubePlaylistDownloader.Preview
{
    public static class PreviewData
    {
        public static readonly string[] SampleUrls = new[]
        {
            "https://www.youtube.com/watch?v=sample1",
            "https://www.youtube.com/playlist?list=sample2"
        };

        public static readonly UserPreferences DefaultPreviewPreferences = new()
        {
            EnableGlassMorphism = true,
            GlassOpacity = 0.2,
            PreferredFormat = "mp4",
            PreferredQuality = "1080p",
            AutomaticallyDownloadSubtitles = true
        };
    }
} 