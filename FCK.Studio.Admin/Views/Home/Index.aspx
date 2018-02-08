<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    <title><%=ViewData["PageTitle"] %></title>
    <link href="/favicon.ico" rel="shortcut icon" type="image/x-icon" />
    <script src="/Content//js/jquery-2.1.1.min.js"></script>
    <script src="/Content//bootstrap/js/bootstrap.min.js"></script>
    <script src="/Content//js/jquery.sticky.min.js"></script>
    <script src="/Content//js/jquery-confirm.js"></script>
    <script src="/Content//js/Common.js"></script>
    <link href="/Content//js/jquery-confirm.css" rel="stylesheet" />
    <link href="/Content//bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="/Content//Style.css" rel="stylesheet" />
    <script>
        $(function () {
            document.onkeydown = function (e) {
                var ev = document.all ? window.event : e;
                if (ev.keyCode == 13) {
                    DoLogin();
                }
            }
            $("#Btn_login").click(function () {
                DoLogin();
                return false;
            });
        });

        function DoLogin() {
            var _username = $("#UserName").val();
            var _password = $("#Password").val();
            if (_username == "") {
                $("#UserName").focus(); return false;
            }
            if (_password == "") {
                $("#Password").focus(); return false;
            }
            $.ajax({
                type: "POST",
                url: "/Admin/DoLogin",
                data: { username: _username, password: _password },
                success: function (result) {
                    if (result.code == 100) {
                        window.location = "/Home/Dashboard";
                    }
                    else {
                        if ("PASSWORD_EROR" == result.message)
                            alertE("对不起，密码错误！");
                        else if("USER_LOCKED" == result.message)
                            alertE("用户已锁定，请联系管理员！");
                        else
                            alertE(result.message);
                    }
                }
            });
            return false;
        }

        function DoInit() {
            var _username = $("#InitUserName").val();
            var _password = $("#InitPassword").val();
            if (_username == "")
            {
                $("#InitUserName").focus(); return false;
            }
            if (_password == "") {
                $("#InitPassword").focus(); return false;
            }
            $.ajax({
                type: "POST",
                url: "/Admin/InitAdmin",
                data: { username: _username, password: _password },
                success: function (result) {
                    if (result.code == 100) {
                        window.location = "/Home/Index";
                    }
                    else {
                        alert(result.message);
                    }
                }
            });
            return false;
        }
    </script>
</head>
<body>
    <%if (ViewBag.AdminCount > 0)
      { %>
    <div class="loginpanel">
        <div class="login-panel panel panel-default">
            <div class="panel-heading">
                <h3 class="panel-title">登录</h3>
            </div>
            <div class="panel-body">
                <form role="form">
                    <div class="form-group">
                        <input class="form-control" placeholder="UserName" name="UserName" id="UserName" type="text" value="Admin">
                    </div>
                    <div class="form-group">
                        <input class="form-control" placeholder="Password" name="Password" id="Password" type="password" value="" autofocus="">
                    </div>
                    <div class="checkbox">
                        <label>
                            <input name="remember" type="checkbox" value="Remember Me">记住我
                        </label>
                    </div>
                    <button type="button" class="btn btn-lg btn-success btn-block" id="Btn_login">登录</button>
                </form>
            </div>
        </div>
    </div>
    <%}else{ %>
    <div class="loginpanel">
        <div class="login-panel panel panel-default">
            <div class="panel-heading">
                <h3 class="panel-title">初始化</h3>
            </div>
            <div class="panel-body">
                <form role="form">
                    <div class="form-group">
                        <input class="form-control" placeholder="请输入用户名" name="UserName" id="InitUserName" type="text">
                    </div>
                    <div class="form-group">
                        <input class="form-control" placeholder="请输入密码" name="Password" id="InitPassword" type="password" value="">
                    </div>
                    <button type="button" class="btn btn-lg btn-success btn-block" onclick="DoInit()">确定</button>
                </form>
            </div>
        </div>
    </div>
    <%} %>
</body>
</html>
