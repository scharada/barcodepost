#pragma once
#include "afxwin.h"


// COrderManage dialog

class COrderManage : public CDialog
{
	DECLARE_DYNAMIC(COrderManage)

public:
	COrderManage(CWnd* pParent = NULL);   // standard constructor
	virtual ~COrderManage();

// Dialog Data
	enum { IDD = IDD_ORDER_MANAGE };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support

	DECLARE_MESSAGE_MAP()
public:
	afx_msg void OnBnClickedBtnReturn();
	CListBox m_OrderList;
	virtual BOOL OnInitDialog();
	afx_msg void OnBnClickedBtnNew();
	afx_msg void OnBnClickedBtnMod();
	afx_msg void OnBnClickedBtnDel();
};
