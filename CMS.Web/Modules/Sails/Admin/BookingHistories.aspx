<%@ Page Language="C#" MasterPageFile="MO-NoScriptManager.Master" AutoEventWireup="true"
    CodeBehind="BookingHistories.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.BookingHistories" %>

<asp:Content ID="Header" ContentPlaceHolderID="Head" runat="server">
    <title>Booking Histories</title>
</asp:Content>
<asp:Content ID="AdminContent" ContentPlaceHolderID="AdminContent" runat="server">
    <div class="row">
        <div class="col-xs-12">
            <h4><strong>Date</strong></h4>
            <table class="table table-bordered table-common">
                <tr class="active">
                    <th class="th__time" style="width: 19%">Time</th>
                    <th class="th__user" style="width: 17%">User</th>
                    <th class="th__from" style="width: 32%">From</th>
                    <th class="th__changedto" style="width: 32%">Changed to</th>
                </tr>
                <asp:Repeater runat="server" ID="rptDates" OnItemDataBound="rptDates_ItemDataBound">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:Literal runat="server" ID="litTime"></asp:Literal></td>
                            <td>
                                <asp:Literal runat="server" ID="litUser"></asp:Literal></td>
                            <td>
                                <asp:Literal runat="server" ID="litFrom"></asp:Literal></td>
                            <td>
                                <asp:Literal runat="server" ID="litTo"></asp:Literal></td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        <tr class='<%= rptDates.Items.Count == 0 ? "shown":"hide"%>'>
                            <td colspan="100%">Không tìm thấy bản ghi nào
                            </td>
                        </tr>
                    </FooterTemplate>
                </asp:Repeater>
            </table>
        </div>
    </div>
    <div class="row">
        <div class="col-xs-12">
            <h4><strong>Trip</strong></h4>
            <table class="table table-bordered table-common">
                <tr class="active">
                    <th class="th__time" style="width: 19%">Time</th>
                    <th class="th__user" style="width: 17%">User</th>
                    <th class="th__from" style="width: 32%">From</th>
                    <th class="th__changedto" style="width: 32%">Changed to</th>
                </tr>
                <asp:Repeater runat="server" ID="rptTrips" OnItemDataBound="rptTrips_ItemDataBound">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:Literal runat="server" ID="litTime"></asp:Literal></td>
                            <td>
                                <asp:Literal runat="server" ID="litUser"></asp:Literal></td>
                            <td>
                                <asp:Literal runat="server" ID="litFrom"></asp:Literal></td>
                            <td>
                                <asp:Literal runat="server" ID="litTo"></asp:Literal></td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        <tr class='<%= rptTrips.Items.Count == 0 ? "shown":"hide"%>'>
                            <td colspan="100%">Không tìm thấy bản ghi nào
                            </td>
                        </tr>
                    </FooterTemplate>
                </asp:Repeater>
            </table>
        </div>
    </div>
    <div class="row">
        <div class="col-xs-12">
            <h4><strong>Total</strong></h4>
            <table class="table table-bordered table-common">
                <tr class="active">
                    <th class="th__time" style="width: 19%">Time</th>
                    <th class="th__user" style="width: 17%">User</th>
                    <th class="th__from" style="width: 32%">From</th>
                    <th class="th__changedto" style="width: 32%">Changed to</th>
                </tr>
                <asp:Repeater runat="server" ID="rptTotals" OnItemDataBound="rptTotals_ItemDataBound">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:Literal runat="server" ID="litTime"></asp:Literal></td>
                            <td>
                                <asp:Literal runat="server" ID="litUser"></asp:Literal></td>
                            <td>
                                <asp:Literal runat="server" ID="litFrom"></asp:Literal></td>
                            <td>
                                <asp:Literal runat="server" ID="litTo"></asp:Literal></td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        <tr class='<%= rptTotals.Items.Count == 0 ? "shown":"hide"%>'>
                            <td colspan="100%">Không tìm thấy bản ghi nào
                            </td>
                        </tr>
                    </FooterTemplate>
                </asp:Repeater>
            </table>
        </div>
    </div>
    <div class="row">
        <div class="col-xs-12">
            <h4><strong>Status</strong></h4>
            <table class="table table-bordered table-common">
                <tr class="active">
                    <th class="th__time" style="width: 19%">Time</th>
                    <th class="th__user" style="width: 17%">User</th>
                    <th class="th__from" style="width: 32%">From</th>
                    <th class="th__changedto" style="width: 32%">Changed to</th>
                </tr>
                <asp:Repeater runat="server" ID="rptStatus" OnItemDataBound="rptStatus_ItemDataBound">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:Literal runat="server" ID="litTime"></asp:Literal></td>
                            <td>
                                <asp:Literal runat="server" ID="litUser"></asp:Literal></td>
                            <td>
                                <asp:Literal runat="server" ID="litFrom"></asp:Literal></td>
                            <td>
                                <asp:Literal runat="server" ID="litTo"></asp:Literal></td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        <tr class='<%= rptStatus.Items.Count == 0 ? "shown":"hide"%>'>
                            <td colspan="100%">Không tìm thấy bản ghi nào
                            </td>
                        </tr>
                    </FooterTemplate>
                </asp:Repeater>
            </table>
        </div>
    </div>
    <div class="row">
        <div class="col-xs-12">
            <h4><strong>Agency</strong></h4>
            <table class="table table-bordered table-common">
                <tr class="active">
                    <th class="th__time" style="width: 19%">Time</th>
                    <th class="th__user" style="width: 17%">User</th>
                    <th class="th__from" style="width: 32%">From</th>
                    <th class="th__changedto" style="width: 32%">Changed to</th>
                </tr>
                <asp:Repeater runat="server" ID="rptAgencies" OnItemDataBound="rptAgencies_ItemDataBound">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:Literal runat="server" ID="litTime"></asp:Literal></td>
                            <td>
                                <asp:Literal runat="server" ID="litUser"></asp:Literal></td>
                            <td>
                                <asp:Literal runat="server" ID="litFrom"></asp:Literal></td>
                            <td>
                                <asp:Literal runat="server" ID="litTo"></asp:Literal></td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        <tr class='<%= rptAgencies.Items.Count == 0 ? "shown":"hide"%>'>
                            <td colspan="100%">Không tìm thấy bản ghi nào
                            </td>
                        </tr>
                    </FooterTemplate>
                </asp:Repeater>
            </table>
        </div>
    </div>
    <div class="row">
        <div class="col-xs-12">
            <h4><strong>Number of cabin</strong></h4>
            <table class="table table-bordered table-common">
                <tr class="active">
                    <th class="th__time" style="width: 19%">Time</th>
                    <th class="th__user" style="width: 17%">User</th>
                    <th class="th__from" style="width: 32%">From</th>
                    <th class="th__changedto" style="width: 32%">Changed to</th>
                </tr>
                <asp:Repeater runat="server" ID="rptCabins" OnItemDataBound="rptCabins_ItemDataBound">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:Literal runat="server" ID="litTime"></asp:Literal></td>
                            <td>
                                <asp:Literal runat="server" ID="litUser"></asp:Literal></td>
                            <td>
                                <asp:Literal runat="server" ID="litFrom"></asp:Literal></td>
                            <td>
                                <asp:Literal runat="server" ID="litTo"></asp:Literal></td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        <tr class='<%= rptCabins.Items.Count == 0 ? "shown":"hide"%>'>
                            <td colspan="100%">Không tìm thấy bản ghi nào
                            </td>
                        </tr>
                    </FooterTemplate>
                </asp:Repeater>
            </table>
        </div>
    </div>
    <div class="row">
        <div class="col-xs-12">
            <h4><strong>Number of customer</strong></h4>
            <table class="table table-bordered table-common">
                <tr>
                    <th class="th__time" style="width: 19%">Time</th>
                    <th class="th__user" style="width: 17%">User</th>
                    <th class="th__from" style="width: 32%">From</th>
                    <th class="th__changedto" style="width: 32%">Changed to</th>
                </tr>
                <asp:Repeater runat="server" ID="rptCustomers" OnItemDataBound="rptCustomers_ItemDataBound">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:Literal runat="server" ID="litTime"></asp:Literal></td>
                            <td>
                                <asp:Literal runat="server" ID="litUser"></asp:Literal></td>
                            <td>
                                <asp:Literal runat="server" ID="litFrom"></asp:Literal></td>
                            <td>
                                <asp:Literal runat="server" ID="litTo"></asp:Literal></td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        <tr class='<%= rptCustomers.Items.Count == 0 ? "shown":"hide"%>'>
                            <td colspan="100%">Không tìm thấy bản ghi nào
                            </td>
                        </tr>
                    </FooterTemplate>
                </asp:Repeater>
            </table>
        </div>
    </div>
    <div class="row">
        <div class="col-xs-12">
            <h4><strong>Special request</strong></h4>
            <table class="table table-bordered table-common">
                <tr class="active">
                    <th class="th__time" style="width: 19%">Time</th>
                    <th class="th__user" style="width: 17%">User</th>
                    <th class="th__from" style="width: 32%">From</th>
                    <th class="th__changedto" style="width: 32%">Changed to</th>
                </tr>
                <asp:Repeater runat="server" ID="rptSpecialRequest" OnItemDataBound="rptSpecialRequest_ItemDataBound">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:Literal runat="server" ID="litTime"></asp:Literal></td>
                            <td>
                                <asp:Literal runat="server" ID="litUser"></asp:Literal></td>
                            <td>
                                <asp:Literal runat="server" ID="litFrom"></asp:Literal></td>
                            <td>
                                <asp:Literal runat="server" ID="litTo"></asp:Literal></td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        <tr class='<%= rptSpecialRequest.Items.Count == 0 ? "shown":"hide"%>'>
                            <td colspan="100%">Không tìm thấy bản ghi nào
                            </td>
                        </tr>
                    </FooterTemplate>
                </asp:Repeater>
            </table>
        </div>
    </div>
    <div class="row">
        <div class="col-xs-12">
            <h4><strong>Pickup Address</strong></h4>
            <table class="table table-bordered table-common">
                <tr class="active">
                    <th class="th__time" style="width: 19%">Time</th>
                    <th class="th__user" style="width: 17%">User</th>
                    <th class="th__from" style="width: 32%">From</th>
                    <th class="th__changedto" style="width: 32%">Changed to</th>
                </tr>
                <asp:Repeater runat="server" ID="rptPickupAddress" OnItemDataBound="rptPickupAddress_ItemDataBound">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:Literal runat="server" ID="litTime"></asp:Literal></td>
                            <td>
                                <asp:Literal runat="server" ID="litUser"></asp:Literal></td>
                            <td>
                                <asp:Literal runat="server" ID="litFrom"></asp:Literal></td>
                            <td>
                                <asp:Literal runat="server" ID="litTo"></asp:Literal></td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        <tr class='<%= rptPickupAddress.Items.Count == 0 ? "shown":"hide"%>'>
                            <td colspan="100%">Không tìm thấy bản ghi nào
                            </td>
                        </tr>
                    </FooterTemplate>
                </asp:Repeater>
            </table>
        </div>
    </div>
    <div class="row">
        <div class="col-xs-12">
            <h4><strong>Room</strong></h4>
            <table class="table table-bordered table-common">
                <tr class="active">
                    <th class="th__time" style="width: 19%">Time</th>
                    <th class="th__user" style="width: 17%">User</th>
                    <th class="th__from" style="width: 32%">From</th>
                    <th class="th__changedto" style="width: 32%">Changed to</th>
                </tr>
                <asp:Repeater runat="server" ID="rptRooms" OnItemDataBound="rptRooms_OnItemDataBound">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:Literal runat="server" ID="litTime"></asp:Literal></td>
                            <td>
                                <asp:Literal runat="server" ID="litUser"></asp:Literal></td>
                            <td>
                                <asp:Literal runat="server" ID="litFrom"></asp:Literal></td>
                            <td>
                                <asp:Literal runat="server" ID="litTo"></asp:Literal></td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        <tr class='<%= rptRooms.Items.Count == 0 ? "shown":"hide"%>'>
                            <td colspan="100%">Không tìm thấy bản ghi nào
                            </td>
                        </tr>
                    </FooterTemplate>
                </asp:Repeater>
            </table>
        </div>
    </div>

</asp:Content>
