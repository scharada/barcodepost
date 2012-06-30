
function initializeSearchTable() {
    $('#searchTable').datagrid({
        loadMsg: '数据装载中......',
        iconCls: 'icon-save',
        width: 'auto',
        height: 'auto',
        nowrap: false,
        striped: true,
        singleSelect: true,
        fitColumns: true,
        pagination: false,
        rownumbers: true,
        collapsible: true,
        singleSelect: true,
        fit: true,
        pagination: true,
        url: getFactoryUIPath(),
        queryParams: { OperType: 'getnull' },
        dataType: 'json',
        sortName: 'Onum',
        sortOrder: 'Onum',
        idField: 'Onum',
        columns: [[{
            field: 'Onum',
            title: '扫描流水号',
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
            field: 'PO',
            title: '订单号',
            align: 'center',
            width: 120,
            sortable: true
        }, {
            field: 'SN',
            title: '扫描序列号',
            align: 'center',
            width: 120,
            sortable: true
        }]]
    });
}

function initializeSearchWindow(wHeight,wWidth) {
    initializeSearchTable();
    $('#searchWindow').window({
        width: 800,
        height: 400,
        title: "订单查询",
        iconCls: 'icon-search', //图标class
        top: (wHeight - 400) * 0.5,
        left: (wWidth - 800) * 0.5,
        collapsible: false, //折叠
        minimizable: false, //最小化
        maximizable: false, //最大化
        resizable: true, //改变窗口大小
        modal: true
    });
}
function searchOrder() {
    initializeSearch();
    $('#searchWindow').window('open');
}

function getOrderByOnum() {
    var _value = $("#searchWindow").find("input[name='Onum']").val();
    if (_value == "") {
        $.messager.alert("提示", "请输入查询关键字", "info");
        return;
    }
    //initializeSearchTable()添加参数  
    var queryParams = $('#searchTable').datagrid('options').queryParams;
    queryParams.Query = "true";
    queryParams.OperType = "getorderby";
    queryParams.Onum = _value;
    queryParams.GN = "";
    queryParams.PO = "";
    queryParams.SN = "";
    //重新加载datagrid的数据  
    $("#searchTable").datagrid('reload');
}

function getOrderByGN() {
    var _value = $("#searchWindow").find("input[name='GN']").val();
    if (_value == "") {
        $.messager.alert("提示", "请输入查询关键字", "info");
        return;
    }
    //initializeSearchTable()添加参数  
    var queryParams = $('#searchTable').datagrid('options').queryParams;
    queryParams.Query = "true";
    queryParams.OperType = "getorderby";
    queryParams.Onum = "";
    queryParams.GN = _value;
    queryParams.PO = "";
    queryParams.SN = "";
    $("#searchTable").datagrid('reload');
}

function getOrderByPO() {
    var _value = $("#searchWindow").find("input[name='PO']").val();
    if (_value == "") {
        $.messager.alert("提示", "请输入查询关键字", "info");
        return;
    }
    //initializeSearchTable()添加参数  
    var queryParams = $('#searchTable').datagrid('options').queryParams;
    queryParams.Query = "true";
    queryParams.OperType = "getorderby";
    queryParams.Onum = "";
    queryParams.GN = "";
    queryParams.PO = _value;
    queryParams.SN = "";
    $("#searchTable").datagrid('getRows');
}

function getOrderBySN() {
    var _value = $("#searchWindow").find("input[name='SN']").val();
    if (_value == "") {
        $.messager.alert("提示", "请输入查询关键字", "info");
        return;
    }
    //initializeSearchTable()添加参数  
    var queryParams = $('#searchTable').datagrid('options').queryParams;
    queryParams.Query = "true";
    queryParams.OperType = "getorderby";
    queryParams.Onum = "";
    queryParams.GN = "";
    queryParams.PO = "";
    queryParams.SN = _value;
    $("#searchTable").datagrid('reload');
}


function initializeSearch() {
    $("#searchWindow").find("input[name='Onum']").val('');
    $("#searchWindow").find("input[name='GN']").val('');
    $("#searchWindow").find("input[name='PO']").val('');
    $("#searchWindow").find("input[name='SN']").val('');
    rows = $('#searchTable').datagrid('getRows');
    if (rows) {
        data = rows.concat()
        for (var i = 0; i < data.length; i++) {
            index = $('#searchTable').datagrid('getRowIndex', data[i]);
            index = $('#searchTable').datagrid('getRowIndex', data[i]);
            $('#searchTable').datagrid('deleteRow', index);
        } 
    }
}

function getOrderByDateTimeRange() {
    var _fromDatetime = $.trim($("#fromDatetime").datetimebox('getValue'));
    var _toDatetime = $.trim($("#toDatetime").datetimebox('getValue'));

    if (_fromDatetime == "") {
        $.messager.alert("提示", "请输入开始时间", "error");
        return;
    }
    if (_toDatetime == "") {
        $.messager.alert("提示", "请输入截止时间", "error");
        return;
    }
    if (_fromDatetime>_toDatetime) {
        $.messager.alert("提示", "请输入正确的时间范围", "error");
        return;
    }
    _fromDatetime = _fromDatetime.replace(/-/g, "").replace(/:/g, "").replace(/\s+/g, "");
    _toDatetime = _toDatetime.replace(/-/g, "").replace(/:/g, "").replace(/\s+/g, ""); 
    
    //initializeSearchTable()添加参数  
    var queryParams = $('#searchTable').datagrid('options').queryParams;
    queryParams.Query = "true";
    queryParams.OperType = "getorderbyrange";
    queryParams.Onum = "";
    queryParams.GN = "";
    queryParams.PO = "";
    queryParams.SN = "";
    queryParams.FromDateTime = _fromDatetime;
    queryParams.ToDateTime = _toDatetime;
    $("#searchTable").datagrid('reload');
}

