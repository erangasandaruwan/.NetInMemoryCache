using Shared;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cache.InMemory
{
    public class Utility : IUtility
    {
        private const string Key = "Key";
        private const string Value = "Value";
        private const string EntriesCollection = "EntriesCollection";

        private readonly IMemoryCache _memoryCache;

        public Utility(IMemoryCache memoryCache) { _memoryCache = memoryCache; }

        public List<string> GetAllEntityObjectList()
        {
            var entityCacheObjectList = new List<string>();

            // Get the empty definition for the EntriesCollection
            var cacheEntriesCollectionDefinition =
                typeof(MemoryCache).GetProperty(EntriesCollection,
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            // Populate the definition with your IMemoryCache instance.  
            // It needs to be cast as a dynamic, otherwise you can't
            // loop through it due to it being a collection of objects.
            var cacheEntriesCollection = cacheEntriesCollectionDefinition.GetValue(_memoryCache) as dynamic;

            // Define a new list we'll be adding the cache entries too
            List<string> cacheKeyCollectionValues = new List<string>();

            foreach (var cacheItem in cacheEntriesCollection)
            {
                string key = cacheItem.GetType().GetProperty(Key).GetValue(cacheItem, null).ToString();
                if (key.StartsWith(Constants.Prefix))
                    entityCacheObjectList.Add(key);
            }

            return entityCacheObjectList;
        }

        public void RemoveAllEntityObjects()
        {
            // Get the empty definition for the EntriesCollection
            var cacheEntriesCollectionDefinition =
                typeof(MemoryCache).GetProperty(EntriesCollection,
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            // Populate the definition with your IMemoryCache instance.  
            // It needs to be cast as a dynamic, otherwise you can't
            // loop through it due to it being a collection of objects.
            var cacheEntriesCollection = cacheEntriesCollectionDefinition.GetValue(_memoryCache) as dynamic;

            // Define a new list we'll be adding the cache entries too
            List<string> cacheKeyCollectionValues = new List<string>();

            foreach (var cacheItem in cacheEntriesCollection)
            {
                string key = cacheItem.GetType().GetProperty(Key).GetValue(cacheItem, null).ToString();
                if (key.StartsWith(Constants.Prefix))
                    _memoryCache.Remove(key);
            }
        }

        public List<ICacheEntry> FindAllEntityObjects()
        {
            // Get the empty definition for the EntriesCollection
            var cacheEntriesCollectionDefinition =
                typeof(MemoryCache).GetProperty(EntriesCollection,
                                            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            // Populate the definition with your IMemoryCache instance.  
            // It needs to be cast as a dynamic, otherwise you can't
            // loop through it due to it being a collection of objects.
            var cacheEntriesCollection = cacheEntriesCollectionDefinition.GetValue(_memoryCache) as dynamic;

            // Define a new list we'll be adding the cache entries too
            List<ICacheEntry> cacheCollectionValues = new List<ICacheEntry>();

            foreach (var cacheItem in cacheEntriesCollection)
            {
                // Get the "Value" from the key/value pair which contains the cache entry   
                ICacheEntry cacheItemValue = cacheItem.GetType().GetProperty(Value).GetValue(cacheItem, null);

                // Add the cache entry to the list
                string key = cacheItem.GetType().GetProperty(Key).GetValue(cacheItem, null).ToString();
                if (key.StartsWith(Constants.Prefix))
                    cacheCollectionValues.Add(cacheItemValue);
            }

            return cacheCollectionValues;
        }
    }
}
