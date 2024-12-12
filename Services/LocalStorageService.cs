using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Runtime.Versioning;
using YoutubePlaylistDownloader.Services.Interfaces;

namespace YoutubePlaylistDownloader.Services
{
    [SupportedOSPlatform("windows")]
    public class LocalStorageService : IWindowsStorageService
    {
        private readonly string _baseFolder;
        private readonly string _secureFolder;

        public LocalStorageService()
        {
            _baseFolder = Path.Combine(GlobalConsts.CurrentDir, "LocalStorage");
            _secureFolder = Path.Combine(_baseFolder, "Secure");
            Directory.CreateDirectory(_baseFolder);
            Directory.CreateDirectory(_secureFolder);
        }

        public async Task SaveAsync<T>(string key, T data)
        {
            var filePath = GetFilePath(key);
            var json = JsonConvert.SerializeObject(data, Formatting.Indented);
            await File.WriteAllTextAsync(filePath, json);
        }

        public async Task<T> GetAsync<T>(string key)
        {
            var filePath = GetFilePath(key);
            if (!File.Exists(filePath))
                return default;

            var json = await File.ReadAllTextAsync(filePath);
            return JsonConvert.DeserializeObject<T>(json);
        }

        public bool Exists(string key)
        {
            return File.Exists(GetFilePath(key));
        }

        public void Remove(string key)
        {
            var filePath = GetFilePath(key);
            if (File.Exists(filePath))
                File.Delete(filePath);
        }

        private string GetFilePath(string key)
        {
            return Path.Combine(_baseFolder, $"{key}.json");
        }

        public void SaveSecureValue(string key, string value)
        {
            var protectedData = ProtectedData.Protect(
                System.Text.Encoding.UTF8.GetBytes(value),
                null,
                DataProtectionScope.CurrentUser);
                
            var secureFilePath = Path.Combine(
                _secureFolder, 
                $"{ComputeHash(key)}.bin");
                
            File.WriteAllBytes(secureFilePath, protectedData);
        }
        
        public string GetSecureValue(string key)
        {
            var secureFilePath = Path.Combine(
                _secureFolder,
                $"{ComputeHash(key)}.bin");
                
            if (!File.Exists(secureFilePath))
                return null;
                
            var protectedData = File.ReadAllBytes(secureFilePath);
            var data = ProtectedData.Unprotect(
                protectedData,
                null,
                DataProtectionScope.CurrentUser);
                
            return System.Text.Encoding.UTF8.GetString(data);
        }
        
        private static string ComputeHash(string input)
        {
            using var sha = SHA256.Create();
            var hash = sha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(input));
            return Convert.ToBase64String(hash);
        }
    }
} 