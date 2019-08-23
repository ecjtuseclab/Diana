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
///模块和代码页功能描述：分组接口，供IOC
///最后修改时间：2018/1/26
///日志说明：
/// </summary>
namespace Diana.Idal
{

    public interface IgroupEx:IRepositoryBase<group>
    {
        #region 基本方法
       
        /// <summary>
        /// 根据id删除
        /// </summary>
        /// <param name="id">主键id</param>
        /// <returns></returns>
        bool deleteById(int id);
        /// <summary>
        /// 根据组名删除
        /// </summary>
        /// <param name="groupName">组名</param>
        /// <returns>bool</returns>
        bool deleteByName(string groupName);
       
        /// <summary>
        /// 差异更新
        /// </summary>
        /// <param name="id">跟新数据主码</param>
        /// <param name="dictionary">需要更新的数据</param>
        /// <returns>返回更新结果（true/false）</returns>
        bool update(int id, Dictionary<string, object> dictionary);
        /// <summary>
        /// 根据主键id更新
        /// </summary>
        /// <param name="id">group主键id</param>
        /// <returns></returns>
        bool updateById(int id);
        /// <summary>
        /// 根据组名更新
        /// </summary>
        /// <param name="groupName">组名</param>
        /// <returns>bool</returns>
        bool updateByGroupName(string groupName);
        /// <summary>
        /// 更新所有
        /// </summary>
        /// <returns></returns>
        bool updateAll();
        /// <summary>
        /// 动态根据字段名，字段值查询指定数据
        /// </summary>
        /// <param name="key">字段名</param>
        /// <param name="value">字段值</param>
        /// <returns>List<group></returns>
        List<group> getDynamicGroup(string key, string value);
        /// <summary>
        /// 根据用户名获取的组
        /// </summary>
        /// <param name="groupName">组名</param>
        /// <returns></returns>
        group getGroup(string groupName);
        /// <summary>
        /// 根据主键id获取组
        /// </summary>
        /// <param name="groupId">主键id</param>
        /// <returns></returns>
        group getGroup(int groupId);
        /// <summary>
        /// 查找指定字段的数据
        /// </summary>
        /// <param name="groupIds">string[] 数组id</param>
        /// <param name="tableNames">字段名</param>
        /// <returns>List<group></returns>
        //List<groupExcelValidateView> getGruop(string[] groupIds, string tableNames);
        /// <summary>
        /// 获取所有组
        /// </summary>
        /// <returns>List<group></returns>
        List<group> getAllGroup();
        /// <summary>
        /// group表分页
        /// </summary>
        /// <param name="pageIndex">当前页的索引</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <returns></returns>
        List<group> getPageGroup(int pageIndex, int pageSize);
        /// <summary>
        /// 根据组名获取主键id
        /// </summary>
        /// <param name="groupName"></param>
        /// <returns></returns>
        int getGroupId(string groupName);
        /// <summary>
        /// 批量粘贴数据并插入数据库
        /// </summary>
        /// <param name="ids">（ids:1,2,3）</param>
        /// <returns></returns>
        bool copypAndPaste(string ids); //复制粘贴资源
        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupIds">字符串拼接的组ids，拼接格式（1,2,3）</param>
        /// <param name="cellHeaders">字符串拼接的字段名(id,groupName)</param>
        /// <param name="cellHeaderNames">字符串拼接的表单别名(ID,组名)</param>
        /// <returns>bool</returns>
        bool excelExport(string groupIds, string cellHeaders, string cellHeaderNames);
        /// <summary>
        /// excel导入
        /// </summary>
        Dictionary<string, string> getCellHeaderName(string[] cellHeaderNames, string[] cellHeaderNameArr);
        /// <summary>
        /// 空方法：后期完善
        /// </summary>
        /// <returns></returns>
        bool excelImport();
        /// <summary>
        /// 空方法：后期完善
        /// </summary>
        /// <returns></returns>
        bool pdfExport();
        /// <summary>
        /// 空方法：后期完善
        /// </summary>
        /// <returns></returns>
        bool wordImport();
        /// <summary>
        /// 空方法：后期完善
        /// </summary>
        /// <returns></returns>
        bool wordExport();
        
        #endregion

        #region 基本方法 ycj
        /// <summary>
        /// 根据搜索信息获取分组列表
        /// </summary>
        /// <param name="searchInfo"></param>
        /// <returns></returns>
        List<group> getGroupList(string searchInfo);
        /// <summary>
        /// 提交表单
        /// </summary>
        /// <param name="g"></param>
        /// <param name="keyValue"></param>
        void SubmitForm(group g, string keyValue);
        #endregion  
    }
    /// <summary>
    /// excel导入验证视图类
    /// </summary>
    public interface groupExcelValidateView
    {
        //bool _isExcelVaildateOK = true;
        /// <summary>
        /// Excel验证是否通过，默认为true
        /// <para>true：通过；false：不通过</para>
        /// </summary>
        //bool IsExcelVaildateOK;
    }

    
}
