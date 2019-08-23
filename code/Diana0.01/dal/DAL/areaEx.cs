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
///模块和代码页功能描述：操作SQLServer数据库的区域表的方法
///最后修改时间：2018/1/26
/// </summary>
namespace Diana.dal
{
    public class areaEx : RepositoryBase<area>, IareaEx
    {
       
        /// <summary>
        /// 判断新增的区域是否合法，即区域名称不能重复
        /// </summary>
        /// <param name="areaname">区域名称</param>
        /// <returns>合法：true;不合法：false</returns>
        public bool isLeagalAreaname(string fullname)
        {
            bool flag = false;
            int count = db.Queryable<area>().Where(d => d.fullname == fullname).Count();
            if (0 == count)
                flag = true;
            else
                flag = false;
            return flag;
        }
       
        /// <summary>
        /// 根据区域名称删除区域
        /// </summary>
        /// <param name="fullname"></param>
        /// <returns>成功：true;失败:false</returns>
        public bool delete(string fullname)
        {
            try
            {
                area r = getArea(fullname);
                db.Delete<area>(r);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// 根据区域id删除区域
        /// </summary>
        /// <param name="areaid">区域id</param>
        /// <returns>成功：true;失败:false</returns>
        public bool delArea(int areaid)//根据区域id删除区域
        {
            try
            {
                area r = getArea(areaid);
                db.Delete<area>(r);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        /// <summary>
        /// 根据多个id组成的字符串删除多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool deleteList(string[] ids)
        {
            try
            {
                List<area> u = db.Queryable<area>().In(a => a.id, ids).ToList();
                db.Delete<area>(u);
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
                db.Update<area>(dictionary, p => p.id == id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// 根据父级获得区域数据
        /// </summary>
        /// <param name="parentid">父级</param>
        /// <returns>区域数据</returns>
        public List<area> getAreaByParentId(int parentid)
        {
            try
            {
                List<area> table = db.Queryable<area>().Where(d => d.parentid == parentid).ToList();
                return table;
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// 根据区域的名称获得某区域
        /// </summary>
        /// <param name="fullname">区域名称</param>
        /// <returns>一个区域对象</returns>
        public area getArea(string fullname)
        {
            area table = db.Queryable<area>().Where(d => d.fullname == fullname).FirstOrDefault();
            return table;
        }
        /// <summary>
        /// 根据区域的id获得区域对象
        /// </summary>
        /// <param name="areaid">区域id</param>
        /// <returns>一个区域对象</returns>
        public area getArea(int areaid)
        {
            area table = db.Queryable<area>().Where(d => d.id == areaid).FirstOrDefault();
            return table;
        }
        /// <summary>
        /// 根据区域的id获得区域对象
        /// </summary>
        /// <param name="areaid">区域id</param>
        /// <returns>一个区域对象</returns>
        public List<area> getParentArea()
        {
            List<area> table = db.Queryable<area>().Where(d => d.parentid == 0).ToList();
            return table;
        }

        /// <summary>
        /// 获得所有的区域对象
        /// </summary>
        /// <returns>区域对象列表</returns>
        public List<area> getAllArea()
        {
            List<area> table = db.Queryable<area>().ToList();
            return table;
        }

        /// <summary>
        /// 根据区域名称获得此区域对象的id
        /// </summary>
        /// <param name="fullname">区域名称</param>
        /// <returns>区域id</returns>
        public int getAreaId(string fullname)
        {
            int id = 0;
            area table = db.Queryable<area>().Where(d => d.fullname == fullname).FirstOrDefault();
            if (table != null) id = table.id;
            return id;
        }
       
        public List<area> getAreaList(string fullname)
        {
            List<area> arealist = new List<area>();
            if (!string.IsNullOrEmpty(fullname))
            {
                arealist = getAreaListByareaname(fullname, arealist);
            }
            else
            {
                arealist = getEntityList();
            }
            return arealist;
        }
        /// <summary>
        /// 获取对应区域的数据以及它的父数据与相关数据
        /// </summary>
        /// <param name="fullname">区域名</param>
        /// <param name="arealist">区域列表</param>
        /// <returns></returns>
        public List<area> getAreaListByareaname(string fullname, List<area> arealist)
        {
            List<area> allarealist = getEntityList();
            area ar = getSuperiorNode(fullname);
            arealist.Add(ar);
            if (ar.parentid != 0)
                getAreaListByareaname(ar.fullname, arealist);
            return arealist;
        }

        /// <summary>
        /// 根据区域名返回区域上级节点数据
        /// </summary>
        /// <param name="fullname">区域名</param>
        /// <returns>区域列表</returns>
        public area getSuperiorNode(string fullname)
        {
            area ar = getArea(fullname);
            List<area> allarealist = getEntityList();
            area area = allarealist.Where(a => a.id == a.parentid).ToList().FirstOrDefault();
            return area;
        }

        /// <summary>
        /// 表单提交
        /// </summary>
        /// <param name="resourceEntity">表单提交的一条资源数据</param>
        /// <param name="keyValue">资源主键</param>
        public void SubmitForm(area areaEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                int id = Convert.ToInt32(keyValue);
                areaEntity.id = id;
                db.Update<area>(areaEntity);
            }
            else
            {
                db.Insert<area>(areaEntity);
            }

        }
       

    }
}
