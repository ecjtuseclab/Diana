using Diana.bll;
using Diana.common.Cache.Factory;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
/// <summary>
///项目名称：Diana轻量级开发框架
///版本：0.0.1版
///开发团队成员：胡凯雨，张梦丽，艾美珍，易传佳，陈祚松，康文洋，张婷婷，王露，周庆，夏萍萍，陈兰兰
///模块和代码页功能描述：缓存登录
///最后修改时间：2018/1/28
///修改日志：
///2018/1/25 用memcache+cookie或者redis+cookie代替Session登录（胡凯雨）
/// </summary>
namespace Web.Login
{
    public class CacheLogin : Login, ILogin
    {

        public CacheLogin(HttpContext ctx, AccountsPrincipal accctx)
            : base(ctx, accctx)
        {
           
        }


        public ArrayList login()
        {
            string msg = "";

            //对于这些东西，实际项目一定要做防sql注入处理，尽量使用ORM方法操作，不要直接SQL
            string userName = ctx.Request["username"];
            string Password = ctx.Request["password"];

            //验证登录信息，如果验证通过则返回当前用户对象的安全上下文信息，并记录失败次数
            accctx = AccountsPrincipal.ValidateLogin(userName, Password);
            HttpCookie ErrorCookie = new HttpCookie("PassErrorCount");
            ErrorCookie.Expires = DateTime.Now.AddDays(1);

            HttpCookie userLoginIdCookie = new HttpCookie("userLoginId");

            if (accctx == null)//登录信息不对
            {
                msg = "登陆失败： " + userName;
                if (HttpContext.Current.Request.Cookies["PassErrorCount"] != null && (HttpContext.Current.Request.Cookies["PassErrorCount"].Value != ""))
                {
                    int PassErroeCount = Convert.ToInt32(HttpContext.Current.Request.Cookies["PassErrorCount"].Value);
                    ErrorCookie.Value = Convert.ToString(PassErroeCount + 1);
                    HttpContext.Current.Response.Cookies.Add(ErrorCookie);
                }
                else
                {
                    ErrorCookie.Value = "1";
                    HttpContext.Current.Response.Cookies.Add(ErrorCookie);
                }
                Arraylists.Add(msg);
                Arraylists.Add(false);
            }
            else
            {
                //用memcache+cookie或者redis+cookie代替之。
                //立即分配一个标志，Guid。把标志作为 mm存储数据的key，把用户对象放到 mm。 把guid写到客户端cookie里面去。
                string userLoginId = Guid.NewGuid().ToString();
                //把用户的数据写到mm
                CacheFactory.Cache().WriteCache<AccountsPrincipal>(accctx, userLoginId, DateTime.Now.AddMinutes(20));

                //往客户端写入cookie
                userLoginIdCookie.Value = userLoginId;
                ctx.User = accctx;
                HttpContext.Current.Response.Cookies.Add(userLoginIdCookie);

                //ctx.Session["accctx"] = accctx;
                msg = "登录成功，页面跳转中!";
                Arraylists.Add(msg);
                Arraylists.Add(true);
                if (ErrorCookie != null)
                {
                    ErrorCookie.Expires = DateTime.Now.AddDays(-1);
                }
            }
            return Arraylists;
        }
    }
}