using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Diana.Idal;
/// <summary>
///项目名称：Diana轻量级开发框架
///版本：0.0.1版
///开发团队成员：胡凯雨，张梦丽，艾美珍，易传佳，陈祚松，康文洋，张婷婷，王露，周庆，夏萍萍，陈兰兰
///模块和代码页功能描述：操作Mysql数据库的工作流表的方法
///最后修改时间：2018/1/26
///日志说明：
/// </summary>
namespace  Diana.mydal
{
    public class workflowManager :  IworkflowManager
    {
        #region//前台通过指定多项查询条件查询、同时分页查询；xpp
        /// <summary>
        /// 多条件+分页查询
        /// </summary>
        /// <param name="pageIndex">当前页为第pageIndex</param>
        /// <param name="pageSize">每页数据条数</param>
        /// <param name="searchStr">接收前台传过来的参数，
        /// 按格式拼接转换成字典类型，查询条件，
        /// 拼接格式为("key=groupname|string，value=总经理")</param>
        /// <returns>List<group></returns>
        public List<workflowManager> getPage(int pageIndex, int pageSize, Dictionary<string, object> searchStr)
        {
            commonEx cmEx = new commonEx();
            List<workflowManager> gList = cmEx.getPage<workflowManager>(pageIndex, pageSize, searchStr);
            return gList;
        }
        #endregion
    }
}
