// depotclientDlg.cpp : 实现文件
//

#include "stdafx.h"
#include "depotClient.h"
#include "barcodeScanDlg.h"
#include "DepotDB.h"


#ifdef _DEBUG
#define new DEBUG_NEW
#endif


#define		countof(x)		sizeof(x)/sizeof(x[0])

// Define user messages
enum tagUSERMSGS
{
	UM_SCAN	= WM_USER + 0x200,
	UM_STARTSCANNING,
	UM_STOPSCANNING,
	UM_BARCODE_SCANOK
};


// CdepotclientDlg 对话框

CBarCodeScanDlg::CBarCodeScanDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CBarCodeScanDlg::IDD, pParent),
	m_hScanner(NULL),m_lpScanBuffer(NULL),
	m_dwScanSize(7095),m_dwScanTimeout(2000)
	,m_bUseText(TRUE),m_bTriggerFlag(FALSE)
	,m_bRequestPending(FALSE),m_bStopScanning(FALSE),m_bContinuousMode(FALSE)
	, m_StatusStr(_T(""))
	, m_BarCode(_T(""))
{
	wcscpy(m_szScannerName,TEXT("SCN1:"));
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

void CBarCodeScanDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	DDX_Text(pDX, IDC_STATIC_STATUS, m_StatusStr);
	DDX_Text(pDX, IDC_EDT_BARCODE, m_BarCode);
	DDX_Control(pDX, IDC_COMBO_POLIST, m_combPoList);
	DDX_Control(pDX,IDC_BARCODE_LIST,m_BarCodeList);
}

BEGIN_MESSAGE_MAP(CBarCodeScanDlg, CDialog)
#if defined(_DEVICE_RESOLUTION_AWARE) && !defined(WIN32_PLATFORM_WFSP)
	ON_WM_SIZE()
#endif
	//}}AFX_MSG_MAP
	ON_CBN_SELCHANGE(IDC_COMBO_POLIST, &CBarCodeScanDlg::OnPoComboSelChange)
END_MESSAGE_MAP()


// CdepotclientDlg 消息处理程序

BOOL CBarCodeScanDlg::OnInitDialog()
{
	CDialog::OnInitDialog();

	// 设置此对话框的图标。当应用程序主窗口不是对话框时，框架将自动
	//  执行此操作
	SetIcon(m_hIcon, TRUE);			// 设置大图标
	SetIcon(m_hIcon, FALSE);		// 设置小图标

	//PostMessage(UM_STARTSCANNING,0,0L);
	PostMessage(UM_STARTSCANNING,0,0L);

	CArray<CString> array;

	// TODO:  Add extra initialization here
	CDepotDB::GetDB().GetPoList(array);

	for(int i = 0 ; i < array.GetSize() ; i++)
	{
		m_combPoList.AddString(array[i]);
	}

	if(m_combPoList.GetCount()>0)
	{
		m_combPoList.SetCurSel(0);
		ReNewBarCodeList(0);
	}




	// TODO: 在此添加额外的初始化代码
	
	return TRUE;//除非将焦点设置到控件，否则返回 TRUE
}

void  CBarCodeScanDlg::ReNewBarCodeList(INT po_index)
{
	CString CurStr;
	CArray<CString> array;
	m_combPoList.GetLBText(po_index,CurStr);

	CDepotDB::GetDB().GetBarCodeList(CurStr,array);

	m_BarCodeList.ResetContent();

	for(int i = 0 ; i < array.GetSize() ; i++)
	{
		m_BarCodeList.AddString(array[i]);
	}
}

#if defined(_DEVICE_RESOLUTION_AWARE) && !defined(WIN32_PLATFORM_WFSP)
void CBarCodeScanDlg::OnSize(UINT /*nType*/, int /*cx*/, int /*cy*/)
{
	/*
	if (AfxIsDRAEnabled())
	{
		DRA::RelayoutDialog(
			AfxGetResourceHandle(), 
			this->m_hWnd, 
			DRA::GetDisplayMode() != DRA::Portrait ? 
			MAKEINTRESOURCE(IDD_DEPOTCLIENT_DIALOG_WIDE) : 
			MAKEINTRESOURCE(IDD_DEPOTCLIENT_DIALOG));
	}*/
	if (AfxIsDRAEnabled())
	{
		DRA::RelayoutDialog(
			AfxGetResourceHandle(), 
			this->m_hWnd, 
			MAKEINTRESOURCE(IDD_DEPOTCLIENT_DIALOG));
	}
}
#endif

