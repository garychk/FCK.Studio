<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    SimpleAdd
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        $(function () {
            GetGroups("");
            $("#Btn_Ok").click(function () {
                if ($("#Member_UserName").val() == "") {
                    alertE("请输入会员名！"); return false;
                }
                if ($("#Member_Name").val() == "") {
                    alertE("请输入会员姓名！"); return false;
                }
                var _mobile = $("#Member_Mobile").val();
                if (_mobile == "") {
                    alertE("请输入会员手机号！"); return false;
                }
                if (!checkMobile(_mobile)) {
                    alertE("手机号码格式不正确！"); return false;
                }
                var options = {
                    url: '/Members/EditMember',
                    type: 'post',
                    success: function (result) {
                        if (result.code == 100) {
                            $(".jconfirm-box-container .closeIcon").click();
                            GetMembers('');
                            GetUserModel(result.id);
                        }
                        else
                            alertE(result.message);
                    }
                };
                $("#SimpleAddForm").ajaxSubmit(options);
                return false;
            });
        });
        function GetGroups(_groupname) {
            $.ajax({
                type: "POST",
                url: "/Members/GetGroups",
                data: { page: 1, pageSize: 0, groupname: _groupname },
                success: function (result) {
                    var datas = result.datas;
                    var htmls = '';
                    for (var i = 0; i < datas.length; i++) {
                        htmls += '<option value="' + datas[i].Group_ID + '">' + datas[i].Group_Name + '</option>';
                    }
                    $("#Group_ID").html(htmls);
                }
            });
        }
    </script>
    <form class="form-horizontal EditForm" name="SimpleAddForm" id="SimpleAddForm">
        <div class="SimpleAdd">
            <div class="form-group">
                <label for="Member_UserName" class="col-sm-3 control-label">会员名</label>
                <div class="col-sm-9">
                    <input type="text" class="form-control input-lg MB20" placeholder="会员名或会员卡号" id="Member_UserName" name="Member_UserName">
                </div>
            </div>
            <input type="hidden" value="123456" name="Member_Password">
            <div class="form-group">
                <label for="Member_Name" class="col-sm-3 control-label">会员姓名</label>
                <div class="col-sm-9">
                    <input type="text" class="form-control input-lg MB20" placeholder="会员姓名" id="Member_Name" name="Member_Name">
                </div>
            </div>
            <div class="form-group">
                <label for="Group_ID" class="col-sm-3 control-label">会员等级</label>
                <div class="col-sm-9">
                    <select class="form-control input-lg MB20" id="Group_ID" name="Group_ID">
                        <option value="0">普通会员</option>
                    </select>
                </div>
            </div>
            <div class="form-group">
                <label for="Member_Mobile" class="col-sm-3 control-label">手机号</label>
                <div class="col-sm-9">
                    <input type="text" class="form-control input-lg MB20" placeholder="手机号" id="Member_Mobile" name="Member_Mobile">
                </div>
            </div>
            <div class="form-group">
                <label for="Member_Mobile" class="col-sm-3 control-label">余额</label>
                <div class="col-sm-9">
                    <input type="text" class="form-control input-lg MB20" placeholder="会员余额" id="Member_Amount" name="Member_Amount" value="0">
                </div>
            </div>
        </div>
        
        <div class="text-center">
            <input type="hidden" id="Member_ID" name="Member_ID" value="0" />
            <button type="submit" class="btn btn-success btn-lg" id="Btn_Ok">确定</button>
        </div>
    </form>

</asp:Content>
