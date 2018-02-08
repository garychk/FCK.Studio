<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        $(function () {
            LoadDatas();
            $.post('/Category/GetCateTree', {}, function (response) {
                $('#CateTree').treeview({
                    levels: 1,
                    data: response,
                    onNodeSelected: function (event, node) {
                        $("#CateId").val(node.id);
                        LoadDatas();
                    },
                    onNodeUnselected: function (event, node) { }
                });
            });
            
        });
        function LoadDatas() {
            var _keywords = $("#keywords").val();
            var _cateid = $("#CateId").val();
            var _page = parseInt($("#currentpage").val());
            $("#DataList").html('<center><img src="/Content/images/loading.gif" /></center>');
            $.ajax({
                type: "POST",
                url: "/Articles/GetArticles",
                data: {
                    page: _page,
                    pageSize: 10,
                    cateid: _cateid,
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
                    htmls += '<td>标题</td>';
                    htmls += '<td>图片</td>';
                    htmls += '<td>分类</td>';
                    htmls += '<td>推荐</td>';
                    htmls += '<td>操作</td>';
                    htmls += '</tr>';
                    for (var i = 0; i < items.length; i++) {
                        htmls += '<tr>';
                        htmls += '<td><input type="checkbox" name="article" value="' + items[i].Article_ID + '"></td>';
                        htmls += '<td>' + items[i].Article_OrderID + '</td>';
                        htmls += '<td>' + items[i].Article_Title + '</td>';
                        htmls += '<td><img src="' + imageServer + (items[i].Article_Pic == null ? "/Content/images/blank.png" : items[i].Article_Pic) + '" height="30"></td>';
                        htmls += '<td>' + items[i].Article_CateName + '</td>';
                        htmls += '<td>' + (items[i].Article_IsRec == 1 ? "荐" : "") + '</td>';
                        htmls += '<td><a href="/Articles/Edit/' + items[i].Article_ID + '" class="btn btn-info">编辑</a><button type="button" class="btn btn-danger" onclick="Del(' + items[i].Article_ID + ')">删除</button></td>';
                        htmls += '</tr>';
                    }
                    $("#DataList").html(htmls);
                    $("#pagination").html(Pagination(_page, pages, total));
                }
            });
            return false;
        }
        function Del(idstr) {
            confirmE("确定删除？", function () {
                $.ajax({
                    type: "POST",
                    url: "/Articles/DelArticle",
                    data: { ids: idstr },
                    success: function (result) {
                        if (result.code == 100)
                            LoadDatas();
                        else
                            alertE(result.message);
                    }
                });
            });
        }
        function DelPacth() {
            confirmE("确定删除？", function () {
                var ids = "";
                $("#DataList input[name=article]").each(function () {
                    if ($(this).prop("checked")) {
                        if (ids == "")
                            ids += $(this).val();
                        else
                            ids += "," + $(this).val();
                    }
                });
                if (ids == "")
                    alertE("至少选择一项！");
                else
                    Del(ids);
            });
        }
        function Rec(idstr) {
            $.ajax({
                type: "POST",
                url: "/Articles/RecArticle",
                data: { ids: idstr },
                success: function (result) {
                    if (result.code == 100)
                        LoadDatas();
                    else
                        alertE(result.message);
                }
            });
        }
        function RecPacth() {
            var ids = "";
            $("#DataList input[name=article]").each(function () {
                if ($(this).prop("checked")) {
                    if (ids == "")
                        ids += $(this).val();
                    else
                        ids += "," + $(this).val();
                }
            });
            if (ids == "")
                alertE("至少选择一项！");
            else
                Rec(ids);
        }
    </script>

    <ol class="breadcrumb">
        <li><a href="/Home/Main"><i class="iconfont icon-home"></i>控制台</a></li>
        <li class="active">文库管理</li>
    </ol>
    <div class="panel panel-default">
        <div class="container">
            <h3><i class="iconfont icon-news"></i>文库管理</h3>
            <hr>
            <div class="row">
                <div class="col-xs-2">
                    <div id="CateTree" class=""></div>
                </div>
                <div class="col-xs-10">
                    <ul class="list-inline">
                        <li>
                            <div class="btn-group" role="group" aria-label="...">
                                <a class="btn btn-info" href="/Articles/Edit">添加</a>
                                <button type="button" class="btn btn-success" onclick="RecPacth()">推荐</button>
                                <button type="button" class="btn btn-danger" onclick="DelPacth()">删除</button>
                            </div>
                        </li>
                        <li>
                            <form class="form-inline" role="form">
                                <input type="hidden" id="currentpage" value="1" />
                                <input type="hidden" id="CateId" value="<%=ViewData["CateId"] %>" />
                                <div class="input-group">
                                    <input type="text" class="form-control" placeholder="快速搜索" id="keywords">
                                    <span class="input-group-btn">
                                        <button type="button" class="btn btn-default" onclick="GoSearch()">搜索</button>
                                    </span>
                                </div>
                            </form>
                        </li>
                    </ul>
                    <table class="table table-hover" id="DataList">
                    </table>
                    <nav class="rgt">
                        <ul class="pagination" id="pagination"></ul>
                    </nav>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
