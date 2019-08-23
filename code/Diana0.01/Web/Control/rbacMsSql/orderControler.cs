using basedal;
using Diana.common.Util;
using Diana.common.Util.WebControl;
using Diana.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
/// <summary>
///项目名称：Diana轻量级开发框架
///版本：0.0.1版
///开发团队成员：胡凯雨，张梦丽，艾美珍，易传佳，陈祚松，康文洋，张婷婷，王露，周庆，夏萍萍，陈兰兰
///模块和代码页功能描述：用户控制器
///更新时间：2018/1/25
/// </summary>
namespace Web.Control
{
    public class orderControler:baseControler
    {
        /// <summary>
        /// 构造函数，不需要验证的方法
        /// </summary>
        public orderControler()
        {
           
        }
        /// <summary>
        /// 默认视图
        /// </summary>
        public override void view()
        {
            tp.Put("buttonlist", accctx.Buttons);
            tp.Display(ctx, "/Html/order.html");
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
                int records = IdalCommon.IorderEx.getOrderList(keyword).Count;//总记录数
                int totalPage = records % pageSize == 0 ? records / pageSize : records / pageSize + 1; //页数
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("OrderNO", keyword);
                List<order> orderlist = IdalCommon.IorderEx.getPaginationEntityList(pageIndex, pageSize, dictionary);
                var data = new
                {
                    rows = orderlist,
                    total = totalPage,
                    page = pageIndex,
                    TotalCount = records,
                    PageSize = pageSize,
                    CurrentPage = pageIndex,
                    TotalPage = totalPage,
                    DataList = orderlist
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
            IdalCommon.IorderEx.delete(id);
            Success("删除成功!");
        }

        /// <summary>
        /// 复制粘贴
        /// </summary>
        public void copyAndPasteForm()
        {
            string keyValue = !string.IsNullOrEmpty(ctx.Request["keyValue"]) ? ctx.Request["keyValue"] : ctx.Request["id"];
            IdalCommon.IorderEx.copyAndPaste(keyValue);
            Success("复制成功!");
        }

        /// <summary>
        /// 获取表单数据
        /// </summary>
        public void GetFormJson()
        {
            string keyValue = ctx.Request["keyValue"];
            int id = Convert.ToInt32(keyValue);
            var data = IdalCommon.IorderEx.getEntityById(id);
            writeJsonBack(data.ToJson());
        }

        /// <summary>
        /// 提交表单数据
        /// </summary>
        public void SubmitForm()
        {
            string keyValue = !string.IsNullOrEmpty(ctx.Request["keyValue"]) ? ctx.Request["keyValue"] : ctx.Request["id"];
            order or = HttpContextDataWrapper.DataToObject<order>(ctx);
            IdalCommon.IorderEx.SubmitForm(or, keyValue);
            Success("操作成功!");
        }
        #endregion

        #region 扩展方法
        /// <summary>
        /// 获得报表数据
        /// </summary>
        public void getReportForm()
        { 
           //string textfield=ctx.Request["text"];
           //string valuefield = ctx.Request["value"];
           
        }
        #endregion
    }
}