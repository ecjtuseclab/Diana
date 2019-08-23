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
///模块和代码页功能描述：操作Mysql数据库的动作表的方法
///最后修改时间：2018/1/26
///日志说明：
/// </summary>
namespace Diana.mydal
{
    public class actionEx : RepositoryBase<action>, IactionEx
    {
        #region 1.基础操作

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
                db.Update<action>(dictionary, p => p.id == id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        /// <summary>
        /// 根据动作名称获得动作数据
        /// </summary>
        /// <param name="actionname">动作名称</param>
        /// <returns>一条动作数据</returns>
        public action getAction(string actionname)
        {
            try
            {
                action table = db.Queryable<action>().Where(d => d.actionname == actionname).FirstOrDefault();
                return table;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 根据动作父级获得子动作数据
        /// </summary>
        /// <param name="actionname">动作名称</param>
        /// <returns>一条动作数据</returns>
        public List<action> getActionByOwner(string actionowner)
        {
            try
            {
                List<action> table = db.Queryable<action>().Where(d => d.actionowner == actionowner).ToList();
                return table;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 根据动作数据id获得动作数据
        /// </summary>
        /// <param name="actionid">动作数据id</param>
        /// <returns>一条动作数据</returns>
        public action getAction(int actionid)
        {
            try
            {
                action table = db.Queryable<action>().Where(d => d.id == actionid).FirstOrDefault();
                return table;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 获得所有动作数据
        /// </summary>
        /// <returns>动作数据List</returns>
        public List<action> getAllAction()
        {
            try
            {
                List<action> table = db.Queryable<action>().ToList();
                return table;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 根据动作名称获得动作数据id
        /// </summary>
        /// <param name="actionname">动作名称</param>
        /// <returns>动作数据id</returns>
        public int getActionId(string actionname)
        {
            try
            {
                int id = 0;
                action table = db.Queryable<action>().Where(d => d.actionname == actionname).FirstOrDefault();
                if (table != null) id = table.id;
                return id;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        #endregion
        public List<action> getActionListbywfid(int wfid)
        {

            workflow wf = workflowEx.Instance.getEntityById(wfid);
            if (wf != null)
            {
                return db.Queryable<action>().Where(p => p.actionowner == wf.wfname).ToList();
            }
            return null;
        }

        #region 基本操作 ycj
        /// <summary>
        /// 根据角色名获取角色列表
        /// </summary>
        /// <param name="rolename"></param>
        /// <returns></returns>
        public List<action> getActionList(string actionname)
        {
            List<action> actionList = new List<action>();
            if (!string.IsNullOrEmpty(actionname))
            {
                actionList = getEntityList().Where(d => d.actionname == actionname).ToList();
            }
            else
            {
                actionList = getEntityList();
            }
            return actionList;
        }

        /// <summary>
        /// 表单提交
        /// </summary>
        /// <param name="roleEntity">表单提交的一条角色数据</param>
        /// <param name="keyValue">主键</param>
        public void SubmitForm(action actionEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                int id = Convert.ToInt32(keyValue);
                actionEntity.id = id;
                db.Update<action>(actionEntity);
            }
            else
            {
                db.Insert<action>(actionEntity);
            }
        }
        #endregion
    }
}
