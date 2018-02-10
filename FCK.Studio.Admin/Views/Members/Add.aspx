<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Add
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        $(function () {
            GetGroups("");
            $("#Btn_Submit").click(function () {
                if ($("#Member_UserName").val() == "") {
                    alertE("请输入会员名！"); return false;
                }
                if ($("#Member_Password").val() == "") {
                    alertE("请输入会员密码！"); return false;
                }
                if ($("#Member_Password2").val() != $("#Member_Password").val()) {
                    alertE("两次密码输入不一致！"); return false;
                }
                if ($("#Member_Name").val() == "") {
                    alertE("请输入会员姓名！"); return false;
                }
                var _mobile = $("#Member_Mobile").val();
                if (_mobile == "") {
                    alertE("请输入会员手机号！"); return false;
                }
                var _email = $("#Member_Email").val();
                if (_email == "") {
                    alertE("请输入电子邮箱！"); return false;
                }
                if (!checkMobile(_mobile)) {
                    alertE("手机号码格式不正确！"); return false;
                }
                if (!checkEmail(_email)) {
                    alertE("电子邮箱格式不正确！"); return false;
                }
                var options = {
                    url: '/Members/EditMember',
                    type: 'post',
                    success: function (result) {
                        if (result.code == 100) {
                            confirmE("编辑成功，是否返回列表？", function () {
                                window.location = '/Members/Index';
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
            $("#FreshGroup").click(function () {
                GetGroups("");
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
        function OpenGroups() {
            OpenWin('分组编辑', '/Members/Groups', 'col-md-6 col-md-offset-3');
        }
    </script>
    <ol class="breadcrumb">
        <li><a href="/Home/Main"><i class="iconfont icon-home"></i>控制台</a></li>
        <li><a href="/Members/">会员管理</a></li>
        <li class="active">会员添加</li>
    </ol>
    <div class="panel panel-default">
        <div class="container">
            <h3><i class="iconfont icon-edit"></i>会员添加</h3>
            <hr>
            <form class="form-horizontal" name="SubmitForm" id="SubmitForm">
                <div class="row">
                    <div class="col-xs-12 col-md-6">
                        <div class="form-group">
                            <label for="Member_UserName" class="col-sm-2 control-label">会员名</label>
                            <div class="col-sm-10">
                                <input type="text" class="form-control" placeholder="会员名或会员卡号" id="Member_UserName" name="Member_UserName">
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="Member_Password" class="col-sm-2 control-label">会员密码</label>
                            <div class="col-sm-10">
                                <input type="password" class="form-control" placeholder="会员密码" id="Member_Password" name="Member_Password">
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="Member_Password2" class="col-sm-2 control-label">重复密码</label>
                            <div class="col-sm-10">
                                <input type="password" class="form-control" id="Member_Password2" name="Member_Password2">
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="Member_Name" class="col-sm-2 control-label">会员姓名</label>
                            <div class="col-sm-10">
                                <input type="text" class="form-control" placeholder="会员姓名" id="Member_Name" name="Member_Name">
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="Member_NickName" class="col-sm-2 control-label">会员昵称</label>
                            <div class="col-sm-10">
                                <input type="text" class="form-control" placeholder="会员昵称" id="Member_NickName" name="Member_NickName">
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="Member_QQ" class="col-sm-2 control-label">会员QQ</label>
                            <div class="col-sm-10">
                                <input type="text" class="form-control" placeholder="会员QQ" id="Member_QQ" name="Member_QQ">
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="Member_Mobile" class="col-sm-2 control-label">余额</label>
                            <div class="col-sm-10">
                                <input type="text" class="form-control" placeholder="会员余额" id="Member_Amount" name="Member_Amount" value="0">
                            </div>
                        </div>

                    </div>
                    <div class="col-xs-12 col-md-6">
                        <div class="form-group">
                            <label for="Member_Sex" class="col-sm-2 control-label">会员性别</label>
                            <div class="col-sm-10">
                                <select class="form-control" id="Member_Sex" name="Member_Sex">
                                    <option value="1">男</option>
                                    <option value="0">女</option>
                                </select>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="Group_ID" class="col-sm-2 control-label">会员等级</label>
                            <div class="col-sm-10">
                                <div class="input-group ">
                                    <select class="form-control" id="Group_ID" name="Group_ID">
                                        <option value="0">普通会员</option>
                                    </select>
                                    <span class="input-group-addon" id="EditGroups"><a href="javascript:void(0)" onclick="OpenGroups()">编辑会员等级</a></span>
                                </div>
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="Member_Mobile" class="col-sm-2 control-label">手机号</label>
                            <div class="col-sm-10">
                                <input type="email" class="form-control" placeholder="手机号" id="Member_Mobile" name="Member_Mobile">
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="Member_Telphone" class="col-sm-2 control-label">联系电话</label>
                            <div class="col-sm-10">
                                <input type="text" class="form-control" placeholder="联系电话" id="Member_Telphone" name="Member_Telphone">
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="Member_Email" class="col-sm-2 control-label">电子邮箱</label>
                            <div class="col-sm-10">
                                <input type="email" class="form-control" placeholder="电子邮箱" id="Member_Email" name="Member_Email">
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="Member_Weixin" class="col-sm-2 control-label">微信号</label>
                            <div class="col-sm-10">
                                <input type="text" class="form-control" placeholder="会员微信号" id="Member_Weixin" name="Member_Weixin">
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="Member_Address" class="col-sm-2 control-label">联系地址</label>
                            <div class="col-sm-10">
                                <input type="text" class="form-control" placeholder="联系地址" id="Member_Address" name="Member_Address">
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-offset-2 col-sm-8">
                                <input type="hidden" id="Member_ID" name="Member_ID" value="0" />
                                <a href="/Members/" class="btn btn-default">返回</a>
                                <button type="reset" class="btn btn-info">重设</button>
                                <input type="hidden" id="FreshGroup" />
                                <button type="submit" class="btn btn-success" id="Btn_Submit">确定</button>
                            </div>
                        </div>
                    </div>
                </div>
            </form>
        </div>
    </div>
</asp:Content>
