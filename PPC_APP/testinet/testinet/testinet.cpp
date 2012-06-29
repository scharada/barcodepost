// testinet.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include "testinet.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


// The one and only application object

CWinApp theApp;

using namespace std;

int _tmain(int argc, TCHAR* argv[], TCHAR* envp[])
{
	int nRetCode = 0;

	// initialize MFC and print and error on failure
	if (!AfxWinInit(::GetModuleHandle(NULL), NULL, ::GetCommandLine(), 0))
	{
		// TODO: change error code to suit your needs
		_tprintf(_T("Fatal Error: MFC initialization failed\n"));
		nRetCode = 1;
	}
	else
	{
		// TODO: code your application's behavior here.
	}

	CInternetSession session;
	//session.SetOption(INTERNET_OPTION_CONNECT_TIMEOUT, 1000 * 20);
	//session.SetOption(INTERNET_OPTION_CONNECT_BACKOFF, 1000);
	//session.SetOption(INTERNET_OPTION_CONNECT_RETRIES, 1);

	CHttpConnection* pConnection = session.GetHttpConnection( TEXT("223.4.233.88"),
		(INTERNET_PORT)80);

	CHttpFile* pFile = pConnection->OpenRequest( CHttpConnection::HTTP_VERB_POST,
		TEXT("/AddNewInfo.aspx"),
		NULL,
		1,
		NULL,
		TEXT("HTTP/1.1"),
		INTERNET_FLAG_RELOAD);

	//需要提交的数据
	CString szHeaders   = L"Content-Type: application/x-www-form-urlencoded;";

	//下面这段编码，则是可以让服务器正常处理
	CHAR strFormData[] = "form1=testform1&form2=testform2&form3=testform3";
	try 
	{
		pFile->SendRequest(szHeaders,
			(LPVOID)strFormData,
			strlen(strFormData));
	}
	catch(CInternetException& e)
	{
		printf("");
	}

	DWORD dwRet;
	pFile->QueryInfoStatusCode(dwRet);

	if(dwRet != HTTP_STATUS_OK)
	{
		CString errText;
		errText.Format(L"POST出错，错误码：%d", dwRet);
		AfxMessageBox(errText);
	}
	else
	{
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
		myfile.Close();
	}

	session.Close();
	pFile->Close(); 
	delete pFile;

	return nRetCode;
}
