using System;
using System.Collections.Generic;
using System.Linq;
using SqlSugar;
using TanMiniToolSet.Common;
using System.Configuration;
/// <summary>
///项目名称：Diana轻量级开发框架
///版本：0.0.1版
///开发团队成员：胡凯雨，张梦丽，艾美珍，易传佳，陈祚松，康文洋，张婷婷，王露，周庆，夏萍萍，陈兰兰
///模块和代码页功能描述：实例化数据库连接对象
///最后修改时间：2018/1/26
/// </summary>
namespace Diana.dal
{
    /// <summary>
    /// SqlSugar
    /// </summary>
    public class DB
    {
        //禁止实例化，一个数据库对象的单例模式, 静态实例方便直接引用，没必要反复去建对象
        private DB()
        {

        }
        public static string ConnectionString 
        {
            get 
            {
                string con = ConfigHelper.GetConfigString("ConnectionString"); 
                return con; 
            }
        }

        private static  SqlSugarClient _db;
             
        public static SqlSugarClient GetInstance()
        {
            if (_db == null)
            {
                _db= new SqlSugarClient(ConnectionString);
            }
            return _db;
        }
    }
}