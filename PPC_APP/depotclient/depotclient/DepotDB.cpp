#include "StdAfx.h"
#include "DepotDB.h"
#include <windows.h>



CDepotDB& CDepotDB::GetDB()
{
	static CDepotDB db;
	return db;
}

CString CDepotDB::GetDBPath()
{
	TCHAR filename[128];
	CString ret;
	GetModuleFileNameW(NULL,filename,sizeof(filename)/sizeof(filename[0]));
	wchar_t * p = wcsrchr(filename,L'\\');
	p[1] = 0;
	ret.Format(L"%s",filename);
	return ret;
}

CDepotDB::CDepotDB(void):m_CurrentDepoID(-1)
{
}

CDepotDB::~CDepotDB(void)
{
}

bool CDepotDB::OpenDB(CString& FileName)
{
	CStringA	str;
	bool		ret = true;;
	char		filename[128];

	str.Format("%S",FileName);
	sprintf(filename,"%s",str);

	try
	{
		m_DepotDB.open(filename,SQLITE_OPEN_READWRITE);
	}
	catch(CppSQLite3Exception& e)
	{
		m_ErrMsg.Format(L"%S",e.errorMessage());
		ret = false;
	}
	
	return ret;
}

bool CDepotDB::GetDepoList(CArray<CString>& array)
{
	//if(!m_DepotDB.IsDBConnected())
	{
	//	m_ErrMsg.Format(L"%S","DB not conneted");
	//	return false;
	}
	CppSQLite3Query q;

	try
	{
		DWORD tid = GetCurrentThreadId();
		q = m_DepotDB.execQuery("select name from depository;");
	}
	catch(CppSQLite3Exception & e)
	{
		char msg[128];
		printf(msg,"%s",e.errorMessage());
		int aa=0;
	}

	

	const char* fn = q.fieldName(0);
	while (!q.eof())
	{
		WCHAR wstr[128]={0};
		const char* depo = q.getStringField(0);
		MultiByteToWideChar(
			CP_UTF8,         // code page
			0,         // character-type options
			depo, // string to map
			strlen(depo),       // number of bytes in string
			wstr,  // wide-character buffer
			sizeof(wstr)/sizeof(wstr[0])        // size of buffer
			);
		array.Add(CString(wstr));
		q.nextRow();
	}
	q.finalize();

	return true;
}

bool CDepotDB::GetPoList(CArray<CString>& array)
{
	if(!m_DepotDB.IsDBConnected())
	{
		m_ErrMsg.Format(L"%S","DB not conneted");
		return false;
	}
	char qrystr[256]={0};
	sprintf(qrystr,"select po_no from po");
	CppSQLite3Query q = m_DepotDB.execQuery(qrystr);

	const char* fn = q.fieldName(0);
	while (!q.eof())
	{
		WCHAR wstr[128]={0};
		const char* po = q.getStringField(0);
		MultiByteToWideChar(
			CP_UTF8,         // code page
			0,         // character-type options
			po, // string to map
			strlen(po),       // number of bytes in string
			wstr,  // wide-character buffer
			sizeof(wstr)/sizeof(wstr[0])        // size of buffer
			);
		array.Add(CString(wstr));
		q.nextRow();
	}
	q.finalize();

	return true;
}

bool CDepotDB::AddPo(CString& pono)
{
	if(!m_DepotDB.IsDBConnected())
	{
		m_ErrMsg.Format(L"%S","DB not conneted");
		return false;
	}

	//检查 是否已经存在
	bool exist=false;
	char qrystr[256]={0};
	char pono_asc[128]={0};

	WideCharToMultiByte(
		CP_UTF8,         // code page
		0,       
		pono, 
		pono.GetLength(),       
		pono_asc,  
		sizeof(pono_asc) ,
		NULL,
		NULL
		);

	sprintf(qrystr,"select po_id from po where po_no=\"%s\" ",pono_asc);

	CppSQLite3Query q = m_DepotDB.execQuery(qrystr);
	while (!q.eof())
	{
		break;
		exist = true;
	}
	q.finalize();

	if(exist)//如果已经存在 不做任何事情
	{
		return true;
	}

	m_DepotDB.execDML("begin");

	sprintf(qrystr,"insert into po (po_id,po_no) values ((select po_base+1 from run_no),\"%s\");",pono_asc);

	bool sql_ret=true;
	try
	{
		m_DepotDB.execDML(qrystr);
	}
	catch(CppSQLite3Exception& e)
	{
		sql_ret=false;
		m_ErrMsg.Format(L"%S",e.errorMessage());
	}
	if(sql_ret)
	{
		sprintf(qrystr,"update run_no set po_base=(select po_base+1 from run_no) where 1");

		try
		{
			m_DepotDB.execDML(qrystr);
		}
		catch(CppSQLite3Exception& e)
		{
			sql_ret=false;
			m_ErrMsg.Format(L"%S",e.errorMessage());
		}
	}


	if(sql_ret)
		m_DepotDB.execDML("commit");
	else
		m_DepotDB.execDML("rollback");

	return sql_ret;
}

