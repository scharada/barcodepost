// loginDlg.cpp : implementation file
//

#include "stdafx.h"
#include "depotclient.h"
#include "loginDlg.h"
#include "depotManager.h"
#include "depotDB.h"


// CLoginDlg dialog

IMPLEMENT_DYNAMIC(CLoginDlg, CDialog)

CLoginDlg::CLoginDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CLoginDlg::IDD, pParent)
	, m_strPassWd(_T(""))
{

}

CLoginDlg::~CLoginDlg()
{
}

void CLoginDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_COMB_DEPOTLIST, m_combDepotList);
	DDX_Text(pDX, IDC_PASSWD, m_strPassWd);
}


BEGIN_MESSAGE_MAP(CLoginDlg, CDialog)
	ON_BN_CLICKED(IDC_BTN_LOGIN, &CLoginDlg::OnBnClickedBtnLogin)
END_MESSAGE_MAP()


// CLoginDlg message handlers

void CLoginDlg::OnBnClickedBtnLogin()
{
	// TODO: Add your control notification handler code here
	//检查密码正确性,如果密码错误，弹出对话框提示，如果密码正确，退出对话框
	//CLoginDlg::OnOK();
	UpdateData();
	CString str = m_DepoArray[m_combDepotList.GetCurSel()];
	if(!CDepotDB::GetDB().CheckPassWd(str,m_strPassWd))
	{
		MessageBox(L"密码错误");
		return;
	}
	// TODO: 在此处放置处理何时用“确定”来关闭
	//  对话框的代码
	CDepotManager	manager_dlg;
	INT_PTR			nResponse;
	nResponse = manager_dlg.DoModal();
	if(nResponse == IDOK)
	{

	}
	CDialog::OnOK();	
}

BOOL CLoginDlg::OnInitDialog()
{
	CDialog::OnInitDialog();

	// TODO:  Add extra initialization here
	//m_combDepotList.AddString(TEXT("仓库1"));
	//m_combDepotList.AddString(TEXT("仓库2"));
	//m_combDepotList.AddString(TEXT("仓库3"));
	//m_combDepotList.AddString(TEXT("仓库4"));
	m_combDepotList.SetEditSel(0, -1);
	m_combDepotList.Clear();
	m_DepoArray.RemoveAll();


	CDepotDB::GetDB().GetDepoList(m_DepoArray);



	for(int i = 0 ; i < m_DepoArray.GetSize() ; i++)
	{
		m_combDepotList.AddString(m_DepoArray[i]);
	}

	m_combDepotList.SetCurSel(0);

	return TRUE;  // return TRUE unless you set the focus to a control
	// EXCEPTION: OCX Property Pages should return FALSE
}

LRESULT CLoginDlg::WindowProc(UINT message, WPARAM wParam, LPARAM lParam)
{
	// TODO: Add your specialized code here and/or call the base class

	return CDialog::WindowProc(message, wParam, lParam);
}
