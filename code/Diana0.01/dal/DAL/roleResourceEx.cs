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
///模块和代码页功能描述：操作SQLServer数据库的角色资源关联表的方法
///最后修改时间：2018/1/26
/// </summary>
namespace Diana.dal
{
   
    public class roleResourceEx : RepositoryBase<role_resource>, IroleResourceEx
    {
        #region 1.基本操作
        /// <summary>
        ///判断新增的role_resource对象是否合法，即是否有同样的数据
        /// </summary>
        /// <param name="roleid">角色id</param>
        /// <param name="resourceid">资源id</param>
        /// <returns></returns>
        public bool isLegalRoleResource(int roleid, int resourceid) 
        {
            bool flag = false;
            int count = 0;
            try
            {
                count = db.Queryable<role_resource>().Where(d => d.roleid == roleid && d.resoureceid == resourceid).Count();
                if (0 == count)
                    flag = true;
                else
                    flag = false;
                return flag;
            }
            catch (Exception)
            {
                return false;
            }


        }
     
        /// <summary>
        /// 删除role_resource对象
        /// </summary>
        /// <param name="roleid">角色id</param>
        /// <param name="resourceid">资源id</param>
        /// <returns></returns>
        public bool delete(int roleid, int resourceid) 
        {

            try
            {
                db.Delete<role_resource>(d => d.resoureceid == resourceid && d.roleid == roleid);
                return true;
            }
            catch (Exception )
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
                db.Update<role_resource>(dictionary, p => p.id == id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 获取指定角色的特定类型和属主的资源列表
        /// </summary>
        /// <param name="roleid">角色id</param>
        /// <param name="type">资源类型</param>
        /// <param name="owner">资源owner</param>
        /// <returns>指定角色的role_resource对象列表</returns>
        public List<resource> getRoleResource(int roleid, int type, string owner = "")
        {
            List<resource> rr = new List<resource>();
            if (owner == "")//不指定属主则获取全部，否则获取指定属主的
            {
                rr = db.Queryable<resource>()
                       .JoinTable<role_resource>((s1, s2) => s1.id == s2.resoureceid)
                       .Where<role_resource>((s1, s2) => s2.roleid == roleid)
                       .Where<resource>((s1, s2) => s1.resourcetype == type)
                       .Select("s1.*")
                       .ToList();
            }
            else
            {
                rr = db.Queryable<resource>()
                       .JoinTable<role_resource>((s1, s2) => s1.id == s2.resoureceid)
                       .Where<role_resource>((s1, s2) => s2.roleid == roleid)
                       .Where<resource>((s1, s2) => s1.resourcetype == type)
                       .Where<resource>((s1, s2) => s1.resourceowner == owner)
                       .Select("s1.*")
                       .ToList();

            }

            return rr;
        }
        /// <summary>
        /// 获取指定角色属主的资源列表
        /// </summary>
        /// <param name="roleid">角色id</param>
        /// <param name="owner">resourceowner</param>
        /// <returns>指定角色的role_resource对象列表</returns>
        public List<resource> getRoleAllResource(int roleid, string owner = "")
        {
            List<resource> rr = new List<resource>();
            if (owner == "")//不指定属主则获取全部，否则获取指定属主的
            {
                rr = db.Queryable<resource>()
                       .JoinTable<role_resource>((s1, s2) => s1.id == s2.resoureceid)
                       .Where<role_resource>((s1, s2) => s2.roleid == roleid)
                    // .Where<resource>((s1, s2) => s1.resourcetype == type)
                       .Select("s1.*")
                       .ToList();
            }
            else
            {
                rr = db.Queryable<resource>()
                       .JoinTable<role_resource>((s1, s2) => s1.id == s2.resoureceid)
                       .Where<role_resource>((s1, s2) => s2.roleid == roleid)
                    //.Where<resource>((s1, s2) => s1.resourcetype == type)
                       .Where<resource>((s1, s2) => s1.resourceowner == owner)
                       .Select("s1.*")
                       .ToList();

            }

            return rr;
        }
        /// <summary>
        /// 根据role_resource id获取role_resource对象
        /// </summary>
        /// <param name="role_resourceid">role_resource的id</param>
        /// <returns>role_resource对象列表</returns>
        public role_resource getRole_Resource(int role_resourceid) 
        {
            role_resource table = db.Queryable<role_resource>().Where(d => d.id == role_resourceid).FirstOrDefault();
            return table;
        }
       /// <summary>
        /// 获取所有的role_resource对象
       /// </summary>
        /// <returns>role_resource对象列表</returns>
        public List<role_resource> getAllRole_Resource()
        {
            List<role_resource> table = db.Queryable<role_resource>().ToList();
            return table;
        }
       
        #endregion
    }
}
