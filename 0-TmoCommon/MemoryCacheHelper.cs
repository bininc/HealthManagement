using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;

namespace TmoCommon
{
    /// <summary>
    /// 基于MemoryCache的缓存辅助类
    /// </summary>
    public static class MemoryCacheHelper
    {
        private static readonly Object _locker = new object();

        /// <summary>
        /// 得到缓存项
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">缓存键</param>
        /// <param name="cachePopulate">返回缓存结果的委托</param>
        /// <param name="absoluteExpiration">指定缓存过期时间</param>
        /// <param name="slidingExpiration">时间间隔内未被访问清除缓存</param>
        /// <returns></returns>
        public static T GetCacheItem<T>(String key, Func<T> cachePopulate, DateTime? absoluteExpiration, TimeSpan? slidingExpiration = null)
        {
            if (String.IsNullOrWhiteSpace(key)) throw new ArgumentException("缓存键无效");
            if (cachePopulate == null) throw new ArgumentNullException("返回缓存结果的委托无效");
            if (slidingExpiration == null && absoluteExpiration == null) throw new ArgumentException("absoluteExpiration或者slidingExpiration必须有一个不为空");

            if (MemoryCache.Default[key] == null)
            {
                lock (_locker)
                {
                    if (MemoryCache.Default[key] == null)
                    {
                        T data = cachePopulate();
                        if (data != null)
                        {
                            var item = new CacheItem(key, data);
                            var policy = CreatePolicy(slidingExpiration, absoluteExpiration);

                            MemoryCache.Default.Add(item, policy);
                        }
                    }
                }
            }

            return (T)MemoryCache.Default[key];
        }

        /// <summary>
        /// 得到缓存项
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">缓存键</param>
        /// <param name="cachePopulate">返回缓存结果的委托</param>
        /// <param name="clearDualTime">指定多长时间后过期</param>
        /// <returns></returns>
        public static T GetCacheT<T>(String key, Func<T> cachePopulate, TimeSpan clearDualTime)
        {
            return GetCacheItem(key, cachePopulate, DateTime.Now.Add(clearDualTime));
        }

        private static CacheItemPolicy CreatePolicy(TimeSpan? slidingExpiration, DateTime? absoluteExpiration)
        {
            var policy = new CacheItemPolicy();

            if (absoluteExpiration.HasValue)
            {
                policy.AbsoluteExpiration = absoluteExpiration.Value;
            }
            else if (slidingExpiration.HasValue)
            {
                policy.SlidingExpiration = slidingExpiration.Value;
            }

            policy.Priority = CacheItemPriority.Default;

            return policy;
        }
        /// <summary>
        /// 清除缓存项
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool ClearCache(string key = null)
        {
            try
            {
                if (string.IsNullOrEmpty(key))
                {//清除所有缓存
                    var marr = MemoryCache.Default.ToArray();
                    foreach (var item in marr)
                    {
                        MemoryCache.Default.Remove(item.Key);
                    }
                }
                else
                {//清除单个项 
                    MemoryCache.Default.Remove(key);
                }
                return true;
            }
            catch { }
            return false;
        }
    }
}
