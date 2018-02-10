<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<style>
    small {
        font-size: 11px;
        color: #999;
    }
</style>
<script>
    var _logid = '<%=ViewData["LogID"]%>';
    $(function () {        
        $.ajax({
            type: "POST",
            url: "/Home/GetLogDetail",
            data: {
                logid: _logid,
            },
            success: function (result) {
                $("#Log_Content").html(result.Log_Content);
                //$("#Log_Type").html(result.Log_Type);
                $("#Log_Time").html(result.Log_Time);
            }
        });
    });
    function DelLog()
    {

    }
</script>
<hr />
<div>
    <p id="Log_Content"></p>
</div>
<hr />
<div class="row">
    <div class="col-md-6"><small id="Log_Time">This line of text is meant to be treated as fine print.</small></div>
    <div class="col-md-6 text-right"><a href="javascript:DelLog()" title="删除"><span class="iconfont icon-delete"></span></a></div>
</div>
