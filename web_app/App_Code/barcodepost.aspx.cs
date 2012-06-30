using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default2 : System.Web.UI.Page
{
    private string DEFAULT_STRING = DataModelUtility.DEFAULT_STRING;

    //错误码
    private string RETURN_NORMAL = DataModelUtility.RETURN_NORMAL;
    private string RETURN_NULL_PRIMARY_KEY = DataModelUtility.RETURN_NULL_PRIMARY_KEY;
    private string RETURN_DUPLICATE_PRIMARY_KEY = DataModelUtility.RETURN_DUPLICATE_PRIMARY_KEY;
    private string RETURN_DUPLICATE_PO_SN = DataModelUtility.RETURN_DUPLICATE_PO_SN;
    private string RETURN_INVALID_MODIFY_FLAG = DataModelUtility.RETURN_INVALID_MODIFY_FLAG;
    private string RETURN_DELETE_INVALID_RECORD = DataModelUtility.RETURN_DELETE_INVALID_RECORD;


    //新增一条记录
    protected string AddNewRecord(string running_no, string po_no, string warehouse_code, string barcode_no, string scan_time, string run_no_for_modify, string modify_flag)
    {
        string _returnString = string.Empty;
        _returnString = DataModelUtility.addNewRecord(running_no, po_no, warehouse_code, barcode_no, scan_time, run_no_for_modify, modify_flag);
        return _returnString;
    }

    //对于get方式，服务器端用Request.QueryString获取变量的值，
    //对于post方式，服务器端用Request.Form获取提交的数据。
    protected void Page_Load(object sender, EventArgs e)
    {

        string _returnString = string.Empty;
        //第一部分，接收手持端参数
        string running_no = "";       //流水号
        string po_no = "";          //订单号
        string warehouse_code = ""; //仓库码
        string barcode_no = "";       //条码号
        string scan_time = "";        //扫描时间
        string run_no_for_modify = "";//待修改流水号Z
        string modify_flag = "0";      //修改标识

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
            _returnString = DataModelUtility.getResponseString(RETURN_NULL_PRIMARY_KEY, running_no, po_no, warehouse_code, barcode_no, scan_time, run_no_for_modify, modify_flag);
            Response.Write(_returnString);
            return;
        }
        //2.按照修改操作符区分
        int i_flag = Convert.ToInt16(modify_flag);
        //普通新增
        if (i_flag == 0)
        {
            _returnString = AddNewRecord(running_no, po_no, warehouse_code, barcode_no, scan_time, run_no_for_modify, modify_flag);
            Response.Write(_returnString);
            return;
        }
        else if (i_flag == 1)//删除操作
        {
            if (new GarbageBLL().IsContainOW(run_no_for_modify))//包含run_no_for_modify
            {
                new GarbageBLL().deleteGarbageInfo(run_no_for_modify);

                _returnString = DataModelUtility.getResponseString(RETURN_NORMAL, running_no, po_no, warehouse_code, barcode_no, scan_time, run_no_for_modify, modify_flag);
                Response.Write(_returnString);
                return;
            }
            else
            {
                _returnString = DataModelUtility.getResponseString(RETURN_DELETE_INVALID_RECORD, running_no, po_no, warehouse_code, barcode_no, scan_time, run_no_for_modify,
                   modify_flag);
                Response.Write(_returnString);
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
                    _returnString = DataModelUtility.getResponseString(RETURN_DUPLICATE_PRIMARY_KEY, running_no, po_no, warehouse_code, barcode_no, scan_time, run_no_for_modify,
                     modify_flag);
                    Response.Write(_returnString);
                    return;
                }
                else
                {
                    _returnString = AddNewRecord(running_no, po_no, warehouse_code, barcode_no, scan_time, run_no_for_modify, modify_flag);
                    Response.Write(_returnString);
                    return;
                }
            }
        }
        else
        {
            _returnString = DataModelUtility.getResponseString(RETURN_INVALID_MODIFY_FLAG, running_no, po_no, warehouse_code, barcode_no, scan_time, run_no_for_modify, modify_flag);
            Response.Write(_returnString);
            return;
        }
    }
}