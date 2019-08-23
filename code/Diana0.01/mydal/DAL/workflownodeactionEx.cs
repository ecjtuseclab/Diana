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
///模块和代码页功能描述：操作Mysql数据库的工作流节点动作表的方法
///最后修改时间：2018/1/26
///日志说明：
/// </summary>
namespace Diana.mydal
{

    public class workflownodeactionEx : RepositoryBase<workflownodeaction>, IworkflownodeactionEx
    {
        private static workflownodeactionEx _instance;
        public static workflownodeactionEx Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new workflownodeactionEx();
                }
                return _instance;
            }
        }

        #region 1.基本操作


        /// <summary>
        /// 根据多个id组成的字符串删除多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool delete(string[] ids)
        {
            try
            {
                List<workflownodeaction> u = db.Queryable<workflownodeaction>().In(a => a.id, ids).ToList();
                db.Delete<workflownodeaction>(u);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        /// <summary>
        /// 差异更新
        /// </summary>
        /// <param name="id">跟新数据主码</param>
        /// <param name="dictionary">需要更新的数据</param>
        /// <returns>返回更新结果（true/false）</returns>
        public bool update(int id, Dictionary<string, object> dictionary)
        {
            try
            {
                db.Update<workflownodeaction>(dictionary, p => p.id == id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// 获得所有节点动作数据
        /// </summary>
        /// <returns></returns>
        public List<workflownodeaction> getInitworkflownodeaction()
        {
            List<workflownodeaction> wfnas = db.Queryable<workflownodeaction>().ToList();
            return wfnas;
        }
        /// <summary>
        /// 根据工作流ID和动作名称获得节点动作
        /// </summary>
        /// <param name="workflowid"></param>
        /// <param name="actionname"></param>
        /// <returns></returns>
        public workflownodeaction getworkflownodeaction(int workflowid, string actionname)
        {
            workflownodeaction table = db.Queryable<workflownodeaction>().Where(d => d.wfid == workflowid && d.nodeactionname == actionname).FirstOrDefault();
            return table;
        }
        /// <summary>
        /// 根据id获得节点动作数据
        /// </summary>
        /// <param name="workflownodeactionid"></param>
        /// <returns></returns>
        public workflownodeaction getworkflownodeaction(int workflownodeactionid)
        {
            workflownodeaction table = db.Queryable<workflownodeaction>()
                                         .Where(d => d.id == workflownodeactionid)
                                         .FirstOrDefault();
            return table;
        }
        /// <summary>
        /// 根据节点动作获得同当前节点和下个节点、同节点类型的节点动作数据
        /// </summary>
        /// <param name="nodeaction"></param>
        /// <returns></returns>
        public List<workflownodeaction> getcountersignnodeaction(workflownodeaction nodeaction)
        {
            List<workflownodeaction> nodeactions = db.Queryable<workflownodeaction>()
                                                     .Where(d => d.currentnodeid == nodeaction.currentnodeid
                                                         && d.nextnodeid == nodeaction.nextnodeid
                                                         && d.nodetype == nodeaction.nodetype)
                                                     .ToList();
            return nodeactions;
        }
        /// <summary>
        /// 获得所有工作流节点动作数据
        /// </summary>
        /// <returns></returns>
        public List<workflownodeaction> getAllworkflownodeaction()
        {
            List<workflownodeaction> table = db.Queryable<workflownodeaction>().ToList();
            return table;
        }

        /// <summary>
        /// 根据节点动作名称返回节点动作数据
        /// </summary>
        /// <param name="nodeactionname"></param>
        /// <returns></returns>
        public List<workflownodeaction> getworkflownodeactionList(string nodeactionname)
        {
            List<workflownodeaction> workflownodeactionList = new List<workflownodeaction>();
            if (!string.IsNullOrEmpty(nodeactionname))
            {
                workflownodeactionList = getEntityList().Where(d => d.nodeactionname == nodeactionname).ToList();
            }
            else
            {
                workflownodeactionList = getEntityList();
            }
            return workflownodeactionList;
        }

        /// <summary>
        /// 表单提交
        /// </summary>
        /// <param name="wfnc"></param>
        /// <param name="KewValue"></param>
        public void SubmitForm(workflownodeaction wfnc, string KeyValue)
        {
            if (!string.IsNullOrEmpty(KeyValue))
            {
                int id = Convert.ToInt32(KeyValue);
                wfnc.id = id;
                db.Update<workflownodeaction>(wfnc);
            }
            else
            {
                db.Insert<workflownodeaction>(wfnc);
            }
        }
        #endregion


        public List<action> getactionList()
        {
            List<action> actions = db.SqlQueryDynamic("select * from action where actiontype==4 or (actiontype=2 and  id not in (select actionid from workflownodeaction))");
            return actions;
        }

        public workflownodeaction getworkflownodeaction(int workflowid, int actionid)
        {
            return db.Queryable<workflownodeaction>().Where(p => p.wfid == workflowid && p.actionid == actionid).FirstOrDefault();
        }



        public List<workflownodeaction> getEntityList(int wfid)
        {
            return db.Queryable<workflownodeaction>().Where(p => p.wfid == wfid).ToList();
        }
    }
    /// <summary>
    /// 节点动作检查结果
    /// </summary>
    public enum nodeActionCheckResult
    {
        Unknow = 2,//未知
        Allow = 1,//运行执行
        Deny = 0,//不允许执行
    }
}
