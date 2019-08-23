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
///模块和代码页功能描述：资源树形父类
///最后修改时间：2017/11/25
/// </summary>
namespace Web.Control
{
    public class RescTree_ParentId
    {
        public class resource_parentId
        {
            public int id { get; set; }
            public string resourcename { get; set; }
            public int? resourcetype { get; set; }
            public string resourceurl { get; set; }
            public string resourceBootstrapscript { get; set; }
            public string resourceAcescript { get; set; }
            public string resourceowner { get; set; }
            public int? resourceleval { get; set; }
            public string resourceleftico { get; set; }
            public string resourcerightico { get; set; }
            public string resourceclass { get; set; }
            public int? resourcenumber { get; set; }
            public int? order { get; set; }
            public string description { get; set; }

            public int? _parentId { get; set; }

            public resource_parentId(resource r)
            {
                this.id = r.id;
                this.resourcename = r.resourcename;
                this.resourcetype = r.resourcetype;
                this.resourceurl = r.resourceurl;
                this.resourceBootstrapscript = r.resourceBootstrapscript;
                this.resourceAcescript = r.resourceAcescript;
                this.resourceowner = r.resourceowner;
                this.resourceleval = r.resourceleval;
                this.resourceleftico = r.resourceleftico;
                this.resourcerightico = r.resourcerightico;
                this.resourceclass = r.resourceclass;
                this.resourcenumber = r.resourcenumber;

                this.order = r.order;
                this.description = r.description;

                if (r.resourceowner != null)
                    this._parentId = int.Parse(r.resourceowner);
            }
        }
    }
}