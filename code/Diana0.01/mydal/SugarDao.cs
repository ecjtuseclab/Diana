using System;
using System.Collections.Generic;
using System.Linq;
using MySqlSugar;
using TanMiniToolSet.Common;
/// <summary>
///项目名称：Diana轻量级开发框架
///版本：0.0.1版
///开发团队成员：胡凯雨，张梦丽，艾美珍，易传佳，陈祚松，康文洋，张婷婷，王露，周庆，夏萍萍，陈兰兰
///模块和代码页功能描述：实例化数据库连接对象
///最后修改时间：2018/1/26
/// </summary>
namespace Diana.mydal
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
        public static string ConnectionString = ConfigHelper.GetConfigString("mysqlConnectionString");

        public static SqlSugarClient GetInstance()
        {
            var db = new SqlSugarClient(ConnectionString);
            //db.IsEnableLogEvent = true;//启用日志事件，能够查看实际SQL是如何操作的
            //db.LogEventStarting = (sql, par) => { Console.WriteLine(sql + " " + par+"\r\n"); };
            return db;
        }
        
    }
}