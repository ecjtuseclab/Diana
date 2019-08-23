using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Diana.model;
using Diana.Idal;
/// <summary>
///项目名称：Diana轻量级开发框架
///版本：0.0.1版
///开发团队成员：胡凯雨，张梦丽，艾美珍，易传佳，陈祚松，康文洋，张婷婷，王露，周庆，夏萍萍，陈兰兰
///模块和代码页功能描述：工作流操作者接口，供IOC
///最后修改时间：2018/1/26
///日志说明：
/// </summary>
namespace Diana.Idal
{
    public interface IworkflownodeoperatorEx : IRepositoryBase<workflownodeoperator>
    {

        /// <summary>
        /// 差异更新
        /// </summary>
        /// <param name="id">跟新数据主码</param>
        /// <param name="dictionary">需要更新的数据</param>
        /// <returns>返回更新结果（true/false）</returns>
         bool update(int id, Dictionary<string, object> dictionary);
        /// <summary>
        /// 根据节点动作id和操作者id即用户id获取节点操作者数据
        /// </summary>
        /// <param name="wfnodeactionid"></param>
        /// <param name="operatorid"></param>
        /// <returns></returns>
         workflownodeoperator getworkflownodeoperator(int wfnodeactionid, int operatorid);
        
        /// <summary>
        /// 根据ID获取节点操作者数据
        /// </summary>
        /// <param name="workflownodeoperatorid"></param>
        /// <returns></returns>
         workflownodeoperator getworkflownodeoperator(int workflownodeoperatorid);
        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <returns></returns>
         List<workflownodeoperator> getAllworkflownodeoperator();
       
         /// <summary>
         /// 表单提交
         /// </summary>
         /// <param name="wfnoEntity"></param>
         /// <param name="KeyValue"></param>
         void SubmitForm(workflownodeoperator wfnoEntity, string KeyValue);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nodeactionid"></param>
        /// <returns></returns>
         bool deletebynodeactionid(int nodeactionid);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nodeactionids"></param>
        /// <returns></returns>
         bool deletebynodeactionids(List<int> nodeactionids);
       
    }
}
