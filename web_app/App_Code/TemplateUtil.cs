using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using System.IO;
using System.Data;

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
        string fileName = GetNumberRandom();
        string to_path = HttpRuntime.AppDomainAppPath + m_TemplatePathTaget + fileName + ".xls";
        string b = string.Empty;
        try
        {
            FileHelper.IsExistFile(to_path);
            FileHelper.Copy(from_path, to_path);
            b = fileName;
        }
        catch (Exception)
        {
            throw;
        }
        return b;
    }

    public static bool FillExportOrderExcel(string filePath, string _Onum, string _GN, string _PO, string _SN, string _FromDateTime, string _ToDateTime)
    {
        bool b = false;
        try
        {
            DataTable funcDs = DataModelUtility.getOrderByComposeForExport(_Onum, _GN, _PO, _SN, _FromDateTime, _ToDateTime);
            //for循环
            if (!System.IO.File.Exists(filePath))
                return b;
            ExcelHelper _excelHelper = new ExcelHelper(filePath);
            _excelHelper.DataTableToExcel(funcDs, 1, 1);
            _excelHelper.SaveFile();
            b = true;
        }
        catch (Exception)
        {
            
            throw;
        }
      
        return b;
    }

    ///   <summary>  
    ///   删除过期的文件。  
    ///   </summary>  
    public static void ClearDateOutExcel()
    {
        //先取得文件夹中的文件列表  
        string[] fileEntries = System.IO.Directory.GetFiles(HttpRuntime.AppDomainAppPath + m_TemplatePathTaget);
        foreach (string singFiles in fileEntries)//遍历文件列表  
        {
            //将文件的生成日期与系统日期相比，如果是1小时以前生成的文件，删除它  
            if (System.DateTime.Compare(System.IO.File.GetCreationTime(singFiles).AddHours(1), System.DateTime.Now) < 0)
            {
                try
                {
                    System.IO.File.Delete(singFiles);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
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