using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using SqlSugar;
using System.Reflection;
using Diana.bll;
using Diana.common.Util;
using Diana.common;
using Ninject.Modules;
using Ninject;
using Diana.dal;
using Diana.mydal;
using Web.Model;
using System.Configuration;
using Diana.common.Cache.Factory;
using Diana.model;
using basedal;
using Diana.Idal;
/// <summary>
///项目名称：Diana轻量级开发框架
///版本：0.0.1版
///开发团队成员：胡凯雨，张梦丽，艾美珍，易传佳，陈祚松，康文洋，张婷婷，王露，周庆，夏萍萍，陈兰兰
///模块和代码页功能描述：基础控制器
///最后修改时间：2018/1/20
///修改日志：
///2018/1/25  getbuttonresource()方法,InterfaceConfig界面切换（胡凯雨）
/// </summary>
namespace Web.Control
{
    public class baseControler : IControler, IHttpHandler
    {
        private static string interfaceConfig;
        private static string InterfaceConfig
        {
            get
            {
                if (interfaceConfig == null)
                {
                    interfaceConfig = ConfigurationManager.AppSettings["InterfaceSwitch"];
                    if (string.IsNullOrEmpty(interfaceConfig))
                    {
                        interfaceConfig = "BootstrapTemplate";
                    }
                }
                return interfaceConfig;
            }
        }

        protected VelocityHelper tp = new VelocityHelper(@"~/Templates/" + InterfaceConfig);//模板静态对象

        protected HttpContext ctx = null; //http上下文对象
        protected AccountsPrincipal accctx = null;//当前用户上下文对象
        public ActionResult result = new ActionResult();//返回结果集
        private string errormsg = "";//错误提示信息
        private string noauthmsg = "";//非授权提示信息
        protected List<string> noauth_actions = new List<string>();//不需要验证的方法列表,默认情况下方法都是要验证的，不需要验证的方法这里添加
        public virtual void ProcessRequest(HttpContext context) //只是预留方便接口统一可以直接处理结果，一般不用
        {

        }

