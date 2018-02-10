<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    GetClothes
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        $("#Btn_Ok").click(function () {
            if ($("#OrderCode").val() == "") {
                alertE("请输入取货码！"); return false;
            }
            if ($("#OrderNumber").val() == "") {
                alertE("订单号不能为空！"); return false;
            }
            var options = {
                url: '/Orders/TakeService',
                type: 'post',
                success: function (result) {
                    if (result.code == 100) {
                        LoadDatas();
                        alertE("成功！");
                    }
                    else {
                        if (result.message == "CODE_ERROR")
                            alertE("取货码错误");
                        else {
                            alertE(result.message);
                        }
                    }
                }
            };
            $("#GetClothesForm").ajaxSubmit(options);
            return false;
        });
    </script>
    <form class="form-inline" id="GetClothesForm" name="GetClothesForm">
        <div class="form-group">
            <input type="text" class="form-control input-lg" id="OrderCode" name="OrderCode" placeholder="请输入取货码" onkeyup="value=value.replace(/[^\d.]/g,'')">
        </div>
        <input type="hidden" id="OrderNumber" name="OrderNumber" value="<%=ViewData["OrderNumber"] %>" />
        <button type="button" class="btn btn-success btn-lg" id="Btn_Ok">确定</button>
    </form>

</asp:Content>
