$(document).ready(function () {
    $("#FormEditMember").validate({
        rules: {
            Member_NickName: {
                required: true,
                remote: {
                    url: "/Members/CheckExist",
                    type: "post",
                    dataType: "json",
                    data: {
                        val: function () {
                            return $("#Member_NickName").val();
                        }
                    }
                }
            },
            Member_Name: {
                isChinese: true
            },
            Member_Telphone: {
                isTel: true,
                remote: {
                    url: "/Members/CheckExist",
                    type: "post",
                    dataType: "json",
                    data: {
                        val: function () {
                            return $("#Member_Telphone").val();
                        }
                    }
                }
            },
            Member_QQ: {
                isQq: true,
                remote: {
                    url: "/Members/CheckExist",
                    type: "post",
                    dataType: "json",
                    data: {
                        val: function () {
                            return $("#Member_QQ").val();
                        }
                    }
                }
            },
            Member_Email: {
                remote: {
                    url: "/Members/CheckExist",
                    type: "post",
                    dataType: "json",
                    data: {
                        val: function () {
                            return $("#Member_Email").val();
                        }
                    }
                }
            }
        },
        messages: {
            Member_NickName: {
                required: "昵称不能空",
                remote: "昵称已存在"
            },
            Member_Name: {
                isChinese: "姓名格式不正确"
            },
            Member_Telphone: {
                isTel: "电话号码格式不正确",
                remote: "电话号码已存在"
            },
            Member_QQ: {
                isInteger: "QQ号码格式不正确",
                remote: "QQ号码已存在"
            },
            Member_Email: {
                remote: "邮箱已存在"
            }
        },
        submitHandler: function (form) {
            var options = {
                url: '/Members/EditMember',
                type: 'POST',
                success: function (response) {
                    if (response.code == 100)
                        toast("提示","info","个人资料修改成功！");
                    else
                        toast("提示", "error", response.message);
                }
            };
            $(form).ajaxSubmit(options);
        }
    });

    $("#FormResetPsw").validate({
        rules: {
            newPassword: {
                required: true,
                minlength: 5
            },
        },
        messages: {
            newPassword: {
                required: "密码不能空",
                minlength: "密码长度不能小于 5 个字符"
            }
        },
        submitHandler: function (form) {
            var options = {
                url: '/Members/ResetPassword',
                type: 'POST',
                success: function (response) {
                    if (response.code == 100)
                        alertE("密码重设成功！");
                    else
                        alertE(response.message);
                }
            };
            $(form).ajaxSubmit(options);
        }
    });

    $("#FormEditArticle").validate({
        ignore:"",
        rules: {
            Article_CateID: {
                required: true,
                isIntGtZero: true
            },
            Article_Title: {
                required: true
            },
            Article_Contents: {
                required: true
            }
        },
        messages: {
            Article_CateID: {
                required: "请选择分类",
                isIntGtZero: "请选择分类"
            },
            Article_Title: {
                required: "请填写文章标题"
            },
            Article_Contents: {
                required: "请填写文章内容"
            }
        },
        submitHandler: function (form) {
            var contents = escape($("#Article_Contents").val());
            $("#Article_Contents").val(contents);
            var options = {
                url: '/Members/EditArticle',
                type: 'POST',
                success: function (response) {
                    if (response.code == 100)
                        alertE("编辑成功！");
                    else
                        alertE(response.message);
                }
            };
            $(form).ajaxSubmit(options);
        }
    });

    $("#FormEditResource").validate({
        rules: {
            Article_CateID: {
                required: true,
                isIntGtZero: true
            },
            Article_Title: {
                required: true
            },
            Article_Contents: {
                required: true
            },
            Article_NavUrl: {
                required: true
            },
            Article_Point: {
                required: true,
                isInteger: true
            }
        },
        messages: {
            Article_CateID: {
                required: "请选择分类",
                isIntGtZero: "请选择分类"
            },
            Article_Title: {
                required: "请填写标题"
            },
            Article_Contents: {
                required: "请填写描述"
            },
            Article_NavUrl: {
                required: "资源下载链接不能空"
            },
            Article_Point: {
                required: "资源分必须填",
                isInteger: "资源分必须是整数"
            }
        },
        submitHandler: function (form) {
            var cateid = parseInt($("#Article_CateID").val());
            if (cateid <= 0) {
                toast("提示", "error", "请选择分类！");
            }
            else {
                var contents = escape($("#Article_Contents").val());
                $("#Article_Contents").val(contents);
                var options = {
                    url: '/Members/EditArticle',
                    type: 'POST',
                    success: function (response) {
                        if (response.code == 100)
                            alertE("编辑成功！");
                        else
                            alertE(response.message);
                    }
                };
                $(form).ajaxSubmit(options);
            }
        }
    });

    $("#DataList").on("click", "#SelAll", function () {
        if ($(this).prop("checked")) {
            $("#DataList input[type=checkbox]").prop("checked", true);
        } else
            $("#DataList input[type=checkbox]").prop("checked", false);
    });

    $("#pagination").on("click", " li a", function () {
        var page = parseInt($(this).attr("href"));
        if (page > 0) {
            $(this).parent().removeClass("active");
            $(this).parent().addClass("active");
            $("#currentpage").val(page);
            LoadDatas();
        }
        return false;
    });

    
    $.post('/Article/GetCateTree?ctype=blog', {}, function (response) {
        var initSelectableTree = function () {
            return $('#ArticleCategory').treeview({
                levels: 9,
                data: response[0].nodes,
                onNodeSelected: function (event, node) {
                    $("#NodeText").text(node.text);
                    $("#Article_CateID").val(node.id);
                },
                onNodeUnselected: function (event, node) { }
            });
        }

        var $selectableTree = initSelectableTree();

    });

    $.post('/Article/GetCateTree?ctype=resource', {}, function (response) {
        var initSelectableTree = function () {
            return $('#ResourceCategory').treeview({
                levels: 9,
                data: response[0].nodes,
                onNodeSelected: function (event, node) {
                    $("#NodeText").text(node.text);
                    $("#Article_CateID").val(node.id);
                },
                onNodeUnselected: function (event, node) { }
            });
        }

        var $selectableTree = initSelectableTree();

    });

    if ($("#DataList").length > 0)
    {
        LoadDatas();
    }

    $("#UpdateUserHeader").click(function () {
        $.ajax({
            type: "POST",
            url: "/Members/UpdateUserHeader",
            data: { id: $(this).attr("href") },
            success: function (result) {
                if (result.code == 100) {
                    toast("message", "info", "更新成功！");
                    $("#UserHeader").attr("src", result.message);
                }
                else
                    alertE(result.message);
            }
        });
        return false;
    });
});

