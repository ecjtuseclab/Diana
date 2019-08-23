using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Diana.model;
using Diana.common.Util;
using Diana.common.Util.WebControl;
using Diana.Idal;
using basedal;
/// <summary>
///项目名称：Diana轻量级开发框架
///版本：0.0.1版
///开发团队成员：胡凯雨，张梦丽，艾美珍，易传佳，陈祚松，康文洋，张婷婷，王露，周庆，夏萍萍，陈兰兰
///模块和代码页功能描述：index控制器
///最后修改时间：2018/1/25
/// </summary>
namespace Web.Control
{
    public class indexControler : baseControler
    {
        private IroleResourceEx _iroleResourceEx;
        public IroleResourceEx IroleResourceEx
        {
            get
            {
                if (_iroleResourceEx == null)
                {
                    _iroleResourceEx = IocModule.GetEntity<IroleResourceEx>();
                }
                return _iroleResourceEx;
            }
        }
        private IuserEx _iuserEx;
        public IuserEx iuserEx
        {
            get
            {
                if (_iuserEx == null)
                {
                    _iuserEx = IocModule.GetEntity<IuserEx>();
                }
                return _iuserEx;
            }
        }
        public override void view() //默认视图
        {
            //if (accctx.Menus != null)
            //{
            //    tp.Put("firstmenulist", accctx.Menus.Where(p => p.resourceleval == 1));
            //    tp.Put("secondmenulist", accctx.Menus.Where(p => p.resourceleval == 2));
            //}
            //头像
            tp.Put("photo", accctx.currentuser.photo);
            tp.Put("username", accctx.currentuser.username);//当前用户
            tp.Put("rolename", accctx.currentrole.rolename);//当前角色
            tp.Display(ctx, "index.html");
        }
    }
}