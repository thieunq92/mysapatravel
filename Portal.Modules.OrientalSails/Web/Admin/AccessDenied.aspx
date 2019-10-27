<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccessDenied.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.AccessDenied"
    MasterPageFile="MO-NoScriptManager.Master" %>

<asp:Content runat="server" ContentPlaceHolderID="Head">
    <style>
        footer {
            margin-top: 0px;
        }
    </style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="AdminContent">
    <iframe src="AccessDenied.html" frameborder="0" style="position: relative; top: -23px;" width="100%"
        height="600px"></iframe>
</asp:Content>
