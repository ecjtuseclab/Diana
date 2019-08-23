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
///模块和代码页功能描述：角色动作接口，供IOC
///最后修改时间：2018/1/26
///日志说明：
/// </summary>
namespace Diana.Idal
{
    public interface IroleActionEx 
    {
        #region//前台通过指定多项查询条件查询、同时分页查询；
        /// <summary>
        /// 多条件+分页查询
        /// </summary>
        /// <param name="pageIndex">当前页为第pageIndex</param>
        /// <param name="pageSize">每页数据条数</param>
        /// <param name="searchStr">接收前台传过来的参数，
        /// 按格式拼接转换成字典类型，查询条件，
        /// 拼接格式为("key=groupname|string，value=总经理")</param>
        /// <returns>List<group></returns>
        List<role_action> getPage(int pageIndex, int pageSize, Dictionary<string, object> searchStr);

        #endregion
        /// <summary>
        /// 插入角色动作数据
        /// </summary>
        /// <param name="table">角色动作数据</param>
        /// <returns>数据主键id</returns>
        int insert(role_action table);
        /// <summary>
        /// 插入角色动作数据
        /// </summary>
        /// <param name="actionid">动作数据id</param>
        /// <param name="roleid">角色数据id</param>
        /// <returns>角色动作数据id</returns>
        int addRoleAction(int actionid, int roleid);
        /// <summary>
        /// 删除角色动作数据
        /// </summary>
        /// <param name="table">角色动作数据</param>
        /// <returns>bool值</returns>
        bool delete(role_action table);
        /// <summary>
        /// 删除角色动作数据
        /// </summary>
        /// <param name="roleid">角色id</param>
        /// <param name="actionid">动作id</param>
        /// <returns></returns>
        bool delete(int roleid, int actionid);
        /// <summary>
        /// 删除指定角色的所有角色动作数据
        /// </summary>
        /// <param name="roleid">角色数据id</param>
        /// <returns>bool值</returns>
        bool delete(int roleid);
        /// <summary>
        /// 删除所有角色动作数据
        /// </summary>
        /// <returns>bool值</returns>
        bool deleteAll();
        /// <summary>
        /// 更新角色动作数据
        /// </summary>
        /// <param name="table">角色动作数据</param>
        /// <returns>bool值</returns>
        bool update(role_action table);
        /// <summary>
        /// 差异更新
        /// </summary>
        /// <param name="id">跟新数据主码</param>
        /// <param name="dictionary">需要更新的数据</param>
        /// <returns>返回更新结果（true/false）</returns>
        bool update(int id, Dictionary<string, object> dictionary);
        /// <summary>
        /// 根据角色动作id获取角色动作数据
        /// </summary>
        /// <param name="roleactionid">角色数据id</param>
        /// <returns>一条角色动作数据</returns>
        role_action getRoleAction(int roleactionid);
        /// <summary>
        /// 获取角色动作所有数据
        /// </summary>
        /// <returns>角色动作数据list</returns>
        List<role_action> getAllRoleAction();
        /// <summary>
        /// 获取所有角色动作对象条数
        /// </summary>
        /// <returns></returns>
        int pageCount();
        /// <summary>
        /// 用字符分割符根据‘，’分割出字符串id值，再根据角色动作数据id获取角色动作数据，再插入一样的数据
        /// </summary>
        /// <param name="ids">传入的参数由角色动作数据id+','组成的字符串</param>
        /// <returns>bool值</returns>
        bool copyAndPaste(string ids);
        /// <summary>
        /// 获得指定角色的动作数据
        /// </summary>
        /// <param name="roleid">角色数据id</param>
        /// <param name="owner">动作数据控制器</param>
        /// <returns>动作数据list</returns>
        List<action> getRoleAllAction(int roleid, string owner = "");
        /// <summary>
        /// 获得指定角色的特定类型的动作数据
        /// </summary>
        /// <param name="roleid">角色数据id</param>
        /// <param name="type">角色类型</param>
        /// <param name="owner">动作数据控制器</param>
        /// <returns>动作数据list</returns>
        List<action> getRoleAction(int roleid, int type, string owner = "");

    }
}
