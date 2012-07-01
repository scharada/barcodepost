// depotclientDlg.h : ͷ�ļ�
//
#include <ScanCAPI.h>
#pragma once



// CdepotclientDlg �Ի���
class CBarCodeScanDlg : public CDialog
{
//����
public:

// ����
public:
	CBarCodeScanDlg(CWnd* pParent = NULL);	// ��׼���캯��

// �Ի�������
	enum { IDD = IDD_DEPOTCLIENT_DIALOG };


	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV ֧��


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

// ʵ��
protected:
	HICON m_hIcon;

	void  ReNewBarCodeList(INT po_index);
	void  OnBarCodeOK();
	void  OnScanCodeDel();

	// for processing Windows messages
	virtual LRESULT WindowProc(UINT message, WPARAM wParam, LPARAM lParam);

	// ���ɵ���Ϣӳ�亯��
	virtual BOOL OnInitDialog();
	afx_msg void OnPoComboSelChange();
#if defined(_DEVICE_RESOLUTION_AWARE) && !defined(WIN32_PLATFORM_WFSP)
	afx_msg void OnSize(UINT /*nType*/, int /*cx*/, int /*cy*/);
#endif
	DECLARE_MESSAGE_MAP()
public:
	// ������
	CString m_StatusStr;
	CString m_BarCode;
	CComboBox m_combPoList;
	CListBox  m_BarCodeList;
};
