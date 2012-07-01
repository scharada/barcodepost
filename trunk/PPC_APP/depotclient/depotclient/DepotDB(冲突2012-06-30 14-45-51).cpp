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
	char qrystr[128]={0};
	sprintf(qrystr,"select po_no,mod_flag,po_no_mod from po_order where depo_id=%d and del_flag=0;",m_CurrentDepoID);
	CppSQLite3Query q = m_DepotDB.execQuery(qrystr);

	const char* fn = q.fieldName(0);
	while (!q.eof())
	{
		WCHAR wstr[128]={0};
		int mod_flag;
		mod_flag = q.getIntField(1);
		const char* depo;
		if(mod_flag)
		{
			depo = q.getStringField(2);
		}
		else
		{
			depo = q.getStringField(0);
		}
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

bool CDepotDB::AddPo(CString& pono)
{
	if(!m_DepotDB.IsDBConnected())
	{
		m_ErrMsg.Format(L"%S","DB not conneted");
		return false;
	}
	int mod_flag;

	//检查 是否已经存在
	bool exist=false;
	char qrystr[128]={0};
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

	sprintf(qrystr,"select po_id from po where po_no=\"%s\" ",);

	CppSQLite3Query q = m_DepotDB.execQuery(qrystr);
	while (!q.eof())
	{
		mod_flag = q.getIntField(0);
		break;
		exist = true;
	}
	q.finalize();

	if(exist)//如果已经存在 不做任何事情
	{
		return true;
	}

	sprintf(qrystr,"insert into po_order (depo_id,po_no,post_flag,del_flag,mod_flag) values (%d,\"%s\",0,0,0);",
		m_CurrentDepoID,pono_asc);

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

bool CDepotDB::DelPo(CString& pono)
{
	if(!m_DepotDB.IsDBConnected())
	{
		m_ErrMsg.Format(L"%S","DB not conneted");
		return false;
	}

	//检查 是否已经存在
	bool exist=false;
	char qrystr[128]={0};
	char pono_asc[128]={0};
	int post_flag;

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

	sprintf(qrystr,"select post_flag from po_order where (depo_id=%d and po_no=\"%s\" and mod_flag=0) or \
				   (depo_id=%d and po_no_mod=\"%s\" and mod_flag=1)",
		m_CurrentDepoID,pono_asc,m_CurrentDepoID,pono_asc);

	CppSQLite3Query q = m_DepotDB.execQuery(qrystr);
	while (!q.eof())
	{
		post_flag = q.getIntField(0);
		exist = true;
		break;
	}
	q.finalize();

	if(!exist)//不存在
	{
		return true;
	}

	

	sprintf(qrystr,"update po_order set del_flag=1 where (depo_id=%d and po_no=\"%s\" and mod_flag=0) or \
				   (depo_id=%d and po_no_mod=\"%s\" and mod_flag=1)",
					m_CurrentDepoID,pono_asc,m_CurrentDepoID,pono_asc);

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

bool CDepotDB::ModPo(CString& pono,CString& new_pono)
{
	if(!m_DepotDB.IsDBConnected())
	{
		m_ErrMsg.Format(L"%S","DB not conneted");
		return false;
	}

	//检查 是否已经存在
	bool exist=false;
	int  post_flag;
	char qrystr[128]={0};
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

	sprintf(qrystr,"select post_flag from po_order where (depo_id=%d and po_no=\"%s\" and del_flag=0 and mod_flag=0) or \
				   (depo_id=%d and po_no_mod=\"%s\" and del_flag=0 and mod_flag=1);",
				   m_CurrentDepoID,pono_asc,m_CurrentDepoID,pono_asc);

	CppSQLite3Query q = m_DepotDB.execQuery(qrystr);
	while (!q.eof())
	{
		post_flag = q.getIntField(0);
		exist = true;
		break;
	}
	q.finalize();

	if(!exist)//不存在
	{
		return false;
	}

	if(post_flag)
	{
		sprintf(qrystr,"update po_order set po_no_mod=\"%s\",mod_flag=1,post_flag=0 where \
							(depo_id=%d and \
							po_no=\"%s\" and \
							del_flag=0 and \
							mod_flag=0) \
						or \
							(depo_id=%d and \
							po_no_mod=\"%s\" and \
							del_flag=0 and \
							mod_flag=1);",
					   pono_asc,m_CurrentDepoID,pono_asc,m_CurrentDepoID,pono_asc);
	}
	else
	{
		sprintf(qrystr,"update po_order set po_no=\"%s\",po_no_mod="",mod_flag=0,post_flag=0 where \
						   (depo_id=%d and \
						   po_no=\"%s\" and \
						   del_flag=0 and \
						   mod_flag=0) \
					   or \
						   (depo_id=%d and \
						   po_no_mod=\"%s\" and \
						   del_flag=0 and \
						   mod_flag=1);",
					   pono_asc,m_CurrentDepoID,pono_asc,m_CurrentDepoID,pono_asc);
	}


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
	char query[128];
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

	sprintf(query,"select po.barcode_no from depository dep,po_order po where \
					(po.po_no=\"%s\" and \
					dep.id=%d and \
					po.del_flag=0 and\
					po.mod_flag=0) \
				  or \
					(po.po_no_mod=\"%s\" and \
					dep.id=%d and \
					po.del_flag=0 and\
					po.mod_flag=1)",
					po_no,m_CurrentDepoID,po_no,m_CurrentDepoID);

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
	char query[128];
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

	sprintf(query,"insert into po_order (depo_id,po_no,barcode_no,post_flag,del_flag,mod_flag) values (%d,\"%s\",\"%s\",0,0,0)",
		m_CurrentDepoID,po_no,barcode);

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
	char query[128];
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

	sprintf(query,"update po_order set del_flag=1 where \
				  (			(po_no=\"%s\" and mod_flag=0 and barcode_no=\"%s\") \
						or \
							(po_no_mod=\"%s\" and mod_flag=1 and barcode_no=\"%s\") \
				  ) \
				  and \
						depo_id=%d",
					po_no,barcode,po_no,barcode,m_CurrentDepoID);

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
