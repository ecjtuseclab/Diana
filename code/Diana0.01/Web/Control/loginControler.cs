using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using Diana.common.Util;
using Diana.bll;
using Web.Login;
using System.Collections;
using Diana.common.Util.Web;
/// <summary>
///项目名称：Diana轻量级开发框架
///版本：0.0.1版
///开发团队成员：胡凯雨，张梦丽，艾美珍，易传佳，陈祚松，康文洋，张婷婷，王露，周庆，夏萍萍，陈兰兰
///模块和代码页功能描述：登录控制器
///最后修改时间：2018/1/20
/// </summary>
namespace Web.Control
{
    public class loginControler : baseControler
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public loginControler()
        {
            noauth_actions.Add("GetAuthCode");
            noauth_actions.Add("login");
            noauth_actions.Add("logout");
            noauth_actions.Add("showAll");
            noauth_actions.Add("view");
        }
        /// <summary>
        /// 显示页面的方法
        /// </summary>
        public override void view()
        {

            // tp.Put("checkcode", "12345");//这里只是给个范例，需要随机生成，并存取来
            tp.Display(ctx, "../login.html");
        }

        public void GetAuthCode()
        {
            VerifyCode verifycode = new VerifyCode();
            ctx.Response.ClearContent();
            ctx.Response.ContentType = "image/Png";
            ctx.Response.BinaryWrite(verifycode.GetVerifyCode());
            ctx.Response.Flush();
            ctx.ApplicationInstance.CompleteRequest();//替换下面代码

        }

        /// <summary>
        /// 实现多种登录模式:1.Session 2.Cookie 3.Cache 在WebConfig中配置
        /// </summary>
        public void login()
        {
            try
            {
                string msg = "";
                ArrayList arrays = new ArrayList();
                //检查记录登录次数
                if ((ctx.Session["PassErrorCount"] != null) && (ctx.Session["PassErrorCount"].ToString() != ""))
                {
                    int PassErroeCount = Convert.ToInt32(ctx.Session["PassErrorCount"]);
                    if (PassErroeCount > 3)
                    {
                        msg = "登录尝试错误次数超过3次";
                        arrays.Add(msg);
                        arrays.Add(false);
                    }
                }
                //检查验证码
                else if (Md5.md5(ctx.Request["checkcode"].ToString().ToLower(), 16).ToString()!=HttpContext.Current.Session["diana_session_verifycode"].ToString())
                {
                    msg = "所填写的验证码与所给的不符 !";
                    arrays.Add(msg);
                    arrays.Add(false);
                }
                else
                {
                    LoginFactory login = new LoginFactory(ctx, accctx);
                    arrays = login.GetLoginType().login();
                }

                WriteBackHtml((string)arrays[0], (bool)arrays[1]);
            }
            catch (Exception ex)
            {
                WriteBackHtml(ex.Message.ToString(), false);
            }

        }
        /// <summary>
        /// 注销登录
        /// </summary>
        public void logout()
        {
            ctx.Session["accctx"] = null;//清掉session中对象，世界就清净了！
            string msg = "退出成功，页面跳转中!";//告知前端，重新刷新页面，重新变成登录模式
            WriteBackHtml(msg, true);//在页面上可以以红字提醒，JS直接direct跳转到下一个链接
        }
    }
}