using Diana.Idal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
///项目名称：Diana轻量级开发框架
///版本：0.0.1版
///开发团队成员：胡凯雨，张梦丽，艾美珍，易传佳，陈祚松，康文洋，张婷婷，王露，周庆，夏萍萍，陈兰兰
///模块和代码页功能描述：所有接口的调用，配合IOC，调用不同数据库（sql,和mysql）的dal
///最后修改时间：2018/1/20
/// </summary>
namespace basedal
{
   public class IdalCommon
    {
        /// <summary>
        /// 公共接口
        /// </summary>
       private  static IcommonEx _IcommonEx;
        public  static IcommonEx IcommonEx
        {
            get
            {
                if (_IcommonEx == null)
                {
                    _IcommonEx = IocModule.GetEntity<IcommonEx>();
                }
                return _IcommonEx;
            }
        }
        /// <summary>
        /// IactionEx接口
        /// </summary>
        private static IactionEx _IactionEx;
        public static IactionEx IactionEx
        {
            get
            {
                if (_IactionEx == null)
                {
                    _IactionEx = IocModule.GetEntity<IactionEx>();
                }
                return _IactionEx;
            }
        }

       /// <summary>
       /// 区域接口
       /// </summary>
        private static IareaEx _IareaEx;
        public static IareaEx IareaEx
        {
            get
            {
                if (_IareaEx == null)
                {
                    _IareaEx = IocModule.GetEntity<IareaEx>();
                }
                return _IareaEx;
            }
        }

        /// <summary>
        /// 用户分组接口
        /// </summary>
        private static IuserGroupEx _IuserGroupEx;
        public static IuserGroupEx IuserGroupEx
        {
            get
            {
                if (_IuserGroupEx == null)
                {
                    _IuserGroupEx = IocModule.GetEntity<IuserGroupEx>();
                }
                return _IuserGroupEx;
            }
        }

       /// <summary>
       /// 备份接口
       /// </summary>
        private static IbackupEx _IbackupEx;
        public static IbackupEx IbackupEx
        {
            get
            {
                if (_IbackupEx == null)
                {
                    _IbackupEx = IocModule.GetEntity<IbackupEx>();
                }
                return _IbackupEx;
            }
        }
       /// <summary>
       /// 分组接口
       /// </summary>
        private static IgroupEx _IgroupEx;
        public static IgroupEx IgroupEx
        {
            get
            {
                if (_IgroupEx == null)
                {
                    _IgroupEx = IocModule.GetEntity<IgroupEx>();
                }
                return _IgroupEx;
            }
        }
       /// <summary>
       /// 字典接口
       /// </summary>
        private static IpropertyMappingEx _IpropertyMappingEx;
        public static IpropertyMappingEx IpropertyMappingEx
        {
            get
            {
                if (_IpropertyMappingEx == null)
                {
                    _IpropertyMappingEx = IocModule.GetEntity<IpropertyMappingEx>();
                }
                return _IpropertyMappingEx;
            }
        }
       /// <summary>
       /// 资源接口
       /// </summary>
        private static IresourceEx _IresourceEx;
        public static IresourceEx IresourceEx
        {
            get
            {
                if (_IresourceEx == null)
                {
                    _IresourceEx = IocModule.GetEntity<IresourceEx>();
                }
                return _IresourceEx;
            }
        }
       /// <summary>
       /// 角色接口
       /// </summary>
        private static IroleEx _IroleEx;
        public static IroleEx IroleEx
        {
            get
            {
                if (_IroleEx == null)
                {
                    _IroleEx = IocModule.GetEntity<IroleEx>();
                }
                return _IroleEx;
            }
        }
       /// <summary>
       /// 角色动作接口
       /// </summary>
        private static IroleActionEx _IroleActionEx;
        public static IroleActionEx IroleActionEx
        {
            get
            {
                if (_IroleActionEx == null)
                {
                    _IroleActionEx = IocModule.GetEntity<IroleActionEx>();
                }
                return _IroleActionEx;
            }
        }
       /// <summary>
       /// 角色资源接口
       /// </summary>
        private static IroleResourceEx _IroleResourceEx;
        public static IroleResourceEx IroleResourceEx
        {
            get
            {
                if (_IroleResourceEx == null)
                {
                    _IroleResourceEx = IocModule.GetEntity<IroleResourceEx>();
                }
                return _IroleResourceEx;
            }
        }