bool CDepotDB::DelPo(CString& pono)
{
	if(!m_DepotDB.IsDBConnected())
	{
		m_ErrMsg.Format(L"%S","DB not conneted");
		return false;
	}

	//检查 是否已经存在
	bool exist=false;
	char qrystr[256]={0};
	char pono_asc[128]={0};
	int po_id;

	WideCharToMultiByte(
		CP_UTF8,         // code page
		0,       
		pono, 
		pono.GetLength(),       
		pono_asc,  
		sizeof(pono_asc) ,
		NULL,
		NULL
		);

	sprintf(qrystr,"select po_id from po where po_no=\"%s\"",pono_asc); 

	CppSQLite3Query q = m_DepotDB.execQuery(qrystr);
	while (!q.eof())
	{
		po_id = q.getIntField(0);
		exist = true;
		break;
	}
	q.finalize();

	if(!exist)//不存在
	{
		return true;
	}

	//m_DepotDB.execDML("begin;");

	/*sprintf(qrystr,"update po_order set del_flag=1 where depo_id=%d and po_id=%d",
					m_CurrentDepoID,po_id);
	
	bool sql_ret=true;
	try
	{
		m_DepotDB.execDML(qrystr);
	}
	catch(CppSQLite3Exception& e)
	{
		sql_ret=false;
		m_ErrMsg.Format(L"%S",e.errorMessage());
	}*/
	bool sql_ret=true;

	if(sql_ret)
	{
		sprintf(qrystr,"delete from po where po_id=%d",po_id);

		try
		{
			m_DepotDB.execDML(qrystr);
		}
		catch(CppSQLite3Exception& e)
		{
			sql_ret=false;
			m_ErrMsg.Format(L"%S",e.errorMessage());
		}
	}
/*
	if(!sql_ret)
	{
		m_DepotDB.execDML("rollback;");

	}
	else
	{

		m_DepotDB.execDML("commit;");
	}
*/
	return sql_ret;
}

bool CDepotDB::ModPo(CString& pono,CString& new_pono)
{
	if(!m_DepotDB.IsDBConnected())
	{
		m_ErrMsg.Format(L"%S","DB not conneted");
		return false;
	}

	//检查 是否已经存在
	bool exist=false;
	int po_id;
	char qrystr[256]={0};
	char pono_asc[128]={0};
	char new_pono_asc[128]={0};

	WideCharToMultiByte(
		CP_UTF8,         // code page
		0,       
		pono, 
		pono.GetLength(),       
		pono_asc,  
		sizeof(pono_asc) ,
		NULL,
		NULL
		);
	WideCharToMultiByte(
		CP_UTF8,         // code page
		0,       
		new_pono, 
		new_pono.GetLength(),       
		new_pono_asc,  
		sizeof(new_pono_asc) ,
		NULL,
		NULL
		);

	sprintf(qrystr,"select po_id from po where po_no=\"%s\"",pono_asc);

	CppSQLite3Query q = m_DepotDB.execQuery(qrystr);
	while (!q.eof())
	{
		po_id = q.getIntField(0);
		exist = true;
		break;
	}
	q.finalize();

	if(!exist)//不存在
	{
		return false;
	}


	sprintf(qrystr,"update po set po_no=\"%s\" where po_id=%d",new_pono_asc,po_id);

	bool sql_ret=true;
	try
	{
		m_DepotDB.execDML(qrystr);
	}
	catch(CppSQLite3Exception& e)
	{
		sql_ret=false;
		m_ErrMsg.Format(L"%S",e.errorMessage());
	}

	return sql_ret;
}

