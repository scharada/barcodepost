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

        if (Request.Form["userName"] != null && Request.Form["passWord"] != null)
        {
            if (Request.Form["userName"].ToString().Trim() != "" && Request.Form["passWord"].ToString() != "")
            {

                string _userName = Request.Form["userName"].ToString().Trim();
                string _passWord = Request.Form["passWord"].ToString();

                if (new LogInBLL().VaiLogInInfo(_userName, _passWord))
                {
                    Session["userName"]= _userName;
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script type='text/javascript'>window.location.href = 'MainPage.aspx';</script>");
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script type='text/javascript'>alert(99999);</script>");
                }
            }
        }
    }
}