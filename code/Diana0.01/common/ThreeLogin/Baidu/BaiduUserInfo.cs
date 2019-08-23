using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
/// <summary>
///项目名称：Diana轻量级开发框架
///版本：0.0.1版
///开发团队成员：胡凯雨，张梦丽，艾美珍，易传佳，陈祚松，康文洋，张婷婷，王露，周庆，夏萍萍，陈兰兰
///模块和代码页功能描述：百度第三方登录 用户信息
///最后修改时间：2018/1/22
/// </summary>
namespace common.ThreeLogin
{

    //    {
    //    "userid":"2097322476",
    //    "username":"wl19871011",
    //    "realname":"阳光",
    //    "userdetail":"喜欢自由",
    //    "birthday":"1987-01-01",
    //    "marriage":"恋爱",
    //    "sex":"男",
    //    "blood":"O",
    //    "constellation":"射手",
    //    "figure":"小巧",
    //    "education":"大学/专科",
    //    "trade":"计算机/电子产品",
    //    "job":"未知",
    //    "birthday_year":"1987",
    //    "birthday_month":"01",
    //    "birthday_day":"01",
    //}

    public class BaiduUserInfo : ThreeLoginUserInfo
    {
        /// <summary>
        /// 当前登录用户的数字ID
        /// </summary>
        //public string userid { get; set; }

        /// <summary>
        /// 当前登录用户的用户名，值可能为空。
        /// </summary>
        //public string username { get; set; }
        /// <summary>
        /// 	用户真实姓名，可能为空。
        /// </summary>
        public string realname { get; set; }

       
        //private string portrait_;
        /// <summary>
        /// 当前登录用户的头像
        /// </summary>
        public string _portrait
        {
            get { return "http://tb.himg.baidu.com/sys/portraitn/item/" + portrait; }
            set { portrait = value; }
        }
        /// <summary>
        /// 自我简介，可能为空。
        /// </summary>
        public string userdetail { get; set; }
        /// <summary>
        /// 生日，以yyyy-mm-dd格式显示。
        /// </summary>
        public string birthday { get; set; }
        /// <summary>
        /// 婚姻状况
        /// </summary>
        public string marriage { get; set; }

        private string sex_;
        /// <summary>
        /// 性别。"1"表示男，"0"表示女。
        /// </summary>
        public string sex
        {
            get
            {
                if (sex_ == "1")
                {
                    return "男";
                }
                else
                {
                    return "女";        
                }
            }
            set { sex_ = value; }
        }

        /// <summary>
        /// 血型
        /// </summary>
        public string blood { get; set; }
        /// <summary>
        /// 星座
        /// </summary>
        public string constellation { get; set; }
        /// <summary>
        /// 体型
        /// </summary>
        public string figure { get; set; }

        /// <summary>
        /// 学历
        /// </summary>
        public string education { get; set; }
        /// <summary>
        /// 当前职业
        /// </summary>
        public string trade { get; set; }
        /// <summary>
        /// 职位
        /// </summary>
        public string job { get; set; }
        public string birthday_year { get; set; }
        public string birthday_month { get; set; }
        public string birthday_day { get; set; }

    }
}