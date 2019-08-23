using Diana.Idal;
using Diana.model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;
using Diana.model;
using Smatrix.Crypto;
/// <summary>
///项目名称：Diana轻量级开发框架
///版本：0.0.1版
///开发团队成员：胡凯雨，张梦丽，艾美珍，易传佳，陈祚松，康文洋，张婷婷，王露，周庆，夏萍萍，陈兰兰
///模块和代码页功能描述：第三方登录数据库相关操作
///最后修改时间：2018/1/26
/// </summary>
namespace Diana.dal
{
    public class threeloginInfoEx : RepositoryBase<threeloginInfo>, IthreeloginInfoEx
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
                List<threeloginInfo> u = db.Queryable<threeloginInfo>().In(a => a.id, ids).ToList();
                db.Delete<threeloginInfo>(u);
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
                db.Update<threeloginInfo>(dictionary, p => p.id == id);
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
        /// <param name="threeloginInfoid"></param>
        /// <returns></returns>
        public threeloginInfo getthreeloginInfo(int threeloginInfoid)
        {
            threeloginInfo table = db.Queryable<threeloginInfo>().Where(d => d.id == threeloginInfoid).FirstOrDefault();
            return table;
        }
        /// <summary>
        /// 获得所有数据
        /// </summary>
        /// <returns></returns>
        public List<threeloginInfo> getAllthreeloginInfo()
        {
            List<threeloginInfo> table = db.Queryable<threeloginInfo>().ToList();
            return table;
        }

        public threeloginInfo getthreeloginInfobyuid(string uid)
        {
            try
            {
                threeloginInfo threeloginInfo = db.Queryable<threeloginInfo>().FirstOrDefault(x => x.uid == uid && x.accessToken != "");
                return threeloginInfo;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void InsertthreeloginInfo(threeloginInfo threeloginInfo)
        {
            try
            {
                if (isLegalUsername(threeloginInfo.useName))
                {
                    userEx ue = new userEx();
                    ue.setPassword(threeloginInfo.useName, threeloginInfo.uid);
                    user table = getUser(threeloginInfo.useName);
                    db.Insert<user>(table).ObjToInt();
                    user_role ur = new user_role();
                    ur.roleid = 32;
                    ur.userid = getUser(table.username).id;
                    ur.rolescode = 0;
                    db.Insert<user_role>(ur).ObjToInt();
                }
            }
            catch (Exception ex)
            {
                
            }
        }


        ///// <summary>
        ///// 设置用户公钥私钥
        ///// </summary>
        ///// <param name="username"></param>
        ///// <param name="password"></param>
        ///// <returns></returns>
        //private user SetUserKey(string username, string password)
        //{
        //    TanSM3Ex sm3 = new TanSM3Ex();
        //    string digest = sm3.TanGetDigest(password);
        //    TanSM2Ex sm2 = new TanSM2Ex();
        //    sm2.TanGenSM2KeyPair();
        //    string pubkey = sm2.publicKey;
        //    string prvkey = sm2.privateKey;
        //    user u = getUser(username);
        //    if (u == null) u = new user();
        //    u.username = username;
        //    u.password = digest;
        //    u.pubkey = pubkey;
        //    u.prikey = prvkey;
        //    return u;
        //}

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
        /// 根据用户名获得某用户数据
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public user getUser(string username)
        {
            user table = db.Queryable<user>().Where(d => d.username == username).FirstOrDefault();
            return table;
        }
        #endregion

    }
}
