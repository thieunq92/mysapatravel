<%@ Page Language="C#" MasterPageFile="MO.Master" ValidateRequest="false" AutoEventWireup="true" CodeBehind="SailsTripEdit.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.SailsTripEdit" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
    <div class="page-header">
        <h3>Trip adding</h3>
    </div>
    <div class="form-group">
        <div class="row">
            <div class="col-xs-1">
                Name
            </div>
            <div class="col-xs-2">
                <asp:TextBox ID="textBoxName" runat="server" CssClass="form-control" placeholder="Name"></asp:TextBox>
            </div>
            <div class="col-xs-offset-1 col-xs-1">
                Trip code
            </div>
            <div class="col-xs-2">
                <asp:TextBox ID="txtTripCode" runat="server" CssClass="form-control" placeholder="Trip code"></asp:TextBox>
            </div>
        </div>
    </div>
    <div class="form-group">
        <div class="row">
            <div class="col-xs-1">
                Number of day
            </div>
            <div class="col-xs-2">
                <asp:TextBox ID="textBoxNumberOfDay" runat="server" CssClass="form-control" placeholder="Number of day"></asp:TextBox>
            </div>
            <div class="col-xs-1 nopadding-left">
                <asp:DropDownList ID="ddlHalfDay" runat="server" CssClass="form-control">
                    <asp:ListItem Value="0">All day</asp:ListItem>
                    <asp:ListItem Value="1">Morning</asp:ListItem>
                    <asp:ListItem Value="2">Afternoon</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-xs-1">
                Number of option
            </div>
            <div class="col-xs-1">
                <asp:DropDownList ID="ddlNumberOfOptions" runat="server" CssClass="form-control">
                    <asp:ListItem Text="0" Value="0"></asp:ListItem>
                    <asp:ListItem Text="2" Value="2"></asp:ListItem>
                    <asp:ListItem Text="3" Value="3"></asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
    </div>
    <div class="form-group">
        <div class="row">
            <div class="col-xs-12">
                Description
            </div>
        </div>
        <div class="row">
            <div class="col-xs-12">
                <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" CssClass="form-control" placeholder="Description">
                </asp:TextBox>
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
