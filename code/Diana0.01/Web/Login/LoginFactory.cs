using Diana.bll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TanMiniToolSet.Common;
/// <summary>
///项目名称：Diana轻量级开发框架
///版本：0.0.1版
///开发团队成员：胡凯雨，张梦丽，艾美珍，易传佳，陈祚松，康文洋，张婷婷，王露，周庆，夏萍萍，陈兰兰
///模块和代码页功能描述：登录工厂类
///最后修改时间：2017/11/25
/// </summary>
namespace Web.Login
{
    public class LoginFactory
    {
        public string LoginType = ConfigHelper.GetConfigString("LoginType");

        private HttpContext ctx;
        private AccountsPrincipal accctx;

        public LoginFactory(HttpContext ctx, AccountsPrincipal accctx)
        {
            this.ctx = ctx;
            this.accctx = accctx;
        }

        public ILogin GetLoginType()
        {
            switch (LoginType)
            {
                case "Session": return new SessionLogin(ctx, accctx);
                case "Cookie": return new CookieLogin(ctx, accctx);
                case "Cache": return new CacheLogin(ctx, accctx);
                default: return new SessionLogin(ctx, accctx); ;
            }
        }

    }
}