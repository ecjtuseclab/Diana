using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
/// <summary>
///项目名称：Diana轻量级开发框架
///版本：0.0.1版
///开发团队成员：胡凯雨，张梦丽，艾美珍，易传佳，陈祚松，康文洋，张婷婷，王露，周庆，夏萍萍，陈兰兰
///模块和代码页功能描述：百度第三方登录 AccessToken信息
///最后修改时间：2018/1/22
/// </summary>
namespace common.ThreeLogin
{

    // {
    //    "access_token": "1.a6b7dbd428f731035f771b8d15063f61.86400.1292922000-2346678-124328",
    //    "expires_in": 86400,
    //    "refresh_token": "2.385d55f8615fdfd9edb7c4b5ebdc3e39.604800.1293440400-2346678-124328",
    //    "scope": "basic email",
    //    "session_key": "ANXxSNjwQDugf8615OnqeikRMu2bKaXCdlLxn",
    //    "session_secret": "248APxvxjCZ0VEC43EYrvxqaK4oZExMB",
    //}
    //{"expires_in":2592000,"refresh_token":"22.09a42fe00ccd53ac9625b8a1cd8e20a5.315360000.1817374830.3476298022-8263730","access_token":"21.b287f82998e311ea78fd72e86dc115eb.2592000.1504606830.3476298022-8263730","session_secret":"354c52b27fc682c73290b9a603025bc3","session_key":"9mnRJqPB0+jWN0+USwWGFx\/1lZq+PgmPLcnqBfd5AxvwVauCavDsdJVhqwyqiVQOzY\/6Z4gS0W1h70S85iYmHg1IY4fr2aGBDw==","scope":"basic"}

    public class BaiduAccessToken:AccessToken
    {
        /// <summary>
        /// Access Token的有效期，以秒为单位
        /// </summary>
        public string expires_in { get; set; }

        /// <summary>
        /// 用于刷新Access Token 的 Refresh Token,所有应用都会返回该参数；（10年的有效期）
        /// </summary>
        public string refresh_token { get; set; }

        /// <summary>
        /// Access Token最终的访问范围
        /// </summary>
        public string scope { get; set; }

        /// <summary>
        /// 基于http调用Open API时所需要的Session Key，其有效期与Access Token一致；
        /// </summary>
        public string session_key { get; set; }

        /// <summary>
        /// 基于http调用Open API时计算参数签名用的签名密钥
        /// </summary>
        public string session_secret { get; set; }
    }
}