using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using TanMiniToolSet.Common;
using System.Text;
using Diana.common.Util;
using Diana.common;
using Diana.tool;
/// <summary>
///项目名称：Diana轻量级开发框架
///版本：0.0.1版
///开发团队成员：胡凯雨，张梦丽，艾美珍，易传佳，陈祚松，康文洋，张婷婷，王露，周庆，夏萍萍，陈兰兰
///模块和代码页功能描述：模板生成器
///最后修改时间：2018/1/20
///修改日志：
/// </summary>

namespace Web
{
    /// <summary>
    /// 该处理程序用于生成前端页面，包括js,layer,html
    /// </summary>
    public class GenerationTemplates : IHttpHandler
    {

        public static string ConnectionString = ConfigHelper.GetConfigString("ConnectionString");

        public class Table_Column
        {
            public string columnName { get; set; }
            public string dataType { get; set; }
            public string dataLength { get; set; }
            public string description { get; set; }

            public Table_Column(string columnName, string dataType, string dataLength, string description)
            {
                this.columnName = columnName;
                this.dataType = dataType;
                this.dataLength = dataLength;
                this.description = description;
            }
        }

        public void ProcessRequest(HttpContext context)
        {
            VelocityHelper vh = new VelocityHelper();
            vh.Init(@"~/TemplateModel");//指定模板文件的相对路径

            List<string> templatFileNameModel = new List<string>();
            templatFileNameModel.Add("Model.html");
            templatFileNameModel.Add("Model_js.js");
            templatFileNameModel.Add("Model_layer.html");

            string urlPath = HttpContext.Current.Server.MapPath("~/TemplateModel/Model/");

            //打开数据库查询表名及字段信息
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();

            string tableName = null;//表名
            string tableCatalog = null;//数据库名
            string columnName = null;//字段名称
            string dataType = null;//字段类型
            string dataLength = null;//字段长度
            string description = null;//字段说明
            List<Table_Column> columnList = new List<Table_Column>();

            //获得数据库中的所有表的表名
            string[] restrictions = new string[4];
            restrictions[3] = "BASE TABLE";

            DataTable table = con.GetSchema("Tables", restrictions);

            foreach (DataRow row in table.Rows)
            {
                tableName = row["TABLE_NAME"].ToString();//表名
                tableCatalog = row["TABLE_CATALOG"].ToString();//数据库名

                string[] restrictionsColumn = new string[4];
                restrictionsColumn[2] = tableName;
                DataTable tableColumn = con.GetSchema("Columns", restrictionsColumn);

                foreach (DataRow rowColumn in tableColumn.Rows)
                {
                    columnName = rowColumn["COLUMN_NAME"].ToString();//字段名称
                    dataType = rowColumn["DATA_TYPE"].ToString();//字段类型
                    dataLength = rowColumn["CHARACTER_MAXIMUM_LENGTH"].ToString();//字段长度
                    description = "";//无法获得字段说明
                    columnList.Add(new Table_Column(columnName, dataType, dataLength, description));
                }

                vh.Put("tableName", tableName);
                vh.Put("columnList", columnList);

                vh.CreateHtml(templatFileNameModel[0], CreateFile(urlPath + tableCatalog + @"\Html\") + tableName.ToString() + ".html");
                vh.CreateHtml(templatFileNameModel[1], CreateFile(urlPath + tableCatalog + @"\js\") + tableName.ToString() + ".js");
                vh.CreateHtml(templatFileNameModel[2], CreateFile(urlPath + tableCatalog + @"\Html\layer\") + tableName.ToString() + "_layer.html");

                columnList.Clear();
            }
            con.Close();

            context.Response.Clear();
            context.Response.ContentEncoding = Encoding.UTF8;
            context.Response.ContentType = "application/json";
            context.Response.Write("生成成功！");
            context.Response.Flush();
            context.ApplicationInstance.CompleteRequest();
        }

        public string CreateFile(string FilePath)
        {
            try
            {
                string filePath = FilePath;     // HttpContext.Current.Server.MapPath("\\" + FilePath); // 文件路径
                if (Directory.Exists(FilePath) == false)//如果不存在就创建file文件夹
                {
                    Directory.CreateDirectory(FilePath);
                }
                return FilePath;
            }
            catch
            {
                return null;
            }
        }


        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}