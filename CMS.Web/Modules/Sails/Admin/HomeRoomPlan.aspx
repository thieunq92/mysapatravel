<%@ Page Title="" Language="C#" MasterPageFile="NewPopup.Master" AutoEventWireup="true" CodeBehind="HomeRoomPlan.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.HomeRoomPlan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <title>Room plan</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="AdminContent" runat="server">
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
    </div>

    <div class="row">
        <div class="col-xs-3">
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClientClick="closePoup();return false;" CssClass="btn btn-default" />
            <asp:Button class="btn btn-danger" ID="btnSaveRoom" Text="SAVE" OnClick="btnSaveRoom_OnClick" runat="server" OnClientClick="return checkRoom();" />

        </div>
        <div class="col-xs-5">
        </div>
        <div class="col-xs-3">
            <div class="col-xs-6">
            </div>
            <div class="col-xs-6">
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-xs-1">
            <h3>DATE</h3>
        </div>
        <div class="col-xs-11">
            <h3 class=""><%=_currentDate.ToString("dd/MM/yyyy") %> (<asp:Literal ID="litCurrentRooms" runat="server"></asp:Literal>)</h3>
        </div>
    </div>

    <asp:Repeater runat="server" ID="rptFloors" OnItemDataBound="rptFloors_OnItemDataBound">
        <ItemTemplate>
            <div class="row">
                <div class="col-xs-1">
                    <h3>Floor <%#Container.DataItem%> </h3>
                </div>
                <div class="col-xs-11">
                    <div class="py-5">
                        <div class="list-card">
                            <asp:Repeater ID="rptRoomsDay" runat="server" OnItemDataBound="rptRoomsDay_OnItemDataBound">
                                <ItemTemplate>
                                    <div class="card">
                                        <h4 class="card-title roomName">
                                            <asp:Literal ID="litRoomName" runat="server"></asp:Literal>
                                            <asp:Literal runat="server" ID="lblSR"></asp:Literal>
                                            <asp:Literal runat="server" ID="lblEX"></asp:Literal>
                                        </h4>
                                        <asp:Literal runat="server" ID="lblL"></asp:Literal>
                                        <asp:Literal runat="server" ID="lblR"></asp:Literal>
                                        <asp:Literal runat="server" ID="litCheckInfo"></asp:Literal>
                                        <div class="connectedRoom" rname='<%#Eval("Name") %>' rid='<%#Eval("Id") %>' runat="server" id="divConnectRoom">
                                            <div class="card-block round" runat="server" id="divBooking">
                                                <h5 class="card-subtitle text-muted bookingCode">
                                                    <asp:HyperLink ID="hplBooking" CssClass="booking" runat="server"></asp:HyperLink></h5>
                                                <p class="card-text p-y-1 customerInfo">
                                                    <asp:Literal ID="litCustomer" runat="server"></asp:Literal>
                                                </p>
                                                <asp:Literal runat="server" ID="litPax"></asp:Literal>
                                                <asp:Literal runat="server" ID="lblA"></asp:Literal>
                                                <asp:Literal runat="server" ID="litAction"></asp:Literal>
                                            </div>
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
    <asp:HiddenField ID="hidChangeRoomIds" runat="server" />
    <asp:HiddenField ID="hidChangeRoomOption" runat="server" />

    <div class="row">
        <div class="form-group"></div>
        <div class="form-group">
            <div class="row">
                <div class="col-xs-1">
                    <strong>Chú thích</strong>
                </div>
                <div class="col-xs-2">
                    <div class="roomOnly2D">
                        <div class="only2D"></div>
                    </div>
                    : Only 2D
                </div>
                <%--<div class="col-xs-2">
                    <span style="width: 50px; height: 16px; display: inline-block; color: yellow; border: 1px solid rgba(0, 0, 0, 0.125); background: #5cb85c; font-weight: bold; text-transform: uppercase;"
                        class="">Room 1</span>
                    : Booking 3D
                </div>
                <div class="col-xs-2">
                    <span style="width: 50px; height: 16px; display: inline-block; border: 1px solid rgba(0, 0, 0, 0.125); background: #5cb85c; font-weight: bold; text-transform: uppercase; color: #0b3bf5"
                        class="">Room 1</span>
                    : Booking 2D
                </div>--%>
            </div>
        </div>
    </div>
    <link rel="stylesheet" type="text/css" href="/Modules/Sails/Themes/Style.css" />
    <style>
        .card {
            position: relative;
            height: 160px;
            min-width: 145px;
            width: max-content;
            overflow: inherit;
            background: #cdcdcd;
        }

        .connectedRoom {
            min-width: 140px;
            min-height: 75px;
        }

        .card-block {
            display: inline-block;
            width: 140px;
            cursor: move;
            overflow: hidden;
            margin-left: 2px;
            height: 100px;
        }

        .card-link-roomType {
            left: 100px;
        }

        .card-block .bookingCode {
            left: 5px;
        }

        .checkCusInfo {
            width: 10px;
            height: 10px;
            right: 0px;
            transform: rotate(0deg);
            -ms-transform: skewY(0deg);
            -webkit-transform: skewY(0deg);
        }

        .only2D {
            transform: rotate(0deg);
            -ms-transform: skewY(0deg);
            -webkit-transform: skewY(0deg);
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
    <script>
        var roomDic = {};
        var roomDicOption = {};

        $(function () {
            $(".connectedRoom").sortable({
                connectWith: ".connectedRoom",
                cancel: ".card-title,.roomClass,.card-link-roomType,.checkCusInfo",
                receive: function (ev, ui) {
                    var fromBkRoomId = $(ui.item[0]).attr("bkrId");
                    var toRoomId = $(ev.target).attr("rId");

                    let rtype1 = $(ev.target).attr("rtype");
                    let bkRtype1 = $(ui.item[0]).attr("bkRtype");
                    if (rtype1 !== bkRtype1) {
                        if (rtype1 === "twin" && bkRtype1 === "double") {
                            let msg = "Cảnh báo : bạn vừa chuyển phòng Double sang Twin, chọn lựa yêu cầu tương ứng";
                            msg += "\n1: Chuyển double thành twin. \n2: Ghép giường thành double";
                            let selectOption = prompt(msg, "1");
                            if (selectOption === null || selectOption === "") {
                                ui.sender.sortable("cancel");
                            } else {
                                saveQueue(fromBkRoomId, toRoomId, selectOption);
                            }
                        } else {
                            let confirmChange =
                                confirm(`Bạn có chắc chắn muốn đổi từ phòng ${bkRtype1} sang ${rtype1}`);
                            if (confirmChange) {
                                saveQueue(fromBkRoomId, toRoomId, "1");
                            } else {
                                ui.sender.sortable("cancel");
                            }
                        }
                    } else {
                        saveQueue(fromBkRoomId, toRoomId, "1");
                    }
                    //$(".connectedRoom").each(function (index) {
                    //    var bkRooms = $(this).find(".booking-room");
                    //    let afn = $(this).attr("afn");
                    //    let rName = $(this).attr("rName");
                    //    if (afn === "2D" && bkRooms.length > 0) {
                    //        for (let i = 0; i < bkRooms.length; i++) {
                    //            let el = $(bkRooms[i]);
                    //            let bkd = el.attr("bkd");
                    //            if (bkd === "3D") {
                    //                alert(rName + " chỉ còn cho booking 2 day");
                    //                return false;
                    //            }
                    //        }

                    //    }
                    //});
                }
            }).disableSelection();

        });

        function saveQueue(fromBkRoomId, toRoomId, selectOption) {
            roomDic[fromBkRoomId] = toRoomId;
            let isFirstDrag = true;
            let changeRoomIds = "";
            for (let key in roomDic) {
                if (roomDic.hasOwnProperty(key)) {
                    let value = roomDic[key];
                    if (isFirstDrag) {
                        changeRoomIds = key + "|" + value;
                        isFirstDrag = false;
                    } else changeRoomIds = changeRoomIds + "$" + key + "|" + value;
                }
            }
            $("#<%=hidChangeRoomIds.ClientID%>").val(changeRoomIds);
            roomDicOption[fromBkRoomId] = selectOption;
            isFirstDrag = true;
            let changeRoomOption = "";
            for (let key in roomDicOption) {
                if (roomDicOption.hasOwnProperty(key)) {
                    let value = roomDicOption[key];
                    if (isFirstDrag) {
                        changeRoomOption = key + "|" + value;
                        isFirstDrag = false;
                    } else changeRoomOption = changeRoomOption + "$" + key + "|" + value;
                }
            }
            $("#<%=hidChangeRoomOption.ClientID%>").val(changeRoomOption);
        }
        function checkRoom() {
            let check = true;
            $(".connectedRoom").each(function (index) {
                let rName = $(this).attr("rName");
                var bkRooms = $(this).find(".booking-room");
                if (bkRooms.length > 1) {
                    alert(rName + " đang có nhiều hơn 1 booking cùng phòng");
                    check = false;
                }
                let afn = $(this).attr("afn");
                if (afn === "2D" && bkRooms.length > 0) {
                    for (let i = 0; i < bkRooms.length; i++) {
                        let el = $(bkRooms[i]);
                        let bkd = el.attr("bkd");
                        let bkld = el.attr("bkld");
                        if (bkd === "3D" && bkld === '0') {
                            alert(rName + " chỉ còn cho booking 2 day");
                            check = false;
                        }
                    }
                }
            });
            return check;
        }
    </script>
</asp:Content>
