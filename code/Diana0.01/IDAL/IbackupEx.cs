using Diana.model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
///项目名称：Diana轻量级开发框架
///版本：0.0.1版
///开发团队成员：胡凯雨，张梦丽，艾美珍，易传佳，陈祚松，康文洋，张婷婷，王露，周庆，夏萍萍，陈兰兰
///模块和代码页功能描述：备份接口，供IOC
///最后修改时间：2018/1/26
///日志说明：
/// </summary>
namespace Diana.Idal
{
    public interface IbackupEx : IRepositoryBase<backup>
    {
        #region 1.基本操作


        /// <summary>
        /// 判断新增的备份文件是否合法，即备份文件名称不能重复
        /// </summary>
        /// <param name="backupname">备份文件名称</param>
        /// <returns>合法：true;不合法：false</returns>
        bool isLeagalBackupname(string backupname);

       
        /// <summary>
        /// 根据备份文件名称删除备份文件
        /// </summary>
        /// <param name="backupname"></param>
        /// <returns>成功：true;失败:false</returns>
        bool delete(string backupname);

        /// <summary>
        /// 根据备份文件id删除备份文件
        /// </summary>
        /// <param name="backupid">备份文件id</param>
        /// <returns>成功：true;失败:false</returns>
        bool delBackup(int backupid);//根据备份文件id删除备份文件

      
        /// <summary>
        /// 根据备份文件的名称获得某备份文件
        /// </summary>
        /// <param name="backupname">备份文件名称</param>
        /// <returns>一个备份文件对象</returns>
        backup getBackup(string backupname);

        /// <summary>
        /// 根据备份文件的id获得备份文件对象
        /// </summary>
        /// <param name="backupid">备份文件id</param>
        /// <returns>一个备份文件对象</returns>
        /// 
        backup getBackup(int backupid);
        /// <summary>
        /// 获得所有的备份文件对象
        /// </summary>
        /// <returns>备份文件对象列表</returns>
        /// 
        List<backup> getAllBackup();
        /// <summary>
        /// 根据备份文件名称获得此备份文件对象的id
        /// </summary>
        /// <param name="backupname">备份文件名称</param>
        /// <returns>备份文件id</returns>
        int getBackupId(string backupname);

        #endregion

        ///〈summary〉 
        ///直接删除目录下的所有文件及文件夹(保留目录)  
        ///〈/summary〉 
        ///〈param name="strDir"〉目录地址〈/param〉 
        void deleteFiles(string strDir);


    }
}
