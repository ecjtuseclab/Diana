using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Diana.model;
/// <summary>
///项目名称：Diana轻量级开发框架
///版本：0.0.1版
///开发团队成员：胡凯雨，张梦丽，艾美珍，易传佳，陈祚松，康文洋，张婷婷，王露，周庆，夏萍萍，陈兰兰
///模块和代码页功能描述：字典接口，供IOC
///最后修改时间：2018/1/26
///日志说明：
/// </summary>
namespace Diana.Idal
{
    public interface IresourceEx : IRepositoryBase<resource>
    {
        #region 1.基本操作
       
        /// <summary>
        /// 判断新增的资源是否合法，即资源名称不能重复
        /// </summary>
        /// <param name="resourcename">资源名称</param>
        /// <returns>合法：true;不合法：false</returns>
        bool isLeagalResourcename(string resourcename);
        /// <summary>
        /// 新增资源
        /// </summary>
        /// <param name="table"></param>
        /// <returns>成功：新增资源数据的主码；失败：-1</returns>
        //int insert(resource table);
        /// <summary>
        /// 根据资源名称删除资源
        /// </summary>
        /// <param name="resourcename"></param>
        /// <returns>成功：true;失败:false</returns>
        bool delete(string resourcename);
        /// <summary>
        /// 根据资源id删除资源
        /// </summary>
        /// <param name="resourceid">资源id</param>
        /// <returns>成功：true;失败:false</returns>
        bool delResource(int resourceid);
       
        /// <summary>
        /// 差异更新
        /// </summary>
        /// <param name="id">跟新数据主码</param>
        /// <param name="dictionary">需要更新的数据</param>
        /// <returns>返回更新结果（true/false）</returns>
        bool update(int id, Dictionary<string, object> dictionary);
        /// <summary>
        /// 根据资源父级获得子资源数据
        /// </summary>
        /// <param name="resourceowner">父级</param>
        /// <returns></returns>
        List<resource> getResourceByOwner(string resourceowner);
        /// <summary>
        /// 根据资源的名称获得某资源
        /// </summary>
        /// <param name="resourcename">资源名称</param>
        /// <returns>一个资源对象</returns>
        resource getResource(string resourcename);
        /// <summary>
        /// 根据资源的id获得资源对象
        /// </summary>
        /// <param name="resourceid">资源id</param>
        /// <returns>一个资源对象</returns>
        resource getResource(int resourceid);
        /// <summary>
        /// 获得所有的资源对象
        /// </summary>
        /// <returns>资源对象列表</returns>
        List<resource> getAllResource();
        /// <summary>
        /// 根据资源名称获得此资源对象的id
        /// </summary>
        /// <param name="resourcename">资源名称</param>
        /// <returns>资源id</returns>
        int getResourceId(string resourcename);
       

        /// <summary>
        /// 通过资源名称获取若干条资源数据
        /// </summary>
        /// <param name="resourcename">资源名称</param>
        /// <returns>若干条资源数据</returns>
        List<resource> getResourceList(string resourcename);

        void SubmitForm(resource resourceEntity, string keyValue);

         /// <summary>
        /// 通过控制器名称获取若干条资源数据
        /// </summary>
        /// <param name="resourcename">资源名称</param>
        /// <returns>若干条资源数据</returns>
        resource getResourcebycontrolername(string controlername,int resourcleval);


        /// <summary>
        /// 根据控制器的名称获得某资源
        /// </summary>
        /// <param name="controlername">控制器名称</param>
        /// <param name="resourceleval">资源级别</param>
        /// <returns>资源对象</returns>
        List<resource> getResourcesbycontrolername(string controlername, int resourceleval);
        

        #endregion
    }
}
