using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Diana.Idal;
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
    public interface IworkflowHelper
    {
        /// <summary>
        /// 检查操作者是否有权限执行该动作
        /// </summary>
        /// <param name="workflowName">工作流名称</param>
        /// <param name="dataid">业务表id</param>
        /// <param name="actionName">动作名称</param>
        /// <param name="operatorid">操作者ID</param>
        /// <returns></returns>
        bool checkNodeAction(string workflowName, int dataid, string actionName, int operatorid);
        /// <summary>
        /// 检查操作者是否有权限执行该动作
        /// </summary>
        /// <param name="actionName">动作名称</param>
        /// <param name="operatorid">操作者ID</param>
        /// <param name="wf">工作流数据</param>
        /// <param name="wfinstances">工作流执行结果数据</param>
        /// <returns></returns>
        bool checkNodeAction(string actionName, int operatorid, workflow wf, workflowinstances wfinstances);

    }
}
