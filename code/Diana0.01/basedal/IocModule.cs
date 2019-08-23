using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Modules;
using Ninject;
using System.Configuration;
using System.Reflection;
/// <summary>
///项目名称：Diana轻量级开发框架
///版本：0.0.1版
///开发团队成员：胡凯雨，张梦丽，艾美珍，易传佳，陈祚松，康文洋，张婷婷，王露，周庆，夏萍萍，陈兰兰
///模块和代码页功能描述：利用IOC实现调用不同数据库（sql,和mysql）
///最后修改时间：2018/1/26
/// </summary>
namespace basedal
{
    public class IocModule
    {

        private static string[] _dalInfo;
        private  static string[] dalInfo
        {
            get
            {
                if (_dalInfo == null)
                {
                    _dalInfo = ConfigurationManager.AppSettings["Dal"].Split('_');
                }
                return _dalInfo;
            }
        }
        private static NinjectModule _module;
        private static  NinjectModule module
        {
            get
            {
                if (_module == null)
                {
                    _module = getModule();
                }
                return _module;
            }
        }
        private static NinjectModule getModule()
        {
            string path = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"bin\";
             Assembly assembly = Assembly.LoadFrom(path + dalInfo[0]);
             NinjectModule nModule = (NinjectModule)assembly.CreateInstance(dalInfo[1]);
             return nModule;
        }
        private static IKernel _kernels;
        private static IKernel kernels
        {
            get
            {
                if (_kernels == null)
                {
                    _kernels = new StandardKernel(module);
                }
                return _kernels;
            }
        }
        public static T GetEntity<T>()
        {
            return  kernels.Get<T>();
        }

        public static string dllPath
        {
            get 
            {
                return AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            }
        }
    }
}
