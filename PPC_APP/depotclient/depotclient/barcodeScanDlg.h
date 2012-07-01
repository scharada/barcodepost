// depotclientDlg.h : 头文件
//
#include <ScanCAPI.h>
#pragma once



// CdepotclientDlg 对话框
class CBarCodeScanDlg : public CDialog
{
//常量
public:

// 构造
public:
	CBarCodeScanDlg(CWnd* pParent = NULL);	// 标准构造函数

// 对话框数据
	enum { IDD = IDD_DEPOTCLIENT_DIALOG };


	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV 支持


private:
	HANDLE			m_hScanner;//			= NULL;
	LPSCAN_BUFFER	m_lpScanBuffer;//		= NULL;
	TCHAR			m_szScannerName[MAX_PATH];// = TEXT("SCN1:");	// default scanner name
	const DWORD		m_dwScanSize;//			= 7095;				// default scan buffer size	
	const DWORD		m_dwScanTimeout;//		= 2000;				// default timeout value (0 means no timeout)	
	BOOL			m_bUseText;//				= TRUE;
	BOOL			m_bTriggerFlag;//			= FALSE;
	BOOL			m_bRequestPending;//		= FALSE;
	BOOL			m_bStopScanning;//		= FALSE;
	BOOL			m_bContinuousMode;//		= FALSE;

// 实现
protected:
	HICON m_hIcon;

	void  ReNewBarCodeList(INT po_index);
	void  OnBarCodeOK();
	void  OnScanCodeDel();

	// for processing Windows messages
	virtual LRESULT WindowProc(UINT message, WPARAM wParam, LPARAM lParam);

	// 生成的消息映射函数
	virtual BOOL OnInitDialog();
	afx_msg void OnPoComboSelChange();
#if defined(_DEVICE_RESOLUTION_AWARE) && !defined(WIN32_PLATFORM_WFSP)
	afx_msg void OnSize(UINT /*nType*/, int /*cx*/, int /*cy*/);
#endif
	DECLARE_MESSAGE_MAP()
public:
	// 订单号
	CString m_StatusStr;
	CString m_BarCode;
	CComboBox m_combPoList;
	CListBox  m_BarCodeList;
};
