using Memcached.ClientLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
///项目名称：Diana轻量级开发框架
///版本：0.0.1版
///开发团队成员：胡凯雨，张梦丽，艾美珍，易传佳，陈祚松，康文洋，张婷婷，王露，周庆，夏萍萍，陈兰兰
///模块和代码页功能描述：Memcached缓存
///最后修改时间：2018/1/26
/// </summary>
namespace Diana.common.Cache.Memcached
{
    public class MemCache : ICache
    {
        private MemcachedClient memcachedClient;
        public MemCache()
        {
            string[] servers = { "127.0.0.1:11211" };//这里设置本机ip  端口固定11211
            //string strAppMemcachedServer= System.Configuration.ConfigurationManager.AppSettings["MemcachedServerList"];  //可配置在Webconfig
            //string[] servers = strAppMemcachedServer.Split(',');
            //初始化池
            SockIOPool pool = SockIOPool.GetInstance();
            pool.SetServers(servers);
            pool.InitConnections = 3;
            pool.MinConnections = 3;
            pool.MaxConnections = 5;
            pool.SocketConnectTimeout = 1000;
            pool.SocketTimeout = 3000;
            pool.MaintenanceSleep = 30;
            pool.Failover = true;
            pool.Nagle = false;
            pool.Initialize();
            //客户端实例
            MemcachedClient mc = new MemcachedClient();
            mc.EnableCompression = false;
            memcachedClient = mc;
        }


        public T GetCache<T>(string cacheKey) where T : class
        {
            return (T)memcachedClient.Get(cacheKey);
        }

        public void WriteCache<T>(T value, string cacheKey) where T : class
        {
            memcachedClient.Add(cacheKey, value);
        }

        public void WriteCache<T>(T value, string cacheKey, DateTime expireTime) where T : class
        {
            memcachedClient.Add(cacheKey, value, expireTime);
        }

        public void RemoveCache(string cacheKey)
        {
            memcachedClient.Delete(cacheKey);
        }

        public void RemoveCache()
        {

        }

        public void SetCache(string key, object value, DateTime extDate)
        {
            memcachedClient.Set(key, value, extDate);
        }

        public void SetCache(string key, object value)
        {
            memcachedClient.Set(key, value);
        }


    }
}
