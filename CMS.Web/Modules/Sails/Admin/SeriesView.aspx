<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SeriesView.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.SeriesView" MasterPageFile="MO.Master" Title="Series View" %>

<asp:Content ID="AdminContent" ContentPlaceHolderID="AdminContent" runat="server">
    <h3 class="page-header">Series Code :
        <asp:Label ID="lblSeriesCode" runat="server"></asp:Label> | <asp:Label ID="lblCreatedBy" runat="server"/>
            <asp:Label ID="lblCreatedDate" runat="server"/></h3>

    <div class="search-panel">
        <div class="form-group">
            <div class="row">
                <div class="col-xs-1">
                    TA code
                </div>
                <div class="col-xs-3">
                    <asp:TextBox runat="server" CssClass="form-control" ID="txtTACode" placeholder="TA code"></asp:TextBox>
                </div>
                <div class="col-xs-1">
                    Booking code
                </div>
                <div class="col-xs-3">
                    <asp:TextBox runat="server" CssClass="form-control" ID="txtBookingCode" placeholder="Booking code"></asp:TextBox>
                </div>
                <div class="col-xs-1">
                    Start date
                </div>
                <div class="col-xs-3">
                    <asp:TextBox runat="server" CssClass="form-control" ID="txtStartDate" placeholder="Start date"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="form-group">
            <div class="row">
                <div class="col-xs-1">
                </div>
                <div class="col-xs-11">
                    <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" CssClass="btn btn-primary" />
                </div>
            </div>
        </div>
    </div>

    <div class="table-panel">
        <div class="row">
            <div class="col-xs-12">
                <table class="table table-bordered table-hover table-common">
                    <tr class="active">
                        <th>STT</th>
                        <th>TA code</th>
                        <th>Booking code</th>
                        <th>Start date</th>
                        <th>Trip</th>
                        <th>Cruise</th>
                        <th>Pax</th>
                        <th>Cabins</th>
                        <th>Cutoff date</th>
                        <th>Special request</th>
                        <th>Status</th>
                        <th>Last edited</th>
                        <th></th>
                    </tr>
                    <asp:Repeater runat="server" ID="rptListBooking" OnItemCommand="rptListBooking_ItemCommand">
                        <ItemTemplate>
                            <tr class="<%# GetSeriesBackground((int)Eval("Status"))%>">
                                <td><%# Container.ItemIndex + 1%></td>
                                <td><%# Eval("AgencyCode")%></td>
                                <td><a href="BookingView.aspx?NodeId=1&SectionId=15&bi=<%# Eval("Id") %>">OS<%# Eval("Id") %></td>
                                <td><%# DataBinder.Eval(Container.DataItem, "StartDate", "{0:dd/MM/yyyy}")%></td>
                                <td><%# Eval("Trip.TripCode") %></td>
                                <td><%# Eval("Cruise.name")%></td>
                                <td>Adults : <%# Eval("Adult")%></br> 
                                    Childs : <%# Eval("Child")%></br>
                                    Baby : <%# Eval("Baby")%></td>
                                <td><%# Eval("RoomName") %>
                                </td>
                                <td><%# Eval("Series.CutoffDate")%> day(s)</td>
                                <td><%# Eval("SpecialRequest")%></td>
                                <td><%# Eval("Status") %></td>
                                <td><%# Eval("ModifiedBy.Name") + " " + DataBinder.Eval(Container.DataItem, "ModifiedDate", "{0:dd/MM/yyyy HH:mm}") %></td>
                                <td>
                                    <a href="BookingView.aspx?NodeId=1&SectionId=15&bi=<%# Eval("Id") %>">
                                        <i class="fa fa-edit fa-2x text-primary" aria-hidden="true" title="Edit" data-toggle="tooltip" data-placement="top"></i>
                                    </a>
                                    <asp:LinkButton ID="btnCancel" runat="server" CommandName="cancel" CommandArgument='<%# Eval("Id") %>' OnClientClick="javascript:return confirm('Chuẩn bị cancel booking này xin hãy xác nhận')">
                                        <i class="fa fa-window-close fa-2x text-danger" aria-hidden="true" title="Cancel" data-toggle="tooltip" data-placement="top" ></i>
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="btnApproved" runat="server" CommandName="approved" CommandArgument='<%# Eval("Id") %>' OnClientClick="javascript:return confirm('Chuẩn bị approved booking này xin hãy xác nhận')">
                                        <i class="fa fa-check fa-2x text-success" aria-hidden="true" title="Approved" data-toggle="tooltip" data-placement="top"></i>
                                    </asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="Scripts" runat="server">
    <script>
        $("#<%= txtStartDate.ClientID%> ").datetimepicker({
            timepicker: false,
            format: 'd/m/Y',
            scrollInput: false,
            scrollMonth: false,
        })
    </script>
</asp:Content>
