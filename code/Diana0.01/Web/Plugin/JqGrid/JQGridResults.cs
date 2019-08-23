using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
/// <summary>
///项目名称：Diana轻量级开发框架
///版本：0.0.1版
///开发团队成员：胡凯雨，张梦丽，艾美珍，易传佳，陈祚松，康文洋，张婷婷，王露，周庆，夏萍萍，陈兰兰
///模块和代码页功能描述：JqGrid表格处理结果类
///最后修改时间：2017/11/25
/// </summary>
namespace Web.Plugin
{
    public class JQGridResults
    {
        public int page;
        public int total;
        public int records;
        public JQGridRow[] rows;
    }

    public struct JQGridRow
    {
        public int id;
        public string[] cell;
    }
    public class jqgridFilter
    {
        public string groupOp;
        public List<jqgridRule> rules;
        public List<jqgridFilter> groups;
    }
    public class jqgridRule
    {
        public string field;
        public string op;
        public string data;
    }
}