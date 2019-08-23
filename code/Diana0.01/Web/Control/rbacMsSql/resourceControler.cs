using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Diana.model;
using Diana.common;
using Diana.common.Util;
using Diana.common.Util.WebControl;
using Ninject.Modules;
using Ninject;
using basedal;
using System.IO;
using System.Text;
/// <summary>
///项目名称：Diana轻量级开发框架
///版本：0.0.1版
///开发团队成员：胡凯雨，张梦丽，艾美珍，易传佳，陈祚松，康文洋，张婷婷，王露，周庆，夏萍萍，陈兰兰
///模块和代码页功能描述：资源控制器
///最后修改时间：2018/1/25
///修改日志：
///2018/1/25 修改view方法，获取当前控制器按钮资源(胡凯雨)
/// </summary>
namespace Web.Control
{
    public class resourceControler : baseControler
    {
        public override void view() //默认视图
        {
            getbuttonresource("resource", 1);
            tp.Display(ctx, "/Html/resource.html");
        }


        /// <summary>
        /// 查看角色详情
        /// </summary>
        /// <returns></returns>
        public void Details()
        {

            tp.Display(ctx, "/Html/layer/resourcedetail_layer.html");
        }


        /// <summary>
        /// 不需要验证的方法在构造方法添加
        /// </summary>
        public resourceControler()
        {
            noauth_actions.Add("GetTreeSelectJson");//删除
            noauth_actions.Add("GetResourceListSelectOption");
            
        }

        public void Form()
        {
            tp.Display(ctx, "/Html/layer/resourceform_layer.html");
        }


        /// <summary>
        /// 数据提交
        /// </summary>
        /// <param name="resource">资源对象</param>
        /// <param name="keyValue">对象id</param>
        /// <returns></returns>
        public void SubmitForm()
        {
            try
            {
                string keyValue = !string.IsNullOrEmpty(ctx.Request["keyValue"]) ? ctx.Request["keyValue"] : ctx.Request["id"];
                resource re = HttpContextDataWrapper.DataToObject<resource>(ctx);
                IdalCommon.IresourceEx.SubmitForm(re, keyValue);
                Success("操作成功!");
            }
            catch (Exception)
            {
                Error("操作失败!");
            }
        }

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="keyValue">对象id</param>
        /// <returns></returns>
        public void DeleteForm()
        {
            try
            {
                string keyValue = !string.IsNullOrEmpty(ctx.Request["keyValue"]) ? ctx.Request["keyValue"] : ctx.Request["id"];
                int key = Convert.ToInt32(keyValue);
                IdalCommon.IresourceEx.delete(key);
                Success("删除成功!");
            }
            catch (Exception)
            {
                Error("删除失败!");
            }
        }
        
        /// <summary>
        /// 通过id获得数据
        /// </summary>
        /// <param name="keyValue">id的string型</param>
        /// <returns></returns>
        public void GetFormJson()
        {
            try
            {
                string keyValue = ctx.Request["keyValue"];
                int id = Convert.ToInt32(keyValue);
                var data = IdalCommon.IresourceEx.getResource(id);
                writeJsonBack(data.ToJson());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        /// <summary>
        /// 复制粘贴
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public void copyAndPasteForm()
        {
            try
            {
                string keyValue = !string.IsNullOrEmpty(ctx.Request["keyValue"]) ? ctx.Request["keyValue"] : ctx.Request["id"];
                IdalCommon.IresourceEx.copyAndPaste(keyValue);
                Success("粘贴成功!");
            }
            catch (Exception)
            {
                Error("粘贴失败!");
            }
        }

        /// <summary>
        /// 显示所有数据
        /// </summary>
        public void showAll()
        {
            try
            {
                //每行显示的数据条数
                int pageSize = Convert.ToInt32(!string.IsNullOrEmpty(ctx.Request["rows"]) ? ctx.Request["rows"] : ctx.Request["pageSize"]);
                //当前页码
                int pageIndex = Convert.ToInt32(!string.IsNullOrEmpty(ctx.Request["page"]) ? ctx.Request["page"] : ctx.Request["pageIndex"]);
                //查询关键字
                string keyword = ctx.Request["keyword"];
                //根据关键字查询获取的数据
                List<resource> resourcelist = IdalCommon.IresourceEx.getResourceList(keyword);

                /*************获取当前页需要显示的数据********************/
                var treeList = new List<TreeGridModel>();
                List<resource> rlist = resourcelist.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

                foreach (resource item in rlist)
                {
                    TreeGridModel treeModel = new TreeGridModel();
                    bool hasChildren = resourcelist.Count(t => t.resourceowner == item.id.ToString()) == 0 ? false : true;
                    treeModel.id = item.id.ToString();
                    treeModel.isLeaf = hasChildren;
                    treeModel.parentId = item.resourceowner;
                    treeModel.expanded = hasChildren;
                    treeModel.entityJson = item.ToJson();
                    treeList.Add(treeModel);
                }

                List<DataTableTree> ret = new List<DataTableTree>();
                DataTableTree.AppendChildren(resourcelist, ref ret, "0", 0, a => a.id.ToString(), a => a.resourceowner);
                var datalist = ret.Skip((pageIndex - 1) * pageSize).Take(pageSize);

                int records = resourcelist.Count;
                int totalPage = records % pageSize == 0 ? records / pageSize : records / pageSize + 1; //页数
                var data = new
                {
                    total = totalPage,
                    page = pageIndex,
                    TotalCount = records,
                    PageSize = pageSize,
                    CurrentPage = pageIndex,
                    TotalPage = totalPage,
                    DataList = datalist
                };
                var json = treeList.TreeGridJson().Substring(0, treeList.TreeGridJson().Length - 1) + "," + data.ToJson().Substring(1);
                writeJsonBack(json);
               
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void GetTreeSelectJson()
        {
            var data = IdalCommon.IresourceEx.getEntityList();
            var treeList = new List<TreeSelectModel>();
            foreach (resource item in data)
            {
                TreeSelectModel treeModel = new TreeSelectModel();
                treeModel.id = item.id.ToString();
                treeModel.text = item.resourcename;
                treeModel.parentId = item.resourceowner;
                treeList.Add(treeModel);
            }
            writeJsonBack(treeList.TreeSelectJson());
        }

        public void GetResourceListSelectOption()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            Dictionary<string, string> dic = new Dictionary<string, string>();
            var data = IdalCommon.IresourceEx.getEntityList();
            foreach (var item in data)
            {
                sb.Append("{\"Value\":\"" + item.id + "\",\"Text\":\"" + item.resourcename + "\"},");
            }
            sb.Remove(sb.Length - 1, 1);
            sb.Append("]");
            writeJsonBack(sb.ToString());
        }
    }
}

