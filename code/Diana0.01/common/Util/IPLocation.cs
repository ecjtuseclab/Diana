using System.Collections.Generic;
using System.Text;
///<summary>
///项目名称：Diana轻量级开发框架
///版本：0.0.1版
///开发团队成员：胡凯雨，张梦丽，艾美珍，易传佳，陈祚松，康文洋，张婷婷，王露，周庆，夏萍萍，陈兰兰
///模块和代码页功能描述：获取IP地址信息类
///最后修改时间：2018/1/28
/// </summary>
namespace Diana.common.Util
{
    public static class IPLocation
    {
        /// <summary>
        /// 获取IP地址信息
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static string GetLocation(string ip)
        {
            string res = "";
            try
            {
                string url = "http://apis.juhe.cn/ip/ip2addr?ip=" + ip + "&dtype=json&key=b39857e36bee7a305d55cdb113a9d725";
                res = HttpMethods.HttpGet(url);
                var resjson = res.ToObject<objex>();
                res =resjson.result.area + " "+ resjson.result.location;
            }
            catch
            {
                res = "";
            }
            if (!string.IsNullOrEmpty(res))
            {
                return res;
            }
            try
            {
                string url = "https://sp0.baidu.com/8aQDcjqpAAV3otqbppnN2DJv/api.php?query=" + ip + "&resource_id=6006&ie=utf8&oe=gbk&format=json";
                res = HttpMethods.HttpGet(url, Encoding.GetEncoding("GBK"));
                var resjson = res.ToObject<obj>();
                res = resjson.data[0].location;
            }
            catch
            {
                res = "";
            }
           
          

            return res;
        }
        /// <summary>
        /// 百度接口
        /// </summary>
        public class obj
        {
            public List<dataone> data { get; set; }
        }
        public class dataone
        {
            public string location { get; set; }
        }
        /// <summary>
        /// 聚合数据
        /// </summary>
        public class objex {
            public string resultcode { get; set; }
            public dataoneex result { get; set; }
            public string reason { get; set; }
            public string error_code { get; set; }
        }
        public class dataoneex {
            public string area { get; set; }
            public string location { get; set; }
        }

    }
}
