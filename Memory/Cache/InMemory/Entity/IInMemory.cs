using Memory.Shared;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Memory.Cache.InMemory.Entity
{
    public interface IInMemory<TEntity, TKey> where TEntity : BaseEntity<TKey> where TKey : struct
    {
        bool TryGetValue(Key<TEntity, TKey> key, out TEntity entity);
        bool TryGetValue(Key<TEntity, TKey> key, out IEnumerable<TEntity> entities);

        void Set(Key<TEntity, TKey> key, TEntity entity);
        void Set(Key<TEntity, TKey> key, IEnumerable<TEntity> entities);

        void Reset(Key<TEntity, TKey> key, TEntity entity, string reason = "");
        void Reset(Key<TEntity, TKey> key, IEnumerable<TEntity> entities, string reason = "");

        void Remove(Key<TEntity, TKey> key);

        List<ICacheEntry> FindAllObjects();

        void Clear();
    }
}
