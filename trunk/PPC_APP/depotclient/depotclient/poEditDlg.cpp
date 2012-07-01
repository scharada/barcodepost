// poEditDlg.cpp : implementation file
//

#include "stdafx.h"
#include "depotclient.h"
#include "poEditDlg.h"


// CPOEditDlg dialog

IMPLEMENT_DYNAMIC(CPOEditDlg, CDialog)

CPOEditDlg::CPOEditDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CPOEditDlg::IDD, pParent)
	, m_PONO(_T(""))
{

}

CPOEditDlg::~CPOEditDlg()
{
}

void CPOEditDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	DDX_Text(pDX, IDC_PO_NO, m_PONO);
}


BEGIN_MESSAGE_MAP(CPOEditDlg, CDialog)
	ON_BN_CLICKED(ID_OK, &CPOEditDlg::OnBnClickedOk)
END_MESSAGE_MAP()


// CPOEditDlg message handlers

void CPOEditDlg::OnBnClickedOk()
{
	UpdateData();
	CDialog::OnOK();
	// TODO: Add your control notification handler code here
}