        public void  getbuttonresource(string controlername, int type)
        {
            if (ctx.Session["accctx"] != null && accctx.currentrole.rolename == "超级管理员")//如果是超级管理员，则不需要判断权限直接调用
            {

                List<resource> btns=IdalCommon.IresourceEx.getResourcesbycontrolername(controlername, 3);
                tp.Put("btns", btns);
            }
            else
            {
                var item = IocModule.GetEntity<IresourceEx>().getResourcebycontrolername(controlername, 2);
                if (item != null)
                {
                    if (accctx.Buttons != null)
                    {
                        List<resource> btns = accctx.Buttons.FindAll(t => t.resourceowner == Convert.ToString(item.id));
                        tp.Put("btns", btns);
                    }
                }
            }
          
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        public baseControler()
        {
            noauth_actions.Add("DefaultAction");
            noauth_actions.Add("login");
            noauth_actions.Add("logout");
            noauth_actions.Add("showAll");
            noauth_actions.Add("view");
        }
        /// <summary>
        /// 视图显示
        /// </summary>
        public virtual void view()
        {
            //一般这里就是什么都没有，这里只是写得好玩，给大家直观认识
            ctx.Response.ContentType = "text/plain";
            ctx.Response.Write("人生就是一个方程-来自baseControler");
        }
        /// <summary>
        /// 无动作名时执行默认方法
        /// </summary>
        public virtual void DefaultAction()//默认动作
        {
            view();
        }
        /// <summary>
        /// //出错发生动作
        /// </summary>
        protected virtual void ErrorAction()
        {
            WriteBackHtml(errormsg, false);
        }
        /// <summary>
        ///非授权动作
        /// </summary>
        protected virtual void NoAuthAction()
        {
            WriteBackHtml(noauthmsg, false);
        }
        /// <summary>
        /// 控制器真正调用的入口方法，就一个函数而已
        /// </summary>
        /// <param name="context">上下文对象</param>
        /// <returns></returns>
        public virtual ActionResult ActionPerform(HttpContext context)
        {
            try
            {
                if (context != null)
                    ctx = context;                                         //获取http内容上下文
                if (ctx.Session["accctx"] != null)
                    accctx = (AccountsPrincipal)ctx.Session["accctx"];    //获取当前用户信息上下文
                else if (HttpContext.Current.Request.Cookies["userLoginId"] != null)
                {
                    string userGuid = HttpContext.Current.Request.Cookies["userLoginId"].Value;
                    AccountsPrincipal accuser = CacheFactory.Cache().GetCache<AccountsPrincipal>(userGuid);
                    if (accuser != null)
                    {
                        accctx = accuser;
                        //滑动窗口机制。
                        CacheFactory.Cache().RemoveCache(userGuid);
                        CacheFactory.Cache().WriteCache<AccountsPrincipal>(accuser, userGuid, DateTime.Now.AddMinutes(20));
                    }
                }
                Type t = this.GetType();
                string actionname = context.Request["action"];           //获取动作信息
                if (actionname == null) actionname = "DefaultAction";//默认调用这个方法，默认处理
                MethodInfo me = t.GetMethod(actionname);//查询控制器中是否有对应的action,应用动作必须是;
                if (me != null) //如果有，则检查权限，通过反射动态调用对应的action.
                {
                    // 加上工作流d的方法的权限判断
                    string wfids = ctx.Request["wfids"];
                    string wfname = ctx.Request["wfname"];
                    if ((wfname != string.Empty) && (wfids != string.Empty) && (wfname != null) && (wfids != null))
                    {
                        //  wfActionPerform(wfids, wfname, actionname, me);
                    }
                    else if (ctx.Session["accctx"] != null && accctx.currentrole.rolename == "超级管理员")//如果是超级管理员，则不需要判断权限直接调用
                    {
                        me.Invoke(this, null);
                    }
                    else if ((noauth_actions.Contains(actionname)) || (accctx.HasActionPermission(actionname)))//如果不需要验证或者验证有权限，则允许调用,实现简单的AOP前置方法包裹
                        me.Invoke(this, null);
                    else
                    {
                        noauthmsg = string.Format("当前用户没有{0}这个动作的授权", actionname);
                        NoAuthAction();
                    }
                }
                else
                {
                    errormsg = string.Format("没有{0}这个动作方法", actionname); //没有这个动作则返回出错信息
                    ErrorAction();
                }
            }
            catch (Exception ex)
            {
                WriteBackHtml(ex.Message.ToString(), false);
            }
            return result;
        }
      
        /// <summary>
        /// 向前端UI回写返回信息
        /// </summary>
        /// <param name="context">上下文对象</param>
        /// <param name="msg">错误信息</param>
        /// <param name="flag">true或者fals</param>
        protected static void WriteBackHtml(HttpContext context, string msg, bool flag)
        {   //Status
           // { OK: 100, Failed: 101, NotLogin: 102, Unauthorized: 103 };
            string state = "success";
            string success = "true";
            if (!flag)
            {
                state = "error";
                success = "false";
            }
            Dictionary<string, string> result = new Dictionary<string, string>();
            result.Add("state", state);
            result.Add("msg", msg);
            result.Add("message", msg);
            result.Add("success", success);
           // result.Add("Status", msg);
            string strJson = Json.ToJson(result);
            writeJsonBack(context, strJson);

        }
        protected virtual void WriteBackHtml(string msg, bool flag)
        {
            WriteBackHtml(ctx, msg, flag);
        }
        /// <summary>
        /// 向前端UI回写返回信息
        /// </summary>
        /// <param name="context">http上下文</param>
        /// <param name="strJson">json语句</param>
        protected static void writeJsonBack(HttpContext context, string strJson)
        {
            context.Response.Clear();
            context.Response.ContentEncoding = Encoding.UTF8;
            context.Response.ContentType = "application/json";
            context.Response.Write(strJson);
            context.Response.Flush();
            context.ApplicationInstance.CompleteRequest();//替换下面代码
        }
        protected virtual void writeJsonBack(string strJson)
        {
            writeJsonBack(ctx, strJson);
        }
        /// <summary>
        /// 向前端UI回写返回信息,按照内容类型排列的 Mime 类型列表中的写法来进行
        ///参考格式：http://www.cnblogs.com/huanhuan86/archive/2012/06/12/2546362.html
        /// </summary>
        /// <param name="context">http上下文</param>
        /// <param name="strHtml">html语句</param>
        protected static void writeHtmlBack(HttpContext context, string strHtml)
        {
            context.Response.Clear();
            context.Response.ContentEncoding = Encoding.UTF8;
            context.Response.ContentType = "text/html";
            context.Response.Write(strHtml);
            context.Response.Flush();
            context.ApplicationInstance.CompleteRequest();//替换下面代码
        }
        protected virtual void writeHtmlBack(string strHtml)
        {
            writeHtmlBack(ctx, strHtml);
        }

        protected virtual void Success(string message)
        {
            writeJsonBack(new AjaxResult { state = ResultType.success.ToString(), message = message }.ToJson());
        }
        protected virtual void Success(string message, object data)
        {
            writeJsonBack(new AjaxResult { state = ResultType.success.ToString(), message = message, data = data }.ToJson());
        }
        protected virtual void Error(string message)
        {
            writeJsonBack(new AjaxResult { state = ResultType.error.ToString(), message = message }.ToJson());
        }
        /// <summary>
        /// 将前台返回的formdata数据转换为对象
        /// </summary>
        /// <returns>T</returns>
        public static T GetTableByFormData<T>(string formdata)
        {
            //{"materialname":"他","materialtype":"3","inventorycount":"3"}//反序列化
            formdata = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(formdata);

            T table = Json.ToObject<T>(formdata);

            return table;
        }

        /// <summary>
        /// 两个对象的数据进行比较
        /// </summary>
        /// <returns>返回一个最新(oldTable)的对象数据</returns>
        public static T GetNewTableValue<T>(T oldTable, T newTable) where T : class,new()
        {
            Object oldValue;
            Object newValue;

            PropertyInfo[] mPi = typeof(T).GetProperties();

            for (int i = 0; i < mPi.Length; i++)
            {
                PropertyInfo pi = mPi[i];

                oldValue = pi.GetValue(oldTable, null);
                newValue = pi.GetValue(newTable, null);

                if (!oldValue.Equals(newValue))
                {
                    if (newValue != null)
                    {
                        pi.SetValue(oldTable, newValue);
                    }
                }
            }
            return oldTable;
        }

        /// <summary>
        /// 两个对象的数据进行比较
        /// </summary>
        /// <returns>返回一个最新(oldTable)的对象数据</returns>
        public static T GetNewTableValue<T>(T oldTable, string formdata) where T : class,new()
        {
            T tempT = GetTableByFormData<T>(formdata);
            oldTable = GetNewTableValue<T>(oldTable, tempT);
            return oldTable;
        }

        /// <summary>
        /// 将json数据反序列化为Dictionary
        /// </summary>
        /// <param name="jsonData">json数据</param>
        /// <returns>Dictionary</returns>
        public static Dictionary<string, object> JsonToDictionary(string jsonData)
        {
            //实例化JavaScriptSerializer类的新实例
            System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            try
            {
                //将指定的 JSON 字符串转换为 Dictionary<string, object> 类型的对象
                return jss.Deserialize<Dictionary<string, object>>(jsonData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}