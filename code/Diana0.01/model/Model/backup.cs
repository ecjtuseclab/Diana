using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
///项目名称：Diana轻量级开发框架
///版本：0.0.1版
///开发团队成员：胡凯雨，张梦丽，艾美珍，易传佳，陈祚送，康文洋，张婷婷，王露，周庆，夏萍萍，陈兰兰
///模块和代码页功能描述：文章实体类
///最后修改时间：2018/1/26
/// </summary>
namespace Diana.model.Model
{
   public class backup
    {
        public int id
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string databasename
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string backupname
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string backupsize
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? backuptime
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string backuppersonnel
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string instructions
        {
            set;
            get;
        }

        public string absolutepath
        {
            get;
            set;
        }

        public string relativepath
        {
            get;
            set;
        }
    }
}
