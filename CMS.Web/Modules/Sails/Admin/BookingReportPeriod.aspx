<%@ Page Language="C#" MasterPageFile="MO.Master" AutoEventWireup="true"
    CodeBehind="BookingReportPeriod.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.BookingReportPeriod"
    Title="Booking By Period" %>

<%@ Register Assembly="CMS.ServerControls" Namespace="CMS.ServerControls" TagPrefix="svc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
    <div class="page-header">
        <h3>Booking by period</h3>
    </div>
    <div class="row">
        <div class="col-xs-1">
            From
        </div>
        <div class="col-xs-2">
            <asp:TextBox ID="txtFrom" runat="server" CssClass="form-control" data-control="datetimepicker" placeholder="From (dd/mm/yyyy)"></asp:TextBox>
        </div>
        <div class="col-xs-1">
            To
        </div>
        <div class="col-xs-2">
            <asp:TextBox ID="txtTo" runat="server" CssClass="form-control" data-control="datetimepicker" placeholder="To (dd/mm/yyyy)"></asp:TextBox>
        </div>
        <div class="col-xs-6">
            <asp:Button ID="btnDisplay" runat="server" Text="Display" OnClick="btnDisplay_Click"
                CssClass="btn btn-primary" />
        </div>
    </div>
    <br />
    <div class="row">
        <div class="col-xs-12 btn-grid">
            <asp:Repeater ID="rptCruises" runat="server" OnItemDataBound="rptCruises_ItemDataBound">
                <HeaderTemplate>
                    <asp:HyperLink ID="hplCruises" runat="server" Text="All" CssClass="btn btn-default"></asp:HyperLink>
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:HyperLink ID="hplCruises" runat="server" CssClass="btn btn-default"></asp:HyperLink>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
    <br />
    <div class="row">
        <div class="col-xs-12">
            <table class="table table-bordered table-hover table-common">
                <asp:Repeater ID="rptBookingList" runat="server" OnItemDataBound="rptBookingList_ItemDataBound"
                    OnItemCommand="rptBookingList_ItemCommand">
                    <HeaderTemplate>
                        <tr class="header">
                            <th rowspan="2">
                                Date
                            </th>
                            <th colspan="4">
                                Number of pax
                            </th>
                            <asp:Repeater ID="rptTrips" runat="server">
                                <ItemTemplate>
                                    <th rowspan="2">
                                        <%# DataBinder.Eval(Container.DataItem, "TripCode") %>
                                    </th>
                                </ItemTemplate>
                            </asp:Repeater>
                            <asp:Repeater ID="rptRooms" runat="server" OnItemDataBound="rptRooms_ItemDataBound">
                                <ItemTemplate>
                                    <th colspan="2">
                                        <asp:Literal ID="litRoomName" runat="server"></asp:Literal>
                                    </th>
                                </ItemTemplate>
                            </asp:Repeater>
                            <th rowspan="2">Tổng
                            </th>
                            <th rowspan="2"></th>
                        </tr>
                        <tr>
                            <th>Adult
                            </th>
                            <th>Child
                            </th>
                            <th>Baby
                            </th>
                            <th>Total
                            </th>
                            <asp:Repeater ID="rptRoomAvail" runat="server">
                                <ItemTemplate>
                                    <th>Total</th>
                                    <th>Avail</th>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Literal ID="litTr" runat="server"></asp:Literal>
                        <td>
                            <asp:HyperLink ID="hplDate" runat="server"></asp:HyperLink>
                        </td>
                        <td>
                            <asp:Literal ID="litAdult" runat="server"></asp:Literal></td>
                        <td>
                            <asp:Literal ID="litChild" runat="server"></asp:Literal></td>
                        <td>
                            <asp:Literal ID="litBaby" runat="server"></asp:Literal></td>
                        <td>
                            <asp:Literal ID="litTotalPax" runat="server"></asp:Literal>
                        </td>
                        <asp:Repeater ID="rptTrips" runat="server" OnItemDataBound="rptTrips_ItemDataBound">
                            <ItemTemplate>
                                <td>
                                    <asp:Literal ID="litPax" runat="server"></asp:Literal>
                                </td>
                            </ItemTemplate>
                        </asp:Repeater>
                        <asp:Repeater ID="rptRoomAvail" runat="server" OnItemDataBound="rptRooms_ItemDataBound">
                            <ItemTemplate>
                                <td>
                                    <asp:Literal ID="litTotal" runat="server"></asp:Literal></td>
                                <td id="tdAvail" runat="server">
                                    <asp:Literal ID="litAvail" runat="server"></asp:Literal></td>
                            </ItemTemplate>
                        </asp:Repeater>
                        <td>
                            <asp:Literal ID="litTotal" runat="server"></asp:Literal>
                        </td>
                        <td>
                            <asp:LinkButton ID="lbtLock" runat="server" CommandName='lock' CssClass="btn btn-primary" Text="Lock"></asp:LinkButton>
                        </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
    </div>
</asp:Content>
