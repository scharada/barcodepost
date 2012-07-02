<%@ WebHandler Language="C#" Class="Login" %>

using System;
using System.Web;
using System.Web.SessionState;

public class Login : IHttpHandler, IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        string json = string.Empty;
        if (context.Request.Form["userName"] != null && context.Request.Form["passWord"] != null)
        {
            if (context.Request.Form["userName"].ToString().Trim() != "" && context.Request.Form["passWord"].ToString() != "")
            {
                string _userName = context.Request.Form["userName"].ToString().Trim();
                string _passWord = context.Request.Form["passWord"].ToString();

                if (new LogInBLL().VaiLogInInfo(_userName, _passWord))
                {
                    context.Session["userName"] = _userName;
                    json = "ok";
                }
                else
                    json = "error";
            }
        }
        context.Response.Write(json);
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}