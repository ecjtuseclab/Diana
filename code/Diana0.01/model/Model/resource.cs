using System;
using System.Linq;
using System.Text;
/// <summary>
///项目名称：Diana轻量级开发框架
///版本：0.0.1版
///开发团队成员：胡凯雨，张梦丽，艾美珍，易传佳，陈祚送，康文洋，张婷婷，王露，周庆，夏萍萍，陈兰兰
///模块和代码页功能描述：资源实体类
///最后修改时间：2018/1/26
/// </summary>
namespace Diana.model
{
      [Serializable]
    public class resource
    {
        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string resourcename { get; set; }

        /// <summary>
        /// Desc:
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int resourcetype { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string resourceurl { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string resourceBootstrapscript { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string resourceAcescript { get; set; }

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string resourceowner { get; set; }

        /// <summary>
        /// Desc:1:一级菜单  2：二级菜单  3：按钮
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int? resourceleval { get; set; }
        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string resourceleftico
        {
            set;
            get;
        }
        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string resourcerightico
        {
            get;
            set;
        }
        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string resourceclass
        {
            set;
            get;
        }
        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int? resourcenumber
        {
            set;
            get;
        }
        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int order
        {
            set;
            get;
        }
        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string description
        {
            set;
            get ;
        }


        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string controlername
        {
            set;
            get;
        }


    }
}
