<%@ Page Title="" Language="C#" MasterPageFile="NewPopup.Master" AutoEventWireup="true" CodeBehind="HomeSeriesAddBooking.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.HomeSeriesAddBooking" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="AdminContent" runat="server">
    <div class="form-group">
        <div class="row">
            <div class="col-xs-8">
            </div>
            <div class="col-xs-3">
                <div class="col-xs-6">
                    <asp:TextBox ID="txtStartDate" data-control='datetimepicker' autocomplete="off" runat="server" CssClass="form-control" placeholder="Start date (dd/mm/yyyy)"></asp:TextBox>
                </div>
                <div class="col-xs-6">
                    <asp:Button ID="btnSearch" CssClass="btn btnHomeSearch" runat="server" Text="Search" OnClick="btnSearch_OnClick" />
                </div>
            </div>

        </div>
    </div>
    <div class="form-group">
        <div class="cruise-list">
            <div class="row">
                <div class="col-xs-12">
                    <div class="btn-group" role="group" aria-label="...">
                        <asp:Repeater runat="server" ID="rptCruises" OnItemDataBound="rptCruises_OnItemDataBound">
                            <ItemTemplate>
                                <asp:HyperLink ID="hplCruise" CssClass="btn btn-default" runat="server"></asp:HyperLink>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-1">
            </div>
            <div class="col-xs-5">

                <input type="button" class="btn btnCharter2day" id="btnCharter2day" runat="server" onclick="charter2day(); return false;" value="Charter 2 days" />
                <input type="button" class="btn btnCharter3day" id="btnCharter3day" runat="server" onclick="charter3day(); return false;" value="Charter 3 days" />
                <asp:Button ID="Button1" runat="server" Text="Select all available" OnClientClick="selectAllAvailable();return false;" CssClass="btn btn-warning" />

            </div>
            <div class="col-xs-2">
            </div>
            <div class="col-xs-3">
                <div class="col-xs-6">
                </div>
                <div class="col-xs-6">
                    <input type="button" class="btn btnAddBooking" id="btnAddBooking" onclick="addBooking(); return false;" value="Add booking" />
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-xs-1">
            <h3>DATE</h3>
        </div>
        <div class="col-xs-11">
            <div class="col-xs-6">
                <h3 class=""><%=_currentDate.ToString("dd/MM/yyyy") %> (<asp:Literal ID="litCurrentRooms" runat="server"></asp:Literal>)</h3>
            </div>
            <div class="col-xs-6">
                <h3 class=""><%=_nextDate.ToString("dd/MM/yyyy") %> (<asp:Literal ID="litNextRooms" runat="server"></asp:Literal>)</h3>
            </div>
        </div>
    </div>

    <asp:Repeater runat="server" ID="rptFloors" OnItemDataBound="rptFloors_OnItemDataBound">
        <ItemTemplate>
            <div class="row">
                <div class="col-xs-1">
                    <h3>Floor <%#Container.DataItem%> </h3>
                </div>
                <div class="col-xs-11">
                    <div class="col-xs-6">
                        <div class="py-5">
                            <div class="list-card">
                                <asp:Repeater ID="rptRoomsDay" runat="server" OnItemDataBound="rptRoomsDay_OnItemDataBound">
                                    <ItemTemplate>
                                        <div class="card">
                                            <div class="card-block round">
                                                <asp:HiddenField ID="hidCurrentDay" Value="1" runat="server" />
                                                <h4 class="card-title roomName">
                                                    <asp:Literal ID="litRoomName" runat="server"></asp:Literal></h4>
                                                <h5 class="card-subtitle text-muted bookingCode">
                                                    <asp:HyperLink ID="hplBooking" CssClass="booking" runat="server"></asp:HyperLink></h5>
                                                <p class="card-text p-y-1 customerInfo">
                                                    <asp:Literal ID="litCustomer" runat="server"></asp:Literal>
                                                </p>
                                                <asp:Literal runat="server" ID="litCheckInfo"></asp:Literal>
                                                <asp:Literal runat="server" ID="lblL"></asp:Literal>
                                                <asp:Literal runat="server" ID="lblR"></asp:Literal>
                                                <asp:Literal runat="server" ID="litAction"></asp:Literal>
                                                <input type="checkbox" id="chkSelectRoom" class="form-control selectRoom currentRoom" data-id='<%#Eval("Id") %>' class-type='<%#Eval("RoomName") %>' current-day="1" runat="server" />
                                                <label for='' id="lableSelectRoom" runat="server"></label>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>

                            </div>
                        </div>
                    </div>
                    <div class="col-xs-6">
                        <div class="py-5">
                            <div class="list-card">
                                <asp:Repeater ID="rptRoomsNextDay" runat="server" OnItemDataBound="rptRoomsDay_OnItemDataBound">
                                    <ItemTemplate>
                                        <div class="card">
                                            <asp:HiddenField ID="hidCurrentDay" Value="0" runat="server" />
                                            <div class="card-block round">
                                                <h4 class="card-title roomName">
                                                    <asp:Literal ID="litRoomName" runat="server"></asp:Literal></h4>
                                                <h5 class="card-subtitle text-muted bookingCode">
                                                    <asp:HyperLink ID="hplBooking" CssClass="booking" runat="server"></asp:HyperLink>
                                                </h5>
                                                <p class="card-text p-y-1 customerInfo">
                                                    <asp:Literal ID="litCustomer" runat="server"></asp:Literal>
                                                </p>
                                                <asp:Literal runat="server" ID="litCheckInfo"></asp:Literal>
                                                <asp:Literal runat="server" ID="lblL"></asp:Literal>
                                                <asp:Literal runat="server" ID="lblR"></asp:Literal>
                                                <asp:Literal runat="server" ID="litAction"></asp:Literal>
                                                <input type="checkbox" id="chkSelectRoom" class="form-control selectRoom nextRoom" data-id='<%#Eval("Id") %>' class-type='<%#Eval("RoomName") %>' current-day="0" runat="server" />
                                                <label for='' id="lableSelectRoom" runat="server"></label>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
    <div class="row">
        <div class="form-group"></div>
        <div class="form-group">
            <div class="row">
                <div class="col-xs-1">
                    <strong>Chú thích</strong>
                </div>
                <div class="col-xs-2">
                    <div style="width: 20px; height: 10px; display: inline-block; border: solid 1px; background: #5cb85c" class=""></div>
                    : Booking Approved
                </div>
                <div class="col-xs-2">
                    <div style="width: 20px; height: 10px; display: inline-block; border: solid 1px; background: #f0ad4e" class=""></div>
                    : Booking Pending
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
    <link rel="stylesheet" type="text/css" href="/Modules/Sails/Themes/Style.css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
    <script>
        var currentDate = '<%=_currentDate.ToString("dd/MM/yyy")%>';
        var nextDate = '<%=_nextDate.ToString("dd/MM/yyy")%>';
        var cruiseId = <%=_currentCruise.Id%>;
        var existRooms = <%=_existRooms%>;
        $( document ).ready(function() {
            $( ".Approved" ).each(function( index ) {
                $(this).parent().parent().parent().css("background", "#5cb85c");
            });
            $( ".Pending" ).each(function( index ) {
                $(this).parent().parent().parent().css("background", "#d58512");
            });
        });
        var currentRoomDic = {};
        <%
        if (_currentRoomDic != null)
        {
            foreach (var current in _currentRoomDic)
            { %>
        currentRoomDic["<%= current.Key %>"] = <%= current.Value %>;
                <% }
        }
        %>

        var nextRoomDic = {};
        <% 
        if (_nextRoomDic != null)
        {
            foreach (var next in _nextRoomDic)
            { %>
        nextRoomDic["<%= next.Key %>"] = <%= next.Value %>;
            <% }
        }
         %>

        var selectCurrentRooms = {}, selectNextRooms = {}, selectRoomIds = [], selectNextRoomIds =[];
        $( document ).ready(function() {
            $('.selectRoom').change(function() {
                let classType = $(this).attr("class-type");
                let id = $(this).attr("data-id");
                if ($(this).is(':checked')) {
                    if ($(this).attr("current-day") === "1") {
                        if (isEqual(selectCurrentRooms, currentRoomDic)) {
                            $(this).prop('checked', false);
                            alert("Quá số phòng trống trong ngày!");
                            return;
                        }
                        //if (!isEmpty(selectNextRooms)) {
                        //    $(this).prop('checked', false);
                        //    alert("Chỉ được chọn phòng cùng ngày!");
                        //    return;
                        //} else {
                        selectRoomIds.push(id);
                        if (selectCurrentRooms[classType]) {
                            selectCurrentRooms[classType] =
                                selectCurrentRooms[classType] + 1;
                        } else {
                            //selectCurrentRooms.push({
                            //    key:  $(this).attr("class-type"),
                            //    value: 1
                            //});
                            selectCurrentRooms[classType] = 1;
                        }
                        //}
                    } else {
                        if (isEqual(selectNextRooms, nextRoomDic)) {
                            $(this).prop('checked', false);
                            alert("Quá số phòng trống trong ngày!");
                            return;
                        }
                        if (isEmpty(selectCurrentRooms)) {
                            $(this).prop('checked', false);
                            alert("Booking 2 đêm thì phòng phải giống nhau! Chọn phòng đêm hôm đầu trước !");
                            return;
                        }
                        //if (!isEmpty(selectCurrentRooms)) {
                        //    $(this).prop('checked', false);
                        //    alert("Chỉ được chọn phòng cùng ngày!");
                        //    return;
                        //} else {
                        selectNextRoomIds.push(id);
                        if (selectNextRooms[classType] > 0) {
                            selectNextRooms[classType] =
                                selectNextRooms[classType] + 1;
                        } else {
                            //selectNextRooms.push({
                            //    key:  $(this).attr("class-type"),
                            //    value: 1
                            //});
                            selectNextRooms[classType] = 1;
                        }
                        //}
                    }
                } else {
                    if ($(this).attr("current-day") === "1") {
                        removeEl(selectRoomIds,id);
                        if (selectCurrentRooms[classType] &&
                            selectCurrentRooms[classType] > 1) {
                            selectCurrentRooms[classType] = selectCurrentRooms[classType] - 1;
                        } else {
                            delete selectCurrentRooms[classType];
                        }
                    } else {
                        removeEl(selectNextRoomIds,id);
                        if (selectNextRooms[classType] &&
                            selectNextRooms[classType] > 1) {
                            selectNextRooms[classType] =
                                selectNextRooms[classType] - 1;
                        } else {
                            delete selectNextRooms[classType];
                        }
                    }
                    
                }
            });
        });
        function isEmpty(obj) {
            for(var key in obj) {
                if(obj.hasOwnProperty(key))
                    return false;
            }
            return true;
        }
        function isEqual(a, b) {
            // Create arrays of property names
            var aProps = Object.getOwnPropertyNames(a);
            var bProps = Object.getOwnPropertyNames(b);

            // If number of properties is different,
            // objects are not equivalent
            if (aProps.length !== bProps.length) {
                return false;
            }

            for (var i = 0; i < aProps.length; i++) {
                var propName = aProps[i];

                // If values of same property are not equal,
                // objects are not equivalent
                if (a[propName] !== b[propName]) {
                    return false;
                }
            }

            // If we made it this far, objects
            // are considered equivalent
            return true;
        }

        function addBooking() {
            var date = '<%=_currentDate.ToString("dd/MM/yyy")%>';
            if (isEmpty(selectCurrentRooms)) {
                alert("Chọn phòng");
            }
            else if (!isEmpty(selectRoomIds) && !isEmpty(selectNextRoomIds)) {
                if (!isEqual(selectRoomIds.sort(), selectNextRoomIds.sort())) {
                    alert("Booking 2 đêm thì phòng 2 ngày phải giống nhau !");
                } else {
                    var src = "/Modules/Sails/Admin/HomeSeriesSubmitBooking.aspx?NodeId=1&SectionId=15&d=3&roomIds=" + selectRoomIds.toString() + "&cruiseId=" + cruiseId +"&date=" + date + "&si=" + '<%=Request["si"]%>';
                    $("#addBookingModal iframe").attr('src', src);
                    $("#addBookingModal").modal();
                    return false;
                }
            }
            else if (!isEmpty(selectRoomIds)) {
                var src = "/Modules/Sails/Admin/HomeSeriesSubmitBooking.aspx?NodeId=1&SectionId=15&roomIds=" + selectRoomIds.toString() + "&cruiseId=" + cruiseId +"&date=" + date + "&si=" + '<%=Request["si"]%>';
                $("#addBookingModal iframe").attr('src', src);
                $("#addBookingModal").modal();
                return false;
            }
            else alert("Chọn phòng");
}

