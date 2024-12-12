using Microsoft.Extensions.Caching.Memory;
using System.Security.Cryptography;
using System;
using System.Threading.Tasks;
using System.Runtime.Versioning;
using YoutubePlaylistDownloader.Services.Interfaces;

namespace YoutubePlaylistDownloader.Services
{
    [SupportedOSPlatform("windows")]
    public class SecureCacheService
    {
        private readonly IMemoryCache _cache;
        private readonly IWindowsStorageService _storage;
        private readonly TimeSpan _defaultExpiration = TimeSpan.FromMinutes(30);
        private readonly byte[] _encryptionKey;

        public SecureCacheService(IMemoryCache cache, IWindowsStorageService storage)
        {
            _cache = cache;
            _storage = storage;
            _encryptionKey = GetOrCreateEncryptionKey();
        }

        public async Task<T> GetOrCreateAsync<T>(string key, Func<Task<T>> factory)
        {
            var secureKey = ComputeHash(key);
            
            if (!_cache.TryGetValue(secureKey, out T result))
            {
                result = await factory();
                var options = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(_defaultExpiration)
                    .SetPriority(CacheItemPriority.Normal)
                    .RegisterPostEvictionCallback(OnPostEviction);

                _cache.Set(secureKey, result, options);
            }
            
            return result;
        }

        private byte[] GetOrCreateEncryptionKey()
        {
            var protectedKey = _storage.GetSecureValue("CacheEncryptionKey");
            if (string.IsNullOrEmpty(protectedKey))
            {
                using var aes = Aes.Create();
                aes.GenerateKey();
                protectedKey = Convert.ToBase64String(
                    ProtectedData.Protect(aes.Key, null, DataProtectionScope.CurrentUser));
                _storage.SaveSecureValue("CacheEncryptionKey", protectedKey);
                return aes.Key;
            }

            var encryptedKey = Convert.FromBase64String(protectedKey);
            return ProtectedData.Unprotect(encryptedKey, null, DataProtectionScope.CurrentUser);
        }

        private string ComputeHash(string input)
        {
            using var hmac = new HMACSHA256(_encryptionKey);
            var hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(input));
            return Convert.ToBase64String(hash);
        }

        private void OnPostEviction(object key, object value, 
            EvictionReason reason, object state)
        {
            if (value is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }
    }
} 