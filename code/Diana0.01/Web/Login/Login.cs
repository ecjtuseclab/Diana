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
///模块和代码页功能描述：登录基类
///最后修改时间：2017/11/25
/// </summary>
namespace Web.Login
{
    public class Login
    {
        protected HttpContext ctx;
        protected AccountsPrincipal accctx;
        protected ArrayList Arraylists;

        public Login(HttpContext ctx, AccountsPrincipal accctx)
        {
            this.ctx = ctx;
            this.accctx = accctx;
            this.Arraylists = new ArrayList();
        }

        public virtual ArrayList login()
        {
            throw new NotImplementedException();
        }


    }
}