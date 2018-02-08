<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        $(function () {
            LoadDatas('');
        });
        function LoadDatas(_ctype) {
            $("#DataList").html('<center><img src="/Content/images/loading.gif" /></center>');
            $.ajax({
                type: "POST",
                url: "/Category/GetCates",
                data: {
                    parentid: $("#ParentID").val(),
                    ctype: _ctype
                },
                success: function (items) {
                    var htmls = '';
                    htmls += '<tr>';
                    htmls += '<td><input type="checkbox" id="chooseall" /></td>';
                    htmls += '<td>序号</td>';
                    htmls += '<td>分类名</td>';
                    htmls += '<td>版块</td>';
                    htmls += '<td>操作</td>';
                    htmls += '</tr>';
                    for (var i = 0; i < items.length; i++) {
                        htmls += '<tr>';
                        htmls += '<td><input type="checkbox" name="category" value="' + items[i].Category_ID + '"></td>';
                        htmls += '<td>' + items[i].Category_OrderID + '</td>';
                        if (items[i].Children_Num > 0) {
                            htmls += '<td><a href="/Category/Index/' + items[i].Category_ID + '">' + items[i].Category_Name + '(' + items[i].Children_Num + ')</a></td>';
                        } else {
                            htmls += '<td>' + items[i].Category_Name + '(' + items[i].Children_Num + ')</td>';
                        }
                        htmls += '<td>' + items[i].Category_Type + '</td>';
                        htmls += '<td><a href="/Category/Edit/' + items[i].Category_ID + '"  class="btn btn-info">编辑</a><button type="button" class="btn btn-danger" onclick="Del(' + items[i].Category_ID + ')">删除</button></td>';
                        htmls += '</tr>';
                    }
                    $("#DataList").html(htmls);
                }
            });
            return false;
        }
        function OpenCates(href) {
            OpenWin('分组编辑', href, 'col-md-6 col-md-offset-3');
        }
        function Del(_ID) {
            confirmE("确定删除？", function () {
                $.ajax({
                    type: "POST",
                    url: "/Category/DelCate",
                    data: { id: _ID },
                    success: function (result) {
                        if (result.code == 100)
                            LoadDatas();
                        else
                            alertE(result.message);
                    }
                });
            });
        }
    </script>

    <ol class="breadcrumb">
        <li><a href="/Home/Main"><i class="iconfont icon-home"></i>控制台</a></li>
        <li class="active">分类管理</li>
    </ol>
    <div class="panel panel-default">
        <div class="container">
            <h3><i class="iconfont icon-tree"></i>分类管理</h3>
            <hr>
            <div class="pull-right">
            </div>
            <ul class="list-inline">
                <li>
                    <div class="btn-group" role="group" aria-label="...">
                        <a class="btn btn-info" href="/Category/Edit/0">添加</a>
                        <button type="button" class="btn btn-success" onclick="GoBack()">返回</button>
                    </div>
                </li>
                <li>
                    <form class="form-inline" role="form">
                        <input type="hidden" id="ParentID" value="<%=ViewData["ParentID"] %>" />
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
