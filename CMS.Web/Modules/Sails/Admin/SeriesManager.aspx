<%@ Page Language="C#" Title="Series Booking Management" AutoEventWireup="true" CodeBehind="SeriesManager.aspx.cs" MasterPageFile="MO.Master"
    Inherits="Portal.Modules.OrientalSails.Web.Admin.SeriesManager" %>

<%@ Register Assembly="CMS.ServerControls" Namespace="CMS.ServerControls" TagPrefix="svc" %>
<asp:Content ID="AdminContent" ContentPlaceHolderID="AdminContent" runat="server">
    <h3 class="page-header">Series management</h3>
    <div class="search-panel">
        <div class="form-group">
            <div class="row">
                <div class="col-xs-1">
                    Partner
                </div>
                <div class="col-xs-3">
                    <asp:TextBox ID="txtPartner" runat="server" CssClass="form-control" placeholder="Partner"></asp:TextBox>
                </div>
                <div class="col-xs-1">
                    Series code
                </div>
                <div class="col-xs-3">
                    <asp:TextBox ID="txtSeriesCode" runat="server" CssClass="form-control" placeholder="Series code"></asp:TextBox>
                </div>
                <div class="col-xs-1 nopadding-right">
                    Sales in charge
                </div>
                <div class="col-xs-3">
                    <asp:DropDownList ID="ddlSalesInCharge" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                        <asp:ListItem Text="-- Sales in charge --" Value=""></asp:ListItem>
                    </asp:DropDownList>
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
                        <th>Series code</th>
                        <th>Partner</th>
                        <th>Booker</th>
                        <th>Sale in charge</th>
                        <th>Cutoff date</th>
                        <th>No of days</th>
                        <th>No of booking</th>
                        <th>Status</th>
                        <th></th>
                    </tr>
                    <asp:Repeater runat="server" ID="rptListSeries" OnItemCommand="rptListSeries_ItemCommand">
                        <ItemTemplate>
                            <tr>
                                <td><a href="AddSeriesBookings.aspx?NodeId=1&SectionId=15&si=<%# Eval("Id") %>"><%# Eval("SeriesCode") %></a></td>
                                <td><%# Eval("Agency.Name")%></td>
                                <td><%# Eval("Booker.Name")%></td>
                                <td><%# Eval("Agency.Sale.UserName")%></td>
                                <td><%# Eval("CutoffDate")%> day(s)</td>
                                <td><%# Eval("NoOfDays")%> days</td>
                                <td><%# Eval("ListBooking.Count")%></td>
                                <td><%# SeriesGetStatus((int)Eval("Id"))%></td>
                                <td>
                                    <asp:LinkButton ID="btnCancel" runat="server" CommandName="cancel" CommandArgument='<%# Eval("Id") %>' Text="Cancel" OnClientClick="javascript:return confirm('Chuẩn bị cancel booking trong series này xin hãy xác nhận')">
                                        <i class="fa fa-window-close fa-2x text-danger" aria-hidden="true" title="Cancel" data-toggle="tooltip" data-placement="top"></i>
                                    </asp:LinkButton>
                                    <a href="AddSeriesBookings.aspx?NodeId=1&SectionId=15&si=<%# Eval("Id") %>">
                                        <i class="fa fa-info-circle fa-2x text-primary" aria-hidden="true" title="Details" data-toggle="tooltip" data-placement="top"></i>
                                    </a>
                                </td>

                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </div>
    </div>
    <nav arial-label="...">
        <div class="pager">
            <svc:Pager ID="pagerSeries" runat="server" HideWhenOnePage="true" ControlToPage="rptListSeries"
                PagerLinkMode="HyperLinkQueryString" PageSize="20" />
        </div>
    </nav>
</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