function getVal(vals, obj) {
    var _ids = vals.split(',');
    for (var i = 0; i < _ids.length; i++) {
        var node = obj.tree('find', _ids[i]);
        if (node != null) {
            obj.tree('check', node.target);
            obj.tree('select', node.target);
        }
    }
}

function Pagination(currentPage, pages, total) {
    var html2 = "";
    if (pages > 1) {
        if (pages <= 10) {
            for (var j = 1; j <= pages; j++) {
                if (j == currentPage)
                    html2 += '<li class="active"><a href="0">' + j + '</a></li>';
                else
                    html2 += '<li><a href="' + j + '">' + j + '</a></li>';
            }
        }
        else {
            var prePage = parseInt(currentPage) - 1; prePage = prePage < 1 ? 1 : prePage;
            var nextPage = parseInt(currentPage) + 1; nextPage = nextPage > pages ? pages : nextPage;

            var startPage = parseInt(currentPage / 5) * 5; startPage = startPage <= 0 ? currentPage : startPage;
            var endPage = parseInt(startPage) + 5; endPage = endPage > pages ? pages : endPage;


            if (currentPage > 1) {
                html2 += '<li><a href="' + prePage + '">&laquo;</a></li>';
                html2 += '<li><a href="1">1</a></li>';
            }
            if (currentPage - 1 >= 2)
                html2 += '<li><a href="0">...</a></li>';
            for (var j = startPage; j <= endPage; j++) {
                if (j == currentPage)
                    html2 += '<li class="active"><a href="0">' + j + '</a></li>';
                else
                    html2 += '<li><a href="' + j + '">' + j + '</a></li>';
            }
            if (currentPage != pages && endPage != pages) {
                html2 += '<li><a href="0">...</a></li>';
                html2 += '<li><a href="' + pages + '">' + pages + '</a></li>';
                html2 += '<li><a href="' + nextPage + '">&raquo;</a></li>';
            }

        }
    }
    return html2;
}

