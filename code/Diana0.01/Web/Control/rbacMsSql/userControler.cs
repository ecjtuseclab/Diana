using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Diana.Idal;
using System.Data;
using Diana.model;
using Ninject.Modules;
using Ninject;
using basedal;
using Diana.tool.Code;
using System.IO;
/// <summary>
///项目名称：Diana轻量级开发框架
///版本：0.0.1版
///开发团队成员：胡凯雨，张梦丽，艾美珍，易传佳，陈祚松，康文洋，张婷婷，王露，周庆，夏萍萍，陈兰兰
///模块和代码页功能描述：用户控制器
///最后修改时间：2018/1/25
///修改日志：
///2018/1/25 修改view方法，获取当前控制器按钮资源(胡凯雨)
/// </summary>
namespace Web.Control
{

    public class userControler : baseControler
    {

        public userControler()
        {
            noauth_actions.Add("GetPermissionTree");
            noauth_actions.Add("GetPermissionGroupTree");
            noauth_actions.Add("RevisePassword");
            noauth_actions.Add("ResetPassword");
        }
        /// <summary>
        /// 默认视图
        /// </summary>
        public override void view()
        {
            getbuttonresource("user",1);
            tp.Display(ctx, "/Html/user.html");
        }

        public void Form()
        {
            tp.Put("username", "");
            tp.Put("userpwd", "");
            tp.Put("userpublickey", "");
            tp.Put("userprivate", "");
            tp.Display(ctx, "/Html/layer/userform_layer.html");
        }


        public void Formedit()
        {
            string keyValue = ctx.Request["keyValue"];
            user user = IdalCommon.IuserEx.getEntityById(Convert.ToInt32(keyValue));
            tp.Put("username", user.username);
            tp.Put("userpwd", user.password);
            tp.Display(ctx, "/Html/layer/userform_layer.html");
        }

        #region 1.基本操作
        /// <summary>
        /// 显示数据
        /// </summary>
        public void showAll()
        {
            try
            {
                //每页显示的数据条数
                int pageSize = Convert.ToInt32(!string.IsNullOrEmpty(ctx.Request["rows"]) ? ctx.Request["rows"] : ctx.Request["pageSize"]);
                //当前页码
                int pageIndex = Convert.ToInt32(!string.IsNullOrEmpty(ctx.Request["page"]) ? ctx.Request["page"] : ctx.Request["pageIndex"]);
                //查询关键字
                string keyword = ctx.Request["keyword"];
                //总记录数
                int records = IdalCommon.IuserEx.getUserList(keyword).Count;
                //总页数
                int totalPage = records % pageSize == 0 ? records / pageSize : records / pageSize + 1;
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("username", keyword);
                List<user> userlist = IdalCommon.IuserEx.getPaginationUserList(pageIndex, pageSize, dictionary);
                var data = new
                {
                    rows = userlist,
                    total = totalPage,
                    page = pageIndex,
                    TotalCount = records,
                    PageSize = pageSize,
                    CurrentPage = pageIndex,
                    TotalPage = totalPage,
                    DataList = userlist
                };
                writeJsonBack(data.ToJson());
            }
            catch (Exception)
            {
                WriteBackHtml("", false);
            }
        }
        
        /// <summary>
        /// 删除表单
        /// </summary>
        public void DeleteForm()
        {
            string keyValue = !string.IsNullOrEmpty(ctx.Request["keyValue"]) ? ctx.Request["keyValue"] : ctx.Request["id"];
            int id = Convert.ToInt32(keyValue);
            if (IdalCommon.IuserEx.delete(id))
            {
                Success("删除成功!");
            }
            else
            {
                Error("删除失败!");
            }
           
        }

        /// <summary>
        /// 获取表单数据
        /// </summary>
        public void GetFormJson()
        {
            string keyValue = ctx.Request["keyValue"];
            int id = Convert.ToInt32(keyValue);
            var data = IdalCommon.IuserEx.getEntityById(id);
            writeJsonBack(data.ToJson());
        }

        /// <summary>
        /// 提交表单
        /// </summary>
        public void SubmitForm()
        {
            try
            {
                string keyValue = !string.IsNullOrEmpty(ctx.Request["keyValue"]) ? ctx.Request["keyValue"] : ctx.Request["id"];
                user u = HttpContextDataWrapper.DataToObject<user>(ctx);
                //关联角色
                string rolelistIds = ctx.Request["rolelistIds"];
                //关联分组
                string grouplistIds = ctx.Request["grouplistIds"];
                IdalCommon.IuserEx.SubmitForm(u, rolelistIds, grouplistIds, keyValue);
                Success("操作成功!");
            }
            catch (Exception)
            {
                Error("操作失败!");
            }
        }

