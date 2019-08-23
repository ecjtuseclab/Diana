using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Diana.model;
using Diana.common;
using Diana.common.Util;
using Diana.common.Util.WebControl;
using Ninject.Modules;
using Ninject;
using basedal;
using System.IO;

/// <summary>
///项目名称：Diana轻量级开发框架
///版本：0.0.1版
///开发团队成员：胡凯雨，张梦丽，艾美珍，易传佳，陈祚松，康文洋，张婷婷，王露，周庆，夏萍萍，陈兰兰
///模块和代码页功能描述：角色控制器(张梦丽)
///最后修改时间：2018/1/20
///修改日志：
///2018.1.26 修改了动作资源树 （张梦丽）
/// </summary>
namespace Web.Control
{   
    public class roleControler : baseControler
    {
        
        /// <summary>
        /// 虚函数，重写了view函数，传出页面
        /// </summary>
        public override void view() //默认视图
        {
            getbuttonresource("role", 1);
            tp.Display(ctx, "/Html/role.html");
        }
        public roleControler()
        {
            noauth_actions.Add("Details");
            noauth_actions.Add("showall");//初始化显示
            noauth_actions.Add("DeleteForm");//删除
            noauth_actions.Add("GetFormJson");//获取表单数据
            noauth_actions.Add("SubmitForm");//提交表单数据
            noauth_actions.Add("GetPermissionTree");//获取资源权限树
            noauth_actions.Add("ActionAuthorityTree");//获取动作权限树
            noauth_actions.Add("copyAndPasteForm");//复制粘贴
        }

        public void Form()
        {
            tp.Display(ctx, "/Html/layer/roleform_layer.html");
        }

        public void Details()
        {
            tp.Display(ctx, "/Html/layer/roledetail_layer.html");
        }

        #region 基本操作 
        /// <summary>
        ///显示数据
        /// </summary>
        public void showAll()
        {
            try
            {
                int pageSize = Convert.ToInt32(!string.IsNullOrEmpty(ctx.Request["rows"]) ? ctx.Request["rows"] : ctx.Request["pageSize"]);
                int pageIndex = Convert.ToInt32(!string.IsNullOrEmpty(ctx.Request["page"]) ? ctx.Request["page"] : ctx.Request["pageIndex"]);
                string keyword = ctx.Request["keyword"];
                int records = IdalCommon.IroleEx.getRoleList(keyword).Count;
                int totalPage = records % pageSize == 0 ? records / pageSize : records / pageSize + 1; //页数
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("rolename", keyword);
                List<role> rolelist = IdalCommon.IroleEx.getPaginationEntityList(pageIndex, pageSize, dictionary);
                var data = new
                {
                    rows = rolelist,
                    total = totalPage,
                    page = pageIndex,
                    TotalCount = records,
                    PageSize = pageSize,
                    CurrentPage = pageIndex,
                    TotalPage = totalPage,
                    DataList = rolelist
                };
                writeJsonBack(data.ToJson());
            }
            catch (Exception)
            {
                WriteBackHtml("", false);
            }
        }

        /// <summary>
        /// 获取表单数据
        /// </summary>
        public void GetFormJson()
        {
            string keyValue = ctx.Request["keyValue"];
            int id = Convert.ToInt32(keyValue);
            var data = IdalCommon.IroleEx.getEntityById(id);
            writeJsonBack(data.ToJson());
        }

        /// <summary>
        /// 提交表单数据
        /// </summary>
        public void SubmitForm()
        {
            try
            {
                string keyValue = !string.IsNullOrEmpty(ctx.Request["keyValue"]) ? ctx.Request["keyValue"] : ctx.Request["id"];
                role role = HttpContextDataWrapper.DataToObject<role>(ctx);
                string resourcepermissionIds = ctx.Request["resourcepermissionIds"];  //资源权限
                string actionpermissionIds = ctx.Request["actionpermissionIds"];  //动作权限
                IdalCommon.IroleEx.SubmitForm(role, resourcepermissionIds, actionpermissionIds, keyValue);
                Success("操作成功!");
            }
            catch (Exception)
            {
                 Error("操作失败!");
            }
        }

