using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
///GarbageDAL 的摘要说明
/// </summary>
public class GarbageDAL
{
	public GarbageDAL()
	{
		
    }

    public void AddGaibageNewInfo(Garbage gb)
    {
        DataClassesDataContext db = new DataClassesDataContext();
        db.Garbage.InsertOnSubmit(gb);
        db.SubmitChanges();
    }


    public Garbage GetGarbageInfoByONum(string num)
    {
        DataClassesDataContext db = new DataClassesDataContext();
        Garbage gb = db.Garbage.Where(g=>g.Onum.Equals(num)).First();
        return gb;

    }



    public Garbage   GarbageInfoByGn(string gn)
    {
        DataClassesDataContext db = new DataClassesDataContext();
        Garbage gb = db.Garbage.Where(g=>g.GN.Equals(gn)).First();

        return gb;

     }

    public Garbage GetGarBageInfoByPo(string po)
    {
        DataClassesDataContext db = new DataClassesDataContext();
        Garbage gb = db.Garbage.Where(g => g.PO_.Equals(po)).First();
        return gb;             
    
    }



    public Garbage GetGarbageBySN(string sn)
    {

        DataClassesDataContext db = new DataClassesDataContext();
        Garbage gb = db.Garbage.Where(g => g.SN.Equals(sn)).First();

        return gb;
    }

    public Garbage GetGarBageByOnumAndGn(string onum, String gn)
    {

        DataClassesDataContext db = new DataClassesDataContext();
        Garbage gb1 = db.Garbage.Where(f => f.Onum.Equals(onum) && f.GN.Equals(gn)).First();
        return gb1;
    }




    public Garbage GetGarBageByOnumAnPo(string onum, String po)
    {

        DataClassesDataContext db = new DataClassesDataContext();
        Garbage gb1 = db.Garbage.Where(f => f.Onum.Equals(onum) || f.PO_.Equals(po)).First();
        return gb1;
    }

    public Garbage GetGarBageByOnumAndSn(string onum, String sn)
    {

        DataClassesDataContext db = new DataClassesDataContext();
        Garbage gb1 = db.Garbage.Where(f => f.Onum.Equals(onum) && f.SN.Equals(sn)).First();
        return gb1;
    }

     /*
    public Garbage GetGarbageInfoByMultipleFactor(string onum,string gn,string po,string sn)
    {
        DataClassesDataContext db = new DataClassesDataContext();
        Garbage gb1 = db.Garbage.Where(f => f.Onum.Equals(onum) || f.GN.Equals(gn) || f.PO_.Equals(po) || f.SN.Equals(sn));

       long  gb1 = db.Garbage.Where(f => f.Onum.Equals(onum)).LongCount();
        return gb1;
    }
    
    */
  
    public long  GetGarbageInfoNum(string onum,string po,string gn,string sn)
    {
        DataClassesDataContext db = new DataClassesDataContext();
        // Garbage gb1 = db.Garbage.Where(f => f.Onum.Equals(onum) || f.GN.Equals(gn) || f.PO_.Equals(po) || f.SN.Equals(sn)).;

        long num = db.Garbage.Where(f => f.Onum.Equals(onum)||(f.PO_.Equals(po) || f.GN.Equals(gn) || f.SN.Equals(sn))).LongCount();
        return num;
    }







   
    public Garbage GetGarBageByGnAndPo(string gn, String po)
    {

        DataClassesDataContext db = new DataClassesDataContext();
        Garbage gb1 = db.Garbage.Where(f => f.GN.Equals(gn) && f.PO_.Equals(po)).First();
        return gb1;
    }

    public Garbage GetGarBageByGnAndSn(string gn, String sn)
    {

        DataClassesDataContext db = new DataClassesDataContext();
        Garbage gb1 = db.Garbage.Where(f => f.GN.Equals(gn) && f.SN.Equals(sn)).First();
        return gb1;
    }


    public Garbage GetGarbageByPoAndSn(string po, string sn)
    {

        DataClassesDataContext db = new DataClassesDataContext();
        Garbage gb1 = db.Garbage.Where(f => f.PO_.Equals(po) && f.SN.Equals(sn)).First();
        return gb1;


    }








   public bool IsContainSn(string sn)
    {

        DataClassesDataContext db = new DataClassesDataContext();
        bool flag =false;

        if ((from s in db.Garbage
             select s.SN).Contains(sn))
        {
            flag =true;

        }
        return flag;
   
    
    }




   public bool IsContainPo(String po)
   {


       DataClassesDataContext db = new DataClassesDataContext();
       bool flag = false;
       if ((from p in db.Garbage
            select p.PO_).Contains(po))
       {
           flag = true;
       }
       return flag;

   }


   public void deleteGarbageInfo(string onum)
   {

       DataClassesDataContext db = new DataClassesDataContext();

       Garbage gb = db.Garbage.Where(n=>n.Onum.Equals(onum)).First();

       db.Garbage.DeleteOnSubmit(gb);
       db.SubmitChanges();

   }



    public  bool IsContainOW(string ow)
    {

        DataClassesDataContext db = new DataClassesDataContext();

        bool flag = false;
        if ((from w in db.Garbage
             select w.Onum).Contains(ow))
        {
            flag = true;
        }

        return flag;

    }



    public bool IsContainOnum(String num)
    {

        DataClassesDataContext db = new DataClassesDataContext();
        bool flag = false;

        if ((from n in db.Garbage
             select n.Onum).Contains(num))
        {

            flag = true;
        }

        return flag;

       

    }


    public bool IsContainGn(String gn)
    {
        DataClassesDataContext db = new DataClassesDataContext();

        bool flag = false;
        if ((from g in db.Garbage
             select g.GN).Contains(gn))
        {
            flag = false;
        }

        return flag;
    }



    public void deleteGarbageIngoByOnum(String num)
    {
        DataClassesDataContext db = new DataClassesDataContext();
        Garbage gb = db.Garbage.Where(d => d.Onum.Equals(num)).First();

        db.Garbage.DeleteOnSubmit(gb);
        db.SubmitChanges();



    }



    public void UpdateGarbageIfoByOnum(string onum,string gn,string po,string sn,string st,string rt,string ow,int flag)
    {
        DataClassesDataContext db = new DataClassesDataContext();


        Garbage gb = db.Garbage.Where(u => u.Onum.Equals(onum)).First();
        gb.GN = gn;
        gb.PO_ = po;
        gb.SN = sn;
        gb.ST = Convert.ToDateTime(st);
        gb.RT =Convert.ToDateTime(rt);
        gb.OW = ow;
        gb.Flag =flag;

      
        db.SubmitChanges();

    }

}