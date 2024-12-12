using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using OpenTelemetry;
using Serilog;
using ILogger = Serilog.ILogger;

namespace YoutubePlaylistDownloader.Services
{
    public class TelemetryService
    {
        private readonly ActivitySource _activitySource;
        private readonly ILogger _logger;

        public TelemetryService(ILogger logger)
        {
            _activitySource = new ActivitySource("YoutubePlaylistDownloader");
            _logger = logger;
        }

        public Task TrackDownloadAsync(string videoId, bool success, TimeSpan duration)
        {
            using var activity = _activitySource.StartActivity("VideoDownload");
            activity?.SetTag("video.id", videoId);
            activity?.SetTag("download.success", success);
            activity?.SetTag("download.duration_ms", duration.TotalMilliseconds);

            _logger.Information("Video download completed. ID: {VideoId}, Success: {Success}, Duration: {Duration}ms",
                videoId, success, duration.TotalMilliseconds);

            return Task.CompletedTask;
        }
    }
} 