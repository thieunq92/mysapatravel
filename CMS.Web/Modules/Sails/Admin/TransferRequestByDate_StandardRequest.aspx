<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TransferRequestByDate_StandardRequest.aspx.cs"
    Inherits="Portal.Modules.OrientalSails.Web.Admin.TransferRequestByDate_StandardRequest" MasterPageFile="NewPopup.Master" %>

<%@ Import Namespace="Portal.Modules.OrientalSails.Domain" %>
<asp:Content ID="AdminContent" runat="server" ContentPlaceHolderID="AdminContent">
    <div class="row">
        <div class="col-xs-12">
            <table class="table table-bordered table-common">
                <tr class="active">
                    <th rowspan="2" style="width: 4%">No
                    </th>
                    <th rowspan="2" style="width: 25%">Name of pax</th>
                    <th rowspan="2" style="width: 9%">Group</th>
                    <th colspan="3" style="width: 10%">Number of pax</th>
                    <th rowspan="2" style="width: 7%">Trip</th>
                    <th rowspan="2">Pickup address</th>
                    <th rowspan="2" style="width: 14%">Booking code</th>
                </tr>
                <tr class="active">
                    <th>Adult</th>
                    <th>Child</th>
                    <th>Baby</th>
                </tr>
                <asp:Repeater ID="rptTransportBookingStandard" runat="server" OnItemDataBound="rptTransportBookingStandard_ItemDataBound">
                    <ItemTemplate>
                        <tr class="custom-warning">
                            <asp:HiddenField ID="hidBookingId" runat="server" Value="<%# ((Booking)Container.DataItem).Id %>" />
                            <td><%# Container.ItemIndex + 1 %></td>
                            <td><%# ((Booking)Container.DataItem).CustomerName %>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlGroup" AppendDataBoundItems="true" CssClass="form-control" Enabled="<%# LockingTransfer == null ? true : false%>">
                                    <asp:ListItem Value="0" Text="--"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td><%# ((Booking)Container.DataItem).Adult%></td>
                            <td><%# ((Booking)Container.DataItem).Child%></td>
                            <td><%# ((Booking)Container.DataItem).Baby%></td>
                            <td>
                                <%# ((Booking)Container.DataItem).Cruise != null ?((Booking)Container.DataItem).Cruise.Code :""%>
                                <br />
                                <%# ((Booking)Container.DataItem).Trip != null ? ((Booking)Container.DataItem).Trip.TripCode:""%>
                            </td>
                            <td>
                                <%# !String.IsNullOrEmpty(((Booking)Container.DataItem).Transfer_Note)
                                                ? ((Booking)Container.DataItem).Transfer_Note  + "<br/><br/>" : ""%>
                                <%# ((Booking)Container.DataItem).PickupAddress%>
                            </td>
                            <td><a href="BookingView.aspx?NodeId=1&SectionId=15&bi=<%# ((Booking)Container.DataItem).Id%>" target="_blank">
                                <%# ((Booking)Container.DataItem).BookingIdOS%></a></td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        <tr style="<%= rptTransportBookingStandard.Items.Count == 0 ? "": "display:none"%>">
                            <td colspan="100%">No records found</td>
                        </tr>
                        <tr style="<%= rptTransportBookingStandard.Items.Count > 0 ? "": "display:none"%>">
                            <td colspan="3"><strong>Total</strong></td>
                            <td><strong><%= ((List<Booking>)rptTransportBookingStandard.DataSource).Sum(x=>x.Adult)%></strong></td>
                            <td><strong><%= ((List<Booking>)rptTransportBookingStandard.DataSource).Sum(x=>x.Child)%></strong></td>
                            <td><strong><%= ((List<Booking>)rptTransportBookingStandard.DataSource).Sum(x=>x.Baby)%></strong></td>
                        </tr>
                    </FooterTemplate>
                </asp:Repeater>
            </table>
        </div>
    </div>
    <div class="row">
        <div class="col-xs-12">
            <asp:Button runat="server" ID="btnSave" Text="Save" OnClick="btnSave_Click" CssClass="btn btn-primary"></asp:Button>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Scripts" runat="server" ContentPlaceHolderID="Scripts">
       <%if (Session["ParentNeedReload"] != null && (bool)Session["ParentNeedReload"] == true)
      { %>
    <script>
        var btnSaveClicked = true;
    </script>
    <% 
    }%>
</asp:Content>

