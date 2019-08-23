using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TanMiniToolSet.Common;
///<summary>
///项目名称：Diana轻量级开发框架
///版本：0.0.1版
///开发团队成员：胡凯雨，张梦丽，艾美珍，易传佳，陈祚松，康文洋，张婷婷，王露，周庆，夏萍萍，陈兰兰
///模块和代码页功能描述：第三方登录工厂类
///最后修改时间：2018/1/22
/// </summary>
namespace common.ThreeLogin
{
    public class ThreeLoginFactory
    {
        private string appKey { get; set; }
        private string appSecret { get; set; }
        private BlakTypeEnum Type { get; set; }

        public ThreeLoginFactory(BlakTypeEnum Type)
        {
            this.Type = Type;
            this.appKey = ConfigHelper.GetConfigString(Type + "_appkey"); ;
            this.appSecret = ConfigHelper.GetConfigString(Type + "_appSecret"); ;
        }

        public IThreeLoginHelp GetLoginHelp()
        {
            switch (Type)
            {
                case BlakTypeEnum.BAIDU: return new HelpBaidu(appKey, appSecret);
                case BlakTypeEnum.WEIBO: return new HelpWeibo(appKey, appSecret);
                case BlakTypeEnum.QQ: return new HelpBaidu(appKey, appSecret);       //待完善
                case BlakTypeEnum.WEIXIN: return new HelpBaidu(appKey, appSecret);  //待完善
                default: return new HelpBaidu(appKey, appSecret);
            }
        }
    }
}