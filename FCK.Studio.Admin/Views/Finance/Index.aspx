<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="/Content/Highcharts/highcharts.js"></script>
    <script src="/Content/Highcharts/exporting.js"></script>
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
            GetFinanceChartData();

        });

        function FreshData() {
            GoSearch();
            GetFinanceChartData();
        }

        function GetFinanceChartData() {
            var FinanceDataArr = new Array();
            var timeDataArr = new Array();

            function FinanceData(name, data) {
                this.name = name;
                this.data = data;
            }
            var _stime = $("#stime").val();
            var _etime = $("#etime").val();
            $.ajax({
                type: "POST",
                url: "/Finance/GetFinanceChartData",
                data: {
                    stime: _stime,
                    etime: _etime
                },
                success: function (result) {
                    for (var i = 0; i < result.length; i++) {
                        var obj = new FinanceData(result[i].name, result[i].data);
                        FinanceDataArr.push(obj);
                    }
                    timeDataArr = result[0].datetime;
                    CreateChart(timeDataArr, FinanceDataArr);
                }
            });
        }

        function LoadDatas() {
            var _stime = $("#stime").val();
            var _etime = $("#etime").val();
            var _member = $("#member").val();
            var _page = parseInt($("#currentpage").val());
            $("#DataList").html('<center><img src="/Content/images/loading.gif" /></center>');
            $.ajax({
                type: "POST",
                url: "/Finance/GetFinances",
                data: {
                    page: _page,
                    pageSize: 5,
                    stime: _stime,
                    etime: _etime,
                    membername: _member
                },
                success: function (result) {
                    var items = result.datas;
                    var pages = result.pages;
                    var total = result.total;
                    var htmls = '';
                    htmls += '<tr>';                    
                    htmls += '<td>类型</td>';
                    htmls += '<td>时间</td>';
                    htmls += '<td>金额</td>';
                    htmls += '<td>说明</td>';
                    htmls += '</tr>';
                    for (var i = 0; i < items.length; i++) {
                        htmls += '<tr>';                        
                        htmls += '<td>' + cFtype(items[i].Finance_Type) + '</td>';
                        htmls += '<td>' + items[i].Finance_Time + '</td>';
                        htmls += '<td>' + items[i].Finance_Amount + '</td>';
                        htmls += '<td>' + items[i].Finance_Memo + '</td>';
                        htmls += '</tr>';
                    }
                    $("#DataList").html(htmls);
                    $("#pagination").html(Pagination(_page, pages, total));
                }
            });
            return false;
        }

        function CreateChart(xData, seriesData) {
            $('#financeChart').highcharts({
                chart: {
                    type: 'spline'
                },
                exporting: {
                    enabled: false
                },
                title: {
                    text: '月度营业状况'
                },
                subtitle: {
                    text: 'Monthly sales status'
                },
                xAxis: {
                    categories: xData
                },
                yAxis: {
                    title: {
                        text: '营业额(￥)'
                    },
                    labels: {
                        formatter: function () {
                            return this.value
                        }
                    }
                },
                tooltip: {
                    crosshairs: true,
                    shared: true
                },
                plotOptions: {
                    spline: {
                        marker: {
                            radius: 4,
                            lineColor: '#666666',
                            lineWidth: 1
                        }
                    }
                },
                series: seriesData
            });
        }
    </script>
    <ol class="breadcrumb">
        <li><a href="/Home/Main"><i class="iconfont icon-home"></i>控制台</a></li>
        <li class="active">财务管理</li>
    </ol>
    <div class="panel panel-default">
        <div class="container">
            <h3><i class="iconfont icon-finance"></i>财务管理</h3>
            <hr>
            <div class="pull-right">
            </div>
            <ul class="list-inline">
                <li>
                    <div class="form-inline">
                        <input type="text" class="form-control" placeholder="开始时间" id="stime">
                        <input type="text" class="form-control" placeholder="结束时间" id="etime">
                    </div>
                </li>
                <li>
                    <form class="form-inline" role="form">
                        <input type="hidden" id="currentpage" value="1" />
                        <div class="input-group">
                            <input type="text" class="form-control" placeholder="输入会员名搜索" id="member">
                            <span class="input-group-btn">
                                <button type="button" class="btn btn-success" onclick="FreshData()">GO</button>
                            </span>
                        </div>
                    </form>
                </li>
            </ul>

            <div class="col-md-11" id="financeChart"></div>
        </div>
    </div>
    <div class="panel panel-default">
        <div class="P10">
            <table class="table table-hover" id="DataList"></table>
            <nav class="rgt">
                <ul class="pagination" id="pagination"></ul>
            </nav>
        </div>
    </div>
</asp:Content>
