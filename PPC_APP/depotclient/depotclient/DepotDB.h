
#include "CppSQLite3.h"

#pragma once

class CDepotDB
{
protected:
	CDepotDB(void);
	~CDepotDB(void);

public:
	static CDepotDB&	GetDB();
	static CString		GetDBPath();

	bool				OpenDB(CString&);
	CString&			GetErrMsg(){return m_ErrMsg;}

	bool				GetDepoList(CArray<CString>&);
	bool				GetPoList(CArray<CString>&);
	bool				AddBarCode(CString&,CString&);
	bool				DelBarCode(CString&,CString&);
	bool				GetBarCodeList(CString&,CArray<CString>&);
	bool				CheckPassWd(CString,CString);

	//´«Èë ¶©µ¥ºÅ
	bool				AddPo(CString&);
	bool				DelPo(CString&);
	bool				ModPo(CString&,CString&);

	CppSQLite3Query     UpLoadBarCodeQuery(CStringA& imei,CStringA& run_no_base);
	bool				SetRunoBase(CStringA& runnobase);
	bool				UpdateOrderRecord(int row_id,CStringA ctr_run_no,int delflag);
	bool				IsNeedUpLoad();

	

public:
	CppSQLite3DB m_DepotDB;
	CString      m_ErrMsg;
	INT			 m_CurrentDepoID;
};
