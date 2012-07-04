using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class ExportExcel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] != null)
        {
        }
        else
        {
            Response.Redirect("Login.aspx");
        }

        if (Request.Params["fileName"] != null)
        {
            if (Request.Params["fileName"].ToString().Trim() != "")
            {
                string _fileName = Request.Params["fileName"].ToString().Trim();

                string _fullPath = HttpRuntime.AppDomainAppPath + TemplateUtil.m_TemplatePathTaget + _fileName + ".xls";

                ResponseFile(_fileName, _fullPath);
            }
        }
    }

    public bool ResponseFile(string _fileName, string _fullPath)
    {
        try
        {
            FileInfo fileInfo = new FileInfo(_fullPath);

            Response.Clear();

            Response.ClearContent();

            Response.ClearHeaders();

            Response.AddHeader("Content-Disposition", "attachment;filename=" + _fileName + ".xls");

            Response.AddHeader("Content-Length", fileInfo.Length.ToString());

            Response.AddHeader("Content-Transfer-Encoding", "binary");

            Response.ContentType = "application/vnd.ms-excel";

            Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");

            Response.WriteFile(fileInfo.FullName);

            Response.Flush();

            Response.End();

            return true;
        }
        catch (Exception e)
        {
            return false;
        }
        finally
        {

        }

    }

}