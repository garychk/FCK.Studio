<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Groups
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        $(function () {
            $("#Btn_Ok").click(function () {
                if ($("#Group_Name").val() == "") {
                    alertE("请输入分组名！"); return false;
                }
                
                if ($("#Group_Discount").val() == "") {
                    alertE("请输入折扣！"); return false;
                }
                else {
                    var discount = parseFloat($("#Group_Discount").val());
                    if (discount > 1 || discount < 0)
                    {
                        alertE("折扣请输入0~1之间数字！"); return false;
                    }
                }
                if ($("#Group_Points").val() == "") {
                    alertE("请输入积分！"); return false;
                }
                var options = {
                    url: '/Members/EditGroup',
                    type: 'post',
                    success: function (result) {
                        if (result.code == 100) {
                            $("#FreshGroup").click();
                            $("#DoList").click();
                            GetGroups();
                        }
                        else
                            alertE(result.message);
                    }
                };
                $("#EditForm").ajaxSubmit(options);
                return false;
            });
            GetGroups();
        });
        function GetGroups() {
            $.ajax({
                type: "POST",
                url: "/Members/GetGroups",
                data: { page: 1, pageSize: 0},
                success: function (result) {
                    var datas = result.datas;
                    var htmls = '';
                    for (var i = 0; i < datas.length; i++) {
                        htmls += '<tr>';
                        htmls += '<td>' + datas[i].Group_Name + '</td>';
                        htmls += '<td>' + datas[i].Group_Discount*10 + '折</td>';
                        htmls += '<td>' + datas[i].Group_Points + '积分</td>';
                        htmls += '<td><a href="javascript:void(0)" onclick="DelGroup(' + datas[i].Group_ID + ')">删除</a> ';
                        htmls += '<a href="javascript:void(0)" onclick="GetModelGroup(' + datas[i].Group_ID + ')">编辑</a>';
                        htmls += '</td>';
                        htmls += '</tr>';
                    }
                    $("#GroupItems").html(htmls);
                }
            });
        }
        function GetModelGroup(ID) {
            $.ajax({
                type: "POST",
                url: "/Members/GetModelGroup",
                data: { groupid: ID },
                success: function (result) {
                    if (result != null)
                    {
                        $("#DoEdit").click();
                        $("#Group_Name").val(result.Group_Name);
                        $("#Group_Discount").val(result.Group_Discount);
                        $("#Group_Points").val(result.Group_Points);
                        $("#GroupEdit #Group_ID").val(result.Group_ID);
                    }
                }
            });

        }
        function DelGroup(_ID) {
            confirmE("确定删除？", function () {
                $.ajax({
                    type: "POST",
                    url: "/Members/DelGroup",
                    data: { groupid: _ID },
                    success: function (result) {
                        if (result.code == 100)
                            GetGroups();
                        else
                            alertE(result.message);
                    }
                });
            });
        }
    </script>
    <ul class="nav nav-tabs MB20" role="tablist" id="myTabs">
        <li role="presentation" class="active"><a href="#GroupList" aria-controls="home" role="tab" data-toggle="tab" id="DoList">所有分组</a></li>
        <li role="presentation"><a href="#GroupEdit" aria-controls="profile" role="tab" data-toggle="tab" id="DoEdit">编辑</a></li>
    </ul>
    <div class="tab-content">
        <div role="tabpanel" class="tab-pane active" id="GroupList">
            <table class="table table-hover" id="GroupItems">
            </table>
        </div>
        <div role="tabpanel" class="tab-pane" id="GroupEdit">
            <form class="form-inline EditForm" id="EditForm">
                <div class="form-group">
                    <label for="Group_Name">分组名：</label>
                    <input type="text" id="Group_Name" name="Group_Name" class="form-control" />
                </div>
                <hr />
                <div class="form-group">
                    <label for="Group_Discount">折　扣：</label>
                    <input type="text" id="Group_Discount" name="Group_Discount" class="form-control" onkeyup="value=value.replace(/[^\d.]/g,'')" /> <i class="tips">请输入0~1之间的数字</i>
                </div>
                <hr />
                <div class="form-group">
                    <label for="Group_Points">积　分：</label>
                    <input type="text" id="Group_Points" name="Group_Points" class="form-control" onkeyup="value=value.replace(/[^\d.]/g,'')" />
                </div>
                <hr />
                <div class="text-center">
                    <input type="hidden" id="Group_ID" name="Group_ID" value="0" />
                    <button type="button" class="btn btn-success" id="Btn_Ok">确定</button>
                </div>

            </form>
        </div>
    </div>


</asp:Content>
