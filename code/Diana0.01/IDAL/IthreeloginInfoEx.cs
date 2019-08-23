using Diana.model;
using Diana.model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
///项目名称：Diana轻量级开发框架
///版本：0.0.1版
///开发团队成员：胡凯雨，张梦丽，艾美珍，易传佳，陈祚松，康文洋，张婷婷，王露，周庆，夏萍萍，陈兰兰
///模块和代码页功能描述：第三方登录接口，供IOC
///最后修改时间：2018/1/26
///日志说明：
/// </summary>
namespace Diana.Idal
{
    public interface IthreeloginInfoEx : IRepositoryBase<threeloginInfo>
    {
        #region 1.基本操作
        /// <summary>
        /// 根据多个id组成的字符串删除多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool delete(string[] ids);

        /// <summary>
        /// 差异更新
        /// </summary>
        /// <param name="id">跟新数据主码</param>
        /// <param name="dictionary">需要更新的数据</param>
        /// <returns>返回更新结果（true/false）</returns>
        bool update(int id, Dictionary<string, object> dictionary);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="threeloginInfoid"></param>
        /// <returns></returns>
        threeloginInfo getthreeloginInfo(int threeloginInfoid);
        /// <summary>
        /// 获得所有数据
        /// </summary>
        /// <returns></returns>
        List<threeloginInfo> getAllthreeloginInfo();

        threeloginInfo getthreeloginInfobyuid(string uid);

        void InsertthreeloginInfo(threeloginInfo threeloginInfo);
      
        #endregion
    }
}
