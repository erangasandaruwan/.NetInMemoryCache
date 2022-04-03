
using Microsoft.Extensions.Caching.Memory;
using Shared;
using System.Collections.Generic;

namespace Cache.InMemory.Entity
{
    public class Operations<TEntity, TKey> : Options, IOperations<TEntity, TKey> where TEntity : BaseEntity<TKey> where TKey : struct
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IUtility _utility;
        public Operations(IMemoryCache memoryCache, IUtility utility)
        {
            _memoryCache = memoryCache;
            _utility = utility;
        }

        public bool TryGetValue(Key<TEntity, TKey> key, out TEntity entity)
        {
            return _memoryCache.TryGetValue<TEntity>(key.Value, out entity);
        }

        public bool TryGetValue(Key<TEntity, TKey> key, out IEnumerable<TEntity> entities)
        {
            return _memoryCache.TryGetValue<IEnumerable<TEntity>>(key.Value, out entities);
        }

        public List<string> GetNameList()
        {
            return _utility.GetAllEntityObjectList();
        }

        public List<ICacheEntry> GetAllObjects()
        {
            return _utility.GetAllEntityObjects();
        }

        public void Set(Key<TEntity, TKey> key, TEntity entity)
        {
            _memoryCache.Set(key.Value, entity, GetMemoryCacheEntryOptions(10, CacheItemPriority.Normal));
        }

        public void Set(Key<TEntity, TKey> key, IEnumerable<TEntity> entities)
        {
            _memoryCache.Set(key.Value, entities, GetMemoryCacheEntryOptions(10, CacheItemPriority.Normal));
        }

        public void Reset(Key<TEntity, TKey> key, TEntity entity, string reason = "")
        {
            _memoryCache.Remove(key.Value);
            _memoryCache.Set(key.Value, entity, GetMemoryCacheEntryOptions(10, CacheItemPriority.Normal));
        }

        public void Reset(Key<TEntity, TKey> key, IEnumerable<TEntity> entities, string reason = "")
        {
            _memoryCache.Remove(key.Value);
            _memoryCache.Set(key.Value, entities, GetMemoryCacheEntryOptions(10, CacheItemPriority.Normal));
        }

        public void Remove(Key<TEntity, TKey> key)
        {
            _memoryCache.Remove(key.Value);
        }

        public void Clear()
        {
            _utility.RemoveAllEntityObjects();
        }
    }
}
