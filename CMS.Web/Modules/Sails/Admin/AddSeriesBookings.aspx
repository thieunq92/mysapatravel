<%@ Page Title="Series Booking Adding" Language="C#" MasterPageFile="MO-NoScriptManager.Master"
    AutoEventWireup="true"
    CodeBehind="AddSeriesBookings.aspx.cs"
    Inherits="Portal.Modules.OrientalSails.Web.Admin.AddSeriesBookings" %>
<asp:Content ID="AdminContent" ContentPlaceHolderID="AdminContent" runat="server">
    <div ng-controller="checkPermissionController">
        <div ng-show="displayErrorPanel">
            <div class="row">
                <div class="col-xs-12">
                    <div class="alert alert-danger" role="alert">
                        <strong>Failed!</strong> Không có quyền truy cập trang này. Liên hệ manager để được cấp quyền
                    </div>
                </div>
            </div>
        </div>
        <div ng-show="displayMainPanel">
            <div ng-controller="seriesController">
                <h3 class="page-header">{{title}}<br />
                    <small><em>{{createby}}</em></small></h3>
                <div class="row">
                    <div class="col-xs-12">
                        <div class="alert alert-success series" role="alert" style="display: none">
                        </div>
                        <div class="alert alert-info series" role="alert" style="display: none">
                        </div>
                        <div class="alert alert-warning series" role="alert" style="display: none">
                        </div>
                        <div class="alert alert-danger series" role="alert" style="display: none">
                        </div>
                    </div>
                </div>

                <div class="seriesForm" ng-show="show.seriesForm" mo-ngshow-storage="show.seriesForm">
                    <div class="form-group">
                        <div class="row">
                            <div class="col-xs-1">
                                Agency
                            </div>
                            <div class="col-xs-3">
                                <input type="text" name="txtAgency" ng-model="series.Agency.Name" id="ctl00_AdminContent_agencySelectornameid" class="form-control"
                                    readonly placeholder="Click to select agency"
                                    ng-blur="bookerGetAllByAgency(null)" />
                                <input id="ctl00_AdminContent_agencySelector" type="hidden" />
                            </div>
                            <div class="col-xs-1">
                                Booker
                            </div>
                            <div class="col-xs-3">
                                <select class="form-control inline-block width90percent" ng-model="series.Booker.Id" ng-options="item.Id as item.Name for item in listBooker">
                                    <option value="">--Select booker--</option>
                                </select>
                                <i class="fa fa-circle-o-notch fa-spin" aria-hidden="true" ng-show="show.bookerLoadingIcon"></i>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <div class="col-xs-4">
                                <div class="row">
                                    <div class="col-xs-3">
                                        Series code
                                    </div>
                                    <div class="col-xs-9">
                                        <input id="txtSeriesCode" name="txtSeriesCode" ng-model="series.SeriesCode" placeholder="Series code" class="form-control" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-xs-12">
                                        <button class="btn btn-primary" type="button" ng-click="createOrUpdateSeries()">{{lblBtnSeries}}</button>
                                        <i class="fa fa-circle-o-notch fa-spin" aria-hidden="true" ng-show="show.createSeriesLoadingIcon"></i>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xs-4">
                                <div class="col-xs-3 nopadding-left">
                                    Cut-off date
                                </div>
                                <div class="col-xs-3" style="padding-left: 7px;">
                                    <div class="input-group">
                                        <input id="txtCutoffDate" name="txtCutoffDate" ng-model="series.CutoffDate" class="form-control" style="padding-left: 9px" onchange="" />
                                        <span class="input-group-addon" style="padding: 0">day(s)</span>
                                    </div>
                                </div>
                                <div class="col-xs-3 nopadding-right nopadding-left" style="width: 18%">
                                    Trip
                                </div>
                                <div class="col-xs-3 nopadding-left nopadding-right">
                                    <select ng-model="series.NoOfDays" id="ddlNoOfDays" name="ddlNoOfDays" class="form-control" style="padding-left: 0px; width: 100%" convert-to-number>
                                        <option value="">-- Trip --</option>
                                        <option value="2">2 Days 1 Night</option>
                                        <option value="3">3 Days 2 Nights</option>
                                    </select>
                                </div>
                            </div>
                            <div class="col-xs-1 nopadding-right">
                                Special request
                            </div>
                            <div class="col-xs-3">
                                <textarea ng-model="series.SpecialRequest" placeholder="Special request" class="form-control" style="width: 90%;"></textarea>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="seriesinfosuccess-panel" ng-show="show.seriesPanel" mo-ngshow-storage="show.seriesPanel">
                    <div class="form-group">
                        <div class="row">
                            <div class="col-xs-1">
                                Series code
                            </div>
                            <div class="col-xs-2 text-wrap">
                                {{series.SeriesCode}}
                            </div>
                            <div class="col-xs-1">
                                Agency
                            </div>
                            <div class="col-xs-4 text-wrap">
                                {{series.Agency.Name}}
                            </div>
                            <div ng-show="series.Booker != null">
                                <div class="col-xs-1">
                                    Booker
                                </div>
                                <div class="col-xs-2">
                                    {{series.Booker.Name}}
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="row">
                            <div class="col-xs-1">
                                Cut-off date
                            </div>
                            <div class="col-xs-2">
                                {{series.CutoffDate}} day(s)
                            </div>
                            <div class="col-xs-1">
                                Trip
                            </div>
                            <div class="col-xs-2">
                                {{series.NoOfDaysTrip}}
                            </div>
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="row">
                            <div class="col-xs-1" ng-hide="listBooked.length > 0">
                                <button type="button" ng-click="editSeries()" class="btn btn-primary">Edit Series</button>
                                <i class="fa fa-circle-o-notch fa-spin" aria-hidden="true" ng-show="show.updateSeriesLoadingIcon"></i>
                            </div>
                            <div class="col-xs-3">
                                <button type="button" onclick="addBooking()" class="btn btn-primary">Add booking</button>
                                <button type="button" onclick="addBookingRandom()" class="btn btn-primary">Add booking random</button>

                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="mutation-observer">
                <div class="booked-panel" ng-controller="bookedController" ng-show="listBooked.length > 0">
                    <hr />
                    <div class="form-group">
                        <div class="row">
                            <div class="col-xs-12">
                                <div class="col-xs-1">
                                    Booking code
                                </div>
                                <div class="col-xs-3">
                                    <input type="text" placeholder="Booking code" ng-model="bookingCode" class="form-control"></input>
                                </div>
                                <div class="col-xs-1">
                                    TA code
                                </div>
                                <div class="col-xs-3">
                                    <input type="text" placeholder="TA code" ng-model="taCode" class="form-control"></input>
                                </div>
                                <div class="col-xs-1">
                                    Start date
                                </div>
                                <div class="col-xs-3">
                                    <input type="text" placeholder="Start date" ng-model="startDate" class="form-control" id="txtSearchStartDate"></input>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-12">
                            <div class="overlay" ng-show="show.loadingBookingTableIcon"><i class="fa fa-circle-o-notch fa-spin loading" aria-hidden="true"></i></div>
                            <table class="table table-bordered table-hover overlay-table table-common">
                                <tr class="active">
                                    <th>STT</th>
                                    <th>Booking code</th>
                                    <th>TA code</th>
                                    <th>Start date</th>
                                    <th>Trip</th>
                                    <th style="width: 7%">Cruise</th>
                                    <th style="width: 6%">Pax</th>
                                    <th style="width: 10%">Cabins</th>
                                    <th>Special request</th>
                                    <th>Status</th>
                                    <th>Last Edited</th>
                                    <th style="width: 7%"></th>
                                </tr>
                                <tr ng-repeat='item in listBooked|filter:{TACode:taCode, BookingId:bookingCode, StartDate:startDate}' ng-repeat-storage="ng-repeat='item in listBooked'"
                                    ng-class="{danger:item.Status == 'Cancelled', success:item.Status == 'Approved'}">
                                    <td>{{$index + 1}}</td>
                                    <td><a href="BookingView.aspx?NodeId=1&SectionId=15&bi={{item.Id}}">{{item.BookingId}}</a></td>
                                    <td>{{item.TACode}}</td>
                                    <td>{{item.StartDate}}</td>
                                    <td>{{item.TripCode}}</td>
                                    <td>{{item.CruiseName}}</td>
                                    <td class="pax">{{item.Pax}}</td>
                                    <td class="room">{{item.Room}}</td>
                                    <td>{{item.SpecialRequest}}</td>
                                    <td>{{item.Status}}</td>
                                    <td>{{item.ModifiedBy + " " + item.ModifiedDate}}</td>
                                    <td>
                                        <a href="BookingView.aspx?NodeId=1&SectionId=15&bi={{item.Id}}">
                                            <i class="fa fa-edit fa-2x text-primary" aria-hidden="true" title="Edit" data-toggle="tooltip" data-placement="top" onmouseenter="$(this).tooltip('show')"></i>
                                        </a>

                                        <a href="javascript:void(0)" ng-click="BookingApproved(item.Id)">
                                            <i class="fa fa-check fa-2x text-success" aria-hidden="true" title="Approved" data-toggle="tooltip" data-placement="top" onmouseenter="$(this).tooltip('show')"></i>
                                        </a>

                                        <a href="javascript:void(0)" ng-click="BookingCancel(item.Id)">
                                            <i class="fa fa-window-close fa-2x text-danger" aria-hidden="true" title="Cancel" data-toggle="tooltip" data-placement="top" onmouseenter="$(this).tooltip('show')"></i>
                                        </a>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>

            <div class="addbooking-panel" style="display: none" ng-controller="bookingController" ng-show="rsShow.bookingForm" mo-ngshow-storage="show.bookingForm">
                <hr />
                <div class="row">
                    <div class="col-xs-12">
                        <div class="alert alert-success booking" role="alert" style="display: none">
                        </div>
                        <div class="alert alert-info booking" role="alert" style="display: none">
                        </div>
                        <div class="alert alert-warning booking" role="alert" style="display: none">
                        </div>
                        <div class="alert alert-danger booking" role="alert" style="display: none">
                        </div>
                    </div>
                </div>
                <div class="booking-block">
                    <div class="form-group">
                        <div class="row">
                            <div class="col-xs-1">
                                <label>Booking {{numberOfBooking}}</label>
                            </div>
                            <div class="col-xs-1">
                                TA Code
                            </div>
                            <div class="col-xs-2">
                                <input id="txtTACode" ng-model="rsBooking.TACode" placeholder="TACode" class="form-control" />
                            </div>
                            <div class="col-xs-1">
                                Start date
                            </div>
                            <div class="col-xs-2">
                                <input id="txtStartDate" ng-model="rsBooking.StartDate" placeholder="Start date (dd/MM/yyyy)" class="form-control" />
                            </div>
                            <div class="col-xs-1">
                                Trip
                            </div>
                            <div class="col-xs-3">
                                <select class="form-control inline-block" ng-model="rsBooking.Trip.Id" ng-options="item.Id as item.Name for item in listTrip">
                                    <option value="">--Select Trip--</option>
                                </select>
                            </div>
                            <div class="col-xs-1">
                                <button class="btn btn-primary" type="button" ng-click="checkRoom()">Check</button>
                                <i class="fa fa-circle-o-notch fa-spin" aria-hidden="true" ng-show="show.checkLoadingIcon"></i>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <div class="col-xs-1">
                            </div>
                            <div class="col-xs-1 nopadding-right">
                                Special Request
                            </div>
                            <div class="col-xs-9">
                                <textarea id="txtSpecialRequest" ng-model="rsBooking.SpecialRequest" placeholder="Special Request" class="form-control"></textarea>
                            </div>

                        </div>
                    </div>
                    <div class="row" ng-show="show.checkRoomPanel" mo-ngshow-storage="show.checkRoomPanel">
                        <div class="col-xs-12">
                            <table class="table table-bordered table-hover">
                                <tr class="active">
                                    <th>Cruise</th>
                                    <th>Số phòng trống</th>
                                    <th>Trong đó</th>
                                </tr>
                                <tr ng-repeat="item in listCheckRoomResult" class="{{item.style}}">
                                    <td><a href="javascript:void(0)" ng-click="getAvaiableRoom(item.Cruise.Id, item.Cruise.Name)">{{item.Cruise.Name}}</a></td>
                                    <td>{{item.NoOfRoomAvaiable}}</td>
                                    <td>{{item.DetailRooms}}</td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <div class="selectroom-panel" ng-show="show.selectRoomPanel">
                        <div class="row">
                            <div class="col-xs-12" style="margin-bottom: 10px">
                                <span>Chọn tàu <strong>{{rsBooking.Cruise.Name}} </strong></span>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row" ng-repeat="item in listAvaiableRoom">
                                <div class="col-xs-2">
                                    {{item.KindOfRoom}}
                                </div>
                                <div class="col-xs-1 custom1-col-xs-1">
                                    <select class="form-control" ng-model="item.selectedRoom" ng-options="adult as (adult  + ' ' + 'room(s)') for adult in range(1, item.NoOfAdult, 1)">
                                        <option value="">0 room(s)</option>
                                    </select>
                                </div>
                                <div class="col-xs-1 custom1-col-xs-1">
                                    <select class="form-control" ng-model="item.selectedChild" ng-options="child as (child + ' ' + 'child(s)') for child in range(1, item.NoOfChild, 1)">
                                        <option value="">0 child(s)</option>
                                    </select>
                                </div>
                                <div class="col-xs-1 custom1-col-xs-1">
                                    <select class="form-control" ng-model="item.selectedBaby" ng-options="baby as (baby + ' ' + 'baby(s)') for baby in range(1, item.NoOfBaby, 1)">
                                        <option value="">0 baby(s)</option>
                                    </select>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-12 float-right">
                                    <button class="btn btn-primary" type="button" ng-click="saveBooking()">Save Booking</button>
                                    <i class="fa fa-circle-o-notch fa-spin" aria-hidden="true" ng-show="show.saveBookingLoadingIcon"></i>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
