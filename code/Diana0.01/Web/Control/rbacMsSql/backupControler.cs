using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Diana.Idal;
using Diana.model;
using Diana.common.Util;
using System.Data;
using Diana.common;
using Ninject.Modules;
using Ninject;
using basedal;
using Diana.model.Model;
using System.Threading.Tasks;
using System.IO;
/// <summary>
///项目名称：Diana轻量级开发框架
///版本：0.0.1版
///开发团队成员：胡凯雨，张梦丽，艾美珍，易传佳，陈祚松，康文洋，张婷婷，王露，周庆，夏萍萍，陈兰兰
///模块和代码页功能描述：备份控制器
///更新时间：2018/1/25
/// </summary>
namespace Web.Control
{
    public class backupControler : baseControler
    {
        /// <summary>
        /// 根据的添加方法名，不需要再验证方法是否有权限访问
        /// </summary>
        public backupControler()//不需要验证的方法在构造方法添加
        {
            noauth_actions.Add("download");
        }

        /// <summary>
        /// 显示视图
        /// </summary>
        public override void view() //默认视图
        {
            tp.Put("buttonlist", accctx.Buttons);
            tp.Display(ctx, "/Html/backup.html");
        }

       

    }
}