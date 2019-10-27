<%@ Page Language="C#" MasterPageFile="MO-NoScriptManager.Master" AutoEventWireup="true"
    CodeBehind="User.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.UserPanel"
    Title="User profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
    <div class="container">
        <h3 class="page-header">User profile</h3>
        <div class="form-group">
            <div class="row">
                <div class="col-xs-2">
                    User name
                </div>
                <div class="col-xs-10">
                    <asp:Literal ID="ltrUserName" runat="server" />
                </div>
            </div>
        </div>
        <div class="form-group">
            <div class="row">
                <div class="col-xs-2">
                    First name
                </div>
                <div class="col-xs-10">
                    <asp:Literal ID="ltrFirstName" runat="server" />
                </div>
            </div>
        </div>
        <div class="form-group">
            <div class="row">
                <div class="col-xs-2">
                    Last name
                </div>
                <div class="col-xs-10">
                    <asp:Literal ID="ltrLastName" runat="server" />
                </div>
            </div>
        </div>
        <div class="form-group">
            <div class="row">
                <div class="col-xs-2">
                    Email
                </div>
                <div class="col-xs-10">
                    <asp:Literal ID="ltrEmail" runat="server" />
                </div>
            </div>
        </div>
        <div class="form-group">
            <div class="row">
                <div class="col-xs-2">
                    Active
                </div>
                <div class="col-xs-10">
                    <asp:CheckBox ID="chkActive" runat="server" Enabled="false"></asp:CheckBox>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
