using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class SelectInfo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (new GarbageBLL().IsContainOnum(this.TextBox2.Text))
        {
            this.Label1.Visible = true;
            this.Label2.Visible = false;
            Garbage gb = new GarbageBLL().GetGaibageInfoByOnum(this.TextBox2.Text);

            this.Label1.Text = "流水号" + gb.Onum;
            this.Label1.Text += "<br>仓库代码" + gb.GN;
            this.Label1.Text += "<br>订单号" + gb.PO_;
            this.Label1.Text += "<br>订单号" + gb.SN;
            this.Label1.Text += "<br>发送时间" + gb.ST;
            this.Label1.Text += "<br>接收时间见" + gb.RT;
            this.Label1.Text += "<br>重复流水号" + gb.OW;
        }
        else
        {
            this.Label2.Visible = true;
            this.Label1.Visible = false;
            this.Label2.Text = "该流水号不存在，请重新输入";
        }
       
    }
    protected void Button2_Click(object sender, EventArgs e)
    {

        if (new GarbageBLL().IsContainGn(this.TextBox3.Text))
        {
            this.Label1.Visible = true;
            this.Label3.Visible = false;
            Garbage gb = new GarbageBLL().GarbageInfoByGn(this.TextBox3.Text);
            this.Label1.Text = "流水号" + gb.Onum;
            this.Label1.Text += "<br>仓库代码" + gb.GN;
            this.Label1.Text += "<br>订单号" + gb.PO_;
            this.Label1.Text += "<br>订单号" + gb.SN;
            this.Label1.Text += "<br>发送时间" + gb.ST;
            this.Label1.Text += "<br>接收时间见" + gb.RT;
            this.Label1.Text += "<br>重复流水号" + gb.OW;
        }
        else
        {
            this.Label3.Visible = true;
            this.Label1.Visible = false;
            this.Label2.Text = "该仓库号不存在，请重新输入";


        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        if (new GarbageBLL().IsContainPo(this.TextBox4.Text))
        {
            this.Label1.Visible = true;
            this.Label4.Visible = false;
            Garbage gb = new GarbageBLL().GarbageInfoBYPo(this.TextBox4.Text);
            this.Label1.Text = "流水号" + gb.Onum;
            this.Label1.Text += "<br>仓库代码" + gb.GN;
            this.Label1.Text += "<br>订单号" + gb.PO_;
            this.Label1.Text += "<br>订单号" + gb.SN;
            this.Label1.Text += "<br>发送时间" + gb.ST;
            this.Label1.Text += "<br>接收时间见" + gb.RT;
            this.Label1.Text += "<br>重复流水号" + gb.OW;
        }
        else
        {
            this.Label4.Visible = true;
            this.Label1.Visible = false;
            this.Label4.Text = "该订单号不存在，请重新输入";
        }
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        
    
     if(new GarbageBLL().IsContainSn(this.TextBox5.Text))
     {
        Garbage gb = new GarbageBLL().GetGarbageBySN(this.TextBox5.Text);
        this.Label1.Visible = true;
        this.Label5.Visible = false;
        this.Label1.Text = "流水号" + gb.Onum;
        this.Label1.Text += "<br>仓库代码" + gb.GN;
        this.Label1.Text += "<br>订单号" + gb.PO_;
        this.Label1.Text += "<br>订单号" + gb.SN;
        this.Label1.Text += "<br>发送时间" + gb.ST;
        this.Label1.Text += "<br>接收时间见" + gb.RT;
        this.Label1.Text += "<br>重复流水号" + gb.OW;
        }
        else
        {
            this.Label5.Visible = true;
            this.Label1.Visible = false;
            this.Label5.Text = "该订单号不存在，请重新输入";

        }
       
    }
   protected void Button5_Click(object sender, EventArgs e)
    {
            
       /*
        string onum =this.txtONUM.Text.Trim();
        string gn = this.txtWarehouseNo.Text.Trim();
        string po= this.txtPO.Text.Trim();
        string sn= this.txtSN.Text.Trim();


       Garbage gb=new GarbageBLL().GetGarbageInfoByMultipleFactor(onum,gn,po,sn);                //?
       this.Label1.Text="流水号"+gb.Onum;
       this.Label1.Text+="<br>仓库编号"+gb.GN;


          /* string onum = this.txtONUM.Text;
        string gn = this.txtWarehouseNo.Text;
        string po = this.txtPO.Text;
        string sn = this.txtSN.Text;
        long  num = new GarbageDAL().GetGarbageInfoNum(onum,po,gn,sn);
        this.Label1.Text = num.ToString();
        */


    }

    /*
   public DataSet GetDateSet()
   {

       SqlConnection myCon = new SqlConnection();
       myCon.ConnectionString = "Initial Catalog=HuiErPu;Integrated Security=true;server=(local)";
       myCon.Open();


       SqlCommand selectCMD = new SqlCommand("select top 3 Onum,GN FROM Garbage", myCon);


       SqlDataAdapter custDA = new SqlDataAdapter();
       custDA.SelectCommand = selectCMD;


       DataSet custDS = new DataSet();
       custDA.Fill(custDS, "Garbage");

       myCon.Close();
       return custDS;

   }


   public void LoadData()
   {

       DataSet ds = GetDateSet();
       GridView1.DataSource = ds;

       GridView1.DataBind();
   }
    */

}