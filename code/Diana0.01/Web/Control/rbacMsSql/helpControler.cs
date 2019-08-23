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
///模块和代码页功能描述：帮助文档控制器
///最后修改时间：2018/1/25
/// </summary>
namespace Web.Control
{
    public class helpControler : baseControler
    {
        /// <summary>
        /// 根据的添加方法名，不需要再验证方法是否有权限访问
        /// </summary>
        public helpControler()//不需要验证的方法在构造方法添加
        {
            noauth_actions.Add("view");
            noauth_actions.Add("List");
            noauth_actions.Add("Details");
            noauth_actions.Add("GetFormJson");
            noauth_actions.Add("showTypeAll");
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

            int type = Convert.ToInt32(ctx.Request["type"]);
            List<article> article = IdalCommon.IarticleEx.getArticleList(type).ToList();
            List<propertymapping> pm = IdalCommon.IpropertyMappingEx.getAllPropertyMapping().ToList();
            propertymapping p = pm.Where(d => d.propertyName == "type" && d.propertyValue == type.ToString()).FirstOrDefault();
            string content = article[0].content;
            string m = p.propertyMeaning;
            tp.Put("content", content);
            tp.Put("title", m);
            tp.Display(ctx, "/Html/help.html");
        }

        public void List()//列表视图
        {
            int type = Convert.ToInt32(ctx.Request["type"]);
            List<propertymapping> pm = IdalCommon.IpropertyMappingEx.getAllPropertyMapping().ToList();
            propertymapping p = pm.Where(d => d.propertyName == "type" && d.propertyValue == type.ToString()).FirstOrDefault();
            string m = p.propertyMeaning;
            tp.Put("title", m);
            tp.Display(ctx, "/Html/layer/helplist_layer.html");
        }
        public void Details()//列表视图
        {
            int id = Convert.ToInt32(ctx.Request["articleid"]);
            article article = IdalCommon.IarticleEx.getArticle(id);
            string content = article.content;

            int type = (int)article.type;
            List<propertymapping> pm = IdalCommon.IpropertyMappingEx.getAllPropertyMapping().ToList();
            propertymapping p = pm.Where(d => d.propertyName == "type" && d.propertyValue == type.ToString()).FirstOrDefault();
            string m = p.propertyMeaning;
            tp.Put("title", m);
            tp.Put("content", content);
            tp.Display(ctx, "/Html/help.html");
        }


        /// <summary>
        ///将得到的某个类型的所有数据转为Json格式，传到前端
        /// </summary>
        public void showTypeAll()
        {
            try
            {
                
                int type = Convert.ToInt32(ctx.Request["type"]);
                int pageSize = Convert.ToInt32(!string.IsNullOrEmpty(ctx.Request["rows"]) ? ctx.Request["rows"] : ctx.Request["pageSize"]);
                int pageIndex = Convert.ToInt32(!string.IsNullOrEmpty(ctx.Request["page"]) ? ctx.Request["page"] : ctx.Request["pageIndex"]);
                string keyword = ctx.Request["keyword"];
                int records = IdalCommon.IarticleEx.getArticleList(keyword).Count;//总记录数
                int totalPage = records % pageSize == 0 ? records / pageSize : records / pageSize + 1; //页数
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("title", keyword);
                List<article> articlelist = IdalCommon.IarticleEx.getPaginationEntityList(pageIndex, pageSize, dictionary);
                List<article> list = articlelist.Where(p => p.type == type).ToList();
                var data = new
                {
                    rows = list,
                    total = totalPage,
                    page = pageIndex,
                    TotalCount = records,
                    PageSize = pageSize,
                    CurrentPage = pageIndex,
                    TotalPage = totalPage,
                    DataList = list
                };
                writeJsonBack(data.ToJson());
            }
            catch (Exception)
            {
                WriteBackHtml("", false);
            }
        }


        /// <summary>
        ///将得到的某个类型的所有数据转为Json格式，传到前端
        /// </summary>
        public void GetFormJson()
        {
            try
            {

                int type = Convert.ToInt32(!string.IsNullOrEmpty(ctx.Request["type"]) ? ctx.Request["type"] : null);
                int id = Convert.ToInt32(!string.IsNullOrEmpty(ctx.Request["articleid"]) ? ctx.Request["articleid"] : null);
                //int type = Convert.ToInt32(ctx.Request["type"]);
                //int id = Convert.ToInt32(ctx.Request["articleid"]);
                article article = new article();
                if (type != 0)
                {
                    List<article> articlelist = IdalCommon.IarticleEx.getArticleList(type).ToList();
                    if (articlelist.Count == 1)
                    {
                        article = articlelist[0];
                    }
                }
                else if (id != 0)
                {
                    article = IdalCommon.IarticleEx.getArticle(id);
                    string content = article.content;
                }
                writeJsonBack(article.ToJson());
            }
            catch (Exception)
            {
                WriteBackHtml("", false);
            }
        }
    }
}