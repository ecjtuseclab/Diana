using Diana.common.Cache.Memcached;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TanMiniToolSet.Common;
/// <summary>
///项目名称：Diana轻量级开发框架
///版本：0.0.1版
///开发团队成员：胡凯雨，张梦丽，艾美珍，易传佳，陈祚松，康文洋，张婷婷，王露，周庆，夏萍萍，陈兰兰
///模块和代码页功能描述：缓存工厂方法
///最后修改时间：2018/1/26
/// </summary>
namespace Diana.common.Cache.Factory
{
    public class CacheFactory
    {

        public static string strCache = ConfigHelper.GetConfigString("CacheType");
        /// <summary>
        /// 定义通用的Repository
        /// </summary>
        /// <returns></returns>
        public static ICache Cache()
        {
            switch (strCache)
            {
                case "HttpRuntimeCache": return new Cache();
                case "RedisCache": return new Diana.common.Cache.Redis.Cache();
                case "Memcached": return new MemCache();
                default: return new Cache();
            }
        }
    }
}
