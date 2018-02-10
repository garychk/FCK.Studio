<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Recharge
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        $("#Btn_Ok").click(function () {
            if ($("#money").val() == "") {
                alertE("请输入充值金额！"); return false;
            }
            if ($("#memberid").val() == "") {
                alertE("请选择一位会员！"); return false;
            }
            var memberid = parseInt($("#memberid").val());
            if (memberid == 0) {
                alertE("请选择一位会员！"); return false;
            }
            var options = {
                url: '/Members/DoRecharge',
                type: 'post',
                success: function (result) {
                    if (result.code == 100) {
                        LoadDatas();
                        alertE("会员充值成功！");
                    }
                    else
                        alertE(result.message);
                }
            };
            $("#RechargeForm").ajaxSubmit(options);
            return false;
        });
    </script>
    <form class="form-inline" id="RechargeForm" name="RechargeForm">
        <div class="form-group">
            <input type="text" class="form-control input-lg" id="money" name="money" placeholder="请输入金额" onkeyup="value=value.replace(/[^\d.]/g,'')">
        </div>
        <input type="hidden" id="memberid" name="memberid" value="<%=ViewData["MemberID"] %>" />
        <button type="button" class="btn btn-success btn-lg" id="Btn_Ok">确定</button>
    </form>

</asp:Content>
