<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    <title><%=ViewData["PageTitle"] %></title>
    <script src="/Content/js/jquery-2.1.1.min.js"></script>
    <script src="/Content/bootstrap/js/bootstrap.min.js"></script>
    <script src="/Content/js/jquery.sticky.min.js"></script>
    <script src="/Content/js/jquery-confirm.js"></script>
    <script src="/Content/js/Common.js"></script>
    <link href="/Content/js/jquery-confirm.css" rel="stylesheet" />
    <link href="/Content/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="//at.alicdn.com/t/font_nbr0c2yzidv1v2t9.css" rel="stylesheet" />
    <link href="/Content/Style.css" rel="stylesheet" />
    <script>
        $(function () {
            window.onresize = resize;
            resize();

            $('#Logout').click(function () {
                confirmE('确定退出?', DoLogout);
            });
            GetLogs();
            $("#LogsList").on("click", "a", function () {
                var logid = $(this).attr("data-v");
                OpenWin("系统消息", "/Home/LogDetail/" + logid, "col-md-6 col-md-offset-3");
            });

            $("#mleft a").click(function () {
                $("#mleft a").removeClass("select");
                $(this).addClass("select");
            });
        });

        function resize() {
            var pageWidth = $(window).width();
            var pageHeight = $(window).height();
            var headerHeight = $("#header").height();
            var footerHeight = $("#footer").height();
            var RtHeight = pageHeight - headerHeight - footerHeight - 25;
            $("#mbody,#mright").height(RtHeight);
            $("#mainframe").height(RtHeight);
        }

        function DoLogout() {
            $.ajax({
                type: "POST",
                url: "/Admin/DoLogout",
                success: function (result) {
                    if (result.code == 100) {
                        window.location = "/Home/";
                    }
                    else {
                        eModal.alert(result.message, "系统信息");
                    }
                }
            });
            return false;
        }

        function GetLogs() {
            var htmls = '';
            $("#LogsList").html('<center><img src="/Content/images/loading.gif" /></center>');
            $.ajax({
                type: "POST",
                url: "/Home/GetLogs",
                data: {
                    page: 1,
                    pageSize: 5,
                },
                success: function (result) {
                    var items = result.datas;
                    var total = result.total;
                    if (items.length > 0) {
                        for (var i = 0; i < items.length; i++) {
                            htmls += '<li><a data-v="' + items[i].Log_ID + '"><span class="colorBlue"><i class="glyphicon glyphicon-comment"></i></span>' + items[i].Log_Content.substr(0, 10) + ' <small class="ML20">' + items[i].Log_Time + '</small></a></li>';
                        }
                    } else {
                        htmls = '<li>&nbsp;&nbsp;&nbsp;&nbsp;<span class="colorBlue"><i class="iconfont icon-announce"></i></span>暂无信息</li>';
                    }
                    $("#LogsList").html(htmls);
                }
            });
            return false;
        }
    </script>
</head>
<body style="overflow: hidden">
    <nav class="navbar navbar-wrapper " id="header">
        <div class="container-fluid">
            <div class="navbar-header">
                <a class="navbar-brand" href="/Home/Dashboard"><i class="iconfont icon-guanli"></i><%=ViewData["PageTitle"].ToString().ToUpper() %></a>
            </div>

            <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
                <ul class="nav navbar-nav">
                </ul>

                <ul class="nav navbar-nav navbar-right">
                    <%--<li>
                        <form class="navbar-form navbar-left" role="search">
                            <div class="form-group">
                                <input type="text" class="form-control" placeholder="快速搜索">
                            </div>
                            <button type="submit" class="btn btn-default">GO</button>
                        </form>
                    </li>--%>
                    <li><a href="/Home/Main" class="dropdown-toggle" target="mainframe"><i class="iconfont icon-home"></i>控制台</a></li>
                    <li class="dropdown">
                        <a href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false"><i class="iconfont icon-email"></i><span class="caret"></span></a>
                        <ul class="dropdown-menu" id="LogsList">
                        </ul>
                    </li>
                    <li class="dropdown">
                        <a href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false"><i class="iconfont icon-member"></i><%=ViewData["AdminName"] %><span class="caret"></span></a>
                        <ul class="dropdown-menu">
                            <%if (FCK.Common.Utility.cInt(FCK.Common.Utility.GetSessionValue("RegisterId")) == 0)
                                {%>
                            <li><a href="/System/" target="mainframe"><i class="iconfont icon-setting"></i>系统设置</a></li>                            
                            <%}else{ %>
                            <li><a href="/Registers/Setting/<%=FCK.Common.Utility.GetSessionValue("RegisterId") %>" target="mainframe"><i class="iconfont icon-render"></i>租户设置</a></li>
                            <%} %>
                            <li role="separator" class="divider"></li>
                            <li><a href="#" id="Logout"><i class="iconfont icon-exist"></i>退出</a></li>
                        </ul>
                    </li>
                </ul>
            </div>
        </div>
    </nav>
    <div class="container">
        <div class="row">
            <div class="col-xs-12 col-md-2" id="mleft">
                <div class="panel panel-default">
                    <div class="list-group">
                        <a href="/Home/Main" target="mainframe" class="list-group-item select"><i class="iconfont icon-home"></i>控制台</a>
                        <a href="/Category/" target="mainframe" class="list-group-item"><i class="iconfont icon-tree"></i>分类管理</a>
                        <a href="/Articles/" target="mainframe" class="list-group-item"><i class="iconfont icon-news"></i>文库管理</a>
                        <a href="/Products/" target="mainframe" class="list-group-item"><i class="iconfont icon-product"></i>产品管理</a>
                        <a href="/Orders/" target="mainframe" class="list-group-item"><i class="iconfont icon-orders"></i>订单管理</a>
                        <a href="/Members/" target="mainframe" class="list-group-item"><i class="iconfont icon-users"></i>会员管理</a>
                        <a href="/Customers/" target="mainframe" class="list-group-item"><i class="iconfont icon-customer"></i>客户管理</a>
                        <a href="/Finance/" target="mainframe" class="list-group-item"><i class="iconfont icon-finance"></i>财务管理</a>
                        <%if (FCK.Common.Utility.cInt(FCK.Common.Utility.GetSessionValue("RegisterId")) == 0)
                            {%>
                        <a href="/System/" target="mainframe" class="list-group-item"><i class="iconfont icon-setting"></i>系统设置</a>
                        <a href="/FckAPI/" target="mainframe" class="list-group-item"><i class="iconfont icon-api"></i>API管理</a>
                        <a href="/Registers/" target="mainframe" class="list-group-item"><i class="iconfont icon-render"></i>租户管理</a>
                        <a href="/Admin/" target="mainframe" class="list-group-item"><i class="iconfont icon-admins"></i>管理员管理</a>
                        <%} %>
                    </div>
                </div>
            </div>
            <div class="col-xs-12 col-md-10" id="mright">
                <div class="mainframe">
                    <iframe name="mainframe" id="mainframe" src="/Home/Main" frameborder="0" scrolling="auto" width="100%" height="100%"></iframe>
                </div>
            </div>
        </div>
    </div>
</body>
</html>
