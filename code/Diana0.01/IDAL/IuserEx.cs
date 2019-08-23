using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Diana.Idal;
using Diana.model;
using System.Data;
/// <summary>
///项目名称：Diana轻量级开发框架
///版本：0.0.1版
///开发团队成员：胡凯雨，张梦丽，艾美珍，易传佳，陈祚松，康文洋，张婷婷，王露，周庆，夏萍萍，陈兰兰
///模块和代码页功能描述：用户接口，供IOC
///最后修改时间：2018/1/26
///日志说明：
/// </summary>
namespace Diana.Idal
{
    public interface IuserEx : IRepositoryBase<user>
    {
        #region 1.基本操作
       
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        //int insert(user table);

        /// <summary>
        /// 根据用户名和密码添加新用户
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        int insert(string username, string password);

        /// <summary>
        /// 根据用户名删除用户
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        bool delete(string username);

        
        /// <summary>
        /// 根据用户id和用户名更新
        /// </summary>
        /// <param name="id"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        bool update(int id, string username);

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        bool updatePsw(string username, string password);

        /// <summary>
        /// 差异更新
        /// </summary>
        /// <param name="id">跟新数据主码</param>
        /// <param name="dictionary">需要更新的数据</param>
        /// <returns>返回更新结果（true/false）</returns>
        bool update(int id, Dictionary<string, object> dictionary);

        /// <summary>
        /// 根据用户名获得某用户数据
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        user getUser(string username);

        /// <summary>
        /// 根据用户id获得某用户数据
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        user getUser(int userid);

        /// <summary>
        /// 获得所有用户数据
        /// </summary>
        /// <returns></returns>
        List<user> getAllUser();

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        int getUserId(string username);

        /// <summary>
        /// 根据用户名获取公钥
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        string getPubkey(string username);

        /// <summary>
        /// 获取私钥
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        string getPrvkey(string username);

        /// <summary>
        /// 根据用户名获取ID
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        int getId(string username);

        /// <summary>
        /// 获取口令摘要，SM3散列
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        string getPasswordCipher(string username);

        /// <summary>
        /// 检查用户口令
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        bool checkPassword(string username, string password);

        /// <summary>
        /// 是否是有效的用户名，如果已存在，则返回false
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        bool isLegalUsername(string username);

        /// <summary>
        /// 签名
        /// </summary>
        /// <param name="username"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        string sign(string username, string msg);

        /// <summary>
        /// 签名验证
        /// </summary>
        /// <param name="username"></param>
        /// <param name="msg"></param>
        /// <param name="sig"></param>
        /// <returns></returns>
        bool verify(string username, string msg, string sig);

        /// <summary>
        /// 设置用户口令，并初始化用户秘钥对,如果用户已存在，则直接更新
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        bool setPassword(string username, string password);

        /// <summary>
        /// 设置用户公钥私钥
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        //   user SetUserKey(string username, string password);

        #endregion


        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        List<user> getUserList(string username);
        void roleAuthority(string rolelistIds, int keyValue);
        void groupAuthority(string grouplistIds, int keyValue);
        void SubmitForm(user u, string rolelistIds, string grouplistIds, string keyValue);
        DataTable getUserRoleGroup(string keyValue);
        List<user> getPaginationUserList(int pageIndex, int pageSize, Dictionary<string, object> searchStr);


    }
}
