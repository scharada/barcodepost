#pragma once
#include "afxwin.h"


// CPOEditDlg dialog

class CPOEditDlg : public CDialog
{
	DECLARE_DYNAMIC(CPOEditDlg)

public:
	CPOEditDlg(CWnd* pParent = NULL);   // standard constructor
	virtual ~CPOEditDlg();

// Dialog Data
	enum { IDD = IDD_PO_EDIT };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support

	DECLARE_MESSAGE_MAP()
public:
	afx_msg void OnBnClickedOk();
	CString m_PONO;
};
