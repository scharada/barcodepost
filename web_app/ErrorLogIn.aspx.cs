using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ErrorLogIn : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (new LogInBLL().VaiLogInInfo(this.TextBox1.Text, this.TextBox2.Text))
        {

            Response.Redirect("FunctionPage.aspx");
        }
        else
        {

            Response.Redirect("ErrorLogIn.aspx");

        }
    }
}