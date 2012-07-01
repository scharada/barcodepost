<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MainPage.aspx.cs" Inherits="MainPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <title>惠而浦货物条码采集监控系统</title>
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

        $(function () {
            var wHeight = $(window).height();
            var wWidth = $(window).width();
            initializeMainGrid();
            initializeAddWindow(wHeight, wWidth);
        });

    </script>
</head>
<body class="easyui-layout">
    <!-- 		主页面布局 -->
    <div region="north" border="false" style="height: 100px; background-repeat: repeat-x;
        background-image: url(image/banner.jpg);">
    </div>
    <div region="center" border="false" id="centerregion">
        <div class="easyui-layout" fit="true">
            <div region="north" border="false" style="height: 36px; padding-left: 5px; padding-top: 5px">
                <div>
                    <label for="Onum">
                        流水号:</label>
                    <input class="easyui-validatebox" type="text" name="Onum" style="width: 100px">
                    </input>
                    <label for="PO">
                        订单号:</label>
                    <input class="easyui-validatebox" type="text" name="PO" style="width: 100px"> </input>
                    <label for="GN">
                        仓库代码:</label>
                    <input class="easyui-validatebox" type="text" name="GN" style="width: 100px"> </input>
                    <label for="SN">
                        序列号:</label>
                    <input class="easyui-validatebox" type="text" name="SN" style="width: 100px"> </input>
                    <label for="fromDatetime">
                        扫描时间 从:</label>
                    <input class="easyui-datetimebox" type="text" id="fromDatetime" name="fromDatetime"
                        style="width: 200px"> </input>
                    <label for="toDatetime">
                        到:</label>
                    <input class="easyui-datetimebox" type="text" id="toDatetime" name="toDatetime" style="width: 200px">
                    </input>
                    <a href="javascript:getOrderByCompose()" class="easyui-linkbutton" plain="true"
                        iconcls="icon-search">查询</a><a href="javascript:refreshDatagrid()" class="easyui-linkbutton" plain="true"
                        iconcls="icon-no">清除查询</a>
                </div>
                <div id="serarchTips" align="center">
                </div>
            </div>
            <div id="mainCenterRegion" region="center" split="false" border="false">
                <table id="mainTable">
                </table>
            </div>
        </div>
    </div>
    <div region="south" border="false" style="height: 30px">
        <div align="center" style="color: #2E3E53; font-family: '宋体'; font-size: 12px; padding-top: 5px;
            padding-bottom: 5px">
            上海鼎方电子科技有限公司 版权所有 Copyright © 2009-2012，All Rights Reserved
        </div>
    </div>
    <!-- 		新增window -->
    <div id="addWindow" class="easyui-window" closed="true" style="width: 20px; height: 20px;
        padding: 5px 5px 5px 5px">
        <form id="addForm" novalidate="" method="post">
        <table width="360" border="0" style="padding: 5px 5px 5px 5px">
            <tr>
                <td>
                    <label for="onumAdd">
                        流水号：</label>
                </td>
                <td>
                    <input class="easyui-validatebox" type="text" id="onumAdd" name="onumAdd" style="width: 180px">
                    </input>
                </td>
            </tr>
            <tr>
                <td>
                    <label for="poAdd">
                        订单号：</label>
                </td>
                <td>
                    <input class="easyui-validatebox" type="text" id="poAdd" name="poAdd" style="width: 180px">
                    </input>
                </td>
            </tr>
            <tr>
                <td>
                    <label for="gnAdd">
                        仓库代码：</label>
                </td>
                <td>
                    <input class="easyui-validatebox" type="text" id="gnAdd" name="gnAdd" style="width: 180px">
                    </input>
                </td>
            </tr>
            <tr>
                <td>
                    <label for="snAdd">
                        扫描序列号：</label>
                </td>
                <td>
                    <input class="easyui-validatebox" type="text" id="snAdd" name="snAdd" style="width: 180px">
                    </input>
                </td>
            </tr>
            <tr>
                <td>
                    <label for="stAdd">
                        扫描时间 ：</label>
                </td>
                <td>
                    <input class="easyui-datetimebox" type="text" id="stAdd" name="stAdd" style="width: 185px">
                    </input>
                </td>
            </tr>
            <tr>
                <td>
                    <label id="forflagAdd">
                        修改操作标识：</label>
                </td>
                <td>
                    <input class="easyui-validatebox" type="text" id="flagAdd" name="flagAdd" style="width: 180px">
                    </input>
                </td>
            </tr>
            <tr>
                <td>
                    <label id="forowAdd">
                        待修改流水号：</label>
                </td>
                <td>
                    <input class="easyui-validatebox" type="text" id="owAdd" name="owAdd" style="width: 180px">
                    </input>
                </td>
            </tr>
        </table>
        <div style="margin: 10px 0;" align="center">
            <a href="javascript:void(0)" class="easyui-linkbutton" id="addBtn" onclick="addOrder()">
                添加</a> <a href="javascript:void(0)" class="easyui-linkbutton" id="reviseBtn" onclick="reviseOrder()"
                    style="display: none">修改</a> <a href="javascript:void(0)" class="easyui-linkbutton"
                        id="clearBtn" onclick="cleardata()">清除</a>
        </div>
        </form>
        <div id="addTips" align="center" style="color: #FF0000; display: none">
        </div>
    </div>
</body>
<script type="text/javascript"></script>
</html>
