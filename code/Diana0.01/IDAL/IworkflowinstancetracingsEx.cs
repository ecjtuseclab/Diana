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
///模块和代码页功能描述：工作流接口，供IOC
///最后修改时间：2018/1/26
///日志说明：
/// </summary>
namespace Diana.Idal
{
    public interface IworkflowinstancetracingsEx : IRepositoryBase<workflowinstancetracings>
    {
        #region 1.基本操作
       
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="executer"></param>
        /// <param name="instancesid"></param>
        /// <param name="wfna"></param>
        /// <returns></returns>
         int insert(string executer, int instancesid, workflownodeaction wfna);
       
        /// <summary>
        /// 根据id获取数据
        /// </summary>
        /// <param name="workflowinstancetracingsid"></param>
        /// <returns></returns>
         workflowinstancetracings getworkflowinstancetracings(int workflowinstancetracingsid);
        /// <summary>
        /// 获得所有数据
        /// </summary>
        /// <returns></returns>
         List<workflowinstancetracings> getAllworkflowinstancetracings();
        /// <summary>
        /// 复制粘贴
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        #endregion

    }
}
