using Microsoft.AspNet.SignalR.Client;
using System.Threading;
///<summary>
///项目名称：Diana轻量级开发框架
///版本：0.0.1版
///开发团队成员：胡凯雨，张梦丽，艾美珍，易传佳，陈祚松，康文洋，张婷婷，王露，周庆，夏萍萍，陈兰兰
///模块和代码页功能描述：SendHubs类
///最后修改时间：2018/1/22
/// </summary>
namespace Diana.common.Util.SignalR
{
    /// <summary>
    /// 发送信息给hubs
    /// </summary
    public static class SendHubs
    {
        /// <summary>
        /// 调用hub方法
        /// </summary>
        /// <param name="methodName"></param>
        public static void callMethod(string methodName, params object[] args)
        {
            var hubConnection = new HubConnection(Config.GetValue("SignalRUrl"));
            IHubProxy ChatsHub = hubConnection.CreateHubProxy("ChatsHub");
            bool done = false;
            hubConnection.Start().ContinueWith(task =>
            {
                if (!task.IsFaulted)
                //连接成功调用服务端方法
                {
                    ChatsHub.Invoke(methodName, args);
                    done = true;
                }
                else
                    done = true;
            });
            while (!done)
            {
                Thread.Sleep(100);
            }
            //结束连接
            hubConnection.Stop();
        }
    }
}