function GoSearch() {
    $("#currentpage").val(1);
    LoadDatas();
}

function LoadDatas() {
    var actionUrl = $("#actionurl").val();
    var editUrl = $("#editurl").val();
    var _keywords = $("#keywords").val();
    var _cateid = $("#CateId").val();
    var _memberid = $("#UserId").val();
    var _page = parseInt($("#currentpage").val());
    $("#DataList").html('<center><img src="/Content/images/loading.gif" /></center>');
    $.ajax({
        type: "POST",
        url: actionUrl,
        data: {
            page: _page,
            pageSize: 10,
            cateid: _cateid,
            keywords: _keywords,
            memberid: _memberid
        },
        success: function (result) {
            var items = result.datas;
            var pages = result.pages;
            var total = result.total;
            if (actionUrl.indexOf("Article") > 0) 
                ArticleTemplate(items, editUrl);
            else if(actionUrl.indexOf("Order")>0)
                OrdersTemplate(items, editUrl);
            else if (actionUrl.indexOf("Collect") > 0)
                CollectTemplate(items, editUrl);
            else
                CommentTemplate(items);
            $("#pagination").html(Pagination(_page, pages, total));
        }
    });
    return false;
}
function ArticleTemplate(items, editUrl) {
    var htmls = '';
    htmls += '<tr>';
    htmls += '<td><input type="checkbox" id="SelAll"> 全选</td>';
    htmls += '<td>序号</td>';
    htmls += '<td>标题</td>';
    htmls += '<td>图片</td>';
    htmls += '<td>分类</td>';
    htmls += '<td>操作</td>';
    htmls += '</tr>';
    for (var i = 0; i < items.length; i++) {
        htmls += '<tr>';
        htmls += '<td><input type="checkbox" name="items" value="' + items[i].Article_ID + '"></td>';
        htmls += '<td>' + items[i].Article_OrderID + '</td>';
        htmls += '<td>' + items[i].Article_Title + '</td>';
        htmls += '<td><img src="' + imgServer + items[i].Article_Pic + '" height="30"></td>';
        htmls += '<td>' + items[i].Article_CateName + '</td>';
        htmls += '<td><a href="' + editUrl + items[i].Article_ID + '" class="btn btn-info">编辑</a><button type="button" class="btn btn-danger" onclick="Del(' + items[i].Article_ID + ')">删除</button></td>';
        htmls += '</tr>';
    }
    $("#DataList").html(htmls);
}

function CommentTemplate(items) {
    var htmls = '';
    htmls += '<tr>';
    htmls += '<td><input type="checkbox" id="SelAll"> 全选</td>';
    htmls += '<td>文章标题</td>';
    htmls += '<td>评论时间</td>';
    htmls += '<td>操作</td>';
    htmls += '</tr>';
    for (var i = 0; i < items.length; i++) {
        htmls += '<tr>';
        htmls += '<td><input type="checkbox" name="items" value="' + items[i].Comment_ID + '"></td>';
        htmls += '<td><a href="/Article/Detail/' + items[i].Article_ID + '" target="_blank">' + items[i].Article_Title + '</td>';
        htmls += '<td>' + ConverTime(items[i].Comment_Times,"yyyy-MM-dd hh:mm:ss") + '</td>';
        htmls += '<td><button type="button" class="btn btn-danger" onclick="DelComments(' + items[i].Comment_ID + ')">删除</button></td>';
        htmls += '</tr>';
    }
    $("#DataList").html(htmls);
}

