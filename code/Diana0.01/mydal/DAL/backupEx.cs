using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Diana.model;
using MySqlSugar;
using Diana.Idal;
using System.IO;
using Diana.model.Model;

/// <summary>
///项目名称：Diana轻量级开发框架
///版本：0.0.1版
///开发团队成员：胡凯雨，张梦丽，艾美珍，易传佳，陈祚松，康文洋，张婷婷，王露，周庆，夏萍萍，陈兰兰
///模块和代码页功能描述：操作Mysql数据库的数据备份表的方法
///最后修改时间：2018/1/26
///日志说明：
/// </summary>
namespace Diana.mydal
{
    public class backupEx : RepositoryBase<backup>, IbackupEx
    {
        #region 1.基本操作
        /// <summary>
        /// 判断新增的备份文件是否合法，即备份文件名称不能重复
        /// </summary>
        /// <param name="backupname">备份文件名称</param>
        /// <returns>合法：true;不合法：false</returns>
        public bool isLeagalBackupname(string backupname)
        {
            bool flag = false;
            int count = db.Queryable<backup>().Where(d => d.backupname == backupname).Count();
            if (0 == count)
                flag = true;
            else
                flag = false;
            return flag;
        }


        /// <summary>
        /// 根据备份文件名称删除备份文件
        /// </summary>
        /// <param name="backupname"></param>
        /// <returns>成功：true;失败:false</returns>
        public bool delete(string backupname)
        {
            try
            {
                backup r = getBackup(backupname);
                db.Delete<backup>(r);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }



        /// <summary>
        /// 根据备份文件id删除备份文件
        /// </summary>
        /// <param name="backupid">备份文件id</param>
        /// <returns>成功：true;失败:false</returns>
        public bool delBackup(int backupid)//根据备份文件id删除备份文件
        {
            try
            {
                backup r = getBackup(backupid);
                db.Delete<backup>(r);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }


        /// <summary>
        /// 根据备份文件的名称获得某备份文件
        /// </summary>
        /// <param name="backupname">备份文件名称</param>
        /// <returns>一个备份文件对象</returns>
        public backup getBackup(string backupname)
        {
            backup table = db.Queryable<backup>().Where(d => d.backupname == backupname).FirstOrDefault();
            return table;
        }

        /// <summary>
        /// 根据备份文件的id获得备份文件对象
        /// </summary>
        /// <param name="backupid">备份文件id</param>
        /// <returns>一个备份文件对象</returns>
        public backup getBackup(int backupid)
        {
            backup table = db.Queryable<backup>().Where(d => d.id == backupid).FirstOrDefault();
            return table;
        }
        /// <summary>
        /// 获得所有的备份文件对象
        /// </summary>
        /// <returns>备份文件对象列表</returns>
        public List<backup> getAllBackup()
        {
            List<backup> table = db.Queryable<backup>().ToList();
            return table;
        }

        /// <summary>
        /// 根据备份文件名称获得此备份文件对象的id
        /// </summary>
        /// <param name="backupname">备份文件名称</param>
        /// <returns>备份文件id</returns>
        public int getBackupId(string backupname)
        {
            int id = 0;
            backup table = db.Queryable<backup>().Where(d => d.backupname == backupname).FirstOrDefault();
            if (table != null) id = table.id;
            return id;
        }

        #endregion

        ///〈summary〉 
        ///直接删除目录下的所有文件及文件夹(保留目录)  
        ///〈/summary〉 
        ///〈param name="strDir"〉目录地址〈/param〉 
        public void deleteFiles(string strDir)
        {
            if (Directory.Exists(strDir))
            {
                string[] strDirs =
                Directory.GetDirectories(strDir);
                string[] strFiles =
                Directory.GetFiles(strDir);
                foreach (string strFile in strFiles)
                {
                    File.Delete(strFile);
                }
                foreach (string strdir in strDirs)
                {
                    Directory.Delete(strdir, true);
                }
            }
        }

    }
}
