<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FCK.Studio.Dto.EditAdminM>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Edit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        var RegisterId = '<%=Model.Register_ID%>';
        $(function () {
            $("#Btn_Submit").click(function () {
                if ($("#Admin_Name").val() == "") {
                    alertE("请输入登录名！"); return false;
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
                            alertE("编辑成功！");
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
                    for (var i = 0; i < items.length; i++) {
                        if (RegisterId == items[i].Register_ID)
                            htmls += '<option value="' + items[i].Register_ID + '" selected="selected">' + items[i].Register_Name + '</option>';
                        else
                            htmls += '<option value="' + items[i].Register_ID + '">' + items[i].Register_Name + '</option>';
                    }
                    $("#Register_ID").append(htmls);
                }
            });
        });
    </script>
    <%--<ol class="breadcrumb">
        <li><a href="/Home/Main"><i class="glyphicon glyphicon-home"></i>控制台</a></li>
        <li><a href="/Admin/">员工管理</a></li>
        <li class="active">员工编辑</li>
    </ol>--%>
    <%--<h3><i class="glyphicon glyphicon-edit"></i>员工编辑</h3><hr>--%>
    <form class="form-horizontal" name="SubmitForm" id="SubmitForm">
        <div class="row">
            <div class="col-xs-12">
                <div class="form-group">
                    <label for="Admin_Name" class="col-sm-3 control-label">登录名</label>
                    <div class="col-sm-9">
                        <input type="text" class="form-control" placeholder="登录名" value="<%=Model.Admin_Name %>" id="Admin_Name" name="Admin_Name">
                    </div>
                </div>
                <div class="form-group">
                    <label for="Admin_Code" class="col-sm-3 control-label">员工编号</label>
                    <div class="col-sm-9">
                        <input type="text" class="form-control" placeholder="员工编号" value="<%=Model.Admin_Code %>" id="Admin_Code" name="Admin_Code" readonly="true">
                    </div>
                </div>
                <div class="form-group">
                    <label for="Admin_TrueName" class="col-sm-3 control-label">员工姓名</label>
                    <div class="col-sm-9">
                        <input type="text" class="form-control" placeholder="员工姓名" value="<%=Model.Admin_TrueName %>" id="Admin_TrueName" name="Admin_TrueName">
                    </div>
                </div>
                <div class="form-group">
                    <label for="Admin_Telphone" class="col-sm-3 control-label">联系电话</label>
                    <div class="col-sm-9">
                        <input type="tel" class="form-control" placeholder="员工联系电话" value="<%=Model.Admin_Telphone %>" id="Admin_Telphone" name="Admin_Telphone">
                    </div>
                </div>
                <div class="form-group">
                    <label for="Admin_Telphone" class="col-sm-3 control-label">管理租户</label>
                    <div class="col-sm-9">
                        <select class="form-control" id="Register_ID" name="Register_ID">
                            <option value="0">请选择</option>
                        </select>
                    </div>
                </div>
            </div>
        </div>

        <div class="text-right">
            <%--<a href="/Admin/" class="btn btn-default btn-lg">返回</a>--%>
            <button type="reset" class="btn btn-info">重设</button>
            <button type="submit" class="btn btn-success" id="Btn_Submit">确定</button>
            <input type="hidden" value="<%=Model.Admin_ID %>" id="Admin_ID" name="Admin_ID" />
        </div>
    </form>

</asp:Content>
