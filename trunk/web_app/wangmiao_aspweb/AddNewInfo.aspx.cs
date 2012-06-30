using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AddNewInfo : System.Web.UI.Page
{
    const string DEFAULT_STRING = "NULL";

    protected void Page_Load(object sender, EventArgs e)
    {
        string website2 = Request.Form["content1"];
        Response.Write("wangmiao chuan zhi:"+website2);


        //this.TextBox10.Text = System.DateTime.Parse(Convert.ToString(System.DateTime.Now)).ToString("yyyy-MM-dd");//yyyy-MM-dd-hh-mm-ss
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
          int operator_i=Convert.ToInt16(this.TextBox9.Text.Trim());

        if (operator_i==0)                                //标记为0
        {
            string newSN = this.TextBox4.Text.Trim();
            string newPO = this.TextBox3.Text.Trim();
            if (new GarbageDAL().IsContainSn(newSN) && new GarbageBLL().IsContainPo(newPO) )
            {

                Response.Write("信息已有，无需再次增加");
            }
            else
            {
                Garbage gb = new Garbage();
                if (new GarbageBLL().IsContainOnum(this.TextBox1.Text))
                {
                    this.Label1.Visible = true;
                    this.Label1.Text = "该流水号已存在，请重新输入";
                }
                else
                {
                    this.Label1.Visible = false;
                    gb.Onum = this.TextBox1.Text;
                    if (this.TextBox2.Text.Trim().Length != 0)
                    {
                        gb.GN = this.TextBox2.Text;
                    }
                    else 
                    {
                        gb.GN = DEFAULT_STRING;
                    }
                     
                    gb.PO_ = this.TextBox3.Text;
                    gb.SN = this.TextBox4.Text;
                    gb.ST = Convert.ToDateTime(this.Calendar1.SelectedDate);
                   // gb.RT = Convert.ToDateTime(this.Calendar2.SelectedDate);

                    //System.DateTime.Parse().ToString("yyyy-MM-dd");

                    //本记录的接收时间按照当前页面的提交时间
                    string time_now = Convert.ToString(System.DateTime.Now);
                    gb.RT = Convert.ToDateTime(time_now);


                    gb.OW = this.TextBox8.Text;
                    gb.Flag = Convert.ToInt16(this.TextBox9.Text);
                    new GarbageBLL().AddGaibageNewInfo(gb);
                    Response.Write("信息添加成功");
                }
             }
        }
        else if (operator_i==1)  //标记为1              
        {
            string water_no = this.TextBox8.Text.Trim();
            if (new GarbageBLL().IsContainOW(water_no))
            {
                new GarbageBLL().deleteGarbageInfo(this.TextBox8.Text);
                Response.Write("错误流水号信息已删除");
            }
            else 
            {
                Response.Write("该条流水号不存在");
            }
        }
        else if (operator_i==2)                                                                  //标记为3
        {
            string water_no = this.TextBox8.Text.Trim();
            if (new GarbageBLL().IsContainOW(water_no))
            {
                new GarbageBLL().deleteGarbageInfo(water_no);

                Garbage gb = new Garbage();

                if (new GarbageBLL().IsContainOnum(this.TextBox1.Text))
                {
                    this.Label1.Visible = true;
                    this.Label1.Text = "该流水号已存在，请重新输入";
                }
                else
                {

                    this.Label1.Visible = false;
                    gb.Onum = this.TextBox1.Text;
                    gb.GN = this.TextBox2.Text;
                    gb.PO_ = this.TextBox3.Text;
                    gb.SN = this.TextBox4.Text;
                    gb.ST = Convert.ToDateTime(this.Calendar1.SelectedDate);
                   // gb.RT = Convert.ToDateTime(this.Calendar2.SelectedDate);
                    gb.RT = Convert.ToDateTime(this.TextBox10.Text);
                    gb.OW = water_no;
                    gb.Flag = operator_i;
                    new GarbageBLL().AddGaibageNewInfo(gb);
                    Response.Write("错误流水号信息已删除,新信息添加成功");
                }

            }        
        }



    }
   
    
   
 
    protected void Button2_Click1(object sender, EventArgs e)
    {
        Response.Redirect("SelectInfo.aspx");
    }
    protected void Button3_Click1(object sender, EventArgs e)
    {
        Response.Redirect("AddNewInfo.aspx");
    }
    protected void Button4_Click1(object sender, EventArgs e)
    {
        Response.Redirect("UpdateGarbageInfo.aspx");
    }
    protected void Button5_Click1(object sender, EventArgs e)
    {
        Response.Redirect("deleteGarbageInfo.aspx");
    }
    protected void TextBox8_TextChanged(object sender, EventArgs e)
    {

    }
    protected void Calendar1_SelectionChanged(object sender, EventArgs e)
    {

    }
}