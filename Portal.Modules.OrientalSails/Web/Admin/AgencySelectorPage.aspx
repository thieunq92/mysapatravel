<%@ Page Language="C#" MasterPageFile="NewPopup.Master" AutoEventWireup="true" CodeBehind="AgencySelectorPage.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.AgencySelectorPage" Title="Untitled Page" %>

<%@ Register Assembly="CMS.ServerControls" Namespace="CMS.ServerControls" TagPrefix="svc" %>
<asp:Content ID="AdminContent" ContentPlaceHolderID="AdminContent" runat="server">
    <div class="form-group">
        <div class="row">
            <div class="col-xs-1">
                Name
            </div>
            <div class="col-xs-2">
                <asp:TextBox ID="txtName" runat="server" CssClass="form-control" placeholder="Name" ></asp:TextBox>
            </div>
            <div class="col-xs-1">
                Role
            </div>
            <div class="col-xs-2">
                <asp:DropDownList ID="ddlRoles" runat="server" CssClass="form-control"></asp:DropDownList>
            </div>
            <div class="col-xs-1 nopadding-right nopadding-left">
                Sales in charge
            </div>
            <div class="col-xs-2">
                <asp:DropDownList ID="ddlSales" runat="server" CssClass="form-control">
                </asp:DropDownList>
            </div>
            <div class="col-xs-1">
                Location
            </div>
            <div class="col-xs-2">
                <asp:DropDownList runat="server" ID="ddlLocations" CssClass="form-control" />
            </div>
        </div>
    </div>
    <div class="form-group">
        <div class="row">
            <div class="col-xs-12">
                <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search"
                    CssClass="btn btn-primary" />
            </div>
        </div>
    </div>
    <div class="row">
        <table class="table table-bordered table-common ">
            <asp:Repeater ID="rptAgencies" runat="server" OnItemDataBound="rptAgencies_ItemDataBound"
                OnItemCommand="rptAgencies_ItemCommand">
                <HeaderTemplate>
                    <tr class="active">
                        <th colspan="2">Agency name</th>
                        <th>Phone</th>
                        <th>Fax</th>
                        <th>Email</th>
                        <th>Contract</th>
                        <th>Sale in charge
                        </th>
                        <th>Role</th>
                    </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr id="trItem" runat="server" class="item">
                        <td>
                            <asp:Literal ID="litIndex" runat="server"></asp:Literal></td>
                        <td>
                            <a id="aName" runat="server" data-id="txtName" data-phone='<%# Eval("Phone")%>' data-agencyid='<%# Eval("Id")%>'></a>
                        </td>
                        <td>
                            <%# DataBinder.Eval(Container.DataItem,"Phone") %>
                        </td>
                        <td>
                            <%# DataBinder.Eval(Container.DataItem,"Fax") %>
                        </td>
                        <td>
                            <%# DataBinder.Eval(Container.DataItem,"Email") %>
                        </td>
                        <td>
                            <asp:Literal ID="litContract" runat="server"></asp:Literal>
                            <asp:HyperLink ID="hplContract" runat="server"></asp:HyperLink>
                        </td>
                        <td>
                            <asp:Literal ID="litSale" runat="server"></asp:Literal>
                        </td>
                        <td>
                            <asp:Literal ID="litRole" runat="server"></asp:Literal></td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
        <nav arial-label="...">
            <div class="pager">
                <svc:Pager ID="pagerBookings" runat="server" HideWhenOnePage="true" ControlToPage="rptAgencies"
                    OnPageChanged="pagerBookings_PageChanged" PageSize="20" />
            </div>
        </nav>
        <asp:Button ID="btnExport" runat="server" Text="Export" OnClick="btnExport_Click" CssCalss="button" Visible="false" />
    </div>
</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="Scripts" runat="server">

</asp:Content>
