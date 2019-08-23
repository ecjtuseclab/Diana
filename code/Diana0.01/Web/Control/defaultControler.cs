using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Diana.dal;
using Diana.model;
using Diana.common.Util;
/// <summary>
///项目名称：Diana轻量级开发框架
///版本：0.0.1版
///开发团队成员：胡凯雨，张梦丽，艾美珍，易传佳，陈祚送，康文洋，张婷婷，王露，周庆，夏萍萍，陈兰兰
///模块和代码页功能描述：默认控制器
///更新时间：2018/1/25
/// </summary>
namespace Web.Control
{
    public class defaultControler : baseControler
    {
        public override void view() //默认视图
        {
            tp.Display(ctx, "../default.html");
        }
    }
}