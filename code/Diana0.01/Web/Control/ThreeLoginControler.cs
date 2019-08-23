using basedal;
using common.ThreeLogin;
using Diana.bll;
using Diana.Idal;
using Diana.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
/// <summary>
///项目名称：Diana轻量级开发框架
///版本：0.0.1版
///开发团队成员：胡凯雨，张梦丽，艾美珍，易传佳，陈祚松，康文洋，张婷婷，王露，周庆，夏萍萍，陈兰兰
///模块和代码页功能描述：第三方登录控制器
///最后修改时间：2018/1/20
/// </summary>
namespace Web.Control
{
    public class ThreeLoginControler:baseControler
    {

        public ThreeLoginControler()
        {
            noauth_actions.Add("view");
        }
      
       
        private LoginApiHelp api = null;

        private threeloginInfo threeloginInfo
        {
            get;
            set;
        }

        /// <summary>
        /// 无动作名时执行默认方法
        /// </summary>
        public override void DefaultAction()//默认动作
        {
            string state = ctx.Request["state"];
            string pageUrl = string.Empty;
            var data = ctx.Request;//获取回调响应参数
            var code = data["code"];// 认证返回的code信息;
            if (!string.IsNullOrEmpty(code))
            {
                api = new LoginApiHelp(state);//存储用户信息
                threeloginInfo = api.DealCode(code);    //据code向百度服务器进行交互获取长期的Access Token   并调用api获取数据
                accctx = AccountsPrincipal.ValidateLogin(threeloginInfo.useName,threeloginInfo.uid);
                ctx.Session["accctx"] = accctx;
                if (string.IsNullOrEmpty(accctx.currentrole.rolename))
                {
                    accctx.currentrole = IocModule.GetEntity<IroleEx>().getAllRole().OrderByDescending(c => c.rolecode).FirstOrDefault();
                }
                ctx.Response.Redirect("/index.ashx?ctrl=index&action=view", false);
            }
        }


     


    }
}