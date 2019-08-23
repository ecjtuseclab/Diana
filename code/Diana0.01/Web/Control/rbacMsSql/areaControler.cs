using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Diana.common;
using Diana.model;
using Ninject.Modules;
using Ninject;
using basedal;
using Web.Plugin;
using System.IO;
using Diana.common.Util;
using Diana.common.Util.WebControl;
/// <summary>
///项目名称：Diana轻量级开发框架
///版本：0.0.1版
///开发团队成员：胡凯雨，张梦丽，艾美珍，易传佳，陈祚松，康文洋，张婷婷，王露，周庆，夏萍萍，陈兰兰
///模块和代码页功能描述：区域控制器
///最后修改时间：2018/1/25
///修改日志：
///2018/1/25 修改view方法，获取当前控制器按钮资源(胡凯雨)
/// </summary>
namespace Web.Control
{
    public class areaControler : baseControler
    {
      
        public override void view()
        {
            getbuttonresource("area", 1);
            tp.Display(ctx, "/Html/area.html");
        }

        public void Form()
        {
            tp.Display(ctx, "/Html/layer/areaform_layer.html");
        }

        public void Details()
        {
            tp.Display(ctx, "/Html/layer/areadetail_layer.html");
        }
        /// <summary>
        /// 不需要验证的方法在构造方法添加
        /// </summary>
        public areaControler()
        {
           
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
                List<area> arealist = IdalCommon.IareaEx.getAreaList(keyword);

                /*************获取当前页需要显示的数据********************/
                var treeList = new List<TreeGridModel>();
                List<area> arlist = arealist.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

                foreach (area item in arlist)
                {
                    TreeGridModel treeModel = new TreeGridModel();
                    bool hasChildren = arealist.Count(t => t.parentid == item.id) == 0 ? false : true;
                    treeModel.id = item.id.ToString();
                    treeModel.isLeaf = hasChildren;
                    treeModel.parentId = item.parentid.ToString();
                    treeModel.expanded = hasChildren;
                    treeModel.entityJson = item.ToJson();
                    treeList.Add(treeModel);
                }

                List<DataTableTree> ret = new List<DataTableTree>();
                DataTableTree.AppendChildren(arealist, ref ret, 0, 0, a => a.id, a => a.parentid);
                var datalist = ret.Skip((pageIndex - 1) * pageSize).Take(pageSize);

                int records = arealist.Count;
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

        /// <summary>
        /// 获得一个tree型数据
        /// </summary>
        /// <returns></returns>
        public void GetTreeSelectJson()
        {
            List<area> data = IdalCommon.IareaEx.getEntityList();
            var treeList = new List<TreeSelectModel>();
            foreach (area item in data)
            {
                TreeSelectModel treeModel = new TreeSelectModel();
                treeModel.id = item.id.ToString();
                treeModel.text = item.fullname;
                treeModel.parentId = item.parentid.ToString();
                treeList.Add(treeModel);
            }
            writeJsonBack(treeList.TreeSelectJson());
        }

        /// <summary>
        /// 通过id获得数据
        /// </summary>
        /// <param name="keyValue">id的string型</param>
        /// <returns></returns>
        public void GetFormJson()
        {
            int id = Convert.ToInt32(ctx.Request["keyValue"]);
            var data = IdalCommon.IareaEx.getEntityById(id);
            writeJsonBack(data.ToJson());
        }

        public void SubmitForm()
        {
            try
            {
                string keyValue = ctx.Request["keyValue"];
                HttpRequest request = HttpContext.Current.Request;
                Stream stream = request.InputStream;
                StreamReader streamReader = new StreamReader(stream);
                string json = string.Empty;
                json = streamReader.ReadToEnd();
                json = HttpUtility.UrlDecode(json);
                string[] area = json.Split('&');
                area areaEntity = new area();
                areaEntity.parentid = Convert.ToInt32(area[0].Split('=')[1]);
                areaEntity.fullname = area[1].Split('=')[1];
                areaEntity.layers = Convert.ToInt32(area[2].Split('=')[1]);
                areaEntity.encode = area[3].Split('=')[1];
                areaEntity.id = Convert.ToInt32(areaEntity.encode);
                IdalCommon.IareaEx.SubmitForm(areaEntity, keyValue);
                Success("操作成功!");
            }
            catch (Exception)
            {
                Error("操作失败!");
            }
        }

        public void DeleteForm()
        {
            int id = Convert.ToInt32(ctx.Request["keyValue"]);
            if (IdalCommon.IareaEx.delete(id))
            {
                Success("操作成功!");
            }
            else
            {
                Error("操作失败!");
            }
        }
    }
}