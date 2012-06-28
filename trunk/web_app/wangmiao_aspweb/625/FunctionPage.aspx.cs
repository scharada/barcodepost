using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FunctionPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("SelectInfo.aspx");
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddNewInfo.aspx");
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.Redirect("UpdateGarbageInfo.aspx");
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        Response.Redirect("deleteGarbageInfo.aspx");
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}