        /// <summary>
        /// 获取资源权限树
        /// </summary>
        /// <param name="roleId">被选中角色的id</param>
        /// <returns></returns>
        public void GetPermissionTree()
        {
            string roleId = ctx.Request["roleId"];
            var allresource = IdalCommon.IresourceEx.getEntityList();
            var authorizedata = new List<resource>();
            if (!string.IsNullOrEmpty(roleId))
            {
                int id = Convert.ToInt32(roleId);
                authorizedata = IdalCommon.IroleResourceEx.getRoleAllResource(id, "");
            }
            var treeList = new List<TreeEntity>();
            foreach (resource item in allresource)
            {
                TreeEntity tree = new TreeEntity();
                bool hasChildren = allresource.Count(t => t.resourceowner == item.id.ToString()) == 0 ? false : true;
                tree.id = item.id.ToString();
                tree.text = item.resourcename;
                tree.value = item.id.ToString();
                tree.parentId = item.resourceowner;
                tree.isexpand = true;
                tree.complete = true;
                tree.showcheck = true;
                tree.checkstate = authorizedata.Count(t => t.id == item.id);
                tree.hasChildren = true;
                treeList.Add(tree);
            }
            writeJsonBack(treeList.TreeToJson());
        }

        /// <summary>
        /// 动作权限树
        /// </summary>
        /// <param name="roleId">被选中角色的id</param>
        /// <returns></returns>
        public void ActionAuthorityTree()
        {
            string roleId = ctx.Request["roleId"];
            var allaction = IdalCommon.IactionEx.getEntityList();
            var authorizedata = new List<action>();
            if (!string.IsNullOrEmpty(roleId))
            {
                int id = Convert.ToInt32(roleId);
                authorizedata = IdalCommon.IroleActionEx.getRoleAllAction(id, "");

            }
            var treeList = new List<TreeEntity>();
            foreach (action item in allaction)
            {
                TreeEntity tree = new TreeEntity();
                bool hasChildren = allaction.Count(t => t.controlername == item.id.ToString()) == 0 ? false : true;
                tree.id = item.id.ToString();
                tree.text = item.actionname;
                tree.value = item.id.ToString();
                tree.parentId = item.actionowner;
                tree.isexpand = true;
                tree.complete = true;
                tree.showcheck = true;
                tree.checkstate = authorizedata.Count(t => t.id == item.id);
                tree.hasChildren = true;
                treeList.Add(tree);
            }
            writeJsonBack(treeList.TreeToJson());
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        public void DeleteForm()
        {
            string keyValue = !string.IsNullOrEmpty(ctx.Request["keyValue"]) ? ctx.Request["keyValue"] : ctx.Request["id"];
            int id = Convert.ToInt32(keyValue);
            if (IdalCommon.IroleEx.delete(id))
            {
                Success("删除成功!");
            }
            else
            {
                Error("删除失败!");
            }
        }

        /// <summary>
        /// 复制
        /// </summary>
        public void copyAndPasteForm()
        {
            string keyValue = !string.IsNullOrEmpty(ctx.Request["keyValue"]) ? ctx.Request["keyValue"] : ctx.Request["id"];
            if (IdalCommon.IroleEx.copyAndPaste(keyValue))
            {
                Success("粘贴成功!");
            }
            else
            {
                Error("粘贴失败!");
            }
        }
        #endregion

        #region 2.业务操作

        /// <summary>
        /// 获得角色所有的角色动作数据
        /// </summary>
        public void getRoleAllAction()
        {
            int roleid = int.Parse(ctx.Request["roleid"]);
            string strJson = IdalCommon.IroleActionEx.getRoleAllAction(roleid).ToJson();
            writeJsonBack(strJson);
        }

        /// <summary>
        /// 为指定角色设置角色动作信息
        /// </summary>
        public void setRoleActions()
        {
            int flagadd = 0;
            int roleid = int.Parse(ctx.Request["roleid"]);
            string[] actionids = ctx.Request["actionids"].Split(',');
            bool flagde = IdalCommon.IroleActionEx.delete(roleid);
            if (!flagde)
                WriteBackHtml("移除角色原有角色动作失败!", false);
            else
            {
                for (int i = 0; i < actionids.Count(); i++)
                {
                    int actionid = int.Parse(actionids[i]);
                    flagadd = IdalCommon.IroleActionEx.addRoleAction(actionid, roleid);
                    if (flagadd == -1)
                    {
                        WriteBackHtml("添加角色的角色动作失败!", false);
                        break;
                    }
                }
                if (flagadd != -1)
                    WriteBackHtml("添加角色的角色动作成功!", true);
            }
        }
      
        public void saveOfRoles()//保存角色的资源授权关系
        {
            string resIdLists = ctx.Request["indexs"];
            string[] strids = ctx.Request["indexs"].Split(',');//资源ID集合
            bool flag = false;
            int intflag = 0;
            string msg = string.Empty;
            try
            {
                int roleid = int.Parse(ctx.Request["roleid"]);
                if (ctx.Request["type"] == "resource")//保存资源角色关联
                {
                    List<resource> oldres =IdalCommon.IroleResourceEx.getRoleAllResource(roleid);//角色的所有资源对象
                    role_resource newr_r = new role_resource();
                    List<resource> oldbtnres = oldres.Where(p => p.resourcetype == 3).ToList();//获取角色的原来的所有btn资源对象
                    List<resource> oldmenures = oldres.Where(p => p.resourcetype == 1).ToList();//获取角色的原来的所有菜单资源对象
                    string newBtnParentID;//添加btn的上级
                    foreach (resource r in oldres)//删除-原来有现在没有权限的资源
                    {
                        if (!resIdLists.Contains(r.id.ToString()))//如果新获取的id不包含原来roles集合的id，则删除
                        {
                            IdalCommon.IroleResourceEx.delete(roleid, r.id);
                        }
                    }
                    for (int i = 0; i < strids.Length; i++)//循环选中的资源
                    {
                        if (int.Parse(strids[i]) != 0 && int.Parse(strids[i]) != 1)
                        {
                            newr_r.roleid = roleid;
                            newr_r.resoureceid = int.Parse(strids[i]);
                            intflag = IdalCommon.IroleResourceEx.insert(newr_r);//在关联表中添加btn或者menu关系
                            newBtnParentID = IdalCommon.IresourceEx.getResource(int.Parse(strids[i])).resourceowner;//1
                            if (newBtnParentID != null)
                            {
                                //if (oldmenures.Where(p => p.id == newBtnParentID).Count() == 0)//判断添加的btn的上级菜单是否在关系表中，如果没有，则添加
                                //{
                                newr_r.resoureceid = int.Parse(newBtnParentID);//btn上级的menu的ID
                                intflag = IdalCommon.IroleResourceEx.insert(newr_r);//在关联表中添加关系
                                // }
                            }
                            if (-1 == intflag) flag = true;
                            else flag = false;
                        }
                    }
                }
                else//保存按钮角色关联
                {
                    List<action> oldres = IdalCommon.IroleActionEx.getRoleAllAction(roleid);//角色的所有资源对象
                    role_action newr_r = new role_action();
                    List<action> oldbtnres = oldres.Where(p => p.actiontype == 3).ToList();//获取角色的原来的所有btn资源对象
                    List<action> oldmenures = oldres.Where(p => p.actiontype == 1).ToList();//获取角色的原来的所有菜单资源对象
                    string newBtnParentID;//添加btn的上级
                    foreach (action r in oldres)//删除-原来有现在没有权限的资源
                    {
                        if (!resIdLists.Contains(r.id.ToString()))//如果新获取的id不包含原来roles集合的id，则删除
                        {
                            IdalCommon.IroleActionEx.delete(roleid, r.id);
                        }
                    }
                    for (int i = 0; i < strids.Length; i++)//循环选中的资源
                    {
                        if (int.Parse(strids[i]) != 0)
                        {
                            newr_r.roleid = roleid;
                            newr_r.actionid = int.Parse(strids[i]);
                            intflag = IdalCommon.IroleActionEx.insert(newr_r);//在关联表中添加btn或者menu关系
                            newBtnParentID = IdalCommon.IactionEx.getAction(int.Parse(strids[i])).actionowner;//将int?转为int
                            if (newBtnParentID != null)
                            {
                                if (oldmenures.Where(p => p.id == int.Parse(newBtnParentID)).Count() == 0)//判断添加的btn的上级菜单是否在关系表中，如果没有，则添加
                                {
                                    newr_r.actionid = int.Parse(newBtnParentID);//btn上级的menu的ID
                                    intflag = IdalCommon.IroleActionEx.insert(newr_r);//在关联表中添加关系
                                }
                            }
                            if (-1 == intflag) flag = true;
                            else flag = false;
                        }

                    }
                }

                if (flag)//关联成功
                {
                    msg = "关联成功!";
                    ////获取当前用户的所有角色列表名称//这里之所以不直接用accctx.Roles的原因：可以添加为当前用户添加了新的角色
                    //List<role> currentRoles = IuserRoleEx.getRole(accctx.currentuser.username).ToList();
                    //if (currentRoles.Where(p => p.id == roleid).Count() > 0)//操作的角色如果是属于当前用户的角色，需要重新加载用户信息
                    //{
                    //    //accctx.ReloadAccounts();//重新加载当前账户信息
                    //    //ctx.User = accctx;
                    //    //ctx.Session["accctx"] = accctx;//数据重新加载
                    //}
                }

                else msg = "关联失败!";
                WriteBackHtml(msg, flag);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                WriteBackHtml(msg, false);
            }
        }

        #endregion

        
    }
}

