using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
///<summary>
///项目名称：Diana轻量级开发框架
///版本：0.0.1版
///开发团队成员：胡凯雨，张梦丽，艾美珍，易传佳，陈祚松，康文洋，张婷婷，王露，周庆，夏萍萍，陈兰兰
///模块和代码页功能描述：实体属性类
///最后修改时间：2018/1/22
/// </summary>
namespace Diana.common.Util.Attributes
{
    /// <summary>
    /// 获取实体类Attribute自定义属性
    /// </summary>
    public class EntityAttribute
    {
        /// <summary>
        ///  获取实体对象Key
        /// </summary>
        /// <returns></returns>
        public static string GetEntityKey<T>()
        {
            Type type = typeof(T);
            PropertyInfo[] props = type.GetProperties();
            foreach (PropertyInfo prop in props)
            {
                foreach (System.Attribute attr in prop.GetCustomAttributes(true))
                {
                    KeyAttribute keyattribute = attr as KeyAttribute;
                    if (keyattribute != null)
                    {
                        return prop.Name;
                    }
                }
            }
            return null;
        }

        /// <summary>
        ///  获取实体对象表名
        /// </summary>
        /// <returns></returns>
        public static string GetEntityTable<T>()
        {
            Type objTye = typeof(T);
            string entityName = "";
            var tableAttribute = objTye.GetCustomAttributes(true).OfType<TableAttribute>();
            var descriptionAttributes = tableAttribute as TableAttribute[] ?? tableAttribute.ToArray();
            if (descriptionAttributes.Any())
                entityName = descriptionAttributes.ToList()[0].Name;
            else
            {
                entityName = objTye.Name;
            }
            return entityName;
        }
    }
}
