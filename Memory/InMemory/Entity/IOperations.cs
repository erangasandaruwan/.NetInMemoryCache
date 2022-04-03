using Shared;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cache.InMemory.Entity
{
    public interface IOperations<TEntity, TKey> where TEntity : BaseEntity<TKey> where TKey : struct
    {
        bool TryGetValue(Key<TEntity, TKey> key, out TEntity entity);
        bool TryGetValue(Key<TEntity, TKey> key, out IEnumerable<TEntity> entities);
        List<string> GetNameList();
        List<ICacheEntry> GetAllObjects();

        void Set(Key<TEntity, TKey> key, TEntity entity);
        void Set(Key<TEntity, TKey> key, IEnumerable<TEntity> entities);

        void Reset(Key<TEntity, TKey> key, TEntity entity, string reason = "");
        void Reset(Key<TEntity, TKey> key, IEnumerable<TEntity> entities, string reason = "");

        void Remove(Key<TEntity, TKey> key);
        void Clear();
    }
}
