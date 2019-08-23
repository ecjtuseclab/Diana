using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Diana.Idal;
using System.Data;
using Diana.model;
using Diana.common;
using Diana.common.Util;
using Diana.common.Util.WebControl;
using basedal;
using System.IO;
using Diana.bll.WorkflowVisualization;

/// <summary>
///项目名称：Diana轻量级开发框架
///版本：0.0.1版
///开发团队成员：胡凯雨，张梦丽，艾美珍，易传佳，陈祚松，康文洋，张婷婷，王露，周庆，夏萍萍，陈兰兰
///模块和代码页功能描述：工作流控制器
///最后修改时间：2018/1/20
///修改日志：
/// </summary>
namespace Web.Control
{
    public class workflowControler : baseControler
    {
        /// <summary>
        /// 根据的添加方法名，不需要再验证方法是否有权限访问
        /// </summary>
        public workflowControler()//不需要验证的方法在构造方法添加
        {
            noauth_actions.Add("WorkflowVisualization");

            noauth_actions.Add("AddProcess");

            noauth_actions.Add("UpdateProcess");
        }
        /// <summary>
        /// 默认视图
        /// </summary>
        public override void view()
        {
            getbuttonresource("workflow", 1);
            tp.Display(ctx, "/Html/workflow.html");
        }

        public void Form()
        {
            tp.Display(ctx, "/Html/layer/workflowform_layer.html");
        }

        public void Details()
        {
            tp.Display(ctx, "/Html/layer/workflowdetail_layer.html");
        }

        #region 基本操作

        /// <summary>
        /// 显示数据
        /// </summary>
        public void showAll()
        {
            int pageSize = Convert.ToInt32(!string.IsNullOrEmpty(ctx.Request["rows"]) ? ctx.Request["rows"] : ctx.Request["pageSize"]);
            int pageIndex = Convert.ToInt32(!string.IsNullOrEmpty(ctx.Request["page"]) ? ctx.Request["page"] : ctx.Request["pageIndex"]);
            string keyword = ctx.Request["keyword"];
            int records = IdalCommon.IworkflowEx.getWorkflowList(keyword).Count;
            int totalPage = records % pageSize == 0 ? records / pageSize : records / pageSize + 1; //页数
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("wfname", keyword);
            List<workflow> wflist = IdalCommon.IworkflowEx.getPaginationEntityList(pageIndex, pageSize, dictionary);
            var data = new
            {
                rows = wflist,
                total = totalPage,
                page = pageIndex,
                TotalCount = records,
                PageSize = pageSize,
                CurrentPage = pageIndex,
                TotalPage = totalPage,
                DataList = wflist
            };
            writeJsonBack(data.ToJson());
        }

        /// <summary>
        /// 获取表单数据
        /// </summary>
        public void GetFormJson()
        {
            int id = Convert.ToInt32(ctx.Request["keyValue"]);
            workflow wf = IdalCommon.IworkflowEx.getEntityById(id);
            writeJsonBack(wf.ToJson());
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        public void DeleteForm()
        {
            string keyValue = !string.IsNullOrEmpty(ctx.Request["keyValue"]) ? ctx.Request["keyValue"] : ctx.Request["id"];
            int id = Convert.ToInt32(keyValue);
            if (IdalCommon.IworkflowEx.delete(id))
            {
                Success("删除成功!");
            }
            else
            {
                Error("删除失败!");
            }
           
        }

        /// <summary>
        /// 复制
        /// </summary>
        public void copyAndPasteForm()
        {
            string keyValue = !string.IsNullOrEmpty(ctx.Request["keyValue"]) ? ctx.Request["keyValue"] : ctx.Request["id"];
            if (IdalCommon.IworkflowEx.copyAndPaste(keyValue))
            {
                Success("复制成功!");
            }
            else
            {
                Error("复制失败!");
            }
        }

        /// <summary>
        /// 提交表单数据
        /// </summary>
        public void SubmitForm()
        {
            try
            {
                string keyValue = !string.IsNullOrEmpty(ctx.Request["keyValue"]) ? ctx.Request["keyValue"] : ctx.Request["id"];
                workflow wf = HttpContextDataWrapper.DataToObject<workflow>(ctx);
                IdalCommon.IworkflowEx.SubmitForm(wf, keyValue);
                Success("操作成功!");
            }
            catch (Exception)
            {
                Error("操作失败!");
            }
        }
        #endregion

        #region 工作流流程设计

        public void WorkflowVisualization()
        {
            object wfid =ctx.Request["wfid"]; 
            if (wfid != null)
            {
                workflow wf = IdalCommon.IworkflowEx.getEntityById(Convert.ToInt32(wfid));
                tp.Put("workflow",wf.ToJson()); 
                string resutlt = "{}";
                if (!string.IsNullOrEmpty(wf.wfjsonstr))
                {
                    resutlt = wf.wfjsonstr.TrimStart('"').TrimEnd('"');
                }
                tp.Put("workflowjsonstr", resutlt);
                string actionliststr=wfselectoption.CreateList(IdalCommon.IactionEx.getActionListbywfid(Convert.ToInt32(wfid)), "id", "actiondescription").ToJson();
                tp.Put("actionlist", actionliststr);
                string roleliststr= wfselectoption.CreateList(IdalCommon.IroleEx.getEntityList(), "id", "rolename").ToJson();
                tp.Put("rolelist", roleliststr);
            }
            tp.Display(ctx, "/Html/WorkflowVisualization.html");
        }

        public void  ProcessDesign()
        {
            try
            {
                string workflowJson = ctx.Request["workflowJson"];
                WorkflowPanel wp =  Json.ToObject<WorkflowPanel>(workflowJson);
                string primitiveJSonStr = ctx.Request["primitiveJSonStr"];
                wp.workflowjson = primitiveJSonStr;
               bool result= wp.Save();
               var para = new { result = result };
               writeJsonBack(para.ToJson());
            }
            catch (Exception ex)
            {
                writeJsonBack(new { result=false }.ToJson());
                string message = ex.Message; 
            }
        }
        #endregion

        public class wfselectoption
        {
            public wfselectoption() { ;}
            public object value { get; set; }
            public object name { get; set; }
            public wfselectoption(object value, string name)
            {
                this.value = value;
                this.name = name;
            }
            public static wfselectoption Create(object instance, string valueProp = "Id", string textProp = "Name")
            {

                Dictionary<string, object> dic = Json.ToObject<Dictionary<string, object>>(Json.ToJson(instance));
                wfselectoption option = new wfselectoption();

                if (dic.Keys.Contains(valueProp) && dic.Keys.Contains(textProp))
                {
                    option.value = dic[valueProp] == null ? null : dic[valueProp].ToString();
                    option.name  = dic[textProp] == null ? null : dic[textProp].ToString();
                }
                else
                {
                    if (option.value == null)
                    {
                        int i = 0;
                        foreach (var k in dic.Keys)//获取第2个
                        {
                            if (i == 0)
                            {
                                option.value = dic[k] == null ? null : dic[k].ToString();

                            }
                            if (i == 1)
                            {
                                option.name= dic[k] == null ? null : dic[k].ToString();
                            }
                            i++;
                        }
                    }
                }
                return option;
            }
            public static List<wfselectoption> CreateList<T>(IEnumerable<T> instanceList, string valueProp = "Id", string textProp = "Name")
            {
                List<wfselectoption> options = new List<wfselectoption>();
                foreach (var instance in instanceList)
                {
                    wfselectoption option = wfselectoption.Create(instance, valueProp, textProp);
                    options.Add(option);
                }

                return options;
            }
        }

    }
}

