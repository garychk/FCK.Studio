<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        $(function () {
            LoadDatas();
        });
        function LoadDatas() {
            var _keywords = $("#keywords").val();
            var _page = parseInt($("#currentpage").val());
            $("#DataList").html('<center><img src="/Content/images/loading.gif" /></center>');
            $.ajax({
                type: "POST",
                url: "/Members/GetMembers",
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
                    htmls += '<td>会员账号</td>';
                    htmls += '<td>会员姓名</td>';
                    htmls += '<td>会员级别</td>';
                    htmls += '<td>联系电话</td>';
                    htmls += '<td>余额</td>';
                    htmls += '<td>操作</td>';
                    htmls += '</tr>';
                    for (var i = 0; i < items.length; i++) {
                        htmls += '<tr>';
                        htmls += '<td><input type="checkbox" name="member" value="' + items[i].Member_ID + '"></td>';
                        htmls += '<td>' + items[i].Member_UserName + '</td>';
                        htmls += '<td>' + items[i].Member_Name + '</td>';
                        htmls += '<td>' + items[i].Group_Name + '</td>';
                        htmls += '<td>' + items[i].Member_Mobile + '</td>';
                        htmls += '<td class="colorRed">￥' + items[i].Member_Amount + '</td>';
                        htmls += '<td><a href="/Members/Edit/' + items[i].Member_ID + '" class="btn btn-primary">编辑</a><button type="button" class="btn btn-danger" onclick="Del(' + items[i].Member_ID + ')">删除</button></td>';
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
                $("#DataList input[name=member]").each(function () {
                    if ($(this).prop("checked")) {
                        i++;
                        var _id = $(this).val();
                        $.ajax({
                            type: "POST",
                            url: "/Members/DelMember",
                            data: { memberid: _id },
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
                    url: "/Members/DelMember",
                    data: { memberid: _ID },
                    success: function (result) {
                        if (result.code == 100)
                            LoadDatas();
                        else
                            alertE(result.message);
                    }
                });
            });
        }
        function OpenRecharge() {
            var memberid = 0;
            var i = 0;
            $("#DataList input[name=member]").each(function () {
                if ($(this).prop("checked")) {
                    i++;
                    memberid = $(this).val();
                }
            });
            if (i == 0 || i > 1) {
                alertE("请选择一个会员，且只能选择一个会员！");
            }
            else {
                OpenWin("用户充值", "/Members/Recharge/" + memberid);
            }
        }
    </script>
    <ol class="breadcrumb">
        <li><a href="/Home/Main"><i class="iconfont icon-home"></i>控制台</a></li>
        <li class="active">会员管理</li>
    </ol>
    <div class="panel panel-default">
        <div class="container">
            <h3><i class="iconfont icon-users"></i>会员管理</h3>
            <hr>
            <div class="pull-right">
            </div>
            <ul class="list-inline">
                <li>
                    <div class="btn-group" role="group">
                        <a class="btn btn-info" href="/Members/Add">添加</a>
                        <button type="button" class="btn btn-success" onclick="OpenRecharge()">充值</button>
                        <button type="button" class="btn btn-danger" onclick="DelPacth()">删除</button>
                    </div>
                </li>
                <li>
                    <form class="form-inline" role="form">
                        <input type="hidden" id="currentpage" value="1" />
                        <div class="input-group">
                            <input type="text" class="form-control" placeholder="快速搜索" id="keywords">
                            <span class="input-group-btn">
                                <button type="button" class="btn btn-success" onclick="GoSearch()">GO</button>
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
