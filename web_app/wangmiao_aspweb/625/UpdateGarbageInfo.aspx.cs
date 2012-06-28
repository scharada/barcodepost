using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UpdateGarbageInfo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("SelectInfo.aspx");
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddNewInfo.aspx");
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
          Response.Redirect("UpdateGarbageInfo.aspx");
    }
    protected void  Button5_Click(object sender, EventArgs e)
   {
         Response.Redirect("deleteGarbageInfo.aspx");
   }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (new GarbageBLL().IsContainOnum(this.TextBox1.Text))
        {
            this.Label2.Visible = false;
            Garbage gb = new GarbageBLL().GetGaibageInfoByOnum(this.TextBox1.Text);

            this.TextBox1.Text = gb.Onum;
            this.TextBox2.Text = gb.GN;
            this.TextBox3.Text = gb.PO_;
            this.TextBox4.Text = gb.SN;
            this.TextBox5.Text = gb.ST.ToString();
            this.TextBox6.Text = System.DateTime.Now.ToString();
            this.TextBox7.Text = gb.OW;
            this.TextBox8.Text = gb.Flag.ToString();
        }
        else
        {

            this.Label2.Visible = true;
            this.Label2.Text = "该流水号不存在，请重新输入";
        }
    }
    protected void Button6_Click(object sender, EventArgs e)
    {
        string onum = this.TextBox1.Text.Trim();
        string gn = this.TextBox2.Text;
        string po = this.TextBox3.Text;
        string sn = this.TextBox4.Text;
        string st = this.Calendar1.SelectedDate.ToString();
        string rt = System.DateTime.Now.ToString();


        string ow = this.TextBox7.Text;

        int flag = Convert.ToInt16(this.TextBox8.Text);


        new GarbageBLL().UpdateGarbageIfoByOnum(onum, gn, po, sn, st, rt, ow, flag);
        Response.Write("信息更新成功");
    }
}
