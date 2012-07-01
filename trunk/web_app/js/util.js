
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
//    if (keynum == 116) {
//        return false;
//    }
//    if (keynum == 82) {
//        return false;
//    }

    if ((window.event.altKey) && ((keynum == 37) || //屏蔽Alt+方向键←
	(keynum == 39))) {//屏蔽Alt+方向键→
        alert("不准你使用ALT+方向键前进或后退网页！");
        event.returnValue = false;
    }
    if ((event.keyCode == 116) || //屏蔽F5刷新键
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