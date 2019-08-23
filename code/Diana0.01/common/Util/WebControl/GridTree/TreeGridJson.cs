using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
///<summary>
///项目名称：Diana轻量级开发框架
///版本：0.0.1版
///开发团队成员：胡凯雨，张梦丽，艾美珍，易传佳，陈祚松，康文洋，张婷婷，王露，周庆，夏萍萍，陈兰兰
///模块和代码页功能描述：构造树形表格Json类
///最后修改时间：2018/1/22
/// </summary>
namespace Diana.common.Util.WebControl
{
    /// <summary>
    /// 构造树形表格Json
    /// </summary>
    public static class TreeGridJson
    {
        public static int lft = 1, rgt = 1000000;
        /// <summary>
        /// 转换树形Json
        /// </summary>
        /// <param name="list">数据源</param>
        /// <param name="ParentId">父节点</param>
        /// <returns></returns>
        public static string TreeJson(List<TreeGridEntity> ListData, int index, string ParentId)
        {
            StringBuilder sb = new StringBuilder();
            var ChildNodeList = ListData.FindAll(t => t.parentId == ParentId);
            if (ChildNodeList.Count > 0) { index++; }
            foreach (TreeGridEntity entity in ChildNodeList)
            {
                string strJson = entity.entityJson;
                strJson = strJson.Insert(1, "\"level\":" + index + ",");
                strJson = strJson.Insert(1, "\"isLeaf\":" + (entity.hasChildren == true ? false : true).ToString().ToLower() + ",");
                strJson = strJson.Insert(1, "\"expanded\":" + (entity.expanded).ToString().ToLower() + ",");
                strJson = strJson.Insert(1, "\"lft\":" + lft++ + ",");
                strJson = strJson.Insert(1, "\"rgt\":" + rgt-- + ",");
                sb.Append(strJson);
                sb.Append(TreeJson(ListData, index, entity.id));
            }
            return sb.ToString().Replace("}{", "},{");
        }
        /// <summary>
        /// 转换树形Json
        /// </summary>
        /// <param name="list">数据源</param>
        /// <returns></returns>
        public static string TreeJson(this List<TreeGridEntity> ListData)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{ \"rows\": [");
            sb.Append(TreeJson(ListData, -1, "0"));
            sb.Append("]}");
            return sb.ToString();
        }
    }
}
