using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.OleDb;

public partial class delete : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Int64 pid = Convert.ToInt64(System.Web.HttpContext.Current.Request.QueryString["pid"]);
        string conn_str = ConfigurationManager.AppSettings["AccessConnString"].ToString() +
            HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["AccessDbPath"]) + ";";
        OleDbConnection conn = new OleDbConnection(conn_str);
        OleDbCommand com = new OleDbCommand();
        com.Connection = conn;
        try
        {
            conn.Open();
            com.CommandType = CommandType.Text;
            com.CommandText = "delete from [post] where (post_id=@post_id)";
            com.Parameters.AddWithValue("@post_id", pid);
            int res = com.ExecuteNonQuery();
            if (res != 0)
            {
                Response.Redirect("index.aspx");
            }
            else
            {
                Response.Write("<script>alert('删除失败')</script>");
            }

            conn.Close();
            com = null;
            conn.Dispose();
        }
        catch
        {
            conn.Close();
            com = null;
            conn.Dispose();
        }
    }
}