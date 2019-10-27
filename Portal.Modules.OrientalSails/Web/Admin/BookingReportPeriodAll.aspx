<%@ Page Language="C#" MasterPageFile="MO.Master" AutoEventWireup="true"
    CodeBehind="BookingReportPeriodAll.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.BookingReportPeriodAll"
    Title="Booking By Period" %>

<%@ Import Namespace="System.Globalization" %>
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
            <asp:Button runat="server" ID="btnDisplay" CssClass="btn btn-primary" Text="Display" OnClick="btnDisplay_Click" />
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
                        <tr class="active">
                            <th rowspan="2">Date
                            </th>
                            <asp:Repeater ID="rptCruisesRow" runat="server" OnItemDataBound="rptCruisesRow_ItemDataBound">
                                <ItemTemplate>
                                    <th id="thCruise" runat="server"></th>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tr>
                        <tr class="active">
                            <asp:Repeater ID="rptCruiseRoom" runat="server" OnItemDataBound="rptCruisesRow_ItemDataBound">
                                <ItemTemplate>
                                    <asp:Repeater ID="rptRooms" runat="server" OnItemDataBound="rptRooms_ItemDataBound">
                                        <ItemTemplate>
                                            <th colspan="2">
                                                <asp:Literal ID="litRoomName" runat="server"></asp:Literal>
                                            </th>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Literal ID="litTr" runat="server"></asp:Literal>
                        <td>
                            <asp:HyperLink ID="hplDate" runat="server"></asp:HyperLink>
                        </td>
                        <asp:Repeater ID="rptCruiseRoom" runat="server" OnItemDataBound="rptCruisesRow_ItemDataBound">
                            <ItemTemplate>
                                <asp:Repeater ID="rptRooms" runat="server" OnItemDataBound="rptRooms_ItemDataBound">
                                    <ItemTemplate>
                                        <td>
                                            <asp:Literal ID="litTotal" runat="server"></asp:Literal>
                                        </td>
                                        <td id="tdAvail" runat="server">
                                            <asp:Literal ID="litAvail" runat="server"></asp:Literal>
                                        </td>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ItemTemplate>
                        </asp:Repeater>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
    </div>
</asp:Content>
