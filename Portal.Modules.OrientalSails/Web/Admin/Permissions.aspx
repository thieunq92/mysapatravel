<%@ Page Language="C#" MasterPageFile="MO.Master" AutoEventWireup="true" CodeBehind="Permissions.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.Permissions" Title="Permission Page - Oriental Sails Management Office" %>

<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
    <div class="page-header">
        <h3>
            <asp:Literal ID="litTitle" runat="server"></asp:Literal></h3>
    </div>
    <div class="row">
        <div class="col-xs-12">
            <asp:Repeater ID="rptPermissions" runat="server" OnItemDataBound="rptPermissions_ItemDataBound">
                <ItemTemplate>
                    <div class="col-xs-12" id="divClear" runat="server" visible="false">
                    </div>
                    <div class="col-xs-3">
                        <asp:HiddenField ID="hiddenName" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "Name") %>' />
                        <div class="checkbox">
                            <label>
                                <asp:CheckBox ID="chkPermission" runat="server" />
                            </label>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
    <div class="row">
        <div class="col-xs-12">
            <asp:Button runat="server" ID="btnSave" CssClass="btn btn-primary"
                Text="Save"
                OnClick="btnSave_Click"></asp:Button>
        </div>
    </div>
</asp:Content>
