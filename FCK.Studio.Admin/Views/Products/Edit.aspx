<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FCK.Studio.Dto.ProductDto>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Edit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        KindEditor.ready(function (K) {
            var editor = K.editor({
                allowFileManager: false,
                formatUploadUrl: false,
                uploadJson: '/Content/kindeditor/asp.net/ftpupload_json.ashx'
            });
            K('.UploadImg').click(function () {
                var obj_pic = $(this).attr("data-pic");
                editor.loadPlugin('image', function () {
                    editor.plugin.imageDialog({
                        showRemote: false,
                        imageUrl: K('#' + obj_pic).val(),
                        clickFn: function (url, title, width, height, border, align) {
                            K('#' + obj_pic).val(url);
                            K('#' + obj_pic + "_Pre").attr('src', imageServer + url);
                            editor.hideDialog();
                        }
                    });
                });
            });

            K.create('#Product_Content', {
                resizeType: 1,
                allowPreviewEmoticons: true,
                allowImageUpload: true,
                allowFileManager: true,
                height: 400,
                uploadJson: '/Content/kindeditor/asp.net/upload_json.ashx',
                fileManagerJson: '/Content/kindeditor/asp.net/file_manager_json.ashx',
                items: editorItems,
                urlType: 'domain',
                afterCreate: function () {
                    this.sync();
                },
                afterBlur: function () {
                    this.sync();
                }
            });
        });
        $(function () {
            $.post('/Category/GetCateTree?ctype=product', {}, function (response) {
                var initSelectableTree = function () {
                    return $('#Categorys').treeview({
                        levels: 9,
                        data: response[0].nodes,
                        onNodeSelected: function (event, node) {
                            $("#NodeText").text(node.text);
                            $("#Product_CateID").val(node.id);
                        },
                        onNodeUnselected: function (event, node) { }
                    });
                }

                var $selectableTree = initSelectableTree();
                //var selectNode = $selectableTree.treeview('search', [$('#Article_CateName').val(), { ignoreCase: false, exactMatch: false }]);
                //$selectableTree.treeview('selectNode', [selectNode, { silent: true }]);

            });

            $("#Btn_Submit").click(function () {
                if ($("#Product_Name").val() == "") {
                    alertE("请输入名称！"); return false;
                }
                if ($("#Product_Price").val() == "") {
                    alertE("请输入价格！"); return false;
                }
                if ($("#Product_Order").val() == "") {
                    $("#Product_Order").val(1);
                }
                if ($("#Product_PriceLower").val() == "") {
                    $("#Product_PriceLower").val($("#Product_Price").val());
                }
                var contents = escape($("#Product_Content").val());
                $("#Product_Content").val(contents);

                var options = {
                    url: '/Products/EditProduct',
                    type: 'post',
                    success: function (result) {
                        if (result.code == 100) {
                            confirmE("编辑成功，是否返回列表？", function () {
                                window.location = '/Products/Index';
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
        });

    </script>
    <ol class="breadcrumb">
        <li><a href="/Home/Main"><i class="iconfont icon-home"></i>控制台</a></li>
        <li><a href="/Products/">产品管理</a></li>
        <li class="active">产品编辑</li>
    </ol>
    <div class="panel panel-default">
        <div class="container">
            <h3><i class="iconfont icon-edit"></i>产品编辑</h3>
            <hr>
            <form class="form-horizontal" name="SubmitForm" id="SubmitForm">
                <div class="form-group">
                    <label for="Product_CateID" class="col-sm-2 control-label">产品分类</label>
                    <div class="col-sm-6">
                        <input type="hidden" id="Product_CateID" name="Product_CateID" value="<%=Model.Product_CateID%>" />

                        <div class="btn-group">
                            <button type="button" class="btn btn-info" id="NodeText"><%=Model.Product_CateName==null?"请选择":Model.Product_CateName %></button>
                            <button type="button" class="btn btn-info dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                <span class="caret"></span>
                                <span class="sr-only">Toggle Dropdown</span>
                            </button>
                            <div class="dropdown-menu" style="min-width: 240px">
                                <div id="Categorys" style="max-height: 240px; overflow: auto;"></div>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-2"></div>
                </div>
                <div class="form-group">
                    <label for="Product_Name" class="col-sm-2 control-label">名称</label>
                    <div class="col-sm-8">
                        <input type="text" class="form-control" placeholder="名称" id="Product_Name" name="Product_Name" value="<%=Model.Product_Name %>">
                    </div>
                </div>
                <div class="form-group">
                    <label for="Product_Code" class="col-sm-2 control-label">编码</label>
                    <div class="col-sm-8">
                        <input type="text" class="form-control" placeholder="编码" id="Product_Code" name="Product_Code" value="<%=Model.Product_Code %>">
                    </div>
                </div>
                <div class="form-group">
                    <label for="Product_Color" class="col-sm-2 control-label">颜色</label>
                    <div class="col-sm-8">
                        <input type="text" class="form-control" placeholder="颜色" id="Product_Color" name="Product_Color" value="<%=Model.Product_Color %>">
                    </div>
                </div>
                <div class="form-group">
                    <label for="Product_Grade" class="col-sm-2 control-label">等级</label>
                    <div class="col-sm-8">
                        <input type="text" class="form-control" placeholder="等级" id="Product_Grade" name="Product_Grade" value="<%=Model.Product_Grade %>">
                    </div>
                </div>
                <div class="form-group">
                    <label for="Product_Pic" class="col-sm-2 control-label">产品图片</label>
                    <div class="col-sm-5">
                        <div class="input-group">
                            <input type="text" class="form-control" name="Product_Pic" id="Product_Pic" value="<%=Model.Product_Pic %>" placeholder="点击右侧按钮上传图片">
                            <div class="input-group-addon">
                                <a href="javascript:void(0)" class="glyphicon glyphicon-folder-open UploadImg" data-pic="Product_Pic"></a>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-5">
                        <img id="Product_Pic_Pre" width="30" height="30" src="<%=FCK.Common.Utility.ImgServer %><%=string.IsNullOrEmpty(Model.Product_Pic)?"/Content/images/blank.png": Model.Product_Pic%>" />
                    </div>
                </div>
                <div class="form-group">
                    <label for="Product_Intro" class="col-sm-2 control-label">概述</label>
                    <div class="col-sm-8">
                        <textarea class="form-control" id="Product_Intro" name="Product_Intro"><%=Model.Product_Intro %></textarea>
                    </div>
                </div>
                <div class="form-group">
                    <label for="Product_Order" class="col-sm-2 control-label">排序</label>
                    <div class="col-sm-8">
                        <input type="number" class="form-control" id="Product_Order" name="Product_Order" onkeyup="value=value.replace(/[^\d.]/g,'')" value="<%=Model.Product_Order %>">
                    </div>
                </div>
                <div class="form-group">
                    <label for="Product_Price" class="col-sm-2 control-label">价格</label>
                    <div class="col-sm-4">
                        <input type="text" class="form-control" placeholder="价格" id="Product_Price" name="Product_Price" onkeyup="value=value.replace(/[^\d.]/g,'')" value="<%=Model.Product_Price.ToString("0.00") %>">
                    </div>
                    <div class="col-sm-4">
                        <input type="checkbox" name="IsOnSale" value="1" <%=Model.IsOnSale==1?"checked":"" %>>
                        是否上架
                    </div>
                </div>
                <div class="form-group">
                    <label for="Product_Order" class="col-sm-2 control-label">分享佣金</label>
                    <div class="col-sm-8">
                        <input type="number" class="form-control" id="Share_Money" name="Share_Money" onkeyup="value=value.replace(/[^\d.]/g,'')" value="<%=Model.Share_Money.ToString("0.00") %>">
                    </div>
                </div>
                <div class="form-group">
                    <label for="Product_Order" class="col-sm-2 control-label">内容</label>
                    <div class="col-sm-8">
                        <textarea class="form-control" id="Product_Content" name="Product_Content"><%=Model.Product_Content %></textarea>
                    </div>
                </div>
                <div class="form-group">
                    <label for="Product_Order" class="col-sm-2 control-label">关键词</label>
                    <div class="col-sm-8">
                        <input type="text" class="form-control" id="Product_Keywords" name="Product_Keywords" value="<%=Model.Product_Keywords %>">
                    </div>
                </div>
                <div class="form-group">
                    <label for="Product_Order" class="col-sm-2 control-label">页面描述</label>
                    <div class="col-sm-8">
                        <textarea class="form-control" id="Product_Description" name="Product_Description"><%=Model.Product_Description %></textarea>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-offset-2 col-sm-5">
                        <input type="hidden" id="Product_ID" name="Product_ID" value="<%=Model.Product_ID %>" />
                        <a href="/Products/" class="btn btn-default">返回</a>
                        <button type="reset" class="btn btn-default">重设</button>
                    </div>
                    <div class="col-sm-3 text-right">
                        <input type="hidden" name="Register_ID" value="<%=FCK.Common.Utility.GetSessionValue("RegisterId") %>" />
                        <input type="hidden" name="Product_Sales" value="<%=Model.Product_Sales %>" />
                        <input type="hidden" name="IsRec" value="<%=Model.IsRec %>" />
                        <button type="submit" class="btn btn-success" id="Btn_Submit">确定</button>
                    </div>
                </div>
            </form>
        </div>
    </div>
</asp:Content>
