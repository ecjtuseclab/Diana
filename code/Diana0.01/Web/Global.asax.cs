using Diana.common.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Diana.dal;

namespace Web
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

            copyrightprotectionEx.verifysign();
            //从配置文件读取log4net的配置，然后进行一个初始化工作。
            log4net.Config.XmlConfigurator.Configure();
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            if (Server.GetLastError() == null) return;
            Exception ex = Server.GetLastError().GetBaseException();
            string error = this.DealException(ex);
            LogHelper.LogWriter(error);
            if (ex.InnerException != null)
            {
                error = this.DealException(ex);
                LogHelper.LogWriter(error);
            }
            this.Server.ClearError();
        }

        /// <summary>
        /// 处理异常，用来将主要异常信息写入文本日志
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        private string DealException(Exception ex)
        {
            string error = string.Format("URl：{0}\n引发异常的方法：{1}\n错误信息：{2}\n错误堆栈：{3}\n",
                this.Request.RawUrl, ex.TargetSite, ex.Message, ex.StackTrace);
            return error;
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}