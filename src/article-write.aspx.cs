using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.OleDb;

public partial class article_write : System.Web.UI.Page
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
                com.CommandType = CommandType.Text;
                /*获取个人信息*/
                string username_session = Session["username"].ToString();
                com.CommandText = string.Format("SELECT username, email, birthday FROM [user] WHERE (username = '{0}')", username_session);
                OleDbDataReader user_info = com.ExecuteReader();
                if (user_info.Read())
                {
                    username.Text = username_session;//user_info["username"].ToString();
                    email.Text = user_info["email"].ToString();
                    birthday.Text = user_info["birthday"].ToString();
                    tags.Text = "本博客系统开发PM";
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
    protected void submit_Click(object sender, EventArgs e)
    {
        string conn_str = ConfigurationManager.AppSettings["AccessConnString"].ToString() +
            HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["AccessDbPath"]) + ";";
        OleDbConnection conn = new OleDbConnection(conn_str);
        OleDbCommand com = new OleDbCommand();
        com.Connection = conn;
        try
        {
            conn.Open();
            //新增博文
            string article_title = a_title.Text.ToString();
            string article_content = a_content.InnerText.ToString();
            string now = DateTime.Now.ToString();
            string username_session = Session["username"].ToString();
            string article_shortcontent = article_content.Substring(0, (article_content.Length) / 3);
            com.CommandText = "insert into post(username,title,post_date,shortcontent,content) values (@username,@title,@post_date,@shortcontent,@content)";
            com.Parameters.AddWithValue("@username", username_session);
            com.Parameters.AddWithValue("@title", article_title);
            com.Parameters.AddWithValue("@post_date", now);
            com.Parameters.AddWithValue("@shortcontent", article_shortcontent);
            com.Parameters.AddWithValue("@content", article_content);
            int post_res = com.ExecuteNonQuery();
            if (post_res != 0)
            {
                Response.Redirect("index.aspx");
            }
            else
            {
                Response.Write("<script>alert('发表文章失败!')</script>");
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
}