<%@ Page Language="C#" MasterPageFile="MO.Master" AutoEventWireup="true"
    CodeBehind="BookingView.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.BookingView"
    Title="Booking View" %>

<%@ Register Assembly="CMS.ServerControls" Namespace="CMS.ServerControls" TagPrefix="svc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register TagPrefix="uc" TagName="customer" Src="../Controls/CustomerInfoRowInput.ascx" %>
<%@ Register Assembly="Portal.Modules.OrientalSails" Namespace="Portal.Modules.OrientalSails.Web.Controls"
    TagPrefix="orc" %>
<asp:Content ID="AdminContent" ContentPlaceHolderID="AdminContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <% if (UserIdentity.UserName != "captain.os1")
               { %>
            <asp:Button ID="buttonSubmit" runat="server" CssClass="btn btn-primary" OnClientClick="screenCapture.capture();return false" disabled="disabled" />
            <asp:Button ID="button1" runat="server" CssClass="btn btn-primary hidden" OnClick="buttonSubmit_Click" />
            <a href="SendEmail.aspx?NodeId=1&SectionId=15&BookingId=<%= Booking.Id %>" class="btn btn-primary" id="sendemail">SendEmail</a>
            <a href="BookingHistories.aspx?NodeId=1&SectionId=15&BookingId=<%= Booking.Id %>" class="btn btn-primary">View History</a>
            <% } %>
        </div>
    </div>
    <label id="screencapture_status" class="hidden"></label>
    <div class="bookinginfor-panel">
        <div class="form-group">
            <div class="row">
                <div class="col-xs-1 ">
                    Booking code
                </div>
                <div class="col-xs-1">
                    <asp:Label ID="lblBookingId" runat="server"></asp:Label>
                </div>
                <div class="col-xs-1">
                    <div class="checkbox" style="display: none">
                        <label>
                            <input type="checkbox" id="chkInspection" runat="server">Inspection
                        </label>
                    </div>
                </div>
                <div class="col-xs-1">
                </div>
                <div class="col-xs-1 --no-padding-left">
                    Trip
                </div>
                <div class="col-xs-2">
                    <asp:PlaceHolder runat="server" ID="plhTripReadonly" Visible="false">
                        <asp:Literal runat="server" ID="litTrip"></asp:Literal>
                        (contact accountant to change) </asp:PlaceHolder>
                    <asp:DropDownList ID="ddlTrips" runat="server" CssClass="form-control">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlOptions" runat="server" CssClass="form-control">
                        <asp:ListItem>Option 1</asp:ListItem>
                        <asp:ListItem>Option 2</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-xs-1 --no-padding-left">
                </div>
                <div class="col-xs-1 nopadding-right --no-padding-left">
                    Number Of Pax
                </div>
                <div class="col-xs-1 nopadding-left nopadding-right" style="width: 3%">
                    <asp:Literal ID="litPax" runat="server"></asp:Literal>
                    <i class="fa fa-info-circle fa-lg" aria-hidden="true" data-toggle="tooltip" data-placement="right" title="<%= PaxGetDetails() %>"></i>
                </div>
            </div>
        </div>
        <div class="form-group">
            <div class="row">
                <div class="col-xs-1">
                    <label for="startdate">Start Date</label>
                </div>
                <div class="col-xs-3">
                    <asp:TextBox ID="txtStartDate" CssClass="form-control" placeholder="Start Date (dd/mm/yyyy)" runat="server"></asp:TextBox>
                </div>
                <div class="col-xs-1 --no-padding-left">
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
        <div class="form-group">
            <div class="row">
                <div class="col-xs-1">
                    <label for="agency">Agency</label>
                </div>
                <div class="col-xs-3">
                    <asp:DropDownList ID="ddlAgencies" CssClass="form-control" runat="server">
                    </asp:DropDownList>
                </div>
                <div class="col-xs-2 nopadding-left" style="padding-right: 8px">
                    <asp:TextBox ID="txtAgencyCode" CssClass="form-control" placeholder="TA Code" runat="server" data-toggle="tooltip" title="TA code" data-placement="top"></asp:TextBox>
                </div>
                <div class="col-xs-2 nopadding-left" style="padding-right: 8px; display: none">
                    <asp:TextBox runat="server" ID="txtSeriesCode" CssClass="form-control" placeholder="Series code" data-toggle="tooltip" title="Series code" data-placement="top"></asp:TextBox>
                </div>
                <div class="col-xs-2 --no-padding-left">
                    <svc:CascadingDropDown ID="cddlBooker" runat="server" CssClass="form-control">
                    </svc:CascadingDropDown>
                </div>
            </div>
        </div>
        <div class="form-group">
            <div class="row">
                <div class="col-xs-1">
                    <label for="total">Total</label>
                </div>
                <div class="col-xs-2" style="padding-right: 8px">
                    <asp:TextBox ID="txtTotal" runat="server" placeholder="Total" CssClass="form-control" data-inputmask="'alias': 'numeric', 'groupSeparator': ',', 'autoGroup': true, 'digits': 2, 'digitsOptional': true, 'placeholder': '0'"></asp:TextBox>
                </div>
                <div class="col-xs-1 nopadding-left">
                    <asp:DropDownList runat="server" ID="ddlCurrencies" CssClass="form-control">
                        <asp:ListItem Value="1">USD</asp:ListItem>
                        <asp:ListItem Value="0">VND</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-xs-2 nopadding-left --width-auto">

                    <asp:Button ID="lbtCalculate" CssClass="btn btn-primary" runat="server" OnClick="lbtCalculate_Click" Text="Calculate"
                        Style="width: auto; display: none"></asp:Button>
                    <asp:Button runat="server" ID="btnLockIncome" CssClass="btn btn-primary" Visible="false" Text="Lock this booking"
                        OnClick="btnLockIncome_Click" />
                    <asp:Button runat="server" ID="btnUnlockIncome" Visible="false" CssClass="btn btn-primary"
                        Text="Unlock" OnClick="btnUnlockIncome_Click" />
                    <i class="fa fa-info-circle fa-lg" aria-hidden="true" data-toggle="tooltip" data-placement="right" title="<%= UserGetUserLockIncomeDetails() %>"></i>

                </div>
                <div class="col-xs-5 nopadding-left">
                    <div class="checkbox" style="display: none">
                        <label class="checkbox-inline">
                            <input runat="server" id="chkSpecial" type="checkbox" />Upgrade/Special price</label>
                        <label class="checkbox-inline">
                            <input type="checkbox" runat="server" id="chkInvoice">Invoice</label>
                        <label class="checkbox-inline">
                            <input id="chkIsPaymentNeeded" runat="server" type="checkbox" />
                            Pay Before Tour
                        </label>
                        <label class="checkbox-inline">
                            <input runat="server" id="chkEarlyBird" type="checkbox" />Early Bird
                        </label>
                    </div>
                </div>
            </div>
        </div>
        <div class="form-group">
            <div class="row">
                <div class="col-xs-4">
                    <div class="form-group">
                        <div class="row">
                            <div class="col-xs-3 --no-padding-left" style="width: calc(8.33333333 * (300% + 90px) / 100);">
                                <label>Chi phí guide</label>
                            </div>
                            <div class="col-xs-6 --no-padding-right" style="width: calc((16.666667 * (300% + 90px) / 100 ) - 15px); padding-right: 8px; padding-left: 0px;">
                                <asp:TextBox ID="txtGuideExpense" runat="server" placeholder="Chi phí guide" CssClass="form-control" data-inputmask="'alias': 'numeric', 'groupSeparator': ',', 'autoGroup': true, 'digits': 2, 'digitsOptional': true, 'placeholder': '0'"></asp:TextBox>
                            </div>
                            <div class="col-xs-3 nopadding-left --no-padding-right" style="width: calc((8.33333333 * (300% + 90px) / 100) - 15px);">
                                <asp:DropDownList runat="server" ID="ddlGuideExpenseCurrencyType" CssClass="form-control">
                                    <asp:ListItem Value="VND">VND</asp:ListItem>
                                    <asp:ListItem Value="USD">USD</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <div class="col-xs-3 --no-padding-left" style="width: calc(8.33333333 * (300% + 90px) / 100);">
                                <label>Chi phí ăn</label>
                            </div>
                            <div class="col-xs-6 --no-padding-right" style="width: calc((16.666667 * (300% + 90px) / 100 ) - 15px); padding-right: 8px; padding-left: 0px;">
                                <asp:TextBox ID="txtMealExpense" runat="server" placeholder="Chi phí ăn" CssClass="form-control" data-inputmask="'alias': 'numeric', 'groupSeparator': ',', 'autoGroup': true, 'digits': 2, 'digitsOptional': true, 'placeholder': '0'"></asp:TextBox>
                            </div>
                            <div class="col-xs-3 nopadding-left --no-padding-right" style="width: calc((8.33333333 * (300% + 90px) / 100) - 15px);">
                                <asp:DropDownList runat="server" ID="ddlMealExpenseCurrencyType" CssClass="form-control">
                                    <asp:ListItem Value="VND">VND</asp:ListItem>
                                    <asp:ListItem Value="USD">USD</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <div class="col-xs-3 --no-padding-left" style="width: calc(8.33333333 * (300% + 90px) / 100);">
                                <label>Chi phí hotel</label>
                            </div>
                            <div class="col-xs-6 --no-padding-right" style="width: calc((16.666667 * (300% + 90px) / 100 ) - 15px); padding-right: 8px; padding-left: 0px;">
                                <asp:TextBox ID="txtHotelExpense" runat="server" placeholder="Chi phí hotel" CssClass="form-control" data-inputmask="'alias': 'numeric', 'groupSeparator': ',', 'autoGroup': true, 'digits': 2, 'digitsOptional': true, 'placeholder': '0'"></asp:TextBox>
                            </div>
                            <div class="col-xs-3 nopadding-left --no-padding-right" style="width: calc((8.33333333 * (300% + 90px) / 100) - 15px);">
                                <asp:DropDownList runat="server" ID="ddlHotelExpenseCurrencyType" CssClass="form-control">
                                    <asp:ListItem Value="VND">VND</asp:ListItem>
                                    <asp:ListItem Value="USD">USD</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <div class="col-xs-3 --no-padding-left" style="width: calc(12.33333333 * (300% + 90px) / 100);">
                                <label>Chi phí vé thắng cảnh</label>
                            </div>
                            <div class="col-xs-6 --no-padding-right" style="width: calc((12.666667 * (300% + 90px) / 100 ) - 15px); padding-right: 8px; padding-left: 0px;">
                                <asp:TextBox ID="txtTicketExpense" runat="server" placeholder="Chi phí vé thắng cảnh" CssClass="form-control" data-inputmask="'alias': 'numeric', 'groupSeparator': ',', 'autoGroup': true, 'digits': 2, 'digitsOptional': true, 'placeholder': '0'"></asp:TextBox>
                            </div>
                            <div class="col-xs-3 nopadding-left --no-padding-right" style="width: calc((8.33333333 * (300% + 90px) / 100) - 15px);">
                                <asp:DropDownList runat="server" ID="ddlTicketExpenseCurrencyType" CssClass="form-control">
                                    <asp:ListItem Value="VND">VND</asp:ListItem>
                                    <asp:ListItem Value="USD">USD</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="row">
                            <div class="col-xs-3 --no-padding-left" style="width: calc(12.33333333 * (300% + 90px) / 100);">
                                <label>Chi phí xe tại Sapa</label>
                            </div>
                            <div class="col-xs-6 --no-padding-right" style="width: calc((12.666667 * (300% + 90px) / 100 ) - 15px); padding-right: 8px; padding-left: 0px;">
                                <asp:TextBox ID="txtBusExpense" runat="server" placeholder="Chi phí xe tại Sapa " CssClass="form-control" data-inputmask="'alias': 'numeric', 'groupSeparator': ',', 'autoGroup': true, 'digits': 2, 'digitsOptional': true, 'placeholder': '0'"></asp:TextBox>
                            </div>
                            <div class="col-xs-3 nopadding-left --no-padding-right" style="width: calc((8.33333333 * (300% + 90px) / 100) - 15px);">
                                <asp:DropDownList runat="server" ID="ddlBusExpenseCurrencyType" CssClass="form-control">
                                    <asp:ListItem Value="VND">VND</asp:ListItem>
                                    <asp:ListItem Value="USD">USD</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <div class="col-xs-3 --no-padding-left" style="width: calc(12.33333333 * (300% + 90px) / 100);">
                                <label>Chi phí xe Limousine</label>
                            </div>
                            <div class="col-xs-6 --no-padding-right" style="width: calc((12.666667 * (300% + 90px) / 100 ) - 15px); padding-right: 8px; padding-left: 0px;">
                                <asp:TextBox ID="txtLimousineExpense" runat="server" placeholder="Chi phí xe Limousine" CssClass="form-control" data-inputmask="'alias': 'numeric', 'groupSeparator': ',', 'autoGroup': true, 'digits': 2, 'digitsOptional': true, 'placeholder': '0'"></asp:TextBox>
                            </div>
                            <div class="col-xs-3 nopadding-left --no-padding-right" style="width: calc((8.33333333 * (300% + 90px) / 100) - 15px);">
                                <asp:DropDownList runat="server" ID="ddlLimousineExpenseCurrencyType" CssClass="form-control">
                                    <asp:ListItem Value="VND">VND</asp:ListItem>
                                    <asp:ListItem Value="USD">USD</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <div class="col-xs-3 --no-padding-left" style="width: calc(12.33333333 * (300% + 90px) / 100);">
                                <label>Chi phí xe Ha Nội Sapa </label>
                            </div>
                            <div class="col-xs-6 --no-padding-right" style="width: calc((12.666667 * (300% + 90px) / 100 ) - 15px); padding-right: 8px; padding-left: 0px;">
                                <asp:TextBox ID="txtBusHaNoiSapaExpense" runat="server" placeholder="Chi phí xe Hà Nội Sapa" CssClass="form-control" data-inputmask="'alias': 'numeric', 'groupSeparator': ',', 'autoGroup': true, 'digits': 2, 'digitsOptional': true, 'placeholder': '0'"></asp:TextBox>
                            </div>
                            <div class="col-xs-3 nopadding-left --no-padding-right" style="width: calc((8.33333333 * (300% + 90px) / 100) - 15px);">
                                <asp:DropDownList runat="server" ID="ddlBusHaNoiSapaExpenseCurrencyType" CssClass="form-control">
                                    <asp:ListItem Value="VND">VND</asp:ListItem>
                                    <asp:ListItem Value="USD">USD</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <div class="col-xs-3 --no-padding-left" style="width: calc(12.33333333 * (300% + 90px) / 100);">
                                <label>Chi phí Commission</label>
                            </div>
                            <div class="col-xs-6 --no-padding-right" style="width: calc((12.666667 * (300% + 90px) / 100 ) - 15px); padding-right: 8px; padding-left: 0px;">
                                <asp:TextBox ID="txtCommissionExpense" runat="server" placeholder="Chi phí Commission" CssClass="form-control" data-inputmask="'alias': 'numeric', 'groupSeparator': ',', 'autoGroup': true, 'digits': 2, 'digitsOptional': true, 'placeholder': '0'"></asp:TextBox>
                            </div>
                            <div class="col-xs-3 nopadding-left --no-padding-right" style="width: calc((8.33333333 * (300% + 90px) / 100) - 15px);">
                                <asp:DropDownList runat="server" ID="ddlCommissionExpenseCurrencyType" CssClass="form-control">
                                    <asp:ListItem Value="VND">VND</asp:ListItem>
                                    <asp:ListItem Value="USD">USD</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-xs-4">
                </div>
                <div class="col-xs-4">
                </div>
            </div>
        </div>
        <div class="form-group" style="display: none">
            <div class="row">
                <div class="col-xs-1">
                    <label for="commission">Commission</label>
                </div>
                <div class="col-xs-2">
                    <asp:TextBox ID="txtCommission" runat="server" placeholder="Commission" CssClass="form-control "></asp:TextBox>
                </div>
                <div class="col-xs-1 nopadding-left">
                    <asp:DropDownList runat="server" ID="ddlCommissionCurrencies" CssClass="form-control">
                        <asp:ListItem Value="1">USD</asp:ListItem>
                        <asp:ListItem Value="0">VND</asp:ListItem>
                    </asp:DropDownList>
                </div>

            </div>
        </div>
        <div class="form-group" style="display: none">
            <div class="row">
                <div class="col-xs-1 nopadding-right">
                    <label for="cancelpenalty">Cancel Penalty</label>
                </div>
                <div class="col-xs-2">
                    <asp:TextBox ID="txtPenalty" runat="server" Text="0" CssClass="form-control" placeholder="Cancel Penalty"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="form-group">
            <div class="row">
                <div class="col-xs-1 --no-padding-right" style="display: none">
                    Voucher Code
                </div>
                <div class="col-xs-4 nopadding-right" style="display: none">
                    <div class="input-group" style="width: 97%">
                        <asp:TextBox ID="txtAllVoucher" placeholder="Voucher code" runat="server" CssClass="form-control"></asp:TextBox>
                        <span class="input-group-btn">
                            <% if (UserIdentity.UserName != "captain.os1")
                               { %>
                            <input type="button" class="btn btn-primary" value="Check Code" id="checkvoucher" style="height: 25px" />
                            <%} %>
                        </span>
                    </div>
                </div>
                <div class="col-xs-7">
                    <p>
                        <asp:Literal runat="server" ID="litCreated"></asp:Literal>
                    </p>
                </div>
            </div>
        </div>
        <div class="form-group" id="extra-service" ng-controller="controllerTransferService">
            <div class="row">
                <div class="col-xs-1 --no-padding-right">
                    <label for="extraservices">Extra Services</label>
                </div>
                <div class="col-xs-1" id="detail-service">
                    <asp:PlaceHolder ID="plhDetailService" runat="server">
                        <asp:Repeater ID="rptExtraServices" runat="server" OnItemDataBound="rptExtraServices_ItemDataBound">
                            <ItemTemplate>
                                <div class="checkbox">
                                    <asp:HiddenField ID="hiddenId" runat="server" Value='<%#Eval("Id") %>' />
                                    <label>
                                        <input id="chkService" runat="server" type="checkbox" ng-model='<%# Eval("Name")%>' /><%#Eval("Name") %></label>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </asp:PlaceHolder>
                </div>
                <div class="col-xs-10" id="transfer-service-details" ng-class="{'ng-hide':Transfers == false}">
                    <div class="row">
                        <div class="col-xs-1 nopadding-right --width-auto">
                            Bus type
                        </div>
                        <div class="col-xs-2" style="margin-top: -8px; width: 10%">
                            <asp:Repeater ID="rptBusType" runat="server">
                                <ItemTemplate>
                                    <div class="radio">
                                        <label>
                                            <input type="radio" name="radBusType" value='<%# Eval("Id") %>'
                                                <%# Booking.Transfer_BusType == null ? (Container.ItemIndex == 0 ? "checked='checked'" : "") 
                                            :(Booking.Transfer_BusType.Id == (int)Eval("Id")?"checked = 'checked'" : "")
                                                %>>
                                            <%# Eval("Name") %>
                                        </label>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                        <div class="col-xs-2 nopadding-right" style="width: 6%">
                            Service
                        </div>
                        <div class="col-xs-2 --width-auto" style="margin-top: -8px">
                            <div class="radio">
                                <label>
                                    <asp:RadioButton runat="server" GroupName="transferService" ID="rbtTransferService_TwoWay" Text="Two ways"
                                        Checked='<%# !String.IsNullOrEmpty(Booking.Transfer_Service) ? (Booking.Transfer_Service == "Two Way" ? true : false) : true%>' />
                                </label>
                            </div>
                            <div class="radio">
                                <label>
                                    <asp:RadioButton runat="server" GroupName="transferService" ID="rbtTransferService_OneWay" Text="One way"
                                        Checked='<%# Booking.Transfer_Service == "One Way" ? true : false %>' />
                                </label>
                            </div>
                        </div>
                        <div class="col-xs-3 --width-auto">
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-xs-4">
                                        Date to
                                    </div>
                                    <div class="col-xs-8">
                                        <asp:TextBox ID="txtTransfer_Dateto" CssClass="form-control"
                                            runat="server" placeholder="Date to(dd/mm/yyyy)" data-control="datetimepicker"
                                            ng-model="transfer_DateTo" ng-change="transferDateToChangedHandler()"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-xs-4">
                                        Date back
                                    </div>
                                    <div class="col-xs-8">
                                        <asp:TextBox ID="txtTransfer_Dateback" CssClass="form-control"
                                            runat="server" placeholder="Date back(dd/mm/yyyy)" data-control="datetimepicker"
                                            ng-model="transfer_DateBack" ng-change="transferDateBackChangedHandler()"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-xs-1 nopadding-right" style="width: 3%">
                            Note
                        </div>
                        <div class="col-xs-5 --no-padding-right" style="width: 32.8%">
                            <asp:TextBox ID="txtTransfer_Note" CssClass="form-control" runat="server" placeholder="Note"
                                TextMode="MultiLine"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="form-group">
            <div class="row">
                <div class="col-xs-12">
                    <asp:Literal runat="server" ID="litInform" Visible="true" />
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-4">
                <label for="pickupaddress">
                    Pickup Address
                </label>
            </div>
            <div class="col-xs-4 --no-padding-left">
                Special Request
            </div>
            <div class="col-xs-4 --no-padding-left">
                Customer Info              
            </div>
        </div>
        <div class="row">
            <div class="col-xs-4">
                <asp:TextBox ID="txtPickup" runat="server" CssClass="form-control" TextMode="MultiLine" placeholder="Pickup Address"></asp:TextBox>
            </div>
            <div class="col-xs-4 --no-padding-left">
                <asp:TextBox ID="txtSpecialRequest" runat="server" CssClass="form-control" TextMode="MultiLine" placeholder="Special Request"></asp:TextBox>
            </div>
            <div class="col-xs-4 --no-padding-left">
                <asp:TextBox ID="txtCustomerInfo" runat="server" CssClass="form-control" TextMode="MultiLine" placeholder="Customer Info"></asp:TextBox>
            </div>
        </div>
        <br />

    </div>

    <div class="form-group">
        <div class="row">
            <div class="col-xs-12">
                <table class="table table-borderless table-common" style="margin-bottom: 0">
                    <asp:Repeater ID="rptCustomers" runat="server" OnItemDataBound="rptCustomers_ItemDataBound">
                        <HeaderTemplate>
                            <tr>
                                <th>Name
                                </th>
                                <th>Gender
                                </th>
                                <th>Type
                                </th>
                                <th>Birthday
                                </th>
                                <th>Nationality
                                </th>
                                <th>Visa No.
                                </th>
                                <th>Passport No.
                                </th>
                                <th>Visa Expired
                                </th>
                                <th style="width: 5%">Viet Kieu
                                </th>
                                <th></th>
                                <th></th>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <uc:customer ID="customerData" runat="server"></uc:customer>
                                <td>
                                    <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" CssClass="btn btn-primary"
                                        Text="Delete" />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </div>
    </div>
    <div class="form-group">
        <div class="row">
            <div class="col-xs-12">
                <asp:PlaceHolder ID="plhAddRoom" runat="server">
                    <asp:DropDownList ID="ddlRoomTypes" runat="server" Visible="false">
                    </asp:DropDownList>
                    <asp:Button ID="btnAddRoom" runat="server" OnClick="btnAddRoom_Click" Text="Add"
                        CssClass="btn btn-primary" />
                </asp:PlaceHolder>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="ScreenCapture" runat="server"></asp:HiddenField>
    <div class="modal fade" id="addBookingModal" tabindex="-1" role="dialog" aria-labelledby="gridSystemModalLabel" data-backdrop="true" data-keyboard="true">
        <div class="modal-dialog" role="document" style="width: 100%">
            <div class="modal-content">
                <div class="modal-header">
                    <%--                    <span>Add booking</span>--%>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                </div>
                <div class="modal-body">
                    <iframe frameborder="0" width="100%" scrolling="yes" onload="resizeIframe(this)"></iframe>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="Scripts" runat="server">
    <script type="text/javascript" src="/modules/sails/admin/bookingviewcontroller.js"></script>
    <script type="text/javascript">
        $(function () {
            $("#<%= txtStartDate.ClientID%>").datetimepicker({
                timepicker: false,
                format: 'd/m/Y',
                scrollInput:false,
                scrollMonth:false
            });

            $("#<%= txtDeadline.ClientID%>").datetimepicker({
                format: 'd/m/Y H:i',
                scrollImput:false,
                scrollMonth:false
            });
        })
    </script>
    <script type="text/javascript">  
        $(function () {
            //workaround datetimepicker do not show date table when off mousedown and focusin and blank input value
            $("[data-control = 'datepicker']").each(function(i,e){
                if($(e).val()==""){
                    $(e).val("workaround");
                }
            })

            $("[data-control = 'datepicker']").datetimepicker({
                timepicker: false,
                format: 'd/m/Y',
                scrollInput:false,
                scrollMonth:false,
            })

            $("[data-control = 'datepicker']").each(function(i,e){
                if($(e).val()=="workaround"){
                    $(e).val("");
                }
            })

            $("[data-control = 'datepicker']").off("mousedown");
            $("[data-control = 'datepicker']").off("focusin");

            $(".fa-calendar").click(function(){
                $(this).siblings("input").datetimepicker("show");
            })
        });

    </script>

    <script>
        DropdownOptionSetVisible();
        $("#<%=ddlTrips.ClientID%>").change(DropdownOptionSetVisible);
        
        function DropdownOptionSetVisible(){
            $("#<%=ddlOptions.ClientID%>").hide();
            if($("#<%=ddlTrips.ClientID%> option:selected").attr("data-option-visible") == "true"){
                $("#<%=ddlOptions.ClientID%>").show();
            }
          
        }
    </script>

    <script>
        TACodeSetVisible();
        $("#<%=ddlAgencies.ClientID%>").change(TACodeSetVisible);

        function TACodeSetVisible(){
            $("#<%=txtAgencyCode.ClientID%>").hide();
            if(!$("#<%=ddlAgencies.ClientID%> option:selected").is($("#<%=ddlAgencies.ClientID%> option:first-child"))){
                $("#<%=txtAgencyCode.ClientID%>").show();
            }
        }
    </script>
    <script type="text/javascript">
        function toggleVisible(id) {
            item = document.getElementById(id);
            if (item.style.display == "") {
                item.style.display = "none";
            } else {
                item.style.display = "";
            }
        }
    </script>
    <script>
        function CheckVoucher() { 
            PopupCenter(url, 'Check Voucher', 400, 600);
        }
    </script>
    <script>
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

    <script>
        function setPersonalInfomation(control, ui) {
            control.val(ui.item.Fullname);
            var divparentControl = control.parents(".row");
            if (ui.item.HasGenderValue === true) {
                if (ui.item.IsMale === false) {
                    divparentControl.find(".ddlGender").val("Female");
                } else {
                    divparentControl.find(".ddlGender").val("Male");
                }
            } else {
                divparentControl.find(".ddlGender").children(":first").prop("selected", "selected");
            }


            if (ui.item.HasBirthdayValue === true) {
                divparentControl.find(".txtBirthday").val(ui.item.Birthday);
            } else {
                divparentControl.find(".txtBirthday").val("");
            }

            if (ui.item.HasNationality === true) {
                divparentControl.find(".ddlNationality").val(ui.item.NationId);
            } else {
                divparentControl.find(".ddlNationality").children(":first").prop("selected", "selected");
            }

            divparentControl.find(".txtVisaNo").val(ui.item.VisaNo);
            divparentControl.find(".txtPassport").val(ui.item.Passport);

            if (ui.item.HasVisaExpiredValue === true) {
                divparentControl.find(".txtVisaExpired").val(ui.item.VisaExpired);
            }

            divparentControl.find(".txtNguyenQuan").val(ui.item.NguyenQuan);

            divparentControl.find(".chkVietKieu").children("input").prop("checked", "checked");

        }

    </script>
    <script>
        var fullNameArray = [];
        $(document).ready(function () {
            $.each($(".acomplete"), function (i, e) {
                if ($(e).val() === "") {
                    fullNameArray.push({ selected: false, originFullName: $(e).val(), control: $(e) });
                } else {
                    fullNameArray.push({ selected: true, originFullName: $(e).val(), control: $(e) });
                }
            });
        });
    </script>
    <script>
        $(document).ready(function () {
            $.each($(".acomplete"), function (i, e) {
                $(e).autocomplete({
                    source: "SearchCustomer.aspx?NodeId=1&SectionId=15",
                    select: function (event, ui) {
                        $.each(fullNameArray, function (index, element) {
                            if ($(e).is(element["control"])) {
                                element["originFullName"] = ui.item.Fullname;
                                element["selected"] = true;
                            }
                        });
                        setPersonalInfomation($(this), ui);
                        return false;
                    }
                }).autocomplete("instance")._renderItem = function (ul, item) {
                    var itemElement = "<a>" + item.Fullname + "<br>";
                    if (item.HasGenderValue === true) {
                        if (item.IsMale === false) {
                            itemElement = itemElement + "<b>Gender : Female</b> ";
                        } else {
                            itemElement = itemElement + "<b>Gender : Male</b> ";
                        }
                    }

                    if (item.HasBirthdayValue === true) {
                        itemElement = itemElement + "<b>Birthday : " + item.Birthday + "</b> ";
                    }

                    if (item.HasNationality === true) {
                        itemElement = itemElement + "<b>Nationality : " + item.Nationality + "</b> ";
                    }
                    itemElement = itemElement + "</a>";
                    return $("<li>").append(itemElement).appendTo(ul);
                };
            });
        });
    </script>
    <script>
        function resetPersonInformation(control) {
            control.siblings(".hiddenId").val("");
        }
    </script>
    <script>
        $(function () {
            $.each($(".acomplete"), function (i, e) {
                $(e).keyup(function () {
                    $.each(fullNameArray, function (index, element) {
                        if ($(e).is(element["control"])) {
                            if (element["selected"] === true) {
                                if ($(e).val() !== element["originFullName"]) {
                                    resetPersonInformation($(e));
                                    element["selected"] = false;
                                }
                            }
                        }
                    });
                });
            });
        })
    </script>
    <script>
        //change all selector nationality follow first selector
        $(function () {
            $(".ddlNationality:first").change(function () {
                $(".ddlNationality").val($(".ddlNationality:first").val());
            });
        });
    </script>
    <script>
        $("#sendemail").colorbox({
            iframe: true,
            width: 1200,
            height: 600,
        });

        if(getParameterValues("confirm") == 1){
            $("#sendemail").colorbox({
                iframe: true,
                width: 1200,
                height: 600,
                open:true
            });    
        }
    </script>
    <script>
        $("#roomorganizer").colorbox({
            iframe: true,
            width: 1200,
            height: 600,
        });
    </script>
    <script>
        AgencyDropdownlistFillTitle();
        $("#<%=ddlAgencies.ClientID%>").change(function(){
            AgencyDropdownlistFillTitle();
        }); 
        function AgencyDropdownlistFillTitle(){
            $("#<%=ddlAgencies.ClientID%>").attr("title", $("#<%=ddlAgencies.ClientID%> option:selected").html());
        }
    </script>
    <script>
        $("#checkvoucher").click(function(){
            var code = document.getElementById('<%= txtAllVoucher.ClientID %>').value;
            var url = 'CheckVoucher.aspx?NodeId=1&SectionId=15&code=' + code + '&bookingid=' + <%= Request.QueryString["bi"] %>;
            $.colorbox({
                href:url,
                iframe:true,
                width:1200,
                height:600,
            })
        });
    </script>
    <script type="text/javascript">
        var screenCapture = {
            capture : function(){
         <%--       var allow = 0;
                $("body").find("div:hidden").remove();
                html2canvas(document.querySelector("body")).then(canvas => {
                    $("#<%= ScreenCapture.ClientID%>").val(canvas.toDataURL().replace('data:image/png;base64,', ''));
                allow = 1;
                })

                var i = setInterval(function(){
                    if(allow === 1){--%>
                $("#<%= button1.ClientID%>").click();
                //        clearInterval(i);
                //    }
                //},1)          
            }
        }
    </script>

    <script>
        $(document).ready(function(){
            $("<%= buttonSubmit.ClientID %>").removeAttr("disabled");
        })
    </script>
    <script>
        $(document).ready(function(){
            var $controllerTransferServiceScope = angular.element(document.querySelector("[ng-controller='controllerTransferService']")).scope();
            $controllerTransferServiceScope.Transfers=Transfers;
        })    
    </script>
    <script>
        $(document).ready(function(){
            var $controllerTransferServiceScope = angular.element(document.querySelector("[ng-controller='controllerTransferService']")).scope();
            $controllerTransferServiceScope.addServiceWatch = function(control){
                $controllerTransferServiceScope.$watch('transfer_Service',function(){
                    if($controllerTransferServiceScope.transfer_Service == "rbtTransferService_TwoWay"){
                        $controllerTransferServiceScope.transfer_DateTo = "<%= Booking.StartDate.ToString("dd/MM/yyyy")%>";
                        $controllerTransferServiceScope.transfer_DateBack = "<%= Booking.EndDate.ToString("dd/MM/yyyy")%>";
                    }else if($controllerTransferServiceScope.transfer_Service == "rbtTransferService_OneWay"){
                        $controllerTransferServiceScope.transfer_DateTo = "<%= Booking.Transfer_DateTo.HasValue 
    ? Booking.Transfer_DateTo.Value.ToString("dd/MM/yyyy"): Booking.StartDate.ToString("dd/MM/yyyy")%>";
                        $controllerTransferServiceScope.transfer_DateBack = "";
                    }
                });
                angular.element(document.querySelector("[ng-controller='controllerTransferService']")).scope().$apply();
            }
            $("#<%= rbtTransferService_OneWay.ClientID %>")
                .attr({"ng-model":"transfer_Service"}).change(function(){
                    angular.element(document.querySelector("[ng-controller='controllerTransferService']")).scope().addServiceWatch(this);
                });
            $("#<%= rbtTransferService_TwoWay.ClientID %>")
                .attr({"ng-model":"transfer_Service","ng-init":"transfer_Service='"
                    +
                    "<%= String.IsNullOrEmpty(Booking.Transfer_Service) 
    ? "rbtTransferService_TwoWay" : (Booking.Transfer_Service == "Two Way" 
    ? "rbtTransferService_TwoWay" : "rbtTransferService_OneWay")%>"
                    +"'"}).change(function(){
                        angular.element(document.querySelector("[ng-controller='controllerTransferService']")).scope().addServiceWatch(this);
                    });
            angular.element(document.querySelector("[ng-controller='controllerTransferService']")).injector().invoke(function($rootScope, $compile) {
                $compile(document.getElementById("<%= rbtTransferService_OneWay.ClientID %>"))($rootScope);
                $compile(document.getElementById("<%= rbtTransferService_TwoWay.ClientID %>"))($rootScope);
            });
        })
    </script>
    <script>
        $(document).ready(function(){   
            var $controllerTransferServiceScope = angular.element(document.querySelector("[ng-controller='controllerTransferService']")).scope();
            function LoadTransferDate(){
                if($controllerTransferServiceScope.transfer_Service == "rbtTransferService_TwoWay"){
                    $controllerTransferServiceScope.transfer_DateTo = "<%= Booking.Transfer_DateTo.HasValue 
    ? Booking.Transfer_DateTo.Value.ToString("dd/MM/yyyy"): Booking.StartDate.ToString("dd/MM/yyyy")%>";
                    $controllerTransferServiceScope.transfer_DateBack = "<%= Booking.Transfer_DateBack.HasValue 
    ? Booking.Transfer_DateBack.Value.ToString("dd/MM/yyyy") : Booking.EndDate.ToString("dd/MM/yyyy")%>";
                }else
                    if($controllerTransferServiceScope.transfer_Service == "rbtTransferService_OneWay"){
                        $controllerTransferServiceScope.transfer_DateTo = "<%= Booking.Transfer_DateTo.HasValue 
    ? Booking.Transfer_DateTo.Value.ToString("dd/MM/yyyy"): ""%>";
                        $controllerTransferServiceScope.transfer_DateBack = "<%= Booking.Transfer_DateBack.HasValue 
    ? Booking.Transfer_DateBack.Value.ToString("dd/MM/yyyy") : ""%>";
                    }  
            }
        
            $controllerTransferServiceScope.transferDateBackChangedHandler = function(){
                if($controllerTransferServiceScope.transfer_Service == "rbtTransferService_OneWay"){
                    $controllerTransferServiceScope.transfer_DateTo = "";
                }
            };
            $controllerTransferServiceScope.transferDateToChangedHandler = function(){
                if($controllerTransferServiceScope.transfer_Service == "rbtTransferService_OneWay"){
                    $controllerTransferServiceScope.transfer_DateBack = "";
                }
            };
            LoadTransferDate();
            $controllerTransferServiceScope.$apply();
        })
    </script>
    <script>
        $(document).ready(function(){
            $("#<%=buttonSubmit.ClientID%>").removeAttr("disabled");
        })
        function closePoup(refesh) {
            $("#addBookingModal").modal('hide');
            if (refesh===1) {
                window.location.href = window.location.href;
            }
        }
        function editRoom(bookingId,roomId) {
            var src = "/Modules/Sails/Admin/HomeChangeRoom.aspx?NodeId=1&SectionId=15&roomId=" + roomId + "&bookingId=" + bookingId + "&tripId="+$("#<%=ddlTrips.ClientID%>").val();
            $("#addBookingModal iframe").attr('src', src);
            $("#addBookingModal").modal();
        }
        function selectRoom(bookingId,bkroomId) {
            var src = "/Modules/Sails/Admin/HomeChangeRoom.aspx?NodeId=1&SectionId=15&bookingRoomId=" + bkroomId + "&bookingId=" + bookingId + "&tripId="+$("#<%=ddlTrips.ClientID%>").val();
            $("#addBookingModal iframe").attr('src', src);
            $("#addBookingModal").modal();
        }

        function addRoom(bookingId) {
            var src = "/Modules/Sails/Admin/BookingAddRoom.aspx?NodeId=1&SectionId=15&addBooking=1&bookingId=" + bookingId ;
            $("#addBookingModal iframe").attr('src', src);
            $("#addBookingModal").modal();
            return false;
        }
    </script>
    <script>
        $("#addBookingModal").on('shown.bs.modal', function(){
            $(this).css('padding-right',0);
        });
    </script>
</asp:Content>
