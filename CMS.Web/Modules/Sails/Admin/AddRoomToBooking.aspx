<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddRoomToBooking.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.AddRoomToBooking" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="btnAdd" runat="server" Text="Start" OnClick="btnAdd_OnClick"/>
        <asp:Button ID="btnGet" runat="server" Text="Get" OnClick="btnGet_OnClick"/>
        <asp:Button ID="btnAddRoom" runat="server" Text="Add room" OnClick="btnAddRoom_OnClick"/>
        <br/>
        <br/>
        <asp:Button ID="btnGetTotalNull" runat="server" Text="GetTotalNull" OnClick="btnGetTotalNull_OnClick"/>

        <asp:Button ID="btnAddRoomNull" runat="server" Text="Add room null" OnClick="btnAddRoomNull_OnClick"/>
        <br/>
        <br/>
        <asp:Button ID="btnDic" runat="server" Text="Add room dic" OnClick="btnDic_OnClick"/>

        <asp:Label ID="lblTotal" runat="server" Text=""></asp:Label><br/>
        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
    </div>
    </form>
</body>
</html>
