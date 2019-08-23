using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Diana.model;
using basedal;
using Diana.Idal;
/// <summary>
///项目名称：Diana轻量级开发框架
///版本：0.0.1版
///开发团队成员：胡凯雨，张梦丽，艾美珍，易传佳，陈祚松，康文洋，张婷婷，王露，周庆，夏萍萍，陈兰兰
///模块和代码页功能描述：获取用户的所有权限资源（不包括超级超级管理员）
///最后修改时间：2018/1/20
///修改日志：
///2018/1/25 修改了将所有基于用户的权限资源改为基于用户的当前角色的权限资源（张梦丽）
/// </summary>
namespace Diana.bll
{ 
    public class UserInfo
    {
        /// <summary>
        /// 获取指定用户的特定类型和属主的所有类型的动作列表,会含有重复项
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="owner">上级</param>
        /// <param name="currentrole">当前角色</param>
        /// <returns></returns>
        public static List<action> getUserAllAction(string username, role currentrole, string owner = "")
        {
            List<action> rr = new List<action>();
            IroleActionEx _roleActionEx = IocModule.GetEntity<IroleActionEx>();
            if (currentrole != null)
            {
                rr.AddRange(_roleActionEx.getRoleAllAction(currentrole.id, owner));
            }
            else
            {
                List<role> rolelist = IdalCommon.IroleEx.getUserRoles(username);
                if (rolelist.Count > 0)
                {
                    foreach (var e in rolelist)
                    {
                        rr.AddRange(_roleActionEx.getRoleAllAction(e.id, owner));
                    }
                }
            }
            
            rr = rr.Where((x, i) => rr.FindIndex(z => z.id == x.id) == i).ToList();//Lambda表达式去重 
            return rr;
        }
        /// <summary>
        /// 获取指定用户的特定类型和属主的动作列表,会含有重复项
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="type">动作类型</param>
        /// <param name="owner">上级</param>
        /// <returns></returns>
        public static List<action> getUserAction(string username, int type, role currentrole, string owner = "")
        {
            List<action> rr = new List<action>();
            IroleActionEx _roleActionEx = IocModule.GetEntity<IroleActionEx>();
            if (currentrole != null)
            {
                rr.AddRange(_roleActionEx.getRoleAllAction(currentrole.id, owner));
            }
            else
            {
                List<role> rolelist = IocModule.GetEntity<IuserRoleEx>().getRole(username);
                if (rolelist.Count > 0)
                {
                    foreach (var e in rolelist)
                    {
                        rr.AddRange(_roleActionEx.getRoleAction(e.id, type, owner));
                    }
                   
                }
            }
           
            rr = rr.Where((x, i) => rr.FindIndex(z => z.id == x.id) == i).ToList();//Lambda表达式去重  
            return rr;
        }
        /// <summary>
        /// 获取指定用户的特定类型和属主的资源列表
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="type"></param>
        /// <param name="owner"></param>
        /// <param name="currentrole"></param>
        /// <returns></returns>
        public static List<resource> getUserResource(string username, int type, role currentrole, string owner = "")
        {
             List<resource> rr = new List<resource>();
            IroleResourceEx _roleResourceEx = IocModule.GetEntity<IroleResourceEx>();
            rr.AddRange(_roleResourceEx.getRoleResource(currentrole.id, type, owner));
            rr = rr.Where((x, i) => rr.FindIndex(z => z.id == x.id) == i).ToList();//Lambda表达式去重
            return rr;
        }
        public static List<resource> getUserAllResource(string username, role currentrole, string owner = "")//获取指定用户的特定类型和属主的资源列表
        {
            List<resource> rr = new List<resource>();
            IroleResourceEx _roleResourceEx = IocModule.GetEntity<IroleResourceEx>();
            if (currentrole != null)//
            {
                rr.AddRange(_roleResourceEx.getRoleAllResource(currentrole.id, owner));
            }
            else
            {
                List<role> rolelist = IdalCommon.IroleEx.getUserRoles(username);
                if (rolelist.Count > 0)
                {
                    foreach (var e in rolelist)
                    {
                        rr.AddRange(_roleResourceEx.getRoleAllResource(e.id, owner));
                    }
                    
                }
            }
            
            rr = rr.Where((x, i) => rr.FindIndex(z => z.id == x.id) == i).ToList();//Lambda表达式去重  
            return rr;
        }
        /////////////////////////////////////////////////////////////////////////新增的工作流相关////////////////////////////////////////////  
        /// <summary>
        /// 获得指定用户工作流关联控制器动作列表，返回的是action列表
        /// </summary>
        /// <param name="username"></param>
        /// <param name="wfname"></param>
        /// <returns></returns>
        //public static List<action> getUserWfCtrlActions(string username, string wfname = "")//获得指定用户工作流关联控制器动作列表
        //{
        //    if (wfname == "")
        //        return workflownodeoperatorEx.getUserActionsLinkWf(username);
        //    else
        //        return workflownodeoperatorEx.getUserActionsLinkWf(username, wfname);
        //}
        ///// <summary>
        ///// 获得指定用户工作流工作动作列表，返回的是workflownodeaction列表
        ///// </summary>
        ///// <param name="username"></param>
        ///// <param name="wfname"></param>
        ///// <returns></returns>
        //public static List<workflownodeaction> getWfActions(string username,  string wfname = "")//获得指定用户工作流工作动作列表
        //{
        //    if (wfname == "")
        //        return workflownodeoperatorEx.getUserWorkflowActions(username);
        //    else
        //        return workflownodeoperatorEx.getUserWorkflowActions(username,wfname);
        //}
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}
