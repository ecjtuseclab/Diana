using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Diana.bll;
using Diana.Idal;
using basedal;
using Diana.model;
/// <summary>
///项目名称：Diana轻量级开发框架
///版本：0.0.1版
///开发团队成员：胡凯雨，张梦丽，艾美珍，易传佳，陈祚松，康文洋，张婷婷，王露，周庆，夏萍萍，陈兰兰
///模块和代码页功能描述：登录控制器
///最后修改时间：2018/1/20
///修改日志：
///2018/1/25 login()方法（胡凯雨）
///2018/1/25 selectrole方法中加载了acctx的所有值（张梦丽）

/// </summary>
namespace Web.Control
{
    public class selectroleControler : baseControler
    {
        public selectroleControler()
        {
            noauth_actions.Add("selectrole");
        }

        public override void view()
        {
            List<role> roles=new List<role>();
            List<user_role> user_roles = IocModule.GetEntity<IuserRoleEx>().getRole(accctx.currentuser.id);
            foreach (var item in user_roles)
	        {
		       role r= IocModule.GetEntity<IroleEx>().getRole(item.roleid);
               roles.Add(r);
	        }
            tp.Put("username", accctx.currentuser.username);
            tp.Put("rolelist", roles);
            tp.Display(ctx, "../selectrole.html");
        }
        public void selectrole()
        {
            string msg = "";
            if (ctx.Request["roleid"] != null)
            {
                string roleid = ctx.Request["roleid"];
                accctx.currentrole = IocModule.GetEntity<IroleEx>().getRole(int.Parse(roleid));
                accctx.SetList(accctx.currentuser.username);//加载当前用户当前角色的所有权限资源
                msg = "角色选择成功，页面跳转中!";
                WriteBackHtml(msg, true);//在页面上可以以红字提醒，JS直接direct跳转到下一个链接
            }
            else
                WriteBackHtml(msg, false);
        }
    }
}