using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default2 : System.Web.UI.Page
{

    protected void Response_post_variable(string name,string value)
    {
        Response.Write(name+"="+value+",");
    }

    protected void Response_all(
        string error_no,
        string running_no,
        string po_no,
        string warehouse_code,
        string barcode_no,
        string scan_time,
        string run_no_for_modify,
        string modify_flag
        )
    {
        Response_post_variable("error", error_no);
        Response_post_variable("running_no", running_no);
        Response_post_variable("warehouse_code", warehouse_code);
        Response_post_variable("po_no", po_no);
        Response_post_variable("barcode_no", barcode_no);
        Response_post_variable("scan_time", scan_time);
        Response_post_variable("run_no_for_modify", run_no_for_modify);
        Response_post_variable("modify_flag", modify_flag);
    }

    //对于get方式，服务器端用Request.QueryString获取变量的值，
    //对于post方式，服务器端用Request.Form获取提交的数据。
    protected void Page_Load(object sender, EventArgs e)
    {
        const string DEFAULT_STRING = "NULL";

        //错误码
        string RETURN_NORMAL = "0";
        
        string RETURN_NULL_PRIMARY_KEY = "1";
        string RETURN_DUPLICATE_PRIMARY_KEY = "2";
        string RETURN_DUPLICATE_PO_SN = "3";

        string RETURN_INVALID_MODIFY_FLAG = "4";

        string RETURN_DELETE_INVALID_RECORD = "21";

        //第一部分，接收手持端参数
        string running_no="";       //流水号
        string po_no = "";          //订单号
        string warehouse_code = ""; //仓库码
        string barcode_no="";       //条码号
        string scan_time="";        //扫描时间
        string run_no_for_modify="";//待修改流水号Z
        string modify_flag="0";      //修改标识

        running_no = Request.Form["running_no"].Trim();
        warehouse_code = Request.Form["warehouse_code"].Trim();
        po_no = Request.Form["po_no"].Trim();
        barcode_no = Request.Form["barcode_no"].Trim();
        scan_time = Request.Form["scan_time"].Trim();
        modify_flag = Request.Form["modify_flag"].Trim();
        run_no_for_modify = Request.Form["run_no_for_modify"].Trim();

        //第二部分，数据检核
        
        //1.主键不为空
        string primary_key_field = running_no;
        if (primary_key_field.Trim().Length == 0)
        {
            Response_all(
                RETURN_NULL_PRIMARY_KEY,
                running_no,
                po_no,
                warehouse_code,
                barcode_no,
                scan_time,
                run_no_for_modify,
                modify_flag);
            return;
        }


        //////////////////

        //Response_all(
        //    "for_check",
        //    running_no,
        //    po_no,
        //    warehouse_code,
        //    barcode_no,
        //    scan_time,
        //    run_no_for_modify,
        //    modify_flag);
        //        return;
        ///////////////////



        //2.按照修改操作符区分
        int i_flag = Convert.ToInt16(modify_flag);

        //普通新增
        if (i_flag == 0)
        {
            Garbage gb = new Garbage();
            if (new GarbageBLL().IsContainOnum(running_no))
            {
                Response_all(
                     RETURN_DUPLICATE_PRIMARY_KEY,
                     running_no,
                     po_no,
                     warehouse_code,
                     barcode_no,
                     scan_time,
                     run_no_for_modify,
                     modify_flag);
                return;
                //this.Label1.Text = "该流水号已存在，请重新输入";
            }
            else
            {
                gb.Onum = running_no;//流水号

                if (warehouse_code.Length != 0)//仓库编码
                {
                    gb.GN = warehouse_code;
                }
                else
                {
                    gb.GN = DEFAULT_STRING;
                }

                gb.PO_ = po_no;//订单号
                gb.SN = barcode_no;//序列号
                gb.ST = Convert.ToDateTime(scan_time);//扫描时间
                //本记录的接收时间按照当前页面的提交时间
                string time_now = Convert.ToString(System.DateTime.Now);
                gb.RT = Convert.ToDateTime(time_now);//接受时间


                gb.OW = run_no_for_modify;//待修改流水号
                gb.Flag = Convert.ToInt16(modify_flag);//修改标识

                //执行Insert的Linq语句
                new GarbageBLL().AddGaibageNewInfo(gb);

                Response_all(
                     RETURN_NORMAL,
                     running_no,
                     po_no,
                     warehouse_code,
                     barcode_no,
                     scan_time,
                     run_no_for_modify,
                     modify_flag);
                return;

                //Response.Write("信息添加成功");
            }

        }
        else if (i_flag == 1)//删除操作
        {
            if (new GarbageBLL().IsContainOW(run_no_for_modify))
            {
                new GarbageBLL().deleteGarbageInfo(run_no_for_modify);
                
                Response_all(
                    RETURN_NORMAL,
                    running_no,
                    po_no,
                    warehouse_code,
                    barcode_no,
                    scan_time,
                    run_no_for_modify,
                    modify_flag);
                //Response.Write("错误流水号信息已删除");
                return;
            }
            else
            {
                Response_all(
                    RETURN_DELETE_INVALID_RECORD,
                    running_no,
                    po_no,
                    warehouse_code,
                    barcode_no,
                    scan_time,
                    run_no_for_modify,
                    modify_flag);
                //Response.Write("该条流水号不存在");
                return;
            }
        }
        else if (i_flag == 2)//修改替换
        {

            if (new GarbageBLL().IsContainOW(run_no_for_modify))
            {
                new GarbageBLL().deleteGarbageInfo(run_no_for_modify);

                Garbage gb = new Garbage();

                if (new GarbageBLL().IsContainOnum(running_no))
                {
                    Response_all(
                     RETURN_DUPLICATE_PRIMARY_KEY,
                     running_no,
                     po_no,
                     warehouse_code,
                     barcode_no,
                     scan_time,
                     run_no_for_modify,
                     modify_flag);
                    return;
                    //this.Label1.Text = "该流水号已存在，请重新输入";
                }
                else
                {                    
                    gb.Onum = running_no;
                    gb.GN = warehouse_code;
                    gb.PO_ = po_no;
                    gb.SN = barcode_no;
                    gb.ST = Convert.ToDateTime(scan_time);
                    string time_now = Convert.ToString(System.DateTime.Now);
                    gb.RT = Convert.ToDateTime(time_now);//接受时间
                    gb.OW = run_no_for_modify;
                    gb.Flag = i_flag;
                    new GarbageBLL().AddGaibageNewInfo(gb);

                    Response_all(
                         RETURN_NORMAL,
                         running_no,
                         po_no,
                         warehouse_code,
                         barcode_no,
                         scan_time,
                         run_no_for_modify,
                         modify_flag);
                    return;
                    //Response.Write("错误流水号信息已删除,新信息添加成功");
                }

            } 
        }
        else
        { 
            Response_all(
                RETURN_INVALID_MODIFY_FLAG,
                running_no,
                po_no,
                warehouse_code,
                barcode_no,
                scan_time,
                run_no_for_modify,
                modify_flag);
            return;
        }

    }
}