<div class="modal fade" id="addBookingModal" tabindex="-1" role="dialog" aria-labelledby="gridSystemModalLabel" data-backdrop="static" data-keyboard="true">
    <div class="modal-dialog" role="document" style="width: 85vw; height: 80vh">
        <div class="modal-content">
            <div class="modal-header">
                <%--                    <span>Add booking</span>--%>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
            </div>
            <div class="modal-body">
                <iframe frameborder="0" width="100%" style="height: 80vh" scrolling="yes"></iframe>
            </div>
        </div>
    </div>
</div>
</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="Scripts" runat="server">
    <script type="text/javascript" src="/modules/sails/admin/addseriesbookingscontroller.js"></script>
    <script>
        $("#ctl00_AdminContent_agencySelectornameid").click(function () {
            var width = 800;
            var height = 600;
            window.open('/Modules/Sails/Admin/AgencySelectorPage.aspx?NodeId=1&SectionId=15&clientid=ctl00_AdminContent_agencySelector', 'Agencyselect', 'width=' + width + ',height=' + height + ',top=' + ((screen.height / 2) - (height / 2)) + ',left=' + ((screen.width / 2) - (width / 2)));
        })
    </script>

    <script>
        $(function () {
            $("#txtStartDate").datetimepicker({
                timepicker: false,
                format: 'd/m/Y',
                scrollInput: false,
                scrollMonth: false,
            })
            $("#txtSearchStartDate").datetimepicker({
                timepicker: false,
                format: 'd/m/Y',
                scrollInput: false,
                scrollMonth: false,
            })
        });
    </script>
    <script>
        var target = document.querySelector(".mutation-observer"),
        observer = new MutationObserver(mutationCallback),
        config = { childList: true, subtree: true }
        function mutationCallback(mutations) {
            mutations.forEach(function (mutation) {
                $(".pax").each(function (e, v) {
                    var parser = new DOMParser();
                    var dom = parser.parseFromString($(v).html(), "text/html")
                    $(v).empty().append(dom.body.textContent).removeClass("pax");
                });
                $(".room").each(function (e, v) {
                    var parser = new DOMParser();
                    var dom = parser.parseFromString($(v).html(), "text/html")
                    $(v).empty().append(dom.body.textContent).removeClass("room");
                });

                var tablePos = $(".overlay-table").position();
                $(".overlay").css({
                    position: "absolute",
                    top: tablePos.top,
                    left: tablePos.left,
                    width: $(".booking-table").width(),
                    height: $(".booking-table").height(),
                })

            });
        }
        observer.observe(target, config);
    </script>

    <script>
        $(document).ready(function () {
            var seriesId = getParameterValues("si");
            if (seriesId = null)
                seriesId = "";

            $("#aspnetForm").validate({
                rules: {
                    txtAgency: "required",
                    txtCutoffDate: {
                        required: true,
                        digits: true,
                    },
                    ddlNoOfDays: {
                        required: true,
                    },
                    txtSeriesCode: {
                        remote: function () {
                            var remoteData = {
                                txtSeriesCode: $("#txtSeriesCode").val(),
                                si: seriesId,
                            };
                            var remoteJSON = JSON.stringify(remoteData);
                            var r = {
                                url: "WebMethod/AddSeriesBookingsWebMethod.asmx/CheckDuplicateSeriesCode",
                                type: "POST",
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                dataFilter: function (data) {
                                    var x = (JSON.parse(data)).d;
                                    return JSON.stringify(x);
                                },
                                data: remoteJSON
                            }
                            return r;
                        },
                    },
                },
                messages: {
                    txtAgency: "Yêu cầu chọn một Agency",
                    txtCutoffDate: {
                        required: "Yêu cầu điền Cutoff date",
                        digits: "Yêu cầu chỉ điền số",
                    },
                    ddlNoOfDays: {
                        required: "Yêu cầu chọn Trip",
                    },
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
            });
        });
    </script>

    <script>
        $(function () {
            var tablePos = $(".overlay-table").position();
            $(".overlay").css({
                position: "absolute",
                top: tablePos.top,
                left: tablePos.left,
                width: $(".booking-table").width(),
                height: $(".booking-table").height(),
            })
        })

        function addBooking() {
            var src = "/Modules/Sails/Admin/HomeSeriesAddBooking.aspx?NodeId=1&SectionId=15&si=" + '<%=Request["si"]%>';
            $("#addBookingModal iframe").attr('src', src);
            $("#addBookingModal").modal();
        }
        function addBookingRandom() {
            var src = "/Modules/Sails/Admin/HomeSeriesAddBookingRandom.aspx?NodeId=1&SectionId=15&si=" + '<%=Request["si"]%>';
            $("#addBookingModal iframe").attr('src', src);
            $("#addBookingModal").modal();
        }
        function closePoup(refesh) {
            $("#addBookingModal").modal('hide');
            if (refesh === 1) {
                window.location.href = window.location.href;
            }
        }
    </script>
</asp:Content>
