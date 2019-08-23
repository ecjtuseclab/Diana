using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Diana.model;
using Ninject.Modules;
using Ninject;
using basedal;
using Diana.Idal;
using System.IO;
using Diana.tool.Code;

/// <summary>
///项目名称：Diana轻量级开发框架
///版本：0.0.1版
///开发团队成员：胡凯雨，张梦丽，艾美珍，易传佳，陈祚松，康文洋，张婷婷，王露，周庆，夏萍萍，陈兰兰
///模块和代码页功能描述：动作控制器
///最后修改时间：2018/1/25
///修改日志：
///2018/1/25 修改view方法，获取当前控制器按钮资源(胡凯雨)
/// </summary>
namespace  Web.Control
{
    public class actionControler : baseControler
    {
     
        /// <summary>
        /// 根据的添加方法名，不需要再验证方法是否有权限访问
        /// </summary>
        public actionControler()//不需要验证的方法在构造方法添加
        {
            
        }

        /// <summary>
        /// 显示视图
        /// </summary>
        public override void view() //默认视图
        {
            getbuttonresource("action", 1);
            tp.Display(ctx, "/Html/action.html");
        }

        public void Form() //新增，修改视图
        {
            tp.Display(ctx, "/Html/layer/actionform_layer.html");
        }

        public void Details()//详情视图
        {
            tp.Display(ctx, "/Html/layer/actiondetail_layer.html");
        }

        #region 业务操作

        /// <summary>
        /// Excel导出
        /// </summary>
        public void excelExport()
        {
            string formdata = ctx.Request["formdata"];
            Dictionary<string, string> cellheader = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(formdata);
            List<action> enlist = IdalCommon.IactionEx.getAllAction();
            string urlPath = Diana.tool.Code.ExcelHelper.EntityListToExcel2003(cellheader, enlist, "动作信息表");//返回Excel的下载地址
            writeJsonBack(urlPath);
        }
        #endregion

        #region 基本操作 
        /// <summary>
        ///将得到的所有角色数据转为Json格式，传到前端
        /// </summary>
        public void showAll()
        {
            try
            {
                int pageSize = Convert.ToInt32(!string.IsNullOrEmpty(ctx.Request["rows"]) ? ctx.Request["rows"] : ctx.Request["pageSize"]);
                int pageIndex = Convert.ToInt32(!string.IsNullOrEmpty(ctx.Request["page"]) ? ctx.Request["page"] : ctx.Request["pageIndex"]);
                string keyword = ctx.Request["keyword"];
                int records = IdalCommon.IactionEx.getActionList(keyword).Count;//总记录数
                int totalPage = records % pageSize == 0 ? records / pageSize : records / pageSize + 1; //页数
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("actionname", keyword);
                List<action> actionlist = IdalCommon.IactionEx.getPaginationEntityList(pageIndex, pageSize, dictionary);
                var data = new
                {
                    rows = actionlist,
                    total = totalPage,
                    page = pageIndex,
                    TotalCount = records,
                    PageSize = pageSize,
                    CurrentPage = pageIndex,
                    TotalPage = totalPage,
                    DataList = actionlist
                };
                writeJsonBack(data.ToJson());
            }
            catch (Exception)
            {
                WriteBackHtml("", false);
            }
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        public void DeleteForm()
        {
            string keyValue = !string.IsNullOrEmpty(ctx.Request["keyValue"]) ? ctx.Request["keyValue"] : ctx.Request["id"];
            int id = Convert.ToInt32(keyValue);
            if (IdalCommon.IactionEx.delete(id))
            {
                Success("删除成功!");
            }
            else
            {
                Error("删除失败!");
            }
        }

        /// <summary>
        /// 复制粘贴
        /// </summary>
        public void copyAndPasteForm()
        {
            string keyValue = !string.IsNullOrEmpty(ctx.Request["keyValue"]) ? ctx.Request["keyValue"] : ctx.Request["id"];
            if (IdalCommon.IactionEx.copyAndPaste(keyValue))
            {
                Success("复制成功!");
            }
            else
            {
                Error("复制失败!");
            }
           
        }

        /// <summary>
        /// 获取表单数据
        /// </summary>
        public void GetFormJson()
        {
            string keyValue = ctx.Request["keyValue"];
            int id = Convert.ToInt32(keyValue);
            var data = IdalCommon.IactionEx.getEntityById(id);
            writeJsonBack(data.ToJson());
        }

        public void GetTreeSelectJson()
        {
            var data = IdalCommon.IactionEx.getEntityList().Where(p=>p.actionowner.ToInt()==0);
            var treeList = new List<TreeSelectModel>();

            ////最高节点
            //action highest = new action();
            //highest.id = 0;
            //highest.actionowner = Convert.ToString(0);
            //highest.actionname = "无上级";
            //highest.actiontype = 1;
            //highest.actioncode = 1;
            //highest.actionparam = "";
            //highest.actionurl = "";
            //highest.controlername = "";

            //TreeSelectModel HighesttreeModel = new TreeSelectModel();
            //HighesttreeModel.id = Convert.ToString(0);
            //HighesttreeModel.parentId = Convert.ToString(0);
            //HighesttreeModel.text = "无上级";
            //HighesttreeModel.data = highest;
            //treeList.Add(HighesttreeModel);

            foreach (action item in data)
            {
                TreeSelectModel treeModel = new TreeSelectModel();
                treeModel.id = item.id.ToString();
                treeModel.text = item.actionname;
                treeModel.parentId = item.actionowner;
                treeModel.data = item;
                treeList.Add(treeModel);
            }
           
            writeJsonBack(treeList.TreeSelectJson());
        }

        /// <summary>
        /// 提交表单数据
        /// </summary>
        public void SubmitForm()
        {
            try
            {
                string keyValue = !string.IsNullOrEmpty(ctx.Request["keyValue"]) ? ctx.Request["keyValue"] : ctx.Request["id"];
                action ac = HttpContextDataWrapper.DataToObject<action>(ctx);
                //if (ac.actiontype != 1)
                //{
                //    ac.actionowner=Convert.ToString(IdalCommon.IactionEx.getActionId(IdalCommon.IresourceEx.getEntityById(Convert.ToInt32(ac.actionowner)).resourcename));
                //}
                IdalCommon.IactionEx.SubmitForm(ac, keyValue);
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

