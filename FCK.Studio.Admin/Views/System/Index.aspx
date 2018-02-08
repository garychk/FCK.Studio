<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        $(function () {
            $("#EditCode").hide();
            $('#myTabs').on("click", "a", function (e) {
                var li = $(this).parent();
                $('#myTabs li').removeClass("active");
                li.addClass("active");
                var _type = $(this).attr("href");
                if (_type != "#") {
                    $("#codetype").val(_type);
                    LoadDatas();
                }
                return false;
            });
            GetCodeTypes();
            LoadDatas();

            $("#Btn_Submit").click(function () {
                if ($("#Code_Name").val() == "") {
                    alertE("请输入索引名！"); return false;
                }
                if ($("#Code_Value").val() == "") {
                    alertE("请输入索引值！"); return false;
                }
                if ($("#Code_Type").val() == "") {
                    alertE("请输入类型！"); return false;
                }
                var options = {
                    url: '/System/EditCode',
                    type: 'post',
                    success: function (result) {
                        if (result.code == 100) {
                            $("#SubmitForm").resetForm();
                            $("#Code_ID").val(0);
                            GetCodeTypes();
                            LoadDatas();
                            $("#EditCode").hide();
                        }
                        else
                            alertE(result.message);
                    }
                };
                $("#SubmitForm").ajaxSubmit(options);
                return false;
            });
        });

        function LoadDatas() {
            var _codetype = $("#codetype").val();
            var _page = parseInt($("#currentpage").val());
            $("#DataList").html('<center><img src="/Content/images/loading.gif" /></center>');
            $.ajax({
                type: "POST",
                url: "/System/GetCodes",
                data: {
                    page: _page,
                    pageSize: 10,
                    codetype: _codetype
                },
                success: function (result) {
                    var items = result.datas;
                    var pages = result.pages;
                    var total = result.total;
                    var htmls = '';
                    htmls += '<tr>';
                    htmls += '<td><input type="checkbox" id="SelAll"> 全选</td>';
                    htmls += '<td>索引</td>';
                    htmls += '<td>值</td>';
                    htmls += '<td>类型</td>';
                    htmls += '<td>操作</td>';
                    htmls += '</tr>';
                    for (var i = 0; i < items.length; i++) {
                        htmls += '<tr>';
                        htmls += '<td><input type="checkbox" name="code" value="' + items[i].Code_ID + '"></td>';
                        htmls += '<td>' + items[i].Code_Name + '</td>';
                        htmls += '<td>' + items[i].Code_Value + '</td>';
                        htmls += '<td>' + items[i].Code_Type + '</td>';
                        htmls += '<td>';
                        htmls += '<button type="button" class="btn btn-danger MR5" onclick="Del(' + items[i].Code_ID + ')">删除</button>';
                        htmls += '<a class="btn btn-success" href="javascript:void(0)" onclick="DoEdit(' + items[i].Code_ID + ')">编辑</a>';
                        htmls += '</td>';
                        htmls += '</tr>';
                    }
                    $("#DataList").html(htmls);
                    $("#pagination").html(Pagination(_page, pages, total));
                }
            });
            return false;
        }

        function GetCodeTypes() {
            var _codetype = $("#codetype").val();
            var _page = parseInt($("#currentpage").val());
            $("#myTabs").html('<center><img src="/Content/images/loading.gif" /></center>');
            $.ajax({
                type: "POST",
                url: "/System/GetCodeTypes",
                success: function (items) {
                    var htmls = '';
                    htmls += '<li role="presentation" class="active"><a href="">全部索引</a></li>';
                    for (var i = 0; i < items.length; i++) {
                        htmls += '<li role="presentation"><a href="' + items[i] + '">' + items[i] + '</a></li>';
                    }
                    htmls += '<li role="presentation"><a href="#" onclick="DoEdit(0)"><i class="glyphicon glyphicon-plus"></i> 添加索引</a></li>';
                    $("#myTabs").html(htmls);
                }
            });
            return false;
        }

        function DoEdit(ID) {
            $("#EditCode").show();
            if (ID > 0) {
                $.ajax({
                    type: "POST",
                    url: "/System/GetModel",
                    data: { codeid: ID },
                    success: function (result) {
                        if (result != null) {
                            $("#Code_Name").val(result.Code_Name);
                            $("#Code_Value").val(result.Code_Value);
                            $("#Code_Type").val(result.Code_Type);
                            $("#Code_ID").val(result.Code_ID);
                        }
                    }
                });
            } else {
                $("#SubmitForm").resetForm();
            }
            return false;
        }

        function Del(ID) {
            confirmE("确定删除？", function () {
                $.ajax({
                    type: "POST",
                    url: "/System/DelCode",
                    data: { codeid: ID },
                    success: function (result) {
                        if (result.code == 100) {
                            LoadDatas(); GetCodeTypes();
                        }
                        else
                            alertE(result.message);
                    }
                });
            });
        }
    </script>
    <ol class="breadcrumb">
        <li><a href="/Home/Main"><i class="iconfont icon-home"></i>控制台</a></li>
        <li class="active">系统设置</li>
    </ol>
    <div class="panel panel-default">
        <div class="container">
            <h3><i class="iconfont icon-setting"></i>系统设置</h3>
            <hr>

            <!-- Nav tabs -->
            <ul class="nav nav-tabs MB20" role="tablist" id="myTabs"></ul>

            <!-- Tab panes -->
            <div class="tab-content">
                <div role="tabpanel" class="tab-pane active" id="config_base">
                    <div class="P10" id="EditCode">
                        <form class="form-inline" id="SubmitForm">
                            <div class="form-group">
                                <label for="Code_Name">索引名：</label>
                                <input type="text" id="Code_Name" name="Code_Name" class="form-control" />
                            </div>
                            <div class="form-group">
                                <label for="Code_Value">索引值：</label>
                                <input type="text" id="Code_Value" name="Code_Value" class="form-control" />
                            </div>
                            <div class="form-group">
                                <label for="Code_Value">类型：</label>
                                <input type="text" id="Code_Type" name="Code_Type" class="form-control" />
                            </div>
                            <input type="hidden" id="Code_ID" name="Code_ID" value="0" />
                            <button type="submit" class="btn btn-success" id="Btn_Submit">确定</button>
                        </form>
                    </div>
                    <table class="table table-hover" id="DataList"></table>
                    <hr>
                    <nav class="rgt">
                        <ul class="pagination" id="pagination"></ul>
                    </nav>
                </div>
            </div>
            <input type="hidden" id="currentpage" value="1" />
            <input type="hidden" id="codetype" value="" />
        </div>
    </div>
</asp:Content>
