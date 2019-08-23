using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Diana.model;
using MySqlSugar;
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
    public class workflowinstancesEx : RepositoryBase<workflowinstances>, IworkflowinstancesEx
    {
        private static workflowinstancesEx _instance;
        public static workflowinstancesEx Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new workflowinstancesEx();
                }
                return _instance;
            }
        }

        #region 1.基本操作

        /// <summary>
        /// 根据工作流id和业务表id获取数据
        /// </summary>
        /// <param name="workflowid"></param>
        /// <param name="dataid"></param>
        /// <returns></returns>
        public workflowinstances getworkflowinstances(int workflowid, int dataid)
        {
            workflowinstances wfinstances = db.Queryable<workflowinstances>().Where(d => d.wfid == workflowid && d.ownertabledataid == dataid).FirstOrDefault();
            return wfinstances;
        }
        /// <summary>
        /// 添加工作流结果表数据
        /// </summary>
        /// <param name="wf"></param>
        /// <param name="instancesid"></param>
        /// <param name="wfna"></param>
        /// <param name="dataid"></param>
        /// <returns></returns>
        public int insert(workflow wf, int instancesid, workflownodeaction wfna, int dataid)
        {
            workflowinstancesEx wfinstancesEx = new workflowinstancesEx();
            workflowinstances wfinstacnes = new workflowinstances();
            wfinstacnes.ownertabledataid = dataid;
            wfinstacnes.nodcode = wfna.nodeactioncode;
            wfinstacnes.wfid = wf.id;
            wfinstacnes.currentnodeid = wfna.nextnodeid;
            instancesid = wfinstancesEx.insert(wfinstacnes);
            return instancesid;
        }
        /// <summary>
        /// 根据id获取数据
        /// </summary>
        /// <param name="workflowinstancesid"></param>
        /// <returns></returns>
        public workflowinstances getworkflowinstances(int workflowinstancesid)
        {
            workflowinstances table = db.Queryable<workflowinstances>().Where(d => d.id == workflowinstancesid).FirstOrDefault();
            return table;
        }
        /// <summary>
        /// 获得所有数据
        /// </summary>
        /// <returns></returns>
        public List<workflowinstances> getAllworkflowinstances()
        {
            List<workflowinstances> table = db.Queryable<workflowinstances>().ToList();
            return table;
        }

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
        public bool trace(string workflowName, int dataid, string actionName, int operatorid, string executer, string remark, bool isRecordTrace = true)
        {
            workflowEx workflowEx = new workflowEx();
            workflow wf = workflowEx.getworkflow(workflowName);
            int instancesid = 0;
            workflowinstancesEx wfinstancesEx = new workflowinstancesEx();
            workflowinstances wfinstances = wfinstancesEx.getworkflowinstances(wf.id, dataid);
            workflownodeactionEx wfnaEx = new workflownodeactionEx();
            workflownodeaction wfna = wfnaEx.getworkflownodeaction(wf.id, actionName);
            workflownodeEx wfnEx = new workflownodeEx();
            string sql = string.Empty;
            if (wfinstances != null)
            {
                int currentnodeid = wfna.nextnodeid;
                int? nodcodevalue = 0;
                if (wfna.nodetype == 2)//会签节点跳跃
                {

                    List<workflownodeaction> wfnas = wfnaEx.getcountersignnodeaction(wfna);//会签动作集合
                    nodcodevalue = (wfinstances.nodcode | wfna.nodeactioncode) == wfna.nodeactioncode ? wfinstances.nodcode : (wfinstances.nodcode + wfna.nodeactioncode);
                    if (nodcodevalue != wfnas.Select(p => p.nodeactioncode).Sum())
                    {
                        currentnodeid = wfna.currentnodeid;
                    }
                }
                //更新Trace表节点信息
                sql = string.Format(@"update {0} 
                                       set currentnodeid={1},
                                                  nodcode={2}
                                                 where id={3}",
                                          wf.wfinstancestable, currentnodeid, nodcodevalue, wfinstances.id);
                instancesid = wfinstances.id;
                db.SqlQueryDynamic(sql);
                //更新业务表工作流字段值

                workflownode wfn = wfnEx.getworkflownode(wfna.nextnodeid);
                string sqlstr = string.Format("update {0} set {1}={2} where id={3}", wf.wfownertable, wf.wffieldname, wfn.wfnodememo);
                db.SqlQueryDynamic(sqlstr);
            }
            else
            {
                instancesid = insert(wf, instancesid, wfna, dataid);
                //更新业务表工作流字段值
                workflownode wfn = wfnEx.getworkflownode(wfna.nextnodeid);
                string sqlstr = string.Format("update {0} set {1}='{2}' where id={3}", wf.wfownertable, wf.wffieldname, wfn.wfnodememo, dataid);
                db.SqlQueryDynamic(sqlstr);
            }
            if (isRecordTrace)
            {
                workflowinstancetracingsEx wfinstancetringEx = new workflowinstancetracingsEx();
                wfinstancetringEx.insert(executer, instancesid, wfna);
            }

            return true;
        }
        #endregion
    }
}
