using Diana.Idal;
using Diana.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
///项目名称：Diana轻量级开发框架
///版本：0.0.1版
///开发团队成员：胡凯雨，张梦丽，艾美珍，易传佳，陈祚松，康文洋，张婷婷，王露，周庆，夏萍萍，陈兰兰
///模块和代码页功能描述：操作SQLServer数据库的排序表的方法
///最后修改时间：2018/1/26
/// </summary>
namespace Diana.dal
{
    public class orderEx: RepositoryBase<order>, IorderEx
    {
        #region 基本操作 
       /// <summary>
       /// 根据订单号获取订单数据
       /// </summary>
       /// <param name="OrderNO">订单号</param>
       /// <returns></returns>
        public List<order> getOrderList(string OrderNO)
        {
            List<order> orderList = new List<order>();
            if (!string.IsNullOrEmpty(OrderNO))
            {
                orderList = getEntityList().Where(d => d.OrderNO == OrderNO).ToList();
            }
            else
            {
                orderList = getEntityList();
            }
            return orderList;
        }

        /// <summary>
        /// 提交表单数据
        /// </summary>
        /// <param name="orderEntity">一条订单数据</param>
        /// <param name="keyValue">主键</param>
        public void SubmitForm(order orderEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                int id = Convert.ToInt32(keyValue);
                orderEntity.id = id;
                db.Update<order>(orderEntity);
            }
            else
            {
                db.Insert<order>(orderEntity);
            }
        }
        #endregion
    }
}
