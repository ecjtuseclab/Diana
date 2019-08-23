using Diana.bll;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
/// <summary>
///项目名称：Diana轻量级开发框架
///版本：0.0.1版
///开发团队成员：胡凯雨，张梦丽，艾美珍，易传佳，陈祚松，康文洋，张婷婷，王露，周庆，夏萍萍，陈兰兰
///模块和代码页功能描述：Session登录
///最后修改时间：2017/11/25
/// </summary>
namespace Web.Login
{
    public class SessionLogin : Login, ILogin
    {
        public SessionLogin(HttpContext ctx, AccountsPrincipal accctx)
            : base(ctx, accctx)
        {
            
        }

        public override ArrayList login()
        {
            string msg = "";

            //对于这些东西，实际项目一定要做防sql注入处理，尽量使用ORM方法操作，不要直接SQL
            string userName = ctx.Request["username"];
            string Password = ctx.Request["password"];

            //验证登录信息，如果验证通过则返回当前用户对象的安全上下文信息，并记录失败次数
            accctx = AccountsPrincipal.ValidateLogin(userName, Password);

            if (accctx == null)//登录信息不对
            {
                msg = "登陆失败： " + userName;
                if ((ctx.Session["PassErrorCount"] != null) && (ctx.Session["PassErrorCount"].ToString() != ""))
                {
                    int PassErroeCount = Convert.ToInt32(ctx.Session["PassErrorCount"]);
                    ctx.Session["PassErrorCount"] = PassErroeCount + 1;
                }
                else
                {
                    ctx.Session["PassErrorCount"] = 1;
                }
                Arraylists.Add(msg);
                Arraylists.Add(false);
            }
            else
            {
                ctx.User = accctx;
                ctx.Session["accctx"] = accctx;
                msg = "登录成功，页面跳转中!";
                Arraylists.Add(msg);
                Arraylists.Add(true);
            }
            return Arraylists;
        }
    }
}