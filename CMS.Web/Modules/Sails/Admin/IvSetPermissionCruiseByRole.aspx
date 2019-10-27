<%@ Page Title="" Language="C#" MasterPageFile="MO.Master" AutoEventWireup="true" CodeBehind="IvSetPermissionCruiseByRole.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.IvSetPermissionCruiseByRole" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="AdminContent" runat="server">
    <div class="page-header">
        <h3>
            <asp:Literal ID="litTitle" runat="server"></asp:Literal></h3>
    </div>
    <div class="row">
        <table class="table table-bordered table-hover">
            <asp:Repeater ID="rptCruise" runat="server" OnItemDataBound="rptCruise_ItemDataBound">
                <ItemTemplate>
                    <tr>
                        <td></td>
                        <td>
                            <asp:HiddenField ID="hidCruiseId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "Id") %>' />
                            <asp:HiddenField ID="hidCruiseRoleId" runat="server" Value='' />
                            <asp:CheckBox ID="chkPermission" runat="server" />
                        </td>
                        <td>
                            <%#Eval("Name") %>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </div>
    <div class="row">
        <div class="col-xs-12">
            <asp:Button runat="server" ID="btnSave" CssClass="btn btn-primary"
                Text="Save"
                OnClick="btnSave_Click"></asp:Button>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
