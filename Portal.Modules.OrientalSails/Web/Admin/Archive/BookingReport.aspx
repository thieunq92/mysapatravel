<%@ Page Language="C#" MasterPageFile="..\MO.Master" AutoEventWireup="true"
    CodeBehind="BookingReport.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.Archive.BookingReport"
    Title="Booking Reporting" %>

<%@ Import Namespace="Portal.Modules.OrientalSails.Domain" %>
<%@ Register Assembly="CMS.ServerControls" Namespace="CMS.ServerControls" TagPrefix="svc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
    <div class="page-header">
        <h3>Booking by date</h3>
    </div>
    <div class="form-group">
        <div class="row">
            <div class="col-xs-1 nopadding-right">
                Date to view 
            </div>
            <div class="col-xs-2 nopadding-left nopadding-right">
                <asp:TextBox ID="txtDate" runat="server" CssClass="form-control" data-control="datetimepicker" autocomplete="off"></asp:TextBox>
            </div>
            <div class="col-xs-2">
                <asp:TextBox runat="server" ID="txtBookingCode" placeholder="Booking code" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
    </div>
    <div class="form-group">
        <div class="row">
            <div class="col-xs-12">
                <asp:Button ID="btnDisplay" runat="server" Text="Display" OnClick="btnDisplay_Click"
                    CssClass="btn btn-primary" />
                <asp:Button ID="btnViewFeedback" runat="server" Text="View feedback" OnClick="btnViewFeedback_Click"
                    CssClass="btn btn-primary" />
                <asp:Button ID="btnOrganizer" runat="server" Text="View room organizer" CssClass="btn btn-primary" />
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-xs-12 btn-grid">
            <asp:Repeater ID="rptCruises" runat="server" OnItemDataBound="rptCruises_ItemDataBound">
                <HeaderTemplate>
                    <asp:HyperLink ID="hplCruises" runat="server" Text="All" CssClass="btn btn-default"></asp:HyperLink>
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:HyperLink ID="hplCruises" runat="server" CssClass="btn btn-default"></asp:HyperLink>
                </ItemTemplate>
            </asp:Repeater>
            <asp:HyperLink runat="server" ID="hplLimousineTab" Text="Limousine" CssClass="btn btn-default"></asp:HyperLink>
        </div>
    </div>
    <input type="button" id="btnComment" runat="server" value="View Comment" class="button"
        visible="false" />
    <br />
    <div class="row">
        <div class="col-xs-12">
            <table class="table table-bordered table-hover table-common">
                <asp:Repeater ID="rptBookingList" runat="server" OnItemDataBound="rptBookingList_ItemDataBound">
                    <HeaderTemplate>
                        <tr class="header active">
                            <th rowspan="2" <%= inspectionBookingList.Count == 0 && customerBirthdayList.Count == 0 ? "style = 'display:none'":"" %><%= inspectionBookingList.Count != 0 && customerBirthdayList.Count != 0 ? "colspan = '2'":""%>></th>
                            <th rowspan="2">No
                            </th>
                            <th rowspan="2">Name of pax
                            </th>
                            <th rowspan="2">Group
                            </th>
                            <th colspan="3">Number of pax
                            </th>
                            <th rowspan="2">Trip
                            </th>
                            <th rowspan="2">Pickup address
                            </th>
                            <th rowspan="2">Special request
                            </th>
                            <asp:PlaceHolder ID="plhHidden" runat="server" Visible="false">
                                <th colspan="2">
                                    Number Of Cabin
                                </th>
                                <th colspan="2"></th>
                            </asp:PlaceHolder>
                            <th rowspan="2">Agency
                            </th>
                            <th rowspan="2">Booking code
                            </th>
                            <asp:PlaceHolder runat="server" ID="plhTotal">
                                <th rowspan="2">Total
                                </th>
                            </asp:PlaceHolder>
                        </tr>
                        <tr class="active">
                            <th>Adult
                            </th>
                            <th>Child
                            </th>
                            <th>Baby
                            </th>
                            <asp:PlaceHolder ID="plhHidden2" runat="server" Visible="false">
                                <th>Double
                                </th>
                                <th>Twin
                                </th>
                                <th>Adult
                                </th>
                                <th>Child
                                </th>
                            </asp:PlaceHolder>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr class="item" id="trItem" runat="server">
                            <td runat="server" id="tdInspection" width="1%">
                                <asp:Literal ID="litInspection" runat="server"> </asp:Literal>
                            </td>
                            <td runat="server" id="tdBirthday" width="1%">
                                <asp:Literal ID="litBirthday" runat="server"> </asp:Literal>
                            </td>
                            <td>
                                <asp:Literal ID="litIndex" runat="server"></asp:Literal>
                                <asp:HiddenField ID="hiddenId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "Id") %>' />
                            </td>
                            <td>
                                <asp:Label ID="label_NameOfPax" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlGroup" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                                <asp:Literal ID="litGroup" runat="server"></asp:Literal>
                            </td>
                            <td>
                                <asp:Label ID="label_NoOfAdult" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="label_NoOfChild" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="label_NoOfBaby" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="labelItinerary" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="labelPuAddress" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="labelSpecialRequest" runat="server"></asp:Label>
                            </td>
                            <td id="hidden1" runat="server" visible="false">
                                <asp:Label ID="label_NoOfDoubleCabin" runat="server"></asp:Label>
                            </td>
                            <td id="hidden2" runat="server" visible="false">
                                <asp:Label ID="label_NoOfTwinCabin" runat="server"></asp:Label>
                            </td>
                            <td id="hidden3" runat="server" visible="false">
                                <asp:Label ID="label_NoOfTransferAdult" runat="server"></asp:Label>
                            </td>
                            <td id="hidden4" runat="server" visible="false">
                                <asp:Label ID="label_NoOfTransferChild" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:HyperLink ID="hyperLink_Partner" runat="server"></asp:HyperLink>
                            </td>
                            <td>
                                <asp:HyperLink ID="hplCode" runat="server"></asp:HyperLink>
                            </td>
                            <td runat="server" id="plhTotal">
                                <asp:Label ID="label_TotalPrice" runat="server"></asp:Label>
                            </td>
                            <td>
                                <a id="anchorFeedback" runat="server" style="cursor: pointer;">Feedback</a>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        <tr class="item">
                            <%
                                int colspan = 4;

                                if (customerBirthdayList.Count == 0 || inspectionBookingList.Count == 0)
                                {
                                    colspan = 3;
                                }

                                if (customerBirthdayList.Count == 0 && inspectionBookingList.Count == 0)
                                {
                                    colspan = 2;
                                }
                            %>
                            <td colspan="<%= colspan %>">
                                Grand total
                            </td>
                            <td></td>
                            <td>
                                <strong>
                                    <asp:Label ID="label_NoOfAdult" runat="server"></asp:Label></strong>
                            </td>
                            <td>
                                <strong>
                                    <asp:Label ID="label_NoOfChild" runat="server"></asp:Label></strong>
                            </td>
                            <td>
                                <strong>
                                    <asp:Label ID="label_NoOfBaby" runat="server"></asp:Label></strong>
                            </td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <asp:PlaceHolder ID="plhHidden" runat="server" Visible="false">
                                <td>
                                    <strong>
                                        <asp:Label ID="label_NoOfDoubleCabin" runat="server"></asp:Label></strong>
                                </td>
                                <td>
                                    <strong>
                                        <asp:Label ID="label_NoOfTwinCabin" runat="server"></asp:Label></strong>
                                </td>
                                <td>
                                    <strong>
                                        <asp:Label ID="label_NoOfTransferAdult" runat="server"></asp:Label></strong>
                                </td>
                                <td>
                                    <strong>
                                        <asp:Label ID="label_NoOfTransferChild" runat="server"></asp:Label></strong>
                                </td>
                            </asp:PlaceHolder>
                            <td></td>
                            <td></td>
                            <td runat="server" id="plhTotal">
                                <strong>
                                    <asp:Label ID="label_TotalPrice" runat="server"></asp:Label></strong>
                            </td>
                        </tr>
                    </FooterTemplate>
                </asp:Repeater>
                <asp:Repeater runat="server" ID="rptShadows" OnItemDataBound="rptBookingList_ItemDataBound">
                    <HeaderTemplate>
                        <tr>
                            <td colspan="12" style="background-color: #FF7F7F;">Booking moved (to another date) or cancelled need attention (within 7 days if <
                                    6 cabins and within 45 days if >=6 cabins)
                            </td>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr class="item" id="trItem" runat="server">
                            <td runat="server" id="tdInspection">
                                <asp:Literal ID="litInspection" runat="server"> </asp:Literal>
                            </td>
                            <td runat="server" id="tdBirthday">
                                <asp:Literal ID="litBirthday" runat="server"> </asp:Literal>
                            </td>
                            <td>
                                <asp:Literal ID="litIndex" runat="server"></asp:Literal>
                            </td>
                            <td>
                                <asp:Label ID="label_NameOfPax" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlGroup" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                                <asp:Literal ID="litGroup" runat="server"></asp:Literal></td>
                            <td>
                                <asp:Label ID="label_NoOfAdult" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="label_NoOfChild" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="label_NoOfBaby" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="labelItinerary" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="labelPuAddress" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="labelSpecialRequest" runat="server"></asp:Label>
                            </td>
                            <td id="hidden1" runat="server" visible="false">
                                <asp:Label ID="label_NoOfDoubleCabin" runat="server"></asp:Label>
                            </td>
                            <td id="hidden2" runat="server" visible="false">
                                <asp:Label ID="label_NoOfTwinCabin" runat="server"></asp:Label>
                            </td>
                            <td id="hidden3" runat="server" visible="false">
                                <asp:Label ID="label_NoOfTransferAdult" runat="server"></asp:Label>
                            </td>
                            <td id="hidden4" runat="server" visible="false">
                                <asp:Label ID="label_NoOfTransferChild" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:HyperLink ID="hyperLink_Partner" runat="server"></asp:HyperLink>
                            </td>
                            <td>
                                <asp:HyperLink ID="hplCode" runat="server"></asp:HyperLink>
                            </td>
                            <td>
                                <asp:Label ID="label_TotalPrice" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:HyperLink runat="server" ID="hplStartDate"></asp:HyperLink>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
    </div>
    <div class="row">
        <div class="col-xs-3">
            <asp:TextBox ID="txtLockDescription" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>
            <asp:Button ID="btnLock" runat="server" CssClass="btn btn-primary" OnClick="btnLock_Click" />
        </div>
        <div class="col-xs-9 text-right">
            <asp:Literal runat="server" ID="litLockIncome"></asp:Literal>
            <asp:Button ID="btnLockIncome" runat="server" CssClass="btn btn-primary" OnClick="btnLockIncome_Click"
                Text="Lock income" OnClientClick="return confirm('Khi đã khóa doanh thu thì sẽ không thể nào mở khóa được! Bạn xác nhận muốn khóa chứ?')" />
            <asp:Button ID="btnUnlockIncome" Visible="False" runat="server" CssClass="btn btn-primary"
                OnClick="btnUnlockIncome_Click" Text="Unlock income" />
        </div>
    </div>
    <br />
    <div class="row">
        <div class="col-xs-12">
            <asp:PlaceHolder ID="plhDailyExpenses" runat="server">
                <table class="table borderless table-expense">
                    <asp:Repeater ID="rptCruiseExpense" runat="server" OnItemDataBound="rptCruiseExpense_ItemDataBound">
                        <ItemTemplate>
                            <asp:PlaceHolder runat="server" ID="plhCruiseExpense">
                                <tr>
                                    <td colspan="7" style="padding-top: 10px">
                                        <asp:HiddenField ID="hiddenId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"Id") %>' />
                                        <strong><%# DataBinder.Eval(Container.DataItem,"Name") %></strong>
                                    </td>
                                </tr>
                                <asp:Repeater ID="rptServices" runat="server" OnItemDataBound="rptServices_ItemDataBound">
                                    <ItemTemplate>
                                        <tr id="seperator" runat="server" class="seperator" visible="false">
                                            <td colspan="8" style="border-top: solid 1px #eee">&nbsp
                                            </td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td>
                                                <asp:HiddenField ID="hiddenId" runat="server" />
                                                <asp:HiddenField ID="hiddenType" runat="server" />
                                                <asp:Literal ID="litType" runat="server"></asp:Literal>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtName" runat="server" CssClass="form-control" placeholder="Name"></asp:TextBox>
                                                <asp:DropDownList ID="ddlGuides" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control" placeholder="Phone"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlSuppliers" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtCost" runat="server" CssClass="form-control" input-mask="{'alias': 'numeric', 'groupSeparator': ',', 'autoGroup': true, 'digits': 2, 'digitsOptional': true, 'rightAlign':false}"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlGroups" runat="server" Visible="true" CssClass="form-control">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:Button ID="btnRemove" runat="server" Text="Remove"
                                                    CssClass="btn btn-primary" OnClick="btnRemove_Click" />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <tr>
                                    <td colspan="7">
                                        <asp:Button ID="btnAddServiceBlock" Text="Add block" CssClass="btn btn-primary" runat="server"
                                            OnClick="btnAddServiceBlock_Click" />
                                        <asp:Repeater ID="rptAddServices" runat="server" OnItemDataBound="rtpAddServices_ItemDataBound"
                                            Visible="true">
                                            <ItemTemplate>
                                                <asp:Button ID="btnAddService" runat="server" OnClick="btnAddService_Click" CssClass="btn btn-primary"
                                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' />
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </td>
                                </tr>
                            </asp:PlaceHolder>
                        </ItemTemplate>
                        <FooterTemplate>
                            <tr>
                                <td colspan="5">Tổng
                                </td>
                                <td>
                                    <strong>
                                        <asp:Literal ID="litTotal" runat="server"></asp:Literal></strong>
                                </td>
                            </tr>
                        </FooterTemplate>
                    </asp:Repeater>
                </table>
            </asp:PlaceHolder>
        </div>
    </div>
    <svc:Popup runat="server" ID="popupManager">
    </svc:Popup>
    <div class="row">
        <div class="col-xs-12 btn-grid">
            <asp:Button ID="btnSaveExpenses" runat="server" Text="Save without export" OnClick="btnSaveExpenses_Click"
                CssClass="btn btn-primary" />
            <asp:Button ID="btnExport" runat="server" Text="Export tour" OnClick="btnExport_Click"
                CssClass="btn btn-primary" />
            <asp:Button ID="btnExportLimousine" runat="server" Text="Export tour" Visible="false" OnClick="btnExportLimousine_Click"
                CssClass="btn btn-primary" />
            <asp:Button ID="btnExport_3" runat="server" Text="Export 3 day" OnClick="btnExport_3_Click"
                CssClass="btn btn-primary" />
            <asp:Button ID="btnIncomeDate" runat="server" Text="Xuất doanh thu" OnClick="btnIncomeDate_Click"
                CssClass="btn btn-primary" />
            <asp:Button ID="btnExportRoom" runat="server" Text="Export room list" OnClick="btnExportRoom_Click"
                CssClass="btn btn-primary" />
            <asp:Button ID="btnExportWelcome" runat="server" Text="Export Welcome board" OnClick="btnExportWelcome_Click"
                CssClass="btn btn-primary" />
            <asp:Button ID="btnExcel" runat="server" Text="Export customer data" OnClick="btnExcel_Click"
                CssClass="btn btn-primary" />
            <asp:Button ID="btnProvisional" runat="server" Text="Export provisional register"
                OnClick="btnProvisional_Click" CssClass="btn btn-primary" />
        </div>
    </div>
</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="Scripts" runat="server">
    <script>
        $(document).ready(function () {
            $(".btn-grid a").each(function (i, e) {
                if ($(e).html().indexOf("Transfer") >= 0
                    || $(e).html().indexOf("OS Scorpio") >= 0
                    || $(e).html().indexOf("Daily Charter") >= 0) {
                    $(e).hide();
                }
            })
        })
    </script>
    <script>
        $(document).ready(function () {
            $(".btn-grid a").each(function (i, e) {
                if ($(e).html().indexOf("Limousine") >= 0) {
                    $(e).hide();
                }
            })
        })
    </script>
</asp:Content>
