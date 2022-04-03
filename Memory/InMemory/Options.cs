using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cache.InMemory
{
    public class Options
    {
        protected MemoryCacheEntryOptions GetMemoryCacheEntryOptions(int inMemAbsExpInMinutes, CacheItemPriority cacheItemPriority)
        {
            // Size and sliding expiration and  will not be specified
            return new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(DateTime.Now.AddMinutes(inMemAbsExpInMinutes))
                .SetPriority(cacheItemPriority);
        }
    }
}
