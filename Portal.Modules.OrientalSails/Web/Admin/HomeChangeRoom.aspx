<%@ Page Title="" Language="C#" MasterPageFile="NewPopup.Master" AutoEventWireup="true" CodeBehind="HomeChangeRoom.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.HomeChangeRoom" %>

<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
    <script>
        function RefreshParentPage() {
            window.parent.closePoup(1);
        }
        function closePoup() {
            window.parent.closePoup(0);
        }
    </script>
    <div class="form-group">
        <div class="row">
            <div class="col-xs-3">
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClientClick="closePoup();return false;" CssClass="btn btn-default" />
            </div>
            <div class="col-xs-5">
            </div>
            <div class="col-xs-3">
                <div class="col-xs-6">
                </div>
                <div class="col-xs-6">
                    <asp:Button class="btn btnAddBooking" ID="btnAddBooking" OnClientClick="return addBooking();" Text="Change Room" OnClick="btnAddBooking_OnClick" runat="server" />
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
    <asp:HiddenField ID="hidRoomId" runat="server" />
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
                <div class="col-xs-2">
                    <div style="width: 20px; height: 10px; display: inline-block; border: solid 1px; background: #FF5722" class=""></div>
                    : Booking Change
                </div>
                <div class="col-xs-2">
                    <span style="width: 50px; height: 16px; display: inline-block; color: yellow; border: 1px solid rgba(0, 0, 0, 0.125); background: #5cb85c; font-weight: bold; text-transform: uppercase;"
                          class="">Room 1</span>
                    : Booking 3D
                </div>
                <div class="col-xs-2">
                    <span style="width: 50px; height: 16px; display: inline-block; border: 1px solid rgba(0, 0, 0, 0.125); background: #5cb85c; font-weight: bold; text-transform: uppercase; color: #0b3bf5"
                          class="">Room 1</span>
                    : Booking 2D
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
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
    <script>
        var currentDate = '<%=_currentDate.ToString("dd/MM/yyy")%>';
        var nextDate = '<%=_nextDate.ToString("dd/MM/yyy")%>';
        var cruiseId = <%=_currentCruise.Id%>;
        var numberOfDay = <%=_numberOfDay%>;

        $( document ).ready(function() {
            $( ".Approved" ).each(function( index ) {
                $(this).parent().parent().parent().css("background", "#5cb85c");
            });
            $( ".Pending" ).each(function( index ) {
                $(this).parent().parent().parent().css("background", "#d58512");
            });
            $( ".booking-room" ).each(function( index ) {
                $(this).parent().parent().parent().css("background", "#FF5722");
            });
        });
        var currentRoomDic = {};
        <%
            if (_currentRoomDic!=null)
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
                        $("#<%=hidRoomId.ClientID%>").val(id);
                        selectRoomIds = [];
                        selectRoomIds.push(id);
                        clearCurrentRoom(id);
                        selectCurrentRooms = {};
                        if (selectCurrentRooms[classType]) {
                            selectCurrentRooms[classType] =
                                selectCurrentRooms[classType] + 1;
                        } else {
                          
                            selectCurrentRooms[classType] = 1;
                        }
                    } else {
                        if (numberOfDay === 2) {
                            $(this).prop('checked', false);
                            alert("Chỉ được chọn khi là booking 3 day!");
                            return;
                        }
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
                        selectNextRoomIds = [];
                        selectNextRoomIds.push(id);
                        clearNextRoom(id);
                        selectNextRooms = {};
                        if (selectNextRooms[classType] > 0) {
                            selectNextRooms[classType] =
                                selectNextRooms[classType] + 1;
                        } else {
                            
                            selectNextRooms[classType] = 1;
                        }
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

        function clearCurrentRoom(id) {
            $( ".currentRoom" ).each(function() {
                let dataId = $(this).attr("data-id");
                if(dataId!==id)
                    $(this).prop('checked', false);
                
            });
        }

        function clearNextRoom(id) {
            $( ".nextRoom" ).each(function() {
                let dataId = $(this).attr("data-id");
                if(dataId!==id)
                    $(this).prop('checked', false);
                
            });
        }
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
                return false;
            }
            else if (numberOfDay === 3 && isEmpty(selectNextRoomIds)) {
                alert("Booking 2 đêm thì phải chọn phòng 2 ngày giống nhau!");
                return false;
            }
            else if (!isEmpty(selectRoomIds) && !isEmpty(selectNextRoomIds)) {
                if (!isEqual(selectRoomIds.sort(), selectNextRoomIds.sort())) {
                    alert("Booking 2 đêm thì phòng 2 ngày phải giống nhau !");
                    return false;
                } else {
                    return true;
                }
            }
            else if (!isEmpty(selectRoomIds)) {
                return true;
            } else {
                alert("Chọn phòng");
                return false;
            }
        }

        function removeEl(arr,value) {
            var index = arr.indexOf(value);
            if (index > -1) {
                arr.splice(index, 1);
            }
        }
    </script>
</asp:Content>
