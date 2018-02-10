<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Add
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        $(function () {
            $("#Btn_Submit").click(function () {
                if ($("#Admin_Name").val() == "") {
                    alertE("请输入登录名！"); return false;
                }
                if ($("#Admin_Password").val() == "") {
                    alertE("请输入密码！"); return false;
                }
                if ($("#Admin_Password2").val() != $("#Admin_Password").val()) {
                    alertE("两次输入密码不一致！"); return false;
                }
                if ($("#Admin_TrueName").val() == "") {
                    alertE("请输入员工真实姓名！"); return false;
                }
                var telphone = $("#Admin_Telphone").val();
                if (telphone == "") {
                    alertE("请输入员工联系电话！"); return false;
                }
                if (!checkMobile(telphone) && !checkPhone(telphone)) {
                    alertE("联系电话格式有误！"); return false;
                }
                var options = {
                    url: '/Admin/EditAdmin',
                    type: 'post',
                    success: function (result) {
                        if (result.code == 100) {
                            confirmE("添加成功，是否返回列表？", function () {
                                window.location = '/Admin/Index';
                            }, function () {
                                alertE(result.message);
                            });
                        }
                        else
                            alertE(result.message);
                    }
                };
                $("#SubmitForm").ajaxSubmit(options);
                return false;
            });

            $.ajax({
                type: "POST",
                url: "/Registers/GetPageList",
                data: {
                    page: 1,
                    pageSize: 0
                },
                success: function (result) {
                    var items = result.datas;
                    var pages = result.pages;
                    var total = result.total;
                    var htmls = '';
                    for (var i = 0; i < items.length; i++)
                    {
                        htmls += '<option value="' + items[i].Register_ID + '">' + items[i].Register_Name + '</option>';
                    }
                    $("#Register_ID").append(htmls);
                }
            });
        });
    </script>
    <ol class="breadcrumb">
        <li><a href="/Home/Main"><i class="iconfont icon-home"></i>控制台</a></li>
        <li><a href="/Admin/">管理员管理</a></li>
        <li class="active">添加管理员</li>
    </ol>
    <div class="panel panel-default">
        <div class="container">
            <h3><i class="iconfont icon-edit"></i>添加管理员</h3>
            <hr>
            <form class="form-horizontal" name="SubmitForm" id="SubmitForm">
                <div class="row">
                    <div class="col-xs-6">
                        <div class="form-group">
                            <label for="Admin_Name" class="col-sm-2 control-label">登录名</label>
                            <div class="col-sm-10">
                                <input type="text" class="form-control" placeholder="登录名" value="" id="Admin_Name" name="Admin_Name">
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="Admin_Name" class="col-sm-2 control-label">登录密码</label>
                            <div class="col-sm-10">
                                <input type="text" class="form-control" placeholder="6~12位数字加字母组合" value="" id="Admin_Password" name="Admin_Password">
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="Admin_Name" class="col-sm-2 control-label">重复密码</label>
                            <div class="col-sm-10">
                                <input type="text" class="form-control" placeholder="" value="" id="Admin_Password2">
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="Admin_TrueName" class="col-sm-2 control-label">员工姓名</label>
                            <div class="col-sm-10">
                                <input type="text" class="form-control" placeholder="输入员工真实姓名" value="" id="Admin_TrueName" name="Admin_TrueName">
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="Admin_Telphone" class="col-sm-2 control-label">联系电话</label>
                            <div class="col-sm-10">
                                <input type="tel" class="form-control" placeholder="员工联系电话" value="" id="Admin_Telphone" name="Admin_Telphone">
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="Admin_Telphone" class="col-sm-2 control-label">管理租户</label>
                            <div class="col-sm-10">
                                <select class="form-control" id="Register_ID" name="Register_ID">
                                    <option value="0">请选择</option>
                                </select>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-offset-2 col-sm-8">
                                <a href="/Admin/" class="btn btn-default">返回</a>
                                <button type="reset" class="btn btn-info">重设</button>
                                <button type="button" class="btn btn-success" id="Btn_Submit">确定</button>
                                <input type="hidden" value="0" id="Admin_ID" name="Admin_ID" />
                            </div>
                        </div>

                    </div>
                </div>

            </form>
        </div>
    </div>
</asp:Content>
