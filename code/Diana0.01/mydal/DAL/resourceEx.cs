using Diana.Idal;
using Diana.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlSugar;
/// <summary>
///项目名称：Diana轻量级开发框架
///版本：0.0.1版
///开发团队成员：胡凯雨，张梦丽，艾美珍，易传佳，陈祚松，康文洋，张婷婷，王露，周庆，夏萍萍，陈兰兰
///模块和代码页功能描述：操作Mysql数据库的资源表的方法
///最后修改时间：2018/1/26
///日志说明：
/// </summary>
namespace Diana.mydal
{

    public class resourceEx : RepositoryBase<resource>, IresourceEx
    {
        #region 1.基本操作

        /// <summary>
        /// 判断新增的资源是否合法，即资源名称不能重复
        /// </summary>
        /// <param name="resourcename">资源名称</param>
        /// <returns>合法：true;不合法：false</returns>
        public bool isLeagalResourcename(string resourcename)
        {
            bool flag = false;
            int count = db.Queryable<resource>().Where(d => d.resourcename == resourcename).Count();
            if (0 == count)
                flag = true;
            else
                flag = false;
            return flag;

        }

        /// <summary>
        /// 根据资源名称删除资源
        /// </summary>
        /// <param name="resourcename"></param>
        /// <returns>成功：true;失败:false</returns>
        public bool delete(string resourcename)
        {
            try
            {
                resource r = getResource(resourcename);
                db.Delete<resource>(r);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// 根据资源id删除资源
        /// </summary>
        /// <param name="resourceid">资源id</param>
        /// <returns>成功：true;失败:false</returns>
        public bool delResource(int resourceid)//根据资源id删除资源
        {
            try
            {
                resource r = getResource(resourceid);
                db.Delete<resource>(r);
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
                List<resource> u = db.Queryable<resource>().In(a => a.id, ids).ToList();
                db.Delete<resource>(u);
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
                db.Update<resource>(dictionary, p => p.id == id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// 根据资源父级获得子资源数据
        /// </summary>
        /// <param name="resourceowner">父级</param>
        /// <returns></returns>
        public List<resource> getResourceByOwner(string resourceowner)
        {
            try
            {
                List<resource> table = db.Queryable<resource>().Where(d => d.resourceowner == resourceowner).ToList();
                return table;
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// 根据资源的名称获得某资源
        /// </summary>
        /// <param name="resourcename">资源名称</param>
        /// <returns>一个资源对象</returns>
        public resource getResource(string resourcename)
        {
            resource table = db.Queryable<resource>().Where(d => d.resourcename == resourcename).FirstOrDefault();
            return table;
        }
        /// <summary>
        /// 根据资源的id获得资源对象
        /// </summary>
        /// <param name="resourceid">资源id</param>
        /// <returns>一个资源对象</returns>
        public resource getResource(int resourceid)
        {
            resource table = db.Queryable<resource>().Where(d => d.id == resourceid).FirstOrDefault();
            return table;
        }

        /// <summary>
        /// 获得所有的资源对象
        /// </summary>
        /// <returns>资源对象列表</returns>
        public List<resource> getAllResource()
        {
            List<resource> table = db.Queryable<resource>().ToList();
            return table;
        }

        /// <summary>
        /// 根据资源名称获得此资源对象的id
        /// </summary>
        /// <param name="resourcename">资源名称</param>
        /// <returns>资源id</returns>
        public int getResourceId(string resourcename)
        {
            int id = 0;
            resource table = db.Queryable<resource>().Where(d => d.resourcename == resourcename).FirstOrDefault();
            if (table != null) id = table.id;
            return id;
        }


        /// <summary>
        /// 通过资源名称获取若干条资源数据
        /// </summary>
        /// <param name="resourcename">资源名称</param>
        /// <returns>若干条资源数据</returns>
        public List<resource> getResourceList(string resourcename)
        {
            List<resource> resourcelist = new List<resource>();
            if (!string.IsNullOrEmpty(resourcename))
            {
                resourcelist = getResourceListByresourcename(resourcename, resourcelist);
            }
            else
            {
                resourcelist = getEntityList();
            }
            return resourcelist;
        }

        /// <summary>
        /// 获取对应资源名的数据以及它的父数据与相关数据
        /// </summary>
        /// <param name="resourcename">资源名</param>
        /// <param name="resourcelist">资源列表</param>
        /// <returns></returns>
        public List<resource> getResourceListByresourcename(string resourcename, List<resource> resourcelist)
        {
            List<resource> allresourcelist = getEntityList();
            resource reso = getSuperiorNode(resourcename);
            resourcelist.Add(reso);
            if (reso.resourceowner != "0")
                getResourceListByresourcename(reso.resourcename, resourcelist);
            return resourcelist;
        }

        /// <summary>
        /// 通过资源名获取资源的上级节点数据
        /// </summary>
        /// <param name="resourcename">资源名</param>
        /// <returns>资源的上级节点数据</returns>
        public resource getSuperiorNode(string resourcename)
        {
            resource re = getResource(resourcename);
            List<resource> allresourcelist = getEntityList();
            resource resource = allresourcelist.Where(r => r.id.ToString() == re.resourceowner).ToList().FirstOrDefault();
            return resource;
        }

        /// <summary>
        /// 表单提交
        /// </summary>
        /// <param name="resourceEntity">表单提交的一条资源数据</param>
        /// <param name="keyValue">资源主键</param>
        public void SubmitForm(resource resourceEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                int id = Convert.ToInt32(keyValue);
                resourceEntity.id = id;
                db.Update<resource>(resourceEntity);
            }
            else
            {
                if (getResource(resourceEntity.resourcename) == null)
                {
                    db.Insert<resource>(resourceEntity);
                }
            }

        }


        /// <summary>
        /// 根据控制器的名称获得某资源
        /// </summary>
        /// <param name="resourcename">控制器名称</param>
        /// <returns>一个资源对象</returns>
        public resource getResourcebycontrolername(string controlername,int type)
        {
            resource table = db.Queryable<resource>().Where(d => d.controlername == controlername).Where(c=>c.resourcetype==type).FirstOrDefault();
            return table;
        }

        /// <summary>
        /// 根据控制器的名称获得某资源
        /// </summary>
        /// <param name="controlername">控制器名称</param>
        /// <param name="resourceleval">资源级别</param>
        /// <returns>资源对象</returns>
        public List<resource> getResourcesbycontrolername(string controlername, int resourceleval)
        {
            List<resource> table = db.Queryable<resource>().Where(d => d.controlername == controlername).Where(c => c.resourceleval == resourceleval).ToList();
            return table;
        }


        #endregion
    }
}
