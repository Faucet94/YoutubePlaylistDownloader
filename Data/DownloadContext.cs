using Microsoft.EntityFrameworkCore;
using YoutubePlaylistDownloader.Models;

namespace YoutubePlaylistDownloader.Data
{
    public class DownloadContext : DbContext
    {
        public DbSet<DownloadHistory> DownloadHistory { get; set; }
        public DbSet<UserPreferences> UserPreferences { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={GlobalConsts.CurrentDir}/downloads.db");
    }

    public class DownloadHistory
    {
        public int Id { get; set; }
        public string VideoId { get; set; }
        public string Title { get; set; }
        public DateTime DownloadDate { get; set; }
        public bool Success { get; set; }
        public string Format { get; set; }
    }
}