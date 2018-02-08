<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FCK.Studio.Dto.ArticleDto>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Edit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        KindEditor.ready(function (K) {
            var editor = K.editor({
                allowFileManager: true,
                formatUploadUrl: false,
                fileManagerJson: '/Content/kindeditor/asp.net/file_manager_json.ashx',
                uploadJson: '/Content/kindeditor/asp.net/ftpupload_json.ashx'
            });
            K('.UploadImg').click(function () {
                var obj_pic = $(this).attr("data-pic");
                editor.loadPlugin('image', function () {
                    editor.plugin.imageDialog({
                        imageUrl: K('#' + obj_pic).val(),
                        clickFn: function (url, title, width, height, border, align) {
                            K('#' + obj_pic).val(url);
                            K('#' + obj_pic + "_Pre").attr('src', imageServer + url);
                            editor.hideDialog();
                        }
                    });
                });
            });

            K.create('#Article_Contents', {
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
        });        $(function () {
            $.post('/Category/GetCateTree', {}, function (response) {
                var initSelectableTree = function () {
                    return $('#ArticleCategory').treeview({
                        levels: 9,
                        data: response,
                        onNodeSelected: function (event, node) {
                            $("#NodeText").text(node.text);
                            $("#Article_CateID").val(node.id);
                        },
                        onNodeUnselected: function (event, node) { }
                    });
                }
                //选中
                var $selectableTree = initSelectableTree();
                //var selectNode = $selectableTree.treeview('search', [$('#Article_CateName').val(), { ignoreCase: false, exactMatch: false }]);
                //$selectableTree.treeview('selectNode', [selectNode, { silent: true }]);

            });            $("#Btn_Submit").click(function () {
                if ($("#Article_CateID").val() == "0") {
                    alertE("请选择分类！"); return false;
                }
                if ($("#Article_Title").val() == "") {
                    alertE("请输入文章标题！"); return false;
                }
                var contents = escape($("#Article_Contents").val());
                if (contents == "") {
                    alertE("文章内容不能为空！"); return false;
                }
                else {
                    $("#Article_Contents").val(contents);
                }
                var options = {
                    url: '/Articles/EditArticle',
                    type: 'post',
                    success: function (result) {
                        if (result.code == 100) {
                            confirmE("编辑成功，是否返回列表？", function () {
                                window.location = '/Articles/Index';
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
        })        function OpenCates() {
            OpenWin('分组编辑', '/Category/Edit/0', 'col-md-6 col-md-offset-3');
        }        function getVal(vals) {
            var _ids = vals.split(',');
            for (var i = 0; i < _ids.length; i++) {
                var node = $('#ArticleCategory').tree('find', _ids[i]);
                if (node != null) {
                    $('#ArticleCategory').tree('check', node.target);
                    $('#ArticleCategory').tree('select', node.target);
                }
            }
        }
    </script>
    <ol class="breadcrumb">
        <li><a href="/Home/Main"><i class="iconfont icon-home"></i>控制台</a></li>
        <li><a href="/Articles/">文章管理</a></li>
        <li class="active">文章编辑</li>
    </ol>
    <div class="panel panel-default">
        <div class="container">
            <h3><i class="iconfont icon-edit"></i>文章编辑</h3>
            <hr>
            <div class="row">
                <div class="col-xs-10">
                    <form class="form-horizontal" name="SubmitForm" id="SubmitForm">
                        <div class="form-group">
                            <label for="Article_CateID" class="col-sm-2 control-label">文章分类</label>
                            <div class="col-sm-6">
                                <input type="hidden" id="Article_CateName" value="<%=Model.Article_CateName %>">
                                <input type="hidden" id="Article_CateID" name="Article_CateID" value="<%=Model.Article_CateID%>" />
                                
                                <div class="btn-group">
                                    <button type="button" class="btn btn-info" id="NodeText"><%=Model.Article_CateName==null?"请选择":Model.Article_CateName %></button>
                                    <button type="button" class="btn btn-info dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                        <span class="caret"></span>
                                        <span class="sr-only">Toggle Dropdown</span>
                                    </button>
                                    <div class="dropdown-menu" style="min-width:240px">
                                        <div id="ArticleCategory" style="max-height:240px;overflow-y:auto"></div>
                                    </div>
                                </div>

                            </div>
                            <div class="col-sm-2"></div>
                        </div>
                        <div class="form-group">
                            <label for="Article_Title" class="col-sm-2 control-label">文章标题</label>
                            <div class="col-sm-6">
                                <input type="text" class="form-control" placeholder="文章标题" id="Article_Title" name="Article_Title" value="<%=Model.Article_Title %>">
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="Article_Brief" class="col-sm-2 control-label">文章简介</label>
                            <div class="col-sm-6">
                                <textarea class="form-control" placeholder="文章简介" id="Article_Brief" name="Article_Brief"><%=Model.Article_Brief %></textarea>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="Product_Pic" class="col-sm-2 control-label">产品图片</label>
                            <div class="col-sm-6">
                                <div class="input-group">
                                    <input type="text" class="form-control" name="Article_Pic" id="Article_Pic" value="<%=Model.Article_Pic %>" placeholder="点击右侧按钮上传图片">
                                    <div class="input-group-addon">
                                        <a href="javascript:void(0)" class="glyphicon glyphicon-folder-open UploadImg" data-pic="Article_Pic"></a>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <img id="Article_Pic_Pre" width="45" height="45" src="<%=FCK.Common.Utility.ImgServer %><%=string.IsNullOrEmpty(Model.Article_Pic)?"/Content/images/blank.png": Model.Article_Pic%>" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="Article_Contents" class="col-sm-2 control-label">文章内容</label>
                            <div class="col-sm-10">
                                <textarea class="form-control" id="Article_Contents" name="Article_Contents"><%=Model.Article_Contents %></textarea>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="Article_Tag" class="col-sm-2 control-label">文章标签</label>
                            <div class="col-sm-6">
                                <input type="text" class="form-control" placeholder="文章标签" id="Article_Tag" name="Article_Tag" value="<%=Model.Article_Tag %>">
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="Article_OrderID" class="col-sm-2 control-label">序号</label>
                            <div class="col-sm-6">
                                <input type="text" class="form-control" placeholder="序号" id="Article_OrderID" name="Article_OrderID" value="<%=Model.Article_OrderID %>">
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="Article_Author" class="col-sm-2 control-label">作者</label>
                            <div class="col-sm-6">
                                <input type="text" class="form-control" placeholder="序号" id="Article_Author" name="Article_Author" value="<%=Model.Article_Author %>">
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="Article_NavUrl" class="col-sm-2 control-label">外部链接</label>
                            <div class="col-sm-6">
                                <input type="text" class="form-control" placeholder="http://" id="Article_NavUrl" name="Article_NavUrl" value="<%=Model.Article_NavUrl %>">
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="Article_NavUrl" class="col-sm-2 control-label">关键词</label>
                            <div class="col-sm-6">
                                <input type="text" class="form-control" id="Article_Keywords" name="Article_Keywords" value="<%=Model.Article_Keywords %>">
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="Article_NavUrl" class="col-sm-2 control-label">页面描述</label>
                            <div class="col-sm-6">
                                <textarea class="form-control" id="Article_Description" name="Article_Description"><%=Model.Article_Description %></textarea>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-offset-2 col-sm-10">
                                <input type="hidden" id="Article_ID" name="Article_ID" value="<%=Model.Article_ID %>" />
                                <input type="hidden" id="Article_Hits" name="Article_Hits" value="<%=Model.Article_Hits %>" />
                                <input type="hidden" id="Article_UpdateTime" name="Article_UpdateTime" value="<%=Model.Article_UpdateTime %>" />
                                <button type="submit" class="btn btn-success" id="Btn_Submit">确定</button>
                                <button type="reset" class="btn btn-info">重设</button>
                                <a href="/Articles/" class="btn btn-default">返回</a>
                            </div>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
