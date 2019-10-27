<%@ Page Language="C#" MasterPageFile="MO.Master" AutoEventWireup="true"
    CodeBehind="OrderReport.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.OrderReport"
    Title="My Booking Pending" %>

<%@ Register Assembly="CMS.ServerControls" Namespace="CMS.ServerControls" TagPrefix="svc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
    <div class="page-header">
        <h3>My booking pending</h3>
    </div>
    <div class="row">
        <div class="col-xs-1">
            Trip
        </div>
        <div class="col-xs-2">
            <asp:DropDownList ID="ddlTrips" runat="server" CssClass="form-control">
            </asp:DropDownList>
        </div>
        <div class="col-xs-1">
            Start date
        </div>
        <div class="col-xs-2">
            <asp:TextBox ID="txtDate" runat="server" CssClass="form-control" placeholder="Start date" data-control="datetimepicker"></asp:TextBox>
        </div>
        <div class="col-xs-1">
            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click"
                CssClass="btn btn-primary" />
        </div>
    </div>
    <br/>
    <div class="row">
        <div class="col-xs-12">
            <table class="table table-bordered table-hover table-common">
                <asp:Repeater ID="rptBookingList" runat="server" OnItemDataBound="rptBookingList_ItemDataBound"
                    OnItemCommand="rptBookingList_ItemCommand">
                    <HeaderTemplate>
                        <tr class="header">
                            <th style="width: 60px;">Code
                            </th>
                            <th style="width: 80px;">
                                <asp:HyperLink ID="hplStartDate" runat="server"><%# base.GetText("textStartDate") %></asp:HyperLink>
                                <asp:Image runat="server" ID="imgStartDateOrder" Visible="false" />
                            </th>
                            <th>
                                <asp:HyperLink ID="hplTrip" runat="server"><%# base.GetText("textTrip") %></asp:HyperLink>
                                <asp:Image runat="server" ID="imgTripOrder" Visible="false" />
                            </th>
                            <th style="width: 180px;">
                                <asp:HyperLink ID="hplAgency" runat="server"><%# base.GetText("textAgency") %></asp:HyperLink>
                                <asp:Image runat="server" ID="imgAgencyOrder" Visible="false" />
                            </th>
                            <th style="width: 180px;">Sale in charge
                            </th>
                            <th style="width: 100px;">
                                <%# base.GetText("textRoom") %>
                            </th>
                            <th style="width: 140px">
                                <%# base.GetText("textNumberOfPax") %>
                            </th>
                            <th style="width: 100px;">Pending until
                            </th>
                            <th style="width: 50px">
                                <%# base.GetText("textNotify") %>
                            </th>
                            <th style="width: 100px;">
                                <%# base.GetText("labelAction") %>
                            </th>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr class="item">
                            <td>
                                <asp:HyperLink ID="hplCode" runat="server"></asp:HyperLink>
                            </td>
                            <td>
                                <asp:Label ID="labelDate" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="labelTrip" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="labelPartner" runat="server"></asp:Label>
                                <asp:Literal ID="litPhone" runat="server"></asp:Literal>
                            </td>
                            <td>
                                <asp:Literal ID="litSale" runat="server"></asp:Literal>
                            </td>
                            <td>
                                <asp:Label ID="labelRoom" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="labelNumberOfPax" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Literal ID="litPendingUntil" runat="server"></asp:Literal>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkNofity" runat="server" Checked="False" />
                            </td>
                            <td>
                                <asp:LinkButton ID="lbtApprove" runat="server" Text='Approve' CommandName="approve"
                                    CommandArgument='<%#DataBinder.Eval(Container.DataItem, "Id") %>'></asp:LinkButton>
                                <asp:LinkButton ID="lbtCancel" runat="server" Text='Cancel' CommandName="cancel"
                                    CommandArgument='<%#DataBinder.Eval(Container.DataItem, "Id") %>'></asp:LinkButton>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
        <div class="pager">
            <svc:Pager ID="pagerOrders" runat="server" ControlToPage="rptBookingList" OnPageChanged="pagerOrders_PageChanged"
                PageSize="20" />
        </div>
    </div>
</asp:Content>
