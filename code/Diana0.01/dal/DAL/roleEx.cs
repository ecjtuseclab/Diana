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
///模块和代码页功能描述：操作SQLServer数据库的角色表的方法
///最后修改时间：2018/1/27
///修改日志：
///2018/1/27 添加delete(int id)方法，根据id删除一条角色数据以及关联信息(易传佳)
/// </summary>
namespace Diana.dal
{
    public class roleEx : RepositoryBase<role>, IroleEx
    {
        public bool delete(int id)
        {
            try
            {
                //根据id删除角色数据
                db.Delete<role>("id=@id", new { id = id });
                //根据角色id删除一条角色关联的资源信息
                db.Delete<role_resource>("roleid=@id", new { id = id });
                //根据角色id删除一条角色关联的动作信息
                db.Delete<role_action>("roleid=@id", new { id = id });
                return true;
            }
            catch (Exception)
            {
                return false;
            } 
        }

        /// <summary>
        /// 根据多个id组成的字符串删除多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool deleteList(string[] ids)
        {
            try
            {
                List<role> u = db.Queryable<role>().In(a => a.id, ids).ToList();
                db.Delete<role>(u);
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
                db.Update<role>(dictionary, p => p.id == id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 根据角色名称获取角色数据
        /// </summary>
        /// <param name="rolename">角色名称</param>
        /// <returns>一条角色数据</returns>
        public role getRole(string rolename)
        {
            try
            {
                role table = db.Queryable<role>().Where(d => d.rolename == rolename).FirstOrDefault();
                return table;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<role> getUserRoles(string userName)
        {
            List<role> roles = db.Queryable<role>().
                JoinTable<user_role>((s1, s2) => s1.id == s2.roleid)
                .JoinTable<user_role, user>((s1, s2, s3) => s2.userid == s3.id)
                .Where<user_role, user>((s1, s2, s3) => s3.username == userName)
                .Select<role>("s1.*")
                .ToList();
            return roles;

                    
        }
        /// <summary>
        /// 根据角色id获取角色数据
        /// </summary>
        /// <param name="roleid">角色数据id</param>
        /// <returns>一条角色数据</returns>
        public role getRole(int roleid) 
        {
            try
            {
                role table = db.Queryable<role>().Where(d => d.id == roleid).FirstOrDefault();
                return table;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 获取角色所有数据
        /// </summary>
        /// <param></param>
        /// <returns>角色数据list</returns>
        public List<role> getAllRole()
        {
            try
            {
                List<role> table = db.Queryable<role>().ToList();
                return table;
            }
            catch (Exception)
            {
                return null;
            }
        }
       
        /// <summary>
        /// 根据角色名称获取角色数据id
        /// </summary>
        /// <param name="rolename">角色名称</param>
        /// <returns>角色数据id</returns>
        public int getRoleId(string rolename)
        {
            try
            {
                int id = 0;
                role table = db.Queryable<role>().Where(d => d.rolename == rolename).FirstOrDefault();
                if (table != null) id = table.id;
                return id;
            }
            catch (Exception)
            {
                return -1;
            }
        }

       

        /// <summary>
        /// 判断角色名称是否已经存在
        /// </summary>
        /// <param name="rolename">角色名称</param>
        /// <returns>bool值</returns>
        public bool isLegalRoleName(string rolename)
        {
            int count = db.Queryable<role>().Where(d => d.rolename == rolename).Count();
            if (count == 0)
                return true;
            else
                return false;
        }

        #region 扩展方法
        /// <summary>
        /// 根据角色名获取角色列表
        /// </summary>
        /// <param name="rolename"></param>
        /// <returns></returns>
        public List<role> getRoleList(string rolename)
        {
            List<role> userList = new List<role>();
            if (!string.IsNullOrEmpty(rolename))
            {
                userList = getEntityList().Where(d => d.rolename == rolename).ToList();
            }
            else
            {
                userList = getEntityList();
            }
            return userList;
        }

        /// <summary>
        /// 获取角色表的分页显示
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize">页面的数据总数</param>
        /// <param name="searchStr">搜索的字段</param>
        /// <returns></returns>
        public List<role> getPaginationRoleList(int pageIndex, int pageSize, Dictionary<string, object> searchStr)
        {
            List<role> entitylist = new List<role>();
            var entitydata = getEntityList(searchStr);
            var pagerfirst = (pageIndex - 1) * pageSize;
            entitylist = entitydata.Skip(pagerfirst).Take(pageSize).ToList();
            return entitylist;
        }

        /// <summary>
        /// 表单提交
        /// </summary>
        /// <param name="roleEntity">表单提交的一条角色数据</param>
        /// <param name="keyValue">主键</param>
        public void SubmitForm(role roleEntity, string resourcepermissionIds, string actionpermissionIds, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                int id = Convert.ToInt32(keyValue);
                roleEntity.id = id;
                db.Update<role>(roleEntity);
                db.Delete<role_action>("roleid=@id", new { id = id });
                actionAuthority(actionpermissionIds, id);
                db.Delete<role_resource>("roleid=@id", new { id = id });
                resourceAuthority(resourcepermissionIds, id);
            }
            else
            {
                db.Insert<role>(roleEntity);
                List<role> rolelist = this.getEntityList();
                actionAuthority(actionpermissionIds, rolelist[rolelist.Count - 1].id);
                resourceAuthority(resourcepermissionIds, rolelist[rolelist.Count - 1].id);
            }
        }

        /// <summary>
        /// 动作授权
        /// </summary>
        /// <param name="actionpermissionIds"></param>
        /// <param name="keyValue"></param>
        public void actionAuthority(string actionpermissionIds, int keyValue)
        {
            if (!string.IsNullOrEmpty(actionpermissionIds))
            {
                string[] ids = actionpermissionIds.Split(',');
                IroleActionEx roleactionex = new roleActionEx();
                IactionEx actionex = new actionEx();
                for (int i = 0; i < ids.Length; i++)
                {
                    if (actionex.getEntityById(Convert.ToInt32(ids[i])).controlername != "0")
                    {
                        roleactionex.addRoleAction(Convert.ToInt32(ids[i]), keyValue);
                    }
                }
            }
        }

        /// <summary>
        /// 资源授权
        /// </summary>
        /// <param name="resourcepermissionIds">资源id集合的字符串</param>
        /// <param name="keyValue"></param>
        public void resourceAuthority(string resourcepermissionIds, int keyValue)
        {
            if (!string.IsNullOrEmpty(resourcepermissionIds))
            {
                string[] ids = resourcepermissionIds.Split(',');
                IroleResourceEx roleresourceex = new roleResourceEx();
                for (int i = 0; i < ids.Length; i++)
                {
                    role_resource ro = new role_resource();
                    ro.roleid = keyValue;
                    ro.resoureceid = Convert.ToInt32(ids[i]);
                    roleresourceex.insert(ro);
                }
            }
        }
        #endregion
    }
}
