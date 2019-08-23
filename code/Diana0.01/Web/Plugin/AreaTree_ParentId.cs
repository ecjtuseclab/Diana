using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Diana.model;
using SqlSugar;
/// <summary>
///项目名称：Diana轻量级开发框架
///版本：0.0.1版
///开发团队成员：胡凯雨，张梦丽，艾美珍，易传佳，陈祚松，康文洋，张婷婷，王露，周庆，夏萍萍，陈兰兰
///模块和代码页功能描述：区域树形父类
///最后修改时间：2017/11/25
/// </summary>
namespace Web.Control
{
    public class AreaTree_ParentId
    {
        public class area_parentId
        {
            public int id { get; set; }
            public string fullname { get; set; }
            public int? layers { get; set; }
            public string encode { get; set; }
            public int? parentid { get; set; }

            public area_parentId(area r)
            {
                this.id = r.id;
                this.fullname = r.fullname;
                this.layers = r.layers;
                this.encode = r.encode;
                this.parentid = r.parentid;
            }
        }
    }
}