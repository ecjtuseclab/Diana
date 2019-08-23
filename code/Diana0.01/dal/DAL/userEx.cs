using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Diana.model;
using Smatrix.Crypto;
using SqlSugar;
using Diana.Idal;
using System.Data;
/// <summary>
///项目名称：Diana轻量级开发框架
///版本：0.0.1版
///开发团队成员：胡凯雨，张梦丽，艾美珍，易传佳，陈祚松，康文洋，张婷婷，王露，周庆，夏萍萍，陈兰兰
///模块和代码页功能描述：操作SQLServer数据库的用户表的方法
///最后修改时间：2018/1/27
///修改日志：
///2018/1/27 添加delete(int id)方法，根据id删除一条用户数据以及关联信息(易传佳)
/// </summary>
namespace Diana.dal
{
    public class userEx : RepositoryBase<user>, IuserEx
    {
        #region 1.基本操作
        
      
        /// <summary>
        /// 根据用户名和密码添加新用户
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public int insert(string username, string password)
        {
            try
            {
                if (isLegalUsername(username))
                {
                    user u = SetUserKey(username, password);
                    return db.Insert<user>(u).ObjToInt();
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }
        /// <summary>
        /// 根据用户名删除用户
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool delete(string username)
        {
            try
            {
                user u = getUser(username);
                return delete(u);
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 根据id删除一条用户数据以及关联信息
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public bool delete(int id)
        {
            try
            {
                //根据id删除一条用户数据
                db.Delete<user>("id = @id", new { id = id });
                //根据用户id删除一条用户关联的角色信息
                db.Delete<user_role>("userid = @id", new { id = id });
                //根据用户id删除一条用户关联的分组信息
                db.Delete<user_group>("userid = @id", new { id = id });
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
        public bool delete(string[] ids)
        {
            try
            {
                //user u = getUser(id);
                //string[] strids = ids.Split(',');
                List<user> u = db.Queryable<user>().In(a => a.id, ids).ToList();
                db.Delete<user>(u);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
       
        /// <summary>
        /// 根据用户id和用户名更新
        /// </summary>
        /// <param name="id"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool update(int id, string username)
        {
            try
            {
                db.Update<user>(new { username = username }, u => u.id == id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool updatePsw(string username, string password)
        {
            try
            {
                //user u = getUser(username);
                user un = SetUserKey(username, password);
                db.Update<user>(un);
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
                db.Update<user>(dictionary, p => p.id == id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 根据用户名获得某用户数据
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public user getUser(string username)
        {
            user table = db.Queryable<user>().Where(d => d.username == username).FirstOrDefault();
            return table;
        }
        /// <summary>
        /// 根据用户id获得某用户数据
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public user getUser(int userid)
        {
            user table = db.Queryable<user>().Where(d => d.id == userid).FirstOrDefault();
            return table;
        }
        /// <summary>
        /// 获得所有用户数据
        /// </summary>
        /// <returns></returns>
        public List<user> getAllUser()
        {
            List<user> table = db.Queryable<user>().ToList();
            return table;
        }
        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public int getUserId(string username)
        {
            int id = 0;
            user table = db.Queryable<user>().Where(d => d.username == username).FirstOrDefault();
            if (table != null) id = table.id;
            return id;
        }
       
        /// <summary>
        /// 根据用户名获取公钥
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public string getPubkey(string username)
        {
            string pkey = "";
            user u = db.Queryable<user>().Where(d => d.username == username).FirstOrDefault();
            if (u != null) pkey = u.pubkey;
            return pkey;
        }
        /// <summary>
        /// 获取私钥
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public string getPrvkey(string username)
        {
            string prvkey = "";
            user u = db.Queryable<user>()
                .Where(d => d.username == username)
                .FirstOrDefault();
            if (u != null) prvkey = u.prikey;
            return prvkey;
        }
        /// <summary>
        /// 根据用户名获取ID
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public int getId(string username)
        {
            int id = 0;
            user u = db.Queryable<user>()
                .Where(d => d.username == username)
                .FirstOrDefault();
            if (u != null) id = u.id;
            return id;
        }
        /// <summary>
        /// 获取口令摘要，SM3散列
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public string getPasswordCipher(string username)
        {
            string pass = "";
            user u = db.Queryable<user>()
                       .Where(d => d.username == username)
                       .FirstOrDefault();
            if (u != null) pass = u.password;
            return pass;
        }
        /// <summary>
        /// 检查用户口令
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool checkPassword(string username, string password)
        {
            bool flag = false;
            TanSM3Ex sm3 = new TanSM3Ex();
            string digest = sm3.TanGetDigest(password);
            string cipher = getPasswordCipher(username);
            if (digest == cipher)
                flag = true;
            else
                flag = false;
            return flag;
        }
        /// <summary>
        /// 是否是有效的用户名，如果已存在，则返回false
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool isLegalUsername(string username)
        {
            bool flag = false;
            int count = db.Queryable<user>()
                .Where(d => d.username == username)
                .Count();
            if (0 == count)
                flag = true;
            else
                flag = false;
            return flag;
        }
        /// <summary>
        /// 签名
        /// </summary>
        /// <param name="username"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public string sign(string username, string msg)
        {
            //  string result = "";
            try
            {
                user u = db.Queryable<user>()
                    .Where(d => d.username == username)
                    .FirstOrDefault();
                if (u == null) return "";
                TanSM2Ex sm2 = new TanSM2Ex();
                string sig = "";
                sm2.TanSM2Sign(msg, u.prikey, out sig);
                return sig;
            }
            catch (Exception)
            {
                return "";
            }
        }
        /// <summary>
        /// 签名验证
        /// </summary>
        /// <param name="username"></param>
        /// <param name="msg"></param>
        /// <param name="sig"></param>
        /// <returns></returns>
        public bool verify(string username, string msg, string sig)
        {
            bool flag = false;
            try
            {
                user u = db.Queryable<user>()
                           .Where(d => d.username == username)
                           .FirstOrDefault();
                if (u == null) return false;
                TanSM2Ex sm2 = new TanSM2Ex();
                flag = sm2.TanSM2Verify(msg, u.pubkey, sig);
                return flag;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// 设置用户口令，并初始化用户秘钥对,如果用户已存在，则直接更新
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool setPassword(string username, string password)
        {
            bool flag = true;
            try
            {
                user u = SetUserKey(username, password);
                if (isLegalUsername(username))
                    db.Insert<user>(u);
                else
                    db.Update<user>(u);
            }
            catch (Exception)
            {
                flag = false;
            }
            return flag;
        }
        /// <summary>
        /// 设置用户公钥私钥
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private user SetUserKey(string username, string password)
        {
            TanSM3Ex sm3 = new TanSM3Ex();
            string digest = sm3.TanGetDigest(password);
            TanSM2Ex sm2 = new TanSM2Ex();
            sm2.TanGenSM2KeyPair();
            string pubkey = sm2.publicKey;
            string prvkey = sm2.privateKey;
            user u = getUser(username);
            if (u == null) u = new user();
            u.username = username;
            u.password = digest;
            u.pubkey = pubkey;
            u.prikey = prvkey;
            return u;
        }
        #endregion


        /// <summary>
        /// 提交表单
        /// </summary>
        /// <param name="u">用户对象</param>
        /// <param name="rolelistIds">角色id</param>
        /// <param name="grouplistIds">分组id</param>
        /// <param name="keyValue"></param>
        public void SubmitForm(user u, string rolelistIds, string grouplistIds, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                int id = Convert.ToInt32(keyValue);
                u.id = id;
                db.Update<user>(u);
                db.Delete<user_role>("userid=@id", new { id = id });
                roleAuthority(rolelistIds, id);
                db.Delete<user_group>("userid=@id", new { id = id });
                groupAuthority(grouplistIds, id);
            }
            else if (getUser(u.username) == null)
            {
                this.insert(u.username, u.password);
                List<user> userlist = this.getEntityList();
                roleAuthority(rolelistIds, userlist[userlist.Count - 1].id);
                groupAuthority(grouplistIds, userlist[userlist.Count - 1].id);
            }
        }

        /// <summary>
        /// 给用户分配分组
        /// </summary>
        /// <param name="grouplistIds">分组id</param>
        /// <param name="keyValue">用户id</param>
        public void groupAuthority(string grouplistIds, int keyValue)
        {
            user u = this.getEntityById(keyValue);
            IuserGroupEx userGroupEx = new userGroupEx();
            userGroupEx uge = new dal.userGroupEx();
            List<group> currentUserGroup = uge.getGroup(u.username);
            List<string> currentUserGroupIds = new List<string>();
            foreach (var item in currentUserGroup)
            {
                currentUserGroupIds.Add(item.id.ToString());
            }
            if (!string.IsNullOrEmpty(grouplistIds))
            {
                string[] ids = grouplistIds.Split(',');
                List<string> groupIds = ids.ToList();
                var interselectList = groupIds.Intersect(currentUserGroupIds).ToList();//对数据库中分组id和当前用户的分组id做交集
                var exceptList = currentUserGroupIds.Except(groupIds).ToList();////做差集，选出当前用户数据库中有但没有传入的分组id。
                if (exceptList != null)
                {
                    foreach (var item in exceptList)
                    {
                        userGroupEx.deleteUserGroup(keyValue, Convert.ToInt32(item));
                    }
                }
                var exceptList1 = groupIds.Except(currentUserGroupIds).ToList();//做差集，选出传入的分组id中有但数据库中该用户没有的分组id
                if (exceptList1 != null)
                {
                    foreach (var item in exceptList1)
                    {
                        userGroupEx.insertUserGroup(keyValue, Convert.ToInt32(item));
                    }
                }
            }
        }


        /// <summary>
        /// 给用户赋予角色
        /// </summary>
        /// <param name="rolelistIds">多个选中的角色id</param>
        /// <param name="keyValue">用户id</param>
        public void roleAuthority(string rolelistIds, int keyValue)
        {
            user u = this.getEntityById(keyValue);
            roleEx re = new roleEx();
            IuserRoleEx userRoleEx = new userRoleEx();
            List<role> currentUserAllRole = re.getUserRoles(u.username);
            List<string> currentUserRoleIds = new List<string>();
            foreach (var item in currentUserAllRole)
            {
                currentUserRoleIds.Add(item.id.ToString());
            }
            if (!string.IsNullOrEmpty(rolelistIds))
            {
                string[] ids = rolelistIds.Split(',');
                List<string> roleIdsList = ids.ToList();
                var interselectlist = roleIdsList.Intersect(currentUserRoleIds).ToList();//对数据库中角色id和当前用户的角色id做交集
                var exceptList = currentUserRoleIds.Except(roleIdsList).ToList();//做差集，选出当前用户数据库中有但没有传入的id。
                if (exceptList != null)
                {
                    foreach (var item in exceptList)
                    {
                        //userRoleEx.delete(Convert.ToInt32(item));
                        // userRoleEx.deleteEntity(Convert.ToInt32(item));
                        userRoleEx.deleteUerRole(keyValue, Convert.ToInt32(item));
                    }
                }
                var exceptList1 = roleIdsList.Except(currentUserRoleIds).ToList();//做差集，选出传入的角色id有但数据库中该用户没有的角色id
                if (exceptList1 != null)
                {
                    foreach (var item in exceptList1)
                    {
                        userRoleEx.insertUserRole(keyValue, Convert.ToInt32(item));
                    }
                }
            }
        }



        #region 扩展方法
        /// <summary>
        /// 根据用户名获取用户列表
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public List<user> getUserList(string username)
        {
            List<user> userList = new List<user>();
            if (!string.IsNullOrEmpty(username))
            {
                userList = getEntityList().Where(d => d.username == username).ToList();
            }
            else
            {
                userList = getEntityList();
            }
            return userList;
        }
        /// <summary>
        /// 获取用户表的分页并显示用户角色及分组
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize">页面的数据总数</param>
        /// <param name="searchStr">搜索的字段</param>
        /// <returns></returns>
        public List<user> getPaginationUserList(int pageIndex, int pageSize, Dictionary<string, object> searchStr)
        {
            userRoleEx ure = new userRoleEx();
            userGroupEx uge = new userGroupEx();
            List<user> entitylist = new List<user>();
            var entitydata = getEntityList(searchStr);
            var pagerfirst = (pageIndex - 1) * pageSize;
            entitylist = entitydata.Skip(pagerfirst).Take(pageSize).ToList();
            foreach (user us in entitylist)
            {
                us.rolelist = ure.getRoleName(us.id);
                us.grouplist = uge.getGroupName(us.id);
            }
            return entitylist;
        }
        /// <summary>
        /// 获取用户及角色和所属分组
        /// </summary>
        /// <returns></returns>
        public DataTable getUserRoleGroup(string keyValue)
        {
            var r = db.Queryable<user>().ToList();
            DataTable dt = db.Queryable<user>().ToDataTable();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                userRoleEx ure = new userRoleEx();
                userGroupEx uge = new userGroupEx();
                dt.Rows[i]["rolelist"] = ure.getRoleName(Convert.ToInt32(dt.Rows[i]["id"]));
                dt.Rows[i]["grouplist"] = uge.getGroupName(Convert.ToInt32(dt.Rows[i]["id"]));
            }
            return dt;

        }

        #endregion

    }
}
