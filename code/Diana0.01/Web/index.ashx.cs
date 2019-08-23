using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Web.Control;
using TanMiniToolSet.Common;
using System.Web.SessionState;
using System.Configuration;
using Diana.common.Cache.Factory;
using Diana.bll;
/// <summary>
///项目名称：Diana轻量级开发框架
///版本：0.0.1版
///开发团队成员：胡凯雨，张梦丽，艾美珍，易传佳，陈祚松，康文洋，张婷婷，王露，周庆，夏萍萍，陈兰兰
///模块和代码页功能描述：单入口模式访问页
///最后修改时间：2018/1/25
///修改日志：
///2018/1/25 加入了缓存验证登录cachecookie()
/// </summary>
namespace Web
{
    //三种基本方法提供验证和存储，1.用cookie 2.用session 3.用微软提供的方式，比如票据ticket,安全上下文对象
    //用cookie一定得加密，否则不安全，我们在用户表自带了ECC公钥和私钥,可以用签名的方法做出很多灵活的授权方案
    public class index : IHttpHandler, IRequiresSessionState
    {
       

        public void ProcessRequest(HttpContext context)
        {

            CallControler(context);//调用控制器处理请求
        }

        private static bool cachecookie()
        {
            try
            {
                //使用mm+cookie代替session
                //校验用户是否已经登录
                //从缓存中拿到当前的登录的用户信息。
                if (HttpContext.Current.Request.Cookies["userLoginId"] == null)
                {
                    return false;
                }

                string userGuid = HttpContext.Current.Request.Cookies["userLoginId"].Value;
                AccountsPrincipal accuser = CacheFactory.Cache().GetCache<AccountsPrincipal>(userGuid);
                if (accuser == null)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }



        /// <summary>
        /// 控制器处理请求
        /// </summary>
        /// <param name="context">上下文对象</param>
        private static void CallControler(HttpContext context) //控制器处理请求函数，利用反射来做，复杂性大为降低
        {
            string Controler_name = "";
            if (context.Session["accctx"] == null && !cachecookie())   
                if (context.Request["state"] != null)   //session对象 && mm缓存中对象木有， state有值(第三方登录标识) 表示没登录  进入第三方登录
                    Controler_name = "ThreeLoginControler";
                else
                    Controler_name = "loginControler";   //session对象 && mm缓存中对象木有，表示没登录，加载登录控制器
            else if (context.Request["ctrl"] == null && cachecookie())  //mm缓存中对象有值 且没有控制器  则加载  index控制器
                Controler_name = "indexControler";
            else if (context.Request["ctrl"] != null) //此参数表示控制器名
                Controler_name = context.Request["ctrl"] + "Controler";//得到控制器名
            else
                Controler_name = "defaultControler"; //如果不指定则使用默认控制器，可以放在web.config中。
            string nspace = "Web.Control"; //控制器命名空间，可以放在web.config中。
            Assembly res = null; //命名空间程序集对象
            IControler Controler = null;//用于接收控制器

            res = getNamespace();//获得程序集对象
            if (res != null)
            {
                Controler = (IControler)res.CreateInstance(nspace + "." + Controler_name); //动态创建控制器实例
                if (Controler != null)
                {
                    Controler.ActionPerform(context);
                }
            }
        }
        /// <summary>
        /// 获得程序集对象方法1：//聪明办法，只能获取当前自己写的命名空间程序集对象，所以Controler也需要用这个命名空间,加载的不行
        /// </summary>
        /// <returns>程序集对象</returns>
        private static Assembly getNamespace()
        {
            Assembly ns = System.Reflection.Assembly.GetExecutingAssembly();
            return ns;
        }
        /// <summary>
        /// 获得程序集对象2：//笨办法，穷举所有的加载类库，获得程序集对象，但灵活，也可以动态从dll加载
        /// </summary>
        /// <returns>程序集对象</returns>
        private static Assembly getNamespace(string nspace, Assembly res)
        {
            //此处因为要进行两次for循环，虽然也比较快，但可以使用缓存更快
            try
            {
                res = (Assembly)DataCache.GetCache("nss");

                if (null != res) //如果有对象缓存，直接用，不过这里需要置于try catch块中
                {
                    return res;
                }
                else
                {
                    Assembly[] nss = AppDomain.CurrentDomain.GetAssemblies();
                    foreach (var e in nss)
                    {
                        foreach (Type type in e.GetTypes())
                        {
                            if (type.Namespace == nspace)
                            {
                                res = type.Assembly;
                                break;
                            }

                        }
                    }
                    if (null != res) DataCache.SetCache("nss", res);//如果找到，则植入当前对象到缓存，免得再次穷举
                    return res;
                }
            }
            catch (Exception)
            {
                return res;
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