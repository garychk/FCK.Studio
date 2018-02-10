var imgServer = "http://img.my80.cn"
$(function () {
    $(".lazy").lazyload({ effect: "fadeIn" });
    $('[data-toggle="popover"]').popover();
    $('[data-toggle="tooltip"]').tooltip();

    var href = document.location.href.toLowerCase();
    $("#menus a").removeClass("selected");
    if (href.indexOf("home") >= 0 && href.indexOf("contact") < 0) {
        $("#home").addClass("selected");
    } else if (href.indexOf("article") >= 0) {
        $("#article").addClass("selected");
    } else if (href.indexOf("resources") >= 0) {
        $("#resources").addClass("selected");
    } else if (href.indexOf("contact") >= 0) {
        $("#contact").addClass("selected");
    } else  {
        $("#home").addClass("selected");
    }


    $(document).scroll(function () {
        var scrollTop1 = $(this).scrollTop();
        if (scrollTop1 > 0) {
            $(".fck-gotop").show();
        }
        else {
            $(".fck-gotop").hide();
        }
    });

    var a = new sHover("sHoverItem", "sIntro");
    a.set({
        slideSpeed: 5,
        opacityChange: true,
        opacity: 80
    });

    $("#pagination").on("click", " li a", function () {
        var page = parseInt($(this).attr("href"));
        if (page > 0) {
            $(this).parent().removeClass("active");
            $(this).parent().addClass("active");
            $("#page").val(page);
            GoSearch();
        }
        return false;
    });

});

function GoSearch()
{
    var action = $("#action").val();
    var renderid = $("#renderid").val();
    var options = {
        url: action,
        type: 'POST',
        success: function (response) {
            $("#" + renderid).html(response);

            var a = new sHover("sHoverItem", "sIntro");
            a.set({
                slideSpeed: 5,
                opacityChange: true,
                opacity: 80
            });

            $(".lazy").lazyload({ effect: "fadeIn" });

            $("#pagination").on("click", " li a", function () {
                var page = parseInt($(this).attr("href"));
                if (page > 0) {
                    $(this).parent().removeClass("active");
                    $(this).parent().addClass("active");
                    $("#page").val(page);
                    GoSearch();
                }
                return false;
            });
        }
    };
    $("#FormSearch").ajaxSubmit(options);
}

function goTop() {
    $('html,body').animate({ 'scrollTop': 0 }, 300);
}function alertE(msg) {
    $.alert({
        //icon: 'glyphicon glyphicon-info-sign',
        title: 'Hello',
        content: msg,
        confirmButton: 'ok',
        confirm: function () {

        }
    });
}

function confirmE(msg, okFun, cancelFun) {
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

function OpenWin(_title, url, classname) {
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
}function toast(_title, _icon, msg) {
    $.toast({
        heading: _title,
        text: msg,
        icon: _icon,
        loader: false,
        loaderBg: '#9EC600',
        position: 'bottom-right'
    })}function Logout() {
    confirmE('确定退出?', function () {
        $.post("/Base/UserLogout", {}, function (result) {
            window.location.href = "/Home/";
        });
    });
    }function ConverTime(str,format){
    var seconds = parseInt(str.replace(/\D/igm, ""));
    var times = new Date(seconds);
    return times.Format(format);
}Date.prototype.Format = function (fmt) {
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
}function OrderStatus(status) {
    switch (status) {
        case 0:
            return '<span class="label label-warning">未确认</span>';
        case 1:
            return '<span class="label label-danger">等待支付</span>';
        case 2:
            return '<span class="label label-success">支付完成</span>';
        case 3:
            return '<span class="label label-primary">已取货</span>';
        case 4:
            return '<span class="label label-info">完成</span>';
    }
}