function addRoom(roomId,isCurrentDay) {
    var date = currentDate;
    if (isCurrentDay === '0') date = nextDate; 
    var src = "/Modules/Sails/Admin/HomeAddBooking.aspx?NodeId=1&SectionId=15&roomIds=" + roomId + "&cruiseId=" + cruiseId +"&date=" + date;
    $("#addBookingModal iframe").attr('src', src);
    $("#addBookingModal").modal();
}

function editRoom(bookingId,roomId,tripId) {
    //var src = "/Modules/Sails/Admin/HomeChangeRoom.aspx?NodeId=1&SectionId=15&roomId=" + roomId + "&bookingId=" + bookingId + "&tripId="+tripId;
    //$("#addBookingModal iframe").attr('src', src);
    //$("#addBookingModal").modal();
}
function closePoup(refesh) {
    $("#addBookingModal").modal('hide');
    if (refesh===1) {
        //                window.location.href = window.location.href;
        window.parent.closePoup(1);
    }
}

function charter2day() {
    if (existRooms > 0) {
        var r = confirm("Charter is not available, there are bookings on the day you selected. Are you sure you want to book the rest of the cruise?");
        if (r === false) {
            return;
        } 
    }
    selectRoomIds = [];
    selectCurrentRooms = {};
    selectNextRooms = {};
    selectNextRoomIds =[];
    $( ".currentRoom" ).each(function() {
        let classType = $(this).attr("class-type");
        let id = $(this).attr("data-id");
        $(this).prop('checked', true);
        selectRoomIds.push(id);
        if (selectCurrentRooms[classType]) {
            selectCurrentRooms[classType] =
                selectCurrentRooms[classType] + 1;
        } else {
            selectCurrentRooms[classType] = 1;
        }
    });
    $( ".nextRoom" ).each(function() {
        $(this).prop('checked', false);
    });
}

