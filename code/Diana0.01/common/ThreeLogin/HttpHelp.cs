using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
///<summary>
///项目名称：Diana轻量级开发框架
///版本：0.0.1版
///开发团队成员：胡凯雨，张梦丽，艾美珍，易传佳，陈祚松，康文洋，张婷婷，王露，周庆，夏萍萍，陈兰兰
///模块和代码页功能描述：第三方登录 帮助类
///最后修改时间：2018/1/22
/// </summary>
namespace common.ThreeLogin
{
    public static class HttpHelp
    {

        #region HTTPGet获取数据
        /// <summary>  
        /// GET请求与获取结果  
        /// </summary>  
        public static string HttpGet(string Url)
        {
           
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
                request.Method = "GET";
                request.ContentType = "text/plain;charset=UTF-8";
                //request.Headers.Add("X-Access-Token", "qJjEFmjHQ0ygBS2eyBcyiDAy97oUeADm");
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
                string retString = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                myResponseStream.Close();
                return retString;
           
        }
        #endregion
        #region HTTPPost获取数据
        /// <summary>  
        /// POST请求与获取结果  
        /// </summary>  
        public static string HttpPost(string Url, string postDataStr)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            //request.ContentLength = postDataStr.Length;
            StreamWriter writer = new StreamWriter(request.GetRequestStream(), Encoding.GetEncoding("gb2312"));
            //StreamWriter writer = new StreamWriter(request.GetRequestStream(), Encoding.ASCII);
            writer.Write(postDataStr);
            writer.Flush();
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string encoding = response.ContentEncoding;
            if (encoding == null || encoding.Length < 1)
            {
                encoding = "UTF-8"; //默认编码  
            }
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(encoding));
            string retString = reader.ReadToEnd();
            return retString;
        }
        #endregion
        #region 获取的accessToken
        //public JObject Login()
        //{
        //    url = "https://open.ys7.com/api/lapp/token/get";
        //    data = string.Format("appKey={0}&appSecret={1}", "9179e427e02341839098695328744134", "c734dcc411a607fda0d84f7a958f7635");
        //    string result = HttpPost(url, data);
        //    JObject resultObj = (JObject)JsonConvert.DeserializeObject(result);
        //    accessToken = resultObj["data"]["accessToken"].ToString();
        //    return resultObj;
        //}
        #endregion

        /// <summary>
        /// post 方法，url当get写
        /// </summary>
        /// <param name="Url"></param>
        /// <returns></returns>
        ///灵感来于http://www.cnblogs.com/yzhzh/archive/2013/03/23/2977812.html
        /// 
        public static string HttpPost(string Url)
        {
            string result = string.Empty;
            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(Url);//code为上一步得到的code
            httpRequest.Method = "POST";
            httpRequest.Headers["Pragma"] = "no-cache";
            httpRequest.Headers["Cache-Control"] = "no-cache";
            httpRequest.ContentType = "Content-Type:application/x-www-form-urlencoded; charset=UTF-8";
            HttpWebResponse httpResponse = null;
            try
            {
                httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                using (var reader = new System.IO.StreamReader(httpResponse.GetResponseStream(), System.Text.UTF8Encoding.UTF8))
                {
                    string xmStream = reader.ReadToEnd();
                    reader.Close();
                    //放回json
                    //if (xmStream.Contains("access_token"))
                    //{
                    //    int start = xmStream.IndexOf("token") + 8;
                    //    int end = xmStream.IndexOf("remind") - 3;
                    //    access_token = xmStream.Substring(start, end - start);

                    //}
                    //else
                    //{

                    //}
                    result = xmStream;
                }
            }
            catch (Exception)
            {

                // MessageBox.Show("(~.~)失败了");
            }
            finally
            {
                if (httpRequest != null) httpRequest.Abort();
                if (httpResponse != null) httpResponse.Close();
            }
            return result;
        }
    }
}