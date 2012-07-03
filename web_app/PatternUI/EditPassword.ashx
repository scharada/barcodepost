<%@ WebHandler Language="C#" Class="EditPassword" %>

using System;
using System.Web;
using System.Web.SessionState;


public class EditPassword : IHttpHandler, IRequiresSessionState
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

                if (PasswordUtil.EditPassword(_userName, _passWord) == "ok_edit")
                {
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