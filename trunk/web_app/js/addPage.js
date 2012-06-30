function initializeAddWindow(wHeight, wWidth) {
    $('#addWindow').window({
        width: 400,
        height: 400,
        title: "订单录入",
        top: (wHeight - 400) * 0.5,
        left: (wWidth - 400) * 0.5,
        iconCls: 'icon-add', //图标class  
        collapsible: false, //折叠
        minimizable: false, //最小化
        maximizable: false, //最大化
        resizable: true, //改变窗口大小
        modal: true
    });
    $("#reviseBtn").hide();
    $("#addBtn").show();
    $("#clearBtn").show();

    $("#addTips").text('');
    $("#addTips").hide();

    $("#onumAdd").removeAttr("disabled"); //要变成Enable，JQuery只能这么写  
    $("#stAdd").removeAttr("disabled"); //再改成disabled  
}

function cleardata() {
    $('#addForm').form('clear');
}

function addOrder() {

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
        $("#addTips").text('订单号不能为空');
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
    //构造json数据对象

    var _order = new Object();
    _order.OperType = "addOrder";
    _order.Onum = _onumAdd;
    _order.GN = _gnAdd;
    _order.PO = _poAdd;
    _order.SN = _snAdd;
    _order.ST = _stAdd;
    _order.OW = _owAdd;
    _order.Flag = _flagAdd;

    var json = JSON.stringify(_order);

    $.ajax({
        url: getFactoryUIPath(),
        type: "post", //以post的方式（该方式能传大量数据）
        dataType: "text", //返回的类型（即下面sucess：中data的类型）
        data: json,
        async: true, //异步进行
        success: function (data) {
            if (data == "ok") {
                $("#addTips").text('添加成功！');
                $("#addTips").show();
                cleardata();
                //重新载入数据
                $("#mainTable").datagrid('reload');
            } else {
                $("#addTips").text('添加失败！');
                $("#addTips").show();
            }
        },
        error: function (data) {
            $("#addTips").text('添加失败！');
            $("#addTips").show();
        }
    });
}