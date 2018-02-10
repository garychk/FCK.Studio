var imageServer = "http://img.oplug.cn";
$(function () {
    $("#DataList").on("click", "#SelAll", function () {
        if ($(this).prop("checked")) {
            $("#DataList input[type=checkbox]").prop("checked", true);
        } else {
            $("#DataList input[type=checkbox]").prop("checked", false);
        }
    });

    $("#pagination").on("click", " li a", function () {
        var page = parseInt($(this).attr("href"));
        if (page > 0) {
            $(this).parent().removeClass("active");
            $(this).parent().addClass("active");
            $("#currentpage").val(page);
            LoadDatas();
        }
        return false;
    });
});

function alertE(msg) {
    $.alert({
        //icon: 'glyphicon glyphicon-info-sign',
        title: 'Hello',
        content: msg,
        confirmButton: 'ok',
        confirm: function () {
            
        }
    });
}

function confirmE(msg, okFun, cancelFun)
{
    $.confirm({
        //icon: 'glyphicon glyphicon-info-sign',
        title: 'Hello',
        content: msg,
        //theme: 'bootstrap', // 'material', 'bootstrap'
        confirmButtonClass: 'btn-info',
        cancelButtonClass: 'btn-danger',
        confirmButton: 'yes',
        cancelButton: 'no',        
        confirm: okFun,
        cancel: cancelFun
    });
}

function OpenWin(_title, url, classname)
{
    classname = classname == "" ? 'col-md-4 col-md-offset-4' : classname;
    $.confirm({
        content: 'url:' + url,
        title: _title,
        cancelButton: false,
        confirmButton: false,
        columnClass: classname,
        keyboardEnabled: true,
        closeIcon: true
    });
}

function checkMobile(str) {
    var reg = /^1\d{10}$/;
    return reg.test(str);
}

function checkEmail(str) {
    var reg = /^(\w-*\.*)+@(\w-?)+(\.\w{2,})+$/;
    return reg.test(str);
}

function checkPhone(str) {
    var reg = /^0\d{2,3}-?\d{7,8}$/;
    return reg.test(str);
}

function checkIDNum(str) {
    var reg = /^(\d{15}$|^\d{18}$|^\d{17}(\d|X|x))$/;
    return reg.test(str);
}

function Pagination(currentPage, pages, total) {
    var html2 = "";
    if (pages > 1) {
        if (pages <= 10) {
            for (var j = 1; j <= pages; j++) {
                if (j == currentPage)
                    html2 += '<li class="active"><a href="0">' + j + '</a></li>';
                else
                    html2 += '<li><a href="' + j + '">' + j + '</a></li>';
            }
        }
        else {
            var prePage = parseInt(currentPage) - 1; prePage = prePage < 1 ? 1 : prePage;
            var nextPage = parseInt(currentPage) + 1; nextPage = nextPage > pages ? pages : nextPage;

            var startPage = parseInt(currentPage / 5) * 5; startPage = startPage <= 0 ? currentPage : startPage;
            var endPage = parseInt(startPage) + 5; endPage = endPage > pages ? pages : endPage;


            if (currentPage > 1) {
                html2 += '<li><a href="' + prePage + '">&laquo;</a></li>';
                html2 += '<li><a href="1">1</a></li>';
            }
            if (currentPage - 1 >= 2)
                html2 += '<li><a href="0">...</a></li>';
            for (var j = startPage; j <= endPage; j++) {
                if (j == currentPage)
                    html2 += '<li class="active"><a href="0">' + j + '</a></li>';
                else
                    html2 += '<li><a href="' + j + '">' + j + '</a></li>';
            }
            if (currentPage != pages && endPage != pages) {
                html2 += '<li><a href="0">...</a></li>';
                html2 += '<li><a href="' + pages + '">' + pages + '</a></li>';
                html2 += '<li><a href="' + nextPage + '">&raquo;</a></li>';
            }

        }
    }
    //html2 += '<li><a href="0">共' + total + '条数据</a></li>';
    return html2;
}

function GoSearch()
{
    $("#currentpage").val(1);
    LoadDatas();
}

function cStatus(status)
{
    switch (status)
    {
        case 0:
            return '<span class="colorGreen">正常</span>';
        case 1:
            return '<span class="colorRed">锁定</span>';
    }
}
function cFtype(_type)
{
    switch (_type) {
        case "comein":
            return '<span class="colorGreen">收入</span>';
        case "payout":
            return '<span class="colorRed">支出</span>';
        case "refund":
            return '退款';
        case "cash":
            return '体现';
        default:
            return _type;
    }
}
function OrderStatus(status) {
    switch (status) {
        case 0:
            return '<span class="colorBlue">未确认</span>';
        case 1:
            return '<span class="colorRed">等待支付</span>';
        case 2:
            return '<span class="colorGreen">支付完成</span>';
        case 3:
            return '<span class="colorBlue">已取货</span>';
        case 4:
            return '<span class="colorGreen">完成</span>';
    }
}
function cOperate(status) {
    switch (status) {
        case 0:
            return '确认';
        case 1:
            return '支付';
        case 2:
            return '取货';
        case 3:
            return '完成';
    }
}
/* 
*  方法:Array.remove(dx) 通过遍历,重构数组 
*  功能:删除数组元素. 
*  参数:dx删除元素的下标. 
*/
Array.prototype.remove = function (dx) {
    if (isNaN(dx) || dx > this.length) { return false; }
    for (var i = 0, n = 0; i < this.length; i++) {
        if (this[i] != this[dx]) {
            this[n++] = this[i]
        }
    }
    this.length -= 1
}

function GoBack()
{
    window.history.go(-1);
}
function delHtmlTag(str) {
    return str.replace(/<[^>]+>/g, "");    
}
function filterCon(str)
{
    str = str.replace(/<br\s*\/?>/g, "\n");
    return str.replace(/<(?!\/?BR|\/?IMG)[^<>]*>/ig, '')
}

function TimeFormat(ts, formatstring)
{
    var seconds = parseInt(ts.replace(/\D/igm, ""));
    var d = new Date(seconds);
    return d.Format(formatstring);
}

var editorItems = [
		'source', '|', 'undo', 'redo', '|', 'preview', 'print', 'template', 'code', 'cut', 'copy', 'paste',
		'plainpaste', 'wordpaste', '|', 'justifyleft', 'justifycenter', 'justifyright',
		'justifyfull', 'insertorderedlist', 'insertunorderedlist', 'indent', 'outdent', 'subscript',
		'superscript', 'clearhtml', 'quickformat', 'selectall', '|', 'fullscreen', '/',
		'formatblock', 'fontname', 'fontsize', '|', 'forecolor', 'hilitecolor', 'bold',
		'italic', 'underline', 'strikethrough', 'lineheight', 'removeformat', '|', 'image', 'multiimage',
		'flash', 'media', 'insertfile', 'table', 'hr', 'emoticons', 'baidumap', 'pagebreak',
		'anchor', 'link', 'unlink', 'code', '|', 'about'
]

Date.prototype.Format = function (fmt) {
    var o = {
        "M+": this.getMonth() + 1,                 //月份   
        "d+": this.getDate(),                    //日   
        "h+": this.getHours(),                   //小时   
        "m+": this.getMinutes(),                 //分   
        "s+": this.getSeconds(),                 //秒   
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度   
        "S": this.getMilliseconds()             //毫秒   
    };
    if (/(y+)/.test(fmt))
        fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
        if (new RegExp("(" + k + ")").test(fmt))
            fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
    return fmt;
}