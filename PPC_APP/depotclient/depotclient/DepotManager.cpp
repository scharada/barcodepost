// DepotManager.cpp : implementation file
//

#include "stdafx.h"
#include "depotClient.h"
#include "depotManager.h"
#include "orderManage.h"
#include "barcodeScanDlg.h"
#include "depotDB.h"


// CDepotManager dialog

IMPLEMENT_DYNAMIC(CDepotManager, CDialog)

CDepotManager::CDepotManager(CWnd* pParent /*=NULL*/)
	: CDialog(CDepotManager::IDD, pParent),
	m_HttpPostData(this,UM_POST_DATA_OK,SERVER_NAME,SERVER_PORT,OBJECT_NAME),
	m_UpLoading(FALSE)
{
	m_RedBrush = ::CreateSolidBrush(RGB(180,0,0));
	m_GreenBrush = ::CreateSolidBrush(RGB(0,200,0));
}

CDepotManager::~CDepotManager()
{
	::DeleteObject(m_RedBrush);
	::DeleteObject(m_GreenBrush);
}

void CDepotManager::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
}


BEGIN_MESSAGE_MAP(CDepotManager, CDialog)
	ON_BN_CLICKED(IDC_BTN_ORDERMANAGE, &CDepotManager::OnBnClickedBtnOrdermanage)
	ON_BN_CLICKED(IDC_BTN_SCAN, &CDepotManager::OnBnClickedBtnScan)
	ON_BN_CLICKED(IDC_UPLOAD, &CDepotManager::OnBnClickedUpload)
	ON_WM_PAINT()
	ON_WM_CTLCOLOR()
END_MESSAGE_MAP()


// CDepotManager message handlers

void CDepotManager::OnBnClickedBtnOrdermanage()
{
	// TODO: Add your control notification handler code here
	//CString h = CDepotDB::GetDBPath();
	//CDepotDB::GetDB().OpenDB(h+CString(L"hello.db"));
	COrderManage order_manager_dlg;
	INT_PTR nResponse;
	nResponse = order_manager_dlg.DoModal();
	if(nResponse == IDOK)
	{

	}
}

void CDepotManager::OnBnClickedBtnScan()
{
	// TODO: Add your control notification handler code here
	
	CBarCodeScanDlg bar_code_scan_dlg;
	INT_PTR nResponse;
	nResponse = bar_code_scan_dlg.DoModal();
	if(nResponse == IDOK)
	{

	}
}

void CDepotManager::OnBnClickedUpload()
{
	// TODO: Add your control notification handler code here
	if(m_UpLoading)
		return;
	((CButton*)GetDlgItem(IDC_UPLOAD))->EnableWindow(FALSE);
	BeginWaitCursor();
	m_HttpPostData.StartPost();
}

LRESULT CDepotManager::WindowProc(UINT message, WPARAM wParam, LPARAM lParam)
{
	// TODO: Add your specialized code here and/or call the base class
	if(message == UM_POST_DATA_OK)
	{
		EndWaitCursor();
		if(!m_HttpPostData.m_PostStatus)
		{
			MessageBox(m_HttpPostData.m_ErrMsg);
		}
		m_UpLoading = FALSE;

		((CButton*)GetDlgItem(IDC_UPLOAD))->EnableWindow(TRUE);
	}
	return CDialog::WindowProc(message, wParam, lParam);
}

BOOL CDepotManager::OnInitDialog()
{
	CDialog::OnInitDialog();

	// TODO:  Add extra initialization her

	return TRUE;  // return TRUE unless you set the focus to a control
	// EXCEPTION: OCX Property Pages should return FALSE
}
/*
void CDepotManager::OnPaint()
{
	return ;
	CPaintDC dc(this); // device context for painting
	//CWnd::OnPaint();
	// TODO: Add your message handler code here
	// Do not call CDialog::OnPaint() for painting messages
	int x,y;
	CBrush brush_green,brush_red;
	CBrush *old_brush;
	x = 340;
	y = 400;
	dc.SelectStockObject(WHITE_BRUSH);
	RECT rect;
	//CClientDC clientdc(this);
	GetClientRect(&rect);
	dc.Rectangle(&rect);

	brush_green.CreateSolidBrush(RGB(0,255,0));
	brush_red.CreateSolidBrush(RGB(255,0,0));
	
	
	
	if(CDepotDB::GetDB().IsNeedUpLoad())
	{
		old_brush = (CBrush*)dc.SelectObject(brush_red);
	}
	else
	{
		old_brush = (CBrush*)dc.SelectObject(brush_green);
	}

	dc.Ellipse(x,y,x+50,y+50);
	dc.SelectObject(old_brush);

}
*/
HBRUSH CDepotManager::OnCtlColor(CDC* pDC, CWnd* pWnd, UINT nCtlColor)
{
	HBRUSH hbr = CDialog::OnCtlColor(pDC, pWnd, nCtlColor);
	if(pWnd->GetDlgCtrlID() == IDC_UPLOAD)
	{
		if(CDepotDB::GetDB().IsNeedUpLoad())
		{
			hbr = m_RedBrush;
		}
		else
		{
			hbr = m_GreenBrush;
		}

	}
	// TODO:  Change any attributes of the DC here

	// TODO:  Return a different brush if the default is not desired
	return hbr;
}