function OrdersTemplate(items) {
    var htmls = '';
    htmls += '<tr>';
    htmls += '<td><input type="checkbox" id="SelAll"> 全选</td>';
    htmls += '<td>订单号</td>';
    htmls += '<td>下单时间</td>';
    htmls += '<td>金额</td>';
    htmls += '<td>状态</td>';
    htmls += '<td>操作</td>';
    htmls += '</tr>';
    for (var i = 0; i < items.length; i++) {
        htmls += '<tr>';
        htmls += '<td><input type="checkbox" name="items" value="' + items[i].Comment_ID + '"></td>';
        htmls += '<td><a href="javascript:void(0)" onclick=OpenDetail(\'/Orders/ODList/' + items[i].Order_Number + '\')>' + items[i].Order_Number + '</a></td>';
        htmls += '<td>' + items[i].Order_Time + '</td>';        htmls += '<td class="colorf00">$ ' + items[i].Order_Amount + '</td>';        htmls += '<td>' + OrderStatus(items[i].Order_Status) + '</td>';
        htmls += '<td><a href="javascript:void(0)" class="btn btn-info" onclick=OpenDetail(\'/Orders/ODList/' + items[i].Order_Number + '\')>详细</a></td>';
        htmls += '</tr>';
    }
    $("#DataList").html(htmls);
}

function CollectTemplate(items) {
    var htmls = '';
    htmls += '<tr>';
    htmls += '<td><input type="checkbox" id="SelAll"> 全选</td>';
    htmls += '<td>图片</td>';
    htmls += '<td>文章标题</td>';
    htmls += '<td>收藏时间</td>';
    htmls += '<td>操作</td>';
    htmls += '</tr>';
    for (var i = 0; i < items.length; i++) {
        htmls += '<tr>';
        htmls += '<td><input type="checkbox" name="items" value="' + items[i].Collection_ID + '"></td>';
        htmls += '<td><a href="/Resources/Detail/' + items[i].Article_ID + '" target="_blank"><img src="' + imgServer + items[i].Article_Pic + '" height="30" /></td>';
        htmls += '<td><a href="/Resources/Detail/' + items[i].Article_ID + '" target="_blank">' + items[i].Article_Title + '</td>';
        htmls += '<td>' + ConverTime(items[i].Collection_Time, "yyyy-MM-dd hh:mm:ss") + '</td>';
        htmls += '<td><button type="button" class="btn btn-danger" onclick="DelCollection(' + items[i].Collection_ID + ')">删除</button></td>';
        htmls += '</tr>';
    }
    $("#DataList").html(htmls);
}

function Del(idstr) {
    confirmE("确定删除？", function () {
        $.ajax({
            type: "POST",
            url: "/Members/DelArticle",
            data: { ids: idstr },
            success: function (result) {
                if (result.code == 100)
                    LoadDatas();
                else
                    alertE(result.message);
            }
        });
    });
}
function DelPacth() {
    confirmE("确定删除？", function () {
        var ids = "";
        $("#DataList input[name=items]").each(function () {
            if ($(this).prop("checked")) {
                if (ids == "")
                    ids += $(this).val();
                else
                    ids += "," + $(this).val();
            }
        });
        if (ids == "")
            alertE("至少选择一项！");
        else
            Del(ids);
    });
}

