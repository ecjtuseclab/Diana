using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
///<summary>
///项目名称：Diana轻量级开发框架
///版本：0.0.1版
///开发团队成员：胡凯雨，张梦丽，艾美珍，易传佳，陈祚松，康文洋，张婷婷，王露，周庆，夏萍萍，陈兰兰
///模块和代码页功能描述：SelectOption类
///最后修改时间：2018/1/22
/// </summary>
namespace Diana.common.Util.Web
{
    public class SelectOption
    {
        public SelectOption()
        {
        }
        public SelectOption(string value, string text)
        {
            this.Value = value;
            this.Text = text;
        }
        public string Value { get; set; }
        public string Text { get; set; }

        public static SelectOption Create(object instance, string valueProp = "Id", string textProp = "Name")
        {

            Dictionary<string, object> dic = Json.ToObject<Dictionary<string, object>>(Json.ToJson(instance));
            SelectOption option = new SelectOption();

            if (dic.Keys.Contains(valueProp) && dic.Keys.Contains(textProp))
            {
                option.Value = dic[valueProp] == null ? null : dic[valueProp].ToString();
                option.Text = dic[textProp] == null ? null : dic[textProp].ToString();
            }
            else
            {
                if (option.Value == null)
                {
                    int i = 0;
                    foreach (var k in dic.Keys)//获取第2个
                    {
                        if (i == 0)
                        {
                            option.Value = dic[k] == null ? null : dic[k].ToString();

                        }
                        if (i == 1)
                        {
                            option.Text = dic[k] == null ? null : dic[k].ToString();
                        }
                        i++;
                    }
                }
            }
            return option;
        }
        public static List<SelectOption> CreateList<T>(IEnumerable<T> instanceList, string valueProp = "Id", string textProp = "Name")
        {
            List<SelectOption> options = new List<SelectOption>();
            foreach (var instance in instanceList)
            {
                SelectOption option = SelectOption.Create(instance, valueProp, textProp);
                options.Add(option);
            }

            return options;
        }
    }
}
