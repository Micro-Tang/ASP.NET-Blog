using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.OleDb;

public partial class photo_dir : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["username"] == null)
        {
            Response.Redirect("login.aspx");
        }
        else
        {
            string session_username = Session["username"].ToString();
            string conn_str = ConfigurationManager.AppSettings["AccessConnString"].ToString() +
                HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["AccessDbPath"]) + ";";
            OleDbConnection conn = new OleDbConnection(conn_str);
            OleDbCommand com = new OleDbCommand();
            com.Connection = conn;
            try
            {
                conn.Open();
                com.CommandType = CommandType.Text;
                //获取相册
                com.CommandText = string.Format("select * from [photo_dir] where [username] = '{0}' order by [photo_dir_id] desc", session_username);
                OleDbDataReader pdir = com.ExecuteReader();
                repeater2.DataSource = pdir;
                repeater2.DataBind();
                pdir.Close();
                /*获取个人信息*/
                string username_session = "";
                if (Convert.ToString(Session["username"]) != "" || Session["username"] != null)
                {
                    username_session = Session["username"].ToString();
                }
                com.CommandText = string.Format("SELECT username, email, birthday FROM [user] WHERE (username = '{0}')", username_session);
                OleDbDataReader user_info = com.ExecuteReader();
                if (user_info.Read())
                {
                    username.Text = username_session;//user_info["username"].ToString();
                    email.Text = user_info["email"].ToString();
                    birthday.Text = user_info["birthday"].ToString();
                }
                else
                {

                    username.Text = "唐堃";
                    email.Text = "microtang@icloud.com";
                    birthday.Text = "1995/08/28";
                }
                user_info.Close();


                string user_tags = "";
                com.CommandText = string.Format("select content from [user_tag] where [username]='{0}'", username_session);
                OleDbDataReader user_tag_obj = com.ExecuteReader();
                while (user_tag_obj.Read())
                {
                    user_tags += user_tag_obj["content"].ToString() + " ";
                }
                tags.Text = user_tags;
                user_tag_obj.Close();

                //获取友情链接
                com.CommandText = string.Format("select friend_url_id, title, url from [friend_url] where (username='{0}')", username_session);
                OleDbDataReader friend_url_res = com.ExecuteReader();
                flink.DataSource = friend_url_res;
                flink.DataBind();
                friend_url_res.Close();

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

    public void Delflink(object sender, EventArgs e)
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
            com.CommandText = "delete from [friend_url] where (friend_url_id=@id)";
            com.Parameters.AddWithValue("@id", id);
            if (com.ExecuteNonQuery() != 0)
            {
                Response.Redirect(Request.Url.ToString());
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

    protected void add_dir_btn_Click(object sender, EventArgs e)
    {
        string session_username = Session["username"].ToString();
        string conn_str = ConfigurationManager.AppSettings["AccessConnString"].ToString() +
            HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["AccessDbPath"]) + ";";
        OleDbConnection conn = new OleDbConnection(conn_str);
        OleDbCommand com = new OleDbCommand();
        com.Connection = conn;
        try
        {
            conn.Open();
            com.CommandType = CommandType.Text;
            com.CommandText = string.Format("insert into [photo_dir] ([username], [title]) values ('{0}', '{1}')", session_username, TextBox1.Text.ToString());
            if (com.ExecuteNonQuery() == 0)
            {
                Response.Write("<script>alert('添加相册失败')</script>");
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

    protected void del_dir_btn_Click(object sender, EventArgs e)
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
            com.CommandText = "delete from [photo_dir] where ([photo_dir_id]=@id)";
            com.Parameters.AddWithValue("@id", id);
            if (com.ExecuteNonQuery() != 0)
            {
                //删除dir里面的图片
                com.CommandText = "delete from [photos] where [dir_id]=@id";
                com.Parameters.AddWithValue("@id", id);
                com.ExecuteNonQuery();
                Response.Redirect(Request.Url.ToString());
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