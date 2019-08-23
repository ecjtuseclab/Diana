using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using Diana.common.Util;
/// <summary>
///项目名称：Diana轻量级开发框架
///版本：0.0.1版
///开发团队成员：胡凯雨，张梦丽，艾美珍，易传佳，陈祚松，康文洋，张婷婷，王露，周庆，夏萍萍，陈兰兰
///模块和代码页功能描述：数据类型转换类
///最后修改时间：2017/11/25
/// </summary>
namespace Web
{
    public class DataConversionHelper
    {
        /// <summary>
        /// 将前台返回的formdata数据转换为对象
        /// </summary>
        /// <returns>T</returns>
        public static T GetTableByFormData<T>(string formdata)
        {
            //{"materialname":"他","materialtype":"3","inventorycount":"3"}//反序列化
            formdata = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(formdata);

            T table = Json.ToObject<T>(formdata);

            return table;
        }

        /// <summary>
        /// 两个对象的数据进行比较
        /// </summary>
        /// <returns>返回一个最新(oldTable)的对象数据</returns>
        public static T GetNewTableValue<T>(T oldTable, T newTable) where T : class,new()
        {
            Object oldValue;
            Object newValue;

            PropertyInfo[] mPi = typeof(T).GetProperties();

            for (int i = 0; i < mPi.Length; i++)
            {
                PropertyInfo pi = mPi[i];

                oldValue = pi.GetValue(oldTable, null);
                newValue = pi.GetValue(newTable, null);

                if (!oldValue.Equals(newValue))
                {
                    if (newValue != null)
                    {
                        pi.SetValue(oldTable, newValue);
                    }
                }
            }
            return oldTable;
        }

        /// <summary>
        /// 两个对象的数据进行比较
        /// </summary>
        /// <returns>返回一个最新(oldTable)的对象数据</returns>
        public static T GetNewTableValue<T>(T oldTable, string formdata) where T : class,new()
        {
            T tempT = GetTableByFormData<T>(formdata);
            oldTable = GetNewTableValue<T>(oldTable, tempT);
            return oldTable;
        }

        /// <summary>
        /// 将json数据反序列化为Dictionary
        /// </summary>
        /// <param name="jsonData">json数据</param>
        /// <returns>Dictionary</returns>
        public static Dictionary<string, object> JsonToDictionary(string jsonData)
        {
            //实例化JavaScriptSerializer类的新实例
            System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            try
            {
                //将指定的 JSON 字符串转换为 Dictionary<string, object> 类型的对象
                return jss.Deserialize<Dictionary<string, object>>(jsonData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //public static Dictionary<string, object> GetNewTableValue<T>(string formdata)
        //{
        //    Dictionary<string, object> fD = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(formdata);

        //}
    }
}