using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.OleDb;

public partial class msgbook : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string conn_str = ConfigurationManager.AppSettings["AccessConnString"].ToString() +
            HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["AccessDbPath"]) + ";";
        OleDbConnection conn = new OleDbConnection(conn_str);
        OleDbCommand com = new OleDbCommand();
        com.Connection = conn;
        try
        {
            conn.Open();
            //获取留言
            com.CommandType = CommandType.Text;
            com.CommandText = "select * from msgbook order by date desc";
            OleDbDataReader msg = com.ExecuteReader();
            Repeater1.DataSource = msg;
            Repeater1.DataBind();
            msg.Close(); ;

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

    protected void logout_Click(object sender, EventArgs e)
    {
        Session["username"] = null;
        Response.Redirect("index.aspx");
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string conn_str = ConfigurationManager.AppSettings["AccessConnString"].ToString() +
            HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["AccessDbPath"]) + ";";
        OleDbConnection conn = new OleDbConnection(conn_str);
        OleDbCommand com = new OleDbCommand();
        com.Connection = conn;
        try
        {
            conn.Open();
            //获取留言
            com.CommandType = CommandType.Text;
            DateTime now = DateTime.Now;
            string uname = "游客";
            if (Session["username"] != null)
            {
                uname = Session["username"].ToString();
            }
            com.CommandText = "insert into [msgbook] ([username], [content], [date]) values (@username, @content, @date)";
            com.Parameters.AddWithValue("@username", uname);
            com.Parameters.AddWithValue("@content", msg_content.InnerText.ToString());
            com.Parameters.AddWithValue("@date", now.ToString());

            if (com.ExecuteNonQuery() == 0)
            {
                Response.Write("<script>alert('留言失败')</script>");
            }

            Response.Redirect(Request.Url.ToString());

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