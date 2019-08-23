using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
///项目名称：Diana轻量级开发框架
///版本：0.0.1版
///开发团队成员：胡凯雨，张梦丽，艾美珍，易传佳，陈祚送，康文洋，张婷婷，王露，周庆，夏萍萍，陈兰兰
///模块和代码页功能描述：第三方登录实体类
///最后修改时间：2018/1/26
/// </summary>
namespace Diana.model
{
   public class threeloginInfo
    {
        public string accessToken { get; set; }
        public int area { get; set; }
        public string code { get; set; }
        public string expiresIn { get; set; }
        public int id { get; set; }
        public string idCode { get; set; }
        public string location { get; set; }
        public int sex { get; set; }
        public int type { get; set; }
        public string typeName { get; set; }
        public string uid { get; set; }
        public string useImg { get; set; }
        public string useName { get; set; }
        public int userId { get; set; }
        public string refreshToken { get; set; }

    }
}
