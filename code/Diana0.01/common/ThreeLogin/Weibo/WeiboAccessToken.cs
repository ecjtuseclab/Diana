using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
/// <summary>
///项目名称：Diana轻量级开发框架
///版本：0.0.1版
///开发团队成员：胡凯雨，张梦丽，艾美珍，易传佳，陈祚松，康文洋，张婷婷，王露，周庆，夏萍萍，陈兰兰
///模块和代码页功能描述：微博第三方登录 AccessToken类
///最后修改时间：2018/1/22
/// </summary>
namespace common.ThreeLogin
{

    //{
    //"access_token": "SlAV32hkKG",
    //"remind_in": 3600,
    //"expires_in": 3600
    //}

    //access_token	string	用户授权的唯一票据，用于调用微博的开放接口，同时也是第三方应用验证微博用户登录的唯一票据，
    //第三方应用应该用该票据和自己应用内的用户建立唯一影射关系，来识别登录状态，不能使用本返回值里的UID字段来做登录识别。
    //expires_in	string	access_token的生命周期，单位是秒数。
    //remind_in	string	access_token的生命周期（该参数即将废弃，开发者请使用expires_in）。
    //uid	string	授权用户的UID，本字段只是为了方便开发者，减少一次user/show接口调用而返回的，第三方应用不能用此字段作为用户登录状态的识别，只有access_token才是用户授权的唯一票据。
    
    public class WeiboAccessToken:AccessToken
    {
        public string remind_in { get; set; }

        public string expires_in { get; set; }

    }
}