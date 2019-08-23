using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
///<summary>
///项目名称：Diana轻量级开发框架
///版本：0.0.1版
///开发团队成员：胡凯雨，张梦丽，艾美珍，易传佳，陈祚松，康文洋，张婷婷，王露，周庆，夏萍萍，陈兰兰
///模块和代码页功能描述：树形表格操作类
///最后修改时间：2018/1/22
/// </summary>
namespace Diana.common.Util.WebControl
{
    public class DataTableTree
    {
        public string id { get; set; }
        public string ParentId { get; set; }
        public bool HasChildren { get; set; }
        public int Level { get; set; }
        public object Data { get; set; }


        public static void AppendChildren<TData>(List<TData> source, ref List<DataTableTree> ret, string parentId, int nodeLevel, Func<TData, string> idSelector, Func<TData, string> parentIdSelector, Func<TData, int?> sortCodeSelector = null)
        {
            var entities = source.Where(a => parentIdSelector(a) == parentId);
            if (sortCodeSelector != null)
            {
                entities = entities.OrderBy(a => sortCodeSelector(a));
            }
            
            foreach (var entity in entities)
            {
                DataTableTree tree = new DataTableTree();
                tree.id = idSelector(entity);
                tree.ParentId = parentId;
                tree.HasChildren = source.Any(a => parentIdSelector(a) == idSelector(entity));
                tree.Level = nodeLevel;
                tree.Data = entity;
                ret.Add(tree);
                AppendChildren(source, ref ret, idSelector(entity), nodeLevel + 1, idSelector, parentIdSelector, sortCodeSelector);
            }
        }

        //当ParentID为int类型时  YCJ
        public static void AppendChildren<TData>(List<TData> source, ref List<DataTableTree> ret, int parentId, int nodeLevel, Func<TData, int> idSelector, Func<TData, int> parentIdSelector, Func<TData, int?> sortCodeSelector = null)
        {
            var entities = source.Where(a => parentIdSelector(a) == parentId);
            if (sortCodeSelector != null)
            {
                entities = entities.OrderBy(a => sortCodeSelector(a));
            }

            foreach (var entity in entities)
            {
                DataTableTree tree = new DataTableTree();
                tree.id = idSelector(entity).ToString();
                tree.ParentId = parentId.ToString();
                tree.HasChildren = source.Any(a => parentIdSelector(a) == idSelector(entity));
                tree.Level = nodeLevel;
                tree.Data = entity;
                ret.Add(tree);
                int id= idSelector(entity);
                AppendChildren(source, ref ret, id, nodeLevel + 1, idSelector, parentIdSelector, sortCodeSelector);
            }
        }
    }
}