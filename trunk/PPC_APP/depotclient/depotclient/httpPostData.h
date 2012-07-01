
#pragma once


class CHttpPostData
{
public:
	CHttpPostData(CWnd* CmdTarget,DWORD MsgID,CString ServerName,INT Port,CString ObjectName);
	~CHttpPostData();
	BOOL Post(CStringA run_no,CStringA po_no,CStringA depo_no,CStringA barcode_no,
		CStringA scantime,CStringA run_no_mod,CStringA mod_flag);
	void StartPost(ULONG timeout = 0);
	static DWORD WINAPI UpLoadThread(LPVOID para);

	HANDLE         m_Thread;
protected:
	CWnd*			m_CmdTarget;
	DWORD			m_MsgID;
	CString			m_ServerName;			//�ύ���ݷ�������ַ
	INT				m_ServerPort;			//�ύ�������˿ں�
	CString			m_ServerObjectName;		//Object name
	CStringA		m_FormData;				//form data to post
public:
	CString			m_ErrMsg;
	BOOL			m_PostStatus;
};