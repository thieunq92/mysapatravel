<%@ Page Language="C#" MasterPageFile="MO.Master" AutoEventWireup="true"
    CodeBehind="BookingList.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.BookingList" Title="Booking Management" %>

<%@ Register Assembly="CMS.ServerControls" Namespace="CMS.ServerControls" TagPrefix="svc" %>
<asp:Content ID="AdminContent" ContentPlaceHolderID="AdminContent" runat="server">
    <div class="form-group">
        <div class="row">
            <div class="col-xs-1">
                Booking code
            </div>
            <div class="col-xs-4">
                <asp:TextBox ID="txtBookingId" runat="server" CssClass="form-control --width-auto" placeholder="Booking code"></asp:TextBox>
            </div>
            <div class="col-xs-1 --no-padding-right">
                Customer name
            </div>
            <div class="col-xs-4">
                <asp:TextBox ID="txtCustomerName" runat="server" CssClass="form-control --width-auto" placeholder="Customer name"></asp:TextBox>
            </div>
        </div>
    </div>
    <div class="form-group">
        <div class="row">
            <div class="col-xs-1">
                Trip
            </div>
            <div class="col-xs-4">
                <asp:DropDownList ID="ddlTrip" runat="server" CssClass="form-control">
                </asp:DropDownList>
            </div>
            <div class="col-xs-1">
                Start date
            </div>
            <div class="col-xs-4">
                <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control --width-auto" placeholder="Start date (dd/mm/yyyy)" data-control="datetimepicker"></asp:TextBox>
            </div>
        </div>
    </div>
    <div class="form-group">
        <div class="row">
            <div class="col-xs-1">
                Cruise
            </div>
            <div class="col-xs-4">
                <asp:DropDownList ID="ddlCruises" runat="server" CssClass="form-control --width-auto">
                </asp:DropDownList>
            </div>
        </div>
    </div>
    <div class="form-group">
        <div class="row">
            <div class="col-xs-1 --no-padding-right">
                Booking status
            </div>
            <div class="col-xs-5">
                <div class="btn-group" role="group" aria-label="...">
                    <input type="button" class="btn btn-default" value="All" id="btnAll" />
                    <input type="button" class="btn btn-default --approved " value="Approved(<%=BookingCountApproved()%>)" id="btnApproved" />
                    <input type="button" class="btn btn-default" value="Cancelled(<%=BookingCountCancelled()%>)" id="btnCancelled" />
                    <input type="button" class="btn btn-default --pending" value="Pending(<%=BookingCountPending()%>)" id="btnPending" />
                </div>
            </div>
            <div class="col-xs-5">
                <em>Lưu ý: Số lượng ghi trong dấu "()" là số lượng booking khởi hành trong tương lai
                                    (ngày xuất phát lớn hơn ngày hiện tại), không bao gồm các điều kiện lọc khác (theo
                                    tên, theo ngày khởi hành...)</em>
            </div>
        </div>
    </div>
    <div class="form-group">
        <div class="row">
            <div class="col-xs-10">
                <asp:Button ID="buttonSearch" runat="server" OnClick="buttonSearch_Click" CssClass="btn btn-primary" />
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-xs-12">
            <div class="booking-table">
                <table class="table table-bordered table-common">
                    <tbody>
                        <tr class="active">
                            <th>Booking Code
                            </th>
                            <th>Trip
                            </th>
                            <th>Cruise
                            </th>
                            <th>Number Of Pax
                            </th>
                            <th>Customer Name
                            </th>
                            <th>Partner
                            </th>
                            <th>TA Code
                            </th>
                            <th>Status
                            </th>
                            <th>Last Edit
                            </th>
                            <th>Start Date
                            </th>
                            <th style="width:4%">Action
                            </th>
                        </tr>
                        <asp:Repeater ID="rptBookingList" runat="server" OnItemDataBound="rptBookingList_ItemDataBound">
                            <ItemTemplate>
                                <tr id="trItem" runat="server">
                                    <td>
                                        <a href='BookingView.aspx?NodeId=1&SectionId=15&bi=<%# Eval("Id")%>'>
                                            <%# DataBinder.Eval(Container.DataItem,"Id","{0:OS00000}") %></a>
                                        <asp:HyperLink ID="hlCode" runat="server"></asp:HyperLink>
                                    </td>
                                    <td>
                                        <%#Eval("Trip.Name")%>
                                    </td>
                                    <td>
                                        <%#Eval("Cruise.Name")%>
                                    </td>
                                    <td>
                                        <asp:Literal ID="ltrNumberPax" runat="server"></asp:Literal></td>
                                    <td>
                                        <asp:Literal ID="ltrCustomerName" runat="server"></asp:Literal>
                                    </td>
                                    <td>
                                        <%# Eval("Agency.Name") %>
                                    </td>
                                    <td>
                                        <%#Eval("AgencyCode") %>
                                    </td>
                                    <td>
                                        <%#Eval("Status").ToString()%>
                                    </td>
                                    <td>
                                        <%#Eval("ModifiedBy.FullName") %>
                                    </td>
                                    <td>
                                        <%# DataBinder.Eval(Container.DataItem,"StartDate","{0:dd/MM/yyyy}") %>
                                    </td>
                                    <td>
                                        <a href="BookingView.aspx?NodeId=1&SectionId=15&bi=<%# Eval("Id") %>">
                                            <i class="fa fa-edit fa-lg" aria-hidden="true" data-toggle="tooltip" data-placement="top" title="Edit"></i>
                                        </a>
                                        <asp:PlaceHolder runat="server" ID="plhNote">
                                            <i class="fa fa-info-circle fa-lg" aria-hidden="true" data-toggle="tooltip" data-placement="top" title="<%#Eval("Note") %>"></i>
                                        </asp:PlaceHolder>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
                <nav arial-label="...">
                    <div class="pager">
                        <svc:Pager ID="pagerBookings" runat="server" HideWhenOnePage="true" ControlToPage="rptBookingList"
                            PagerLinkMode="HyperLinkQueryString" PageSize="20" />
                    </div>
                </nav>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="Scripts" runat="server">
    <script>
        var datetimePicker = {
            configStartDate: function () {
                $("#<%=txtStartDate.ClientID%>").datetimepicker({
                    timepicker: false,
                    format: 'd/m/Y',
                    scrollInput: false,
                    scrollMonth: false,
                });
            }
        }

        var button = {
            btnAllClick: function () {
                $("#btnAll").click(function () {
                    window.location = "<%=UrlGetByAll()%>"
                })
            },

            btnApprovedClick: function () {
                $("#btnApproved").click(function () {
                    window.location = "<%=UrlGetByApproved()%>"
                })
            },

            btnPendingClick: function () {
                $("#btnPending").click(function () {
                    window.location = "<%=UrlGetByPending()%>"
                })
            },

            btnCancelledClick: function () {
                $("#btnCancelled").click(function () {
                    window.location = "<%=UrlGetByCancelled()%>"
                })
            }
        }

        $(function () {
            button.btnAllClick();
            button.btnApprovedClick();
            button.btnPendingClick();
            button.btnCancelledClick();
        })
    </script>
</asp:Content>