function DelComments(idstr) {
    confirmE("确定删除？", function () {
        $.ajax({
            type: "POST",
            url: "/Members/DelComments",
            data: { ids: idstr },
            success: function (result) {
                if (result.code == 100)
                    LoadDatas();
                else
                    alertE(result.message);
            }
        });
    });
}
function DelCommentPacth() {
    confirmE("确定删除？", function () {
        var ids = "";
        $("#DataList input[name=items]").each(function () {
            if ($(this).prop("checked")) {
                if (ids == "")
                    ids += $(this).val();
                else
                    ids += "," + $(this).val();
            }
        });
        if (ids == "")
            alertE("至少选择一项！");
        else
            DelComments(ids);
    });
}

function DelCollection(idstr) {
    confirmE("确定删除？", function () {
        $.ajax({
            type: "POST",
            url: "/Members/DelCollection",
            data: { ids: idstr },
            success: function (result) {
                if (result.code == 100)
                    LoadDatas();
                else
                    alertE(result.message);
            }
        });
    });
}
function DelCollectionPacth() {
    confirmE("确定删除？", function () {
        var ids = "";
        $("#DataList input[name=items]").each(function () {
            if ($(this).prop("checked")) {
                if (ids == "")
                    ids += $(this).val();
                else
                    ids += "," + $(this).val();
            }
        });
        if (ids == "")
            alertE("至少选择一项！");
        else
            DelCollection(ids);
    });
}

function OpenDetail(href) {
    OpenWin('订单详情', href, 'col-md-8 col-md-offset-2');
    return false;
}

var editorItems = [
		'source', '|', 'undo', 'redo', '|', 'preview', 'print', 'template', 'code', 'cut', 'copy', 'paste',
		'plainpaste', 'wordpaste', '|', 'justifyleft', 'justifycenter', 'justifyright',
		'justifyfull', 'insertorderedlist', 'insertunorderedlist', 'indent', 'outdent', 'subscript',
		'superscript', 'clearhtml', 'quickformat', 'selectall', '|', 'fullscreen', '/',
		'formatblock', 'fontname', 'fontsize', '|', 'forecolor', 'hilitecolor', 'bold',
		'italic', 'underline', 'strikethrough', 'lineheight', 'removeformat', '|', 'image', 'multiimage',
		'flash', 'media', 'insertfile', 'table', 'hr', 'emoticons', 'baidumap', 'pagebreak',
		'anchor', 'link', 'unlink', '|', 'about'
]

var editorNoUpload = [
		'source', '|', 'undo', 'redo', '|', 'preview', 'print', 'template', 'code', 'cut', 'copy', 'paste',
		'plainpaste', 'wordpaste', '|', 'justifyleft', 'justifycenter', 'justifyright',
		'justifyfull', 'insertorderedlist', 'insertunorderedlist', 'indent', 'outdent', 'subscript',
		'superscript', 'clearhtml', 'quickformat', 'selectall', '|', 'fullscreen', '/',
		'formatblock', 'fontname', 'fontsize', '|', 'forecolor', 'hilitecolor', 'bold',
		'italic', 'underline', 'strikethrough', 'lineheight', 'removeformat', '|', 'insertfile', 'table', 'hr', 'emoticons', 'baidumap', 'pagebreak',
		'anchor', 'link', 'unlink', '|', 'about'
]

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
                    K('#' + obj_pic + "_Pre").attr('src', imgServer + url);
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
        items: editorNoUpload,
        afterCreate: function () {
            this.sync();
        },
        afterBlur: function () {
            this.sync();
        }
    });

    var editorFile = K.editor({
        allowFileManager: false,
        uploadJson: '/Content/kindeditor/asp.net/upload_json.ashx'
    });

    K('.UploadFile').click(function () {
        var obj_url = $(this).attr("data-url");
        editorFile.loadPlugin('insertfile', function () {
            editorFile.plugin.fileDialog({
                fileUrl: K('#' + obj_url).val(),
                clickFn: function (url, title) {
                    K('#' + obj_url).val(url);
                    editorFile.hideDialog();
                }
            });
        });
    });
});