

function deleteRows() {
    rows = $('#mainTable').datagrid('getSelections');
    data = rows.concat()
    if (rows.length == 0) {
        $.messager.alert("提示", "请选择记录！", "info");
    }
    else {
        $.messager.confirm('订单删除', '您确定要删除选中的订单?', function (r) {
            if (r) {
                for (var i = 0; i < data.length; i++) {
                    index = $('#mainTable').datagrid('getRowIndex', data[i]);
                    $('#mainTable').datagrid('deleteRow', index);
                }
                deleteOrders(data);
            }
        });
    }
}

function deleteRow(index) {
    var list = index + 1;
    $.messager.confirm('订单删除', '确定把第' + list + '条记录删除?', function (r) {
        if (r) {
            var orderId = $('#mainTable').datagrid('getRows')[index];
            $('#mainTable').datagrid('deleteRow', index);
            deleteOrder(orderId);
        }
    });
}

function deleteOrder(orderId) {
    if (orderId == null) {
        return
    }
    var _order = new Object();
    _order.OperType = "deleteorder";
    _order.Onum = orderId;

    var json = JSON.stringify(_order);

    $.ajax({
        url: getFactoryUIPath(),
        type: "post", //以post的方式（该方式能传大量数据）
        dataType: "text", //返回的类型（即下面sucess：中data的类型）
        data: json,
        async: false, //同步进行
        success: function (data) {
            if (data == "ok") {
                $.messager.alert("提示", "订单删除成功", "info");
                $('#deleteDialog').dialog('close');
            }
            else {
                $.messager.alert("提示", "订单删除失败", "error");
                $('#deleteDialog').dialog('close');
            }
        }
    });
}

function deleteOrders(orderIds) {
    if (orderIds == null) {
        return
    }
    if (orderIds.length == 1) {
        deleteOrder(orderIds[0].Onum);
    }
    else {
        var tmp = "";
        for (i = 0; i < orderIds.length; i++) {
            tmp += "'" + orderIds[i].Onum + "',";
        }
        tmp = tmp.substring(0, tmp.length - 1);
        var _order = new Object();
        _order.OperType = "deleteorders";
        _order.Onum = tmp;

        var json = JSON.stringify(_order);

        $.ajax({
            url: getFactoryUIPath(),
            type: "post", //以post的方式（该方式能传大量数据）
            dataType: "text", //返回的类型（即下面sucess：中data的类型）
            data: json,
            async: false, //同步进行
            success: function (data) {
                if (data == "ok") {
                    $.messager.alert("提示", "订单删除成功", "info");
                    $('#deleteDialog').dialog('close');
                }
                else {
                    $.messager.alert("提示", "订单删除失败", "error");
                    $('#deleteDialog').dialog('close');
                }
            }
        });
    }
}



