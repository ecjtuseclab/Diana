using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;
using Diana.Idal;
using Diana.common.Util;
/// <summary>
///项目名称：Diana轻量级开发框架
///版本：0.0.1版
///开发团队成员：胡凯雨，张梦丽，艾美珍，易传佳，陈祚松，康文洋，张婷婷，王露，周庆，夏萍萍，陈兰兰
///模块和代码页功能描述：公用的连接数据库的方法
///最后修改时间：2018/1/26
/// </summary>
namespace Diana.dal
{
    public class commonEx : DalBase, IcommonEx
    {
        #region 分页操作
        /// <summary>
        /// 分页后返回json格式数据
        /// </summary>
        /// <typeparam name="T">当前实体类</typeparam>
        /// <param name="pageIndex">当前页为第pageIndex</param>
        /// <param name="pageSize">每页数据条数</param>
        /// <param name="searchInfo">前台查询数据</param>
        /// <returns></returns>
        public string pageTotal<T>(int pageSize, int pageIndex, string searchInfo) where T : class,new()
        {
            Dictionary<string, object> searchStr = Json.ToObject<Dictionary<string, object>>(searchInfo.ToJson().ToString());
            List<T> ulist = getPage<T>(pageIndex, pageSize, searchStr);
            int uCount = pageCount<T>(searchStr);
            string json = "";
            json = "{\"total\":" + uCount + ",\"rows\":" + ulist.ToJson() + "}";
            return json;
        }
        /// <summary>
        /// 总条数
        /// </summary>
        /// <param name="searchStr">接收前台传过来的参数，
        /// 按格式拼接转换成字典类型，查询条件，
        /// 拼接格式为("key=groupname|string，value=总经理")</param>
        /// <returns>int<group></returns>
        public int pageCount<T>(Dictionary<string, object> searchStr) where T : class,new()
        {
            var page = db.Queryable<T>()
                .Where(getWhere(searchStr)).Count();
            return page;
        }

        /// <summary>
        /// 多条件+分页查询
        /// </summary>
        /// <param name="pageIndex">当前页为第pageIndex</param>
        /// <param name="pageSize">每页数据条数</param>
        /// <param name="searchStr">接收前台传过来的参数，
        /// 按格式拼接转换成字典类型，查询条件，
        /// 拼接格式为("key=groupname|string，value=总经理")</param>
        /// <returns>List<group></returns>
        public List<T> getPage<T>(int pageIndex, int pageSize, Dictionary<string, object> searchStr) where T : class,new()
        {
            //设置分页的默认参数
            int pageCount = 0;
            var page = db.Queryable<T>()
                         .Where(getWhere(searchStr))
                         //.Where(a=>a.id>0)
                         .OrderBy("id")
                         .ToPageList<T>(pageIndex, pageSize, ref pageCount);
            return page;
        }
        /// <summary>
        /// 返回where 条件
        /// </summary>
        /// <param name="searchStr">查询条件，拼接格式为("key=groupname|string，value=总经理")</param>
        /// <returns>string ：拼接后的where条件</returns>
        public string getWhere(Dictionary<string, object> searchStr)
        {
            string value = string.Empty;
            string where = " ''=''";
            if (searchStr.Count!=0)
            {
                searchStr.Remove("ctrl");
                searchStr.Remove("action");
                foreach (var dic in searchStr)
                {
                    string filedName = dic.Key;                        //字段名
                    Type fieldType = dic.Value.GetType();               //字段类型    
                    if (fieldType.IsValueType)//值类型
                        where += string.Format("and (({0} = {1})or({2}=0)) ", filedName, dic.Value, dic.Value);
                    else
                        where += string.Format("and (({0} like '%{1}%' )or ('{2}'=''))", filedName, dic.Value, dic.Value);//字符类型
                }
            }
            return where;
        }
        #endregion




    }
}
