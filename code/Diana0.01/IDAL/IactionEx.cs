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
///模块和代码页功能描述：动作接口，供IOC
///最后修改时间：2018/1/26
///日志说明：
///开发者：胡凯雨
///新增获取所有对象条数方法
///最后更新时间：2018/1/27
/// </summary>
namespace Diana.Idal
{
    public interface IactionEx:IRepositoryBase<action>
    {
        
        /// <summary>
        /// 差异更新
        /// </summary>
        /// <param name="id">跟新数据主码</param>
        /// <param name="dictionary">需要更新的数据</param>
        /// <returns>返回更新结果（true/false）</returns>
        bool update(int id, Dictionary<string, object> dictionary);
        /// <summary>
        /// 根据动作名称获得动作数据
        /// </summary>
        /// <param name="actionname">动作名称</param>
        /// <returns>一条动作数据</returns>
        action getAction(string actionname);
        /// <summary>
        /// 根据动作父级获得子动作数据
        /// </summary>
        /// <param name="actionname">动作名称</param>
        /// <returns>一条动作数据</returns>
        List<action> getActionByOwner(string actionowner);
        /// <summary>
        /// 根据动作数据id获得动作数据
        /// </summary>
        /// <param name="actionid">动作数据id</param>
        /// <returns>一条动作数据</returns>
        action getAction(int actionid);
        /// <summary>
        /// 获得所有动作数据
        /// </summary>
        /// <returns>动作数据List</returns>
        List<action> getAllAction();
        /// <summary>
        /// 根据动作名称获得动作数据id
        /// </summary>
        /// <param name="actionname">动作名称</param>
        /// <returns>动作数据id</returns>
        int getActionId(string actionname);

        List<action> getActionListbywfid(int wfid);


        #region 基本操作 ycj
        /// <summary>
        /// 获取动作列表
        /// </summary>
        /// <param name="actionname">动作名称</param>
        /// <returns></returns>
        List<action> getActionList(string actionname);

        /// <summary>
        /// 表单提交
        /// </summary>
        /// <param name="roleEntity">表单提交的一条角色数据</param>
        /// <param name="keyValue">主键</param>
        void SubmitForm(action a, string keyValue);
        #endregion
    }
}
