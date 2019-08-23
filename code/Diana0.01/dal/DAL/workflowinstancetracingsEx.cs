using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Diana.model;
using SqlSugar;
using Diana.Idal;
/// <summary>
///项目名称：Diana轻量级开发框架
///版本：0.0.1版
///开发团队成员：胡凯雨，张梦丽，艾美珍，易传佳，陈祚松，康文洋，张婷婷，王露，周庆，夏萍萍，陈兰兰
///模块和代码页功能描述：操作SQLServer数据库的工作流的方法
///最后修改时间：2018/1/26
/// </summary>
namespace Diana.dal
{
    public class workflowinstancetracingsEx : RepositoryBase<workflowinstancetracings>, IworkflowinstancetracingsEx
    {

        private static workflowinstancetracingsEx _instance;
        public static workflowinstancetracingsEx Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new workflowinstancetracingsEx();
                }
                return _instance;
            }
        }

        #region 1.基本操作
       
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="executer"></param>
        /// <param name="instancesid"></param>
        /// <param name="wfna"></param>
        /// <returns></returns>
        public int insert(string executer, int instancesid, workflownodeaction wfna)
        {
            workflownodeEx wfEx = new workflownodeEx();
            workflownode startnode = wfEx.getworkflownode(wfna.currentnodeid);
            workflownode endnode = wfEx.getworkflownode(wfna.nextnodeid);
            workflowinstancetracings wfiting = new workflowinstancetracings();
            wfiting.instanceid = instancesid;
            wfiting.startnode = startnode.wfnodememo;
            wfiting.endnode = endnode.wfnodememo;
            wfiting.executer = executer;
            wfiting.executeaction = wfna.nodeactionmemo;
            wfiting.executetime = DateTime.Now;
            workflowinstancetracingsEx wfinstancetracingEx = new workflowinstancetracingsEx();
            return wfinstancetracingEx.insert(wfiting).ObjToInt();
        }
        
        /// <summary>
        /// 根据id获取数据
        /// </summary>
        /// <param name="workflowinstancetracingsid"></param>
        /// <returns></returns>
        public workflowinstancetracings getworkflowinstancetracings(int workflowinstancetracingsid)
        {
            workflowinstancetracings table = db.Queryable<workflowinstancetracings>()
                                               .Where(d => d.id == workflowinstancetracingsid)
                                               .FirstOrDefault();
            return table;
        }
        /// <summary>
        /// 获得所有数据
        /// </summary>
        /// <returns></returns>
        public List<workflowinstancetracings> getAllworkflowinstancetracings()
        {
            List<workflowinstancetracings> table = db.Queryable<workflowinstancetracings>().ToList();
            return table;
        }

       
        /// <summary>
        /// 复制粘贴
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool copypandaste(string ids)
        {
            try
            {
                string[] id = ids.Split(',');
                workflowinstancetracings table = new workflowinstancetracings();
                for (int i = 0; i < id.Length; i++)
                {
                    table = getworkflowinstancetracings(int.Parse(id[i]));
                    db.Insert<workflowinstancetracings>(table);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        #endregion
    }
}