void CBarCodeScanDlg::OnBarCodeOK()
{
	UpdateData();
	CString pono;
	m_combPoList.GetLBText(m_combPoList.GetCurSel(),pono);
	CDepotDB::GetDB().AddBarCode(pono,m_BarCode);
	m_BarCode=L"";
	ReNewBarCodeList(m_combPoList.GetCurSel());
	UpdateData(FALSE);
	//send message to post


}

LRESULT CBarCodeScanDlg::WindowProc(UINT message, WPARAM wParam, LPARAM lParam)
{
	DWORD			dwResult;
	TCHAR			szLabelType[10];
	LPSCAN_BUFFER	lpScanBuf;
	TCHAR			szLen[MAX_PATH];

	switch(message)
    {
		case UM_BARCODE_SCANOK:
			OnBarCodeOK();
			return TRUE;

		case UM_STARTSCANNING:

			// Open scanner, prepare for scanning, 
			// and submit the first read request
			dwResult = SCAN_Open(m_szScannerName, &m_hScanner);
			if ( dwResult != E_SCN_SUCCESS )
			{
				break;
			}

			dwResult = SCAN_Enable(m_hScanner);
			if ( dwResult != E_SCN_SUCCESS )
			{
				break;
			}

			m_lpScanBuffer = SCAN_AllocateBuffer(m_bUseText, m_dwScanSize);
			if (m_lpScanBuffer == NULL)
			{
				return TRUE;
			}

			dwResult = SCAN_ReadLabelMsg(m_hScanner,
										 m_lpScanBuffer,
										 m_hWnd,
										 UM_SCAN,
										 m_dwScanTimeout,
										 NULL);

			if ( dwResult != E_SCN_SUCCESS )
			{

			}
			else
				m_bRequestPending = TRUE;

			break;

			return TRUE;

		case UM_STOPSCANNING:

			// We stop scanning in two steps: first, cancel any pending read 
			// request; second, after there is no more pending request, disable 
			// and close the scanner. We may need to do the second step after 
			// a UM_SCAN message told us that the cancellation was completed.
			if (!m_bStopScanning && m_bRequestPending)													
				SCAN_Flush(m_hScanner);

			if (!m_bRequestPending)			
			{							 
				SCAN_Disable(m_hScanner);

				if (m_lpScanBuffer)
					SCAN_DeallocateBuffer(m_lpScanBuffer);

				SCAN_Close(m_hScanner);

				EndDialog(0);
			}
			m_bStopScanning = TRUE;

			return TRUE;

		case WM_ACTIVATE:

			// In foreground scanning mode, we need to cancel read requests
			// when the application is deactivated, and re-submit read request
			// when the application is activated again.
			switch(LOWORD(wParam))
			{
			case WA_INACTIVE:

				// Cancel any pending request since we are going to lose focus
				if (m_bRequestPending)
					dwResult = SCAN_Flush(m_hScanner);
				// Do not set bRequestPending to FALSE until
				// we get the UM_SCAN message
				break;

			default:	// activating

				if (!m_bRequestPending && m_lpScanBuffer != NULL && !m_bStopScanning)
				{	
					// Submit a read request if no request pending
					dwResult = SCAN_ReadLabelMsg(m_hScanner,
						m_lpScanBuffer,
						m_hWnd,
						UM_SCAN,
						m_dwScanTimeout,
						NULL);

					if ( dwResult != E_SCN_SUCCESS )
					{
						m_StatusStr = TEXT("条形码读取错误 reReadLabelMsg failed");
						UpdateData(FALSE);
					}
					else
						m_bRequestPending = TRUE;
				}
				break;
			}
			break;


		case UM_SCAN:

			m_bRequestPending = FALSE;

			// Clear the soft trigger
			m_bTriggerFlag = FALSE;
			SCAN_SetSoftTrigger(m_hScanner,&m_bTriggerFlag);

			// Get scan result from the scan buffer, and display it
			lpScanBuf = (LPSCAN_BUFFER)lParam;
			if ( lpScanBuf == NULL )
			{
				m_StatusStr = TEXT("条形码读取错误 数据为空");
				UpdateData(FALSE);
				return FALSE;
			}
				//ErrorExit(hwnd, IDS_ERR_BUF, 0);

			//hctl_data = GetDlgItem(hwnd,IDC_EDIT_DATA);
			//hctl_length = GetDlgItem(hwnd,IDC_EDIT_LEN);
			//hctl_type = GetDlgItem(hwnd,IDC_EDIT_TYPE);

			switch (SCNBUF_GETSTAT(lpScanBuf))
			{ 
				case E_SCN_DEVICEFAILURE:
					m_StatusStr = TEXT("条形码读取错误 device failure");
					UpdateData(FALSE);
					break;

				case E_SCN_READPENDING:
					m_StatusStr = TEXT("条形码读取错误 read pending");
					UpdateData(FALSE);
					break;

				case E_SCN_READCANCELLED:
					if (m_bStopScanning)
					{	// complete the second step of UM_STOPSCANNING
						SendMessage(UM_STOPSCANNING,0,0L);	
						return TRUE;
					}
					if (!GetFocus())	
						break;	// Do nothing if read was cancelled while deactivation
					m_StatusStr = TEXT("条形码读取错误 read cancelled");
					UpdateData(FALSE);
					break;

				case E_SCN_READTIMEOUT:
					if(m_bContinuousMode)
					{
						//PostMessage(WM_COMMAND,IDC_BUTTON_SOFTTRIGGER,0L);
					}
					break;
			
				case E_SCN_SUCCESS:

					//Edit_SetText(hctl_data, (LPTSTR)SCNBUF_GETDATA(lpScanBuffer));
					m_StatusStr = TEXT("条形码读取OK");
					m_BarCode = (LPTSTR)SCNBUF_GETDATA(m_lpScanBuffer);
						
					// Format label type as a hex constant for display
					wsprintf(szLabelType, TEXT("0x%.2X"), SCNBUF_GETLBLTYP(lpScanBuf));
					//Edit_SetText(hctl_type, szLabelType);
					wsprintf(szLen, TEXT("%d"), SCNBUF_GETLEN(lpScanBuf));
					//m_BarCode=szLabelType;

					UpdateData(FALSE);

					PostMessage(UM_BARCODE_SCANOK,0,0L);

					//if(m_bContinuousMode)
						//PostMessage(WM_COMMAND,IDC_BUTTON_SOFTTRIGGER,0L);
					break;
			}
			// Submit next read request if we are foreground
			if (GetFocus())
			{
				dwResult = SCAN_ReadLabelMsg(m_hScanner,
					m_lpScanBuffer,
					m_hWnd,
					UM_SCAN,
					m_dwScanTimeout,
					NULL);

				if ( dwResult != E_SCN_SUCCESS )
				{
					m_StatusStr = TEXT("条形码读取错误 ReInitFail");
					UpdateData(FALSE);
				}
				else
					m_bRequestPending = TRUE;
			}

			return TRUE;
		case WM_COMMAND:
			switch (LOWORD(wParam))
            {
				case IDC_BTN_BEGIN_SCAN:
					// Clear the state first before we set it to TRUE
					m_bTriggerFlag = FALSE;
					SCAN_SetSoftTrigger(m_hScanner,&m_bTriggerFlag);

					m_bTriggerFlag = TRUE;
					SCAN_SetSoftTrigger(m_hScanner,&m_bTriggerFlag);

					break;
				case IDC_ADD_BARCODE:
					UpdateData();
					if(m_BarCode.GetLength()>0)
					{
						OnBarCodeOK();
					}
					break;
				case IDC_BTN_DEL:
					OnScanCodeDel();
					break;
				case IDC_BTN_SCAN_RETURN:
					CDialog::OnOK();
					break;
				case IDOK:	// fall through

				case IDCANCEL:
					SendMessage(UM_STOPSCANNING,0,0L);
					break;
			}
	}
	return CDialog::WindowProc(message,wParam,lParam);
}

void CBarCodeScanDlg::OnPoComboSelChange()
{
	ReNewBarCodeList(m_combPoList.GetCurSel());
}

void CBarCodeScanDlg::OnScanCodeDel()
{
	CString barcode;
	CString pono;
	if(LB_ERR  == m_BarCodeList.GetCurSel())
	{
		return;
	}
	m_BarCodeList.GetText(m_BarCodeList.GetCurSel(),barcode);
	m_combPoList.GetLBText(m_combPoList.GetCurSel(),pono);

	if(CDepotDB::GetDB().DelBarCode(pono,barcode))
	{
		ReNewBarCodeList(m_combPoList.GetCurSel());
	}
}




