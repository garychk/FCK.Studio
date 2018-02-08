<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        $(function () {
            LoadDatas();
            GetRegisters();
        });
        var apis = null;
        function LoadDatas() {
            var _keywords = $("#keywords").val();
            var _page = parseInt($("#currentpage").val());
            $("#DataList").html('<center><img src="/Content/images/loading.gif" /></center>');
            $.ajax({
                type: "POST",
                url: "/FckAPI/GetAPIs",
                data: {
                    page: _page,
                    pageSize: 0,
                    keywords: _keywords
                },
                success: function (result) {
                    var items = result.datas;
                    var pages = result.pages;
                    var total = result.total;
                    apis = items;
                    var htmls = '';
                    htmls += '<tr>';
                    htmls += '<td><input type="checkbox" id="SelAll"> 全选</td>';
                    htmls += '<td>API</td>';
                    htmls += '<td>方法</td>';
                    htmls += '<td>状态</td>';
                    htmls += '<td>调用次数</td>';
                    htmls += '<td>说明</td>';
                    htmls += '<td>操作</td>';
                    htmls += '</tr>';
                    for (var i = 0; i < items.length; i++) {
                        htmls += '<tr>';
                        htmls += '<td><input type="checkbox" name="api" value="' + items[i].API + '" data-id="' + items[i].ID + '"></td>';
                        htmls += '<td>' + items[i].API + '</td>';
                        htmls += '<td>' + items[i].Methord + '</td>';
                        if (items[i].Status == 1)
                            htmls += '<td><i class="glyphicon glyphicon-ok colorGreen"></i></td>';
                        else
                            htmls += '<td><i class="glyphicon glyphicon-remove colorRed"></i></td>';
                        htmls += '<td>' + items[i].UseTimes + '</td>';
                        htmls += '<td><input class="form-control input-sm" value="' + items[i].Title + '" onblur="UpdateApi(' + items[i].ID + ',this.value)" /></td>';
                        htmls += '<td>';
                        htmls += '<a href="javascript:OpenEdit(' + items[i].ID + ')" class="btn btn-success">编辑</a>';
                        htmls += '<button type="button" class="btn btn-danger" onclick="Del(' + items[i].ID + ')">删除</button>';
                        htmls += '<a href="/FckAPI/Paras/' + items[i].ID + '" class="btn btn-info">参数</a>';
                        htmls += '</td>';
                        htmls += '</tr>';
                    }
                    $("#DataList").html(htmls);
                    $("#pagination").html(Pagination(_page, pages, total));
                }
            });
            return false;
        }

        function UpdateApi(id, val) {
            var api = null;
            for (var i = 0; i < apis.length; i++)
            {
                if (apis[i].ID == id)
                    api = apis[i];
            }
            if (api != null) {
                api.Title = val;
                $.ajax({
                    type: "POST",
                    url: "/FckAPI/CreateOrUpdate",
                    data: api,
                    success: function (result) {
                        LoadDatas();
                    }
                });
            }
        }

        function EditApiPower() {
            var registid = parseInt($("#registid").val());
            var powers = "";
            $("#DataList input[name=api]").each(function () {
                if ($(this).prop("checked")) {
                    if (powers == "")
                        powers += $(this).val();
                    else
                        powers += "," + $(this).val();
                }
            });
            $("#powers").val(powers);

            if (powers == "") {
                alertE("请至少选择一项API"); return false;
            }
            var options = {
                url: "/Registers/UpdatePower",
                type: 'post',
                success: function (result) {
                    if (result.code == 100) {
                        LoadDatas();
                        alertE("权限更新成功！");
                    }
                    else
                        alertE(result.message);
                }
            };
            $("#FormEditPower").ajaxSubmit(options);
            return false;
        }
        var Registers = null;
        function GetRegisters() {
            $.ajax({
                type: "POST",
                url: "/Registers/GetPageList",
                data: {
                    page: 1,
                    pageSize: 0
                },
                success: function (result) {
                    var items = result.datas;
                    Registers = items;
                    var htmls = '<option value="0">请选择租户</option>';
                    for (var i = 0; i < items.length; i++) {
                        htmls += '<option value="' + items[i].Register_ID + '">' + items[i].Register_Name + '</option>';
                    }
                    $("#registid").html(htmls);
                }
            });
        }

        function SetVal() {
            var id = $("#registid").val();
            for (var i = 0; i < Registers.length; i++) {
                if (Registers[i].Register_ID == id) {
                    var powers = Registers[i].Register_ApiPower;
                    powers = powers == null ? "" : powers;
                    $("#DataList input[name=api]").each(function () {
                        if (powers.indexOf($(this).val()) >= 0) {
                            $(this).prop("checked", true);
                            $(this).parent().parent().addClass('warning');
                        } else {
                            $(this).prop("checked", false);
                            $(this).parent().parent().removeClass('warning');
                        }
                    });

                }
            }
        }

        function Lock(_status) {
            var ids = "";
            $("#DataList input[name=api]").each(function () {
                if ($(this).prop("checked")) {
                    if (ids == "")
                        ids += $(this).data("id");
                    else
                        ids += "," + $(this).data("id");
                }
            });
            if (ids == "") {
                alertE("请至少选择一项！"); return false;
            } else
                UpdateStatus(ids, _status);
        }

        function UpdateStatus(_id, _status) {
            $.ajax({
                type: "POST",
                url: "/FckAPI/UpdateStatus",
                data: { ids: _id, status: _status },
                success: function (result) {
                    if (result.code == 100)
                        LoadDatas();
                    else
                        alertE(result.message);
                }
            });
        }

        function Del(_id) {
            confirmE("确定删除？", function () {
                $.ajax({
                    type: "POST",
                    url: "/FckAPI/Del",
                    data: { ids: _id },
                    success: function (result) {
                        if (result.code == 100)
                            LoadDatas();
                        else
                            alertE(result.message);
                    }
                });
            });
        }

        function OpenEdit(id) {
            OpenWin('接口编辑', '/FckAPI/Edit/' + id, '');
            return false;
        }
    </script>
    <ol class="breadcrumb">
        <li><a href="/Home/Main"><i class="iconfont icon-home"></i>控制台</a></li>
        <li class="active">API管理</li>
    </ol>
    <div class="panel panel-default">
        <div class="container">
            <h3><i class="iconfont icon-api"></i>API管理</h3>
            <hr>
            <div class="pull-right">
            </div>
            <ul class="list-inline">
                <li>
                    <div class="btn-group" role="group" aria-label="...">
                        <a href="javascript:OpenEdit(0)" class="btn btn-info">添加</a>
                        <button type="button" class="btn btn-danger" onclick="Lock(0)">锁定</button>
                        <button type="button" class="btn btn-success" onclick="Lock(1)">解锁</button>
                    </div>
                </li>
                <li>
                    <form class="form-inline" role="form">
                        <input type="hidden" id="currentpage" value="1" />
                        <div class="input-group">
                            <input type="text" class="form-control" id="keywords" placeholder="快速搜索">
                            <span class="input-group-btn">
                                <button type="button" class="btn btn-default" onclick="GoSearch()">搜索</button>
                            </span>
                        </div>
                    </form>
                </li>
                <li>
                    <form id="FormEditPower" name="FormEditPower" class="form-inline">
                        <div class="input-group">
                            <input type="hidden" name="powers" id="powers" />
                            <select id="registid" name="registid" class="form-control" onchange="SetVal()">
                            </select>
                            <span class="input-group-btn">
                                <button type="button" class="btn btn-primary" onclick="EditApiPower()">更新权限</button>
                            </span>
                        </div>
                    </form>

                </li>
            </ul>

            <table class="table table-hover" id="DataList"></table>
            <hr>
            <nav class="rgt">
                <ul class="pagination" id="pagination"></ul>
            </nav>

        </div>
    </div>
</asp:Content>
