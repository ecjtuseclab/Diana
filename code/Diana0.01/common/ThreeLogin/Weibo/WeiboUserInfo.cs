using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
///<summary>
///项目名称：Diana轻量级开发框架
///版本：0.0.1版
///开发团队成员：胡凯雨，张梦丽，艾美珍，易传佳，陈祚松，康文洋，张婷婷，王露，周庆，夏萍萍，陈兰兰
///模块和代码页功能描述：微博第三方登录 用户类
///最后修改时间：2018/1/22
/// </summary>
namespace common.ThreeLogin
{
    public class WeiboUserInfo:ThreeLoginUserInfo
    {
        //        {
        //    "id": 1404376560,
        //    "screen_name": "zaku",
        //    "name": "zaku",
        //    "province": "11",
        //    "city": "5",
        //    "location": "北京 朝阳区",
        //    "description": "人生五十年，乃如梦如幻；有生斯有死，壮士复何憾。",
        //    "url": "http://blog.sina.com.cn/zaku",
        //    "profile_image_url": "http://tp1.sinaimg.cn/1404376560/50/0/1",
        //    "domain": "zaku",
        //    "gender": "m",
        //    "followers_count": 1204,
        //    "friends_count": 447,
        //    "statuses_count": 2908,
        //    "favourites_count": 0,
        //    "created_at": "Fri Aug 28 00:00:00 +0800 2009",
        //    "following": false,
        //    "allow_all_act_msg": false,
        //    "geo_enabled": true,
        //    "verified": false,
        //    "status": {
        //        "created_at": "Tue May 24 18:04:53 +0800 2011",
        //        "id": 11142488790,
        //        "text": "我的相机到了。",
        //        "source": "<a href="http://weibo.com" rel="nofollow">新浪微博</a>",
        //        "favorited": false,
        //        "truncated": false,
        //        "in_reply_to_status_id": "",
        //        "in_reply_to_user_id": "",
        //        "in_reply_to_screen_name": "",
        //        "geo": null,
        //        "mid": "5610221544300749636",
        //        "annotations": [],
        //        "reposts_count": 5,
        //        "comments_count": 8
        //    },
        //    "allow_all_comment": true,
        //    "avatar_large": "http://tp1.sinaimg.cn/1404376560/180/0/1",
        //    "verified_reason": "",
        //    "follow_me": false,
        //    "online_status": 0,
        //    "bi_followers_count": 215
        //}

        public Int64 id { get; set; }                 //用户UID  64为  GUID
        //public string idstr { get; set; }             //字符串型的用户UID
        public string idstr                           
        {
            get { return idstr; }
            set { userid = value; }
        }      
        
        //public string screen_name { get; set; }       //用户昵称
        public string screen_name
        {
            get { return screen_name; }
            set { username = value; }
        }      

        public string name { get; set; }              //友好显示名称
        public int province { get; set; }             //用户所在省级ID
        public int city { get; set; }                 //int	用户所在城市ID
        public string location { get; set; }          //用户所在地
        public string description { get; set; }       //用户个人描述
        public string url { get; set; }               //用户博客地址
        public string profile_url { get; set; }       //用户的微博统一URL地址
        public string profile_image_url               //用户头像地址（中图），50×50像素
        {
            get { return profile_image_url; }
            set { portrait = value; }
        }      
        

        public string domain { get; set; }            //用户的个性化域名
        public string weihao { get; set; }            //用户的微号

        private string gender_;
        public string gender                           //性别，m：男、f：女、n：未知
        {
            get
            {
                switch (gender_)
               {
                   case "m": return "男";
                   case "f": return "女"; 
                   case "n": return "未知"; 
                   default: return "未知";
               }
            }
            set { gender_ = value; }
        }      
        public int followers_count { get; set; }      //粉丝数
        public int friends_count { get; set; }        //关注数
        public int statuses_count { get; set; }       //微博数
        public int favourites_count { get; set; }     //收藏数
        public string created_at { get; set; }        //用户创建（注册）时间
        public bool allow_all_act_msg { get; set; }   //是否允许所有人给我发私信，true：是，false：否
        public bool geo_enabled { get; set; }         //是否允许标识用户的地理位置，true：是，false：否
        public bool verified { get; set; }            //是否是微博认证用户，即加V用户，true：是，false：否
        public string remark { get; set; }            //用户备注信息，只有在查询用户关系时才返回此字段
        public string avatar_large { get; set; }      //用户头像地址（大图），180×180像素
        public string avatar_hd { get; set; }         //用户头像地址（高清），高清头像原图
        public string verified_reason { get; set; }   //认证原因
        public string follow_me { get; set; }         //该用户是否关注当前登录用户，true：是，false：否
        public int online_status { get; set; }        //用户的在线状态，0：不在线、1：在线
        public int bi_followers_count { get; set; }   //用户的互粉数
        public string lang { get; set; }              //用户当前的语言版本，zh-cn：简体中文，zh-tw：繁体中文，en：英语

    }
}