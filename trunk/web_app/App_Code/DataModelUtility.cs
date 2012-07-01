using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
///DataModel 的摘要说明
/// </summary>
public class DataModelUtility
{
    /// <summary>
    /// ?
    /// </summary>
    public static string Field_Onum = "Onum";

    /// <summary>
    /// 订单号
    /// </summary>
    public static string Field_PO = "PO#";

    /// <summary>
    /// ?
    /// </summary>
    public static string Field_GN = "GN";

    /// <summary>
    /// 扫描条码
    /// </summary>
    public static string Field_SN = "SN";

    /// <summary>
    /// 扫描时间
    /// </summary>
    public static string Field_ST = "ST";

    /// <summary>
    /// ?时间
    /// </summary>
    public static string Field_RT = "RT";

    /// <summary>
    /// ?
    /// </summary>
    public static string Field_OW = "OW";

    /// <summary>
    /// 修改操作标识
    /// </summary>
    public static string Field_Flag = "Flag";

    public static string Table_Garbage = "Garbage";

    public static string DEFAULT_STRING = "NULL";

    //错误码
    public static string RETURN_NORMAL = "0";
    public static string RETURN_NULL_PRIMARY_KEY = "1";
    public static string RETURN_DUPLICATE_PRIMARY_KEY = "2";
    public static string RETURN_DUPLICATE_PO_SN = "3";
    public static string RETURN_INVALID_MODIFY_FLAG = "4";
    public static string RETURN_DELETE_INVALID_RECORD = "21";

    public static string Sql = String.Format("SELECT {0},{1},{2} AS PO,{3},{4},{5},{6}, {7}  FROM {8} ", Field_Onum, Field_GN, Field_PO, Field_SN, Field_ST, Field_RT, Field_OW, Field_Flag, Table_Garbage);

    public static bool getExistRunning_no(string Running_no)
    {
        bool _e = false;
        return _e;
    }

    public static DataTable getAllOrder()
    {
        DataTable _dt = null;
        string _sql = Sql + " order by " + Field_RT+" desc";
        _dt = DbHelperSQL.DoQueryEx("a", _sql, true);
        return _dt;
    }

    /// <summary>
    /// 通过  查所有订单
    /// </summary>
    /// <param name="Onum"></param>
    /// <returns></returns>
    public static DataTable getAllOrderByOnum(string Onum)
    {
        DataTable _dt = null;
        string _sql = Sql + " where " + Field_Onum + " = '" + Onum + "'";
        _dt = DbHelperSQL.DoQueryEx("a", _sql, true);
        return _dt;
    }

    public static DataTable getOrderNull()
    {
        DataTable _dt = null;
        string _sql = Sql + " WHERE " + Field_Onum + " ='-1111111111'" + " ORDER BY " + Field_RT;
        _dt = DbHelperSQL.DoQueryEx("b", _sql, true);
        return _dt;
    }

    internal static DataTable getOrderByCompose(string _Onum, string _GN, string _PO, string _SN, string _FromDateTime, string _ToDateTime)
    {
        string _whereCause = string.Empty;
        if (_Onum != string.Empty)
        {
            _whereCause += Field_Onum + " LIKE '%" + _Onum + "%' AND ";
        }
        if (_GN != string.Empty)
        {
            _whereCause += Field_GN + " LIKE '%" + _GN + "%'  AND ";
        }
        if (_PO != string.Empty)
        {
            _whereCause += Field_PO + " LIKE '%" + _PO + "%' AND ";
        }
        if (_SN != string.Empty)
        {
            _whereCause += Field_SN + " LIKE '%" + _SN + "%' AND ";
        }
        if (_FromDateTime != string.Empty)
        {
            string _s = _FromDateTime;
            _FromDateTime = _s.Substring(0, 4) + "-" + _s.Substring(4, 2) + "-" + _s.Substring(6, 2) + " " + _s.Substring(8, 2) + ":" + _s.Substring(10, 2) + ":" + _s.Substring(12, 2);
            _whereCause += Field_RT + " >= '" + _FromDateTime + "' AND ";
        }
        if (_ToDateTime != string.Empty)
        {
            string _s = _ToDateTime;
            _ToDateTime = _s.Substring(0, 4) + "-" + _s.Substring(4, 2) + "-" + _s.Substring(6, 2) + " " + _s.Substring(8, 2) + ":" + _s.Substring(10, 2) + ":" + _s.Substring(12, 2);
            _whereCause += Field_RT + " <= '" + _ToDateTime + "' AND ";
        }
        _whereCause = _whereCause.Trim().TrimEnd("AND".ToCharArray());
        DataTable _dt = null;
        string _sql = Sql + " WHERE " + _whereCause + " ORDER BY " + Field_RT + " DESC";
        _dt = DbHelperSQL.DoQueryEx("b", _sql, true);
        return _dt;
    }

    internal static string deleteRecord(string running_no)
    {
        string _returnString = string.Empty;
        running_no = running_no.Trim();
        string _sql = "delete from " + Table_Garbage + " where " + Field_Onum + "='" + running_no + "'";
        if (DbHelperSQL.ExecuteSql(_sql) > 0)
        {
            _returnString += "ok";
        }
        else
            _returnString += "error";
        return _returnString;
    }

    internal static string deleteRecords(string running_nos)
    {
        string _returnString = string.Empty;
        string _sql = "DELETE FROM " + Table_Garbage + " WHERE " + Field_Onum + " IN " + running_nos;
        if (DbHelperSQL.ExecuteSql(_sql) > 0)
        {
            _returnString += "ok";
        }
        else
            _returnString += "error";
        return _returnString;
    }

    public static string addNewRecord(string running_no, string po_no, string warehouse_code, string barcode_no, string scan_time, string run_no_for_modify, string modify_flag)
    {
        string _returnString = string.Empty;
        Garbage gb = new Garbage();
        if (new GarbageBLL().IsContainOnum(running_no))
        {
            _returnString = getResponseString(RETURN_DUPLICATE_PRIMARY_KEY, running_no, po_no, warehouse_code, barcode_no, scan_time, run_no_for_modify, modify_flag);
            return _returnString;
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
            _returnString = getResponseString(RETURN_NORMAL, running_no, po_no, warehouse_code, barcode_no, scan_time, run_no_for_modify, modify_flag);
            return _returnString;
        }
    }

    public static string getResponseString(string error_string, string running_no, string po_no, string warehouse_code, string barcode_no, string scan_time, string run_no_for_modify, string modify_flag)
    {
        string _returnString = string.Empty;
        _returnString = "error=" + error_string + ",running_no=" + running_no + ",warehouse_code=" + warehouse_code + ",po_no=" + po_no + ",barcode_no=" + barcode_no + ",scan_time=" + scan_time + ",run_no_for_modify=" + run_no_for_modify + ",modify_flag=" + modify_flag;
        return _returnString;
    }

}