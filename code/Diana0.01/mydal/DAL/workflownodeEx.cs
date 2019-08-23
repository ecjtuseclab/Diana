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
///模块和代码页功能描述：操作Mysql数据库的工作流程节点的方法
///最后修改时间：2018/1/26
///日志说明：
/// </summary>
namespace Diana.mydal
{
    public class workflownodeEx : RepositoryBase<workflownode>, IworkflownodeEx
    {
        private static workflownodeEx _instance;
        public static workflownodeEx Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new workflownodeEx();
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
                List<workflownode> u = db.Queryable<workflownode>().In(a => a.id, ids).ToList();
                db.Delete<workflownode>(u);
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
                db.Update<workflownode>(dictionary, p => p.id == id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// 根据工作流节点id获取节点信息
        /// </summary>
        /// <param name="workflownodeid"></param>
        /// <returns></returns>
        public workflownode getworkflownode(int workflownodeid)
        {
            workflownode table = db.Queryable<workflownode>().Where(d => d.id == workflownodeid).FirstOrDefault();
            return table;
        }
        /// <summary>
        /// 获得所有数据
        /// </summary>
        /// <returns></returns>
        public List<workflownode> getAllworkflownode()
        {
            List<workflownode> table = db.Queryable<workflownode>().ToList();
            return table;
        }

        public List<workflownode> getWorkflownodeList(string wfnodename)
        {
            List<workflownode> workflownodeList = new List<workflownode>();
            if (!string.IsNullOrEmpty(wfnodename))
            {
                workflownodeList = getEntityList().Where(d => d.wfnodename == wfnodename).ToList();
            }
            else
            {
                workflownodeList = getEntityList();
            }
            return workflownodeList;
        }

        public void SubmitForm(workflownode workflownodeEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                int id = Convert.ToInt32(keyValue);
                workflownodeEntity.id = id;
                db.Update<workflownode>(workflownodeEntity);
            }
            else
            {
                db.Insert<workflownode>(workflownodeEntity);
            }
        }
        #endregion


        public List<workflownode> getEntityList(int workflowID)
        {

            return db.Queryable<workflownode>().Where(p => p.wfid == workflowID).ToList();
        }
    }

}
