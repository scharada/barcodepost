var _O = '', _G = '', _P = '',_S = '',_F = '', _T = '';

function initializeMainGrid() {
    $('#mainTable').datagrid({
        loadMsg: '数据装载中......',
        iconCls: 'icon-save',
        width: 'auto',
        height: 'auto',
        nowrap: false,
        striped: true,
        singleSelect: false,
        fitColumns: true,
        pagination: true,
        pageSize: 20,
        rownumbers: true,
        collapsible: true,
        fit: true,
        url: getFactoryUIPath(),
        queryParams: { OperType: 'getallorder' },
        dataType: 'json',
        sortName: 'RT',
        sortOrder: 'RT',
        sortOrder: 'DESC ',
        idField: 'Onum',
        columns: [[{
            field: 'ck',
            checkbox: true
        }, {
            field: 'Onum',
            title: '扫描流水号',
            align: 'center',
            width: 160,
            sortable: true
        }, {
            field: 'PO',
            title: '发运单号',
            align: 'center',
            width: 120,
            sortable: true
        }, {
            field: 'GN',
            title: '仓库代码',
            align: 'center',
            width: 80,
            sortable: true
        }, {
            field: 'SN',
            title: '扫描序列号',
            align: 'center',
            width: 120,
            sortable: true
        }, {
            field: 'ST',
            title: '扫描时间',
            align: 'center',
            width: 160,
            sortable: true
        }, {
            field: 'RT',
            title: '接收确认时间',
            align: 'center',
            width: 160,
            sortable: true
        }, {
            field: 'OW',
            title: '待修改流水号',
            align: 'center',
            width: 120,
            hidden: true,
            sortable: true
        }, {
            field: 'Flag',
            title: '修改操作标识',
            align: 'center',
            width: 120,
            hidden: true,
            sortable: true
        }, {
            field: 'action',
            title: '操作',
            width: 60,
            align: 'center',
            formatter: function (value, row, index) {
                if (row.editing) {
                    var s = '<a href="javascript:void(0)" onclick="saverow(' + index + ')">保存</a> ';
                    var c = '<a href="javascript:void(0)" onclick="cancelrow(' + index + ')">取消</a>';
                    return s + c;
                } else {
                    var e = '<a href="javascript:void(0)" onclick="reviseRow(' + index + ')">编辑</a> ';
                    return e ;
                }
            }
        }]],
        toolbar: [{
            id: 'btnRefresh',
            text: '刷新',
            iconCls: 'icon-reload',
            handler: function () {
                refreshDatagrid();
            }
        }, {
            id: 'btnAdd',
            text: '新增',
            iconCls: 'icon-add',
            handler: function () {
                addRows();
            }
        }, {
            id: 'btnRevise',
            text: '修改',
            iconCls: 'icon-edit',
            handler: function () {
                var row = $('#mainTable').datagrid('getSelections');
                reviseRows(row);
            }
        }, {
            id: 'btnDelete',
            text: '删除',
            iconCls: 'icon-remove',
            handler: function () {
                deleteRows();
            }
        }, '-', {
            id: 'btnExportExcel',
            text: '订单导出',
            iconCls: 'icon-save',
            handler: function () {
                initializeExportExcel();
            }
        }, '-', {
            id: 'btnEditPassword',
            text: '修改密码',
            iconCls: 'icon-help',
            handler: function () {
                initializeEditPassword();
            }
        }],
        onSelect: function (rowIndex, rowData) {
        },
        onLoadSuccess: function () {
        },
        onDblClickRow: function () {
        }
    });
    $("#btnExportExcel").linkbutton('disable');
}

