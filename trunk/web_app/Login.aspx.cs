using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (new LogInBLL().VaiLogInInfo(this.TextBox1.Text, this.TextBox2.Text))
        {
            Session["UserName"] = this.TextBox1.Text;
            Response.Redirect("MainPage.aspx");
        }
        else
        {
            Response.Redirect("ErrorLogIn.aspx");
        }

    }
}