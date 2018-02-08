$(document).ready(function () {
    $(".container").css("width", "100%");
    resize();
    $("#member_menu a").click(function () {
        var _url = $(this).attr("href");
        var li = $(this).parent();
        $("#member_menu li").removeClass("active");
        li.addClass("active");
    });   
    
});

function resize() {
    var pageWidth = $(window).width();
    var pageHeight = $(window).height();
    var headerHeight = 84;
    var footerHeight = 10;
    var RtHeight = pageHeight - headerHeight - footerHeight;
    $("#member_left,#member_right").height(RtHeight);
    $("#mainframe").height(RtHeight);
}