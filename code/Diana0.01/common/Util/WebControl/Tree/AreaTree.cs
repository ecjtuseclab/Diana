using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Diana.model;
using Diana.Idal;
using basedal;
///<summary>
///项目名称：Diana轻量级开发框架
///版本：0.0.1版
///开发团队成员：胡凯雨，张梦丽，艾美珍，易传佳，陈祚松，康文洋，张婷婷，王露，周庆，夏萍萍，陈兰兰
///模块和代码页功能描述：区域树形表格实体类
///最后修改时间：2018/1/22
/// </summary>
namespace Diana.common.Util.WebControl
{
    public class AreaTree
    {
        private area area { get; set; }
        public int id { get { return area.id; } }
        public string text { get { return area.fullname; } }
        public bool @checked { get; set; }//加@是因为和关键字冲突
        private List<area> arealist { get; set; }//已有的地区
        private List<AreaTree> _children;
        public List<AreaTree> children
        {
            get
            {
                if (_children == null)
                    _children = new List<AreaTree>();
                return _children;
            }
        }
        ///// <summary>
        ///// 动作数据处理接口
        ///// </summary>
        //private IareaEx _iareaEx;
        //public IareaEx IareaEx
        //{
        //    get
        //    {
        //        if (_iareaEx == null)
        //        {
        //            _iareaEx = IocModule.GetEntity<IareaEx>();
        //        }
        //        return _iareaEx;
        //    }
        //}
        /// <summary>
        /// 构造函数，返回树形区域信息并且勾选已有区域
        /// </summary>
        /// <param name="currentNode"></param>
        /// <param name="arealist">已拥有区域</param>
        public AreaTree(area currentNode, List<area> arealist)
        {
            this.area = currentNode;
            this.arealist = arealist;
            if (currentNode != null)
            {
                List<area> allNodes = IdalCommon.IareaEx.getAreaByParentId(currentNode.id);//获取属于这个节点的子节点
                foreach (area an in allNodes)//遍历当前节点下的所有子节点
                {
                    AreaTree at = new AreaTree(an, arealist);
                    at.@checked = false;
                    if (arealist.Where(p => p.id == an.id).Count() > 0)
                        at.@checked = true;
                    this.children.Add(at);//递归创建子节点
                }
            }
        }
        /// <summary>
        /// 构造函数，单纯显示树形
        /// </summary>
        /// <param name="currentNode">当前区域</param>
        public AreaTree(area currentNode)//单纯显示树形
        {
            this.area = currentNode;

            //List<area> allNodes = IareaEx.getAllArea().Where(p => p.parentid == currentNode.id).ToList();//获取属于这个节点的子节点
            List<area> allNodes = IdalCommon.IareaEx.getAreaByParentId(currentNode.id);//获取属于这个节点的子节点
            foreach (area a in allNodes)//遍历当前节点下的所有子节点
            {
                AreaTree tn = new AreaTree(a);
                this.children.Add(tn);//递归创建子节点
            }
        }
    }
}
