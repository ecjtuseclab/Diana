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
///模块和代码页功能描述：操作Mysql数据库的工作流节点操作表的方法
///最后修改时间：2018/1/26
///日志说明：
/// </summary>
namespace Diana.mydal
{
    public class workflownodeoperatorEx : RepositoryBase<workflownodeoperator>, IworkflownodeoperatorEx
    {
        private static workflownodeoperatorEx _instance;
        public static workflownodeoperatorEx Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new workflownodeoperatorEx();
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
                List<workflownodeoperator> u = db.Queryable<workflownodeoperator>().In(a => a.id, ids).ToList();
                db.Delete<workflownodeoperator>(u);
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
                db.Update<workflownodeoperator>(dictionary, p => p.id == id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// 根据节点动作id和操作者id即用户id获取节点操作者数据
        /// </summary>
        /// <param name="wfnodeactionid"></param>
        /// <param name="operatorid"></param>
        /// <returns></returns>
        public workflownodeoperator getworkflownodeoperator(int wfnodeactionid, int operatorid)
        {
            workflownodeoperator table = db.Queryable<workflownodeoperator>()
                                           .Where(d => d.nodeactionid == wfnodeactionid
                                               && d.operatorid == operatorid)
                                           .FirstOrDefault();
            return table;
        }
        /// <summary>
        /// 根据ID获取节点操作者数据
        /// </summary>
        /// <param name="workflownodeoperatorid"></param>
        /// <returns></returns>
        public workflownodeoperator getworkflownodeoperator(int workflownodeoperatorid)
        {
            workflownodeoperator table = db.Queryable<workflownodeoperator>()
                                           .Where(d => d.id == workflownodeoperatorid)
                                           .FirstOrDefault();
            return table;
        }
        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <returns></returns>
        public List<workflownodeoperator> getAllworkflownodeoperator()
        {
            List<workflownodeoperator> table = db.Queryable<workflownodeoperator>().ToList();
            return table;
        }



        public void SubmitForm(workflownodeoperator workflownodeoperatorEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                int id = Convert.ToInt32(keyValue);
                workflownodeoperatorEntity.id = id;
                db.Update<workflownodeoperator>(workflownodeoperatorEntity);
            }
            else
            {
                db.Insert<workflownodeoperator>(workflownodeoperatorEntity);
            }
        }
        #endregion


 

        public bool deletebynodeactionid(int nodeactionid)
        {
            try
            {
                db.Delete<workflownodeoperator>("nodeactionid=@nodeactionid", new { nodeactionid = nodeactionid });
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<workflownodeoperator> getEntityList(int workflowID)
        {
            List<workflownodeoperator> wfnoo = new List<workflownodeoperator>();
            wfnoo = db.Queryable<workflownodeoperator>().In(
                p => p.nodeactionid,
                workflownodeactionEx.Instance.getEntityList(workflowID).Select(e => e.id)
                ).ToList();
            return wfnoo;
        }
        public bool deletebynodeactionids(List<int> nodeactionids)
        {
            return db.Delete<workflownodeaction>("nodeactionid", nodeactionids);
        }
    }
}

