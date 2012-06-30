using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
///LogInBLL 的摘要说明
/// </summary>
public class LogInBLL
{
	public LogInBLL()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}


    public bool VaiLogInInfo(String id, String pwd)
    {

        return new LogInDAL().ValLoginInfo(id,pwd);
    }
}