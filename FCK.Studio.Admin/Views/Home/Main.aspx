<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Main
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="/Content/Highcharts/highcharts.js"></script>
    <script src="/Content/Highcharts/exporting.js"></script>
    <script>
        $(function () {
            GetFinanceChartData();
            GetArticleChartData();
            ChartLineBasic(0, 0, 'LineBasic', 'Monthly sales status', '营业额(￥)', "LineBasic");
            ChartAreabasic('AreaBasic');
            ChartBarBasic('BarBasic');
        });

        function GetArticleChartData() {
            $.ajax({
                type: "POST",
                url: "/Articles/GetArticleTj",
                success: function (result) {

                    $('#articleChart').highcharts({
                        chart: {
                            plotBackgroundColor: null,
                            plotBorderWidth: 0,
                            plotShadow: false
                        },
                        exporting: {
                            enabled: false
                        },
                        title: {
                            text: '文章访问量',
                            align: 'center',
                            verticalAlign: 'middle',
                            y: 50
                        },
                        tooltip: {
                            pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
                        },
                        plotOptions: {
                            pie: {
                                dataLabels: {
                                    enabled: true,
                                    distance: -50,
                                    style: {
                                        fontWeight: 'bold',
                                        color: 'white',
                                        textShadow: '0px 1px 2px black'
                                    }
                                },
                                startAngle: -90,
                                endAngle: 90,
                                center: ['50%', '75%']
                            }
                        },
                        series: [{
                            type: 'pie',
                            name: '访问占比',
                            innerSize: '50%',
                            data: result
                        }]
                    });
                }
            });
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
                success: function (result) {
                    for (var i = 0; i < result.length; i++) {
                        var obj = new FinanceData(result[i].name, result[i].data);
                        FinanceDataArr.push(obj);
                    }
                    timeDataArr = result[0].datetime;
                    CreateSpLine(timeDataArr, FinanceDataArr, '月度营业额', 'Monthly sales status', '营业额(￥)', "financeChart");
                }
            });
        }

        function CreateSpLine(xData, seriesData, title, subtitle, yTitle, renderTo) {
            $('#' + renderTo).highcharts({
                chart: {
                    type: 'spline'
                },
                exporting: {
                    enabled: false
                },
                title: {
                    text: title
                },
                subtitle: {
                    text: subtitle
                },
                xAxis: {
                    categories: xData
                },
                yAxis: {
                    title: {
                        text: yTitle
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

        function ChartLineBasic(xData, seriesData, title, subtitle, yTitle, renderTo) {
            $('#' + renderTo).highcharts({
                title: {
                    text: title,
                    x: -20 //center
                },
                exporting: {
                    enabled: false
                },
                subtitle: {
                    text: subtitle,
                    x: -20
                },
                xAxis: {
                    categories: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun',
                         'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec']
                },
                yAxis: {
                    title: {
                        text: yTitle
                    },
                    plotLines: [{
                        value: 0,
                        width: 1,
                        color: '#808080'
                    }]
                },
                tooltip: {
                    valueSuffix: '°C'
                },
                legend: {
                    layout: 'vertical',
                    align: 'right',
                    verticalAlign: 'middle',
                    borderWidth: 0
                },
                series: [{
                    name: 'Tokyo',
                    data: [7.0, 6.9, 9.5, 14.5, 18.2, 21.5, 25.2, 26.5, 23.3, 18.3, 13.9, 9.6]
                }, {
                    name: 'New York',
                    data: [-0.2, 0.8, 5.7, 11.3, 17.0, 22.0, 24.8, 24.1, 20.1, 14.1, 8.6, 2.5]
                }, {
                    name: 'Berlin',
                    data: [-0.9, 0.6, 3.5, 8.4, 13.5, 17.0, 18.6, 17.9, 14.3, 9.0, 3.9, 1.0]
                }, {
                    name: 'London',
                    data: [3.9, 4.2, 5.7, 8.5, 11.9, 15.2, 17.0, 16.6, 14.2, 10.3, 6.6, 4.8]
                }]
            });
        }

        function ChartAreabasic(renderTo) {
            $('#' + renderTo).highcharts({
                chart: {
                    type: 'area'
                },
                exporting: {
                    enabled: false
                },
                title: {
                    text: 'US and USSR nuclear stockpiles'
                },
                subtitle: {
                    text: 'Source: <a href="#">' +
                    'thebulletin.metapress.com</a>'
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
                    name: 'USA',
                    data: [null, null, null, null, null, 6, 11, 32, 110, 235, 369, 640,
                           1005, 1436, 2063, 3057, 4618, 6444, 9822, 15468, 20434, 24126,
                           27387, 29459, 31056, 31982, 32040, 31233, 29224, 27342, 26662,
                           26956, 27912, 28999, 28965, 27826, 25579, 25722, 24826, 24605,
                           24304, 23464, 23708, 24099, 24357, 24237, 24401, 24344, 23586,
                           22380, 21004, 17287, 14747, 13076, 12555, 12144, 11009, 10950,
                           10871, 10824, 10577, 10527, 10475, 10421, 10358, 10295, 10104]
                }, {
                    name: 'USSR/Russia',
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
                exporting: {
                    enabled: false
                },
                title: {
                    text: 'Historic World Population by Region'
                },
                subtitle: {
                    text: 'Source: Wikipedia.org'
                },
                xAxis: {
                    categories: ['Africa', 'America', 'Asia', 'Europe', 'Oceania'],
                    title: {
                        text: null
                    }
                },
                yAxis: {
                    min: 0,
                    title: {
                        text: 'Population (millions)',
                        align: 'high'
                    },
                    labels: {
                        overflow: 'justify'
                    }
                },
                tooltip: {
                    valueSuffix: ' millions'
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
                    name: 'Year 1800',
                    data: [107, 31, 635, 203, 2]
                }, {
                    name: 'Year 1900',
                    data: [133, 156, 947, 408, 6]
                }, {
                    name: 'Year 2008',
                    data: [973, 914, 4054, 732, 34]
                }]
            });
        }
    </script>
    <div class="container">
        <div class="row">
            <div class="col-xs-12 col-md-6">
                <div class="panel panel-default">
                    <div class="panel-body" id="BarBasic"></div>
                </div>
            </div>
            <div class="col-xs-12 col-md-6">
                <div class="panel panel-default">
                    <div class="panel-body" id="LineBasic"></div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-12 col-md-6">
                <div class="panel panel-default">
                    <div class="panel-body" id="AreaBasic"></div>
                </div>
            </div>
            <div class="col-xs-12 col-md-6">
                <div class="panel panel-default">
                    <div class="panel-body" id="articleChart"></div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-12 col-md-6">
                <div class="panel panel-default">
                    <div class="panel-body" id="financeChart"></div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
