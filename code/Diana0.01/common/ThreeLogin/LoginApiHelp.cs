using basedal;
using Diana.Idal;
using Diana.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
///<summary>
///项目名称：Diana轻量级开发框架
///版本：0.0.1版
///开发团队成员：胡凯雨，张梦丽，艾美珍，易传佳，陈祚松，康文洋，张婷婷，王露，周庆，夏萍萍，陈兰兰
///模块和代码页功能描述：第三方登录处理类
///最后修改时间：2018/1/22
/// </summary>
namespace common.ThreeLogin
{
    public class LoginApiHelp
    {
        
        //private ThreeLoginFactory _ThreeLoginFactory;
        //public ThreeLoginFactory ThreeLoginFactory
        //{
        //    get
        //    {
        //        if (_ThreeLoginFactory == null)
        //        {
        //            _ThreeLoginFactory = new ThreeLoginFactory();
        //        }
        //        return _ThreeLoginFactory;
        //    }
        //}

        //private string threeRegisterUrl_ { get; set; }
        private string Type { get; set; }
        private string portraiturl { get; set; }

        public LoginApiHelp()
        {

        }

        public LoginApiHelp(string Type)
        {
            this.Type = Type;
        }



        public BlakTypeEnum GetThreeLoginType()
        {
            switch (Type.ToUpper())
            {
                case "BAIDU": portraiturl = "http://tb.himg.baidu.com/sys/portraitn/item/"; return BlakTypeEnum.BAIDU;
                case "WEIBO": portraiturl = ""; return BlakTypeEnum.WEIBO;
                case "QQ": return BlakTypeEnum.QQ;
                case "WEIXIN": return BlakTypeEnum.WEIXIN;
                default: return BlakTypeEnum.BAIDU;
            }
        }

        public threeloginInfo DealCode(string code) 
        {
            threeloginInfo tl = null;
            ThreeLoginFactory three = new ThreeLoginFactory(GetThreeLoginType());
            var token = three.GetLoginHelp().AccessToken(code);
            if (token != null)
            {
                var userInfo = three.GetLoginHelp().UserInfo(token.access_token, token.uid);//调用API  获取用户数据
                if (userInfo != null)
                {
                    var item = IocModule.GetEntity<IthreeloginInfoEx>().getthreeloginInfobyuid(userInfo.userid);
                    tl = new threeloginInfo();
                    tl.accessToken = token.access_token;
                    tl.code = code;
                    tl.uid = userInfo.userid;
                    tl.useImg =portraiturl+ userInfo.portrait;
                    tl.type = (int)BlakTypeEnum.BAIDU;
                    tl.typeName = BlakTypeEnum.BAIDU.ToString();
                    //tl.sex = Convert.ToInt32(userInfo.sex);
                    tl.useName = userInfo.username;
                    //tl.refreshToken = token.refresh_token;

                    if (item == null)
                    {
                        #region 第一次第三方登录
                        //存储数据库
                        IocModule.GetEntity<IthreeloginInfoEx>().insert(tl);
                        IocModule.GetEntity<IthreeloginInfoEx>().InsertthreeloginInfo(tl);
                        #endregion
                    }
                    else
                    {
                        tl.id = item.id;
                        IocModule.GetEntity<IthreeloginInfoEx>().update(tl);
                    }
                }
            }
            return tl;
        }

    }
}