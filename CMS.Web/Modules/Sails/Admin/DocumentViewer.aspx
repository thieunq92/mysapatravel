<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DocumentViewer.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.DocumentViewer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Document View</title>
    <style>
        iframe {
            display: block; /* iframes are inline by default */
            background: #000;
            border: none; /* Reset default border */
            height: 100vh; /* Viewport-relative units */
            width: 100vw;
        }
    </style>
</head>
<body style="margin: 0px; padding: 0px; overflow: hidden">
    <form id="form1" runat="server">
        <div>
            <iframe visible="False" runat="server" id="iframeDoc" src='' allowfullscreen webkitallowfullscreen frameborder='0' width='724' height='100%'>This is an embedded <a target='_blank' href='http://office.com'>Microsoft Office</a> document, powered by <a target='_blank' href='http://office.com/webapps'>Office Online</a>.</iframe>
            <iframe visible="False" runat="server" id="iframePdf" src='' allowfullscreen webkitallowfullscreen frameborder='0' width='724' height='100%'></iframe>
        </div>
    </form>
</body>
</html>
