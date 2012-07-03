
function initializeReviseWindow(row) {
    if (row == null)
        return;
    if (row.length >= 1)
        row = row[0];
    $("#reviseBtn").show();
    $("#addBtn").hide();
    $("#clearBtn").hide();

    $("#addTips").text('');
    $("#addTips").hide();

    $("#onumAdd").val(row.Onum);
    $("#onumAdd").attr("disabled", "disabled"); //再改成disabled  
    $("#gnAdd").val(row.GN);
    $("#poAdd").val(row.PO);
    $("#snAdd").val(row.SN);
    $("#stAdd").datetimebox('setValue', row.ST);
    $("#stAdd").attr("disabled", "disabled"); //再改成disabled 
    $("#forflagAdd").show();
    $("#forowAdd").show();
    $("#flagAdd").show();
    $("#owAdd").show();
    $("#flagAdd").val(row.Flag);
    $("#owAdd").val(row.OW);
}
function reviseOrder() {

    //验证条件
    var _onumAdd = $.trim($("#onumAdd").val());
    var _gnAdd = $.trim($("#gnAdd").val());
    var _poAdd = $.trim($("#poAdd").val());
    var _snAdd = $.trim($("#snAdd").val());
    var _stAdd = $.trim($("#stAdd").datetimebox('getValue'));
    var _flagAdd = $.trim($("#flagAdd").val());
    var _owAdd = $.trim($("#owAdd").val());

    if (_onumAdd == "") {
        $("#addTips").text('流水号不能为空');
        $("#addTips").show();
        return;
    } else if (_gnAdd == "") {
        $("#addTips").text('仓库代码不能为空');
        $("#addTips").show();
        return;
    } else if (_poAdd == "") {
        $("#addTips").text('发运单号不能为空');
        $("#addTips").show();
        return;
    } else if (_snAdd == "") {
        $("#addTips").text('扫描序列号不能为空');
        $("#addTips").show();
        return;
    } else if (_stAdd == "") {
        $("#addTips").text('扫描时间不能为空');
        $("#addTips").show();
        return;
    } else if (_flagAdd == "") {
        $("#addTips").text('修改操作标识不能为空');
        $("#addTips").show();
        return;
    } else if (_owAdd == "") {
        $("#addTips").text('待修改流水号不能为空');
        $("#addTips").show();
        return;
    }
    $("#addTips").text('');
    $("#addTips").hide();
    
    var _order = new Object();
    _order.OperType = "reviseorder";
    _order.Onum = _onumAdd;
    _order.GN = _gnAdd;
    _order.PO = _poAdd;
    _order.SN = _snAdd;
    _order.ST = _stAdd;
    _order.OW = _owAdd;
    _order.Flag = _flagAdd;
    var jsonAdd = JSON.stringify(_order);
    $.ajax({
        url: getFactoryUIPath(),
        type: "post", //以post的方式（该方式能传大量数据）
        dataType: "text", //返回的类型（即下面sucess：中data的类型）
        data: jsonAdd,
        async: false, //同步进行
        success: function (data) {
            if (data == "ok") {
                $.messager.alert("提示", "修改订单信息成功", "info");
                cleardata();
                //重新载入数据
                $("#mainTable").datagrid('reload');
            } else {
            $.messager.alert("提示", "修改订单信息失败", "error");
            }
        },
        error: function (data) {
             $.messager.alert("提示", "修改订单信息失败", "error");
        }
    });
    //关闭
    $('#addWindow').window('close');
    //重新载入数据
    $("#mainTable").datagrid('reload');
}




function reviseRows() {
 var row = $('#mainTable').datagrid('getSelections');
                if (row != null&&row.length>0) {
                    initializeReviseWindow(row);
                       $('#addWindow').window({
                           title: "订单修改"
                       }).window('open');
                }
                else{
                 $.messager.alert("提示", "请选择要修改的订单", "info");
        return;
                }
}
function reviseRow(index){
            var row = $('#mainTable').datagrid('getRows')[index];
                    initializeReviseWindow(row);
                       $('#addWindow').window({
                           title: "订单修改"
                       }).window('open');
             
}
function reviseRowFromSearch(index){
            var row = $('#searchTable').datagrid('getRows')[index];
                    initializeReviseWindow(row);
                       $('#addWindow').window({
                           title: "订单修改"
                       }).window('open');
             
}