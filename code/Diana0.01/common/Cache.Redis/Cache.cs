using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.Redis;
/// <summary>
///项目名称：Diana轻量级开发框架
///版本：0.0.1版
///开发团队成员：胡凯雨，张梦丽，艾美珍，易传佳，陈祚松，康文洋，张婷婷，王露，周庆，夏萍萍，陈兰兰
///模块和代码页功能描述：Redis缓存
///最后修改时间：2018/1/26
/// </summary>
namespace Diana.common.Cache.Redis
{
    public class Cache : ICache
    {
        public RedisClient MasterRedis;
        public RedisClient Slave1Redis;
        public RedisClient Slave2Redis;

        /// <summary>
        /// 使用本机地址
        /// </summary>
        public Cache()
        {
            MasterRedis = new RedisClient("127.0.0.1", 6379);  //主服务器
            Slave1Redis = new RedisClient("127.0.0.1", 6381);  //从服务器
            Slave2Redis = new RedisClient("127.0.0.1", 6382);  //从服务器
        }


        /// <summary>
        /// 读取缓存
        /// </summary>
        /// <param name="cacheKey">键</param>
        /// <returns></returns>
        public T GetCache<T>(string cacheKey) where T : class
        {
            return Slave1Redis.Get<T>(cacheKey);
        }

        /// <summary>
        /// 写入缓存
        /// </summary>
        /// <param name="value">对象数据</param>
        /// <param name="cacheKey">键</param>
        public void WriteCache<T>(T value, string cacheKey) where T : class
        {
            MasterRedis.Set(cacheKey, value);
        }
        /// <summary>
        /// 写入缓存
        /// </summary>
        /// <param name="value">对象数据</param>
        /// <param name="cacheKey">键</param>
        /// <param name="expireTime">到期时间</param>
        public void WriteCache<T>(T value, string cacheKey, DateTime expireTime) where T : class
        {
            MasterRedis.Set(cacheKey, value, expireTime);
        }
        /// <summary>
        /// 移除指定数据缓存
        /// </summary>
        /// <param name="cacheKey">键</param>
        public void RemoveCache(string cacheKey)
        {
            MasterRedis.Remove(cacheKey);
        }

        /// <summary>
        /// 移除全部缓存
        /// </summary>
        public void RemoveCache()
        {
        }

    }
}
