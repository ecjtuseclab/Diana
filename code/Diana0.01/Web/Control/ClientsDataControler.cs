using basedal;
using Diana.Idal;
using Diana.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Diana.tool.Code;
using Diana.bll;
/// <summary>
///项目名称：Diana轻量级开发框架
///版本：0.0.1版
///开发团队成员：胡凯雨，张梦丽，艾美珍，易传佳，陈祚松，康文洋，张婷婷，王露，周庆，夏萍萍，陈兰兰
///模块和代码页功能描述：客户端获取资源控制器
///最后修改时间：2018/1/25
///修改日志：
///2018/1/25 GetMenuList方法(对超级管理员进行特殊处理)  ToMenuJson(获取资源中级别为1和2的资源)(胡凯雨)
/// </summary>
namespace Web.Control
{
    public class ClientsDataControler : baseControler
    {

        public ClientsDataControler()
        {
            noauth_actions.Add("GetClientsDataJson");
            noauth_actions.Add("GetMenuList");
        }


        public void GetClientsDataJson()
        {
            var data = new
            {
                dataItems = "",//this.GetDataItemList(),
                organize = "",//this.GetOrganizeList(),
                role = "",//this.GetRoleList(),
                duty = "",//this.GetDutyList(),
                user = "",
                authorizeMenu = this.GetMenuList(),
                authorizeButton = "",//this.GetMenuButtonList(),
            };
            writeJsonBack(data.ToJson());
        }

        private object GetMenuList()
        {
            List<resource> list;
            accctx = (AccountsPrincipal)ctx.Session["accctx"];    //获取当前用户信息上下文
            if (accctx.currentrole.rolename == "超级管理员")
            {
                list = IocModule.GetEntity<IresourceEx>().getEntityList();
            }
            else
            {
                if (accctx.Menus!=null)
                {
                    list = accctx.Menus;
                }
                else
                {
                    list=IdalCommon.IroleResourceEx.getRoleAllResource(accctx.currentrole.id);
                    list = list.Where((x, i) => list.FindIndex(z => z.id == x.id) == i).ToList();//Lambda表达式去重 
                    return ToMenuJson(list, "0");
                }
            }
            return ToMenuJson(list, "0");
        }

        private string ToMenuJson(List<resource> data, string parentId)
        {
            StringBuilder sbJson = new StringBuilder();
            if (data.Count > 0)
            {
                sbJson.Append("[");
                List<resource> entitys = data.FindAll(t => t.resourceowner == parentId).FindAll(t => t.resourceleval < 3);
                if (entitys.Count > 0)
                {
                    foreach (var item in entitys)
                    {

                        string strJson = item.ToJson();
                        strJson = strJson.Insert(strJson.Length - 1, ",\"ChildNodes\":" + ToMenuJson(data, item.id.ToString()) + "");
                        sbJson.Append(strJson + ",");

                    }
                    sbJson = sbJson.Remove(sbJson.Length - 1, 1);
                }
                sbJson.Append("]");
                return sbJson.ToString();
            }
            else
            {
                return "";
            }
        }


    }
}