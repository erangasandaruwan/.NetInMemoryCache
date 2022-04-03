using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Memory.Cache.InMemory
{
    public interface IUtility
    {
        List<string> GetAllEntityObjectList();
        List<ICacheEntry> FindAllEntityObjects();
        void RemoveAllEntityObjects();
    }
}
