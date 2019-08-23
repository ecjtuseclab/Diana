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
///模块和代码页功能描述：操作SQLServer数据库的用户与组的关联表的方法
///最后修改时间：2018/1/26
/// </summary>

namespace Diana.dal
{

    public class userGroupEx :  RepositoryBase<user_group>, IuserGroupEx
    {
        #region 1.基本操作
       
       
        /// <summary>
        /// 根据多个用户名组成的字符串删除多个用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool delete(string[] ids)
        {
            try
            {
                //user u = getUser(id);
                //string[] strids = ids.Split(',');
                List<user_group> u = db.Queryable<user_group>().In(a => a.id, ids).ToList();
                db.Delete<user_group>(u);
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
                db.Update<user_group>(dictionary, p => p.id == id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }


        }
        /// <summary>
        /// 根据用户组id获取用户组数据
        /// </summary>
        /// <param name="user_groupid">表的主键id</param>
        /// <returns></returns>
        public user_group getUserGroup(int user_groupid)
        {
            user_group table = db.Queryable<user_group>().Where(d => d.id == user_groupid).FirstOrDefault();
            return table;
        }

        /// <summary>
        /// 获取所有的用户组数据
        /// </summary>
        /// <returns>List</returns>
        public List<user_group> getAllUserGroup()
        {
            List<user_group> table = db.Queryable<user_group>().ToList();
            return table;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string getAllUserGroupView()
        {
            var ugv =
                db.Queryable<user_group>()
                .JoinTable<user>((ug, u) => ug.userid == u.id) //默认left join
                .ToJson();
            return ugv;
        }
       
        #endregion



       
        /// <summary>
        /// 根据用户名获取分组列表
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public List<group> getGroup(string username)
        {
            List<group> g = new List<group>();
            user uu = db.Queryable<user>().Where(d => d.username == username).FirstOrDefault();
            if (uu != null)
            {
                g = db.Queryable<group>()
                    .JoinTable<user_group>((s1, s2) => s1.id == s2.groupid)
                    .Where<user_group>((s1, s2) => s2.userid == uu.id)
                    .Select("s1.*")
                    .ToList();
            }
            return g;
        }
        /// <summary>
        /// 根据用户id获取分组名称
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <returns>分组名称</returns>
        public string getGroupName(int userid)
        {
            List<group> g = new List<group>();
            user uu = db.Queryable<user>().Where(d => d.id == userid).FirstOrDefault();
            if (uu != null)
            {
                g = db.Queryable<group>()
                    .JoinTable<user_group>((s1, s2) => s1.id == s2.groupid)
                    .Where<user_group>((s1, s2) => s2.userid == userid)
                    .Select("s1.*")
                    .ToList();
            }
            string gname = "";
            if (g.Count > 0)
                foreach (var e in g)
                {
                    gname += e.groupname;
                    gname += ',';
                }
            return gname;
        }
        /// <summary>
        /// 新增用户分组
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <param name="groupid">分组id</param>
        /// <returns></returns>
        public int insertUserGroup(int userid, int groupid)
        {
            try
            {
                user_group ug = db.Queryable<user_group>().Where(d => d.groupid == groupid && d.userid == userid).FirstOrDefault();
                if (ug == null)
                {
                    user_group ugp = new user_group();
                    ugp.userid = userid;
                    ugp.groupid = groupid;
                    return db.Insert<user_group>(ugp).ObjToInt();
                }
                else
                    return ug.id;

            }
            catch (Exception)
            {
                return -1;
            }
        }
        /// <summary>
        /// 根据用户id和分组id删除用户分组
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <param name="groupid">分组id</param>
        public void deleteUserGroup(int userid, int groupid)
        {
            user_group ug = db.Queryable<user_group>().Where(d => d.userid == userid && d.groupid == groupid).FirstOrDefault();
            if (ug != null)
            {
                this.delete(ug);
            }
        }
    }
}
