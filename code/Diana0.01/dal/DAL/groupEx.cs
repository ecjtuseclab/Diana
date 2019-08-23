using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Diana.Idal;
using Diana.model;
using SqlSugar;
/// <summary>
///项目名称：Diana轻量级开发框架
///版本：0.0.1版
///开发团队成员：胡凯雨，张梦丽，艾美珍，易传佳，陈祚松，康文洋，张婷婷，王露，周庆，夏萍萍，陈兰兰
///模块和代码页功能描述：操作SQLServer数据库的分组表的方法
///最后修改时间：2018/1/26
/// </summary>
namespace Diana.dal
{

    public class groupEx : RepositoryBase<group>, IgroupEx
    {
        #region 1.基本操作
        /// <summary>
        /// 根据多个id组成的字符串删除多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool delete(string[] ids)
        {
            try
            {
                //user u = getUser(id);
                //string[] strids = ids.Split(',');
                List<group> u = db.Queryable<group>().In(a => a.id, ids).ToList();
                db.Delete<group>(u);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
       
        /// <summary>
        /// 根据id删除
        /// </summary>
        /// <param name="id">主键id</param>
        /// <returns></returns>
        public bool deleteById(int id)
        {
            try
            {
                db.Delete<group>(a => a.id == id);
                return true;
            }
            catch (Exception) { return false; }
        }
        /// <summary>
        /// 根据组名删除
        /// </summary>
        /// <param name="groupName">组名</param>
        /// <returns>bool</returns>
        public bool deleteByName(string groupName)
        {
            try
            {
                db.Delete<group>(a => a.groupname == groupName);
                return true;
            }
            catch (Exception) { return false; }
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
                db.Update<group>(dictionary, p => p.id == id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// 根据主键id更新
        /// </summary>
        /// <param name="id">group主键id</param>
        /// <returns></returns>
        public bool updateById(int id)
        {
            try
            {
                db.Update<group>(getGroup(id));
                return true;
            }
            catch (Exception) { return false; }

        }
        /// <summary>
        /// 根据组名更新
        /// </summary>
        /// <param name="groupName">组名</param>
        /// <returns>bool</returns>
        public bool updateByGroupName(string groupName)
        {
            try
            {
                db.Update<group>(getGroup(groupName));
                return true;
            }
            catch (Exception) { return false; }

        }
        /// <summary>
        /// 更新所有
        /// </summary>
        /// <returns></returns>
        public bool updateAll()
        {
            try
            {
                List<group> gList = getAllGroup();
                foreach (group item in gList)
                {
                    db.Update<group>(item);
                }
                return true;
            }
            catch (Exception) { return false; }

        }
        /// <summary>
        /// 动态根据字段名，字段值查询指定数据
        /// </summary>
        /// <param name="key">字段名</param>
        /// <param name="value">字段值</param>
        /// <returns>List<group></returns>
        public List<group> getDynamicGroup(string key, string value)
        {
            string sql = string.Format("select * from group where {0}={1}", key, value);
            List<group> glist = db.SqlQueryDynamic(sql);
            return glist;
        }
        /// <summary>
        /// 根据用户名获取的组
        /// </summary>
        /// <param name="groupName">组名</param>
        /// <returns></returns>
        public group getGroup(string groupName)
        {
            group table = db.Queryable<group>().Where(d => d.groupname == groupName).FirstOrDefault();
            return table;
        }
        /// <summary>
        /// 根据主键id获取组
        /// </summary>
        /// <param name="groupId">主键id</param>
        /// <returns></returns>
        public group getGroup(int groupId)
        {
            group table = db.Queryable<group>().Where(d => d.id == groupId).FirstOrDefault();
            return table;
        }

      
        /// <summary>
        /// 获取所有组
        /// </summary>
        /// <returns>List<group></returns>
        public List<group> getAllGroup()
        {
            try
            {
                List<group> table = db.Queryable<group>().ToList();
                return table;
            }
            catch (Exception) { return null; }
        }

        /// <summary>
        /// group表分页
        /// </summary>
        /// <param name="pageIndex">当前页的索引</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <returns></returns>
        public List<group> getPageGroup(int pageIndex, int pageSize)
        {
            int pageCount = 0;
            var page = db.Queryable<group>()
                .Where(c => c.id > 1).OrderBy(it => it.id)
                .ToPageList(pageIndex, pageSize, ref pageCount);//pageCount总条数
            return page;
        }
        /// <summary>
        /// 根据组名获取主键id
        /// </summary>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public int getGroupId(string groupName)
        {
            try
            {
                int id = 0;
                group table = db.Queryable<group>().Where(d => d.groupname == groupName).FirstOrDefault();
                if (table != null) id = table.id;
                return id;
            }
            catch (Exception) { return -1; }
        }
        /// <summary>
        /// 批量粘贴数据并插入数据库
        /// </summary>
        /// <param name="ids">（ids:1,2,3）</param>
        /// <returns></returns>
        public bool copypAndPaste(string ids) //复制粘贴资源
        {
            try
            {
                string[] id = ids.Split(',');
                group table = new group();
                for (int i = 0; i < id.Length; i++)
                {
                    table = getGroup(int.Parse(id[i]));
                    db.Insert<group>(table);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        /// <summary>
        /// 导出excel
        /// </summary>
        /// <param name="groupIds">字符串拼接的组ids，拼接格式（1,2,3）</param>
        /// <param name="cellHeaders">字符串拼接的字段名(id,groupName)</param>
        /// <param name="cellHeaderNames">字符串拼接的表单别名(ID,组名)</param>
        /// <returns>bool</returns>
        public bool excelExport(string groupIds, string cellHeaders, string cellHeaderNames)
        {
            //cellheader.Add(cellHeaderNames, cellHeaderNameArr);
            try
            {
                return true;
            }
            catch (Exception) { return false; }
        }
        /// <summary>
        /// excel导入
        /// </summary>
        public Dictionary<string, string> getCellHeaderName(string[] cellHeaderNames, string[] cellHeaderNameArr)
        {
            Dictionary<string, string> cellheader = new Dictionary<string, string>();
            //添加到字典
            for (int i = 0; i < cellHeaderNames.Length; i++)
            {
                for (int j = 0; j < cellHeaderNames.Length; j++)
                {
                    cellheader.Add(cellHeaderNames[i], cellHeaderNameArr[i]);
                }
            }
            return cellheader;
        }
        /// <summary>
        /// 空方法：后期完善
        /// </summary>
        /// <returns></returns>
        public bool excelImport()
        {
            try
            {

                return true;
            }
            catch (Exception) { return false; }
        }
        /// <summary>
        /// 空方法：后期完善
        /// </summary>
        /// <returns></returns>
        public bool pdfExport()
        {
            try
            {
                return true;
            }
            catch (Exception) { return false; }
        }
        /// <summary>
        /// 空方法：后期完善
        /// </summary>
        /// <returns></returns>
        public bool wordImport()
        {
            try
            {
                return true;
            }
            catch (Exception) { return false; }
        }
        /// <summary>
        /// 空方法：后期完善
        /// </summary>
        /// <returns></returns>
        public bool wordExport()
        {
            try
            {
                return true;
            }
            catch (Exception) { return false; }
        }
        #endregion

        public List<group> getGroupList(string groupname)
        {
            List<group> groupList = new List<group>();
            if (!string.IsNullOrEmpty(groupname))
            {
                groupList = getEntityList().Where(d => d.groupname == groupname).ToList();
            }
            else
            {
                groupList = getEntityList();
            }
            return groupList;
        }

        public void SubmitForm(group groupEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                int id = Convert.ToInt32(keyValue);
                groupEntity.id = id;
                db.Update<group>(groupEntity);
            }
            else
            {
                db.Insert<group>(groupEntity);
            }
        }
    }
    /// <summary>
    /// excel导入验证视图类
    /// </summary>
    public class groupExcelValidateView : group
    {
        private bool _isExcelVaildateOK = true;

        /// <summary>
        /// Excel验证是否通过，默认为true
        /// <para>true：通过；false：不通过</para>
        /// </summary>
        public bool IsExcelVaildateOK
        {
            get { return _isExcelVaildateOK; }
            set { _isExcelVaildateOK = value; }
        }
    }
}
