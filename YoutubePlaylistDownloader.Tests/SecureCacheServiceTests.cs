using Xunit;
using YoutubePlaylistDownloader.Services;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace YoutubePlaylistDownloader.Tests
{
    public class SecureCacheServiceTests
    {
        private readonly IMemoryCache _memoryCache;
        private readonly SecureCacheService _cacheService;

        public SecureCacheServiceTests()
        {
            _memoryCache = new MemoryCache(new MemoryCacheOptions());
            _cacheService = new SecureCacheService(_memoryCache);
        }

        [Fact]
        public void GetOrCreate_ShouldCreateNewItemIfNotExists()
        {
            // Arrange
            var key = "testKey";
            var expectedValue = "testValue";

            // Act
            var result = _cacheService.GetOrCreate(key, () => expectedValue);

            // Assert
            Assert.Equal(expectedValue, result);
        }

        [Fact]
        public void GetOrCreate_ShouldReturnExistingItem()
        {
            // Arrange
            var key = "testKey";
            var expectedValue = "testValue";
            _cacheService.GetOrCreate(key, () => expectedValue);

            // Act
            var result = _cacheService.GetOrCreate(key, () => "differentValue");

            // Assert
            Assert.Equal(expectedValue, result);
        }

        [Fact]
        public void Remove_ShouldRemoveItemFromCache()
        {
            // Arrange
            var key = "testKey";
            var value = "testValue";
            _cacheService.GetOrCreate(key, () => value);

            // Act
            _cacheService.Remove(key);
            var result = _cacheService.GetOrCreate(key, () => "newValue");

            // Assert
            Assert.NotEqual(value, result);
            Assert.Equal("newValue", result);
        }
    }
} 