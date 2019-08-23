using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Diana.Idal;
using Diana.model;
using Diana.common;
using Diana.common.Util;
using Diana.common.Util.Web;
using Diana.common.Util.WebControl;
using basedal;
using System.IO;
using System.Text;
/// <summary>
///项目名称：Diana轻量级开发框架
///版本：0.0.1版
///开发团队成员：胡凯雨，张梦丽，艾美珍，易传佳，陈祚松，康文洋，张婷婷，王露，周庆，夏萍萍，陈兰兰
///模块和代码页功能描述：工作流节点动作控制器
///最后修改时间：2018/1/25
/// </summary>
namespace Web.Control
{

    public class workflownodeactionControler : baseControler
    {
        /// <summary>
        /// 根据的添加方法名，不需要再验证方法是否有权限访问
        /// </summary>
        public workflownodeactionControler()//不需要验证的方法在构造方法添加
        {
           
        }

        /// <summary>
        /// 默认视图
        /// </summary>
        public override void view()
        {
            getbuttonresource("workflownodeaction", 1);
            List<SelectOption> actionOptions = SelectOption.CreateList(IdalCommon.IworkflownodeactionEx.getactionList(), "id", "actionname");
            List<SelectOption> workflowList = SelectOption.CreateList(IdalCommon.IworkflowEx.getEntityList(), "id", "wfname");
            List<SelectOption> nodeList = SelectOption.CreateList(IdalCommon.IworkflownodeEx.getEntityList(), "id", "wfnodename");
            tp.Put("WorkflowList", workflowList);
            tp.Put("WfNodeList", nodeList);
            tp.Put("actionList", actionOptions);
            tp.Display(ctx, "/Html/workflownodeaction.html");
        }

        public void Form()
        {
            tp.Display(ctx, "/Html/layer/workflownodeactionform_layer.html");
          
        }

        public void Details()
        {
            tp.Display(ctx, "/Html/layer/workflownodeactiondetail_layer.html");
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
            int records = IdalCommon.IworkflownodeactionEx.getworkflownodeactionList(keyword).Count;
            int totalPage = records % pageSize == 0 ? records / pageSize : records / pageSize + 1; //页数
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("nodeactionname", keyword);
            List<workflownodeaction> wfnodeacionlist = IdalCommon.IworkflownodeactionEx.getPaginationEntityList(pageIndex, pageSize, dictionary);
            var data = new
            {
                rows = wfnodeacionlist,
                total = totalPage,
                page = pageIndex,
                TotalCount = records,
                PageSize = pageSize,
                CurrentPage = pageIndex,
                TotalPage = totalPage,
                DataList = wfnodeacionlist
            };
            writeJsonBack(data.ToJson());
        }

        /// <summary>
        /// 获取表单数据
        /// </summary>
        public void GetFormJson()
        {
            int id = Convert.ToInt32(ctx.Request["keyValue"]);
            workflownodeaction wfna = IdalCommon.IworkflownodeactionEx.getEntityById(id);
            writeJsonBack(wfna.ToJson());
        }

        /// <summary>
        /// 获取工作流
        /// </summary>
        public void GetSelectJson()
        {
            string keyValue = ctx.Request["keyValue"];
            List<object> list = new List<object>();
            if (!string.IsNullOrEmpty(keyValue))
            {
                int wfid = int.Parse(keyValue);
                var data = IdalCommon.IworkflowEx.getEntityById(wfid);
                list.Add(new { id = data.id, text = data.wfname });
            }
            else
            {
                var data = IdalCommon.IworkflowEx.getEntityList();
                foreach (workflow item in data)
                {
                    list.Add(new { id = item.id, text = item.wfname });
                }
            }
            writeJsonBack(list.ToJson());
        }
        /// <summary>
        /// 获取节点
        /// </summary>
        public void GetSelectNodeJson()
        {
            string keyValue = ctx.Request["keyValue"];
            List<object> list = new List<object>();
            if (!string.IsNullOrEmpty(keyValue))
            {
                int id = int.Parse(keyValue);
                var data = IdalCommon.IworkflownodeEx.getEntityById(id);
                list.Add(new { id = data.id, text = data.wfnodename });
            }
            else
            {
                var data = IdalCommon.IworkflownodeEx.getEntityList();
                foreach (workflownode item in data)
                {
                    list.Add(new { id = item.id, text = item.wfnodename });
                }
            }
            writeJsonBack(list.ToJson());
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        public void DeleteForm()
        {
            string keyValue = !string.IsNullOrEmpty(ctx.Request["keyValue"]) ? ctx.Request["keyValue"] : ctx.Request["id"];
            int id = Convert.ToInt32(keyValue);
            if (IdalCommon.IworkflownodeactionEx.delete(id))
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
            if (IdalCommon.IworkflownodeactionEx.copyAndPaste(keyValue))
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
                workflownodeaction wfna = HttpContextDataWrapper.DataToObject<workflownodeaction>(ctx);
                IdalCommon.IworkflownodeactionEx.SubmitForm(wfna, keyValue);
                Success("操作成功!");
            }
            catch (Exception)
            {
                Error("操作失败!");
            }
        }

        #endregion

        #region 2.扩展方法

        /// <summary>
        /// 获取所属动作名称
        /// </summary>
        public void GetActionName()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            //List<action> data = IdalCommon.IworkflownodeactionEx.getactionList();
            List<action> data = IdalCommon.IactionEx.getEntityList();
            foreach (var item in data)
            {
                sb.Append("{\"Value\":\"" + item.id + "\",\"Text\":\"" + item.actionname + "\"},");
            }
            sb.Remove(sb.Length - 1, 1);
            sb.Append("]");
            writeJsonBack(sb.ToString());
        }
        /// <summary>
        /// 获取所属工作流名称
        /// </summary>
        public void GetWorkflowName()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            List<workflow> data = IdalCommon.IworkflowEx.getEntityList();
            foreach (var item in data)
            {
                sb.Append("{\"Value\":\"" + item.id + "\",\"Text\":\"" + item.wfname + "\"},");
            }
            sb.Remove(sb.Length - 1, 1);
            sb.Append("]");
            writeJsonBack(sb.ToString());
        }
        /// <summary>
        /// 获取所属工作流节点名称
        /// </summary>
        public void GetWorkflownodeName()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            List<workflownode> data = IdalCommon.IworkflownodeEx.getEntityList();
            foreach (var item in data)
            {
                sb.Append("{\"Value\":\"" + item.id + "\",\"Text\":\"" + item.wfnodename + "\"},");
            }
            sb.Remove(sb.Length - 1, 1);
            sb.Append("]");
            writeJsonBack(sb.ToString());
        }
        #endregion
    }
}

