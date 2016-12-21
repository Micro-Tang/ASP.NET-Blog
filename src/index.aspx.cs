using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.OleDb;

public partial class index : System.Web.UI.Page
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
            com.CommandType = CommandType.Text;
            /*获取博文列表*/
            com.CommandText = string.Format("select * from [post] order by post_id desc");
            OleDbDataReader post_lst = com.ExecuteReader();
            Repeater1.DataSource = post_lst;
            Repeater1.DataBind();
            post_lst.Close();
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
}