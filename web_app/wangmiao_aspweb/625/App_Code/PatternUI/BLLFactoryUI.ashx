<%@ WebHandler Language="C#" Class="BLLFactoryUI" %>

using System;
using System.Web;

public class BLLFactoryUI : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        context.Response.Cache.SetCacheability(HttpCacheability.ServerAndNoCache);
        string json = BLLFactory.Json(context.Request.Form.ToString());
        context.Response.Write(json);
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}