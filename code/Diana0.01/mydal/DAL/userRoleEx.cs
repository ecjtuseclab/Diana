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
///模块和代码页功能描述：操作Mysql数据库的用户角色关联表的方法
///最后修改时间：2018/1/26
///日志说明：
/// </summary>
namespace Diana.mydal
{
    public class userRoleEx : RepositoryBase<user_role>, IuserRoleEx
    {
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
                List<user_role> u = db.Queryable<user_role>().In(a => a.id, ids).ToList();
                db.Delete<user_role>(u);
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
                db.Update<user_role>(dictionary, p => p.id == id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 根据用户名获取角色列表
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public List<role> getRole(string username)
        {
            List<role> rr = new List<role>();
            user uu = db.Queryable<user>().Where(d => d.username == username).FirstOrDefault();
            if (uu != null)
                rr = db.Queryable<role>()
                    .JoinTable<user_role>((s1, s2) => s1.id == s2.roleid)
                    .Where<user_role>((s1, s2) => s2.userid == uu.id)
                    .Select("s1.*")
                    .ToList();
            return rr;
        }
        /// <summary>
        /// 通过用户id获取所有关联表信息
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public List<user_role> getRole(int userid)
        {
            List<user_role> ur = db.Queryable<user_role>().Where(d => d.userid == userid).ToList();
            return ur;
        }
        /// <summary>
        /// 通过用户id和角色ID获取所有关联表信息
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="roleid"></param>
        /// <returns></returns>
        public user_role getRole(int userid, int roleid)
        {
            user_role ur = db.Queryable<user_role>().Where(d => d.userid == userid && d.roleid == roleid).FirstOrDefault();
            return ur;
        }
        /// <summary>
        /// 通过用户id获取所有关联表信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public user_role getUserRole(int id)
        {
            return db.Queryable<user_role>().Where(d => d.id == id).FirstOrDefault();
        }
        /// <summary>
        /// 通过用户名获取所有角色
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public List<string> getRoleName(string username)
        {
            List<role> rr = new List<role>();
            user uu = db.Queryable<user>().Where(d => d.username == username).FirstOrDefault();
            if (uu != null)
                rr = db.Queryable<role>()
                    .JoinTable<user_role>((s1, s2) => s1.id == s2.roleid)
                    .Where<user_role>((s1, s2) => s2.userid == uu.id)
                    .Select("s1.*")
                    .ToList();
            List<string> rname = new List<string>(); ;
            if (rr.Count > 0)
                foreach (var e in rr) rname.Add(e.rolename);
            return rname;
        }
        /// <summary>
        /// 通过用户id获取所有角色名称，多个角色用逗号隔开
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public string getRoleName(int userid)
        {
            List<role> rr = new List<role>();
            user uu = db.Queryable<user>().Where(d => d.id == userid).FirstOrDefault();
            if (uu != null)
                rr = db.Queryable<role>()
                    .JoinTable<user_role>((s1, s2) => s1.id == s2.roleid)
                    .Where<user_role>((s1, s2) => s2.userid == uu.id)
                    .Select("s1.*")
                    .ToList();
            string rname = ""; ;
            if (rr.Count > 0)
                foreach (var e in rr)
                {
                    rname += e.rolename;
                    rname += ',';
                }
            return rname.Trim(',');
        }
        /// <summary>
        /// 通过用户id获取所有角色id，多个角色id用逗号隔开
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public string getRoleId(int userid)
        {
            List<user_role> ur = db.Queryable<user_role>().Where(d => d.userid == userid).ToList();
            string roleids = ""; ;
            if (ur.Count > 0)
                foreach (var e in ur)
                {
                    roleids += e.roleid;
                    roleids += ',';
                }
            return roleids.Trim(',');

        }
        /// <summary>
        /// 获得所有用户角色数据
        /// </summary>
        /// <returns></returns>
        public List<user_role> getAllUserRole()
        {
            List<user_role> table = db.Queryable<user_role>().ToList();
            return table;
        }
        /// <summary>
        /// 是否是有效的用户名，存在返回true
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="roleid"></param>
        /// <returns></returns>
        public bool isExistURid(int userid, int roleid)
        {
            bool flag = false;
            int count = db.Queryable<user_role>()
                .Where(d => d.userid == userid && d.roleid == roleid)
                .Count();
            if (0 == count)//不存在
                flag = false;
            else
                flag = true;
            return flag;
        }

        #endregion


        #region 扩展方法
        /// <summary>
        /// 新增用户角色
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <param name="roleid">角色id</param>
        /// <returns></returns>
        public int insertUserRole(int userid, int roleid)
        {
            try
            {
                user_role ur = db.Queryable<user_role>().Where(d => d.roleid == roleid && d.userid == userid).FirstOrDefault();
                if (ur == null)
                {
                    user_role ure = new user_role();
                    ure.roleid = roleid;
                    ure.userid = userid;
                    return db.Insert<user_role>(ure).ObjToInt();
                }
                else
                    return ur.id;
            }
            catch (Exception)
            {
                return -1;
            }
        }
        /// <summary>
        /// 根据角色id删除用户角色
        /// </summary>
        /// <param name="roleid">角色id</param>
        public void deleteUerRole(int userid, int roleid)
        {
            //List<user_role> urList = db.Queryable<user_role>().Where(d => d.roleid == roleid).ToList();
            //foreach (var item in urList)
            //{
            //    this.delete(item.id);
            //}
            user_role ur = db.Queryable<user_role>().Where(d => d.roleid == roleid && d.userid == userid).FirstOrDefault();
            if (ur != null)
            {
                ur.roleid = roleid;
                ur.userid = userid;
                this.delete(ur);
            }
        }


        #endregion

    }
}
