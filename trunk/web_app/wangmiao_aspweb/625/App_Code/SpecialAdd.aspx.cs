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
         
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
          int operator_i=Convert.ToInt16(this.DropDownList1.SelectedItem.Text.Trim());

        if (operator_i==0)                                //标记为0
        {
            string newSN = this.TextBoxSn.Text.Trim();
            string newPO = this.TextBoxPo.Text.Trim();
            if (new GarbageDAL().IsContainSn(newSN) && new GarbageBLL().IsContainPo(newPO) )
            {

                Response.Write("信息已有，无需再次增加");
            }
            else
            {
                Garbage gb = new Garbage();
                if (new GarbageBLL().IsContainOnum(this.TextBoxOnum.Text))
                {
                    this.Label1.Visible = true;
                    this.Label1.Text = "该流水号已存在，请重新输入";
                }
                else
                {
                    this.Label1.Visible = false;
                    gb.Onum = this.TextBoxOnum.Text;
                    if (this.TextBoxGN.Text.Trim().Length != 0)
                    {
                        gb.GN = this.TextBoxGN.Text;
                    }
                    else 
                    {
                        gb.GN = DEFAULT_STRING;
                    }
                     
                    //gb.PO_ = this.TextBox3.Text;


                    if (this.TextBoxPo.Text.Trim().Length != 0)
                    {
                        gb.PO_ = this.TextBoxPo.Text;
                    }
                    else
                    {
                        gb.PO_ = DEFAULT_STRING;
                    }

                   // gb.SN = this.TextBox4.Text;

                    if (this.TextBoxSn.Text.Trim().Length != 0)
                    {
                        gb.SN = this.TextBoxSn.Text;
                    }
                    else
                    {
                        gb.SN = DEFAULT_STRING;
                    }

                    string time_now = Convert.ToString(System.DateTime.Now);

                    gb.ST = Convert.ToDateTime(time_now);
                   


                   // gb.OW = this.TextBox8.Text;
                    if (this.TextBoxOw.Text.Trim().Length != 0)
                    {
                        gb.OW = this.TextBoxOw.Text;
                    }
                    else
                    {
                        gb.OW = DEFAULT_STRING;
                    }

                    gb.Flag = Convert.ToInt16(this.DropDownList1.SelectedItem.Text);
                    if (this.DropDownList1.SelectedItem.Text.Trim().Length != 0)
                    {
                        gb.Flag= Convert.ToInt16(this.DropDownList1.SelectedItem.Text);
                    }
                    else
                    {
                        gb.Flag=0;
                    }










                    new GarbageBLL().AddGaibageNewInfo(gb);
                    Response.Write("信息添加成功");
                }
             }
        }
        else if (operator_i==1)  //标记为1              
        {
            string water_no = this.TextBoxOw.Text.Trim();
            if (new GarbageBLL().IsContainOW(water_no))
            {
                new GarbageBLL().deleteGarbageInfo(this.TextBoxOw.Text);
                Response.Write("错误流水号信息已删除");
            }
            else 
            {
                Response.Write("该条流水号不存在");
            }
        }
        else if (operator_i==2)                                                                  //标记为2
        {
            string water_no = this.TextBoxOw.Text.Trim();
            if (new GarbageBLL().IsContainOW(water_no))
            {
                new GarbageBLL().deleteGarbageInfo(water_no);

                Garbage gb = new Garbage();

                if (new GarbageBLL().IsContainOnum(this.TextBoxOnum.Text))
                {
                    this.Label1.Visible = true;
                    this.Label1.Text = "该流水号已存在，请重新输入";
                }
                else
                {

                    this.Label1.Visible = false;
                    gb.Onum = this.TextBoxOnum.Text;
                    if (this.TextBoxGN.Text.Trim().Length != 0)
                    {
                        gb.GN = this.TextBoxGN.Text;
                    }
                    else
                    {
                        gb.GN = DEFAULT_STRING;
                    }

                    //gb.PO_ = this.TextBox3.Text;


                    if (this.TextBoxPo.Text.Trim().Length != 0)
                    {
                        gb.PO_ = this.TextBoxPo.Text;
                    }
                    else
                    {
                        gb.PO_ = DEFAULT_STRING;
                    }

                    // gb.SN = this.TextBox4.Text;

                    if (this.TextBoxSn.Text.Trim().Length != 0)
                    {
                        gb.SN = this.TextBoxSn.Text;
                    }
                    else
                    {
                        gb.SN = DEFAULT_STRING;
                    }

                    string time_now = Convert.ToString(System.DateTime.Now);

                    gb.ST = Convert.ToDateTime(time_now);



                    // gb.OW = this.TextBox8.Text;
                    if (this.TextBoxOw.Text.Trim().Length != 0)
                    {
                        gb.OW = this.TextBoxOw.Text;
                    }
                    else
                    {
                        gb.OW = DEFAULT_STRING;
                    }

                    gb.Flag = Convert.ToInt16(this.DropDownList1.SelectedItem.Text);
                    if (this.DropDownList1.SelectedItem.Text.Trim().Length != 0)
                    {
                        gb.Flag = Convert.ToInt16(this.DropDownList1.SelectedItem.Text);
                    }
                    else
                    {
                        gb.Flag = 0;
                    }

                    new GarbageBLL().AddGaibageNewInfo(gb);

                    Response.Write("错误流水号信息已删除,新信息添加成功");
                }

            }
            else
            {
                Response.Write(" 请输入待修改流水号或通过标记为0状态添加信息");

            }
        }



    }





    protected void TextBoxPo_TextChanged(object sender, EventArgs e)
    {

    }
}