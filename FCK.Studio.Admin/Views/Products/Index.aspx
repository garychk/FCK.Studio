<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        var imgServer = "<%=FCK.Common.Utility.ImgServer%>";
        $(function () {
            LoadDatas();
        });
        function LoadDatas() {
            var _keywords = $("#keywords").val();
            var _page = parseInt($("#currentpage").val());
            $("#DataList").html('<center><img src="/Content/images/loading.gif" /></center>');
            $.ajax({
                type: "POST",
                url: "/Products/GetProducts",
                data: {
                    page: _page,
                    pageSize: 10,
                    keywords: _keywords
                },
                success: function (result) {
                    var items = result.datas;
                    var pages = result.pages;
                    var total = result.total;
                    var htmls = '';
                    htmls += '<tr>';
                    htmls += '<td><input type="checkbox" id="SelAll"> 全选</td>';
                    htmls += '<td>序号</td>';
                    htmls += '<td>品名</td>';
                    htmls += '<td>图片</td>';
                    htmls += '<td>价格</td>';
                    htmls += '<td>推荐</td>';
                    htmls += '<td>操作</td>';
                    htmls += '</tr>';
                    for (var i = 0; i < items.length; i++) {
                        htmls += '<tr>';
                        htmls += '<td><input type="checkbox" name="product" value="' + items[i].Product_ID + '"></td>';
                        htmls += '<td>' + items[i].Product_Order + '</td>';
                        htmls += '<td>' + items[i].Product_Name + '</td>';
                        if (items[i].Product_Pic == null)
                            htmls += '<td></td>';
                        else
                            htmls += '<td><img src="' + imgServer + items[i].Product_Pic + '" height="50"></td>';
                        htmls += '<td>' + items[i].Product_Price + '</td>';
                        htmls += '<td>' + (items[i].IsRec == 1 ? "荐" : "") + '</td>';
                        htmls += '<td>';
                        htmls += '<a href="/Products/Edit/' + items[i].Product_ID + '" class="btn btn-info">编辑</a>';
                        htmls += '<button type="button" class="btn btn-danger" onclick="Del(' + items[i].Product_ID + ')">删除</button>';
                        htmls += '<a href="/Products/Extra/' + items[i].Product_ID + '" class="btn btn-primary">属性</a>';
                        htmls += '</td>';
                        htmls += '</tr>';
                    }
                    $("#DataList").html(htmls);
                    $("#pagination").html(Pagination(_page, pages, total));
                }
            });
            return false;
        }

        function DelPacth() {
            confirmE("确定删除？", function () {
                var i = 0;
                $("#DataList input[name=product]").each(function () {
                    if ($(this).prop("checked")) {
                        i++;
                        var _prdid = $(this).val();
                        $.ajax({
                            type: "POST",
                            url: "/Products/DelProduct",
                            data: { ID: _prdid },
                            success: function (result) {
                                if (result.code == 100)
                                    LoadDatas();
                            }
                        });
                    }
                });
                if (i == 0)
                    alertE("至少选择一项！");
            });
        }

        function Del(_ID) {
            confirmE("确定删除？", function () {
                $.ajax({
                    type: "POST",
                    url: "/Products/DelProduct",
                    data: { ID: _ID },
                    success: function (result) {
                        if (result.code == 100)
                            LoadDatas();
                        else
                            alertE(result.message);
                    }
                });
            });
        }

        function RecPacth() {
            var i = 0;
            var ids = "";
            $("#DataList input[name=product]").each(function () {
                if ($(this).prop("checked")) {
                    i++;
                    if (ids != "")
                        ids += "," + $(this).val();
                    else
                        ids += $(this).val();
                }
            });
            if (i == 0)
                alertE("至少选择一项！");
            else {
                $.ajax({
                    type: "POST",
                    url: "/Products/RecProduct",
                    data: { ID: ids },
                    success: function (result) {
                        if (result.code == 100)
                            LoadDatas();
                    }
                });
            }
        }
    </script>

    <ol class="breadcrumb">
        <li><a href="/Home/Main"><i class="iconfont icon-home"></i>控制台</a></li>
        <li class="active">产品管理</li>
    </ol>
    <div class="panel panel-default">
        <div class="container">
            <h3><i class="iconfont icon-product"></i>产品管理</h3>
            <hr>
            <ul class="list-inline">
                <li>
                    <div class="btn-group" role="group" aria-label="...">
                        <a class="btn btn-info" href="/Products/Edit">添加</a>
                        <button type="button" class="btn btn-success" onclick="RecPacth()">推荐</button>
                        <button type="button" class="btn btn-danger" onclick="DelPacth()">删除</button>
                    </div>
                </li>
                <li>
                    <form class="form-inline" role="form">
                        <input type="hidden" id="currentpage" value="1" />
                        <div class="input-group">
                            <input type="text" class="form-control" placeholder="快速搜索" id="keywords">
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
