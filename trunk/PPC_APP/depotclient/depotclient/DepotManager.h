#include "httpPostData.h"

#pragma once

#define UM_POST_DATA_OK (WM_USER + 0x300)

#define SERVER_NAME TEXT("223.4.233.88")
#define SERVER_PORT 80
#define OBJECT_NAME TEXT("/hep/barcodepost.aspx")

// CDepotManager dialog

class CDepotManager : public CDialog
{
	DECLARE_DYNAMIC(CDepotManager)

public:
	CDepotManager(CWnd* pParent = NULL);   // standard constructor
	virtual ~CDepotManager();

	CHttpPostData	m_HttpPostData;
	HBRUSH m_RedBrush,m_GreenBrush;
	BOOL			m_UpLoading;


// Dialog Data
	enum { IDD = IDD_DEPOT_MANAGER };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support

	DECLARE_MESSAGE_MAP()
public:
	afx_msg void OnBnClickedBtnOrdermanage();
	afx_msg void OnBnClickedBtnScan();
	afx_msg void OnBnClickedUpload();
protected:
	virtual LRESULT WindowProc(UINT message, WPARAM wParam, LPARAM lParam);
public:
	virtual BOOL OnInitDialog();
	afx_msg HBRUSH OnCtlColor(CDC* pDC, CWnd* pWnd, UINT nCtlColor);
};
