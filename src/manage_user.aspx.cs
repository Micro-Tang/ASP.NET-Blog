using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.OleDb;

public partial class manage_user : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["admin"] == null)
        {
            Session["admin_username"] = null;
            Session["admin"] = null;
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
                com.CommandType = CommandType.Text;
                com.CommandText = "select * from [user] where ([type]=0) order by [user_id] desc";

                OleDbDataReader user = com.ExecuteReader();
                user_lst.DataSource = user;
                user_lst.DataBind();
                user.Close();

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
        Session["admin"] = null;
        Session["admin_username"] = null;
        Response.Redirect("index.aspx");
    }
    protected void del_Click(object sender, EventArgs e)
    {
        int id = Convert.ToInt32((sender as LinkButton).CommandArgument);
        string conn_str = ConfigurationManager.AppSettings["AccessConnString"].ToString() +
            HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["AccessDbPath"]) + ";";
        OleDbConnection conn = new OleDbConnection(conn_str);
        OleDbCommand com = new OleDbCommand();
        com.Connection = conn;
        try
        {
            conn.Open();
            com.CommandType = CommandType.Text;
            //查找用户名
            string username = "";
            com.CommandText = "select username from [user] where [user_id]=@id";
            com.Parameters.AddWithValue("@id", id);
            OleDbDataReader u = com.ExecuteReader();
            if (u.Read())
            {
                username = u["username"].ToString();
            }
            u.Close();

            com.CommandText = "delete from [user] where ([user_id]=@id)";
            com.Parameters.AddWithValue("@id", id);
            com.ExecuteNonQuery();
            //删除友链
            com.CommandText = string.Format("delete from [friend_url] where [username]='{0}'", username);
            com.Parameters.AddWithValue("@username", username);
            com.ExecuteNonQuery();
            //删除评论
            com.CommandText = string.Format("delete from [comment] where [username]='{0}'", username);
            com.Parameters.AddWithValue("@username", username);
            com.ExecuteNonQuery();
            //删除私信
            com.CommandText = string.Format("delete from [msg] where ([send_username]='{0}' or [recieve_username]='{1}')", username, username);
            com.Parameters.AddWithValue("@s_user", username);
            com.Parameters.AddWithValue("@r_user", username);
            com.ExecuteNonQuery();
            //更新留言板
            com.CommandText = string.Format("update [msgbook] set [username]=游客 where ([username]='{0}')", username);
            com.ExecuteNonQuery();
            //删除博文
            com.CommandText = string.Format("delete from [post] where [username]='{0}'", username);
            com.ExecuteNonQuery();
            //删除标签
            com.CommandText = string.Format("delete from [user_tag] where [username]='{0}'", username);
            com.ExecuteNonQuery();
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