function charter3day() {
    selectRoomIds = [];
    selectCurrentRooms = {};
    selectNextRooms = {};
    selectNextRoomIds =[];
    $( ".currentRoom" ).each(function() {
        let classType = $(this).attr("class-type");
        let id = $(this).attr("data-id");
        $(this).prop('checked', true);
        selectRoomIds.push(id);
        if (selectCurrentRooms[classType]) {
            selectCurrentRooms[classType] =
                selectCurrentRooms[classType] + 1;
        } else {
            selectCurrentRooms[classType] = 1;
        }
    });
    $( ".nextRoom" ).each(function() {
        let classType = $(this).attr("class-type");
        let id = $(this).attr("data-id");
        $(this).prop('checked', true);
        selectNextRoomIds.push(id);
        if (selectNextRooms[classType] > 0) {
            selectNextRooms[classType] =
                selectNextRooms[classType] + 1;
        } else {
            selectNextRooms[classType] = 1;
        }
    });
}
function selectAllAvailable() {
    $(".currentRoom").each(function() {
        let classType = $(this).attr("class-type");
        let id = $(this).attr("data-id");
        $(this).prop('checked', true);
        if(selectRoomIds.indexOf(id)<0)selectRoomIds.push(id);
        if (selectCurrentRooms[classType]) {
            selectCurrentRooms[classType] =
                selectCurrentRooms[classType] + 1;
        } else {
            selectCurrentRooms[classType] = 1;
        }
    });
}
function removeEl(arr,value) {
    var index = arr.indexOf(value);
    if (index > -1) {
        arr.splice(index, 1);
    }
}
    </script>
</asp:Content>
