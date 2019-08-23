using Diana.common.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
/// <summary>
///项目名称：Diana轻量级开发框架
///版本：0.0.1版
///开发团队成员：胡凯雨，张梦丽，艾美珍，易传佳，陈祚松，康文洋，张婷婷，王露，周庆，夏萍萍，陈兰兰
///模块和代码页功能描述：微博第三方登录 帮助信息类
///最后修改时间：2018/1/22
/// </summary>
namespace common.ThreeLogin
{
    public class HelpWeibo : ThreeLoginHelp,IThreeLoginHelp
    {
        //private readonly string appKey = "170252295";
        //private readonly string appSecret = "87f56d44f51a71adbb0b162d909b6b1b";

        public HelpWeibo(string appKey, string appSecret)
            :base(appKey,appSecret)
        {
          
        }

        public  string Authorize()
        {
            url = "https://api.weibo.com/oauth2/authorize";
            data = string.Format("client_id={0}&redirect_uri={1}&response_type=code", appKey, redirect_uri);
            return HttpHelp.HttpGet(url + "?" + data);
        }

        public  AccessToken AccessToken(string code)
        {
            url = " https://api.weibo.com/oauth2/access_token";
            data = string.Format("grant_type=authorization_code&client_id={0}&client_secret={1}&code={2}&redirect_uri={3}", appKey, appSecret, code, redirect_uri);
            string result = HttpHelp.HttpPost(url + "?" + data);
            return Json.ToObject<WeiboAccessToken>(result);
        }

        public  ThreeLoginUserInfo UserInfo(string access_token, string uid)
        {
            url = "https://api.weibo.com/2/users/show.json";
            data = string.Format("access_token={0}&uid={1}", access_token, uid);
            string result = HttpHelp.HttpGet(url + "?" + data);
            return Json.ToObject<WeiboUserInfo>(result);
        }

    }
}