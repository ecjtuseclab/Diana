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
///模块和代码页功能描述：工作流接口，供IOC
///最后修改时间：2018/1/26
///日志说明：
/// </summary>
namespace Diana.Idal
{
    public interface IworkflowEx : IRepositoryBase<workflow>
    {
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        //int insert(workflow table);
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
        /// 根据工作流名获得某条数据
        /// </summary>
        /// <param name="wfname"></param>
        /// <returns></returns>
        workflow getworkflow(string wfname);
        /// <summary>
        /// 根据工作流id获得某条数据
        /// </summary>
        /// <param name="workflowid"></param>
        /// <returns></returns>
        workflow getworkflow(int workflowid);

        /// <summary>
        /// 获得所有工作流数据
        /// </summary>
        /// <returns></returns>
        List<workflow> getAllworkflow();
        /// <summary>
        /// 根据工作流名获取工作流ID
        /// </summary>
        /// <param name="wfname"></param>
        /// <returns></returns>
        int getworkflowId(string wfname);
        /// <summary>
        /// 复制粘贴
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        //bool copyAndPaste(string ids);

        /// <summary>
        /// 根据搜索信息获取工作流数据
        /// </summary>
        /// <param name="searchInfo"></param>
        /// <returns></returns>
        List<workflow> getWorkflowList(string searchInfo);

        /// <summary>
        /// 提交表单数据
        /// </summary>
        /// <param name="wfEntity"></param>
        /// <param name="keyValue"></param>
        void SubmitForm(workflow wfEntity, string keyValue);

     
    }
}
