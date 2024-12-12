using System;
using System.Diagnostics.Metrics;
using OpenTelemetry;
using OpenTelemetry.Metrics;

public class AnalyticsService
{
    private readonly Meter _meter;
    private readonly Counter<int> _downloadCounter;
    private readonly Histogram<double> _downloadDuration;

    public AnalyticsService()
    {
        _meter = new Meter("YoutubePlaylistDownloader");
        _downloadCounter = _meter.CreateCounter<int>("downloads.total");
        _downloadDuration = _meter.CreateHistogram<double>("download.duration");
    }

    public void TrackDownload(TimeSpan duration)
    {
        _downloadCounter.Add(1);
        _downloadDuration.Record(duration.TotalSeconds);
    }
} 