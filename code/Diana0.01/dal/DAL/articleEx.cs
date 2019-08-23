using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Diana.model;
using SqlSugar;
using Diana.Idal;
/// <summary>
///项目名称：Diana轻量级开发框架
///版本：0.0.1版
///开发团队成员：胡凯雨，张梦丽，艾美珍，易传佳，陈祚松，康文洋，张婷婷，王露，周庆，夏萍萍，陈兰兰
///模块和代码页功能描述：操作SQLServer数据库的文章表的方法
///最后修改时间：2018/1/26
/// </summary>
namespace Diana.dal
{
    public class articleEx : RepositoryBase<article>, IarticleEx
    {
        #region 1.基础操作

        /// <summary>
        /// 差异更新
        /// </summary>
        /// <param name="id">跟新数据主码</param>
        /// <param name="dictionary">需要更新的数据</param>
        /// <returns>返回更新结果（true/false）</returns>
        public bool update(int id, Dictionary<string, object> dictionary)
        {
            try
            {
                db.Update<article>(dictionary, p => p.id == id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        /// <summary>
        /// 根据标题获得数据
        /// </summary>
        /// <param name="title">标题</param>
        /// <returns>一条数据</returns>
        public article getArticle(string title)
        {
            try
            {
                article table = db.Queryable<article>().Where(d => d.title == title).FirstOrDefault();
                return table;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 根据数据id获得数据
        /// </summary>
        /// <param name="articleid">数据id</param>
        /// <returns>一条数据</returns>
        public article getArticle(int articleid)
        {
            try
            {
                article table = db.Queryable<article>().Where(d => d.id == articleid).FirstOrDefault();
                return table;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 获得所有数据
        /// </summary>
        /// <returns>数据List</returns>
        public List<article> getAllArticle()
        {
            try
            {
                List<article> table = db.Queryable<article>().ToList();
                return table;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 根据标题获得数据id
        /// </summary>
        /// <param name="title">标题</param>
        /// <returns>数据id</returns>
        public int getArticleId(string title)
        {
            try
            {
                int id = 0;
                article table = db.Queryable<article>().Where(d => d.title == title).FirstOrDefault();
                if (table != null) id = table.id;
                return id;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        /// <summary>
        /// 根据类型获得数据
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>数据List</returns>
        public List<article> getArticleList(int type)
        {
            try
            {
                List<article> table = db.Queryable<article>().Where(d => d.type == type).ToList();
                return table;
            }
            catch (Exception)
            {
                return null;
            }
        }


        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="title">标题</param>
        /// <returns></returns>
        public List<article> getArticleList(string title)
        {
            try
            {
                List<article> articleList = new List<article>();
                if (!string.IsNullOrEmpty(title))
                {
                    articleList = getEntityList().Where(d => d.title == title).ToList();
                }
                else
                {
                    articleList = getEntityList();
                }
                return articleList;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 表单提交
        /// </summary>
        /// <param name="roleEntity">表单提交的一条角色数据</param>
        /// <param name="keyValue">主键</param>
        public void SubmitForm(article articleEntity, string keyValue)
        {
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    int id = Convert.ToInt32(keyValue);
                    articleEntity.id = id;
                    articleEntity.edittime = Convert.ToDateTime(DateTime.Now.ToString());
                    articleEntity.inserttime = Convert.ToDateTime(DateTime.Now.ToString());
                    db.Update<article>(articleEntity);
                }
                else
                {
                    articleEntity.inserttime = Convert.ToDateTime(DateTime.Now.ToString());
                    articleEntity.edittime = Convert.ToDateTime(DateTime.Now.ToString());
                    db.Insert<article>(articleEntity);
                }
            }
            catch (Exception)
            {
            }
        }
        #endregion
    }
}
