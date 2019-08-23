using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Smatrix.Crypto;
/// <summary>
///项目名称：Diana轻量级开发框架
///版本：0.0.1版
///开发团队成员：胡凯雨，张梦丽，艾美珍，易传佳，陈祚松，康文洋，张婷婷，王露，周庆，夏萍萍，陈兰兰
///模块和代码页功能描述：版权保护
///最后修改时间：2018/1/26
/// </summary>
namespace Diana.dal
{
    public class copyrightprotectionEx
    {
        /// <summary>
        /// 版权保护对象
        /// </summary>
        private static string _copyright;
        public static string copyright
        {
            get
            {
                if (string.IsNullOrEmpty(_copyright))
                {
                    _copyright = ConfigurationManager.AppSettings["copyright"];
                } return _copyright;
            }
        }
        /// <summary>
        /// 版权保护签名
        /// </summary>
        private static string _copyrightsign;
        public static string copyrightsign
        {
            get
            {
                if (string.IsNullOrEmpty(_copyrightsign))
                {
                    _copyrightsign = ConfigurationManager.AppSettings["copyrightsign"];
                }
                return _copyrightsign;
            }
        }
        /// <summary>
        /// 版权保护公钥
        /// </summary>
        private static string _copyrightpublickey;
        public static string copyrightpublickey
        {
            get
            {
                if (string.IsNullOrEmpty(_copyrightpublickey))
                {
                    _copyrightpublickey = ConfigurationManager.AppSettings["copyrightpublickey"];
                }
                return _copyrightpublickey;
            }
        }
       /// <summary>
       /// SM2对象
       /// </summary>
        private static TanSM2Ex _sm2;
        public static TanSM2Ex SM2
        {
            get
            {
                if (_sm2 == null)
                {
                    _sm2 = new TanSM2Ex();
                }
                return _sm2;
            }
        }
        /// <summary>
        /// 验证签名
        /// </summary>
        /// <returns></returns>
        public static bool verifysign()
        {
            if (string.IsNullOrEmpty(copyright))
            {
                throw new Exception("版权标识被移除！");
            }
            if (string.IsNullOrEmpty(copyrightsign))
            {
                throw new Exception("版权签名被移除！");
            }
            if (string.IsNullOrEmpty(copyrightpublickey))
            {
                throw new Exception("版权公钥被移除！");
            }
            if (!SM2.TanSM2Verify(copyright, copyrightpublickey, copyrightsign))
            {

                throw new Exception("版权签名信息验证失败!");
            }
            return true;
        }
    }
}
