<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TransferRequestByDate_TransportBooking.aspx.cs"
    Inherits="Portal.Modules.OrientalSails.Web.Admin.TransferRequestByDate_TransportBooking" MasterPageFile="NewPopup.Master" %>

<%@ Import Namespace="Portal.Modules.OrientalSails.Domain" %>
<asp:Content ID="AdminContent" runat="server" ContentPlaceHolderID="AdminContent">
    <div class="form-group">
        <div class="row">
            <div class="col-xs-6 nopadding-right">
                <input type="text" readonly="true" class="form-control"
                    value="<%= BusByDate != null && BusByDate.Supplier != null ? BusByDate.Supplier.Name :"" %>"
                    data-toggle="tooltip" title="Supplier name" placeholder="Supplier name" />
            </div>
        </div>
    </div>
    <div class="form-group">
        <div class="row">
            <asp:HiddenField runat="server" ID="hidExpenseDriverId" />
            <div class="col-xs-2 nopadding-right">
                <span data-toggle="tooltip" title="Driver name" data-placement="right">
                    <asp:TextBox runat="server" ID="txtDriverName" CssClass="form-control" placeholder="Driver name" />
                </span>
            </div>
            <div class="col-xs-2 nopadding-right">
                <span data-toggle="tooltip" title="Driver phone" data-placement="right">
                    <asp:TextBox runat="server" ID="txtDriverPhone" CssClass="form-control" placeholder="Driver phone"
                         data-control="phoneinputmask" />
                </span>
            </div>
            <div class="col-xs-2 nopadding-right">
                <div class="input-group">
                    <asp:TextBox runat="server" ID="txtDriverCost" Text="0" CssClass="form-control"
                        data-control="inputmask"
                        data-inputmask="'alias': 'numeric', 'groupSeparator': ',', 'autoGroup': true, 'digits': 2, 'digitsOptional': true, 'rightAlign':false"
                        data-toggle="tooltip"
                        data-placement="right"
                        title="Cost" />
                    <span class="input-group-addon">₫</span>
                </div>
            </div>
        </div>
    </div>
    <div class="form-group">
        <div class="row">
            <div class="col-xs-2 nopadding-right">
                <input type="text" readonly="true" class="form-control"
                    value="<%= BusByDate != null && BusByDate.Guide != null ? BusByDate.Guide.Name :"" %>"
                    data-toggle="tooltip" title="Guide name" placeholder="Guide name" />
            </div>
            <div class="col-xs-2 nopadding-right">
                <input type="text" readonly="true" class="form-control phone"
                    value="<%= BusByDate != null && BusByDate.Guide != null ? BusByDate.Guide.Phone :"" %>"
                    data-toggle="tooltip" title="Guide phone" placeholder="Guide phone" data-control="phoneinputmask" />
            </div>
            <div class="col-xs-2 nopadding-right">
                <div class="input-group">
                    <asp:TextBox runat="server" ID="txtGuideCost" Text="0" CssClass="form-control"
                        data-control="inputmask"
                        data-inputmask="'alias': 'numeric', 'groupSeparator': ',', 'autoGroup': true, 'digits': 2, 'digitsOptional': true, 'rightAlign':false"
                        data-toggle="tooltip"
                        title="Cost" Visible="false" />
                </div>
            </div>
        </div>
    </div>
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
                <asp:Repeater ID="rptTransportBooking" runat="server" OnItemDataBound="rptTransportBooking_ItemDataBound">
                    <ItemTemplate>
                        <tr class="<%# TableRowColorGetByGroup((Booking)Container.DataItem) %>">
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
                            <td>
                                <a href="BookingView.aspx?NodeId=1&SectionId=15&bi=<%# ((Booking)Container.DataItem).Id%>" target="_blank">
                                    <%# ((Booking)Container.DataItem).BookingIdOS%></a>
                                <i
                                    class="fa fa-lg fa-angle-double-up text-success"
                                    data-toggle="tooltip"
                                    data-placement="top"
                                    title="Upgraded"
                                    <%# ((Booking)Container.DataItem).Transfer_Upgraded ? "":"style='visibility:hidden'"%>></i>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        <tr style="<%= rptTransportBooking.Items.Count == 0 ? "": "display:none"%>">
                            <td colspan="100%">No records found</td>
                        </tr>
                        <tr style="<%= rptTransportBooking.Items.Count > 0 ? "": "display:none"%>">
                            <td colspan="3"><strong>Total</strong></td>
                            <td><strong><%= ((List<Booking>)rptTransportBooking.DataSource).Sum(x=>x.Adult)%></strong></td>
                            <td><strong><%= ((List<Booking>)rptTransportBooking.DataSource).Sum(x=>x.Child)%></strong></td>
                            <td><strong><%= ((List<Booking>)rptTransportBooking.DataSource).Sum(x=>x.Baby)%></strong></td>
                        </tr>
                    </FooterTemplate>
                </asp:Repeater>
            </table>
        </div>
    </div>
    <div class="row">
        <div class="col-xs-12">
            <asp:Button runat="server" ID="btnSave" Text="Save" OnClick="btnSave_Click" CssClass="btn btn-primary"></asp:Button>
            <button
                id="btnviewstandardrequest"
                type="button"
                class="btn btn-primary"
                data-url="TransferRequestByDate_StandardRequest.aspx?NodeId=1&SectionId=15&r=<%= Route.Id %>&w=<%= Route.Way %>&bt=<%= BusType.Id %>&d=<%= Date.HasValue ? Date.Value.ToString("dd/MM/yyyy") : "" %>&gr=<%= Group %>"
                <%= BusType != null && BusType.Name != "Limousine" ? "style='display:none'" : "" %>>
                Standard request</button>
        </div>
    </div>
    <br />
    <div class="row">
        <div class="col-xs-12">
            <table class="table table-bordered table-common"
                style="<%= ((List<Booking>)rptTransportBookingWithoutGroup.DataSource).Count() <= 0 ? "display:none": "" %>">
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
                <asp:Repeater ID="rptTransportBookingWithoutGroup" runat="server" OnItemDataBound="rptTransportBookingWithoutGroup_ItemDataBound">
                    <ItemTemplate>
                        <tr class="<%# TableRowColorGetByGroup((Booking)Container.DataItem) %>">
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
                        <tr style="<%= rptTransportBookingWithoutGroup.Items.Count == 0 ? "": "display:none"%>">
                            <td colspan="100%">No records found</td>
                        </tr>
                        <tr style="<%= rptTransportBookingWithoutGroup.Items.Count > 0 ? "": "display:none"%>">
                            <td colspan="3"><strong>Total</strong></td>
                            <td><strong><%= ((List<Booking>)rptTransportBookingWithoutGroup.DataSource).Sum(x=>x.Adult)%></strong></td>
                            <td><strong><%= ((List<Booking>)rptTransportBookingWithoutGroup.DataSource).Sum(x=>x.Child)%></strong></td>
                            <td><strong><%= ((List<Booking>)rptTransportBookingWithoutGroup.DataSource).Sum(x=>x.Baby)%></strong></td>
                        </tr>
                    </FooterTemplate>
                </asp:Repeater>
            </table>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Scripts" runat="server" ContentPlaceHolderID="Scripts">
    <%if (Session["ParentNeedReload"] != null && (bool)Session["ParentNeedReload"] == true)
      { %>
    <script>
        var btnSaveClicked = true;
    </script>
    <% Session["ParentNeedReload"] = false;
      }%>
    <script>
        $("#btnviewstandardrequest").click(function () {
            var $modal = $(".modal-standardrequest", window.parent.document);
            $modal.find("iframe").attr("src", $(this).attr("data-url"));
            var $btnmodalstandardrequest = $("#btnmodalstandardrequest", window.parent.document);
            $btnmodalstandardrequest.click();
        })
    </script>
</asp:Content>
