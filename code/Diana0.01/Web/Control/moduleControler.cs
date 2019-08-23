using Diana.dal;
using Diana.Idal;
using Ninject;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
/// <summary>
///项目名称：Diana轻量级开发框架
///版本：0.0.1版
///开发团队成员：胡凯雨，张梦丽，艾美珍，易传佳，陈祚松，康文洋，张婷婷，王露，周庆，夏萍萍，陈兰兰
///模块和代码页功能描述：客户端获取资源控制器
///最后修改时间：2018/1/25
/// </summary>
namespace Web.Control
{
    public class moduleControler
    {
        private static NinjectModule module = new dalModule();
        public  IKernel kernels = new StandardKernel(module);
       
    }
}