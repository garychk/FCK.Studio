$(document).ready(function () {
    $(".entry-content pre").attr("class", "");
    $(".entry-content pre").addClass("prettyprint linenums");
    prettyPrint();

    $("#FormComment").validate({
        rules: {
            Member_ID: {
                required: true
            },
            Comment_Contents: {
                required: true,
                minlength: 12
            },
            vcode: {
                remote: {
                    url: "/Base/CheckVcode",
                    type: "post",
                    dataType: "json",
                    data: {
                        code: function () {
                            return $("#vcode").val();
                        }
                    }
                }
            }
        },
        messages: {
            Member_ID: {
                required: "请先登录"
            },
            Comment_Contents: {
                required: "请至少评论12个字符"
            },
            vcode: {
                required: "验证码不能空",
                remote: "验证码不正确"
            }
        },
        submitHandler: function (form) {
            var MemberId = parseInt($("#Member_ID").val());
            if (MemberId > 0) {
                var options = {
                    url: '/Article/AddComment',
                    type: 'POST',
                    success: function (response) {
                        if (response.code == 100)
                            toast("message", "info", "评论成功！");
                        else
                            toast("message", "error", response.message);
                    }
                };
                $(form).ajaxSubmit(options);
            } else {
                toast("message", "info", "登录后才能评论哦！");
            }
        }
    });

    $("#FormCollection").validate({
        rules: {
            Member_ID: {
                required: true
            },
            Comment_Contents: {
                required: true,
                minlength: 12
            }
        },
        messages: {
            Member_ID: {
                required: "请先登录"
            },
            Comment_Contents: {
                required: "请至少评论12个字符"
            }
        },
        submitHandler: function (form) {
            var MemberId = parseInt($("#Member_ID").val());
            if (MemberId > 0) {
                var options = {
                    url: '/Members/AddCollect',
                    type: 'POST',
                    success: function (response) {
                        if (response.code == 100)
                            toast("message", "info", "收藏成功！");
                        else if (response.code == 101)
                            toast("message", "info", "您已经收藏过了哦！");
                        else
                            alertE(response.message);
                    }
                };
                $(form).ajaxSubmit(options);
            } else {
                alertE("登录后才能收藏哦！");
            }
        }
    });
});

function CheckDownload(userP, resourceP) {
    var MemberId = parseInt($("#Member_ID").val());
    if (MemberId > 0) {
        if (userP >= resourceP)
            $("#FormDownload").submit();
        else {
            alertE("您的M币不够哦！");
        }
    } else {
        alertE("登录后才能下载哦！");
    }
}

function Ok(_id, _step) {
    $.post("/Article/Ok", {
        id: _id,
        step: _step
    }, function (response) {
        if (response.code == 100)
            toast("message", "info", "OK！");
        else
            alertE(response.message);
    });
}