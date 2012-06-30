using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
///LogInDAL 的摘要说明
/// </summary>
public class LogInDAL
{
	public LogInDAL()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}


    public bool ValLoginInfo(string id,string pwd)
    {
        DataClassesDataContext db = new DataClassesDataContext();

        bool flag = false;

        try
        {
            manger mg = db.manger.Where(m => m.id.Equals(id) && m.pwd.Equals(pwd)).First();
            flag = true;
        
        }
        catch
        {
            flag = false;
        }
        return flag;


    }
}