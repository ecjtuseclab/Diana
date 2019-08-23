using System;
using System.Runtime.Serialization;
///<summary>
///项目名称：Diana轻量级开发框架
///版本：0.0.1版
///开发团队成员：胡凯雨，张梦丽，艾美珍，易传佳，陈祚松，康文洋，张婷婷，王露，周庆，夏萍萍，陈兰兰
///模块和代码页功能描述：数据访问层异常类
///最后修改时间：2018/1/22
/// </summary>
namespace Diana.common.Util
{
    /// <summary>
    /// 数据访问层异常类，用于封装数据访问层引发的异常，以供 业务逻辑层 抓取
    /// </summary>
    [Serializable]
    public class DataAccessException : Exception
    {
        /// <summary>
        ///使用异常消息与一个内部异常实例化一个 类的新实例
        /// </summary>
        /// <param name="message">异常消息</param>
        /// <param name="inner">用于封装在DalException内部的异常实例</param>
        public DataAccessException(string message, Exception inner)
            : base(message, inner) { }

        /// <summary>
        ///向调用层抛出数据访问层异常
        /// </summary>
        /// <param name="msg"> 自定义异常消息 </param>
        /// <param name="e"> 实际引发异常的异常实例 </param>
        public static DataAccessException ThrowBusinessException(Exception e, string msg = "")
        {
            return new DataAccessException("业务逻辑层异常，详情请查看日志信息。", e);
        }
        /// <summary>
        ///向调用层抛出数据访问层异常
        /// </summary>
        /// <param name="msg"> 自定义异常消息 </param>
        /// <param name="e"> 实际引发异常的异常实例 </param>
        public static DataAccessException ThrowDataAccessException(Exception e, string msg = "")
        {
            if (!string.IsNullOrEmpty(msg))
            {
                return new DataAccessException(msg, e);
            }
            else
            {
                return new DataAccessException("数据访问层异常，详情请查看日志信息。", e);
            }
        }
        /// <summary>
        ///     向调用层抛出组件异常
        /// </summary>
        /// <param name="msg"> 自定义异常消息 </param>
        /// <param name="e"> 实际引发异常的异常实例 </param>
        public static DataAccessException ThrowComponentException(Exception e, string msg = "")
        {
            return new DataAccessException("组件异常，详情请查看日志信息。", e);
        }
    }

}
