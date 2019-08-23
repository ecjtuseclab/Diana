using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Diana.Idal;
using Diana.model;
/// <summary>
///项目名称：Diana轻量级开发框架
///版本：0.0.1版
///开发团队成员：胡凯雨，张梦丽，艾美珍，易传佳，陈祚松，康文洋，张婷婷，王露，周庆，夏萍萍，陈兰兰
///模块和代码页功能描述：操作SQLServer数据库的工作流的方法
///最后修改时间：2018/1/26
/// </summary>
namespace  Diana.dal
{
   public class workflowManager:IworkflowManager
    {
    
        #region//前台通过指定多项查询条件查询、同时分页查询；xpp
       
        #endregion
        private string _workflowName;
        public workflowManager(string workflowName)
        {
            this._workflowName = workflowName;
            _workflow = workflowEx.Instance.getworkflow(this._workflowName);
        }
        private workflow _workflow;
        public workflow GetWorkflow()
        {

            return _workflow;
        }
        private List<workflownode> _workflownodeList;
        public List<workflownode> workflownodeList
        {
            get
            {
                if (_workflownodeList == null)
                {
                    _workflownodeList = workflownodeEx.Instance.getEntityList(this._workflow.id);
                }
                return _workflownodeList;
            }
        }
        private List<workflownodeaction> _workflownodeactionList;
        public List<workflownodeaction> workflownodeactionList
        {
            get
            {
                if (_workflownodeactionList == null)
                {
                    _workflownodeactionList = workflownodeactionEx.Instance.getEntityList(this._workflow.id);
                }
                return _workflownodeactionList;
            }
        }
        private List<workflownodeoperator> _workflownodeoperatorList;
        public List<workflownodeoperator> workflownodeoperatorList
        {
            get
            {
                if (_workflownodeoperatorList == null)
                {
                    _workflownodeoperatorList = workflownodeoperatorEx.Instance.getEntityList(this._workflow.id);
                }
                return _workflownodeoperatorList;
            }
        }
        //获取后置节点
        public IEnumerable<workflownode> GetPostNodes(int nodeid)
        {
            foreach (workflownodeaction Item in workflownodeactionList)
            {
                if (Item.currentnodeid.Equals(nodeid))
                {
                    yield return workflownodeList.Where(p => p.id == Item.nextnodeid).FirstOrDefault();
                }
            }

        }
        //获取前置节点
        public IEnumerable<workflownode> GetPreNodes(int nodeid)
        {
            foreach (workflownodeaction Item in workflownodeactionList)
            {
                if (Item.nextnodeid.Equals(nodeid))
                {
                    yield return workflownodeList.Where(p => p.id == Item.currentnodeid).FirstOrDefault();
                }
            }

        }
        //根据名称获取工作流节点
        public workflownode GetWorkflowNode(string name)
        {
            return this.workflownodeList.Where(p => p.wfnodename == name).FirstOrDefault();
        }
        //根据ID获取工作流节点
        public workflownode GetWorkflowNode(int id)
        {
            return this.workflownodeList.Where(p => p.id == id).FirstOrDefault();
        }
        //根据ID获取节点动作（跃迁信息）
        public workflownodeaction GetNodeActionbyID(int id)
        {
            return this.workflownodeactionList.Where(p => p.id == id).FirstOrDefault();
        }
        //根据执行ID获取节点动作（跃迁信息）
        public workflownodeaction GetNodeActionbyOperatorID(int operatorID)
        {
            int id = this.workflownodeoperatorList.Where(p => p.id == operatorID).Select(e => e.nodeactionid).FirstOrDefault();
            return this.workflownodeactionList.Where(p => p.id == id).FirstOrDefault();
        }
        //获取以节点ID为 终点 的跃迁数组
        public IEnumerable<workflownodeaction> GetEnterNodeAction(int nodeID)
        {
            foreach (workflownodeaction Item in this.workflownodeactionList.Where(p => p.nextnodeid == nodeID))
            {
                yield return Item;
            }
        }
        //获取以节点ID为 起点 的跃迁数组
        public IEnumerable<workflownodeaction> GetOutNodeAction(int nodeID)
        {
            foreach (workflownodeaction Item in this.workflownodeactionList.Where(p => p.currentnodeid == nodeID))
            {
                yield return Item;
            }
        }
        //根据当前节点状态 下一个可跃迁节点
        public IEnumerable<workflownodeaction> GetNextNodeAction(int currentNode)
        {
            return this.GetOutNodeAction(currentNode);
        }
        public IEnumerable<workflownode> GetNextNode(int currentNode)
        {
            foreach (workflownodeaction Item in this.workflownodeactionList.Where(p => p.currentnodeid == currentNode))
            {
                yield return this.workflownodeList.Where(p => p.id == Item.nextnodeid).FirstOrDefault();
            }
        }
    }
   public class workflowexception : Exception
   {
       internal workflowexception(string errorMessage)
           : base(errorMessage)
       {
       }
   }
}
