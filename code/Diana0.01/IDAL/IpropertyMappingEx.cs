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


    public interface IpropertyMappingEx : IRepositoryBase<propertymapping>
    {
        /// <summary>
        /// 根据List id 批量删除
        /// </summary>
        /// <param name="ids">List<ids></param>
        /// <returns>bool</returns>
        bool deleteList(string[] ids);
        /// <summary>
        /// 差异更新
        /// </summary>
        /// <param name="id">跟新数据主码</param>
        /// <param name="dictionary">需要更新的数据</param>
        /// <returns>返回更新结果（true/false）</returns>
        bool update(int id, Dictionary<string, object> dictionary);
        /// <summary>
        /// 根据propertyMeaning获得数据
        /// </summary>
        /// <param name="propertyMeaning">propertyMeaning</param>
        /// <returns>一条数据</returns>
        propertymapping getPropertyMapping(string propertyMeaning);
        /// <summary>
        /// 根据数据id获得动作数据
        /// </summary>
        /// <param name="actionid">数据id</param>
        /// <returns>一条数据</returns>
        propertymapping getPropertyMapping(int propertymappingid);
        /// <summary>
        /// 获得所有数据
        /// </summary>
        /// <returns>数据List</returns>
        List<propertymapping> getAllPropertyMapping();
        /// <summary>
        /// 获取所有数据字典对象条数
        /// </summary>
        /// <returns></returns>
        //int pageCount();
        /// <summary>
        /// 根据propertyMeaning获得数据id
        /// </summary>
        /// <param name="actionname">propertyMeaning</param>
        /// <returns>数据id</returns>
        int getPropertyMappingId(string propertyMeaning);

        /// <summary>
        /// 提交表单数据
        /// </summary>
        /// <param name="pro">数据字典实体</param>
        /// <param name="KeyValue">主键</param>
        void SubmitForm(propertymapping pro,string KeyValue);

        /// <summary>
        /// 根据名称模糊查询获取数据
        /// </summary>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        List<propertymapping> getPropertymappingList(string keyword);
       
    }
}
