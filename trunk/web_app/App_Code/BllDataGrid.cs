using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Summary description for BllDataGrid
/// </summary>
public class BllDataGrid
{
    private static Hashtable htSession = new Hashtable();
    public static string ShowDataGrid(Hashtable ht)
    {
        string json = string.Empty;
        if (!htSession.Contains("demoDataGrid"))
        {
            htSession.Add("demoDataGrid", RandomUtility.RandomDataTable());
        }
        DataTable funcDs = (DataTable)htSession["demoDataGrid"];
        List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
        int page = int.Parse(ht["page"].ToString());
        int rows = int.Parse(ht["rows"].ToString());
        //int start = rows * (page - 1);
        int end = rows * page < funcDs.Rows.Count ? rows * page : funcDs.Rows.Count;
        try
        {
            for (int start = rows * (page - 1); start < end; start++)
            {
                Dictionary<string, object> json1 = new Dictionary<string, object>();
                foreach (DataColumn dc in funcDs.Columns)
                {
                    json1.Add(dc.ColumnName, funcDs.Rows[start][dc.ColumnName].ToString());
                }
                list.Add(json1);
            }
            Dictionary<string, object> result = new Dictionary<string, object>();
            result.Add("total", funcDs.Rows.Count);
            result.Add("rows", list);
            json = JsonUtility.GetSuccessJson(result);
        }
        catch (Exception ex)
        {
            json = JsonUtility.GetErrorJson("Error Message Is " + ex.Message);
        }
        return json;
    }

    public static string ShowAllOrder(Hashtable ht)
    {
        string json = string.Empty;
        DataTable funcDs = DataModelUtility.getAllOrder();
        json = getJsonFromDataTable(funcDs, ht);
        return json;
    }

    public static string GetOrderByCompose(Hashtable ht)
    {
        string _Onum = ht["Onum"].ToString().Trim();
        string _GN = ht["GN"].ToString().Trim();
        string _PO = ht["PO"].ToString().Trim();
        string _SN = ht["SN"].ToString().Trim();
        string _FromDateTime = ht["FromDateTime"].ToString().Trim();
        string _ToDateTime = ht["ToDateTime"].ToString().Trim();

        DataTable funcDs = DataModelUtility.getOrderByCompose(_Onum,_GN,_PO,_SN,_FromDateTime,_ToDateTime);
        string json = getJsonFromDataTable(funcDs, ht);
        return json;
    }


    public static string GetOrderNull(Hashtable ht)
    {
        string json = string.Empty;
        string _Onum = string.Empty;
        DataTable funcDs = DataModelUtility.getOrderNull();
        json = getJsonFromDataTable(funcDs, ht);
        return json;
    }

    public static string getJsonFromDataTable(DataTable funcDs, Hashtable ht)
    {
        string json = string.Empty;

        List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
        int page = int.Parse(ht["page"].ToString());
        int rows = int.Parse(ht["rows"].ToString());
        //int start = rows * (page - 1);
        int end = rows * page < funcDs.Rows.Count ? rows * page : funcDs.Rows.Count;
        try
        {
            for (int start = rows * (page - 1); start < end; start++)
            {
                Dictionary<string, object> json1 = new Dictionary<string, object>();
                foreach (DataColumn dc in funcDs.Columns)
                {
                    json1.Add(dc.ColumnName, funcDs.Rows[start][dc.ColumnName].ToString());
                }
                list.Add(json1);
            }
            Dictionary<string, object> result = new Dictionary<string, object>();
            result.Add("total", funcDs.Rows.Count);
            result.Add("rows", list);
            json = JsonUtility.GetSuccessJson(result);
        }
        catch (Exception ex)
        {
            json = JsonUtility.GetErrorJson("Error Message Is " + ex.Message);
        }

        return json;
    }

    internal static string AddNewOrder(Dictionary<string, object> param)
    {
        string _returnString = string.Empty;
        string running_no = string.Empty, po_no = string.Empty, warehouse_code = string.Empty, barcode_no = string.Empty, scan_time = string.Empty, run_no_for_modify = string.Empty, modify_flag = string.Empty;

        running_no = param["Onum"].ToString();
        warehouse_code = param["GN"].ToString();
        po_no = param["PO"].ToString();
        barcode_no = param["SN"].ToString();
        scan_time = param["ST"].ToString();
        run_no_for_modify = param["OW"].ToString();
        modify_flag = param["Flag"].ToString();

        _returnString = DataModelUtility.addNewRecord(running_no, po_no, warehouse_code, barcode_no, scan_time, run_no_for_modify, modify_flag);
        _returnString = "ok";
        return _returnString;
    }

    internal static string DeleteOrder(Dictionary<string, object> param)
    {
        string _returnString = string.Empty;
        string running_no = string.Empty;
        running_no = param["Onum"].ToString();
        _returnString = DataModelUtility.deleteRecord(running_no);
        return _returnString;
    }

    internal static string DeleteOrders(Dictionary<string, object> param)
    {
        string _returnString = string.Empty;
        string running_nos = string.Empty;
        running_nos = param["Onum"].ToString();
        string[] _strings = running_nos.Split(',');
        var tmp = "(";
        for (int i = 0; i < _strings.Length; i++)
        {
            tmp += "'" + _strings[i] + "',";
        }
        tmp = tmp.Substring(0, tmp.Length - 1);
        tmp += ")";
        _returnString = DataModelUtility.deleteRecords(tmp);
        _returnString = AddNewOrder(param);
        return _returnString;
    }

    internal static string ReviseOrder(Dictionary<string, object> param)
    {
        string _returnString = string.Empty;
        string running_no = string.Empty;
        running_no = param["Onum"].ToString();
        _returnString = DataModelUtility.deleteRecord(running_no);
        _returnString = AddNewOrder(param);
        return _returnString;
    }

    internal static string Login(Dictionary<string, object> param)
    {
        string _returnString = string.Empty;
        string _userName = string.Empty;
        string _passWord = string.Empty;
        _userName = param["userName"].ToString();
        _passWord = param["passWord"].ToString();
        if (new LogInBLL().VaiLogInInfo(_userName, _passWord))
        {
            _returnString = "ok";
        }
        else
        {
            _returnString = "error";
        }
        return _returnString;
    }

    internal static string ExportAllOrder(Dictionary<string, object> param)
    {
        string _returnString = string.Empty;
        string _newExportFile = TemplateUtil.CreateNewOrderExportExcel();
        if (_newExportFile!=string.Empty)
        {
            _returnString = "ok";
        }
        else
        {
            _returnString = "error";
        }
        return _returnString;
    }
}
