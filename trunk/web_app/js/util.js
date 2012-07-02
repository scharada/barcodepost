
function getGlobalVirtualDirectoryName() {
    var pathName = window.location.pathname;
    pathName = pathName.substr(1);
    pathName = pathName.substr(0, pathName.search("/"));

    return "/" + pathName;
}

function getFactoryUIPath() {
    return getGlobalVirtualDirectoryName() + "/PatternUI/BLLFactoryUI.ashx";
}

function getLoginPath() {
    return getGlobalVirtualDirectoryName() + "/PatternUI/Login.ashx";
}


//禁用右键、文本选择功能、复制按键

$(document).bind("contextmenu", function () {
    return false;
});

/**
* @author yukaizhao
*/
$.extend($.fn.validatebox.defaults.rules, {
    /*必须和某个字段相等*/
    equalTo: {
        validator: function (value, param) {
            return $(param[0]).val() == value;
        },
        message: '字段不匹配'
    }

});