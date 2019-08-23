using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Diana.model;
using Diana.Idal;
using SqlSugar;
/// <summary>
///项目名称：Diana轻量级开发框架
///版本：0.0.1版
///开发团队成员：胡凯雨，张梦丽，艾美珍，易传佳，陈祚松，康文洋，张婷婷，王露，周庆，夏萍萍，陈兰兰
///模块和代码页功能描述：操作SQLServer数据库的工作流的方法
///最后修改时间：2018/1/26
/// </summary>

namespace Diana.dal
{
    public class workflowEx : RepositoryBase<workflow>, IworkflowEx
    {
        private static workflowEx _instance;
        public static workflowEx Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new workflowEx();
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
                //user u = getUser(id);
                //string[] strids = ids.Split(',');
                List<workflow> u = db.Queryable<workflow>().In(a => a.id, ids).ToList();
                db.Delete<workflow>(u);
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
                db.Update<workflow>(dictionary, p => p.id == id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }


        }
        /// <summary>
        /// 根据工作流名获得某条数据
        /// </summary>
        /// <param name="wfname"></param>
        /// <returns></returns>
        public workflow getworkflow(string wfname)
        {
            workflow table = db.Queryable<workflow>().Where(d => d.wfname == wfname).FirstOrDefault();
            return table;
        }
        /// <summary>
        /// 根据工作流id获得某条数据
        /// </summary>
        /// <param name="workflowid"></param>
        /// <returns></returns>
        public workflow getworkflow(int workflowid)
        {
            workflow table = db.Queryable<workflow>().Where(d => d.id == workflowid).FirstOrDefault();
            return table;
        }

        /// <summary>
        /// 获得所有工作流数据
        /// </summary>
        /// <returns></returns>
        public List<workflow> getAllworkflow()
        {
            List<workflow> table = db.Queryable<workflow>().ToList();
            return table;
        }
        /// <summary>
        /// 根据工作流名获取工作流ID
        /// </summary>
        /// <param name="wfname"></param>
        /// <returns></returns>
        public int getworkflowId(string wfname)
        {
            int id = 0;
            workflow table = db.Queryable<workflow>().Where(d => d.wfname == wfname).FirstOrDefault();
            if (table != null) id = table.id;
            return id;
        }

       

        /// <summary>
        /// 根据工作流名称返回工作流数据
        /// </summary>
        /// <param name="wfname"></param>
        /// <returns></returns>
        public List<workflow> getWorkflowList(string wfname)
        {
            List<workflow> workflowList = new List<workflow>();
            if (!string.IsNullOrEmpty(wfname))
            {
                workflowList = getEntityList().Where(d => d.wfname == wfname).ToList();
            }
            else
            {
                workflowList = getEntityList();
            }
            return workflowList;
        }

        public void SubmitForm(workflow workflowEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                int id = Convert.ToInt32(keyValue);
                workflowEntity.id = id;
                db.Update<workflow>(workflowEntity);
            }
            else
            {
                if (getworkflow(workflowEntity.wfname) == null)
                {
                    db.Insert<workflow>(workflowEntity);
                }
            }
        }
        #endregion
    }
}
