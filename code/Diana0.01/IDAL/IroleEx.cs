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
///模块和代码页功能描述：角色接口，供IOC
///最后修改时间：2018/1/26
///日志说明：
/// </summary>
namespace Diana.Idal
{
    public interface IroleEx : IRepositoryBase<role>
    {
        #region  基本操作

        /// <summary>
        /// 差异更新
        /// </summary>
        /// <param name="id">跟新数据主码</param>
        /// <param name="dictionary">需要更新的数据</param>
        /// <returns>返回更新结果（true/false）</returns>
        bool update(int id, Dictionary<string, object> dictionary);
        /// <summary>
        /// 根据角色名称获取角色数据
        /// </summary>
        /// <param name="rolename">角色名称</param>
        /// <returns>一条角色数据</returns>
        role getRole(string rolename);
        /// <summary>
        /// 获取用户角色列表
        /// </summary>
        /// <param name="userName">用户名称</param>
        /// <returns>用户角色</returns>
        List<role> getUserRoles(string userName);

        /// <summary>
        /// 根据角色id获取角色数据
        /// </summary>
        /// <param name="roleid">角色数据id</param>
        /// <returns>一条角色数据</returns>
        role getRole(int roleid);

        /// <summary>
        /// 获取角色所有数据
        /// </summary>
        /// <param></param>
        /// <returns>角色数据list</returns>
        List<role> getAllRole();

        /// <summary>
        /// 根据角色名称获取角色数据id
        /// </summary>
        /// <param name="rolename">角色名称</param>
        /// <returns>角色数据id</returns>
        int getRoleId(string rolename);

        /// <summary>
        /// 判断角色名称是否已经存在
        /// </summary>
        /// <param name="rolename">角色名称</param>
        /// <returns>bool值</returns>
        bool isLegalRoleName(string rolename);

      
        #endregion

        #region 扩展方法
        List<role> getRoleList(string rolename);

        List<role> getPaginationRoleList(int pageIndex, int pageSize, Dictionary<string, object> searchStr);

        /// <summary>
        /// 表单提交
        /// </summary>
        /// <param name="roleEntity">表单提交的一条角色数据</param>
        /// <param name="keyValue">主键</param>
        void SubmitForm(role r, string resourcepermissionIds, string actionpermissionIds, string keyValue);
        #endregion
    }  
}
