using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.OleDb;

public partial class article_detail : System.Web.UI.Page
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
            Int64 pid = Convert.ToInt64(System.Web.HttpContext.Current.Request.QueryString["pid"]);
            com.CommandType = CommandType.Text;
            string username_session;
            if (Session["username"] != null)
            {
                username_session = Session["username"].ToString();
            }
            else
            {
                username_session = "";
            }
            /*更新博文查看数*/
            com.CommandText = string.Format("update [post] set readcount=readcount+1 where (post_id = {0})", pid);
            com.ExecuteNonQuery();
            /*获取博文详情*/
            com.CommandText = string.Format("SELECT * FROM [post] WHERE (post_id = {0})", pid);
            OleDbDataReader article = com.ExecuteReader();
            if (article.Read())
            {
                article_title.Text = article["title"].ToString();
                article_date.Text = article["post_date"].ToString();
                article_content.Text = article["content"].ToString();
                if (article["username"].ToString() == username_session)
                {
                    delete.Text = string.Format("<a href='delete.aspx?pid={0}'>删除</a>", pid);
                }
            }
            article.Close();

            /*获取个人信息*/
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

            //获取评论列表
            com.CommandText = string.Format("select username, content, comment_date from [comment] where (post_id={0})", pid);
            OleDbDataReader comment_list = com.ExecuteReader();
            comment_lst.DataSource = comment_list;
            comment_lst.DataBind();
            comment_list.Close();

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
    protected void comment_Click(object sender, EventArgs e)
    {
        string conn_str = ConfigurationManager.AppSettings["AccessConnString"].ToString() +
            HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["AccessDbPath"]) + ";";
        OleDbConnection conn = new OleDbConnection(conn_str);
        OleDbCommand com = new OleDbCommand();
        com.Connection = conn;
        try
        {
            conn.Open();
            Int64 pid = Convert.ToInt64(System.Web.HttpContext.Current.Request.QueryString["pid"]);
            string username_session;
            if (Session["username"] != null)
            {
                username_session = Session["username"].ToString();
            }
            else
            {
                username_session = "";
            }
            string c_content = comment_content.Value;
            DateTime now=DateTime.Now;
            com.CommandType = CommandType.Text;
            com.CommandText = string.Format("insert into [comment] (username, post_id, content, comment_date) values ('{0}', {1}, '{2}', '{3}')", username_session, pid, c_content, now);
            int res = com.ExecuteNonQuery();
            if (res != 0)
            {
                Response.Redirect(Request.Url.ToString()); 
            }
            else
            {
                Response.Redirect("<script>alert('评论失败！')</script>"); 
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