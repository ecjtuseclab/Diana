using System;
using Diana.model;
using System.Collections.Generic;
/// <summary>
///项目名称：Diana轻量级开发框架
///版本：0.0.1版
///开发团队成员：胡凯雨，张梦丽，艾美珍，易传佳，陈祚松，康文洋，张婷婷，王露，周庆，夏萍萍，陈兰兰
///模块和代码页功能描述：工作流接口，供IOC
///最后修改时间：2018/1/26
///日志说明：
/// </summary>
namespace Diana.Idal
{
    public interface IworkflownodeactionEx : IRepositoryBase<workflownodeaction>
    {
       
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        //int insert(workflownodeaction table);
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
        /// 获得所有节点动作数据
        /// </summary>
        /// <returns></returns>
        List<workflownodeaction> getInitworkflownodeaction();
        /// <summary>
        /// 根据工作流ID和动作名称获得节点动作
        /// </summary>
        /// <param name="workflowid"></param>
        /// <param name="actionname"></param>
        /// <returns></returns>
        workflownodeaction getworkflownodeaction(int workflowid, string actionname);
        /// <summary>
        /// 根据id获得节点动作数据
        /// </summary>
        /// <param name="workflownodeactionid"></param>
        /// <returns></returns>
        workflownodeaction getworkflownodeaction(int workflownodeactionid);
        /// <summary>
        /// 根据节点动作获得同当前节点和下个节点、同节点类型的节点动作数据
        /// </summary>
        /// <param name="nodeaction"></param>
        /// <returns></returns>
        List<workflownodeaction> getcountersignnodeaction(workflownodeaction nodeaction);
        /// <summary>
        /// 获得所有工作流节点动作数据
        /// </summary>
        /// <returns></returns>
        List<workflownodeaction> getAllworkflownodeaction();
        /// <summary>
        /// 复制粘贴
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        //bool copyAndPaste(string ids);

        /// <summary>
        /// 根据节点动作名称返回节点动作数据
        /// </summary>
        /// <param name="nodeactionname"></param>
        /// <returns></returns>
        List<workflownodeaction> getworkflownodeactionList(string nodeactionname);

        /// <summary>
        /// 表单提交
        /// </summary>
        /// <param name="wfnc"></param>
        /// <param name="KewValue"></param>
        void SubmitForm(workflownodeaction wfnc, string KeyValue);
        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <returns></returns>
        List<action> getactionList();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="wfid"></param>
        /// <returns></returns>
        List<workflownodeaction> getEntityList(int wfid);
    }
}
