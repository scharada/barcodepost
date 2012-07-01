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
using System.Collections.Generic;
using System.Collections;

/// <summary>
/// Summary description for BLLFactory
/// </summary>
public class BLLFactory
{
    public static string Json(string parameters)
    {
        string json = string.Empty;
        string jsondata = StringUtility.UrlDecode(parameters);
        char[] c = "data=".ToCharArray();
        jsondata = jsondata.TrimStart(c);
        json = getJsonFromPara(jsondata);
        return json;
    }

    private static string getJsonFromPara(string jsondata)
    {
        string _json = string.Empty;
        Dictionary<string, object> param = JsonUtility.GetContentInfo(jsondata);
        string typestr = JsonUtility.GetOperType(jsondata).ToLower();
        if (typestr == string.Empty)
        {
            _json = JsonUtility.GetErrorJson("no operator type!");
            return _json;
        }

        Hashtable ht = StringUtility.ParseHashtable(param);
        switch (typestr)
        {

            case "getallorder":
                _json = BllDataGrid.ShowAllOrder(ht);
                break;
            case "getorderby":
                _json = BllDataGrid.GetOrderByCompose(ht);
                break;
            case "getnull":
                _json = BllDataGrid.GetOrderNull(ht);
                break;
            case "addorder":
                _json = BllDataGrid.AddNewOrder(param);
                break;
            case "deleteorder":
                _json = BllDataGrid.DeleteOrder(param);
                break;
            case "deleteorders":
                _json = BllDataGrid.DeleteOrders(param);
                break;
            case "reviseorder":
                _json = BllDataGrid.ReviseOrder(param);
                break;
            case "login":
                _json = BllDataGrid.Login(param);
                break;

                
                
        }
        return _json;
    }
}
