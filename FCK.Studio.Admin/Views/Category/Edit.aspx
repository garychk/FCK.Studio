<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FCK.Studio.Dto.CategoryDto>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Edit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        $(function () {
            $("#Btn_Ok").click(function () {
                if ($("#Category_Name").val() == "") {
                    alertE("请输入分类名称！"); return false;
                }
                if ($("#Category_ParentID").val() == $("#Category_ID").val() && $("#Category_ID").val() > 0) {
                    alertE("不能选择自身为父级！"); return false;
                }
                var options = {
                    url: '/Category/EditCate',
                    type: 'post',
                    success: function (result) {
                        if (result.code == 100) {
                            window.history.go(-1);
                        }
                        else
                            alertE(result.message);
                    }
                };
                $("#SimpleEditForm").ajaxSubmit(options);
                return false;
            });

            $.post('/Category/GetCateTree', {}, function (response) {
                var initSelectableTree = function () {
                    return $('#CategoryTree').treeview({
                        levels: 9,
                        data: response,
                        onNodeSelected: function (event, node) {
                            $("#NodeText").text(node.text);
                            $("#Category_ParentID").val(node.id);
                        },
                        onNodeUnselected: function (event, node) { }
                    });
                }
                var $selectableTree = initSelectableTree();

            });

        });
        
    </script>
    <ol class="breadcrumb">
        <li><a href="/Home/Main"><i class="iconfont icon-home"></i>控制台</a></li>
        <li><a href="/Category/">分类管理</a></li>
        <li class="active">分类编辑</li>
    </ol>
    <div class="panel panel-default">
        <div class="container">
            <h3><i class="iconfont icon-edit"></i>分类编辑</h3>
            <hr>
    <form class="form-horizontal EditForm" name="SimpleEditForm" id="SimpleEditForm">
                <div class="form-group">
                    <label for="Category_ParentID" class="col-sm-2 control-label">父级分类</label>
                    <div class="col-sm-6">
                        <input type="hidden" id="Category_ParentID" name="Category_ParentID" value="<%=Model.Category_ParentID%>" />
                        <div class="btn-group">
                            <button type="button" class="btn btn-info" id="NodeText"><%=ViewData["ParentName"] %></button>
                            <button type="button" class="btn btn-info dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                <span class="caret"></span>
                                <span class="sr-only">Toggle Dropdown</span>
                            </button>
                            <div class="dropdown-menu" style="min-width: 240px">
                                <div id="CategoryTree" style="max-height: 180px; overflow-y: scroll"></div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="form-group">
                    <label for="Article_Title" class="col-sm-2 control-label">分类名称</label>
                    <div class="col-sm-6">
                        <input type="text" class="form-control" placeholder="分类名称" id="Category_Name" name="Category_Name" value="<%=Model.Category_Name %>">
                    </div>
                </div>
                <div class="form-group">
                    <label for="Category_Type" class="col-sm-2 control-label">版块</label>
                    <div class="col-sm-6">
                        <input type="text" class="form-control" placeholder="版块名称" id="Category_Type" name="Category_Type" value="<%=Model.Category_Type %>">
                    </div>
                </div>
                <div class="form-group">
                    <label for="Category_OrderID" class="col-sm-2 control-label">序号</label>
                    <div class="col-sm-6">
                        <input type="text" class="form-control" placeholder="分类名称" id="Category_OrderID" name="Category_OrderID" value="<%=Model.Category_OrderID %>">
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-offset-2 col-sm-6">
                        <input type="hidden" id="Category_ID" name="Category_ID" value="<%=Model.Category_ID %>" />
                        <button type="submit" class="btn btn-success" id="Btn_Ok">确定</button>
                    </div>
                </div>
            </form>
        </div>
    </div>
</asp:Content>
