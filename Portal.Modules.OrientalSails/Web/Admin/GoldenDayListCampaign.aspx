<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GoldenDayListCampaign.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.GoldenDayListCampaign"
    MasterPageFile="MO.Master" %>

<%@ Register Assembly="CMS.ServerControls" Namespace="CMS.ServerControls" TagPrefix="svc" %>
<%@ Import Namespace="Portal.Modules.OrientalSails.Domain" %>
<%@ Import Namespace="Portal.Modules.OrientalSails.Web.Util" %>
<asp:Content ID="Head" runat="server" ContentPlaceHolderID="Head">
    <title>List campaign</title>
</asp:Content>
<asp:Content ID="AdminContent" runat="server" ContentPlaceHolderID="AdminContent">
    <div class="wrapper wrapper__body">
        <h4 class="page-header --text-bold">List campaign</h4>
        <div class="row">
            <div class="col-xs-6 --no-padding-leftright">
                <table class="table table-bordered table-hover table-common">
                    <tbody>
                        <tr class="active">
                            <th style="width: 23%">Campaign name
                            </th>
                            <th style="width: 16%">Date applied
                            </th>
                            <th style="">Created by
                            </th>
                            <th style="width: 11%">Export
                            </th>
                            <th style="width: 23%">No of new bookings
                            </th>
                            <th></th>
                        </tr>
                        <asp:Repeater runat="server" ID="rptCampaign" OnItemCommand="rptCampaign_ItemCommand" OnItemDataBound="rptCampaign_ItemDataBound">
                            <ItemTemplate>
                                <tr>
                                    <td><%# ((Campaign)Container.DataItem).Name %></td>
                                    <td><%# GetDateApplied((Campaign)Container.DataItem) %></a></td>
                                    <td><%# ((Campaign)Container.DataItem).CreatedBy.FullName%></td>
                                    <td>
                                        <asp:LinkButton runat="server" ID="lbtExport" CommandName="Export" CommandArgument="<%# ((Campaign)Container.DataItem).Id %>" Text="Export"></asp:LinkButton></td>
                                    <td>(<%# GetNoOfNewBooking((Campaign)Container.DataItem) %> Bookings)
                                        <asp:LinkButton runat="server" ID="lbtView">View</asp:LinkButton></td>
                                    <td>
                                        <a href="GoldenDayCreateCampaign.aspx?NodeId=1&SectionId=15&ci=<%# ((Campaign)Container.DataItem).Id %>">Edit</a>
                                        <asp:LinkButton runat="server" ID="lbtDelete" CommandName="Delete" CommandArgument="<%# ((Campaign)Container.DataItem).Id %>" Text="Delete" OnClientClick="return confirm('Are you sure?')"></asp:LinkButton>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                    <% if (!IsPostBack)
                       {
                           if (((IEnumerable<Campaign>)rptCampaign.DataSource).Count() <= 0)
                           {
                    %>
                    <tr>
                        <td colspan="100%">No records found </td>
                    </tr>
                    <%
                           }
                       }
                    %>
                </table>
                <nav arial-label="...">
                    <div class="pager">
                        <svc:Pager ID="pagerCampaign" runat="server" HideWhenOnePage="true" ControlToPage="rptCampaign"
                            PagerLinkMode="HyperLinkQueryString" PageSize="20" />
                    </div>
                </nav>
            </div>
            <asp:Button ID="btnTrigger" runat="server" Style="display: none" OnClick="btnTrigger_Click" />
            <asp:HiddenField ID="hidCampaignId" runat="server" />
            <div class="col-xs-6 --no-padding-right">
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <asp:PlaceHolder ID="plhTableBooking" runat="server" Visible="false">
                            <table class="table table-bordered table-hover table-common">
                                <tr class="active">
                                    <th>Booking code
                                    </th>
                                    <th>Trip
                                    </th>
                                    <th>Cruise
                                    </th>
                                    <th>Start date
                                    </th>
                                    <th>Agency
                                    </th>
                                    <th>No of room  
                                    </th>
                                    <th>No of pax
                                    </th>
                                </tr>
                                <asp:Repeater runat="server" ID="rptBooking" OnItemCommand="rptBooking_ItemCommand">
                                    <ItemTemplate>
                                        <tr class="<%# ((Booking)Container.DataItem).Status == StatusType.Cancelled ? "--cancelled" : ""%>
                                                <%# ((Booking)Container.DataItem).Status == StatusType.Pending ? "--pending" : ""%>">
                                            <td><a href="BookingView.aspx?NodeId=1&SectionId=15&bi=<%#((Booking)Container.DataItem).Id %>"><%#((Booking)Container.DataItem).BookingIdOS%></td>
                                            <td><%#((Booking)Container.DataItem).Trip.TripCode%></td>
                                            <td><%#((Booking)Container.DataItem).Cruise.Code%></td>
                                            <td><%#((Booking)Container.DataItem).StartDate.ToString("dd/MM/yyyy")%></td>
                                            <td><a href="BookingView.aspx?NodeId=1&SectionId=15&ai=<%#((Booking)Container.DataItem).Agency.Id %>"><%#((Booking)Container.DataItem).Agency.Name%></a></td>
                                            <td><%#((Booking)Container.DataItem).BookingRooms.Count %></td>
                                            <td><%#((Booking)Container.DataItem).Adult + ((Booking)Container.DataItem).Baby + ((Booking)Container.DataItem).Child%></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <% if (((IEnumerable<Booking>)rptBooking.DataSource).Count() <= 0)
                                   {
                                %>
                                <tr>
                                    <td colspan="100%">No records found </td>
                                </tr>
                                <%
                                   }
                                %>
                            </table>
                        </asp:PlaceHolder>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnTrigger" EventName="click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content runat="server" ID="Scripts" ContentPlaceHolderID="Scripts">
    <script>
        $(function () {
            $("[data-toggle=tooltip][title='']").show();
        })
    </script>
</asp:Content>


