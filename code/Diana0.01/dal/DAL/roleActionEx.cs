using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Diana.Idal;
using Diana.model;
using SqlSugar;
/// <summary>
///项目名称：Diana轻量级开发框架
///版本：0.0.1版
///开发团队成员：胡凯雨，张梦丽，艾美珍，易传佳，陈祚松，康文洋，张婷婷，王露，周庆，夏萍萍，陈兰兰
///模块和代码页功能描述：操作SQLServer数据库的角色动作关联表的方法
///最后修改时间：2018/1/26
/// </summary>
namespace Diana.dal
{
    public class roleActionEx : RepositoryBase<role_action>, IroleActionEx
    {
       
        /// <summary>
        /// 插入角色动作数据
        /// </summary>
        /// <param name="actionid">动作数据id</param>
        /// <param name="roleid">角色数据id</param>
        /// <returns>角色动作数据id</returns>
        public int addRoleAction(int actionid, int roleid)
        {
            try
            {
                role_action r = db.Queryable<role_action>().Where(d => d.actionid == actionid && d.roleid == roleid).FirstOrDefault();
                if (r == null)
                {
                    role_action ra = new role_action();
                    ra.roleid = roleid;
                    ra.actionid = actionid;
                    return db.Insert<role_action>(ra).ObjToInt();
                }
                else
                    return r.id;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        /// <summary>
        /// 删除角色动作数据
        /// </summary>
        /// <param name="roleid">角色id</param>
        /// <param name="actionid">动作id</param>
        /// <returns></returns>
        public bool delete(int roleid, int actionid)
        {
            try
            {
                db.Delete<role_action>(d => d.actionid == actionid && d.roleid == roleid);
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
                db.Update<role_action>(dictionary, p => p.id == id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// 根据角色动作id获取角色动作数据
        /// </summary>
        /// <param name="roleactionid">角色数据id</param>
        /// <returns>一条角色动作数据</returns>
        public role_action getRoleAction(int roleactionid)
        {
            try
            {
                role_action table = db.Queryable<role_action>().Where(d => d.id == roleactionid).FirstOrDefault();
                return table;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 获取角色动作所有数据
        /// </summary>
        /// <returns>角色动作数据list</returns>
        public List<role_action> getAllRoleAction()
        {
            try
            {
                List<role_action> table = db.Queryable<role_action>().ToList();
                return table;
            }
            catch (Exception)
            {
                return null;
            }
        }
       

        /// <summary>
        /// 获得指定角色的动作数据
        /// </summary>
        /// <param name="roleid">角色数据id</param>
        /// <param name="owner">动作数据控制器</param>
        /// <returns>动作数据list</returns>
        public List<action> getRoleAllAction(int roleid, string owner = "")
        {
            List<action> rr = new List<action>();
            if (owner == "")//不指定属主则获取全部，否则获取指定属主的
            {
                rr = db.Queryable<action>()
                    .JoinTable<role_action>((s1, s2) => s1.id == s2.actionid)
                    .Where<role_action>((s1, s2) => s2.roleid == roleid)
                    .Select("s1.*")
                    .ToList();
            }
            else
            {

                rr = db.Queryable<action>()
                    .JoinTable<role_action>((s1, s2) => s1.id == s2.actionid)
                    .Where<role_action>((s1, s2) => s2.roleid == roleid)
                    .Where<action>((s1, s2) => s1.actionowner == owner)
                    .Select("s1.*")
                    .ToList();

            }

            return rr;
        }

        /// <summary>
        /// 获得指定角色的特定类型的动作数据
        /// </summary>
        /// <param name="roleid">角色数据id</param>
        /// <param name="type">角色类型</param>
        /// <param name="owner">动作数据控制器</param>
        /// <returns>动作数据list</returns>
        public List<action> getRoleAction(int roleid, int type, string owner = "")//获取指定角色的特定类型和属主的动作列表
        {
            List<action> rr = new List<action>();
            if (owner == "")//不指定属主则获取全部，否则获取指定属主的
            {

                rr = db.Queryable<action>()
                       .JoinTable<role_action>((s1, s2) => s1.id == s2.actionid)
                       .Where<role_action>((s1, s2) => s2.roleid == roleid)
                       .Where<action>((s1, s2) => s1.actiontype == type)
                       .Select("s1.*")
                       .ToList();

            }
            else
            {

                rr = db.Queryable<action>()
                       .JoinTable<role_action>((s1, s2) => s1.id == s2.actionid)
                       .Where<role_action>((s1, s2) => s2.roleid == roleid)
                       .Where<action>((s1, s2) => s1.actiontype == type)
                       .Where<action>((s1, s2) => s1.actionowner == owner)
                       .Select("s1.*")
                       .ToList();
            }

            return rr;
        }
       
    }
}
