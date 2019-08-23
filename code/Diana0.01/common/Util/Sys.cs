using System;
using System.Diagnostics;
using System.IO;
using System.Web;
///<summary>
///项目名称：Diana轻量级开发框架
///版本：0.0.1版
///开发团队成员：胡凯雨，张梦丽，艾美珍，易传佳，陈祚松，康文洋，张婷婷，王露，周庆，夏萍萍，陈兰兰
///模块和代码页功能描述：系统操作类
///最后修改时间：2018/1/28
/// </summary>
namespace Diana.common.Util
{
    /// <summary>
    /// 系统操作
    /// </summary>
    public class Sys
    {
        #region Line(换行符)

        /// <summary>
        /// 换行符
        /// </summary>
        public static string Line
        {
            get
            {
                return Environment.NewLine;
            }
        }

        #endregion

        #region Guid

        /// <summary>
        /// Guid
        /// </summary>
        public static Guid Guid
        {
            get { return Guid.NewGuid(); }
        }

        #endregion

        #region GetType(获取类型)

        /// <summary>
        /// 获取类型,对可空类型进行处理
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        public static Type GetType<T>()
        {
            return Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T);
        }

        #endregion

        #region GetPhysicalPath(获取物理路径)

        /// <summary>
        /// 获取物理路径
        /// </summary>
        /// <param name="relativePath">相对路径</param>
        public static string GetPhysicalPath(string relativePath)
        {
            if (string.IsNullOrWhiteSpace(relativePath))
                return string.Empty;
            if (HttpContext.Current == null)
            {
                if (relativePath.StartsWith("~"))
                    relativePath = relativePath.Remove(0, 2);
                return Path.GetFullPath(relativePath);
            }
            if (relativePath.StartsWith("~"))
                return HttpContext.Current.Server.MapPath(relativePath);
            if (relativePath.StartsWith("/") || relativePath.StartsWith("\\"))
                return HttpContext.Current.Server.MapPath("~" + relativePath);
            return HttpContext.Current.Server.MapPath("~/" + relativePath);
        }

        #endregion

        #region StartProcess(启动进程)

        /// <summary>
        /// 启动进程
        /// </summary>
        /// <param name="processName">进程名称</param>
        public static void StartProcess(string processName)
        {
            Process.Start(processName);
        }

        #endregion
    }
}
