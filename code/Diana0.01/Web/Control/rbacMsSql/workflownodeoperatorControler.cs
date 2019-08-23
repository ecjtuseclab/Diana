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
/// <summary>
///项目名称：Diana轻量级开发框架
///版本：0.0.1版
///开发团队成员：胡凯雨，张梦丽，艾美珍，易传佳，陈祚松，康文洋，张婷婷，王露，周庆，夏萍萍，陈兰兰
///模块和代码页功能描述：工作流节点操作者控制器
///最后修改时间：2018/1/25
/// </summary>
namespace Web.Control
{


    public class workflownodeoperatorControler : baseControler
    {        
        /// <summary>
        /// 根据的添加方法名，不需要再验证方法是否有权限访问
        /// </summary>
        public workflownodeoperatorControler()//不需要验证的方法在构造方法添加
        {
           
        }

        /// <summary>
        /// 默认视图
        /// </summary>
        public override void view()
        {
            getbuttonresource("workflownodeoperator", 1);
            tp.Display(ctx, "/Html/workflownodeoperator.html");
        }

        public void Form()
        {
            tp.Display(ctx, "/Html/layer/workflownodeoperatorform_layer.html");
        }

        public void Details()
        {
            tp.Display(ctx, "/Html/layer/workflownodeoperatordetail_layer.html");
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
            int records = IdalCommon.IworkflownodeoperatorEx.getEntityList().Count;
            int totalPage = records % pageSize == 0 ? records / pageSize : records / pageSize + 1; //页数
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("operatorid", keyword);
            List<workflownodeoperator> wfnodeperatorlist = IdalCommon.IworkflownodeoperatorEx.getPaginationEntityList(pageIndex, pageSize, dictionary);
            var data = new
            {
                rows = wfnodeperatorlist,
                total = totalPage,
                page = pageIndex,
                TotalCount = records,
                PageSize = pageSize,
                CurrentPage = pageIndex,
                TotalPage = totalPage,
                DataList = wfnodeperatorlist
            };
            writeJsonBack(data.ToJson());
        }

        /// <summary>
        /// 获取表单数据
        /// </summary>
        public void GetFormJson()
        {
            int id = Convert.ToInt32(ctx.Request["keyValue"]);
            workflownodeoperator wfnodeoperator = IdalCommon.IworkflownodeoperatorEx.getEntityById(id);
            writeJsonBack(wfnodeoperator.ToJson());
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        public void DeleteForm()
        {
            string keyValue = !string.IsNullOrEmpty(ctx.Request["keyValue"]) ? ctx.Request["keyValue"] : ctx.Request["id"];
            int id = Convert.ToInt32(keyValue);
            if (IdalCommon.IworkflownodeoperatorEx.delete(id))
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
            if (IdalCommon.IworkflownodeoperatorEx.copyAndPaste(keyValue))
            {
                Success("复制成功!");
            }
            else
            {
                Error("复制失败!");
            }
        }

        /// <summary>
        /// 获取树形数据
        /// </summary>
        public void GetSelectNaJson()
        {
            string keyValue = ctx.Request["keyValue"];
            List<object> list = new List<object>();
            if (!string.IsNullOrEmpty(keyValue))
            {
                int wfid = int.Parse(keyValue);
                var data = IdalCommon.IworkflownodeactionEx.getEntityById(wfid);
                list.Add(new { id = data.id, text = data.nodeactionname });
            }
            else
            {
                var data = IdalCommon.IworkflownodeactionEx.getEntityList();
                foreach (workflownodeaction item in data)
                {
                    list.Add(new { id = item.id, text = item.nodeactionname });
                }
            }
            writeJsonBack(list.ToJson());
        }

        public void GetSelectUserJson()
        {
            string keyValue = ctx.Request["keyValue"];
            List<object> list = new List<object>();
            if (!string.IsNullOrEmpty(keyValue))
            {
                int wfid = int.Parse(keyValue);
                var data = IdalCommon.IuserEx.getEntityById(wfid);
                list.Add(new { id = data.id, text = data.username });
            }
            else
            {
                var data = IdalCommon.IuserEx.getEntityList();
                foreach (user item in data)
                {
                    list.Add(new { id = item.id, text = item.username });
                }
            }
            writeJsonBack(list.ToJson());
        }

        /// <summary>
        /// 提交表单数据
        /// </summary>
        public void SubmitForm()
        {
            try
            {
                string keyValue = !string.IsNullOrEmpty(ctx.Request["keyValue"]) ? ctx.Request["keyValue"] : ctx.Request["id"];
                workflownodeoperator wfnodeoperator = HttpContextDataWrapper.DataToObject<workflownodeoperator>(ctx);
                IdalCommon.IworkflownodeoperatorEx.SubmitForm(wfnodeoperator, keyValue);
                Success("操作成功!");
            }
            catch (Exception)
            {
                Error("操作失败!");
            }
        }
        #endregion
    }
}

