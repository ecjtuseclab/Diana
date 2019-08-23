using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Diana.model;
/// <summary>
///项目名称：Diana轻量级开发框架
///版本：0.0.1版
///开发团队成员：胡凯雨，张梦丽，艾美珍，易传佳，陈祚松，康文洋，张婷婷，王露，周庆，夏萍萍，陈兰兰
///模块和代码页功能描述：用户角色接口，供IOC
///最后修改时间：2018/1/26
///日志说明：
/// </summary>
namespace Diana.Idal
{
    public interface IuserRoleEx : IRepositoryBase<user_role>
    {
        #region 基本操作
        
       
        /// <summary>
        /// 根据多个id组成的字符串删除多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool delete(string[] ids);
       

        /// <summary>
        /// 差异更新
        /// </summary>
        /// <param name="id">跟新数据主码</param>
        /// <param name="dictionary">需要更新的数据</param>
        /// <returns>返回更新结果（true/false）</returns>
        bool update(int id, Dictionary<string, object> dictionary);

        /// <summary>
        /// 根据用户名获取角色列表
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        List<role> getRole(string username);

        /// <summary>
        /// 通过用户id获取所有关联表信息
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        List<user_role> getRole(int userid);

        /// <summary>
        /// 通过用户id和角色ID获取所有关联表信息
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="roleid"></param>
        /// <returns></returns>
        user_role getRole(int userid, int roleid);

        /// <summary>
        /// 通过用户id获取所有关联表信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        user_role getUserRole(int id);

        /// <summary>
        /// 通过用户名获取所有角色
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        List<string> getRoleName(string username);

        /// <summary>
        /// 通过用户id获取所有角色名称，多个角色用逗号隔开
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        string getRoleName(int userid);

        /// <summary>
        /// 通过用户id获取所有角色id，多个角色id用逗号隔开
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        string getRoleId(int userid);

        /// <summary>
        /// 获得所有用户角色数据
        /// </summary>
        /// <returns></returns>
        List<user_role> getAllUserRole();

        /// <summary>
        /// 是否是有效的用户名，存在返回true
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="roleid"></param>
        /// <returns></returns>
        bool isExistURid(int userid, int roleid);

        #endregion
        /// <summary>
        /// 新增用户角色
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <param name="roleid">角色id</param>
        /// <returns>用户角色id</returns>
        int insertUserRole(int userid, int roleid);
        /// <summary>
        /// 根据角色id删除用户角色
        /// </summary>
        /// <param name="roleid">角色id</param>
        //void deleteEntity(int roleid);
        void deleteUerRole(int userid, int roleid);

    }
}
