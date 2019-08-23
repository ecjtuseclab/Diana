using System;
using System.Linq;
using System.Text;
/// <summary>
///项目名称：Diana轻量级开发框架
///版本：0.0.1版
///开发团队成员：胡凯雨，张梦丽，艾美珍，易传佳，陈祚送，康文洋，张婷婷，王露，周庆，夏萍萍，陈兰兰
///模块和代码页功能描述：文章实体类
///最后修改时间：2018/1/26
/// </summary>
namespace Diana.model
{
    public class article
    {
        public int id { get; set; }

        public string title { get; set; }

        public string content { get; set; }

        public DateTime inserttime { get; set; }

        public DateTime edittime { get; set; }

        public int? type { get; set; }

    }
}
