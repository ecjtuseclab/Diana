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

    public interface IworkflowinstancesEx : IRepositoryBase<workflowinstances>
    {
        #region 1.基本操作
        
        /// <summary>
        /// 根据工作流id和业务表id获取数据
        /// </summary>
        /// <param name="workflowid"></param>
        /// <param name="dataid"></param>
        /// <returns></returns>
        workflowinstances getworkflowinstances(int workflowid, int dataid);
        /// <summary>
        /// 添加工作流结果表数据
        /// </summary>
        /// <param name="wf"></param>
        /// <param name="instancesid"></param>
        /// <param name="wfna"></param>
        /// <param name="dataid"></param>
        /// <returns></returns>
        int insert(workflow wf, int instancesid, workflownodeaction wfna, int dataid);
        /// <summary>
        /// 根据id获取数据
        /// </summary>
        /// <param name="workflowinstancesid"></param>
        /// <returns></returns>
        workflowinstances getworkflowinstances(int workflowinstancesid);
        /// <summary>
        /// 获得所有数据
        /// </summary>
        /// <returns></returns>
        List<workflowinstances> getAllworkflowinstances();
        
        #endregion
        #region 2.业务操作
        /// <summary>
        /// 节点跃迁方法
        /// </summary>
        /// <param name="workflowName">工作流名称</param>
        /// <param name="dataid">业务数据主码</param>
        /// <param name="actionName">动作名称</param>
        /// <param name="operatorid">操作者ID</param>
        /// <param name="executer">执行者</param>
        /// <param name="remark">工作流执行备注</param>
        /// <param name="isRecordTrace">是否记录工作流操作日志（默认记录）</param>
        /// <returns>返回是否执行成功</returns>
        bool trace(string workflowName, int dataid, string actionName, int operatorid, string executer, string remark, bool isRecordTrace = true);

        #endregion

    }
}
