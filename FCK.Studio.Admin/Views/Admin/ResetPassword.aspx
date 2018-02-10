<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FCK.Studio.Dto.EditAdminM>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    ResetPassword
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<script>
    $(function () {
        $("#Btn_Submit").click(function () {
            if ($("#newpassword").val() == "") {
                alertE("请输入新密码！"); return false;
            }
            if ($("#newpassword2").val() != $("#newpassword").val()) {
                alertE("两次密码输入不一致！"); return false;
            }
            var options = {
                url: '/Admin/UpdateAdminPsw',
                type: 'post',
                success: function (result) {
                    if (result.code == 100) {
                        alertE("密码修改成功！");
                    }
                    else
                        alertE(result.message);
                }
            };
            $("#SubmitForm").ajaxSubmit(options);
            return false;
        });
    });
    </script>
    <form class="form-horizontal" name="SubmitForm" id="SubmitForm">
        <div class="row">
            <div class="col-xs-12">
                
                <div class="form-group">
                    <label class="col-sm-4 control-label">管理员名</label>
                    <div class="col-sm-8">
                        <%=Model.Admin_Name %>
                    </div>
                </div>
                <div class="form-group">
                    <label for="newpassword" class="col-sm-4 control-label">新密码</label>
                    <div class="col-sm-8">
                        <input type="password" class="form-control" id="newpassword" name="newpassword">
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-4 control-label">确认新密码</label>
                    <div class="col-sm-8">
                        <input type="password" class="form-control" id="newpassword2">
                    </div>
                </div>
            </div>
        </div>

        <div class="text-right">
            <button type="reset" class="btn btn-info">重设</button>
            <button type="submit" class="btn btn-success" id="Btn_Submit">确定</button>
            <input type="hidden" value="<%=Model.Admin_ID %>" id="adminid" name="adminid" />
        </div>
    </form>
</asp:Content>
