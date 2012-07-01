#include "stdafx.h"
#include "httpPostData.h"
#include "DepotDB.h"

class update_record
{
public:
	update_record(int r,CStringA s,int d):rowid(r),runno(s),del_flag(d){}
	update_record():rowid(0),runno(""),del_flag(0){}
	int rowid;
	CStringA runno;
	int del_flag;
};

CHttpPostData::CHttpPostData(CWnd* CmdTarget,DWORD MsgID,CString ServerName,INT Port,CString ObjectName):
m_CmdTarget(CmdTarget),m_MsgID(MsgID),
m_ServerName(ServerName),m_ServerPort(Port),
m_ServerObjectName(ObjectName),
m_FormData(""),m_Thread(NULL)
{
}

CHttpPostData::~CHttpPostData()
{
	if(m_Thread)
	{
		TerminateThread(m_Thread,0);
	}
}


void CHttpPostData::StartPost(ULONG timeout)
{
	//return nRetCode;
	m_Thread = CreateThread(NULL,0,UpLoadThread,this,0,NULL);
}

BOOL CHttpPostData::Post(CStringA run_no,CStringA po_no,CStringA depo_no,CStringA barcode_no,
						 CStringA scantime,CStringA run_no_mod,CStringA mod_flag)
{
	BOOL ret=TRUE;
	CInternetSession session;

	CHttpConnection* pConnection = session.GetHttpConnection( m_ServerName,
		(INTERNET_PORT)m_ServerPort);

	CHttpFile* pFile = pConnection->OpenRequest( CHttpConnection::HTTP_VERB_POST,
		m_ServerObjectName,
		NULL,
		1,
		NULL,
		TEXT("HTTP/1.1"),
		0);

	//需要提交的数据
	CString szHeaders   = L"Content-Type: application/x-www-form-urlencoded;";

	//下面这段编码，则是可以让服务器正常处理
	CHAR strFormData[512] = "name=WaterLin";

	sprintf(strFormData,"running_no=%s&po_no=%s&warehouse_code=%s&barcode_no=%s&scan_time=%s&run_no_for_modify=%s&modify_flag=%s",
					   run_no,po_no,depo_no,barcode_no,scantime,run_no,mod_flag
					   );
	try
	{
		pFile->SendRequest(szHeaders,
			(LPVOID)strFormData,
			strlen(strFormData));
	}
	catch(CInternetException* e)
	{
		//printf("",e.GetErrorMessage);
		//e->ReportError();
		//e->GetErrorMessage()
		TCHAR   szCause[255];
		CString strFormatted;

		e->GetErrorMessage(szCause, 255);

		// (in real life, it's probably more
		// appropriate to read this from
		//  a string resource so it would be easy to
		// localize)

		m_ErrMsg = _T("网络连接错误: ");
		m_ErrMsg += szCause;
		ret = FALSE;
		goto fin;
	}

	DWORD dwRet;
	pFile->QueryInfoStatusCode(dwRet);

	if(dwRet != HTTP_STATUS_OK)
	{
		//CString errText;
		//errText.Format(L"POST出错，错误码：%d", dwRet);
		//AfxMessageBox(errText);
		m_ErrMsg.Format(L"出错，错误码:%d",dwRet);
		ret = FALSE;
		goto fin;
	}
	else
	{
		/*
		int len = pFile->GetLength();
		char buf[2000];
		int numread;
		CString filepath;
		CString strFile = L"result.html";
		filepath.Format(L".\\%s", strFile);
		CFile myfile(filepath,
			CFile::modeCreate|CFile::modeWrite|CFile::typeBinary);
		while ((numread = pFile->Read(buf,sizeof(buf)-1)) > 0)
		{
			buf[numread] = '\0';
			strFile += buf;
			myfile.Write(buf, numread);
		}
		myfile.Close();*/
	}

fin:
	pFile->Close();
	session.Close();
	 
	delete pFile;
	return ret;
}

DWORD WINAPI CHttpPostData::UpLoadThread(LPVOID para)
{
	CHttpPostData* This = (CHttpPostData*)para;
	
	CStringA imei,runno;
	CppSQLite3Query q = CDepotDB::GetDB().UpLoadBarCodeQuery(imei,runno);
	INT64 iRunNo;
	This->m_PostStatus = TRUE;
	iRunNo = _atoi64(runno)+1;

	CArray<update_record> update_array;
	

	//depo.name,  po.po_no,  or.rowid,  or.barcode_no,  or.scan_time,  or.mod_flag,  or.del_flag
	while(!q.eof())
	{
		const char* depo_no = q.getStringField(0);
		const char* po_no = q.getStringField(1); 
		int         row_id = q.getIntField(2);
		const char* barcode = q.getStringField(3);
		const char* scantime = q.getStringField(4);
		int			modflag = q.getIntField(5);
		int			delflag = q.getIntField(6);
		int			postflag=q.getIntField(7);
		const char* runingno = q.getStringField(8);

		CStringA    cstr_mod_flag;
		if(modflag == 1)
		{
			cstr_mod_flag = "2";
		}
		if(delflag == 1)
		{
			cstr_mod_flag = "1";
		}
		else
		{
			cstr_mod_flag = "0";
		}


		CStringA ctr_run_no;
		if((postflag == 1 && delflag == 1) || (postflag == 1 && modflag == 1))
		{
			ctr_run_no.Format("%s",runingno);
		}
		else
		{
			ctr_run_no.Format("%s%017I64d",imei,iRunNo++);
		}
		
		if(!(postflag==0 && delflag==1))//在本地增加后又删除，还未上传服务器
		{
			if(!This->Post(ctr_run_no,po_no,depo_no,barcode,
				scantime,ctr_run_no,cstr_mod_flag))
			{
				This->m_PostStatus = FALSE;
				break;
			}
		}

		update_array.Add(update_record(row_id,ctr_run_no,delflag));


		q.nextRow();
	}
	q.finalize();

	//更新runno
	for(int i = 0 ; i < update_array.GetSize() ; i++)
	{
		update_record r = update_array[i];
		CDepotDB::GetDB().UpdateOrderRecord(r.rowid,r.runno,r.del_flag);
	}


	CStringA runobase;
	runobase.Format("%I64d",iRunNo);

	CDepotDB::GetDB().SetRunoBase(runobase);

	This->m_CmdTarget->PostMessage(This->m_MsgID,0,0);

	return 0;
}