bool CDepotDB::CheckPassWd( CString depoName,CString passWd )
{
	if(!m_DepotDB.IsDBConnected())
	{
		m_ErrMsg.Format(L"%S","DB not conneted");
		return false;
	}

	bool ret = false;
	char query[128];
	char name[128]={0};
	char passwd[128]={0};

	WideCharToMultiByte(
		CP_UTF8,         // code page
		0,       
		depoName, 
		depoName.GetLength(),       
		name,  
		sizeof(name) ,
		NULL,
		NULL
		);
	WideCharToMultiByte(
		CP_UTF8,         // code page
		0,        
		passWd, 
		passWd.GetLength(),       
		passwd, 
		sizeof(passwd),
		NULL,
		NULL
		);

	sprintf(query,"select id from depository where name=\"%s\" and passwd=\"%s\";",name,passwd);

	CppSQLite3Query q = m_DepotDB.execQuery(query);

	while (!q.eof())
	{
		m_CurrentDepoID = q.getIntField(0);
		ret = true;
		break;
	}
	q.finalize();

	return ret;
}

bool CDepotDB::GetBarCodeList(CString& PoNo,CArray<CString>& array)
{
	if(!m_DepotDB.IsDBConnected())
	{
		m_ErrMsg.Format(L"%S","DB not conneted");
		return false;
	}

	bool ret = true;
	char query[256];
	char po_no[128]={0};
	WCHAR w_barcode[128]={0};

	WideCharToMultiByte(
		CP_UTF8,         // code page
		0,       
		PoNo, 
		PoNo.GetLength(),       
		po_no,  
		sizeof(po_no) ,
		NULL,
		NULL
		);

	sprintf(query,"select barcode_no from po_order where po_id=(select po_id from po where po_no=\"%s\") and depo_id=%d and del_flag=0",
					po_no,m_CurrentDepoID);

	CppSQLite3Query q = m_DepotDB.execQuery(query);

	while (!q.eof())
	{
		if(!q.fieldIsNull(0))
		{
			const char* barcode = q.getStringField(0);
			memset(w_barcode,0,sizeof(w_barcode));
			MultiByteToWideChar(
				CP_UTF8,         // code page
				0,         // character-type options
				barcode, // string to map
				strlen(barcode),       // number of bytes in string
				w_barcode,  // wide-character buffer
				sizeof(w_barcode)/2        // size of buffer
				);
			array.Add(CString(w_barcode));
		}
		q.nextRow();
	}
	q.finalize();
	return ret;
}


