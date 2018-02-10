$(function () {
    if ($("#AreaBasic").length > 0) {
        ChartAreabasic("AreaBasic");
    }
    if ($("#BarBasic").length > 0) {
        ChartBarBasic("BarBasic");
    }
});
function ChartAreabasic(renderTo) {
    $('#' + renderTo).highcharts({
        chart: {
            type: 'area'
        },
        title: {
            text: '广告展现效果汇总'
        },
        subtitle: {
            text: 'Source: oplug.cn'
        },
        xAxis: {
            allowDecimals: false,
            labels: {
                formatter: function () {
                    return this.value; // clean, unformatted number for year
                }
            }
        },
        yAxis: {
            title: {
                text: 'Nuclear weapon states'
            },
            labels: {
                formatter: function () {
                    return this.value / 1000 + 'k';
                }
            }
        },
        tooltip: {
            pointFormat: '{series.name} produced <b>{point.y:,.0f}</b><br/>warheads in {point.x}'
        },
        plotOptions: {
            area: {
                pointStart: 1940,
                marker: {
                    enabled: false,
                    symbol: 'circle',
                    radius: 2,
                    states: {
                        hover: {
                            enabled: true
                        }
                    }
                }
            }
        },
        series: [{
            name: '展现量',
            data: [null, null, null, null, null, 6, 11, 32, 110, 235, 369, 640,
                   1005, 1436, 2063, 3057, 4618, 6444, 9822, 15468, 20434, 24126,
                   27387, 29459, 31056, 31982, 32040, 31233, 29224, 27342, 26662,
                   26956, 27912, 28999, 28965, 27826, 25579, 25722, 24826, 24605,
                   24304, 23464, 23708, 24099, 24357, 24237, 24401, 24344, 23586,
                   22380, 21004, 17287, 14747, 13076, 12555, 12144, 11009, 10950,
                   10871, 10824, 10577, 10527, 10475, 10421, 10358, 10295, 10104]
        }, {
            name: '点击量',
            data: [null, null, null, null, null, null, null, null, null, null,
                   5, 25, 50, 120, 150, 200, 426, 660, 869, 1060, 1605, 2471, 3322,
                   4238, 5221, 6129, 7089, 8339, 9399, 10538, 11643, 13092, 14478,
                   15915, 17385, 19055, 21205, 23044, 25393, 27935, 30062, 32049,
                   33952, 35804, 37431, 39197, 45000, 43000, 41000, 39000, 37000,
                   35000, 33000, 31000, 29000, 27000, 25000, 24000, 23000, 22000,
                   21000, 20000, 19000, 18000, 18000, 17000, 16000]
        }]
    });
}
function ChartBarBasic(renderTo) {
    $('#' + renderTo).highcharts({
        chart: {
            type: 'bar'
        },
        title: {
            text: '各区域销售季度汇总'
        },
        subtitle: {
            text: 'Source: www.oplug.cn'
        },
        xAxis: {
            categories: ['南通', '苏州', '无锡', '浙江', '安徽'],
            title: {
                text: null
            }
        },
        yAxis: {
            min: 0,
            title: {
                text: '单位 (￥)',
                align: 'high'
            },
            labels: {
                overflow: 'justify'
            }
        },
        tooltip: {
            valueSuffix: ' ￥'
        },
        plotOptions: {
            bar: {
                dataLabels: {
                    enabled: true
                }
            }
        },
        legend: {
            layout: 'vertical',
            align: 'right',
            verticalAlign: 'top',
            x: -40,
            y: 100,
            floating: true,
            borderWidth: 1,
            backgroundColor: ((Highcharts.theme && Highcharts.theme.legendBackgroundColor) || '#FFFFFF'),
            shadow: true
        },
        credits: {
            enabled: false
        },
        series: [{
            name: '一月',
            data: [107, 31, 635, 203, 2]
        }, {
            name: '二月',
            data: [133, 156, 947, 408, 6]
        }, {
            name: '三月',
            data: [973, 914, 4054, 732, 34]
        }]
    });
}

function alertE(msg) {
    $.alert({
        //icon: 'glyphicon glyphicon-info-sign',
        title: '',
        content: msg,
        backgroundDismiss: true,
        confirmButton: 'ok',
        confirmButtonClass: 'btn-success',
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
        backgroundDismiss: true,
        closeIcon: true
    });
}

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


var Member = function (node, form, url, callback) {
    this.node = node;
    this.form = form;
    this.url = url;
    Member.prototype.Login = function () {
        var $btn = $("#" + node).button('loading');
        var options = {
            url: '/Member/Login',
            type: 'post',
            success: function (result) {
                if (result.code == 100)
                    window.location = url;
                else
                    alertE(result.message);
                $btn.button('reset');
            }
        };
        $("#" + form).ajaxSubmit(options);
        return false;
    }

    Member.prototype.Regist = function () {

    }
}

var Events = function () {    
    Events.prototype.GetPageList = function (form) {        
        this.datas = asyncPost(form, '/Events/GetPageList');
    }

    Events.prototype.CreateOrUpdate = function (form) {
        this.datas = asyncPost(form, '/Events/CreateOrUpdate');
    }
}

var Register = function () {   
    Register.prototype.GetModelByNumber = function (form) {
        var result = asyncPost(form, '/Register/GetModelByNumber');
        this.datas = result.datas;
    }
}

function asyncPost(form, action)
{
    var datas = null;
    var options = {
        url: action,
        type: 'POST',
        async: false,
        success: function (result) {
            if (result.code == 100)
                datas = result;
            else
                alertE(result.message);
        }
    };
    $("#" + form).ajaxSubmit(options);
    return datas;
}