        /// <summary>
        /// 复制粘贴
        /// </summary>
        public void copyAndPaste()
        {
            try
            {
                string keyValue = ctx.Request["keyValue"];
                if (IdalCommon.IuserEx.copyAndPaste(keyValue))
                {
                    Success("复制粘贴成功!");
                }
                else
                {
                    Error("复制粘贴失败!");
                }
            }
            catch (Exception)
            {
                WriteBackHtml("", false);
            }
        }
        #endregion

        #region 2.扩展方法
        /// <summary>
        /// 根据用户id获取用户角色列表
        /// </summary>
        public void GetPermissionTree()
        {
            string userId = ctx.Request["userId"];
            var rolelist = IdalCommon.IroleEx.getEntityList();
            var authorityRole = new List<role>();
            if (!string.IsNullOrEmpty(userId))
            {
                try
                {
                    int id = Convert.ToInt32(userId);
                    user u = IdalCommon.IuserEx.getEntityById(id);
                    //authorityRole = IdalCommon.IuserRoleEx.getRole(u.username);
                    authorityRole = IocModule.GetEntity<IuserRoleEx>().getRole(u.username);
                }
                catch (Exception ex)
                {
                    string message = ex.Message;
                }
            }
            var treeList = new List<TreeViewModel>();
            foreach (role item in rolelist)
            {
                TreeViewModel tree = new TreeViewModel();
                tree.id = item.id.ToString();
                tree.text = item.rolename;
                tree.value = item.id.ToString();
                tree.parentId = "0";
                tree.isexpand = true;
                tree.complete = true;
                tree.showcheck = true;
                tree.checkstate = authorityRole.Count(t => t.id == item.id);
                tree.hasChildren = false;
                treeList.Add(tree);
            }
            string jsontree = treeList.TreeViewJson();
            writeJsonBack(jsontree);
        }

        /// <summary>
        /// 根据用户id获取用户分组列表
        /// </summary>
        public void GetPermissionGroupTree()
        {
            string userid = ctx.Request["userId"];
            var grouplist = IdalCommon.IgroupEx.getEntityList();
            var authorityGroup = new List<group>();
            if (!string.IsNullOrEmpty(userid))
            {
                int id = Convert.ToInt32(userid);
                user u = IdalCommon.IuserEx.getEntityById(id);
                authorityGroup = IdalCommon.IuserGroupEx.getGroup(u.username);
            }
            var treeList = new List<TreeViewModel>();
            foreach (group item in grouplist)
            {
                TreeViewModel tree = new TreeViewModel();
                tree.id = item.id.ToString();
                tree.text = item.groupname;
                tree.value = item.id.ToString();
                tree.parentId = "0";
                tree.isexpand = false;
                tree.complete = true;
                tree.showcheck = true;
                tree.checkstate = authorityGroup.Count(t => t.id == item.id);
                tree.hasChildren = false;
                treeList.Add(tree);
            }
            writeJsonBack(treeList.TreeViewJson());
        }
        #endregion

        /// <summary>
        /// 详情
        /// </summary>
        public void Details()
        {
            string keyValue = ctx.Request["keyValue"];
            user user = IdalCommon.IuserEx.getEntityById(Convert.ToInt32(keyValue));
            tp.Put("username", user.username);
            tp.Put("userpwd", user.password);
            if (string.IsNullOrEmpty(user.pubkey))
            {
                tp.Put("userpublickey", "");
            }
            else
            {
                tp.Put("userpublickey", user.pubkey);
            }

            if (string.IsNullOrEmpty(user.prikey))
            {
                tp.Put("userprivate", "");
            }
            else
            {
                tp.Put("userprivate", user.prikey);
            }
            tp.Display(ctx, "/Html/layer/userdetaill_layer.html");
        }


        public void RevisePassword()
        {
            tp.Display(ctx, "/Html/layer/revisepassword_layer.html");
        }

        /// <summary>
        /// 密码重置
        /// </summary>
        public void ResetPassword()
        {
            string newpassword = "123456";
            string keyValue = !string.IsNullOrEmpty(ctx.Request["keyValue"]) ? ctx.Request["keyValue"] : ctx.Request["id"];
            int id = Convert.ToInt32(keyValue);
            user u = IdalCommon.IuserEx.getEntityById(id);
            if (IdalCommon.IuserEx.updatePsw(u.username, newpassword))
            {
                Success("密码已经重置为123456!");
            }
            else
            {
                Error("密码重置失败!");
            }
        }

    }
}

