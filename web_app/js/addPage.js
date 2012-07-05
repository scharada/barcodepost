function initializeAddWindow(wHeight, wWidth) {
	$('#addWindow').window({
		width : 400,
		height : 400,
		title : "订单录入",
		left : (wWidth - 400) * 0.5,
		top : (wHeight - 400) * 0.5,
		iconCls : 'icon-add', //图标class
		collapsible : false, //折叠
		minimizable : false, //最小化
		maximizable : false, //最大化
		resizable : true, //改变窗口大小
		modal : true
	});
}

function initializeEditWindow(wHeight, wWidth) {
	$('#EditWindow').window({
		width : 400,
		height : 200,
		title : "密码修改",
		left : (wWidth - 400) * 0.5,
		top : (wHeight - 200) * 0.5,
		iconCls : 'icon-add', //图标class
		collapsible : false, //折叠
		minimizable : false, //最小化
		maximizable : false, //最大化
		resizable : true, //改变窗口大小
		modal : true
	});
}
function initializeExportExcel() {
    var opt = $("#mainTable").datagrid('options');
    var total = opt.total;
 if(total>=65536)
 {
        $.messager.alert("提示", "导出记录数大于Excel可容纳行数", "error");
		return;
        }
    _F = _F.replace(/-/g, "").replace(/:/g, "").replace(/\s+/g, "");
    _T = _T.replace(/-/g, "").replace(/:/g, "").replace(/\s+/g, ""); 
    window.open('ExportExcel.aspx?O='+_O+"&&G="+_G+"&&P="+_P+"&&S="+_S+"&&F="+_F+"&&T="+_T,'下载订单', 'height=100, width=400, top=0,left=0, toolbar=no, menubar=no, scrollbars=no, resizable=no,location=no, status=no');
}

function initializeEditPassword() {
	$("#uesrName").val('admin');
	$("#uesrName").attr("disabled", "disabled");
	//再改成disabled
	$("#newPassword").val('');
	$("#newPasswordAgain").val('');
	$('#EditWindow').window('open');
}

function editPassword() {
	var np = $("#newPassword").val();
	var npa = $("#newPasswordAgain").val();
	var userName = $("#uesrName").val()
	if(np.length < 3 || np.length > 10) {
		$.messager.alert("提示", "请输入正确的密码", "error");
		return;
	}
	if(npa.length < 3 || npa.length > 10) {
		$.messager.alert("提示", "请输入正确的密码", "error");
		return;
	}
	if(np != npa) {
		$.messager.alert("提示", "请输入相同的密码", "error");
		return;
	}
	$.ajax({
		url : getEditPasswordPath(),
		type : "post", //以post的方式（该方式能传大量数据）
		dataType : "text", //返回的类型（即下面sucess：中data的类型）
		data : "userName=" + userName + "&passWord=" + np,
		cache : false,
		async : true, //异步进行
		beforeSend : function() {
		},
		success : function(data) {
			if(data == "ok") {
				$.messager.alert('提示', '密码修改成功', 'info');
				$('#EditWindow').window('close');
			} else
				$.messager.alert('提示', '密码修改失败', 'error');
		},
		error : function(data) {
			$.messager.alert('提示', '密码修改失败', 'error');
		}
	});
}

function initializeAddOrder() {
	cleardata();
	$("#reviseBtn").hide();
	$("#addBtn").show();
	$("#clearBtn").show();

	$("#addTips").text('');
	$("#addTips").hide();

	$("#onumAdd").removeAttr("disabled");
	$("#stAdd").removeAttr("disabled");

	$("#forflagAdd").hide();
	$("#forowAdd").hide();
	$("#flagAdd").hide();
	$("#owAdd").hide();

	$('#addWindow').window({
		title : "订单录入"
	});
}

function cleardata() {
	$("#onumAdd").val('');
	$("#gnAdd").val('');
	$("#poAdd").val('');
	$("#snAdd").val('');
	$("#flagAdd").val('');
	$("#owAdd").val('');
	$("#stAdd").datetimebox('setValue', '');
}

function addRows() {
	initializeAddOrder();
	$('#addWindow').window('open');
}

function addOrder() {

	var _onumAdd = $.trim($("#onumAdd").val());
	var _gnAdd = $.trim($("#gnAdd").val());
	var _poAdd = $.trim($("#poAdd").val());
	var _snAdd = $.trim($("#snAdd").val());
	var _stAdd = $.trim($("#stAdd").datetimebox('getValue'));
	var _flagAdd = "0";
	var _owAdd = "0";

	if(_onumAdd == "") {
		$("#addTips").text('流水号不能为空');
		$("#addTips").show();
		return;
	} else if(_gnAdd == "") {
		$("#addTips").text('仓库代码不能为空');
		$("#addTips").show();
		return;
	} else if(_poAdd == "") {
		$("#addTips").text('发运单号不能为空');
		$("#addTips").show();
		return;
	} else if(_snAdd == "") {
		$("#addTips").text('扫描序列号不能为空');
		$("#addTips").show();
		return;
	} else if(_stAdd == "") {
		$("#addTips").text('扫描时间不能为空');
		$("#addTips").show();
		return;
	} else if(_flagAdd == "") {
		$("#addTips").text('修改操作标识不能为空');
		$("#addTips").show();
		return;
	} else if(_owAdd == "") {
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
		url : getFactoryUIPath(),
		type : "post", //以post的方式（该方式能传大量数据）
		dataType : "text", //返回的类型（即下面sucess：中data的类型）
		data : json,
		async : true, //异步进行
		success : function(data) {
			if(data == "ok") {
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
		error : function(data) {
			$("#addTips").text('添加失败！');
			$("#addTips").show();
		}
	});
}

