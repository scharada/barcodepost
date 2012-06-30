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
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

/// <summary>
/// Summary description for JsonUtility
/// </summary>
public class JsonUtility
{
    public static string GetOperType(string json)
    {
        string opertype = string.Empty;
        if (json.Contains("&"))
        {
            opertype = GetUrlOperType(json);
        }
        else
            opertype = GetJsonOperType(json);
        return opertype;
    }

    public static Dictionary<string, object> GetContentInfo(string json)
    {
        Dictionary<string, object> info = new Dictionary<string, object>();
        if (StringUtility.DecideUrlParam(json))
        {
            info = GetUrlContentInfo(json);
        }
        else
            info = GetJsonContentInfo(json);
        return info;
    }
    private static Dictionary<string, object> GetUrlContentInfo(string json)
    {
        Dictionary<string, object> info = new Dictionary<string, object>();
        List<object> fiterString = new List<object>();
        //fiterString.Add("opertype");
        //fiterString.Add("page");
        //fiterString.Add("rows");
        info = StringUtility.PaserPattern(json, fiterString);
        return info;
    }
    private static Dictionary<string, object> GetJsonContentInfo(string json)
    {
        Dictionary<string, object> info = new Dictionary<string, object>();
        IEnumerable<JProperty> propers = GetProperties(json);
        if (propers != null)
        {
            foreach (JProperty proper in propers)
            {
                if (proper.Name.ToLower() != "opertype" && proper.Value.HasValues)
                {
                    IEnumerable<JProperty> contents = GetProperties(proper.Value.ToString());
                    foreach (JProperty content in contents)
                    {
                        info.Add(content.Name, StringUtility.RemoveQuotation(content.Value.ToString()));
                    }
                }
                if (proper.Name.ToLower() != "opertype" && !proper.Value.HasValues)
                {
                    info.Add(proper.Name, StringUtility.RemoveQuotation(proper.Value.ToString()));
                }
            }
        }
        return info;
    }
    private static string GetUrlOperType(string urloper)
    {
        string opertype = string.Empty;
        string[] arrs = StringUtility.ParseArray(urloper, '&');
        foreach (string s in arrs)
        {
            string[] arr = StringUtility.ParseArray(s, '=');
            if (arr[0].ToLower() == "opertype")
            {
                opertype = arr[1];
                break;
            }
        }
        return opertype;
    }
    private static string GetJsonOperType(string json)
    {
        IEnumerable<JProperty> propers = GetProperties(json);
        string opertype = string.Empty;
        if (propers != null)
        {
            foreach (JProperty proper in propers)
            {
                if (proper.Name.ToLower() == "opertype")
                {
                    opertype = StringUtility.RemoveQuotation(proper.Value.ToString());
                    break;
                }
            }
        }
        return opertype;
    }
    private static IEnumerable<JProperty> GetProperties(string json)
    {
        IEnumerable<JProperty> proper = null;
        JObject o = ParseJson(json);
        if (o != null)
        {
            proper = o.Properties();
        }
        return proper;
    }
    private static JObject ParseJson(string json)
    {
        JObject o = null;
        try
        {
            o = JObject.Parse(json);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return o;
    }
    public static string GetErrorJson(string error)
    {
        string errorinfo = string.Empty;
        JObject so = new JObject();
        so.Add(new JProperty("result", false));
        so.Add(new JProperty("msg", error));
        errorinfo = JavaScriptConvert.SerializeObject(so);
        return errorinfo;
    }
    public static string GetSuccessJson(string success)
    {
        string successinfo = string.Empty;
        JObject so = new JObject();
        so.Add(new JProperty("result", true));
        so.Add(new JProperty("msg", success));
        successinfo = JavaScriptConvert.SerializeObject(so);
        return successinfo;
    }
    public static string GetSuccessJson(Dictionary<string, object> prepareJson)
    {
        return SerializeObject(true, prepareJson);
    }
    private static string SerializeObject(bool flg, Dictionary<string, object> prepareJson)
    {
        prepareJson.Add("result", flg);
        return SerializeObject(prepareJson);
    }

    public static string SerializeObject(Dictionary<string, object> prepareJson)
    {
        string json = string.Empty;
        JObject so = new JObject();
        string listStr = "System.Collections.Generic.List`1[System.String]";
        foreach (KeyValuePair<string, object> dic in prepareJson)
        {
            if (dic.Value.GetType().IsGenericType)
            {

                Type listType = dic.Value.GetType().GetInterface("IList");
                if (listType != null)
                {
                    Type t = dic.Value.GetType();
                    if (dic.Value.GetType() == Type.GetType(listStr))
                    {
                        JArray ja = new JArray();
                        List<string> strList = (List<string>)dic.Value;
                        foreach (string str in strList)
                        {
                            ja.Add(str);
                        }
                        so.Add(new JProperty(dic.Key, (JToken)ja));
                    }
                    else
                    {
                        List<Dictionary<string, object>> list = (List<Dictionary<string, object>>)dic.Value;
                        JArray ja = new JArray();
                        foreach (Dictionary<string, object> dicother in list)
                        {
                            JObject data = new JObject();
                            foreach (KeyValuePair<string, object> d in dicother)
                            {
                                if (string.IsNullOrEmpty(d.Value.ToString()))
                                    data.Add(new JProperty(d.Key, string.Empty));
                                else
                                    data.Add(new JProperty(d.Key, d.Value));
                            }
                            ja.Add(data);
                        }
                        so.Add(new JProperty(dic.Key, (JToken)ja));
                    }
                }
                else
                {
                    Type dicType = dic.Value.GetType().GetInterface("IDictionary");
                    if (dicType != null)
                    {
                        Dictionary<string, object> dicother = (Dictionary<string, object>)dic.Value;
                        JArray ja = new JArray();
                        JObject data = new JObject();
                        foreach (KeyValuePair<string, object> d in dicother)
                        {
                            if (d.Value.GetType() == Type.GetType(listStr))
                            {
                                List<string> strList = (List<string>)d.Value;
                                JArray jastr = new JArray();
                                foreach (string str in strList)
                                {
                                    jastr.Add(str);
                                }
                                data.Add(new JProperty(d.Key, (JToken)jastr));
                            }
                            else
                            {
                                if (string.IsNullOrEmpty(d.Value.ToString()))
                                    data.Add(new JProperty(d.Key, string.Empty));
                                else
                                    data.Add(new JProperty(d.Key, d.Value));
                            }
                        }
                        ja.Add(data);
                        so.Add(new JProperty(dic.Key, (JToken)ja));
                    }
                }
            }
            else
            {
                if (string.IsNullOrEmpty(dic.Value.ToString()))
                    so.Add(new JProperty(dic.Key, string.Empty));
                else
                    so.Add(new JProperty(dic.Key, dic.Value));
            }
        }
        json = JavaScriptConvert.SerializeObject(so);
        return json;
    }
    private static string SerializeObject(string key, string value)
    {
        JObject so = new JObject();
        so.Add(new JProperty(key, value));
        string json = JavaScriptConvert.SerializeObject(so);
        return json;
    }
}
