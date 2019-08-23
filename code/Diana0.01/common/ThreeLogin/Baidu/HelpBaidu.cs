using Diana.common.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
/// <summary>
///项目名称：Diana轻量级开发框架
///版本：0.0.1版
///开发团队成员：胡凯雨，张梦丽，艾美珍，易传佳，陈祚松，康文洋，张婷婷，王露，周庆，夏萍萍，陈兰兰
///模块和代码页功能描述：百度第三方登录 帮助类
///最后修改时间：2018/1/22
/// </summary>
namespace common.ThreeLogin
{
    public class HelpBaidu : ThreeLoginHelp,IThreeLoginHelp
    {
        //private readonly string appKey = "ommMs96Gp06jMEdRamFzFxcT";
        //private readonly string appSecret = "DdBV6WuPDEBrgq9KyeTwPdf4EX5WNa2v";


        public HelpBaidu(string appKey,string appSecret)
            :base(appKey,appSecret)
        {
          
        }

        public  string Authorize()
        {
            url = "http://openapi.baidu.com/oauth/2.0/authorize";
            data = string.Format("client_id={0}&redirect_uri={1}&response_type=code", appKey, redirect_uri);
            return HttpHelp.HttpGet(url + "?" + data);
        }

        public  AccessToken AccessToken(string code)
        {
            url = "https://openapi.baidu.com/oauth/2.0/token";
            data = string.Format("grant_type=authorization_code&client_id={0}&client_secret={1}&code={2}&redirect_uri={3}", appKey, appSecret, code, redirect_uri);
            string result = HttpHelp.HttpPost(url + "?" + data);
            return Json.ToObject<BaiduAccessToken>(result);
        }

        /// <summary>
        /// 使用Refresh Token获取Access Token
        /// </summary>
        /// <param name="refresh_token"></param>
        /// <returns></returns>
        public BaiduAccessToken UpdateAccessToken(string refresh_token)
        {
            url = "https://openapi.baidu.com/oauth/2.0/token";
            data = string.Format("grant_type=refresh_token&client_id={0}&client_secret={1}&refresh_token={2}", appKey, appSecret, refresh_token);
            string result = HttpHelp.HttpPost(url + "?" + data);
            return Json.ToObject<BaiduAccessToken>(result);
        }

        public  ThreeLoginUserInfo UserInfo(string access_token, string uid)
        {
            url = "https://openapi.baidu.com/rest/2.0/passport/users/getInfo";
            data = string.Format("access_token={0}", access_token);
            string result = HttpHelp.HttpGet(url + "?" + data);
            return Json.ToObject<BaiduUserInfo>(result);
        }



    }
}