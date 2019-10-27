<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="MO-NoScriptManager.Master" CodeBehind="ViewActivities.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.ViewActivities" Title="View Activities" %>

<asp:Content ID="AdminContent" ContentPlaceHolderID="AdminContent" runat="server">
    <div class="user-panel" ng-controller="userController">
        <h3 class="page-header">{{user.FirstName + " " +  user.LastName}}</h3>
    </div>
    <div class="activities-panel" ng-controller="activitiesController">
        <div class="form-group">
            <div class="row">
                <div class="col-xs-1">From</div>
                <div class="col-xs-3">
                    <input type="text" placeholder="From (dd/mm/yyyy)" class="form-control" ng-model="from" id="txtFrom" ng-change="reload()">
                </div>
                <div class="col-xs-1">To</div>
                <div class="col-xs-3">
                    <input type="text" placeholder="To (dd/mm/yyyy)" class="form-control" ng-model="to" id="txtTo" ng-change="reload()">
                </div>
            </div>
        </div>
        <div class="form-group mutation-observer">
            <div class="row">
                <div class="col-xs-12">
                    <ul class="nav nav-tabs">
                        <li class="active"><a data-toggle="tab" href="#meetings">Meetings</a></li>
                        <li><a data-toggle="tab" href="#bookings">Bookings</a></li>
                        <li><a data-toggle="tab" href="#series">Series</a></li>
                        <li><a data-toggle="tab" href="#partners">Partners</a></li>
                    </ul>

                    <div class="tab-content">
                        <div id="meetings" class="tab-pane fade in active">
                            <table class="table table-bordered table-hover">
                                <tbody>
                                    <tr class="active">
                                        <th style="width: 7%">Update time</th>
                                        <th style="width: 7%">Date meeting</th>
                                        <th style="width: 12%">Sales</th>
                                        <th>Meet with</th>
                                        <th>Position</th>
                                        <th>Belong to agency</th>
                                        <th>Note</th>
                                    </tr>
                                    <tr ng-repeat="item in listMeeting">
                                        <td>{{item.UpdateTime}}</td>
                                        <td>{{item.DateMeeting}}</td>
                                        <td>{{item.SalesName}}</td>
                                        <td>{{item.MeetWith}}</td>
                                        <td>{{item.Position}}</td>
                                        <td>{{item.BelongToAgency}}</td>
                                        <td>
                                            <article>
                                                <p>{{item.Note}}</p>
                                            </article>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <div id="bookings" class="tab-pane fade">
                            <table class=" table table-bordered table-hover">
                                <tbody>
                                    <tr class="active">
                                        <th>Booking code</th>
                                        <th>Trip</th>
                                        <th>Cruise</th>
                                        <th>Number of pax</th>
                                        <th>Customer name</th>
                                        <th>Partner</th>
                                        <th>TA code</th>
                                        <th>Status</th>
                                        <th>Last edit</th>
                                        <th>Start date</th>
                                    </tr>
                                    <tr ng-repeat="item in listBooking">
                                        <td>OS{{item.Id}}</td>
                                        <td>{{item.TripCode}}</td>
                                        <td>{{item.CruiseName}}</td>
                                        <td>{{item.NoOfPax}}</td>
                                        <td class="transform">{{item.CustomerName}}</td>
                                        <td>{{item.AgencyName}}</td>
                                        <td>{{item.TACode}}</td>
                                        <td>{{item.Status}}</td>
                                        <td>{{item.ModifiedBy}}</td>
                                        <td>{{item.StartDate}}</td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <div id="series" class="tab-pane fade">
                            <table class=" table table-bordered table-hover">
                                <tbody>
                                    <tr class="active">
                                        <th>Series code</th>
                                        <th>Partner</th>
                                        <th>Booker</th>
                                        <th>Sales in charge</th>
                                        <th>Cutoff date</th>
                                        <th>No of days</th>
                                        <th>No of booking</th>
                                        <th>Status</th>
                                    </tr>
                                    <tr ng-repeat="item in listSeries">
                                        <td>{{item.SeriesCode}}</td>
                                        <td>{{item.AgencyName}}</td>
                                        <td>{{item.BookerName}}</td>
                                        <td>{{item.SalesInChargeName}}</td>
                                        <td>{{item.CutoffDate}} day(s)</td>
                                        <td>{{item.NoOfDays}} day(s)</td>
                                        <td>{{item.NoOfBooking}}</td>
                                        <td class="transform">{{item.Status}}</td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <div id="partners" class="tab-pane fade">
                            <table class="table table-bordered table-hover">
                                <tbody>
                                    <tr class="active">
                                        <th>Agency name</th>
                                        <th>Phone</th>
                                        <th>Fax</th>
                                        <th>Email</th>
                                        <th>Role</th>
                                    </tr>
                                    <tr ng-repeat="item in listAgency">
                                        <td>{{item.Name}}</td>
                                        <td>{{item.Phone}}</td>
                                        <td>{{item.Fax}}</td>
                                        <td>{{item.Email}}</td>
                                        <td>{{item.RoleName}}</td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

<asp:Content ID="Scripts" ContentPlaceHolderID="Scripts" runat="server">
    <script type="text/javascript" src="/modules/sails/admin/viewactivitiescontroller.js"></script>
    <script>
        $("#txtFrom").datetimepicker({
            timepicker: false,
            format: 'd/m/Y',
            scrollInput: false,
            scrollMonth: false,
        })

        $("#txtTo").datetimepicker({
            timepicker: false,
            format: 'd/m/Y',
            scrollInput: false,
            scrollMonth: false,
        })
    </script>
    <script>
        var target = document.querySelector(".mutation-observer"),
        observer = new MutationObserver(mutationCallback),
        config = { childList: true, subtree: true }
        function mutationCallback(mutations) {
            mutations.forEach(function (mutation) {
                $(".transform").each(function (e, v) {
                    var parser = new DOMParser();
                    var dom = parser.parseFromString($(v).html(), "text/html")
                    $(v).empty().append(dom.body.textContent).removeClass("transform");
                });
            });
        }
        observer.observe(target, config);
    </script>
</asp:Content>
