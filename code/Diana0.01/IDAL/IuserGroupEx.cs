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
///模块和代码页功能描述：用户分组接口，供IOC
///最后修改时间：2018/1/26
///日志说明：
/// </summary>
namespace Diana.Idal
{
    public interface IuserGroupEx : IRepositoryBase<user_group>
    {
       
        /// <summary>
        /// 根据多个用户名组成的字符串删除多个用户
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
        /// 根据用户组id获取用户组数据
        /// </summary>
        /// <param name="user_groupid">表的主键id</param>
        /// <returns></returns>
        user_group getUserGroup(int user_groupid);

        /// <summary>
        /// 获取所有的用户组数据
        /// </summary>
        /// <returns>List</returns>
        List<user_group> getAllUserGroup();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string getAllUserGroupView();

        /// <summary>
        /// 新增用户分组
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <param name="groupid">分组id</param>
        /// <returns></returns>
        int insertUserGroup(int userid, int groupid);
        /// <summary>
        /// 根据用户id和分组id删除用户分组
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <param name="groupid">分组id</param>
        void deleteUserGroup(int userid, int groupid);

        string getGroupName(int userid);

        /// <summary>
        /// 根据用户名获取分组
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        List<group> getGroup(string username);


    }
}