bool CDepotDB::AddBarCode(CString& PoNo,CString& BarCode)
{
	if(!m_DepotDB.IsDBConnected())
	{
		m_ErrMsg.Format(L"%S","DB not conneted");
		return false;
	}

	bool ret = true;
	char query[256];
	char po_no[128]={0};
	char barcode[128]={0};
	WCHAR w_barcode[128]={0};

	WideCharToMultiByte(
		CP_UTF8,         // code page
		0,       
		PoNo, 
		PoNo.GetLength(),       
		po_no,  
		sizeof(po_no) ,
		NULL,
		NULL
		);

	WideCharToMultiByte(
		CP_UTF8,         // code page
		0,       
		BarCode, 
		BarCode.GetLength(),       
		barcode,  
		sizeof(barcode) ,
		NULL,
		NULL
		);

	CTime time = CTime::GetCurrentTime();;
	CStringA time_string_a;
	time_string_a.Format("%04d-%02d-%02d %02d:%02d:%02d",
		time.GetYear(),time.GetMonth(),time.GetDay(),time.GetHour(),time.GetMinute(),time.GetSecond());

	sprintf(query,"insert into po_order (depo_id,po_id,barcode_no,scan_time) values (%d,(select po_id from po where po_no=\"%s\"),\
				  \"%s\",\"%s\")",
		m_CurrentDepoID,po_no,barcode,time_string_a);

	try
	{
		m_DepotDB.execDML(query);
	}
	catch(CppSQLite3Exception& e)
	{
		ret=false;
		m_ErrMsg.Format(L"%S",e.errorMessage());
	}

	return ret;
}
bool CDepotDB::DelBarCode(CString& PoNo,CString& BarCode)
{
	if(!m_DepotDB.IsDBConnected())
	{
		m_ErrMsg.Format(L"%S","DB not conneted");
		return false;
	}

	bool ret = true;
	char query[256];
	char po_no[128]={0};
	char barcode[128]={0};
	WCHAR w_barcode[128]={0};
	int rowid;
	int  foundrow=0;

	WideCharToMultiByte(
		CP_UTF8,         // code page
		0,       
		PoNo, 
		PoNo.GetLength(),       
		po_no,  
		sizeof(po_no) ,
		NULL,
		NULL
		);

	WideCharToMultiByte(
		CP_UTF8,         // code page
		0,       
		BarCode, 
		BarCode.GetLength(),       
		barcode,  
		sizeof(barcode) ,
		NULL,
		NULL
		);

	sprintf(query,"select rowid from po_order where po_id=(select po_id from po where po_no=\"%s\")\
				  and barcode_no=\"%s\" and depo_id=%d and del_flag=0",
				  po_no,barcode,m_CurrentDepoID);

	CppSQLite3Query q = m_DepotDB.execQuery(query);

	while (!q.eof())
	{
		foundrow = 1;
		rowid = q.getIntField(0);
		break;
	}
	q.finalize();

	if(!foundrow)
	{
		return true;
	}



	sprintf(query,"update po_order set del_flag=1 where rowid=%d",
					rowid);

	try
	{
		m_DepotDB.execDML(query);
	}
	catch(CppSQLite3Exception& e)
	{
		ret=false;
		m_ErrMsg.Format(L"%S",e.errorMessage());
	}

	return ret;
}


CppSQLite3Query CDepotDB::UpLoadBarCodeQuery(CStringA& imei,CStringA& run_no_base)
{
	CppSQLite3Query q1 = m_DepotDB.execQuery("select imei,runno from run_no");
	imei.Format("%s",q1.getStringField(0));
	run_no_base.Format("%s",q1.getStringField(1));

	q1.finalize();

	CppSQLite3Query q = 
		m_DepotDB.execQuery("select depo.name,\
							po.po_no,\
							ord.rowid,\
							ord.barcode_no,\
							ord.scan_time,\
							ord.mod_flag,\
							ord.del_flag,\
							ord.post_flag, \
							ord.running_no\
							from depository depo,po,po_order ord \
							where depo.id=ord.depo_id and po.po_id=ord.po_id and (ord.post_flag=0 or (ord.post_flag=1 and ord.del_flag=1)) ");
	return q;
}

bool CDepotDB::SetRunoBase(CStringA& runnobase)
{
	char query[128];
	bool ret = true;
	sprintf(query,"update run_no set runno=\"%s\"",runnobase);
	try
	{
		m_DepotDB.execDML(query);
	}
	catch(CppSQLite3Exception& e)
	{
		ret=false;
		m_ErrMsg.Format(L"%S",e.errorMessage());
	}

	return ret;
}

bool CDepotDB::UpdateOrderRecord(int row_id,CStringA ctr_run_no,int delflag)
{
	bool ret=true;
	char query[256];

	if(delflag)
	{
		sprintf(query,"delete from po_order where rowid=%d",row_id);
	}
	else
	{
		sprintf(query,"update po_order set running_no=\"%s\",post_flag=1 where rowid=%d",ctr_run_no,row_id);
	}

	try
	{
		m_DepotDB.execDML(query);
	}
	catch(CppSQLite3Exception& e)
	{
		ret=false;
		m_ErrMsg.Format(L"%S",e.errorMessage());
	}

	return ret;
}

bool CDepotDB::IsNeedUpLoad()
{
	if(!m_DepotDB.IsDBConnected())
	{
		m_ErrMsg.Format(L"%S","DB not conneted");
		return false;
	}

	char query[256];
	bool found = false;

	sprintf(query,"select * from po_order where post_flag=0 or (post_flag=1 and del_flag=1)");

	CppSQLite3Query q = m_DepotDB.execQuery(query);

	while (!q.eof())
	{
		found = true;
		break;
	}
	q.finalize();

	return found;
}
