﻿//1 短时间，形如 (13:04:06)
function isTime(str) {
    var a = str.match(/^(\d{1,2})(:)?(\d{1,2})\2(\d{1,2})$/);
    if (a == null) {
        return false;
    }
    if (a[1] > 24 || a[3] > 60 || a[4] > 60) {
        return false
    }
    return true;
}

//2. 短日期，形如 (2008-07-22)
function strDateTime(str) {
    var r = str.match(/^(\d{1,4})(-|\/)(\d{1,2})\2(\d{1,2})$/);
    if (r == null)
        return false;
    var d = new Date(r[1], r[3] - 1, r[4]);
    return (d.getFullYear() == r[1] && (d.getMonth() + 1) == r[3] && d.getDate() == r[4]);
}

//3 长时间，形如 (2008-07-22 13:04:06)

function strDateTime(str) {
    var reg = /^(\d{1,4})(-|\/)(\d{1,2})\2(\d{1,2}) (\d{1,2}):(\d{1,2}):(\d{1,2})$/;
    var r = str.match(reg);
    if (r == null)
        return false;
    var d = new Date(r[1], r[3] - 1, r[4], r[5], r[6], r[7]);
    return (d.getFullYear() == r[1] && (d.getMonth() + 1) == r[3] && d.getDate() == r[4] && d.getHours() == r[5] && d.getMinutes() == r[6] && d.getSeconds() == r[7]);
}

/**

判断时间大小

sdate1: 开始时间

edate2: 结束时间

return: true/false

**/
function checkDate(sdate1, edate2) {
    if (sdate1 != "" && edate2 != "") {//输入不为空时；
        // 对字符串进行处理
        // 以 - / 或 空格 为分隔符, 将日期字符串分割为数组
        var date1 = sdate1.split("-");
        var date2 = edate2.split("-");
        // 创建 Date 对象
        var myDate1 = new Date(date1[0], date1[1], date1[2]);
        var myDate2 = new Date(date2[0], date2[1], date2[2]);

        // 对日起进行比较
        if (myDate1 <= myDate2) {
            return true;
        } else {
            return false;
        }
    } else {
        return true;
    }
}

/**
判断日期格式 2000-01-01
strDate：检测的日期格式
return： true/false
**/
function isDate(strDate) {
    var strSeparator = "-";
    //日期分隔符
    var strDateArray;
    var intYear;
    var intMonth;
    var intDay;
    var boolLeapYear;
    //var strDate=form1.a.value   //表单中的日期值
    strDateArray = strDate.split(strSeparator);

    if (strDateArray.length != 3) {
        $.messager.alert('提示: 日期格式错误! \r\n 请依【YYYY-MM-DD】格式输入！');
        return false;
    }
    intYear = parseInt(strDateArray[0], 10);
    intMonth = parseInt(strDateArray[1], 10);
    intDay = parseInt(strDateArray[2], 10);

    if (isNaN(intYear) || isNaN(intMonth) || isNaN(intDay)) {
        $.messager.alert('提示: 日期格式错误! \r\n 请依【YYYY-MM-DD】格式输入！');
        return false;
    }

    if (intMonth > 12 || intMonth < 1) {
        $.messager.alert('提示: 日期格式错误! \r\n 请依【YYYY-MM-DD】格式输入！');
        return false;
    }

    if ((intMonth == 1 || intMonth == 3 || intMonth == 5 || intMonth == 7 || intMonth == 8 || intMonth == 10 || intMonth == 12) && (intDay > 31 || intDay < 1)) {
        $.messager.alert('提示: 日期格式错误! \r\n 请依【YYYY-MM-DD】格式输入！');
        return false;
    }

    if ((intMonth == 4 || intMonth == 6 || intMonth == 9 || intMonth == 11) && (intDay > 30 || intDay < 1)) {
        $.messager.alert('提示: 日期格式错误! \r\n 请依【YYYY-MM-DD】格式输入！');
        return false;
    }

    if (intMonth == 2) {
        if (intDay < 1) {
            $.messager.alert('提示: 日期格式错误! \r\n 请依【YYYY-MM-DD】格式输入！');
            return false;
        }
        boolLeapYear = false;
        if ((intYear % 4 == 0 && intYear % 100 != 0) || (intYear % 400 == 0)) {
            boolLeapYear = true;
        }

        if (boolLeapYear) {
            if (intDay > 29) {
                $.messager.alert('提示: 日期格式错误! \r\n 请依【YYYY-MM-DD】格式输入！');
                return false;
            }
        } else {
            if (intDay > 28) {
                $.messager.alert('提示: 日期格式错误! \r\n 请依【YYYY-MM-DD】格式输入！');
                return false;
            }
        }
    }

    return true;
}

function getGlobalVirtualDirectoryName() {
    var pathName = window.location.pathname;
    pathName = pathName.substr(1);
    pathName = pathName.substr(0, pathName.search("/"));

    return "/" + pathName;
}

function getFactoryUIPath() {
    return getGlobalVirtualDirectoryName() + "/PatternUI/BLLFactoryUI.ashx";
}

function getQueryPath() {
    return getGlobalVirtualDirectoryName() + "/PatternUI/BLLQuery.ashx";
}

//禁用右键、文本选择功能、复制按键

$(document).bind("contextmenu", function () {
    return false;
});

$(document).bind("selectstart", function () {
    return false;
});

$(document).keydown(function () {
    return key(arguments[0])
});
//按键时提示警告

function key(e) {
    var keynum;
    if (window.event) {
        keynum = e.keyCode;
    } // IE

    else if (e.which) {
        keynum = e.which;
    }
    // Netscape/Firefox/Opera
    //禁止复制内容
    if (keynum == 17) {
        return false;
    }
    //屏蔽退格删除键
    if (keynum == 116) {
        return false;
    }
    if (keynum == 82) {
        return false;
    }

    if ((window.event.altKey) && ((keynum == 37) || //屏蔽Alt+方向键←
	(keynum == 39))) {//屏蔽Alt+方向键→
        alert("不准你使用ALT+方向键前进或后退网页！");
        event.returnValue = false;
    }
    if ((event.keyCode == 8) || //屏蔽退格删除键
	(event.keyCode == 116) || //屏蔽F5刷新键
	(event.ctrlKey && event.keyCode == 82)) {//Ctrl+R
        event.keyCode = 0;
        event.returnValue = false;
    }
    if (event.keyCode == 122) {
        event.keyCode = 0;
        event.returnValue = false;
    } //屏蔽F11
    if (event.ctrlKey && event.keyCode == 78)
        event.returnValue = false;
    //屏蔽Ctrl+n
    if (event.shiftKey && event.keyCode == 121)
        event.returnValue = false;
    //屏蔽shift+F10
    if (window.event.srcElement.tagName == "A" && window.event.shiftKey)
        window.event.returnValue = false;
    //屏蔽shift加鼠标左键新开一网页
    if ((window.event.altKey) && (keynum == 115)) {//屏蔽Alt+F4
        window.showModelessDialog("about:blank", "", "dialogWidth:1px;dialogheight:1px");
        return false;
    }
}