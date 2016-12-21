using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Text.RegularExpressions;

public partial class register : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    protected void register_Click(object sender, EventArgs e)
    {
        string uname = username.Text;
        string eml = email.Text;
        string passwd = pwd.Text;
        string cpasswd = confirm_pwd.Text;
        string birthday = Convert.ToDateTime(birth.Text).ToShortDateString();
        Regex eml_p = new Regex(@"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$",
            RegexOptions.IgnoreCase);
        if ((uname.Length < 2) || (uname.Length > 20))
        {
            username.Text = "";
            Response.Write("<script>alert('用户名长度在2-20')</script>");

        }
        if (!eml_p.IsMatch(eml))
        {
            email.Text = "";
            Response.Write("<script>alert('邮箱不符合规则')</script>");
        }
        if (passwd.Length < 6)
        {
            pwd.Text = "";
            Response.Write("<script>alert('密码长度至少为2位')</script>");
        }
        if (passwd != cpasswd)
        {
            confirm_pwd.Text = "";
            Response.Write("<script>alert('密码不一致')</script>");
        }
        if ((uname.Length >= 2 && uname.Length <= 20) && eml_p.IsMatch(eml) && (passwd == cpasswd))
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
                com.CommandText = "select * from [user] where (username=@username)";
                com.Parameters.AddWithValue("@username", uname);
                OleDbDataReader u_res = com.ExecuteReader();
                if (u_res.Read())
                {
                    Response.Write("<script>alert('用户名已被注册，请重新填写')</script>");
                    u_res.Close();
                } 
                else
                {
                    u_res.Close();
                    string str = string.Format(@"insert into [user] ([username], [pwd], [email], [birthday], [type]) values ('{0}', '{1}', '{2}', '{3}', '{4}')", uname, passwd, eml, birthday, 0);
                    com.CommandText = str;
                    if (Convert.ToBoolean(com.ExecuteNonQuery()))
                    {
                        Session["username"] = uname;
                        Response.Redirect("index.aspx");
                        conn.Close();
                        com = null;
                        conn.Dispose();
                    }
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
    protected void Calendar1_SelectionChanged(object sender, EventArgs e)
    {
        birth.Text = Calendar1.SelectedDate.ToString();
    }

    protected void year_s_TextChanged(object sender, EventArgs e)
    {
        Int32 year = Convert.ToInt32(year_s.Text);
        Int32 month = 1;
        Calendar1.TodaysDate = new DateTime(year, month, 1);
    }
}