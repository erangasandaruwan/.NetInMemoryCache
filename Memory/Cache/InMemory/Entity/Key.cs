using Memory.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Memory.Cache.InMemory.Entity
{
    public class Key<TEntity, TKey> where TEntity : BaseEntity<TKey> where TKey : struct
    {
        public string Value { get; private set; }

        public static Key<TEntity, TKey> GetKey(bool isSingle = false)
        {
            return new Key<TEntity, TKey>()
            {
                Value = Constants.Prefix + typeof(TEntity).Name.ToUpper() + (isSingle ? Constants.Object : Constants.List)
            };
        }
    }
}
