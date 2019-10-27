<%@ Page Title="" Language="C#" MasterPageFile="NewPopup.Master" AutoEventWireup="true" CodeBehind="HomeChangeAllRoom.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.HomeChangeAllRoom" %>

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
            <div class="col-xs-12">
                Booking code :    
                <asp:Label ID="lblBookingCode" ForeColor="red" runat="server" Text="Code"></asp:Label>
            </div>
        </div>
    </div>
    <div class="form-group">
        <div class="row">
            <div class="col-xs-1 --width-auto">
                Start Date
            </div>
            <div class="col-xs-2 --no-padding-left">
                <asp:TextBox ID="txtStartDate" CssClass="form-control" placeholder="Start Date (dd/mm/yyyy)" runat="server" data-control="datetimepicker" autocomplete="off"></asp:TextBox>
            </div>
            <div class="col-xs-1 --no-padding-left --width-auto">
                Trip
            </div>
            <div class="col-xs-2 --no-padding-left" style="width:35%">
                <asp:DropDownList ID="ddlTrips" runat="server" class="form-control">
                </asp:DropDownList>
            </div>
            <div class="col-xs-1 --no-padding-left --width-auto">
                Cruise
            </div>
            <div class="col-xs-2 --no-padding-left --width-auto">
                <asp:DropDownList ID="ddlCruises" runat="server" CssClass="form-control">
                </asp:DropDownList>
            </div>
            <div class="col-xs-2 --no-padding-left --width-auto">
                <asp:Button class="btn btn-primary" ID="btnCheck" Text="Check available" OnClick="btnSearch_OnClick" runat="server" />
                <asp:Button class="btn btn-primary" ID="btnAddBooking" Text="Save" OnClick="btnAddBooking_OnClick" runat="server" OnClientClick="return checkRoom();" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClientClick="closePoup();return false;" CssClass="btn btn-default" />
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-xs-12">
            <div class="col-xs-6">
                <h3 class="">From:
                    <asp:Literal ID="litCurrentCruise" runat="server"></asp:Literal></h3>
                <h3 class=""><%=_currentDate.ToString("dd/MM/yyyy") %> (<asp:Literal ID="litCurrentRooms" runat="server"></asp:Literal>)</h3>
            </div>
            <div class="col-xs-6">
                <h3 class="">To:
                    <asp:Literal ID="litNextCruise" runat="server"></asp:Literal></h3>
                <h3 class=""><%=_nextDate.ToString("dd/MM/yyyy") %> (<asp:Literal ID="litNextRooms" runat="server"></asp:Literal>)</h3>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hidChangeRoomIds" runat="server" />
    <div id="dragContainer" class="row">
        <div class="form-group">
            <div class="col-xs-6">
                <asp:Repeater runat="server" ID="rptFloors" OnItemDataBound="rptFloors_OnItemDataBound">
                    <ItemTemplate>
                        <div class="row">
                            <div class="col-xs-1 --no-padding-right">
                                <h3>F <%#Container.DataItem%> </h3>
                            </div>
                            <div class="col-xs-11">
                                <div class="py-5">
                                    <div class="list-card">
                                        <asp:Repeater ID="rptRoomsDay" runat="server" OnItemDataBound="rptRoomsDay_OnItemDataBound">
                                            <ItemTemplate>
                                                <div class="card" id="divRoom" runat="server">
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
                                                        <%--<input type="checkbox" id="chkSelectRoom" class="form-control selectRoom currentRoom" data-id='<%#Eval("Id") %>' class-type='<%#Eval("RoomName") %>' current-day="1" runat="server" />
                                                        <label for='' id="lableSelectRoom" runat="server"></label>--%>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <div class="col-xs-6">
                <asp:Repeater runat="server" ID="rptNextFloors" OnItemDataBound="rptNextFloors_OnItemDataBound">
                    <ItemTemplate>
                        <div class="row">
                            <div class="col-xs-1 --no-padding-right">
                                <h3>F <%#Container.DataItem%> </h3>
                            </div>
                            <div class="col-xs-11">
                                <div class="py-5">
                                    <div class="list-card">
                                        <asp:Repeater ID="rptRoomsNextDay" runat="server" OnItemDataBound="rptRoomsNextDay_OnItemDataBound">
                                            <ItemTemplate>
                                                <div class="card" id="divRoom" runat="server">
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
                                                        <%-- <input type="checkbox" id="chkSelectRoom" class="form-control selectRoom nextRoom" data-id='<%#Eval("Id") %>' class-type='<%#Eval("RoomName") %>' current-day="0" runat="server" />
                                                        <label for='' id="lableSelectRoom" runat="server"></label>--%>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="form-group"></div>
        <br />
        <br />
        <div class="form-group">
            <div class="row">
                <div class="col-xs-1">
                    <strong>Chú thích</strong>
                </div>
                <div class="col-xs-2 --no-padding-left --width-auto">
                    <div style="width: 20px; height: 10px; display: inline-block; border: solid 1px; background: #5cb85c" class=""></div>
                    : Booking Approved
                </div>
                <div class="col-xs-2 --no-padding-left --width-auto">
                    <div style="width: 20px; height: 10px; display: inline-block; border: solid 1px; background: #f0ad4e" class=""></div>
                    : Booking Pending
                </div>
                <div class="col-xs-2 --no-padding-left --width-auto">
                    <div style="width: 20px; height: 10px; display: inline-block; border: solid 1px; background: #FF5722" class=""></div>
                    : Booking Change
                </div>
                <div class="col-xs-2 --no-padding-left --width-auto">
                    <div class="roomOnly2D">
                        <div class="only2D"></div>
                    </div>
                    : Only 2D
                </div>
                <div class="col-xs-2 --no-padding-left --width-auto">
                    <span style="width: 50px; height: 16px; display: inline-block; color: yellow; border: 1px solid rgba(0, 0, 0, 0.125); background: #5cb85c; font-weight: bold; text-transform: uppercase;"
                        class="">Room 1</span>
                    : Booking 3D
                </div>
                <div class="col-xs-2 --no-padding-left --width-auto">
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
        var oldDate = '<%=_booking.StartDate.ToString("dd/MM/yyy")%>';
        var newDate = '<%=Request["startDate"]%>';
        var currentDate = '<%=_currentDate.ToString("dd/MM/yyy")%>';
        var nextDate = '<%=_nextDate.ToString("dd/MM/yyy")%>';
        var cruiseId = <%=_currentCruise.Id%>;
        var numberOfDay = <%=_numberOfDay%>;
        var numberOfRoom = <%=_numberOfRoom%>;
        var numberRoomDrag = 0;
        var changeType = '<%=Request["change"]%>';

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
      
        
        $( init );
        var changeRoomIds = "";
        var isFirstDrag = true;
        function init() {
            $('.dragbox').draggable( {
                containment: '#dragContainer',
                stack: '.dropbox',
                cursor: 'move',
                revert: true
            } );
            $(".dropbox").droppable( {
                accept: '.dragbox',
                hoverClass: 'hovered',
                drop: handleCardDrop
            } );
        }
        function handleCardDrop( event, ui ) {
            numberRoomDrag++;
            var fromRoom = ui.draggable.attr("rName");
            var toRoom = $(this).attr("rName");

            ui.draggable.find(".roomName").text( fromRoom + " > " + toRoom);
            ui.draggable.find(".roomName").css("color", "#fff");
            ui.draggable.find(".roomName").css("font-size", "12px");

            ui.draggable.find(".card-subtitle").css("color", "#fff");
            ui.draggable.find(".card-subtitle").css("top", "22px");
            ui.draggable.find(".booking-room").css("color", "#fff");

            var fromRoomId = ui.draggable.attr("rId");
            var toRoomId = $(this).attr("rId");

            if (isFirstDrag) {
                changeRoomIds =fromRoomId + "|" + toRoomId;
                isFirstDrag = false;
            }
            else changeRoomIds = changeRoomIds + "$" + fromRoomId + "|" + toRoomId;

            $("#<%=hidChangeRoomIds.ClientID%>").val(changeRoomIds);


            ui.draggable.addClass( 'correct' );
            ui.draggable.draggable( 'disable' );
            $(this).droppable( 'disable' );
            ui.draggable.position( { of: $(this), my: 'left top', at: 'left top' } );
            ui.draggable.draggable( 'option', 'revert', false );

        }

        function checkRoom() {
            //if (numberRoomDrag === 0) {
            //    alert("Chọn và kéo di chuyển phòng");
            //    return false;
            //}
            if (numberRoomDrag < numberOfRoom) {
                if (changeType === "trip") {
                    if(newDate!==oldDate)
                        alert("Yêu cầu chuyển toàn bộ phòng sang ngày mới");
                    else alert("Yêu cầu chuyển các phòng không đủ 2 đêm cho hành trình");
                }
                else alert("Yêu cầu chuyển toàn bộ các phòng sang tàu đích");
                return false;
            }
            return true;
        }
    </script>
</asp:Content>
