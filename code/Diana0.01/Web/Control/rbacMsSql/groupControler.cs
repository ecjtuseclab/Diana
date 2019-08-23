using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Diana.dal;
using Diana.model;
using Diana.common;
using Diana.common.Util;
using Diana.bll;
using Web.Control;
using System.Text;
using System.Data;
using System.Reflection;
using Diana.Idal;
using Ninject.Modules;
using Ninject;
using basedal;
using System.IO;
/// <summary>
///项目名称：Diana轻量级开发框架
///版本：0.0.1版
///开发团队成员：胡凯雨，张梦丽，艾美珍，易传佳，陈祚松，康文洋，张婷婷，王露，周庆，夏萍萍，陈兰兰
///模块和代码页功能描述：分组控制器
///最后修改时间：2018/1/25
///修改日志：
///2018/1/25 修改view方法，获取当前控制器按钮资源(胡凯雨)
/// </summary>
namespace Web.Control
{
    
    public class groupControler : baseControler
    {       
        /// <summary>
        /// 构造函数，不需要验证的方法
        /// </summary>
        public groupControler()
        {
           
        }
        /// <summary>
        /// 默认视图
        /// </summary>
        public override void view()
        {
            getbuttonresource("group", 1);
            tp.Display(ctx, "/Html/group.html");
        }

        public void Form()
        {
            tp.Display(ctx, "/Html/layer/groupform_layer.html");
        }

        public void Details()
        {
            tp.Display(ctx, "/Html/layer/groupdetail_layer.html");
        }

        /// <summary>
        ///将得到的所有角色数据转为Json格式，传到前端
        /// </summary>
        public void showAll()
        {
            int pageSize = Convert.ToInt32(!string.IsNullOrEmpty(ctx.Request["rows"]) ? ctx.Request["rows"] : ctx.Request["pageSize"]);
            int pageIndex = Convert.ToInt32(!string.IsNullOrEmpty(ctx.Request["page"]) ? ctx.Request["page"] : ctx.Request["pageIndex"]);
            string keyword = ctx.Request["keyword"];
            int records = IdalCommon.IgroupEx.getGroupList(keyword).Count;
            int totalPage = records % pageSize == 0 ? records / pageSize : records / pageSize + 1; //页数
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("groupname", keyword);
            List<group> grouplist = IdalCommon.IgroupEx.getPaginationEntityList(pageIndex, pageSize, dictionary);
            var data = new
            {
                rows = grouplist,
                total = totalPage,
                page = pageIndex,
                TotalCount = records,
                PageSize = pageSize,
                CurrentPage = pageIndex,
                TotalPage = totalPage,
                DataList = grouplist
            };
            writeJsonBack(data.ToJson());
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        public void DeleteForm()
        {
            string keyValue = !string.IsNullOrEmpty(ctx.Request["keyValue"]) ? ctx.Request["keyValue"] : ctx.Request["id"];
            int id = Convert.ToInt32(keyValue);
            if (IdalCommon.IgroupEx.delete(id))
            {
                Success("删除成功!");
            }
            else
            {
                Error("删除失败!");
            }
        }
        public void copyAndPasteForm()
        {
            string keyValue = !string.IsNullOrEmpty(ctx.Request["keyValue"]) ? ctx.Request["keyValue"] : ctx.Request["id"];
            if (IdalCommon.IgroupEx.copyAndPaste(keyValue))
            {
                Success("复制成功!");
            }
            else
            {
                Error("删除失败!");
            }
        }
        /// <summary>
        /// 获取表单数据
        /// </summary>
        public void GetFormJson()
        {
            string keyValue = ctx.Request["keyValue"];
            int id = Convert.ToInt32(keyValue);
            var data = IdalCommon.IgroupEx.getEntityById(id);
            writeJsonBack(data.ToJson());
        }

        public void SubmitForm()
        {
            try
            {
                string keyValue = !string.IsNullOrEmpty(ctx.Request["keyValue"]) ? ctx.Request["keyValue"] : ctx.Request["id"];
                group gr = HttpContextDataWrapper.DataToObject<group>(ctx);
                IdalCommon.IgroupEx.SubmitForm(gr, keyValue);
                Success("操作成功!");
            }
            catch (Exception)
            {
                 Error("操作失败!");
            }
        }

    }
}

