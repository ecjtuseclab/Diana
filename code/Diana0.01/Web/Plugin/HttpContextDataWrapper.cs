using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Collections.Specialized;
using System.Reflection;
using System.ComponentModel;
///项目名称：Diana轻量级开发框架
///版本：0.0.1版
///开发团队成员：胡凯雨，张梦丽，艾美珍，易传佳，陈祚松，康文洋，张婷婷，王露，周庆，夏萍萍，陈兰兰
///模块和代码页功能描述：HttpContext对象数据包装类
///最后修改时间：2017/11/25
/// </summary>
namespace Web
{
    /// <summary>
    /// HttpContent数据包装类
    /// </summary>
    public static class HttpContextDataWrapper
    {
        /// <summary>
        /// HttpContext 对象传输参数包装方法
        /// </summary>
        /// <param name="context"> HttpContext 对象</param>
        /// <returns>Dictionary类型数据</returns>
        public static Dictionary<string, object> WrapperToDictionary(HttpContext context)
        {
            Dictionary<string, object> dataDictionary = new Dictionary<string, object>();
            List<string> ParamKeys = GetParamKeys(context);
            foreach (string key in ParamKeys)
            {
                dataDictionary[key] = context.Request[key];
            }
            int fileCount = context.Request.Files.Count;
            if (fileCount > 0)
            {
                foreach (string fileKey in context.Request.Files.AllKeys)
                {
                    dataDictionary[fileKey] = context.Request.Files[fileKey];
                }
            }
            return dataDictionary;
        }
        /// <summary>
        /// 获取HttpContext 对象的参数的主键
        /// </summary>
        /// <param name="context"></param>
        /// <returns>HttpContext 对象的参数的主键集合</returns>
        private static List<string> GetParamKeys(HttpContext context)
        {
            List<string> ParamKeys = new List<string>();
            List<string> postKeys = context.Request.Form.AllKeys.ToList();
            List<string> getKeys = context.Request.QueryString.AllKeys.ToList();
            if (postKeys.Count > 0)
            {
                ParamKeys.AddRange(postKeys);
            }
            foreach (string key in getKeys)
            {
                if (!ParamKeys.Contains(key))
                {
                    ParamKeys.Add(key);
                }
            }
            return ParamKeys;
        }
        /// <summary>
        /// 转化HttpContext对象传输的数据为Json格式
        /// </summary>
        /// <param name="context">HttpContext对象</param>
        /// <returns>HttpContext对象数据的Json格式</returns>
        public static string WrapperToJson(HttpContext context)
        {
            string json = string.Empty;
            List<string> ParamKeys = GetParamKeys(context);
            if (ParamKeys.Count > 0)
            {
                json = "{";
                json += string.Format("\"{0}\":\"{1}\"", ParamKeys[0], context.Request[ParamKeys[0]]);
                for (int i = 1; i < ParamKeys.Count; i++)
                {
                    json += string.Format(",\"{0}\":\"{1}\"", ParamKeys[i], context.Request[ParamKeys[i]]);
                }
                json += "}";
            }
            return json;
        }
        /// <summary>
        /// 转化HttpContext对象以Post方式传输的数据
        /// </summary>
        /// <param name="context">HttpContext对象</param>
        /// <returns>返回Dictionary数据类型</returns>
        public static Dictionary<string, object> PostMethodDataWrapper(HttpContext context)
        {
            Dictionary<string, object> dataDictionary = new Dictionary<string, object>();
            List<string> paramKeys = context.Request.Form.AllKeys.ToList();
            foreach (string paramkey in paramKeys)
            {
                dataDictionary[paramkey] = context.Request[paramkey];
            }
            int fileCount = context.Request.Files.Count;
            if (fileCount > 0)
            {
                foreach (string fileKey in context.Request.Files.AllKeys)
                {
                    dataDictionary[fileKey] = context.Request.Files[fileKey];
                }
            }
            return dataDictionary;
        }
        /// <summary>
        /// 转化HttpContext对象以Get方式传输的数据
        /// </summary>
        /// <param name="context">HttpContext对象</param>
        /// <returns>Dictionary类型数据</returns>
        public static Dictionary<string, object> GetMethodDataWrapper(HttpContext context)
        {
            Dictionary<string, object> dataDictionary = new Dictionary<string, object>();
            List<string> paramKeys = context.Request.QueryString.AllKeys.ToList();
            foreach (string paramkey in paramKeys)
            {
                dataDictionary[paramkey] = context.Request.QueryString[paramkey];
            }
            return dataDictionary;
        }
        /// <summary>
        /// 将HttpContext传输数据转化为实体对象
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="context"></param>
        /// <returns>返回实体</returns>
        public static T DataToObject<T>(HttpContext context) where T : class,new()
        {
            Dictionary<string, object> dictionary = PostMethodDataWrapper(context);
            T t = new T();
            PropertyInfo[] propertys = t.GetType().GetProperties();
            foreach (PropertyInfo p in propertys)
            {
                if (dictionary.Keys.Contains(p.Name))
                    p.SetValue(t, ChanageType(dictionary[p.Name],p.PropertyType), null);
            }
            return t;
        }

        private static object ChanageType(this object value, Type convertsionType)
        {
            //判断convertsionType类型是否为泛型，因为nullable是泛型类,
            if (convertsionType.IsGenericType &&
                //判断convertsionType是否为nullable泛型类
                convertsionType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == null || value.ToString().Length == 0)
                {
                    return null;
                }
                //如果convertsionType为nullable类，声明一个NullableConverter类，该类提供从Nullable类到基础基元类型的转换
                NullableConverter nullableConverter = new NullableConverter(convertsionType);
                //将convertsionType转换为nullable对的基础基元类型
                convertsionType = nullableConverter.UnderlyingType;
            }
            return Convert.ChangeType(value, convertsionType);
        }
    }

}