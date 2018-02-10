$(document).ready(function () {
    $("#FormLogin").validate({
        messages: {
            UserName: {
                required: "请输入用户名或注册邮箱"
            },
            Password: {
                required: "密码不能空"
            }
        },
        submitHandler: function (form) {
            var options = {
                url: '/Base/UserLogin',
                type: 'POST',
                success: function (response) {
                    if (response.code == 100) {
                        confirmE('登录成功，立即前往会员中心?', function () {
                            window.location.href = "/Members/";
                        });
                    }
                    else
                        alertE(response.message);
                }
            };
            $(form).ajaxSubmit(options);
        }
    });

    $("#FormRegist").validate({
        rules: {
            Member_UserName: {
                required: true,
                minlength: 4
            },
            Member_Password: {
                required: true,
                minlength: 5
            },
            Confirm_Password: {
                required: true,
                minlength: 5,
                equalTo: "#Member_Password"
            },
            Member_Email: {
                required: true
            },
            Member_Mobile: {
                required: true,
                isMobile: true
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
            Member_UserName: {
                required: "请输入用户名"
            },
            Member_Password: {
                required: "密码不能空"
            },
            Confirm_Password: {
                required: "请输入密码",
                minlength: "密码长度不能小于 5 个字符",
                equalTo: "两次密码输入不一致"
            },
            Member_Email: {
                required: "邮箱不能空"
            },
            Member_Mobile: {
                required: "手机号不能空"
            },
            vcode: {
                required: "验证码不能空",
                remote: "验证码不正确"
            },
            iAgreeLaw: {
                required: "请勾选同意服务条款"
            }
        },
        submitHandler: function (form) {
            var options = {
                url: '/Base/UserRegist',
                type: 'POST',
                success: function (response) {
                    if (response.code == 100) {
                        confirmE('注册成功，立即登录?', function () {
                            window.location.href = "/Home/Login";
                        });
                    }
                    else
                        alertE(response.message);
                }
            };
            $(form).ajaxSubmit(options);
        }
    });
});