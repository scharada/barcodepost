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
        rownumbers: true,
        collapsible: true,
        fit: true,
        url: getFactoryUIPath(),
        queryParams: { opertype: 'getallorder' },
        dataType: 'json',
        sortName: 'Onum',
        sortOrder: 'Onum',
        idField: 'Onum',
        columns: [[{
            field: 'ck',
            checkbox: true
        }, {
            field: 'Onum',
            title: '扫描流水号',
            align: 'center',
            width: 120,
            sortable: true
        }, {
            field: 'PO',
            title: '订单号',
            align: 'center',
            width: 120,
            sortable: true
        }, {
            field: 'GN',
            title: '仓库代码',
            align: 'center',
            width: 120,
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
            sortable: true
        }, {
            field: 'Flag',
            title: '修改操作标识',
            align: 'center',
            width: 120,
            sortable: true
        }, {
            field: 'action',
            title: '操作',
            width: 120,
            align: 'center',
            formatter: function (value, row, index) {
                if (row.editing) {
                    var s = '<a href="#" onclick="saverow(' + index + ')">保存</a> ';
                    var c = '<a href="#" onclick="cancelrow(' + index + ')">取消</a>';
                    return s + c;
                } else {
                    var e = '<a href="#" onclick="reviseRow(' + index + ')">编辑</a> ';
                    var d = '<a href="#" onclick="deleteRow(' + index + ')">删除</a>';
                    return e + d;
                }
            }
        }]],
        toolbar: [{
            id: 'btnRefresh',
            text: '刷新',
            iconCls: 'icon-reload',
            handler: function () {
                $("#mainTable").datagrid('reload');
            }
        }, {
            id: 'btnSearch',
            text: '查询',
            iconCls: 'icon-search',
            handler: function () {
                setSearchCaseEmpty();
                $('#searchWindow').window('open');
            }
        }, {
            id: 'btnAdd',
            text: '新增',
            iconCls: 'icon-add',
            handler: function () {
                initializeAddOrder()
                $('#addWindow').window('open');
            }
        }, {
            id: 'btnRevise',
            text: '编辑',
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
        }],
        onSelect: function (rowIndex, rowData) {

        },
        onLoadSuccess: function () {
        },
        onDblClickRow: function () {
            var row = $('#mainTable').datagrid('getSelected');
        }
    });
}

