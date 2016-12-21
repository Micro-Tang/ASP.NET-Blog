using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class manage_index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (Session["admin"] == null)
        {
            Session["admin_username"] = null;
            Session["admin"] = null;
            Response.Redirect("login.aspx");
        }
    }

    protected void logout_Click(object sender, EventArgs e)
    {
        Session["admin"] = null;
        Session["admin_username"] = null;
        Response.Redirect("index.aspx");
    }
}