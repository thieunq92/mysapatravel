<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="MO.Master" CodeBehind="DashBoard.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.DashBoard" %>

<%@ Import Namespace="Portal.Modules.OrientalSails.Domain" %>
<asp:Content ID="Head" ContentPlaceHolderID="Head" runat="server">
    <title>Dashboard</title>
    <!-- blueimp Gallery styles -->
    <link rel="stylesheet" href="https://blueimp.github.io/Gallery/css/blueimp-gallery.min.css">
</asp:Content>
<asp:Content ID="AdminContent" ContentPlaceHolderID="AdminContent" runat="server">
    <div class="row">
        <div class="col-xs-12">
            <h2 class="--text-bold">Xin chào <%= CurrentUser.FullName %>, chúc bạn một ngày làm việc đầy năng lượng</h2>
        </div>
    </div>
    <div class="row">
        <div class="col-xs-4 col__buttons --no-padding-right" style="width: auto; padding-right: 5px">
            <a class="btn btn-primary" href="AddBookingRandom.aspx?NodeId=1&SectionId=15">Add booking</a>
            <a class="btn btn-primary" href="AddSeriesBookings.aspx?NodeId=1&SectionId=15">Add series</a>
            <a class="btn btn-primary" href="" data-toggle="modal" data-target="#addMeetingModal" onclick="$('#addMeetingModal .modal-title').html('Add meeting / Problem report');clearFormMeeting();">Add meeting</a>
            <a class="btn btn-primary" href="AgencyEdit.aspx?NodeId=1&SectionId=15">Add partner</a>
        </div>
        <div class="col-xs-2 --no-padding-left">
            <div class="input-group">
                <asp:TextBox runat="server" ID="txtPartnerSearch" AutoPostBack="true" OnTextChanged="txtPartnerSearch_TextChanged" CssClass="form-control" placeholder="Partner search" />
                <span id="basic-addon1" class="input-group-addon">
                    <i class="fas fa-search"></i>
                </span>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-xs-10">
            <div class="row">
                <div class="col-xs-6 col__today-booking --no-padding-left">
                    <h4 class="--text-bold">Your today bookings - <a href="BookingReport.aspx?NodeId=1&SectionId=15&d=<%= DateTime.Today.ToString("dd/MM/yyyy")%>">
                        <%= DateTime.Today.ToString("dd/MM/yyyy")%></a></h4>
                    <table class="table table-bordered table-common table__total">
                        <tbody>
                            <tr class="header active">
                                <th style="width: 13%">Code</th>
                                <th style="width: 9%">Trip</th>
                                <th style="width: 8%">NoP</th>
                                <th style="width: 17%">Revenue</th>
                                <th>Agency</th>
                                <th style="width: 8%">View</th>
                            </tr>
                            <asp:Repeater runat="server" ID="rptTodayBookings">
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <a href="BookingView.aspx?NodeId=1&SectionId=15&bi=<%#((Booking)Container.DataItem).Id%>">
                                                <%# ((Booking)Container.DataItem).BookingIdOS %>
                                        </td>
                                        <td><%# ((Booking)Container.DataItem).Trip != null ? ((Booking)Container.DataItem).Trip.TripCode : "" %></td>
                                        <td><%# ((Booking)Container.DataItem).Pax %></td>
                                        <td class="--text-right"><%# GetTotalAsString((Booking)Container.DataItem)%>₫</td>
                                        <td><a href="AgencyView.aspx?NodeId=1&SectionId=15&agencyid=<%# ((Booking)Container.DataItem).Agency != null ? ((Booking)Container.DataItem).Agency.Id : 0 %> ">
                                            <%# ((Booking)Container.DataItem).Agency != null ? ((Booking)Container.DataItem).Agency.Name : "" %></a></td>
                                        <td><a href="" data-toggle="tooltip" data-toggle-hide="false" title="<%# ((Booking)Container.DataItem).SpecialRequest%>">View</a></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                            <% 
                                if (rptTodayBookings != null && rptTodayBookings.DataSource != null && ((List<Booking>)rptTodayBookings.DataSource).Count <= 0)
                                {
                            %>
                            <tr>
                                <td colspan="100%">No records found</td>
                            </tr>
                            <%
                                }
                            %>
                        </tbody>
                        <tfoot>
                            <tr>
                                <td colspan="2"></td>
                                <td class="td__total --text-bold">Total</td>
                                <td class="td__total --text-right"><%= rptTodayBookings != null && rptTodayBookings.DataSource != null ? GetTotalOfBookings((IEnumerable<Booking>)rptTodayBookings.DataSource) : "0"%>₫</td>
                            </tr>
                        </tfoot>
                    </table>
                </div>
                <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="col-xs-6 col__cruise-avaibility --no-padding-left">
                            <div class="row">
                                <h4 class="--text-bold col-xs-7 --no-padding-leftright">Cruise availability next 7 days</h4>
                                <div class="col-xs-5 --no-padding-leftright">
                                    <div class="input-group">
                                        <asp:TextBox runat="server" ID="txtDateSearching" placeholder="Cruise availability search" CssClass="form-control" data-control="datetimepicker" OnTextChanged="txtDateSearching_TextChanged" AutoPostBack="true" />
                                        <span id="basic-addon1" class="input-group-addon">
                                            <i class="fas fa-search"></i>
                                        </span>
                                    </div>
                                </div>
                            </div>
                            <table class="table table-bordered table-common table__cruise-avaiability">
                                <asp:Repeater ID="rptCruiseAvaibility" runat="server" OnItemDataBound="rptCruiseAvaibility_ItemDataBound">
                                    <HeaderTemplate>
                                        <tr class="header active">
                                            <th style="width: 16%">Date</th>
                                            <asp:Literal ID="ltrHeader" runat="server"></asp:Literal>
                                        </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Literal ID="ltrRow" runat="server"></asp:Literal>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <tr>
                                </tr>
                            </table>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="row">
                <div class="col-xs-8 --no-padding-leftright">
                    <h4 class="--text-bold">Your new bookings received Today</h4>
                    <table class="table table-bordered table-common table__total">
                        <tbody>
                            <tr class="header active">
                                <th style="width: 10%">Code</th>
                                <th style="width: 8%">Trip</th>
                                <th style="width: 6%">NoP</th>
                                <th style="width: 15%">Revenue</th>
                                <th style="width: 20%">Start date</th>
                                <th style="width: 8%">View</th>
                                <th>Created by</th>
                            </tr>
                            <asp:Repeater runat="server" ID="rptNewBookings">
                                <ItemTemplate>
                                    <tr>
                                        <td><%# ((Booking)Container.DataItem).BookingIdOS %></td>
                                        <td><%# ((Booking)Container.DataItem).Trip != null ? ((Booking)Container.DataItem).Trip.TripCode : "" %></td>
                                        <td><%# ((Booking)Container.DataItem).Pax %></td>
                                        <td class="--text-right"><%# GetTotalAsString((Booking)Container.DataItem)%>₫</td>
                                        <td><%# ((Booking)Container.DataItem).StartDate.ToString("dd/MM/yyyy") %></td>
                                        <td><a href="" data-toggle="tooltip" title="<%# ((Booking)Container.DataItem).SpecialRequest%>">View</a></td>
                                        <td><%# ((Booking)Container.DataItem).CreatedBy != null ? ((Booking)Container.DataItem).CreatedBy.FullName : ""%></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                            <% 
                                if (!IsPostBack)
                                {
                                    if (rptNewBookings != null && rptNewBookings.DataSource != null && ((List<Booking>)rptNewBookings.DataSource).Count <= 0)
                                    {
                            %>
                            <tr>
                                <td colspan="100%">No records found</td>
                            </tr>
                            <%
                                    }
                                }
                            %>
                        </tbody>
                        <tfoot>
                            <tr>
                                <td colspan="2"></td>
                                <td class="td__total --text-bold">Total</td>
                                <td class="td__total --text-right"><%= rptNewBookings != null && rptNewBookings.DataSource != null ? GetTotalOfBookings((IEnumerable<Booking>)rptNewBookings.DataSource) : "0"%>₫</td>
                            </tr>
                        </tfoot>
                    </table>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-12 --no-padding-left area" ng-controller="getActivityByIdController">
                    <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <h4 class="--text-bold area__title">Most recent meetings&nbsp&nbsp&nbsp<a href="" data-toggle="modal" data-target="#addMeetingModal" onclick="$('#addMeetingModal .modal-title').html('Add meeting / Problem report');clearFormMeeting();"><i class="fas fa-plus"></i></a></h4>
                            <div class="form-group area__title-control-group">
                                <div class="row">
                                    <div class="col-xs-2 --no-padding-right --width-auto">
                                        <asp:Button runat="server" ID="btnSearch" OnClick="btnSearch_Click" Text="Search" CssClass="btn btn-primary" />
                                        <asp:Button runat="server" ID="btnExport" OnClick="btnExport_Click" Text="Export" CssClass="btn btn-primary"></asp:Button>
                                    </div>
                                    <div class="col-xs-4 --no-padding-right">
                                        <asp:TextBox runat="server" ID="txtToRecentMeetingSearch" CssClass="form-control" placeholder="To (dd/MM/yyyy)" data-control="datetimepicker" />
                                    </div>
                                    <div class="col-xs-4 --no-padding-leftright">
                                        <asp:TextBox runat="server" ID="txtFromRecentMeetingSearch" CssClass="form-control" placeholder="From (dd/MM/yyyy)" data-control="datetimepicker" />
                                    </div>
                                </div>
                            </div>
                            <table class="table table-bordered table-common">
                                <tbody>
                                    <tr class="header active">
                                        <th style="width: 8%">Date</th>
                                        <th style="width: 25%">Agency</th>
                                        <th style="width: 10%">Contact</th>
                                        <th style="width: 46%">View meetings</th>
                                        <th style="width: 6%">Problem</th>
                                        <th style="width: auto"></th>
                                    </tr>
                                    <asp:Repeater ID="rptRecentMeetings" runat="server" OnItemCommand="rptRecentMeetings_ItemCommand" OnItemDataBound="rptRecentMeetings_ItemDataBound">
                                        <ItemTemplate>
                                            <tr class="<%# ((Activity)Container.DataItem).Type == "Problem Report"  ? "--problem" :"" %>">
                                                <td>
                                                    <i class="fa fa-lg fa-star star star--blink star--yellow <%# ((Activity)Container.DataItem).NeedManagerAttention == false ? "hidden" : "" %>"></i>
                                                    <%# ((Activity)Container.DataItem).DateMeeting != null ? ((Activity)Container.DataItem).DateMeeting.ToString("dd/MM/yyyy"):""  %></td>
                                                <td><a href="AgencyView.aspx?NodeId=1&SectionId=15&agencyid=<%#((Activity)Container.DataItem).Params %>">
                                                    <%# AgencyGetById(((Activity)Container.DataItem).Params)!= null ? AgencyGetById(((Activity)Container.DataItem).Params).Name : "" %></td>
                                                <td><%# AgencyContactGetById(((Activity)Container.DataItem).ObjectId) != null ? AgencyContactGetById(((Activity)Container.DataItem).ObjectId).Name : ""  %></td>
                                                <td class="--text-left"><%# ((Activity)Container.DataItem).Note%> </td>
                                                <td>
                                                    <%# !String.IsNullOrEmpty(((Activity)Container.DataItem).Problems) ? ((Activity)Container.DataItem).Problems.Replace(",","<br/>") : ""%>
                                                </td>
                                                <td class="--text-right">
                                                    <div class="button-group">
                                                        <asp:LinkButton runat="server" ID="lbtDownload" Visible="<%# !String.IsNullOrEmpty(((Activity)Container.DataItem).Attachment) ? true : false %>" CommandName="Download" CommandArgument="<%#((Activity)Container.DataItem).Attachment + ',' + ((Activity)Container.DataItem).AttachmentContentType %>">
                                                        <i class="fa fa-lg fa-file-download icon icon__download" data-toggle="tooltip" title="Download"></i></asp:LinkButton>
                                                        <a href="" data-toggle="modal" data-target="#addMeetingModal" ng-click="getActivityById(<%#(((Activity)Container.DataItem).Id)%>)"
                                                            onclick="$('#addMeetingModal .modal-title').html('Edit <%#(((Activity)Container.DataItem).Type)%>');clearFormMeeting();">
                                                            <i class="fa fa-lg fa-edit icon icon__edit" data-toggle="tooltip" title="Edit"></i>
                                                        </a>
                                                    </div>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <%     
                                        if (rptRecentMeetings != null && rptRecentMeetings.DataSource != null && ((List<Activity>)rptRecentMeetings.DataSource).Count() <= 0)
                                        {
                                    %>
                                    <tr>
                                        <td colspan="100%">No records found</td>
                                    </tr>
                                    <%
                                        }                 
                                    %>
                                </tbody>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <h4 class="--text-bold --text-italic"><a href="ViewMeetings.aspx?NodeId=1&SectionId=15&sales=<%= CurrentUser.Id %>">View more meetings</a></h4>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-12 --no-padding-left">
                    <h4 class="--text-bold">Agencies not visited / updated last 2 months</h4>
                    <table class="table table-bordered table-common">
                        <asp:Repeater runat="server" ID="rptAgencyNotVisited">
                            <ItemTemplate>
                                <tr>
                                    <td><%# Container.ItemIndex %></td>
                                    <td style="width: 27%"><a href="AgencyView.aspx?NodeId=1&SectionId=15&agencyid=<%# Eval("AgencyId") %>"><%# Eval("Name")%></td>
                                    <td><%# Eval("LastMeeting")%></td>
                                    <td class="--text-left"><%# Eval("Note")%></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </div>
            </div>
        </div>
        <div class="col-xs-2 --no-padding-leftright">
            <div class="row">
                <div class="col-xs-12 col__golden-day --no-padding-leftright">
                    <h4 class="golden-day__header --text-bold --no-margin-bottom">Golden days</h4>
                    <input type="hidden" data-control="datepicker-inline" id="golden-day">
                </div>
            </div>
            <div class="row">
                <div class="col-xs-12 --no-padding-leftright">
                    <h4 class="--text-bold" style="display: inline-block; max-width: 50%">Month summary</h4>
                    <asp:DropDownList runat="server" ID="ddlMonthSearching" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlMonthSearching_SelectedIndexChanged" Style="width: 21%; padding: 0; display: inline-block; position: relative; bottom: 7px">
                    </asp:DropDownList>
                    <asp:DropDownList runat="server" ID="ddlYearSearching" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlYearSearching_SelectedIndexChanged" Style="width: 24%; padding: 0; display: inline-block; position: relative; bottom: 7px">
                    </asp:DropDownList>
                    <table class="table table-borderless table__archivement">
                        <tr>
                            <td>Number of pax:</td>
                            <td>
                                <asp:Label runat="server" ID="lblNumberOfPax" /></td>
                        </tr>
                        <tr>
                            <td>Number of bookings:</td>
                            <td>
                                <asp:Label runat="server" ID="lblNumberOfBookings" /></td>
                        </tr>
                        <tr>
                            <td>Total revenue:</td>
                            <td>
                                <asp:Label runat="server" ID="lblTotalRevenue" />₫</td>
                        </tr>
                        <tr>
                            <td>Agencies visited:</td>
                            <td>
                                <asp:Label runat="server" ID="lblAgenciesVisited" /></td>
                        </tr>
                        <tr>
                            <td>Meeting reports:</td>
                            <td>
                                <asp:Label runat="server" ID="lblMeetingReports" /></td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-12 --no-padding-leftright">
                    <h4 class="--text-bold">Top 10 partners in - <%= DateTime.Now.ToString("MMMM").Substring(0,3)%> </h4>
                    <table class="table table-borderless table__top10partner">
                        <asp:Repeater runat="server" ID="rptTop10Partner">
                            <ItemTemplate>
                                <tr>
                                    <td><%# Container.ItemIndex + 1 %></td>
                                    <td><a href="AgencyView.aspx?NodeId=1&SectionId=15&agencyid=<%# Eval("AgencyId") %>"><%# Eval("AgencyName")%></td>
                                    <td><%# Eval("NumberOfPax")%></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-12 --no-padding-leftright">
                    <h4 class="--text-bold">Agencies send no bookings last 3 months</h4>
                    <table class="table table-borderless table__agencies-no-bookings">
                        <asp:Repeater ID="rptAgenciesSendNoBookings" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td><%# Container.ItemIndex + 1%></td>
                                    <td><a href="AgencyView.aspx?NodeId=1&SectionId=15&agencyid=<%# Eval("Agency.Id")%>"><%# Eval("Agency.Name")%></td>
                                    <td><%# ((DateTime)Eval("CreatedDate")).ToString("dd/MM/yyyy")%></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade meeting-modal" id="addMeetingModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog meeting-modal__modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title --text-bold" id="myModalLabel">Add meeting / Problem report</h4>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <div class="row" data-hidactivityidclientid="<%= hidActivityId.ClientID %>" id="hidActivityIdClientId">
                            <asp:HiddenField runat="server" ID="hidActivityId" />
                            <div class="col-xs-2 --no-padding-leftright">
                                Agency
                            </div>
                            <div class="col-xs-10 --no-padding-leftright">
                                <input type="text" data-id="hidGuideId" style="display: none" name="txtAgencyId" />
                                <input type="text" id="txtAgency" placeholder="Select agency" readonly class="form-control" data-toggle="modal" data-target=".modal-selectGuide"
                                    data-url="AgencySelectorPage.aspx?NodeId=1&SectionId=15" onclick="setTxtGuideClicked(this)"
                                    data-id="txtName" name="txtAgency">
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <div class="col-xs-2 --no-padding-leftright">
                                Contact
                            </div>
                            <div class="col-xs-4 --no-padding-leftright">
                                <select class="form-control" name="ddlContact" data-id="ddlContact">
                                    <option value="0">-- Contact --</option>
                                </select>
                            </div>
                            <div class="col-xs-2 --no-padding-left --text-right">
                                Position
                            </div>
                            <div class="col-xs-4 --no-padding-leftright">
                                <asp:TextBox runat="server" ID="txtPosition" CssClass="form-control" placeholder="Position" disabled="disabled" data-id="txtPosition" />
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <div class="col-xs-2 --no-padding-leftright">
                                Date meeting
                            </div>
                            <div class="col-xs-4 --no-padding-leftright">
                                <asp:TextBox runat="server" ID="txtDateMeeting" CssClass="form-control" placeholder="Date meeting" data-control="datetimepicker" data-id="txtDateMeeting" autocomplete="off" />
                            </div>
                            <div class="col-xs-2 --no-padding-left --text-right">
                                Type
                            </div>
                            <div class="col-xs-4 --no-padding-leftright">
                                <asp:DropDownList runat="server" ID="ddlType" CssClass="form-control" data-id="ddlType">
                                    <asp:ListItem Text="Meeting">Meeting</asp:ListItem>
                                    <asp:ListItem Text="Problem Report">Problem Report</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="form-group" id="problem-group">
                        <div class="row">
                            <div class="col-xs-2 --no-padding-leftright">
                                Cruise
                            </div>
                            <div class="col-xs-4 --no-padding-leftright">
                                <asp:DropDownList runat="server" ID="ddlCruise" AppendDataBoundItems="true" CssClass="form-control" data-id="ddlCruise">
                                    <asp:ListItem Value="0" Text="-- Cruise --"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-xs-offset-1 col-xs-5 --no-padding-leftright --text-right checkbox-group">
                                <fieldset class="--reset-this" style="padding-top: 5px; padding-bottom: 0">
                                    <legend class="--reset-this" style="line-height: 0">Problems</legend>
                                    <label for="<%= chkFood.ClientID %>" class="--text-normal">
                                        Food
                                <asp:CheckBox runat="server" ID="chkFood" Text="" CssClass="checkbox-group__horizontal" data-id="chkFood"></asp:CheckBox>
                                    </label>
                                    <label for="<%= chkCabin.ClientID %>" class="--text-normal">
                                        Cabin
                                <asp:CheckBox runat="server" ID="chkCabin" Text="" CssClass="checkbox-group__horizontal" data-id="chkCabin"></asp:CheckBox>
                                    </label>
                                    <label for="<%= chkGuide.ClientID %>" class="--text-normal">
                                        Guide
                                <asp:CheckBox runat="server" ID="chkGuide" Text="" CssClass="checkbox-group__horizontal" data-id="chkGuide"></asp:CheckBox>
                                    </label>
                                    <label for="<%= chkBus.ClientID %>" class="--text-normal">
                                        Bus
                                <asp:CheckBox runat="server" ID="chkBus" Text="" CssClass="checkbox-group__horizontal" data-id="chkBus"></asp:CheckBox>
                                    </label>
                                    <label for="<%= chkOthers.ClientID %>" class="--text-normal">
                                        Others
                                <asp:CheckBox runat="server" ID="chkOthers" Text="" CssClass="checkbox-group__horizontal" data-id="chkOthers"></asp:CheckBox>
                                    </label>
                                </fieldset>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <div class="col-xs-2 --no-padding-leftright">
                                Note
                            </div>
                            <div class="col-xs-10 --no-padding-leftright">
                                <asp:TextBox runat="server" ID="txtNote" CssClass="form-control" TextMode="MultiLine" Rows="12" placeholder="Note" Text="" data-id="txtNote" />
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <div class="col-xs-2 --no-padding-leftright">
                                Attachment
                            </div>
                            <div class="col-xs-10 --no-padding-leftright">
                                <asp:FileUpload runat="server" ID="fuAttachment"></asp:FileUpload>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <div class="col-xs-12 --no-padding-leftright --text-right">
                                <label for="<%= chkNeedManagerAttention.ClientID %>" class="--text-normal">Need manager's immediate attention</label>
                                <asp:CheckBox runat="server" ID="chkNeedManagerAttention" Text="" data-id="chkNeedManagerAttention"></asp:CheckBox>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    <asp:Button runat="server" ID="btnSave" OnClick="btnSave_Click" Text="Save" CssClass="btn btn-primary" OnClientClick="return checkDouble(this)" />
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade modal-selectGuide" tabindex="-1" role="dialog" aria-labelledby="gridSystemModalLabel">
        <div class="modal-dialog" role="document" style="width: 1230px">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h3 class="modal-title">Select agency</h3>
                </div>
                <div class="modal-body">
                    <iframe frameborder="0" width="1200" scrolling="no" onload="resizeIframe(this)" src=""></iframe>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="Scripts" runat="server">
    <script type="text/javascript" src="/modules/sails/admin/dashboardcontroller.js"></script>
    <script>
        $('.modal-selectGuide').on('shown.bs.modal', function () {
            $(".modal-selectGuide iframe").attr('src', 'AgencySelectorPage.aspx?NodeId=1&SectionId=15')
        })

        var txtGuideClicked = null;
        var rowGuideSelected = null;
        var txtGuideNameSelected = null;
        var txtPhoneSelected = null;
        var hidGuideIdSelected = null;
        function setTxtGuideClicked(txtGuide) {
            txtGuideClicked = txtGuide;
            if (typeof (txtGuideClicked) != "undefined") {
                rowGuideSelected = $(txtGuideClicked).closest(".row");
            }
        }

        var selectGuideIframe = $(".modal-selectGuide iframe");
        selectGuideIframe.on("load", function () {
            //giữ vị trí của scroll khi sang trang mới -- chức năng của phần selectguide
            if (window.name.search('^' + location.hostname + '_(\\d+)_(\\d+)_') == 0) {
                var name = window.name.split('_');
                $(".modal-selectGuide").scrollLeft(name[1]);
                $(".modal-selectGuide").scrollTop(name[2]);
                window.name = name.slice(3).join('_');
            }
            $(".pager a", selectGuideIframe.contents()).click(function () {
                window.name = location.hostname + "_" + $(".modal-selectGuide").scrollLeft() + "_" + $(".modal-selectGuide").scrollTop() + "_";
            })
            //--

            //chức năng select agency bằng popup
            $("[data-id = 'txtName']", selectGuideIframe.contents()).click(function () {
                if (typeof (txtGuideClicked) != "undefined") {
                    $(txtGuideClicked).val($(this).text())
                }
                if (typeof (rowGuideSelected) != "undefined") {
                    txtGuideNameSelected = $(rowGuideSelected).find("[data-id='txtName']");
                    txtPhoneSelected = $(rowGuideSelected).find("[data-id='txtPhone']");
                    hidGuideIdSelected = $(rowGuideSelected).find("[data-id='hidGuideId']");
                }
                if (typeof (txtPhoneSelected) != "undefined") {
                    $(txtPhoneSelected).val($(this).attr("data-phone"))
                }
                if (typeof (hidGuideIdSelected) != "undefined") {
                    $(hidGuideIdSelected).val($(this).attr("data-agencyid"));
                }
                if (typeof (txtGuideNameSelected) != "undefined") {
                    $(txtGuideNameSelected).val($(this).text())
                }
                $('.modal-selectGuide').modal('hide')
                $(hidGuideIdSelected).trigger('input');
                $(hidGuideIdSelected).trigger('change');
                $(txtGuideNameSelected).trigger('input');
                $(txtGuideNameSelected).trigger('change');
            });
            //--
        })
    </script>
    <script>
        $("[name = txtAgencyId]").change(function () {
            $('[name = ddlContact]').find('option:not(:first)').remove();
            $.ajax({
                type: 'POST',
                url: 'WebMethod/DashBoardWebService.asmx/AgencyContactGetByAgencyId',
                data: "{ 'ai': '" + $(this).val() + "'}",
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
            }).done(function (data) {
                var agencyContacts = JSON.parse(data.d);
                $.each(agencyContacts, function (i, agencyContact) {
                    $('[name = ddlContact]')
                        .append($('<option>', {
                            value: agencyContact.Id,
                            text: agencyContact.Name,
                            _position: agencyContact.Position,
                        }));
                });
            })
        })
        $('[name = ddlContact]').change(function () {
            $('#<%= txtPosition.ClientID %>').val($(this).find('option:selected').attr('_position'));
        })
    </script>
    <script>
        $(function () {
            $('#golden-day').datetimepicker({
                timepicker: false,
                format: 'd/m/Y',
                inline: true,
                value: '<%=DateTime.Today.ToString("dd/MM/yyyy")%>',
                onGenerate: function (ct, $i) {
                    var isGenerated = false;
                    hightlightedDates(ct, $i, this, isGenerated);
                    isGenerated = true;
                },
                onChangeMonth: function (ct, $i) {
                    var isGenerated = false;
                    hightlightedDates(ct, $i, this, isGenerated);
                    $('.tooltip').remove();
                },
            });
        });
        function hightlightedDates(ct, $i, control, isGenerated) {
            if (isGenerated) {
                return;
            }
            $(".col__golden-day .xdsoft_date").removeClass('xdsoft_current').removeClass('xdsoft_today');
            var dd = ct.getDate();
            var MM = ct.getMonth() + 1;
            var yyyy = ct.getFullYear();
            var date = MM + '/' + dd + '/' + yyyy;
            $.ajax({
                type: 'POST',
                url: 'WebMethod/DashBoardWebService.asmx/GoldenDayGetAllInMonthByDate',
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                data: '{"date":"' + date + '"}',
            }).done(function (data) {
                var goldenDays = JSON.parse(data.d);
                var highlightedDates = new Array();
                for (var i = 0; i < goldenDays.length; i++) {
                    var date = new Date(goldenDays[i].Date);
                    var dateAsString = date.getFullYear() + '/' + (date.getMonth() + 1) + '/' + date.getDate();
                    var policy = goldenDays[i].Policy === null ? '' : goldenDays[i].Policy;
                    var highlightedDate = dateAsString + ',' + policy.replace(new RegExp(',', 'g'), '\u201A') + ',hightlight__golden-day';
                    console.log(highlightedDate);
                    highlightedDates.push(highlightedDate);
                }
                control.setOptions({
                    highlightedDates: highlightedDates,
                    value: ct,
                    onGenerate: function (ct) {
                        $('.col__golden-day .xdsoft_date').removeClass('xdsoft_current').removeClass('xdsoft_today');
                        goldenDayGenerateTooltip();
                        $('.xdsoft_date').click(function () {
                            $('.tooltip').remove();   
                        });               
                        $('.xdsoft_date').mouseleave(function () {
                            $('.tooltip').remove();   
                        });      
                    }
                });
            })
        }
    </script>
    <script>
        function goldenDayGenerateTooltip() {
            $('.col__golden-day .hightlight__golden-day').tooltip({
                container: 'body',
                placement: 'left'
            });
        }
    </script>
    <script>
        $('#<%=fuAttachment.ClientID%>').change(function (e) {
            var uploadSize = e.target.files[0].size;
            if (uploadSize >= 26214400) {
                e.target.value = "";
                alert("File upload too large. Please send file have size <= 25MB");
            }
        })
    </script>
    <script>
        $(document).ready(function () {
            $('#<%= ddlType.ClientID %>').change(function () {
                if ($(this).val() == 'Meeting') $('#problem-group').hide();
                if ($(this).val() == 'Problem Report') $('#problem-group').show();
            })
        })
    </script>
    <script>
        function clearFormMeeting() {
            clearForm($('#addMeetingModal .modal-content'));
            $('[data-id=ddlType]').val('Meeting');
            $('[data-id=ddlType]').trigger('change')
        }
    </script>
    <script>
        $(document).ready(function () {
            $("#aspnetForm").validate({
                rules: {
                    txtAgency: "required",
                    <%= txtDateMeeting.UniqueID%>: "required",
                    <%= txtNote.UniqueID%> : "required",
                },
                messages: {
                    txtAgency: "Yêu cầu chọn một Agency",
                    <%= txtDateMeeting.UniqueID%>: "Yêu cầu chọn ngày",
                    <%= txtNote.UniqueID%>:"Yêu cầu điền Note",
                },
                errorElement: "em",
                errorPlacement: function (error, element) {
                    error.addClass("help-block");

                    if (element.prop("type") === "checkbox") {
                        error.insertAfter(element.parent("label"));
                    } else {
                        error.insertAfter(element);
                    }

                    if (element.siblings("span").prop("class") === "input-group-addon") {
                        error.insertAfter(element.parent()).css({ color: "#a94442" });
                    }
                },
                highlight: function (element, errorClass, validClass) {
                    $(element).closest("div").addClass("has-error").removeClass("has-success");
                },
                unhighlight: function (element, errorClass, validClass) {
                    $(element).closest("div").removeClass("has-error");
                }
            })

        })
    </script>
</asp:Content>
