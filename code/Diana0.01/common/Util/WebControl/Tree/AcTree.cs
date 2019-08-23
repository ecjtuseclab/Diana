using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Diana.model;
using basedal;
using Diana.Idal;
///<summary>
///项目名称：Diana轻量级开发框架
///版本：0.0.1版
///开发团队成员：胡凯雨，张梦丽，艾美珍，易传佳，陈祚松，康文洋，张婷婷，王露，周庆，夏萍萍，陈兰兰
///模块和代码页功能描述：AC树形表格实体类
///最后修改时间：2018/1/22
/// </summary>
namespace Diana.common.Util.WebControl
{

    public class AcTree
    {
        private action action { get; set; }
        public int id { get { return action.id; } }
        public string text { get { return action.actionname; } }
        // public int? type { get { return resource.actiontype; } }
        // public int? _parentId { get; set; }
        public bool @checked { get; set; }//加@是因为和关键字冲突
        private List<action> roleresource { get; set; }
        private List<AcTree> _children;
        public List<AcTree> children
        {
            get
            {
                if (_children == null)
                    _children = new List<AcTree>();
                return _children;
            }
        }
        ///// <summary>
        ///// 动作数据处理接口
        ///// </summary>
        //private IactionEx _iactionEx;
        //public IactionEx IactionEx
        //{
        //    get
        //    {
        //        if (_iactionEx == null)
        //        {
        //            _iactionEx = IocModule.GetEntity<IactionEx>();
        //        }
        //        return _iactionEx;
        //    }
        //}

        /// <summary>
        /// 构造函数，返回树形动作信息并且勾选已有动作
        /// </summary>
        /// <param name="action"></param>
        /// <param name="roleresource">已拥有动作</param>
        public AcTree(action action, List<action> roleresource)
        {
            this.action = action;
            this.roleresource = roleresource;

            if (action != null)
            {
                //List<action> rlist = IactionEx.getAllAction().Where(p => p.actionowner == action.id.ToString()).ToList();//获取属于这个节点的子节点
                List<action> rlist = IdalCommon.IactionEx.getActionByOwner(action.id.ToString());
                foreach (action r in rlist)
                {
                    AcTree tn = new AcTree(r, roleresource);
                    if (roleresource.Where(a => a.id == r.id).Count() > 0)
                        tn.@checked = true;        //需要获取check值的初始化方法
                    this.children.Add(tn);
                }
            }
        }
    }
}