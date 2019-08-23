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
///模块和代码页功能描述：角色资源接口，供IOC
///最后修改时间：2018/1/26
///日志说明：
/// </summary>
namespace Diana.Idal
{
    public interface IroleResourceEx : IRepositoryBase<role_resource>
    {
        #region 1.基本操作
       
        /// <summary>
        ///判断新增的role_resource对象是否合法，即是否有同样的数据
        /// </summary>
        /// <param name="roleid">角色id</param>
        /// <param name="resourceid">资源id</param>
        /// <returns></returns>
        bool isLegalRoleResource(int roleid, int resourceid);
        
       
        /// <summary>
        /// 删除role_resource对象
        /// </summary>
        /// <param name="roleid">角色id</param>
        /// <param name="resourceid">资源id</param>
        /// <returns></returns>
        bool delete(int roleid, int resourceid);
        
        /// <summary>
        /// 差异更新
        /// </summary>
        /// <param name="id">跟新数据主码</param>
        /// <param name="dictionary">需要更新的数据</param>
        /// <returns>返回更新结果（true/false）</returns>
        bool update(int id, Dictionary<string, object> dictionary);
        /// <summary>
        /// 获取指定角色的特定类型和属主的资源列表
        /// </summary>
        /// <param name="roleid">角色id</param>
        /// <param name="type">资源类型</param>
        /// <param name="owner">资源owner</param>
        /// <returns>指定角色的role_resource对象列表</returns>
        List<resource> getRoleResource(int roleid, int type, string owner = "");
        
        /// <summary>
        /// 获取指定角色属主的资源列表
        /// </summary>
        /// <param name="roleid">角色id</param>
        /// <param name="owner">resourceowner</param>
        /// <returns>指定角色的role_resource对象列表</returns>
        List<resource> getRoleAllResource(int roleid, string owner = "");
        
        /// <summary>
        /// 根据role_resource id获取role_resource对象
        /// </summary>
        /// <param name="role_resourceid">role_resource的id</param>
        /// <returns>role_resource对象列表</returns>
        role_resource getRole_Resource(int role_resourceid);
       
        /// <summary>
        /// 获取所有的role_resource对象
        /// </summary>
        /// <returns>role_resource对象列表</returns>
        List<role_resource> getAllRole_Resource();
        #endregion
    }
}
