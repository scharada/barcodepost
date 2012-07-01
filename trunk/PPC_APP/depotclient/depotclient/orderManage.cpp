// orderManage.cpp : implementation file
//

#include "stdafx.h"
#include "depotclient.h"
#include "orderManage.h"
#include "depotDB.h"
#include "poEditDlg.h"


// COrderManage dialog

IMPLEMENT_DYNAMIC(COrderManage, CDialog)

COrderManage::COrderManage(CWnd* pParent /*=NULL*/)
	: CDialog(COrderManage::IDD, pParent)
{

}

COrderManage::~COrderManage()
{
}

void COrderManage::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_ORDER_LIST, m_OrderList);
}


BEGIN_MESSAGE_MAP(COrderManage, CDialog)
	ON_BN_CLICKED(IDC_BTN_RETURN, &COrderManage::OnBnClickedBtnReturn)
	ON_BN_CLICKED(IDC_BTN_NEW, &COrderManage::OnBnClickedBtnNew)
	ON_BN_CLICKED(IDC_BTN_MOD, &COrderManage::OnBnClickedBtnMod)
	ON_BN_CLICKED(IDC_BTN_DEL, &COrderManage::OnBnClickedBtnDel)
END_MESSAGE_MAP()


// COrderManage message handlers

void COrderManage::OnBnClickedBtnReturn()
{
	// TODO: Add your control notification handler code here
	CDialog::OnOK();
}


BOOL COrderManage::OnInitDialog()
{
	CDialog::OnInitDialog();
	CArray<CString> array;

	// TODO:  Add extra initialization here
	CDepotDB::GetDB().GetPoList(array);

	for(int i = 0 ; i < array.GetSize() ; i++)
	{
		m_OrderList.AddString(array[i]);
	}

	return TRUE;  // return TRUE unless you set the focus to a control
	// EXCEPTION: OCX Property Pages should return FALSE
}

void COrderManage::OnBnClickedBtnNew()
{
	// TODO: Add your control notification handler code here
	CPOEditDlg dlg;
	if(dlg.DoModal()==IDOK)
	{
		if(CDepotDB::GetDB().AddPo(dlg.m_PONO))
		{
			//m_PoArray.Add(dlg.m_PONO);
			m_OrderList.AddString(dlg.m_PONO);
			UpdateData(FALSE);
		}
	}
}

void COrderManage::OnBnClickedBtnMod()
{
	INT cur_idx;
	if(LB_ERR  == (cur_idx = m_OrderList.GetCurSel()))
	{
		return;
	}
	CString pono;
	m_OrderList.GetText(cur_idx,pono);

	CPOEditDlg dlg;
	if(dlg.DoModal()==IDOK)
	{
		if(CDepotDB::GetDB().ModPo(pono,dlg.m_PONO))
		{
			pono = dlg.m_PONO;
		}
		CArray<CString> array;
		CDepotDB::GetDB().GetPoList(array);

		m_OrderList.ResetContent();

		for(int i = 0 ; i < array.GetSize() ; i++)
		{
			m_OrderList.AddString(array[i]);
		}
	}


}

void COrderManage::OnBnClickedBtnDel()
{
	// TODO: Add your control notification handler code here
	INT cur_idx;
	if(LB_ERR  == (cur_idx = m_OrderList.GetCurSel()))
	{
		return;
	}

	CString str_pono;
	m_OrderList.GetText(cur_idx,str_pono);

	if(CDepotDB::GetDB().DelPo(str_pono))
	{
		m_OrderList.DeleteString(cur_idx);
	}

	CArray<CString> array;
	CDepotDB::GetDB().GetPoList(array);

	m_OrderList.ResetContent();

	for(int i = 0 ; i < array.GetSize() ; i++)
	{
		m_OrderList.AddString(array[i]);
	}
}
