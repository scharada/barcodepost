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
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Collections;

/// <summary>
/// Summary description for StringUtility
/// </summary>
public class StringUtility
{
    public static string[] ParseArray(string source, params char[] splitchar)
    {
        return source.Split(splitchar);
    }
    public static Dictionary<string, object> ParseDictionary(string json)
    {
        Dictionary<string, object> dic = new Dictionary<string, object>();
        return dic;
    }
    public static Hashtable ParseHashtable(Dictionary<string, object> parames)
    {
        Hashtable ht = new Hashtable();
        if (parames.Count > 0)
        {
            foreach (string key in parames.Keys)
            {
                ht.Add(key, parames[key]);
            }
        }
        return ht;
    }
    public static string UrlDecode(string data)
    {
        return HttpUtility.UrlDecode(data);
    }
    public static string UrlEnCode(string data)
    {
        return HttpUtility.UrlEncode(data);
    }
    public static string RemoveQuotation(string data)
    {
        return data.Replace("\"", "").Replace("'", "").Trim();
    }
    public static Dictionary<string, object> PaserPattern(string text, List<object> args)
    {
        Dictionary<string, object> dic = new Dictionary<string, object>();
        string pattern = string.Empty;
        if (DecideArrayParam(text))
            pattern = RegPattern.UrlArrayPattern;
        else
            pattern = RegPattern.UrlPattern;
        if (!string.IsNullOrEmpty(pattern))
        {
            Regex reg = new Regex(pattern, RegexOptions.IgnoreCase);
            MatchCollection matches = reg.Matches(text);
            int len = args.Count;
            foreach (Match match in matches)
            {
                GroupCollection gc = match.Groups;
                if (len > 0 && gc.Count > 0)
                {
                    if (!args.Contains(gc[1].Value))
                    {
                        dic.Add(gc[1].Value, gc[2].Value);
                    }
                }
                if (len == 0 && gc.Count > 0)
                {
                    if (!args.Contains(gc[1].Value))
                    {
                        dic.Add(gc[1].Value, gc[2].Value);
                    }
                }
            }
        }
        return dic;
    }
    public static bool DecideUrlParam(string param)
    {
        bool flg = false;
        Regex reg = new Regex(RegPattern.UrlDecidePatten, RegexOptions.IgnoreCase);
        MatchCollection matches = reg.Matches(param);
        if (matches.Count > 0)
        {
            flg = true;
        }
        return flg;
    }
    private static bool DecideArrayParam(string param)
    {
        bool flg = false;
        Regex reg = new Regex(RegPattern.UrlDecideArrayPatten, RegexOptions.IgnoreCase);
        MatchCollection matches = reg.Matches(param);
        if (matches.Count > 0)
        {
            flg = true;
        }
        return flg;
    }
}
