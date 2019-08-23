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
///模块和代码页功能描述：区域接口，供IOC
///最后修改时间：2018/1/26
///日志说明：
/// </summary>
namespace Diana.Idal
{

    public interface IareaEx : IRepositoryBase<area>
    {
       
       
        /// <summary>
        /// 判断新增的区域是否合法，即区域名称不能重复
        /// </summary>
        /// <param name="areaname">区域名称</param>
        /// <returns>合法：true;不合法：false</returns>
        bool isLeagalAreaname(string fullname);
        /// <summary>
        /// 新增区域
        /// </summary>
        /// <param name="table"></param>
        /// <returns>成功：新增区域数据的主码；失败：-1</returns>
        //int insert(area table);
        /// <summary>
        /// 根据区域名称删除区域
        /// </summary>
        /// <param name="fullname"></param>
        /// <returns>成功：true;失败:false</returns>
        bool delete(string fullname);
        /// <summary>
        /// 根据区域id删除区域
        /// </summary>
        /// <param name="areaid">区域id</param>
        /// <returns>成功：true;失败:false</returns>
        bool delArea(int areaid);
       
        /// <summary>
        /// 差异更新
        /// </summary>
        /// <param name="id">跟新数据主码</param>
        /// <param name="dictionary">需要更新的数据</param>
        /// <returns>返回更新结果（true/false）</returns>
        bool update(int id, Dictionary<string, object> dictionary);
        /// <summary>
        /// 根据父级获得区域数据
        /// </summary>
        /// <param name="parentid">父级</param>
        /// <returns>区域数据</returns>
        List<area> getAreaByParentId(int parentid);
        /// <summary>
        /// 根据区域的名称获得某区域
        /// </summary>
        /// <param name="fullname">区域名称</param>
        /// <returns>一个区域对象</returns>
        area getArea(string fullname);
        /// <summary>
        /// 根据区域的id获得区域对象
        /// </summary>
        /// <param name="areaid">区域id</param>
        /// <returns>一个区域对象</returns>
        area getArea(int areaid);
        /// <summary>
        /// 根据区域的id获得区域对象
        /// </summary>
        /// <param name="areaid">区域id</param>
        /// <returns>一个区域对象</returns>
        List<area> getParentArea();
        /// <summary>
        /// 获得所有的区域对象
        /// </summary>
        /// <returns>区域对象列表</returns>
        List<area> getAllArea();
        /// <summary>
        /// 根据区域名称获得此区域对象的id
        /// </summary>
        /// <param name="fullname">区域名称</param>
        /// <returns>区域id</returns>
        int getAreaId(string fullname);
       

        /// <summary>
        /// 根据区域名返回区域数据
        /// </summary>
        /// <param name="areaname"></param>
        /// <returns></returns>
        List<area> getAreaList(string areaname);

        /// <summary>
        /// 表单提交
        /// </summary>
        /// <param name="areaEntity"></param>
        /// <param name="keyValue"></param>
        void SubmitForm(area areaEntity, string keyValue);


    }
}
