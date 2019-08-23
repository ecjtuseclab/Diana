using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Diana.model;
using basedal;
using Diana.Idal;
namespace Diana.common.Util.WebControl
{

    public class RescTree
    {
        private resource resource { get; set; }

        public int id { get { return resource.id; } }
        public string text { get { return resource.resourcename; } }
        public int? type { get { return resource.resourcetype; } }
        public string url { get { return resource.resourceurl; } }
        public string owner { get { return resource.resourceowner; } }

        public string leftico { get { return resource.resourceleftico; } }
        public string rightico { get { return resource.resourcerightico; } }
        public int? number { get { return resource.resourcenumber; } }

        public string description { get { return resource.resourcename; } }

        public bool @checked { get; set; }//加@是因为和关键字冲突
        private List<resource> roleresource { get; set; }

        private List<RescTree> _children;
        public List<RescTree> children
        {
            get
            {
                if (_children == null)
                    _children = new List<RescTree>();
                return _children;
            }
        }
        ///// <summary>
        ///// 资源数据处理接口
        ///// </summary>
        //private IresourceEx _iresourceEx;
        //public IresourceEx IresourceEx
        //{
        //    get
        //    {
        //        if (_iresourceEx == null)
        //        {
        //            _iresourceEx = IocModule.GetEntity<IresourceEx>();
        //        }
        //        return _iresourceEx;
        //    }
        //}

        /// <summary>
        /// 构造函数，单纯显示树形
        /// </summary>
        /// <param name="currentNode">当前资源</param>
        public RescTree(resource resource)
        {
            this.resource = resource;

            if (resource != null)
            {
                List<resource> rlist =IdalCommon.IresourceEx.getResourceByOwner(resource.id.ToString());
                foreach (resource r in rlist)
                {
                    RescTree tn = new RescTree(r);
                    this.children.Add(tn);
                }
            }
        }

        /// <summary>
        /// 构造函数，返回树形资源信息并且勾选已有资源
        /// </summary>
        /// <param name="currentNode"></param>
        /// <param name="arealist">已拥有资源</param>
        public RescTree(resource resource, List<resource> roleresource)
        {
            this.resource = resource;
            this.roleresource = roleresource;

            if (resource != null)
            {
                //List<resource> rlist = IresourceEx.getAllResource().Where(p => p.resourceowner == resource.id.ToString()).ToList();//获取属于这个节点的子节点
                List<resource> rlist = IdalCommon.IresourceEx.getResourceByOwner(resource.id.ToString());
                foreach (resource r in rlist)
                {

                    RescTree tn = new RescTree(r, roleresource);
                    tn.@checked = false;//需要获取check值的初始化方法
                    if (roleresource.Where(a => a.id == r.id).Count() > 0)
                        tn.@checked = true;
                    this.children.Add(tn);
                }
            }
        }
    }
}