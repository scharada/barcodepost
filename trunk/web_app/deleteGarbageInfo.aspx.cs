using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class deleteGarbageInfo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (new GarbageBLL().IsContainOnum(this.TextBox1.Text))
        {


            this.Label2.Visible = false;
            Garbage gb = new GarbageBLL().GetGaibageInfoByOnum(this.TextBox1.Text);
            this.Label1.Text = "流水号" + gb.Onum;
            this.Label1.Text += "<br>仓库代码" + gb.GN;
            this.Label1.Text += "<br>发运单号" + gb.PO_;
            this.Label1.Text += "<br>序列号" + gb.SN;
            this.Label1.Text += "<br>录入时间" + gb.RT;
            this.Label1.Text += "<br>重复流水号" + gb.OW;

        }
        else
        {

            this.Label2.Visible = true;
            this.Label1.Text = "该流水号不存在，请重新输入";
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        new GarbageBLL().deleteGarbageIngoByOnum(this.TextBox1.Text);

        Response.Write("该条信息已删除");
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.Redirect("SelectInfo.aspx");
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddNewInfo.aspx");
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        Response.Redirect("UpdateGarbageInfo.aspx");
    }
    protected void Button6_Click(object sender, EventArgs e)
    {
        Response.Redirect("deleteGarbageInfo.aspx");
    }
}