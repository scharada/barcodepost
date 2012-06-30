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

/// <summary>
/// Summary description for RandomUtility
/// </summary>
public class RandomUtility
{
    public static DataTable RandomDataTable()
    {
        DataTable dt = new DataTable();
        DataColumn dcid = new DataColumn("id", typeof(string));
        DataColumn dcname = new DataColumn("name", typeof(string));
        dt.Columns.Add(dcid);
        dt.Columns.Add(dcname);
        for (int i = 0; i < 30; i++)
        {
            DataRow dr = dt.NewRow();
            //foreach (DataColumn dc in dt.Columns)
            //{
            //    dr[dc.ColumnName] = "id" + i;
            //}
            dr["id"] = "id" + i;
            dr["name"] = "name" + i;
            dt.Rows.Add(dr);
        }
        return dt;
    }
}
