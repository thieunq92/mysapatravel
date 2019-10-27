 <%@ Page Language="C#" MasterPageFile="MO-NoScriptManager.Master" AutoEventWireup="true"
    CodeBehind="AgencyList.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.AgencyList"
    Title="Agency Manager" %>

<%@ Register Assembly="CMS.ServerControls" Namespace="CMS.ServerControls" TagPrefix="svc" %>
<asp:Content ID="AdminContent" ContentPlaceHolderID="AdminContent" runat="server">
    <div class="search-panel">
        <div class="form-group">
            <div class="row">
                <div class="col-xs-1">
                    Name
                </div>
                <div class="col-xs-2">
                    <asp:TextBox ID="txtName" runat="server" CssClass="form-control" placeholder="Name"></asp:TextBox>
                </div>
                <div class="col-xs-1">
                    Role
                </div>
                <div class="col-xs-2">
                    <asp:DropDownList ID="ddlRoles" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                        <asp:ListItem Value="All roles">-- Roles --</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-xs-1 nopadding-right">
                    Sales in charge
                </div>
                <div class="col-xs-2">
                    <asp:DropDownList ID="ddlSales" runat="server" CssClass="form-control">
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="form-group">
            <div class="row">
                <div class="col-xs-1 nopadding-right">
                    Contract status
                </div>
                <div class="col-xs-2">
                    <asp:DropDownList runat="server" ID="ddlContracts" CssClass="form-control">
                        <asp:ListItem Value="-1">All contract status</asp:ListItem>
                        <asp:ListItem Value="0">No contract</asp:ListItem>
                        <asp:ListItem Value="4">Contract sent</asp:ListItem>
                        <asp:ListItem Value="2">Contract in valid</asp:ListItem>
                        <asp:ListItem Value="3">Expired in 30 days</asp:ListItem>
                        <asp:ListItem Value="1">Expired</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-xs-1">
                    Location
                </div>
                <div class="col-xs-2">
                    <asp:DropDownList runat="server" ID="ddlLocations" CssClass="form-control">
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="form-group">
            <div class="row">
                <div class="col-xs-12">
                    <asp:Button runat="server" ID="btnSearch" CssClass="btn btn-primary"
                        OnClick="btnSearch_Click" Text="Search"></asp:Button>
                    <asp:Button runat="server" ID="btnReboundSale" CssClass="btn btn-primary"
                        OnClick="btnReboundSale_Click" Text="Rebound sales"></asp:Button>
                    <asp:Button runat="server" ID="btnRecheck" CssClass="btn btn-primary"
                        OnClick="btnRecheck_Click" Text="Recheck sales"></asp:Button>
                </div>
            </div>
        </div>
    </div>
    <div class="agency-panel">
        <div class="row">
            <div class="col-xs-12">
                <table class="table table-bordered table-common">
                    <asp:Repeater ID="rptAgencies" runat="server" OnItemDataBound="rptAgencies_ItemDataBound"
                        OnItemCommand="rptAgencies_ItemCommand">
                        <HeaderTemplate>
                            <tr class="active">
                                <th>STT
                                </th>
                                <th>Agency name
                                </th>
                                <th>Phone
                                </th>
                                <th>Fax
                                </th>
                                <th>Email
                                </th>
                                <th>Contract status
                                </th>
                                <th>Payment
                                </th>
                                <th>Sales in charge
                                </th>
                                <th>Role
                                </th>
                                <th style="width:8%">Last booking
                                </th>
                                <th>Price
                                </th>
                                <th>
                                </th>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr id="trItem" runat="server" class="item">
                                <td>
                                    <asp:Literal ID="litIndex" runat="server"></asp:Literal>
                                </td>
                                <td class="--text-left">
                                    <asp:HyperLink ID="hplName" runat="server"></asp:HyperLink>
                                </td>
                                <td class="--text-left">
                                    <%# DataBinder.Eval(Container.DataItem,"Phone") %>
                                </td>
                                <td class="--text-left">
                                    <%# DataBinder.Eval(Container.DataItem,"Fax") %>
                                </td>
                                <td class="--text-left">
                                    <%# DataBinder.Eval(Container.DataItem,"Email") %>
                                </td>
                                <td>
                                    <asp:Literal ID="litContract" runat="server"></asp:Literal>
                                    <asp:HyperLink ID="hplContract" runat="server"></asp:HyperLink>
                                </td>
                                <td>
                                    <asp:Literal runat="server" ID="litPayment"></asp:Literal>
                                </td>
                                <td>
                                    <asp:Literal ID="litSale" runat="server"></asp:Literal>
                                </td>
                                <td>
                                    <asp:Literal ID="litRole" runat="server"></asp:Literal>
                                </td>
                                <td id="tdLastBooking" runat="server"></td>
                                <td>
                                    <asp:HyperLink ID="hplPriceSetting" runat="server">Setting
                                    </asp:HyperLink>
                                </td>
                                <td>
                                    <asp:HyperLink ID="hplEdit" runat="server">
                                        <i class="fa fa-edit fa-lg" aria-hidden="true" data-toggle="tooltip" data-placement="top" title="Edit"></i>
                                    </asp:HyperLink>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
                <div class="form-group">
                    <div class="row">
                        <div class="col-xs-12 --no-padding-left">
                            <asp:Button runat="server" ID="btnExportAgency" CssClass="btn btn-primary"
                                Text="Export agency" OnClick="btnExport_Click"></asp:Button>
                            <asp:HyperLink runat="server" ID="hplViewMeetings" CssClass="btn btn-primary">View meetings</asp:HyperLink>
                        </div>
                        <div class="col-xs-12">
                            <div class="pager">
                                <svc:Pager ID="pagerBookings" runat="server" HideWhenOnePage="true" ControlToPage="rptAgencies"
                                    OnPageChanged="pagerBookings_PageChanged" PageSize="20" />
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>
</asp:Content>
