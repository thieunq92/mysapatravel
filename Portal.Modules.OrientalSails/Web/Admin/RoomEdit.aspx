<%@ Page Language="C#" MasterPageFile="MO.Master" AutoEventWireup="true" CodeBehind="RoomEdit.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.RoomEdit" Title="Room Adding" %>

<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
    <div class="page-header">
        <h3>Room adding</h3>
    </div>
    <div class="form-group">
        <div class="row">
            <div class="col-xs-1">
                Name
            </div>
            <div class="col-xs-2">
                <asp:TextBox ID="textBoxName" runat="server" CssClass="form-control" placeholder="Name"></asp:TextBox>
            </div>
            <div class="col-xs-1">
                Cruise
            </div>
            <div class="col-xs-2">
                <asp:DropDownList ID="ddlCruises" OnSelectedIndexChanged="ddlCruises_OnSelectedIndexChanged" runat="server" CssClass="form-control"></asp:DropDownList>
            </div>
            <div class="col-xs-1">
                Floor
            </div>
            <div class="col-xs-2">
                <asp:DropDownList ID="ddlFloor" runat="server" CssClass="form-control"></asp:DropDownList>
            </div>
        </div>
    </div>
    <div class="form-group">
        <div class="row">
            <div class="col-xs-1">
                Room Type
            </div>
            <div class="col-xs-2">
                <asp:DropDownList ID="ddlRoomTypex" runat="server" CssClass="form-control"></asp:DropDownList>
            </div>
            <div class="col-xs-1">
                Room Class
            </div>
            <div class="col-xs-2">
                <asp:DropDownList ID="ddlRoomClass" runat="server" CssClass="form-control"></asp:DropDownList>
            </div>
        </div>
    </div>
    <div class="form-group">
        <div class="row">
            <div class="col-xs-12">
                <asp:Button ID="buttonSave" runat="server" OnClick="buttonSave_Click" CssClass="btn btn-primary" />
                <asp:Button ID="buttonCancel" runat="server" OnClick="buttonCancel_Clicl" CssClass="btn btn-primary" />
            </div>
        </div>
    </div>
</asp:Content>
