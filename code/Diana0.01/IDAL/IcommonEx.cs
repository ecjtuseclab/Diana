using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Diana.model;
/// <summary>
///项目名称：Diana轻量级开发框架
///版本：0.0.1版
///开发团队成员：胡凯雨，张梦丽，艾美珍，易传佳，陈祚松，康文洋，张婷婷，王露，周庆，夏萍萍，陈兰兰
///模块和代码页功能描述：公用接口，供IOC
///最后修改时间：2018/1/26
///日志说明：
/// </summary>
namespace Diana.Idal
{

    public interface IcommonEx
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
        string pageTotal<T>(int pageSize, int pageIndex, string searchInfo) where T : class,new();
        /// <summary>
        /// 总条数
        /// </summary>
        /// <param name="searchStr">接收前台传过来的参数，
        /// 按格式拼接转换成字典类型，查询条件，
        /// 拼接格式为("key=groupname|string，value=总经理")</param>
        /// <returns>int<group></returns>
        int pageCount<T>(Dictionary<string, object> searchStr) where T : class,new();
        /// <summary>
        /// 多条件+分页查询
        /// </summary>
        /// <param name="pageIndex">当前页为第pageIndex</param>
        /// <param name="pageSize">每页数据条数</param>
        /// <param name="searchStr">接收前台传过来的参数，
        /// 按格式拼接转换成字典类型，查询条件，
        /// 拼接格式为("key=groupname|string，value=总经理")</param>
        /// <returns>List<group></returns>
        List<T> getPage<T>(int pageIndex, int pageSize, Dictionary<string, object> searchStr) where T : class,new();
        /// <summary>
        /// 返回where 条件
        /// </summary>
        /// <param name="searchStr">查询条件，拼接格式为("key=groupname|string，value=总经理")</param>
        /// <returns>string ：拼接后的where条件</returns>
        string getWhere(Dictionary<string, object> searchStr);
        #endregion
    }
}
