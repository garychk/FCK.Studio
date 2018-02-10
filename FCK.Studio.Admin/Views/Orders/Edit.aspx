<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Edit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <ol class="breadcrumb">
        <li><a href="/Home/Main"><i class="glyphicon glyphicon-home"></i>控制台</a></li>
        <li><a href="/Orders/">订单管理</a></li>
        <li class="active">下单</li>
    </ol>
    <h3><i class="glyphicon glyphicon-shopping-cart"></i>下单</h3>
    <hr>

    


</asp:Content>
