<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Orders
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        $(function () {
            $('#stime').datetimepicker({
                lang: 'ch',
                format: 'Y/m/d',
                timepicker: false,
                allowTimes: false,
                yearStart: 2000,
                yearEnd: 2050
            });
            $('#etime').datetimepicker({
                lang: 'ch',
                format: 'Y/m/d',
                timepicker: false,
                allowTimes: false,
                yearStart: 2000,
                yearEnd: 2050
            });
            LoadDatas();
        });
        function LoadDatas() {
            var _stime = $("#stime").val();
            var _etime = $("#etime").val();
            var _ordernumber = $("#ordernumber").val();
            var _OrderIndex = $("#OrderIndex").val();
            var _page = parseInt($("#currentpage").val());
            $("#DataList").html('<center><img src="/Content/images/loading.gif" /></center>');
            $.ajax({
                type: "POST",
                url: "/Orders/GetOrders",
                data: {
                    page: _page,
                    pageSize: 10,
                    stime: _stime,
                    etime: _etime,
                    OrderNumber: _ordernumber,
                    OrderIndex: _OrderIndex
                },
                success: function (result) {
                    var items = result.datas;
                    var pages = result.pages;
                    var total = result.total;
                    var htmls = '';
                    htmls += '<tr>';
                    htmls += '<td><input type="checkbox" id="SelAll"> 全选</td>';
                    htmls += '<td>订单号</td>';
                    htmls += '<td>会员名</td>';
                    htmls += '<td>下单时间</td>';
                    htmls += '<td>金额</td>';
                    htmls += '<td>状态</td>';
                    htmls += '<td>操作</td>';
                    htmls += '</tr>';
                    for (var i = 0; i < items.length; i++) {
                        htmls += '<tr>';
                        htmls += '<td><input type="checkbox" name="order" value="' + items[i].Order_Number + '"></td>';
                        htmls += '<td><a href="javascript:void(0)" onclick=OpenDetail(\'/Orders/ODList/' + items[i].Order_Number + '\')>' + items[i].Order_Number + '</a></td>';
                        htmls += '<td>' + items[i].Member_Name + '</td>';
                        htmls += '<td>' + items[i].Order_Time + '</td>';
                        htmls += '<td class="colorRed">$ ' + items[i].Order_Amount + '</td>';
                        htmls += '<td>' + OrderStatus(items[i].Order_Status) + '</td>';
                        htmls += '<td>';
                        if (parseInt(items[i].Order_Status) < 4) {
                            htmls += '<button class="btn btn-info" onclick="UpdateOrderStatus(\'' + items[i].Order_Number + '\',' + (items[i].Order_Status + 1) + ')">' + cOperate(items[i].Order_Status) + '</button>';
                        }
                        htmls += ' <a href="javascript:void(0)" class="btn btn-info" onclick=OpenDetail(\'/Orders/ODList/' + items[i].Order_Number + '\')>详细</a>';
                        htmls += '</td>';
                        htmls += '</tr>';
                    }
                    $("#DataList").html(htmls);
                    $("#pagination").html(Pagination(_page, pages, total));
                }
            });
            return false;
        }

        function UpdateOrderStatus(ordernumber, _status) {
            if (_status == 3) {
                OpenWin('取货', "/Orders/GetService/" + ordernumber);
            }
            else {
                $.ajax({
                    type: "POST",
                    url: "/Orders/UpdateOrderStatus",
                    data: { OrderNumber: ordernumber, status: _status },
                    success: function (result) {
                        if (result.code == 100)
                            LoadDatas();
                        else
                            alertE(result.message);
                    }
                });
            }
            return false;
        }

        function DoStatus(_status) {
            confirmE("确定操作？", function () {
                var i = 0;
                $("#DataList input[name=order]").each(function () {
                    if ($(this).prop("checked")) {
                        i++;
                        var _ordernumber = $(this).val();
                        UpdateOrderStatus(_ordernumber, _status);
                    }
                });
                if (i == 0)
                    alertE("至少选择一项！");
            });
        }
        function OpenDetail(href) {
            OpenWin('订单详情', href, 'col-md-8 col-md-offset-2');
            return false;
        }
    </script>
    <ol class="breadcrumb">
        <li><a href="/Home/Main"><i class="iconfont icon-home"></i>控制台</a></li>
        <li class="active">订单管理</li>
    </ol>
    <div class="panel panel-default">
        <div class="container">
            <h3><i class="iconfont icon-orders"></i>订单管理</h3>
            <hr>
            <ul class="list-inline">
                <li>
                    <div class="btn-group" role="group" aria-label="...">
                        <a class="btn btn-primary" href="/Orders/Edit">添加</a>
                        <button type="button" class="btn btn-primary" onclick="DoStatus(1)">确认</button>
                        <button type="button" class="btn btn-primary" onclick="DoStatus(2)">支付</button>
                        <button type="button" class="btn btn-primary" onclick="DoStatus(3)">取货</button>
                        <button type="button" class="btn btn-primary" onclick="DoStatus(4)">完成</button>
                    </div>
                </li>
                <li>
                    <div class="form-inline">
                        <input type="text" class="form-control" placeholder="开始时间" id="stime">
                        <input type="text" class="form-control" placeholder="结束时间" id="etime">
                    </div>
                </li>
                <li>
                <li>
                    <form class="form-inline" role="form">
                        <input type="hidden" id="currentpage" value="1" />
                        <input type="hidden" id="OrderIndex" value="time_desc" />
                        <div class="input-group">
                            <input type="text" class="form-control" placeholder="快速搜索" id="ordernumber" value="<%=ViewData["OrderNumber"]%>">
                            <span class="input-group-btn">
                                <button type="button" class="btn btn-default" onclick="GoSearch()">GO</button>
                            </span>
                        </div>
                    </form>
                </li>
            </ul>
            <table class="table table-hover" id="DataList"></table>
            <nav class="rgt">
                <ul class="pagination" id="pagination"></ul>
            </nav>
        </div>
    </div>
</asp:Content>
