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

namespace Web.Control
{
    /// <summary>
    ///项目名称：Diana轻量级开发框架
    ///版本：0.0.1版
    ///开发团队成员：胡凯雨，张梦丽，艾美珍，易传佳，陈祚松，康文洋，张婷婷，王露，周庆，夏萍萍，陈兰兰
    ///模块和代码页功能描述：文章控制器
    ///最后修改时间：2018/1/25
    ///修改日志：
    /// <summary>
    public class articleControler: baseControler
    {
        /// <summary>
        /// 根据的添加方法名，不需要再验证方法是否有权限访问
        /// </summary>
        public articleControler()//不需要验证的方法在构造方法添加
        {
            noauth_actions.Add("view");
            noauth_actions.Add("Form");
            noauth_actions.Add("Details");
            noauth_actions.Add("GetFormJson");
            noauth_actions.Add("ACE_GetFormJson");
            noauth_actions.Add("SubmitForm");
            noauth_actions.Add("GetTreeSelectJson");
            noauth_actions.Add("DeleteForm");
            noauth_actions.Add("copyAndPasteForm");
            noauth_actions.Add("Details");
        }
        /// <summary>
        /// 显示视图
        /// </summary>
        public override void view() //默认视图
        {
            tp.Display(ctx, "/Html/article.html");
        }

        public void Form() //新增，修改视图
        {
            List<propertymapping> pros = IocModule.GetEntity<IpropertyMappingEx>().getEntityList();
            List<propertymapping> types = pros.Where(p => p.propertyName == "type" && p.parentId != 0).ToList();
            tp.Put("types", types);
            tp.Display(ctx, "/Html/layer/articleform_layer.html");
        }

        public void Details()//详情视图
        {
            List<propertymapping> pros = IocModule.GetEntity<IpropertyMappingEx>().getEntityList();
            List<propertymapping> types = pros.Where(p => p.propertyName == "type" && p.parentId != 0).ToList();
            tp.Put("types", types);
            tp.Display(ctx, "/Html/layer/articledetail_layer.html");
        }


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
                int records = IdalCommon.IarticleEx.getArticleList(keyword).Count;//总记录数
                int totalPage = records % pageSize == 0 ? records / pageSize : records / pageSize + 1; //页数
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("title", keyword);
                List<article> articlelist = IdalCommon.IarticleEx.getPaginationEntityList(pageIndex, pageSize, dictionary);
                var data = new
                {
                    rows = articlelist,
                    total = totalPage,
                    page = pageIndex,
                    TotalCount = records,
                    PageSize = pageSize,
                    CurrentPage = pageIndex,
                    TotalPage = totalPage,
                    DataList = articlelist
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
            if (IdalCommon.IarticleEx.delete(id))
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
            if (IdalCommon.IarticleEx.copyAndPaste(keyValue))
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
            var data = IdalCommon.IarticleEx.getEntityById(id);
            writeJsonBack(data.ToJson());
        }

        /// <summary>
        /// 提交表单数据
        /// </summary>
        public void SubmitForm()
        {
            try
            {
                string keyValue = !string.IsNullOrEmpty(ctx.Request["keyValue"]) ? ctx.Request["keyValue"] : ctx.Request["id"];
                article ac = HttpContextDataWrapper.DataToObject<article>(ctx);
                IdalCommon.IarticleEx.SubmitForm(ac, keyValue);
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