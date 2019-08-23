using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
///<summary>
///项目名称：Diana轻量级开发框架
///版本：0.0.1版
///开发团队成员：胡凯雨，张梦丽，艾美珍，易传佳，陈祚松，康文洋，张婷婷，王露，周庆，夏萍萍，陈兰兰
///模块和代码页功能描述：树表格实体类
///最后修改时间：2018/1/22
/// </summary>
namespace Diana.common.Util.WebControl
{
    /// <summary>
    /// 树表格实体
    /// </summary>
    public class TreeGridEntity
    {
        public string parentId { get; set; }
        public string id { get; set; }
        public string text { get; set; }
        public string entityJson { get; set; }
        public bool expanded { get; set; }
        public bool hasChildren { get; set; }
    }
}
