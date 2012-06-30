using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
///GarbageBLL 的摘要说明
/// </summary>
public class GarbageBLL
{
	public GarbageBLL()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}



    public Garbage GetGaibageInfoByOnum(String num)
    {

        return new GarbageDAL().GetGarbageInfoByONum(num);
    }


    public Garbage GarbageInfoByGn(string gn)
    {

        return new GarbageDAL().GarbageInfoByGn(gn);
    }


    public Garbage GarbageInfoBYPo(string po)
    {

        return new GarbageDAL().GetGarBageInfoByPo(po);
    }


    public void AddGaibageNewInfo(Garbage gb)
    {

        new GarbageDAL().AddGaibageNewInfo(gb);
    }

    public Garbage GetGarbageBySN(string sn)
    {
       return new GarbageDAL().GetGarbageBySN(sn);
    }

    public bool IsContainSn(string sn)
    {
        return new GarbageDAL().IsContainSn(sn);
    }

    public bool IsContainPo(String po)
    {
        return new GarbageDAL().IsContainPo(po);
    }


    public void deleteGarbageInfo(string onum)
    {
        new GarbageDAL().deleteGarbageInfo(onum);
    }

    public bool IsContainOW(string ow)
    {
        return new GarbageDAL().IsContainOW(ow);
    }



    public bool IsContainOnum(String num)
    {

        return new GarbageDAL().IsContainOnum(num);
    }

    public void deleteGarbageIngoByOnum(String num)
    {
        new GarbageDAL().deleteGarbageIngoByOnum(num);
    }

    public void UpdateGarbageIfoByOnum(string onum, string gn, string po, string sn, string st, string rt, string ow, int flag)
    {

        new GarbageDAL().UpdateGarbageIfoByOnum(onum,gn,po,sn,st,rt,ow,flag);
    }


    /*public Garbage GetGarbageInfoByMultipleFactor(string onum, string gn, string po, string sn)
    {
        return new GarbageDAL().GetGarbageInfoByMultipleFactor(onum,gn,po,sn);
    }
    */


    public bool IsContainGn(String gn)
    {
        return new GarbageDAL().IsContainGn(gn);
    }
}