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
            var _adminname = $("#adminname").val();
            var _page = parseInt($("#currentpage").val());
            $("#DataList").html('<center><img src="/Content/images/loading.gif" /></center>');
            $.ajax({
                type: "POST",
                url: "/Admin/GetAdmins",
                data: {
                    page: _page,
                    pageSize: 10,
                    adminname: _adminname
                },
                success: function (result) {
                    var items = result.datas;
                    var pages = result.pages;
                    var total = result.total;
                    var htmls = '';
                    htmls += '<tr>';
                    htmls += '<td><input type="checkbox" id="SelAll"> 全选</td>';
                    htmls += '<td>用户名</td>';
                    htmls += '<td>姓名</td>';
                    htmls += '<td>联系电话</td>';
                    htmls += '<td>入职时间</td>';
                    htmls += '<td>状态</td>';
                    htmls += '<td>操作</td>';
                    htmls += '</tr>';
                    for (var i = 0; i < items.length; i++) {
                        htmls += '<tr>';
                        htmls += '<td><input type="checkbox" name="admin" value="' + items[i].Admin_ID + '"></td>';
                        htmls += '<td>' + items[i].Admin_Name + '</td>';
                        htmls += '<td>' + items[i].Admin_TrueName + '</td>';
                        htmls += '<td>' + items[i].Admin_Telphone + '</td>';
                        htmls += '<td>' + items[i].Admin_RegTime + '</td>';
                        htmls += '<td>' + cStatus(items[i].Admin_Status) + '</td>';
                        htmls += '<td>';
                        htmls += '<button type="button" class="btn btn-danger MR5" onclick="Del(' + items[i].Admin_ID + ')">删除</button>';
                        htmls += '<a class="btn btn-success MR5" href="javascript:void(0)" onclick="OpenDetail(\'/Admin/Edit/' + items[i].Admin_ID + '\',\'编辑\')">编辑</a>';
                        htmls += '<a class="btn btn-info" href="javascript:void(0)" onclick="OpenDetail(\'/Admin/ResetPassword/' + items[i].Admin_ID + '\',\'修改密码\')">改密</a>';
                        htmls += '</td>';
                        htmls += '</tr>';
                    }
                    $("#DataList").html(htmls);
                    $("#pagination").html(Pagination(_page, pages, total));
                }
            });
            return false;
        }

        function Del(ID) {
            confirmE("确定删除？", function () {
                $.ajax({
                    type: "POST",
                    url: "/Admin/DelAdmin",
                    data: { adminid: ID },
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
                var i = 0;
                $("#DataList input[name=admin]").each(function () {
                    if ($(this).prop("checked")) {
                        i++;
                        var _adminid = $(this).val();
                        $.ajax({
                            type: "POST",
                            url: "/Admin/DelAdmin",
                            data: { adminid: _adminid },
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

        function LockAdmin(_status) {
            $("#DataList input[name=admin]").each(function () {
                if ($(this).prop("checked")) {
                    var _adminid = $(this).val();
                    UpdateAdminStatus(_adminid, _status);
                }
            });
        }

        function UpdateAdminStatus(_adminid, _status) {
            $.ajax({
                type: "POST",
                url: "/Admin/UpdateAdminStatus",
                data: { adminid: _adminid, status: _status },
                success: function (result) {
                    if (result.code == 100)
                        LoadDatas();
                    else
                        alertE(result.message);
                }
            });
        }

        function OpenDetail(href, title) {
            OpenWin(title, href);
            return false;
        }
    </script>
    <ol class="breadcrumb">
        <li><a href="/Home/Main"><i class="iconfont icon-home"></i>控制台</a></li>
        <li class="active">管理员管理</li>
    </ol>
    <div class="panel panel-default">
        <div class="container">
            <h3><i class="iconfont icon-admins"></i>管理员管理</h3>
            <hr>
            <div class="pull-right">
            </div>
            <ul class="list-inline">
                <li>
                    <div class="btn-group" role="group" aria-label="...">
                        <button type="button" class="btn btn-primary" onclick="javascript:window.location='/Admin/Add'">添加</button>
                        <button type="button" class="btn btn-success LockAdmin" onclick="LockAdmin(1)">锁定</button>
                        <button type="button" class="btn btn-info LockAdmin" onclick="LockAdmin(0)">解锁</button>
                        <button type="button" class="btn btn-danger" onclick="DelPacth()">删除</button>
                    </div>
                </li>
                <li>
                    <form class="form-inline" role="form">
                        <input type="hidden" id="currentpage" value="1" />
                        <div class="input-group">
                            <input type="text" class="form-control" id="adminname" placeholder="快速搜索">
                            <span class="input-group-btn">
                                <button type="button" class="btn btn-default" onclick="GoSearch()">搜索</button>
                            </span>
                        </div>
                    </form>
                </li>
            </ul>
            <table class="table table-hover" id="DataList">
                <tr>
                    <td>
                        <input type="checkbox" id="chooseall" /></td>
                    <td>编号</td>
                    <td>姓名</td>
                    <td>联系电话</td>
                    <td>注册时间</td>
                    <td>操作</td>
                </tr>
                <tr>
                    <td scope="row">
                        <input type="checkbox" /></td>
                    <td>Mark</td>
                    <td>Otto</td>
                    <td>@mdo</td>
                    <td>Otto</td>
                    <td>
                        <button type="button" class="btn btn-info  btn-sm">锁定</button>
                        <button type="button" class="btn btn-success  btn-sm">编辑</button></td>
                </tr>
                <tr>
                    <th scope="row">
                        <input type="checkbox" /></th>
                    <td>Jacob</td>
                    <td>Thornton</td>
                    <td>@fat</td>
                    <td>Otto</td>
                    <td>
                        <button type="button" class="btn btn-info  btn-sm">锁定</button>
                        <button type="button" class="btn btn-success  btn-sm">编辑</button></td>
                </tr>
                <tr>
                    <th scope="row">
                        <input type="checkbox" /></th>
                    <td>Larry</td>
                    <td>the Bird</td>
                    <td>@twitter</td>
                    <td>Otto</td>
                    <td>
                        <button type="button" class="btn btn-info  btn-sm">锁定</button>
                        <button type="button" class="btn btn-success  btn-sm">编辑</button></td>
                </tr>
            </table>
            <hr>
            <nav class="rgt">
                <ul class="pagination" id="pagination">
                    <li>
                        <a href="#" aria-label="Previous">
                            <span aria-hidden="true">&laquo;</span>
                        </a>
                    </li>
                    <li class="active"><a href="#">1 <span class="sr-only">(current)</span></a></li>
                    <li><a href="#">2</a></li>
                    <li><a href="#">3</a></li>
                    <li><a href="#">4</a></li>
                    <li><a href="#">5</a></li>
                    <li>
                        <a href="#" aria-label="Next">
                            <span aria-hidden="true">&raquo;</span>
                        </a>
                    </li>
                </ul>
            </nav>
        </div>
    </div>
</asp:Content>
