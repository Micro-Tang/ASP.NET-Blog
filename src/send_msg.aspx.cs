using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.OleDb;

public partial class send_msg : System.Web.UI.Page
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
                //获取收信人
                com.CommandType = CommandType.Text;
                com.CommandText = string.Format("SELECT username FROM [user] WHERE (user_id = {0})", pid);
                OleDbDataReader rec_user = com.ExecuteReader();
                if (rec_user.Read())
                {
                    user.Text = rec_user[0].ToString();
                }
                rec_user.Close();
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

    protected void send_msg_btn_Click(object sender, EventArgs e)
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
            string username_session = Session["username"].ToString();
            
            //获取收信人
            com.CommandType = CommandType.Text;
            string recieve_user = "";
            com.CommandText = string.Format("SELECT username FROM [user] WHERE (user_id = {0})", pid);
            Response.Write(pid);
            OleDbDataReader rec_user = com.ExecuteReader();
            if (rec_user.Read())
            {
                recieve_user = rec_user[0].ToString();
            } 
            rec_user.Close();

            string textarea_msg = msg.InnerText.ToString();
            string now = DateTime.Now.ToString();
            com.CommandText = string.Format("insert into [msg] (send_username,recieve_username,content,msg_date) values (@s_user, r_user, content, msg_date)");
            com.Parameters.AddWithValue("@s_user", username_session);
            com.Parameters.AddWithValue("@r_user", recieve_user);
            com.Parameters.AddWithValue("@content", textarea_msg);
            com.Parameters.AddWithValue("@msg_date", now);
            int res = com.ExecuteNonQuery();
            if (res != 0)
            {
                Response.Redirect("friends.aspx");
            }
            else
            {
                Response.Write("<script>alert('私信失败')</script>");
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