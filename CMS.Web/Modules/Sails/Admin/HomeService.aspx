<%@ Page Title="" Language="C#" MasterPageFile="MO.Master" AutoEventWireup="true" CodeBehind="HomeService.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.HomeService" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <title>Dịch vụ trên tàu</title>
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
            <div class="col-xs-4">
                <asp:Button runat="server" CssClass="btn btn-default" Text="All" ID="btnAll" OnClick="btnAll_OnClick" />

                <asp:Button runat="server" CssClass="btn btn-success" Text="Cleaned" ID="btnCleaned" OnClick="btnCleaned_OnClick" />

                <asp:Button runat="server" CssClass="btn btn-primary" Text="In Used" ID="btnInUsed" OnClick="btnInUsed_OnClick" />

                <asp:Button runat="server" CssClass="btn btn-warning" Text="Not cleaned" ID="btnNotCleaned" OnClick="btnNotCleaned_OnClick" />
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
                            <asp:Repeater ID="rptRoomsDay" runat="server" OnItemDataBound="rptRoomsDay_OnItemDataBound" OnItemCommand="rptRoomsDay_OnItemCommand">
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
                                            <div class="card-link-action">
                                                <asp:ImageButton runat="server" ID="btnStatus" CommandName="updateStatus" CommandArgument='<%#Eval("Id") %>' />
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
    <%--<div class="row">
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
    </div>--%>
    <div class="modal fade" id="osModal" tabindex="-1" role="dialog" aria-labelledby="gridSystemModalLabel" data-backdrop="static" data-keyboard="true">
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
<asp:Content ID="Scripts" ContentPlaceHolderID="Scripts" runat="server">
    <script>

        $(document).ready(function () {
            $(".Cleaned").each(function (index) {
                $(this).parent().parent().parent().css("background", "#5cb85c");
                $(this).parent().parent().parent().css("border-color", "#4cae4c");
                $(this).parent().parent().parent().css("color", "#fff");
            });
            $(".InUsed").each(function (index) {
                $(this).parent().parent().parent().css("background", "#337ab7");
                $(this).parent().parent().parent().css("border-color", "#2e6da4");
                $(this).parent().parent().parent().css("color", "#fff");
            });
            $(".NotCleaned").each(function (index) {
                $(this).parent().parent().parent().css("background", "#f0ad4e");
                $(this).parent().parent().parent().css("border-color", "#eea236");
                $(this).parent().parent().parent().css("color", "#fff");
            });
        });
        function viewService(bookingRoomId, ivExportId) {
            var src = "/Modules/Sails/Admin/IvExportAdd.aspx?NodeId=1&SectionId=15&bookingRoomId=" + bookingRoomId + "&ExportId=" + ivExportId;
            $("#osModal iframe").attr('src', src);
            $("#osModal").modal();
            return false;
        }
        function editRoom(bookingId, roomId, tripId) {
            var src = "/Modules/Sails/Admin/HomeChangeRoom.aspx?NodeId=1&SectionId=15&roomId=" + roomId + "&bookingId=" + bookingId + "&tripId=" + tripId;
            $("#osModal iframe").attr('src', src);
            $("#osModal").modal();
        }

        function closePoup(refesh) {
            $("#osModal").modal('hide');
            if (refesh === 1) {
                window.location.href = window.location.href;
            }
        }
    </script>
</asp:Content>

