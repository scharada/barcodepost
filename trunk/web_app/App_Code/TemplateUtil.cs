using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using System.IO;

/// <summary>
///TemplateUtil 的摘要说明
/// </summary>
public class TemplateUtil
{
    public static string m_TemplatePath = @"Template\orderTemplate.xls";

    public static string m_TemplatePathTaget = @"OrderDatum\";

    public TemplateUtil()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }
    public static string CreateNewOrderExportExcel()
    {
        string from_path = HttpRuntime.AppDomainAppPath + m_TemplatePath;
        string to_path = HttpRuntime.AppDomainAppPath + m_TemplatePathTaget + GetNumberRandom() + ".xls";
        string b = string.Empty;
        try
        {
            FileHelper.IsExistFile(to_path);
            FileHelper.Copy(from_path, to_path);
            b = to_path;
        }
        catch (Exception)
        {
            throw;
        }
        return b;
    }

    public static string GetNumberRandom()
    {
        int mikecat_intNum;
        long mikecat_lngNum;
        string mikecat_strNum = System.DateTime.Now.ToString();
        mikecat_strNum = mikecat_strNum.Replace(":", "");
        mikecat_strNum = mikecat_strNum.Replace("-", "");
        mikecat_strNum = mikecat_strNum.Replace(" ", "");
        mikecat_lngNum = long.Parse(mikecat_strNum);
        System.Random mikecat_ran = new Random();
        mikecat_intNum = mikecat_ran.Next(1, 99999);
        mikecat_ran = null;
        mikecat_lngNum += mikecat_intNum;
        return mikecat_lngNum.ToString();
    }
    
}