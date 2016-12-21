using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.OleDb;

public partial class manage_msgbook : System.Web.UI.Page
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
                com.CommandText = "select * from [msgbook] order by [ID] desc";

                OleDbDataReader msgbook = com.ExecuteReader();
                msgbook_lst.DataSource = msgbook;
                msgbook_lst.DataBind();
                msgbook.Close();

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
            com.CommandText = string.Format("delete from [msgbook] where [ID]={0}", id);
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