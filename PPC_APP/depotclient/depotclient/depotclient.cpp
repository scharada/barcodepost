// depotclient.cpp : 定义应用程序的类行为。
//

#include "stdafx.h"
#include "depotClient.h"
#include "barcodeScanDlg.h"
#include "loginDlg.h"
#include "depotManager.h"
#include "depotDB.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


// CdepotclientApp

BEGIN_MESSAGE_MAP(CdepotclientApp, CWinApp)
END_MESSAGE_MAP()


// CdepotclientApp 构造
CdepotclientApp::CdepotclientApp()
	: CWinApp()
{
	// TODO: 在此处添加构造代码，
	// 将所有重要的初始化放置在 InitInstance 中
}


// 唯一的一个 CdepotclientApp 对象
CdepotclientApp theApp;

// CdepotclientApp 初始化

BOOL CdepotclientApp::InitInstance()
{
    // 在应用程序初始化期间，应调用一次 SHInitExtraControls 以初始化
    // 所有 Windows Mobile 特定控件，如 CAPEDIT 和 SIPPREF。
    SHInitExtraControls();

	if (!AfxSocketInit())
	{
		AfxMessageBox(IDP_SOCKETS_INIT_FAILED);
		return FALSE;
	}

	AfxEnableControlContainer();

	// 标准初始化
	// 如果未使用这些功能并希望减小
	// 最终可执行文件的大小，则应移除下列
	// 不需要的特定初始化例程
	// 更改用于存储设置的注册表项
	// TODO: 应适当修改该字符串，
	// 例如修改为公司或组织名
	//SetRegistryKey(_T("应用程序向导生成的本地应用程序"));

	if(!CDepotDB::GetDB().OpenDB(CDepotDB::GetDBPath()+TEXT("depoconfig.db")))
	{
		MessageBox(NULL,L"没有发现配置文件",L"错误",MB_OK);
		return FALSE;
	}
	

	//CBarCodeScanDlg dlg;
	CLoginDlg dlg;
	m_pMainWnd = &dlg;
	INT_PTR nResponse = dlg.DoModal();
	if (nResponse == IDOK)
	{

	}

	// 由于对话框已关闭，所以将返回 FALSE 以便退出应用程序，
	//  而不是启动应用程序的消息泵。
	return FALSE;
}
