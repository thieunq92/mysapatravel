<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DashBoardManager.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.DashBoardManager"
    MasterPageFile="MO.Master" %>

<%@ Import Namespace="Portal.Modules.OrientalSails.Domain" %>
<%@ Import Namespace="Portal.Modules.OrientalSails.DataTransferObject" %>
<%@ Import Namespace="Portal.Modules.OrientalSails.Web.Util" %>
<asp:Content ID="Head" ContentPlaceHolderID="Head" runat="server">
    <title>Dashboard Manager</title>
</asp:Content>
<asp:Content ID="AdminContent" ContentPlaceHolderID="AdminContent" runat="server">
    <div class="row">
        <div class="col-xs-12">
            <h2 class="--text-bold">Xin chào <%= CurrentUser.FullName %>, chúc bạn một ngày làm việc đầy năng lượng</h2>
        </div>
    </div>
    <div class="row">
        <div class="col-xs-4 col__buttons --no-padding-right" style="width: auto; padding-right: 5px">
            <a class="btn btn-primary" onclick="goToByScroll('viewmeetings')">View meetings</a>
            <a class="btn btn-primary" onclick="goToByScroll('notvisited')">Not visited</a>
            <a class="btn btn-primary" onclick="goToByScroll('nobookings')">No bookings</a>
            <a class="btn btn-primary" href="GoldenDayCreateCampaign.aspx?NodeId=1&SectionId=15">Create golden days</a>
            <a class="btn btn-primary" href="GoldenDayListCampaign.aspx?NodeId=1&SectionId=15">View all campaign</a>
        </div>
    </div>
    <div class="row">
        <div class="col-xs-6">
            <div class="row">
                <div class="col-xs-12 --no-padding-leftright">
                    <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <h4 class="--text-bold" style="margin-bottom: 0">Month summary 
                        <asp:DropDownList runat="server" ID="ddlMonthSearching" AutoPostBack="true" CssClass="form-control --dropdown-inline" OnSelectedIndexChanged="ddlMonthSearching_SelectedIndexChanged">
                        </asp:DropDownList>
                                <asp:DropDownList runat="server" ID="ddlYearSearching" AutoPostBack="true" CssClass="form-control --dropdown-inline" OnSelectedIndexChanged="ddlYearSearching_SelectedIndexChanged">
                                </asp:DropDownList></h4>
                            <table class="table table-bordered table-common table__total ">
                                <tbody>
                                    <tr>
                                        <td></td>
                                        <asp:Repeater ID="rptSales" runat="server">
                                            <ItemTemplate>
                                                <td class="active --text-bold"><%# ((SalesMonthSummaryDTO)Container.DataItem).SalesUserName %></td>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tr>
                                    <tr>
                                        <td class="active --text-bold">M Reports</td>
                                        <asp:Repeater ID="rptMeetingReports" runat="server">
                                            <ItemTemplate>
                                                <td class="--text-right"><%# ((SalesMonthSummaryDTO)Container.DataItem).MeetingReports.ToString("#,0")%></td>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tr>
                                    <tr>
                                        <td class="active --text-bold">NoB</td>
                                        <asp:Repeater ID="rptNoOfBookings" runat="server">
                                            <ItemTemplate>
                                                <td class="--text-right"><%# ((SalesMonthSummaryDTO)Container.DataItem).NumberOfBookings.ToString("#,0")%></td>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tr>
                                    <tr>
                                        <td class="active --text-bold">Revenue</td>
                                        <asp:Repeater ID="rptRevenueInUSD" runat="server">
                                            <ItemTemplate>
                                                <td class="--text-right"><%# ((SalesMonthSummaryDTO)Container.DataItem).Revenue.ToString("#,0")%></td>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tr>
                                    <tr>
                                        <td class="active --text-bold">NoP 2D</td>
                                        <asp:Repeater ID="rptNoOfPax2Days" runat="server">
                                            <ItemTemplate>
                                                <td class="--text-right"><%# ((SalesMonthSummaryDTO)Container.DataItem).NumberOfPax2Days.ToString("#,0")%></td>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tr>
                                    <tr>
                                        <td class="active --text-bold">NoP 3D</td>
                                        <asp:Repeater ID="rptNoOfPax3Days" runat="server">
                                            <ItemTemplate>
                                                <td class="--text-right"><%# ((SalesMonthSummaryDTO)Container.DataItem).NumberOfPax3Days.ToString("#,0")%></td>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tr>
                                    <tr class="active">
                                        <td class="--text-bold">Total pax</td>
                                        <asp:Repeater ID="rptTotalPax" runat="server">
                                            <ItemTemplate>
                                                <td class="--text-right --text-bold"><%# (((SalesMonthSummaryDTO)Container.DataItem).NumberOfPax2Days + ((SalesMonthSummaryDTO)Container.DataItem).NumberOfPax3Days).ToString("#,0")%></td>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tr>
                                </tbody>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-12 --no-padding-leftright">
                    <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <h4 class="--text-bold --inline-block">Booking report</h4>
                            <asp:TextBox runat="server" ID="txtBookingReportDateSearching" CssClass="form-control --float-right" Style="width: 30%" AutoPostBack="true" OnTextChanged="txtBookingReportDateSearching_TextChanged"
                                data-control="datetimepicker"
                                placeholder="Date(dd/MM/yyyy)" />
                            <table class="table table-bordered table-common table__total">
                                <tbody>
                                    <tr class="header active">
                                        <th style="width: 10%">Code</th>
                                        <th style="width: 8%">Trip</th>
                                        <th style="width: 6%">NoP</th>
                                        <th style="width: 15%">Revenue</th>
                                        <th style="width: 20%">Start date</th>
                                        <th style="width: 8%">View</th>
                                        <th>Agency</th>
                                    </tr>
                                    <asp:Repeater runat="server" ID="rptNewBookings">
                                        <ItemTemplate>
                                            <tr class="<%# ((Booking)Container.DataItem).Status == StatusType.Approved ? "--approved":""  %>
                                               <%# ((Booking)Container.DataItem).Status == StatusType.Cancelled ? "--cancelled":""  %>
                                               <%# ((Booking)Container.DataItem).Status == StatusType.Pending ? "--pending":""  %>
                                        ">
                                                <td><a href="BookingView.aspx?NodeId=1&SectionId=15&bi=<%# ((Booking)Container.DataItem).Id %>"><%# ((Booking)Container.DataItem).BookingIdOS %></td>
                                                <td><%# ((Booking)Container.DataItem).Trip != null ? ((Booking)Container.DataItem).Trip.TripCode : "" %></td>
                                                <td><%# ((Booking)Container.DataItem).Pax %></td>
                                                <td class="--text-right"><%# GetTotalAsString((Booking)Container.DataItem)%></td>
                                                <td><%# ((Booking)Container.DataItem).StartDate.ToString("dd/MM/yyyy") %></td>
                                                <td><a href="" data-toggle="tooltip" title="<%# ((Booking)Container.DataItem).SpecialRequest%>">View</a></td>
                                                <td><a href="AgencyView.aspx?NodeId=1&SectionId=15&agencyid=<%# ((Booking)Container.DataItem).Agency != null ? ((Booking)Container.DataItem).Agency.Id : 0%>">
                                                    <%# ((Booking)Container.DataItem).Agency != null ? ((Booking)Container.DataItem).Agency.Name : ""%></a></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <% 
                                        if (!IsPostBack)
                                        {
                                            if (((List<Booking>)rptNewBookings.DataSource).Count <= 0)
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
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <div class="col-xs-6 --no-padding-left">
            <div class="row">
                <div class="col-xs-7 --no-padding-left">
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <h4 class="--text-bold" style="display: inline-block">Cruise availability</h4>
                            <div class="input-group input-group__date-searching" style="width: 54%">
                                <asp:TextBox runat="server" ID="txtDateSearching" placeholder="Cruise availability search" CssClass="form-control" data-control="datetimepicker" OnTextChanged="txtDateSearching_TextChanged" AutoPostBack="true" />
                                <span id="basic-addon1" class="input-group-addon">
                                    <i class="fas fa-search"></i>
                                </span>
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
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="col-xs-5 --no-padding-leftright">
                    <div class="row">
                        <div class="col-xs-12 col__golden-day col__golden-day-dashboard-manager --no-padding-leftright">
                            <h4 class="golden-day__header --text-bold --no-margin-bottom">Golden days</h4>
                            <input type="hidden" data-control="datepicker-inline" id="golden-day">
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-12 --no-padding-leftright">
                            <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <h4 class="--text-bold --inline-block">Top 20 partners in - </h4>
                                    <asp:DropDownList runat="server" ID="ddlMonthTopPartner" AutoPostBack="true" CssClass="form-control --text-bold --dropdown-inline" OnSelectedIndexChanged="ddlMonthTopPartner_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <table class="table table-borderless table__top10partner">
                                        <asp:Repeater runat="server" ID="rptTop10Partner">
                                            <ItemTemplate>
                                                <tr>
                                                    <td><%# Container.ItemIndex + 1 %></td>
                                                    <td><a href="AgencyView.aspx?NodeId=1&SectionId=15&agencyid=<%# Eval("AgencyId") %>"><%# Eval("AgencyName")%></td>
                                                    <td style="padding-right: 0"><%# Eval("NumberOfPax")%></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-xs-6">
            <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="row">
                        <div class="col-xs-12 --no-padding-leftright" id="nobookings">
                            <h4 class="--text-bold" style="display: inline-block">Agencies send no bookings last 3 months</h4>
                            <asp:DropDownList runat="server" ID="ddlSales" AppendDataBoundItems="true" CssClass="form-control" Style="float: right; width: auto" AutoPostBack="true" OnSelectedIndexChanged="ddlSales_SelectedIndexChanged">
                                <asp:ListItem Text="-- Sales --" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                            <table class="table table-bordered table-common">
                                <tr class="header active">
                                    <th>No</th>
                                    <th>Name</th>
                                    <th>Last booking</th>
                                    <th>Last meeting</th>
                                    <th>Meeting details</th>
                                </tr>
                                <asp:Repeater runat="server" ID="rptAgenciesSendNoBookingLast3Months">
                                    <ItemTemplate>
                                        <tr>
                                            <td><%# Container.ItemIndex + 1 %></td>
                                            <td><a href="AgencyView.aspx?NodeId=1&SectionId=15&agencyid=<%# ((AgencySendNoBookingDTO)Container.DataItem).AgencyId%>"><%# ((AgencySendNoBookingDTO)Container.DataItem).AgencyName%></td>
                                            <td><%# ((AgencySendNoBookingDTO)Container.DataItem).LastBookingDate.HasValue ? ((AgencySendNoBookingDTO)Container.DataItem).LastBookingDate.Value.ToString("dd/MM/yyyy") : ""%></td>
                                            <td><%# ((AgencySendNoBookingDTO)Container.DataItem).LastMeetingDate.HasValue ? ((AgencySendNoBookingDTO)Container.DataItem).LastMeetingDate.Value.ToString("dd/MM/yyyy") :""%></td>
                                            <td class="--text-left">
                                                <article class="article__nobooking">
                                                    <p>
                                                        <%# ((AgencySendNoBookingDTO)Container.DataItem).MeetingDetails%>
                                                    </p>
                                                </article>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <% 
                                    if (!IsPostBack)
                                    {
                                        if (((List<AgencySendNoBookingDTO>)rptAgenciesSendNoBookingLast3Months.DataSource).Count() <= 0)
                                        {
                                %>
                                <tr>
                                    <td colspan="100%">No records found</td>
                                </tr>
                                <%
                                        }
                                    }
                                %>
                            </table>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div class="col-xs-6 --no-padding-left">
            <div class="row">
                <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="col-xs-12 --no-padding-leftright" id="viewmeetings">
                            <h4 class="--text-bold" style="display: inline-block">Most recent meetings</h4>
                            <asp:DropDownList runat="server" ID="ddlRecentMeetingSearchSales" AppendDataBoundItems="true" CssClass="form-control" Style="float: right; width: auto" AutoPostBack="true" OnSelectedIndexChanged="ddlRecentMeetingSearchSales_SelectedIndexChanged">
                                <asp:ListItem Text="-- Sales --" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                            <table class="table table-bordered table-common">
                                <tbody>
                                    <tr class="header active">
                                        <th style="width: 8%">Date</th>
                                        <th style="width: 25%">Agency</th>
                                        <th style="width: 10%">Contact</th>
                                        <th>View meetings</th>
                                        <th style="width: 3%"></th>
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
                                                <td class="--text-left">
                                                    <article class="article__recent-meeting">
                                                        <p>
                                                            <%# ((Activity)Container.DataItem).Note%>
                                                        </p>
                                                    </article>
                                                </td>
                                                <td>
                                                    <asp:LinkButton runat="server" ID="lbtDownload" Visible="<%# !String.IsNullOrEmpty(((Activity)Container.DataItem).Attachment) ? true : false %>" CommandName="Download" CommandArgument="<%#((Activity)Container.DataItem).Attachment + ',' + ((Activity)Container.DataItem).AttachmentContentType %>">
                                                        <i class="fa fa-lg fa-file-download icon icon__download" data-toggle="tooltip" title="Download"></i></asp:LinkButton></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <%     
                                        if (!IsPostBack)
                                        {
                                            if (rptRecentMeetings != null && ((List<Activity>)rptRecentMeetings.DataSource).Count() <= 0)
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
                            </table>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="row">
                        <div class="col-xs-12 --no-padding-leftright" id="notvisited">
                            <h4 class="--text-bold" style="display: inline-block">Agencies not visited / updated last 2 months</h4>
                            <asp:DropDownList runat="server" ID="ddlAgenciesNotVisitedSearchSales" AppendDataBoundItems="true" CssClass="form-control" Style="float: right; width: auto" AutoPostBack="true" OnSelectedIndexChanged="ddlAgenciesNotVisitedSearchSales_SelectedIndexChanged">
                                <asp:ListItem Text="-- Sales --" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                            <table class="table table-bordered table-common">
                                <tr class="header active">
                                    <th>No</th>
                                    <th>Name</th>
                                    <th>Last meeting</th>
                                    <th>View last meeting</th>
                                </tr>
                                <asp:Repeater runat="server" ID="rptAgenciesNotVisitedUpdated">
                                    <ItemTemplate>
                                        <tr>
                                            <td><%# Container.ItemIndex + 1%></td>
                                            <td><a href="AgencyView.aspx?NodeId=1&SectionId=15&agencyid=<%# ((AgencyNotVisitedUpdatedDTO)(Container.DataItem)).AgencyId%>"><%# ((AgencyNotVisitedUpdatedDTO)(Container.DataItem)).AgencyName%></td>
                                            <td><%# ((AgencyNotVisitedUpdatedDTO)(Container.DataItem)).LastMeetingDate.HasValue ? ((AgencyNotVisitedUpdatedDTO)(Container.DataItem)).LastMeetingDate.Value.ToString("dd/MM/yyyy"):""%></td>
                                            <td class="--text-left">
                                                <article class="article__agencies-notvisited">
                                                    <p>
                                                        <%# ((AgencyNotVisitedUpdatedDTO)(Container.DataItem)).MeetingDetails%>
                                                    </p>
                                                </article>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <% 
                                    if (!IsPostBack)
                                    {
                                        if (((List<AgencyNotVisitedUpdatedDTO>)rptAgenciesNotVisitedUpdated.DataSource).Count() <= 0)
                                        {
                                %>
                                <tr>
                                    <td colspan="100%">No records found</td>
                                </tr>
                                <%
                                        }
                                    }
                                %>
                            </table>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Scripts">
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
                }
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
        function readMore(articles) {
            let index = 0;
            let readmoreInterval = null;
            let articlesWithNoBlank = articles.filter(function (i, obj) {
                return obj.innerText != '';
            })
            readmoreInterval = setInterval(function () {
                $(articlesWithNoBlank[index]).readmore({
                    collapsedHeight: 85,
                });
                index++;
                if (index >= articles.length) {
                    clearInterval(readmoreInterval);
                }
            }, 1)
        }
        $(document).ready(function () {
            readMore($('article.article__agencies-notvisited'));
            readMore($('article.article__recent-meeting'));
            readMore($('article.article__nobooking'));
        });
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        function EndRequestHandler(sender, args) {
            readMore($('article.article__agencies-notvisited'));
            readMore($('article.article__recent-meeting'));
            readMore($('article.article__nobooking'));
        }
    </script>
</asp:Content>
