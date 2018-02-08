$(function () {
    $("#FormEmail").validate({
        rules: {
            inputEmail: {
                required: true
            },
            inputName: {
                required: true
            },
            inputContent: {
                required: true
            },
            vcode: {
                required: true,
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
            inputContent: {
                required: "留言内容不能为空哦"
            },
            inputEmail: {
                required: "至少给我们一个回复的机会吧"
            },
            inputName: {
                required: "请问尊姓大名呢"
            },
            vcode: {
                required: "验证码不能空",
                remote: "验证码不正确"
            }
        },
        submitHandler: function (form) {
            var options = {
                url: '/Base/SendEmail',
                type: 'POST',
                success: function (response) {
                    if (response.code == 100)
                        toast("提示", "info", "感谢您的留言，我们会第一时间回复您！");
                    else
                        toast("错误", "error", response.message);
                }
            };
            $(form).ajaxSubmit(options);
        }
    });
});