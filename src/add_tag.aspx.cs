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
        else
        {
            string conn_str = ConfigurationManager.AppSettings["AccessConnString"].ToString() +
            HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["AccessDbPath"]) + ";";
            OleDbConnection conn = new OleDbConnection(conn_str);
            OleDbCommand com = new OleDbCommand();
            com.Connection = conn;
            try
            {
                conn.Open();
                Int64 pid = Convert.ToInt64(HttpContext.Current.Request.QueryString["pid"]);
                //获取标签
                com.CommandType = CommandType.Text;
                string user_tags = "";
                string need_username = "";
                com.CommandText = string.Format("select username from [user] where [user_id]={0}", pid);
                OleDbDataReader username = com.ExecuteReader();
                if (username.Read())
                {
                    need_username = username["username"].ToString();
                }
                rec_user.Text = need_username;
                username.Close();
                com.CommandText = string.Format("select content from [user_tag] where [username]='{0}'", need_username);
                OleDbDataReader tags_obj = com.ExecuteReader();
                while (tags_obj.Read())
                {
                    user_tags += tags_obj["content"].ToString() + "  ";
                }
                tags.Text = user_tags;
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
    protected void logout_Click(object sender, EventArgs e)
    {
        Session["username"] = null;
        Response.Redirect("index.aspx");
    }

    protected void tag_btn_Click(object sender, EventArgs e)
    {
        string conn_str = ConfigurationManager.AppSettings["AccessConnString"].ToString() +
            HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["AccessDbPath"]) + ";";
        OleDbConnection conn = new OleDbConnection(conn_str);
        OleDbCommand com = new OleDbCommand();
        com.Connection = conn;
        try
        {
            conn.Open();
            Int64 pid = Convert.ToInt64(HttpContext.Current.Request.QueryString["pid"]);
            //获取标签
            string tag_input = tag_tb.Text.ToString();
            com.CommandType = CommandType.Text;
            string need_username = "";
            com.CommandText = string.Format("select username from [user] where [user_id]={0}", pid);
            OleDbDataReader username = com.ExecuteReader();
            if (username.Read())
            {
                need_username = username["username"].ToString();
            }
            username.Close();

            com.CommandText = string.Format("insert into [user_tag] (username, content) values (@username, @content)");
            com.Parameters.AddWithValue("@username", need_username);
            com.Parameters.AddWithValue("@content", tag_input);
            int res = com.ExecuteNonQuery();
            if(res != 0)
            {
                Response.Redirect("friends.aspx");
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