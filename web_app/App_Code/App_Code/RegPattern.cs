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
/// Summary description for RegPattern
/// </summary>
public class RegPattern
{
    public static string UrlPattern = @"(\w+)=(\w*[\d\/ \d\:]*)";//@"(\w+)=(\w*)";
    public static string UrlArrayPattern = @"[\[(\d*)\]]*\[(\w+)\]=(\w*)";
    public static string UrlDecidePatten = @"\]*=(\w*)&";
    public static string UrlDecideArrayPatten = @"\]=(\w*)&";
}
