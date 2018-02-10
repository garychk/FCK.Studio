<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    ODList
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        var _ordernumber = '<%=ViewData["OrderNumber"]%>';
        $(function () {
            LoadODatas();
        });
        function LoadODatas() {

            $("#ODataList").html('<center><img src="/Content/images/loading.gif" /></center>');
            $.ajax({
                type: "POST",
                url: "/Orders/GetOrderDetails",
                data: { Order_Number: _ordernumber },
                success: function (items) {
                    var htmls = '';
                    htmls += '<tr>';
                    htmls += '<td>订单号</td>';
                    htmls += '<td>名称</td>';
                    htmls += '<td>数量</td>';
                    htmls += '<td>价格</td>';
                    htmls += '</tr>';
                    for (var i = 0; i < items.length; i++) {
                        htmls += '<tr>';
                        htmls += '<td>' + items[i].Order_Number + '</td>';
                        htmls += '<td>' + items[i].Product_Name + '</td>';
                        htmls += '<td>' + items[i].Product_Number + '</td>';
                        htmls += '<td>' + items[i].Product_Price + '</td>';
                        htmls += '</tr>';
                    }
                    $("#ODataList").html(htmls);
                }
            });
            return false;
        }
    </script>
    <table class="table table-hover" id="ODataList"></table>
</asp:Content>