        /// <summary>
        /// 用户数据处理接口
        /// </summary>
        private static IuserEx _IuserEx;
        public static IuserEx IuserEx
        {
            get
            {
                if (_IuserEx == null)
                {
                    _IuserEx = IocModule.GetEntity<IuserEx>();
                }
                return _IuserEx;
            }
        }
        /// <summary>
        /// 工作流表接口
        /// </summary>
        private static IworkflowEx _iworkflowEx;
        public static IworkflowEx IworkflowEx
        {
            get
            {
                if (_iworkflowEx == null)
                {
                    _iworkflowEx = IocModule.GetEntity<IworkflowEx>();
                }
                return _iworkflowEx;
            }
        }
       /// <summary>
       /// 工作流记录接口
       /// </summary>
        private static IworkflowinstancesEx _IworkflowInstanceEx;
        public static IworkflowinstancesEx IworkflowinstancesEx
        {
            get
            {
                if (_IworkflowInstanceEx == null)
                {
                    _IworkflowInstanceEx = IocModule.GetEntity<IworkflowinstancesEx>();
                }
                return _IworkflowInstanceEx;
            }
        }
        /// <summary>
        /// 工作流过程记录接口
        /// </summary>
        private static IworkflowinstancetracingsEx _IworkflowinstancetracingsEx;
        public static IworkflowinstancetracingsEx IworkflowinstancetracingsEx
        {
            get
            {
                if (_IworkflowinstancetracingsEx == null)
                {
                    _IworkflowinstancetracingsEx = IocModule.GetEntity<IworkflowinstancetracingsEx>();
                }
                return _IworkflowinstancetracingsEx;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private static IworkflowManager _IworkflowManagerEx;
        public static IworkflowManager IworkflowManagerEx
        {
            get
            {
                if (_IworkflowManagerEx == null)
                {
                    _IworkflowManagerEx = IocModule.GetEntity<IworkflowManager>();
                }
                return _IworkflowManagerEx;
            }
        }
        /// <summary>
        /// 工作流动作节点接口
        /// </summary>
        private static IworkflownodeactionEx _IworkflownodeactionEx;
        public static IworkflownodeactionEx IworkflownodeactionEx
        {
            get
            {
                if (_IworkflownodeactionEx == null)
                {
                    _IworkflownodeactionEx = IocModule.GetEntity<IworkflownodeactionEx>();
                }
                return _IworkflownodeactionEx;
            }
        }
        /// <summary>
        /// 工作流节点接口
        /// </summary>
        private static IworkflownodeEx _IworkflownodeEx;
        public static IworkflownodeEx IworkflownodeEx
        {
            get
            {
                if (_IworkflownodeEx == null)
                {
                    _IworkflownodeEx = IocModule.GetEntity<IworkflownodeEx>();
                }
                return _IworkflownodeEx;
            }
        }
        /// <summary>
        /// 工作流操作者接口
        /// </summary>
        private static IworkflownodeoperatorEx _IworkflownodeoperatorEx;
        public static IworkflownodeoperatorEx IworkflownodeoperatorEx
        {
            get
            {
                if (_IworkflownodeoperatorEx == null)
                {
                    _IworkflownodeoperatorEx = IocModule.GetEntity<IworkflownodeoperatorEx>();
                }
                return _IworkflownodeoperatorEx;
            }
        }

        /// <summary>
        /// 订单接口
        /// </summary>
        private static IorderEx _IorderEx;
        public static IorderEx IorderEx
        {
            get
            {
                if (_IorderEx == null)
                {
                    _IorderEx = IocModule.GetEntity<IorderEx>();
                }
                return _IorderEx;
            }
        }
        /// <summary>
        /// IarticleEx接口
        /// </summary>
        private static IarticleEx _IarticleEx;
        public static IarticleEx IarticleEx
        {
            get
            {
                if (_IarticleEx == null)
                {
                    _IarticleEx = IocModule.GetEntity<IarticleEx>();
                }
                return _IarticleEx;
            }
        }
    }
}
