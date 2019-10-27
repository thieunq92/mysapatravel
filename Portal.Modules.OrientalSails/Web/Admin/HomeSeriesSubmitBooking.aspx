<%@ Page Title="" Language="C#" MasterPageFile="NewPopup.Master" AutoEventWireup="true" CodeBehind="HomeSeriesSubmitBooking.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.HomeSeriesSubmitBooking" %>

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
                <asp:Label ID="lblMsg" ForeColor="red" runat="server"></asp:Label>
            </div>
        </div>
    </div>
    <div class="form-group">
        <div class="row">
            <div class="col-xs-1">
                Start date
            </div>
            <div class="col-xs-2">
                <asp:TextBox ID="txtDate" runat="server" data-control="datetimepicker" class="form-control" placeholder="Start date (dd/mm/yyyy)" autocomplete="off">
                </asp:TextBox>
            </div>
            <div class="row">
                <div class="col-xs-2">
                    Trip
                </div>
                <div class="col-xs-4">
                    <asp:DropDownList ID="ddlTrips" runat="server" class="form-control">
                    </asp:DropDownList>
                </div>
            </div>
        </div>
    </div>
    <div class="form-group">
        <div class="row">
            <div class="col-xs-1">
                TA code
            </div>
            <div class="col-xs-2">
                <asp:TextBox ID="txtAgencyCode" runat="server" class="form-control" placeholder="TA Code"></asp:TextBox>
            </div>
            <div class="col-xs-2">
                Special Request
            </div>
            <div class="col-xs-4">
                <asp:TextBox ID="txtSpecialRequest" runat="server" TextMode="MultiLine" class="form-control" placeholder="Special Request"></asp:TextBox>

            </div>
            <div class="col-xs-2">
                <asp:Repeater ID="rptExtraServices" runat="server" OnItemDataBound="rptExtraServices_ItemDataBound">
                    <ItemTemplate>
                        <div class="checkbox">
                            <label>
                                <input id="chkService" runat="server" type="checkbox" /><%#Eval("Name") %></label>
                        </div>
                        <asp:HiddenField ID="hiddenId" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "Id") %>' />
                        <asp:HiddenField ID="hiddenValue" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "Price") %>' />
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-1">
                Status
            </div>
            <div class="col-xs-2">
                <asp:DropDownList ID="ddlStatusType" runat="server" CssClass="form-control">
                </asp:DropDownList>
            </div>
            <div class="col-xs-2 nopadding-left">
                <div id="statusinfor-placeholder">
                    <asp:TextBox ID="txtDeadline" runat="server" placeholder="Deadline Pending" CssClass="form-control"></asp:TextBox>
                    <asp:TextBox ID="txtCutOffDays" runat="server" placeholder="CutOff Days" CssClass="form-control"></asp:TextBox>
                    <asp:TextBox runat="server" ID="txtCancelledReason" TextMode="MultiLine" placeholder="Lý do hủy booking" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-xs-12">
            <em>Thông tin phòng</em>
            <div class="form-group">
                <div class="row">
                    <div class="col-xs-2">
                        Chọn tàu <strong>
                            <asp:Literal ID="litCurrentCruise" runat="server" /></strong>
                    </div>
                    <div class="col-xs-2">
                        <asp:CheckBox runat="server" ID="chkCharter" Text=" Charter Booking"></asp:CheckBox>
                    </div>
                </div>
            </div>
            <table class="table table-bordered table-hover table-common">
                <asp:Repeater ID="rptRoom" runat="server" OnItemDataBound="rptRoom_ItemDataBound">
                    <HeaderTemplate>
                        <tr class="active">
                            <th>No
                            </th>
                            <th>Name
                            </th>
                            <th>Room Type
                            </th>
                            <th>Room Class
                            </th>
                            <th>Cruise
                            </th>
                            <th>Floor
                            </th>
                            <th>Adult
                            </th>
                            <th>Child
                            </th>
                            <th>Baby
                            </th>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr class="item">
                            <td><%#Container.ItemIndex + 1%>
                            </td>
                            <td>
                                <asp:HiddenField Value='<%# Eval("Id")%>' ID="hid" runat="server" />
                                <asp:HyperLink ID="hyperLink_Name" runat="server"></asp:HyperLink>
                            </td>
                            <td>
                                <asp:Label ID="label_RoomType" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="label_RoomClass" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="labelCruise" runat="server"></asp:Label>
                            </td>
                            <td><%#Eval("Floor") %></td>
                            <td>
                                <asp:DropDownList ID="ddlAdults" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="2">2 adult(s)</asp:ListItem>
                                    <asp:ListItem Value="1">1 adult(s)</asp:ListItem>
                                </asp:DropDownList></td>
                            <td>
                                <asp:DropDownList ID="ddlChild" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="0">0 child(s)</asp:ListItem>
                                    <asp:ListItem Value="1">1 child(s)</asp:ListItem>
                                </asp:DropDownList></td>
                            <td>
                                <asp:DropDownList ID="ddlBaby" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="0">0 baby(s)</asp:ListItem>
                                    <asp:ListItem Value="1">1 baby(s)</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
    </div>
    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" CssClass="btn btn-primary" /><asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClientClick="closePoup();return false;" CssClass="btn btn-default" />
</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="Scripts" runat="server">
    <script>
        $("#ctl00_AdminContent_agencySelectornameid").click(function () {
            var width = 800;
            var height = 600;
            window.open('/Modules/Sails/Admin/AgencySelectorPage.aspx?NodeId=1&SectionId=15&clientid=ctl00_AdminContent_agencySelector', 'Agencyselect', 'width=' + width + ',height=' + height + ',top=' + ((screen.height / 2) - (height / 2)) + ',left=' + ((screen.width / 2) - (width / 2)));
        });
        $(document).ready(function () {
            $("#aspnetForm").validate({
                rules: {
                    <%=txtDate.UniqueID%>: {
                        required: true,
                    },
                    txtAgency:{
                        required: true,
                    },
                },
                messages: {
                    <%=txtDate.UniqueID%>: {
                        required: "Yêu cầu chọn ngày khởi hành",
                    },
                    txtAgency:{
                        required: "Yêu cầu chọn Agency",
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
        })
        $(function () {
            $("#<%= txtDeadline.ClientID%>").datetimepicker({
                format: 'd/m/Y H:i',
                scrollImput:false,
                scrollMonth:false
            });
        })
        function statusInforShow(){
            var txtCutOffDays = $("#<%= txtCutOffDays.ClientID%>");
            var txtDeadline = $("#<%= txtDeadline.ClientID%>");
            var txtCancelledReason = $("#<%= txtCancelledReason.ClientID%>");
            var selectedStatus =  $("#<%= ddlStatusType.ClientID%> option:selected");

            if (selectedStatus.html() === "Pending") {
                txtDeadline.show();
                txtCutOffDays.hide();
                txtCancelledReason.hide();
            }
            if (selectedStatus.html() === "CutOff") {
                txtDeadline.hide();
                txtCutOffDays.show();
                txtCancelledReason.hide();
            }
            if (selectedStatus.html() === "Cancelled") {
                txtDeadline.hide();
                txtCutOffDays.hide();
                txtCancelledReason.show();
            }
            if (selectedStatus.html() === "Approved") {
                txtDeadline.hide();
                txtCutOffDays.hide();
                txtCancelledReason.hide();
            }
        }
   
        statusInforShow();
        $("#<%= ddlStatusType.ClientID%>").change(statusInforShow);
    </script>
</asp:Content>
