using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.OleDb;

public partial class add_tag : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["username"] == null)
        {
            Response.Redirect("login.aspx");
        }
    }
    protected void logout_Click(object sender, EventArgs e)
    {
        Session["username"] = null;
        Response.Redirect("index.aspx");
    }

    
    protected void flink_btn_Click(object sender, EventArgs e)
    {
        string conn_str = ConfigurationManager.AppSettings["AccessConnString"].ToString() +
            HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["AccessDbPath"]) + ";";
        OleDbConnection conn = new OleDbConnection(conn_str);
        OleDbCommand com = new OleDbCommand();
        com.Connection = conn;
        try
        {
            conn.Open();
            com.CommandType = CommandType.Text;
            string username_session;
            if (Convert.ToString(Session["username"]) != "" || Session["username"] != null)
            {
                username_session = Session["username"].ToString();
            }
            else
            {
                username_session = "";
            }
            com.CommandText = "insert into [friend_url] (username, title, url) values (@username, @title, @url)";
            com.Parameters.AddWithValue("@username", username_session);
            com.Parameters.AddWithValue("@title", flink_t.Text.ToString());
            com.Parameters.AddWithValue("@url", flink_c.Text.ToString());
            int res = com.ExecuteNonQuery();
            if (res != 0)
            {
                Response.Redirect("index.aspx");
            }
            else
            {
                Response.Write("<script>alert('添加失败')</script>");
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