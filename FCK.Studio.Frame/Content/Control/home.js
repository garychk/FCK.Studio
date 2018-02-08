$(function () {
    $("#ButtonLogin").click(function () {
        var member = new Member("ButtonLogin", "SubmitForm", "/Home/Dashboard");
        member.Login();
    });
});

