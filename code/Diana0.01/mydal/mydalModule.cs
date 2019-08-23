using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using Ninject.Modules;
using Diana.Idal;
/// <summary>
///项目名称：Diana轻量级开发框架
///版本：0.0.1版
///开发团队成员：胡凯雨，张梦丽，艾美珍，易传佳，陈祚松，康文洋，张婷婷，王露，周庆，夏萍萍，陈兰兰
///模块和代码页功能描述：IOC针对MySQLServer的所有对象接口
///最后修改时间：2018/1/26
///日志说明：
/// </summary>
namespace Diana.mydal
{
  public  class mydalModule : NinjectModule
    {
        public override void Load()
        {
           Bind<IactionEx>().To<actionEx>();
           Bind<IareaEx>().To<areaEx>();
           Bind<IgroupEx>().To<groupEx>();
           //Bind<IpropertyMappingEx>().To<propertyMappingEx>();
           //Bind<IresourceEx>().To<resourceEx>();
           Bind<IroleActionEx>().To<roleActionEx>();
           Bind<IroleEx>().To<roleEx>();
           Bind<IroleResourceEx>().To<roleResourceEx>();
           Bind<IuserEx>().To<userEx>();
           Bind<IuserGroupEx>().To<userGroupEx>();
           Bind<IuserRoleEx>().To<userRoleEx>();
           Bind<IworkflowEx>().To<workflowEx>();
           Bind<IworkflowHelper>().To<workflowHelper>();
           Bind<IworkflowinstancesEx>().To<workflowinstancesEx>();
           Bind<IworkflowinstancetracingsEx>().To<workflowinstancetracingsEx>();
           Bind<IworkflowManager>().To<workflowManager>();
           Bind<IworkflownodeactionEx>().To<workflownodeactionEx>();
           Bind<IworkflownodeEx>().To<workflownodeEx>();
           Bind<IworkflownodeoperatorEx>().To<workflownodeoperatorEx>();
           Bind<IcommonEx>().To<commonEx>();
           Bind<IbackupEx>().To<backupEx>();
           Bind<IarticleEx >().To<articleEx>();
        }
    }
}
