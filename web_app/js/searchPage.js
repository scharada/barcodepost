function refreshDatagrid() {
    $("#centerregion").find("input[name='Onum']").val('');
    $("#centerregion").find("input[name='GN']").val('');
    $("#centerregion").find("input[name='PO']").val('');
    $("#centerregion").find("input[name='SN']").val('');
    $("#fromDatetime").datetimebox('setValue', '');
    $("#toDatetime").datetimebox('setValue', '');
    getOrderAll();
}

function getOrderAll() {
    var queryParams = $('#mainTable').datagrid('options').queryParams;
    queryParams.Query = "true";
    queryParams.OperType = "getallorder";
    queryParams.Onum = "";
    queryParams.GN = "";
    queryParams.PO = "";
    queryParams.SN = "";
    $("#mainTable").datagrid('load');
}

function getOrderByCompose() {
    var _Onum = $("#centerregion").find("input[name='Onum']").val();
    var _GN = $("#centerregion").find("input[name='GN']").val();
    var _PO = $("#centerregion").find("input[name='PO']").val();
    var _SN = $("#centerregion").find("input[name='SN']").val();
    var _fromDatetime = $.trim($("#fromDatetime").datetimebox('getValue'));
    var _toDatetime = $.trim($("#toDatetime").datetimebox('getValue'));

    if (_Onum == "" && _GN == "" && _PO == "" && _SN == "" && _fromDatetime == "" && _toDatetime == "") {
        $.messager.alert("提示", "请输入查询关键字", "info");
        return;
    }
    if (_fromDatetime != "" && _toDatetime != "") {
        if (_fromDatetime > _toDatetime) {
            $.messager.alert("提示", "请输入正确的时间范围", "error");
            return;
        }
    }
    var queryParams = $('#mainTable').datagrid('options').queryParams;
    queryParams.Query = "true";
    queryParams.OperType = "getorderby";
    queryParams.Onum = $("#centerregion").find("input[name='Onum']").val();
    queryParams.GN = $("#centerregion").find("input[name='GN']").val();
    queryParams.PO = $("#centerregion").find("input[name='PO']").val();
    queryParams.SN = $("#centerregion").find("input[name='SN']").val();
    _fromDatetime = _fromDatetime.replace(/-/g, "").replace(/:/g, "").replace(/\s+/g, "");
    _toDatetime = _toDatetime.replace(/-/g, "").replace(/:/g, "").replace(/\s+/g, ""); 
    queryParams.FromDateTime = _fromDatetime;
    queryParams.ToDateTime = _toDatetime;
    $("#mainTable").datagrid('reload');

}

