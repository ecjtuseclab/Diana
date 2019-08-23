using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;
using Diana.Idal;
using Diana.model;
using basedal;
/// <summary>
///项目名称：Diana轻量级开发框架
///版本：0.0.1版
///开发团队成员：胡凯雨，张梦丽，艾美珍，易传佳，陈祚松，康文洋，张婷婷，王露，周庆，夏萍萍，陈兰兰
///模块和代码页功能描述：登录控制器
///最后修改时间：2018/1/20
///修改日志：
///2018/1/25  去掉了AccountsPrincipal方法中的设置所有参数值SetList（张梦丽）
/// </summary>
namespace Diana.bll
{
    [Serializable]
    public class SiteIdentity:IIdentity
    {
        user webuser = new user();
      
        #region  用户属性
        private int userid;       //用户id
        private string username;   //用户名
        private string password;  //用户口令
        private string pubkey;    //用户公钥
        private string prvkey;    //用户私钥

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            get
            {
                return username;
            }
        }
        /// <summary>
        /// 用户公钥
        /// </summary>
        public string PubKey
        {
            get
            {
                return pubkey;
            }
        }
        /// <summary>
        /// 用户私钥
        /// </summary>
        public string PrvKey
        {
            get
            {
                return prvkey;
            }
        }
        /// <summary>
        /// 用户编号
        /// </summary>
        public int UserID
        {
            get
            {
                return userid;
            }
        }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password
        {
            get
            {
                return password;
            }
        }

        #endregion

        #region IIdentity 接口中要求实现的:

        /// <summary>
        /// 当前用户的名称
        /// </summary>
        public string Name
        {
            get
            {
                return username;
            }
        }

        /// <summary>
        /// 获取所使用的身份验证的类型。
        /// </summary>
        public string AuthenticationType
        {
            get
            {
                return "Custom Authentication";
            }
            set
            {
                // do nothing
            }
        }
        /// <summary>
        /// 是否验证了用户
        /// </summary>
        public bool IsAuthenticated
        {
            get
            {
                return true;
            }
        }
        #endregion

        /// <summary>
        /// 根据用户名构造
        /// </summary>
        public SiteIdentity(string currentUserName)
        {
            user u = IdalCommon.IuserEx.getUser(currentUserName);
            if (u != null)
            {
                username = currentUserName;
                userid = u.id;
                pubkey = u.pubkey;
                prvkey = u.prikey;
                password = u.password;
                webuser = u;
            }
        }
        /// <summary>
        /// 根据用户ID构造
        /// </summary>
        public SiteIdentity(int currentUserID)
        {
            user u = IdalCommon.IuserEx.getUser(currentUserID);
            if (u != null)
            {
                username = u.username;
                userid = currentUserID;
                pubkey = u.pubkey;
                prvkey = u.prikey;
                password = u.password;
                webuser = u;
            }

        }
        /// <summary>
        /// 检查当前用户对象密码
        /// </summary>
        public bool CheckPassword(string password)
        {
            return IdalCommon.IuserEx.checkPassword(username, password);
        }
    }
}
