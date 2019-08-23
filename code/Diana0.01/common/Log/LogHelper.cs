using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
/// <summary>
///项目名称：Diana轻量级开发框架
///版本：0.0.1版
///开发团队成员：胡凯雨，张梦丽，艾美珍，易传佳，陈祚松，康文洋，张婷婷，王露，周庆，夏萍萍，陈兰兰
///模块和代码页功能描述：日志写入帮助类
///最后修改时间：2018/1/28
///修改日志：
///2018/1/28 采用观察者模式对日志操作进行处理(胡凯雨)
/// </summary>
namespace Diana.common.Log
{
    public class LogHelper
    {
        public static Queue<string> Exceptions = new Queue<string>();
        public static List<ILogWriter> LogWriters = new List<ILogWriter>();


        static LogHelper()
        {
            LogWriters.Add(new Log4NetWriter());

            ThreadPool.QueueUserWorkItem((o) =>
            {

                while (true)
                {
                    if (Exceptions.Count > 0)
                    {
                        string str = Exceptions.Dequeue();
                        foreach (var LogWriter in LogWriters)
                        {
                            LogWriter.WriteLogInfo(str);
                        }
                    }
                }

            });
        }


        public static void LogWriter(string Exceptiontxt)
        {
            lock (Exceptions)
            {
                Exceptions.Enqueue(Exceptiontxt);
            }
        }

    }

}
