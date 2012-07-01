// depotclient.cpp : ����Ӧ�ó��������Ϊ��
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


// CdepotclientApp ����
CdepotclientApp::CdepotclientApp()
	: CWinApp()
{
	// TODO: �ڴ˴���ӹ�����룬
	// ��������Ҫ�ĳ�ʼ�������� InitInstance ��
}


// Ψһ��һ�� CdepotclientApp ����
CdepotclientApp theApp;

// CdepotclientApp ��ʼ��

BOOL CdepotclientApp::InitInstance()
{
    // ��Ӧ�ó����ʼ���ڼ䣬Ӧ����һ�� SHInitExtraControls �Գ�ʼ��
    // ���� Windows Mobile �ض��ؼ����� CAPEDIT �� SIPPREF��
    SHInitExtraControls();

	if (!AfxSocketInit())
	{
		AfxMessageBox(IDP_SOCKETS_INIT_FAILED);
		return FALSE;
	}

	AfxEnableControlContainer();

	// ��׼��ʼ��
	// ���δʹ����Щ���ܲ�ϣ����С
	// ���տ�ִ���ļ��Ĵ�С����Ӧ�Ƴ�����
	// ����Ҫ���ض���ʼ������
	// �������ڴ洢���õ�ע�����
	// TODO: Ӧ�ʵ��޸ĸ��ַ�����
	// �����޸�Ϊ��˾����֯��
	//SetRegistryKey(_T("Ӧ�ó��������ɵı���Ӧ�ó���"));

	if(!CDepotDB::GetDB().OpenDB(CDepotDB::GetDBPath()+TEXT("depoconfig.db")))
	{
		MessageBox(NULL,L"û�з��������ļ�",L"����",MB_OK);
		return FALSE;
	}
	

	//CBarCodeScanDlg dlg;
	CLoginDlg dlg;
	m_pMainWnd = &dlg;
	INT_PTR nResponse = dlg.DoModal();
	if (nResponse == IDOK)
	{

	}

	// ���ڶԻ����ѹرգ����Խ����� FALSE �Ա��˳�Ӧ�ó���
	//  ����������Ӧ�ó������Ϣ�á�
	return FALSE;
}
