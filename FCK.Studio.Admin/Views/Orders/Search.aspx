<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Search
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        $(function () {
            document.onkeydown = function (e) {
                var ev = document.all ? window.event : e;
                if (ev.keyCode == 13) {
                    DoSearch();
                }
            }
            $("#keywords").focus(function () {
                var obj = $(this);
                var _top = $(this).offset().top;
                var _left = $(this).offset().left;

                var htmls = '<div class="NumberPicker" id="NumberPicker"><ul><li>1</li><li>2</li><li>3</li><li>4</li><li>5</li><li>6</li><li>7</li><li>8</li><li>9</li><li>0</li><li>.</li><li>C</li></ul><div class="clr"></div></div>';

                $("body").append(htmls);

                $("#NumberPicker").show();
                $("#NumberPicker").css({
                    "top": _top + 86,
                    "left": _left
                });

                $("#NumberPicker li").click(function () {
                    if ($(this).text() != "C") {
                        obj.val(obj.val() + "" + $(this).text());
                    } else {
                        obj.val("");
                    }
                });
            });
        });
        function DoSearch()
        {
            if ($("#keywords").val() != "") {
                window.location = "/Orders/Index/" + $("#keywords").val();
            } else {
                $("#keywords").focus();
            }

        }
    </script>
    <ol class="breadcrumb">
        <li><a href="/Home/Main"><i class="glyphicon glyphicon-home"></i>控制台</a></li>
        <li><a href="/Orders/">订单管理</a></li>
        <li class="active">订单查询</li>
    </ol>
    <div class="text-center ordersearch">
        <input type="text" class="form-control input-lg pleft" id="keywords" placeholder="请输入单号或客户姓名或手机号查询" /><button type="button" class="btn btn-success btn-lg pleft" onclick="DoSearch()">查询</button>
    </div>
    
</asp:Content>
