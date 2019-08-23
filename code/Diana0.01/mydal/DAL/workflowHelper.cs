using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Diana.model;
using Diana.Idal;
/// <summary>
///项目名称：Diana轻量级开发框架
///版本：0.0.1版
///开发团队成员：胡凯雨，张梦丽，艾美珍，易传佳，陈祚松，康文洋，张婷婷，王露，周庆，夏萍萍，陈兰兰
///模块和代码页功能描述：操作Mysql数据库的工作流表的方法
///最后修改时间：2018/1/26
///日志说明：
/// </summary>
namespace Diana.mydal
{
    public class workflowHelper : IworkflowHelper
    {
        /// <summary>
        /// 检查操作者是否有权限执行该动作
        /// </summary>
        /// <param name="workflowName">工作流名称</param>
        /// <param name="dataid">业务表id</param>
        /// <param name="actionName">动作名称</param>
        /// <param name="operatorid">操作者ID</param>
        /// <returns></returns>
        public bool checkNodeAction(string workflowName, int dataid, string actionName, int operatorid)
        {
            workflowEx workflowEx = new workflowEx();
            workflow wf = workflowEx.getworkflow(workflowName);//获取该工作流名称对应的工作流数据
            workflowinstancesEx wfinstancesEx = new workflowinstancesEx();
            workflowinstances wfinstances = wfinstancesEx.getworkflowinstances(wf.id, dataid);//获得工作流执行结果
            //状态跃迁时
            //判断操作者是否有权限
            if (wfinstances != null)
            {
                return checkNodeAction(actionName, operatorid, wf, wfinstances);
            }
            else//新增动作只判断当前操作者是否有权限执行这个动作
            {
                workflownodeactionEx wfnaEx = new workflownodeactionEx();
                workflownodeaction wfna = wfnaEx.getworkflownodeaction(wf.id, actionName);
                workflownodeoperatorEx wfnoEx = new workflownodeoperatorEx();
                workflownodeoperator wfno = wfnoEx.getworkflownodeoperator(wfna.id, operatorid);
                if (wfno == null)
                {
                    return false;
                }
                return true;
            }
        }
        /// <summary>
        /// 检查操作者是否有权限执行该动作
        /// </summary>
        /// <param name="actionName">动作名称</param>
        /// <param name="operatorid">操作者ID</param>
        /// <param name="wf">工作流数据</param>
        /// <param name="wfinstances">工作流执行结果数据</param>
        /// <returns></returns>
        public bool checkNodeAction(string actionName, int operatorid, workflow wf, workflowinstances wfinstances)
        {
            workflownodeactionEx wfnaEx = new workflownodeactionEx();
            workflownodeaction wfna = wfnaEx.getworkflownodeaction(wf.id, actionName);
            if (wfna.currentnodeid != wfinstances.currentnodeid)
            {
                return false;
            }
            workflownodeoperatorEx wfnoEx = new workflownodeoperatorEx();
            workflownodeoperator wfno = wfnoEx.getworkflownodeoperator(wfna.id, operatorid);
            if (wfno == null)
            {
                return false;
            }
            return true;
        }

    }
}
