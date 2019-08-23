using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Diana.model;
/// <summary>
///项目名称：Diana轻量级开发框架
///版本：0.0.1版
///开发团队成员：胡凯雨，张梦丽，艾美珍，易传佳，陈祚松，康文洋，张婷婷，王露，周庆，夏萍萍，陈兰兰
///模块和代码页功能描述：文章接口，供IOC
///最后修改时间：2018/1/26
///日志说明：
/// </summary>
namespace Diana.Idal
{
    public interface IarticleEx : IRepositoryBase<article>
    {
        /// <summary>
        /// 差异更新
        /// </summary>
        /// <param name="id">跟新数据主码</param>
        /// <param name="dictionary">需要更新的数据</param>
        /// <returns>返回更新结果（true/false）</returns>
        bool update(int id, Dictionary<string, object> dictionary);
        /// <summary>
        /// 根据标题获得数据
        /// </summary>
        /// <param name="title">标题</param>
        /// <returns>一条数据</returns>
        article getArticle(string title);
        /// <summary>
        /// 根据数据id获得数据
        /// </summary>
        /// <param name="articleid">数据id</param>
        /// <returns>一条数据</returns>
        article getArticle(int articleid);
        /// <summary>
        /// 获得所有动作数据
        /// </summary>
        /// <returns>动作数据List</returns>
        List<article> getAllArticle();
        /// <summary>
        /// 根据标题获得数据id
        /// </summary>
        /// <param name="title">标题</param>
        /// <returns>数据id</returns>
        int getArticleId(string title);
        /// <summary>
        /// 根据类型获得数据
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>数据List</returns>
        List<article> getArticleList(int type);



        #region 基本操作
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="title">标题</param>
        /// <returns></returns>
        List<article> getArticleList(string title);

        /// <summary>
        /// 表单提交
        /// </summary>
        /// <param name="roleEntity">表单提交的一条角色数据</param>
        /// <param name="keyValue">主键</param>
        void SubmitForm(article a, string keyValue);
        #endregion
    }
}
