using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Diana.Idal;
using Diana.model;
using SqlSugar;
/// <summary>
///项目名称：Diana轻量级开发框架
///版本：0.0.1版
///开发团队成员：胡凯雨，张梦丽，艾美珍，易传佳，陈祚松，康文洋，张婷婷，王露，周庆，夏萍萍，陈兰兰
///模块和代码页功能描述：操作SQLServer数据库的字典表的方法
///最后修改时间：2018/1/26
/// </summary>
namespace Diana.dal
{
    public class propertyMappingEx : RepositoryBase<propertymapping>, IpropertyMappingEx
    {
        
        /// <summary>
        /// 根据多个id组成的字符串删除多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool deleteList(string[] ids)
        {
            try
            {
                List<propertymapping> u = db.Queryable<propertymapping>().In(a => a.id, ids).ToList();
                db.Delete<propertymapping>(u);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        
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
                db.Update<propertymapping>(dictionary, p => p.id == id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// 根据propertyMeaning获得数据
        /// </summary>
        /// <param name="propertyMeaning">propertyMeaning</param>
        /// <returns>一条数据</returns>
        public propertymapping getPropertyMapping(string propertyMeaning)
        {
            try
            {
                propertymapping table = db.Queryable<propertymapping>().Where(d => d.propertyMeaning == propertyMeaning).FirstOrDefault();
                return table;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 根据数据id获得动作数据
        /// </summary>
        /// <param name="actionid">数据id</param>
        /// <returns>一条数据</returns>
        public propertymapping getPropertyMapping(int propertymappingid)
        {
            try
            {
                propertymapping table = db.Queryable<propertymapping>().Where(d => d.id == propertymappingid).FirstOrDefault();
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
        public List<propertymapping> getAllPropertyMapping()
        {
            try
            {
                List<propertymapping> table = db.Queryable<propertymapping>().ToList();
                return table;
            }
            catch (Exception)
            {
                return null;
            }
        }
       
        /// <summary>
        /// 根据propertyMeaning获得数据id
        /// </summary>
        /// <param name="actionname">propertyMeaning</param>
        /// <returns>数据id</returns>
        public int getPropertyMappingId(string propertyMeaning)
        {
            try
            {
                int id = 0;
                propertymapping table = db.Queryable<propertymapping>().Where(d => d.propertyMeaning == propertyMeaning).FirstOrDefault();
                if (table != null) id = table.id;
                return id;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        /// <summary>
        /// 根据属性名称模糊查询
        /// </summary>
        /// <param name="propertyName">属性名称</param>
        /// <returns>数据字典数据</returns>
        public List<propertymapping> getPropertymappingList(string propertyName)
        {
            List<propertymapping> proList = new List<propertymapping>();
            if (!string.IsNullOrEmpty(propertyName))
            {
                proList = getEntityList().Where(d => d.propertyName.Contains(propertyName)).ToList();
            }
            else
            {
                proList = getEntityList();
            }
            return proList;
        }

        public void SubmitForm(propertymapping proEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                int id = Convert.ToInt32(keyValue);
                proEntity.id = id;
                db.Update<propertymapping>(proEntity);
            }
            else
            {
                db.Insert<propertymapping>(proEntity);
            }
        }
    }
}
