#pragma once
#include "afxwin.h"


// CLoginDlg dialog

class CLoginDlg : public CDialog
{
	DECLARE_DYNAMIC(CLoginDlg)

public:
	CLoginDlg(CWnd* pParent = NULL);   // standard constructor
	virtual ~CLoginDlg();

// Dialog Data
	enum { IDD = IDD_DEPOT_LOGIN };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support

	DECLARE_MESSAGE_MAP()
public:
	CArray<CString> m_DepoArray;
	// ≤÷ø‚¡–±Ì
	CComboBox	m_combDepotList;
	// √‹¬Î
	CString		m_strPassWd;
	afx_msg void OnBnClickedBtnLogin();
	virtual BOOL OnInitDialog();
protected:
	virtual LRESULT WindowProc(UINT message, WPARAM wParam, LPARAM lParam);
};
