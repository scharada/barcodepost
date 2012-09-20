<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>惠而浦货物条码采集监控系统</title>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link rel="stylesheet" type="text/css" href="easyui/themes/default/easyui.css">
    <link rel="stylesheet" type="text/css" href="easyui/themes/icon.css">
    <script type="text/javascript" src="jQuery/js/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="easyui/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="js/json2.js"></script>
    <script type="text/javascript" src="js/util.js"></script>
    <script type="text/javascript" src="js/deletePage.js"></script>
    <script type="text/javascript" src="js/searchPage.js"></script>
    <script type="text/javascript" src="js/addPage.js"></script>
    <script type="text/javascript" src="js/revisePage.js"></script>
    <script type="text/javascript" src="js/mainPage.js"></script>
    <script type="text/javascript">
        function initializelogWindow() {
            $('#logWindow').window({
                width: 380,
                height: 160,
                title: "用户登录",
                iconCls: 'icon-add', //图标class  
                collapsible: false, //折叠
                minimizable: false, //最小化
                maximizable: false, //最大化
                resizable: false, //改变窗口大小
                closable: false,
                modal: true
            });
        }
        function Logerror() {
            $.messager.alert('提示', '登录失败', 'error');
        }
        function LogIn() {

            var userName = $("#userName").val();
            var passWord = $("#passWord").val();
            if (passWord == "")
                $.messager.alert('提示', '密码不能为空', 'error');

            $.ajax({
                url: getLoginPath(),
                type: "post", //以post的方式（该方式能传大量数据）
                dataType: "text", //返回的类型（即下面sucess：中data的类型）
                data: "userName=" + userName + "&passWord=" + passWord,
                cache: false,
                async: true, //异步进行
                beforeSend: function () {
                },
                success: function (data) {
                    if (data == "ok")
                        window.location.href = "MainPage.aspx";
                    else
                        $.messager.alert('提示', '登录失败', 'error');
                },
                error: function (data) {
                    $.messager.alert('提示', '登录失败', 'error');
                }
            });
        }
        $(function () {
            initializelogWindow();
            $('#userName').attr("disabled", "disabled"); //再改成disabled  
        });
    </script>
</head>
<body>
    <div id="logWindow" class="easyui-window" closed="false" style="width: 400px; height: 200px;
        padding: 5px 5px 5px 5px">
        <table width="360" border="0" style="padding: 5px 5px 5px 5px">
            <tr>
                <td>
                    <label for="userName">
                        用户名：</label>
                </td>
                <td>
                    <input class="easyui-validatebox" type="text" value="admin" id="userName" name="userName"
                        style="width: 240px"> </input>
                </td>
            </tr>
            <tr>
                <td>
                    <label for="passWord">
                        密码：</label>
                </td>
                <td>
                    <input id="passWord" name="passWord" style="width: 240px" validtype="length[3,32]"
                        class="easyui-validatebox" required="true" type="password" value="" />
                </td>
            </tr>
        </table>
        <div style="margin: 10px 0;" align="center">
            <a href="javascript:void(0)" class="easyui-linkbutton" id="loginButton" onclick="LogIn()"
                iconcls="icon-ok">登录</a>
        </div>
        <div id="loginTips" align="center" style="color: #FF0000; display: none">
        </div>
    </div>
</body>
</html>
