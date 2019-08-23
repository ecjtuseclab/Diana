﻿using Diana.common.Util.Attributes;
using Diana.common.Util.Extension;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
///<summary>
///项目名称：Diana轻量级开发框架
///版本：0.0.1版
///开发团队成员：胡凯雨，张梦丽，艾美珍，易传佳，陈祚松，康文洋，张婷婷，王露，周庆，夏萍萍，陈兰兰
///模块和代码页功能描述：树形结构查询类
///最后修改时间：2018/1/22
/// </summary>
namespace Diana.common.Util.WebControl
{
    /// <summary>
    /// 树形结构查询
    /// </summary>
    public static class QueryTree
    {
        /// <summary>
        /// 树形查询条件
        /// </summary>
        /// <param name="entityList">数据源</param>
        /// <param name="condition">查询条件</param>
        /// <param name="primaryKey">主键</param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public static List<T> TreeWhere<T>(this List<T> entityList, Predicate<T> condition, string primaryKey, string parentId = "ParentId") where T : class
        {
            List<T> locateList = entityList.FindAll(condition);
            var parameter = Expression.Parameter(typeof(T), "t");
            //模糊查询表达式
            List<T> treeList = new List<T>();
            foreach (T entity in locateList)
            {
                //先把自己加入进来
                treeList.Add(entity);
                //向上查询
                string pId = entity.GetType().GetProperty(parentId).GetValue(entity, null).ToString();
                while (true)
                {
                    if (string.IsNullOrEmpty(pId) && pId == "0")
                    {
                        break;
                    }
                    Predicate<T> upLambda = (Expression.Equal(parameter.Property(primaryKey), Expression.Constant(pId))).ToLambda<Predicate<T>>(parameter).Compile();
                    T upRecord = entityList.Find(upLambda);
                    if (upRecord != null)
                    {
                        treeList.Add(upRecord);
                        pId = upRecord.GetType().GetProperty(parentId).GetValue(upRecord, null).ToString();
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return treeList.Distinct().ToList();
        }
        /// <summary>
        /// 树形查询条件
        /// </summary>
        /// <param name="entityList">数据源</param>
        /// <param name="condition">查询条件</param>
        /// <param name="primaryKey">主键</param>
        /// <returns></returns>
        public static DataTable TreeWhere(this DataTable table, string condition, string primaryKey)
        {
            DataRow[] drs = table.Select(condition);
            DataTable treeTable = table.Clone();
            foreach (DataRow dr in drs)
            {
                treeTable.ImportRow(dr);
                string pId = dr["ParentId"].ToString();
                while (true)
                {
                    if (string.IsNullOrEmpty(pId) && pId == "0")
                    {
                        break;
                    }
                    DataRow[] pdrs = table.Select(primaryKey + "='" + pId + "'");
                    if (pdrs.Length > 0)
                    {
                        treeTable.ImportRow(pdrs[0]);
                        pId = pdrs[0]["ParentId"].ToString();
                    }
                    else
                    {
                        break;
                    }
                }
            }
            DataView treeView = treeTable.DefaultView;
            return treeView.ToTable(true);
        }
    }
}