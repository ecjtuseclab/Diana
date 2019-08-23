using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System;
using System.Configuration;
///<summary>
///项目名称：Diana轻量级开发框架
///版本：0.0.1版
///开发团队成员：胡凯雨，张梦丽，艾美珍，易传佳，陈祚松，康文洋，张婷婷，王露，周庆，夏萍萍，陈兰兰
///模块和代码页功能描述：UnityIoc操作类
///最后修改时间：2018/1/22
/// </summary>
namespace Diana.common.Util.Ioc
{
    /// <summary>
    /// 描 述：UnityIoc
    /// </summary>
    public class UnityIocHelper : IServiceProvider
    {
        private readonly IUnityContainer _container;
        private static readonly UnityIocHelper dbinstance = new UnityIocHelper("DBcontainer");
        private UnityIocHelper(string containerName)
        {
            UnityConfigurationSection section = (UnityConfigurationSection)ConfigurationManager.GetSection("unity");
            _container = new UnityContainer();
            section.Configure(_container, containerName);
            //section.Configure(_container);
        }
        public static string GetmapToByName(string containerName, string itype, string name = "")
        {
            try
            {
                UnityConfigurationSection section = (UnityConfigurationSection)ConfigurationManager.GetSection("unity");
                var _Containers = section.Containers;
                foreach (var _Container in _Containers)
                {
                    if (_Container.Name == containerName)
                    {
                        var _Registrations = _Container.Registrations;
                        foreach (var _Registration in _Registrations)
                        {
                            if (name == "" && string.IsNullOrEmpty(_Registration.Name) && _Registration.TypeName == itype)
                            {
                                return _Registration.MapToName;
                            }
                        }
                        break;
                    }
                }
                return "";
            }
            catch
            {
                throw;
            }

        }

        public static UnityIocHelper DBInstance
        {
            get { return dbinstance; }
        }
        public object GetService(Type serviceType)
        {
            return _container.Resolve(serviceType);
        }
        public T GetService<T>()
        {
            return _container.Resolve<T>();
        }
        public T GetService<T>(params ParameterOverride[] obj)
        {
            return _container.Resolve<T>(obj);
        }
        public T GetService<T>(string name, params ParameterOverride[] obj)
        {
            return _container.Resolve<T>(name, obj);
        }
    }
}
