using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.OleDb;

public partial class login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void submit_Click(object sender, EventArgs e)
    {
        string uname = username.Text;
        string pwd = password.Text;

        string conn_str = ConfigurationManager.AppSettings["AccessConnString"].ToString() +
            HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["AccessDbPath"]) + ";";
        OleDbConnection conn = new OleDbConnection(conn_str);
        OleDbCommand com = new OleDbCommand();
        com.Connection = conn;
        try
        {
            conn.Open();
            com.CommandText = string.Format(@"select * from [user] where [username]='{0}' and [pwd]='{1}'", uname, pwd);
            com.CommandType = CommandType.Text;
            OleDbDataReader rd = com.ExecuteReader();
            if (rd.Read())
            {
                if (rd["type"].ToString() == "0")
                {
                    Session["username"] = rd["username"].ToString();
                    Response.Redirect("index.aspx");
                }
                else
                {
                    Session["admin"] = rd["type"].ToString();
                    Session["admin_username"] = rd["username"].ToString();
                    Response.Redirect("manage_index.aspx");
                }
                conn.Close();
                com = null;
                conn.Dispose();
                
            }
            else
            {
                Response.Write("<script>alert('用户名或密码错误')</script>");
                conn.Close();
                com = null;
                conn.Dispose();
            }
        }
        catch
        {
            conn.Close();
            com = null;
            conn.Dispose();
        }
    }
    protected void register_Click(object sender, EventArgs e)
    {
        Response.Write("register.aspx");
    }
}