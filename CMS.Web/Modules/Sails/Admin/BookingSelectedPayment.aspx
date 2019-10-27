<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="NewPopup.Master" CodeBehind="BookingSelectedPayment.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.BookingSelectedPayment" %>

<asp:Content ID="AdminContent" runat="server" ContentPlaceHolderID="AdminContent">
    <asp:PlaceHolder ID="plhAdminContent" runat="server">
        <asp:PlaceHolder ID="phResult" Visible="False" runat="server">
            <div class="form-group">
                <div class="row alert alert-success">
                    <div class="col-xs-12">
                        <asp:Literal ID="litResultUsd" runat="server"></asp:Literal>
                    </div>
                    <div class="col-xs-12">
                        <asp:Literal ID="litResultVND" runat="server"></asp:Literal>
                    </div>
                </div>
            </div>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="phList" runat="server">
            <table class="table table-bordered table-common ">
                <tr class="header custom-warning">
                    <th rowspan="2">Booking code</th>
                    <th rowspan="2">TA Code</th>
                    <th rowspan="2">Agency</th>
                    <th rowspan="2">Date</th>
                    <th colspan="2">Tổng thu</th>
                    <th colspan="2">Đã thanh toán</th>
                    <th colspan="2">Còn lại</th>
                </tr>
                <tr class="custom-warning">
                    <th>USD</th>
                    <th>VND</th>
                    <th>USD</th>
                    <th>VND</th>
                    <th>USD</th>
                    <th>VND</th>
                </tr>
                <tr>
                    <asp:Repeater runat="server" ID="rptBooking">
                        <ItemTemplate>
                            <tr>
                                <td><%# DataBinder.Eval(Container.DataItem,"Id","OS{0:00000}") %></td>
                                <td>
                                    <%# Eval("AgencyCode")%>
                                </td>
                                <td style="text-align: left!important"><%# Eval("Agency.Name") %></td>
                                <td><%# DataBinder.Eval(Container.DataItem,"StartDate","{0:dd/MM/yyyy}") %>
                                </td>
                                <td style="text-align: right!important"><%#((bool)Eval("IsTotalUsd")) ? ((Double)Eval("Total")).ToString("#,##0.##"):"0"%></td>
                                <td style="text-align: right!important"><%#((bool)Eval("IsTotalUsd")) ? "0":((Double)Eval("Total")).ToString("#,##0.##") %></td>
                                <td style="text-align: right!important"><%# ((Double)Eval("Paid")).ToString("#,##0.##")%></td>
                                <td style="text-align: right!important"><%# ((Double)Eval("PaidBase")).ToString("#,##0.##")%></td>
                                <td style="text-align: right!important">
                                    <%#((bool)Eval("IsTotalUsd")) ? ((Double)Eval("MoneyLeft")).ToString("#,##0.##"):"0"%></td>
                                <td style="text-align: right!important">
                                    <%# ((bool)Eval("IsTotalUsd")) ? "0":((Double)Eval("MoneyLeft")).ToString("#,##0.##") %></td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            <tr style="display: <%= rptBooking.Items.Count == 0 ? "" : "none"%>">
                                <td colspan="100%">Không tìm thấy bản ghi nào
                                </td>
                            </tr>
                            <tr style="font-weight: bold; display: <%= rptBooking.Items.Count > 0 ? "" : "none"%>">
                                <td colspan="4">Tổng</td>
                                <td style="text-align: right!important"><%= ListBooking.Where(x=>x.IsTotalUsd).Select(x=>x.Total).Sum().ToString("#,##0.##")%></td>
                                <td style="text-align: right!important"><%= ListBooking.Where(x=>x.IsTotalUsd==false).Select(x=>x.Total).Sum().ToString("#,##0.##")%></td>
                                <td style="text-align: right!important"><%= ListBooking.Select(x=>x.Paid).Sum().ToString("#,##0.##")%></td>
                                <td style="text-align: right!important"><%= ListBooking.Select(x=>x.PaidBase).Sum().ToString("#,##0.##")%></td>
                                <td style="text-align: right!important"><%= ListBooking.Where(x=>x.IsTotalUsd).Select(x=>x.MoneyLeft).Sum().ToString("#,##0.##")%></td>
                                <td style="text-align: right!important"><%= ListBooking.Where(x=>x.IsTotalUsd==false).Select(x=>x.MoneyLeft).Sum().ToString("#,##0.##")%></td>
                            </tr>
                        </FooterTemplate>
                    </asp:Repeater>
                </tr>
            </table>
            <div class="form-group">
                <div class="row">
                    <div class="col-xs-2">
                    </div>
                    <div class="col-xs-1" style="text-align: right!important; width: 10%">
                        USD
                    </div>

                    <div class="col-xs-1" style="text-align: right!important; width: 10%">
                        VND
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="row">
                    <div class="col-xs-2">
                        Tổng thu
                    </div>
                    <div class="col-xs-1" style="text-align: right!important; width: 10%">
                        <%= ListBooking.Where(x=>x.IsTotalUsd).Select(x=>x.Total).Sum().ToString("#,##0.##")%>
                    </div>
                    <div class="col-xs-1" style="text-align: right!important; width: 10%">
                        <%= ListBooking.Where(x=>x.IsTotalUsd==false).Select(x=>x.Total).Sum().ToString("#,##0.##")%>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="row">
                    <div class="col-xs-2">
                        Tổng Đã thanh toán
                    </div>

                    <div class="col-xs-1" style="text-align: right!important; width: 10%">
                        <%= ListBooking.Select(x=>x.Paid).Sum().ToString("#,##0.##")%>
                    </div>

                    <div class="col-xs-1" style="text-align: right!important; width: 10%">
                        <%= ListBooking.Select(x=>x.PaidBase).Sum().ToString("#,##0.##")%>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="row">
                    <div class="col-xs-2">
                        Tổng Còn lại
                    </div>

                    <div class="col-xs-1" style="text-align: right!important; width: 10%">
                        <%--                    <%= ListBooking.Where(x=>x.IsTotalUsd).Select(x=>x.MoneyLeft).Sum().ToString("#,##0.##")%>--%>
                        <%= ListBooking.Where(x=>x.IsTotalUsd).Select(x=>x.MoneyLeft).Sum().ToString("#,##0.##")%>
                    </div>

                    <div class="col-xs-1" style="text-align: right!important; width: 10%">
                        <%= ListBooking.Where(x=>x.IsTotalUsd==false).Select(x=>x.MoneyLeft).Sum().ToString("#,##0.##")%>
                    </div>
                </div>
            </div>
        </asp:PlaceHolder>
        <div class="form-group">
            <div class="row">
                <div class="col-md-12">
                    <label for="usdpaid">
                        Có thể cùng lúc thanh toán bằng USD và VND
                    </label>
                </div>
            </div>
        </div>
        <div class="form-group">
            <div class="row">
                <div class="col-md-2">
                    <label for="usdpaid">
                        Tính theo Tỷ giá : 
                    </label>
                </div>
                <div class="col-xs-2">
                    <div class="input-group">
                        <asp:TextBox ID="txtRate" runat="server" CssClass="form-control" Text="" data-inputmask="'alias': 'numeric', 'groupSeparator': ',', 'autoGroup': true, 'digits': 2, 'digitsOptional': true, 'rightAlign':true" Style="padding: 0" />
                    </div>
                </div>
            </div>
        </div>
        <div class="form-group">
            <div class="row">
                <div class="col-md-2">
                    <label for="usdpaid">
                        Số tiền thanh toán USD
                    </label>
                </div>
                <div class="col-xs-3">
                    <div class="input-group">
                        <asp:TextBox ID="txtPaidUSD" runat="server" CssClass="form-control" autocomplete="off" placeholder="USD" Text="0" data-inputmask="'alias': 'numeric', 'groupSeparator': ',', 'autoGroup': true, 'digits': 2, 'digitsOptional': true, 'rightAlign':true" Style="padding: 0" />
                        <span class="input-group-addon" style="padding-left: 3px">$</span>
                    </div>
                </div>
            </div>
        </div>
        <div class="form-group">
            <div class="row">
                <div class="col-md-2">
                    <label for="usdpaid">
                        Số tiền thanh toán quy VND
                    </label>
                </div>
                <div class="col-xs-3">
                    <div class="input-group">
                        <asp:TextBox ID="txtPaidVND" runat="server" CssClass="form-control" autocomplete="off" placeholder="VND" Text="0" data-inputmask="'alias': 'numeric', 'groupSeparator': ',', 'autoGroup': true, 'digits': 2, 'digitsOptional': true, 'rightAlign':true" Style="padding: 0" />
                        <span class="input-group-addon" style="padding-left: 3px">₫</span>
                    </div>
                </div>
            </div>
        </div>
        <div class="form-group">
            <div class="row">
                <div class="col-md-2">
                    <label for="usdpaid">
                        Ghi chú               
                    </label>
                </div>
                <div class="col-md-8">
                        <asp:TextBox ID="txtNote" runat="server" CssClass="form-control" placeholder="Note"/>
                </div>
            </div>
        </div>
        <%--<div class="form-group">
            <div class="row">
                <div class="col-xs-2">
                    Số tiền thiếu sau thanh toán
                </div>
                <div class="col-xs-1" style="text-align: right!important; width: 10%">
                    <span id="lblDeficit"></span>
                </div>
            </div>
        </div>
        <div class="form-group">
            <div class="row">
                <div class="col-xs-2">
                    Số tiền còn lại nhỏ nhất 
                </div>
                <div class="col-xs-1" style="text-align: right!important; width: 10%">
                    <span id="lblLowwestReceivable"
                        data-bookingid='<%= ListBooking.Count > 0 ? ListBooking.OrderBy(x=>x.Receivable).FirstOrDefault().Id.ToString() : ""%>'
                        data-bookingcode='<%= ListBooking.Count > 0 ? ListBooking.OrderBy(x=>x.Receivable).FirstOrDefault().Code : ""%>'>
                        <%= ListBooking.Count > 0 ? ListBooking.OrderBy(x=>x.Receivable).FirstOrDefault().Receivable.ToString("#,##0.##") + "₫" : "0₫" %>
                    </span>
                </div>
            </div>
        </div>--%>
        <div class="form-group">
            <div class="row">
                <div class="col-xs-12">
                    <asp:Button runat="server" ID="btnPayment20" CssClass="btn btn-primary" Text="Thanh toán" OnClick="btnPayment20_OnClick"></asp:Button>

                    <asp:Button runat="server" ID="btnPayment" CssClass="btn btn-primary" Text="Thanh toán" OnClick="btnPayment_Click"></asp:Button>
                    <asp:Button ID="btnClose" runat="server" Text="Đóng" OnClientClick="closePoup();return false;" CssClass="btn btn-default" />
                </div>
            </div>
        </div>
    </asp:PlaceHolder>
</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="Scripts" runat="server">

    <%--<script>
        $(document).ready(function () {
            calculateDeficit();
        })
        $("#<%= txtPaid.ClientID %>").on("input", function () {
            calculateDeficit();
        })
        function calculateDeficit() {
            var totalReceivable = parseFloat($("#lblTotalReceivable").html().replace('₫', '').replace(/,/g, ''));
            var totalPaid = parseFloat($("#<%= txtPaid.ClientID %>").val().replace(/,/g, ''));
            var deficit = totalReceivable - totalPaid;
            $("#lblDeficit").html(deficit.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",") + "₫");
        }
    </script>--%>
    <script>
        function closePoup() {
            window.parent.closePoup();
        }
        $.validator.addMethod("valueNotEquals", function (value, element, arg) {
            return arg !== value;
        }, "");
        $("#aspnetForm").validate({
            rules: '',
            messages: '',
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
    </script>
    <script>
        <%--$("#<%= btnPayment.ClientID %>").click(function (e) {
            e.preventDefault();
            var lowwestReceivable = parseFloat($("#lblLowwestReceivable").html().replace('₫', '').replace(/,/g, ''));
            var totalReceivable = parseFloat($("#lblTotalReceivable").html().replace('₫', '').replace(/,/g, ''));
            var totalPaid = parseFloat($("#<%= txtPaid.ClientID %>").val().replace(/,/g, ''));
            var deficit = totalReceivable - totalPaid;
            var excessCash = totalPaid - (totalReceivable - lowwestReceivable);
            if (deficit < lowwestReceivable) {
                $.confirm({
                    title: "",
                    content: "Số tiền thừa " + excessCash.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",") + "₫ " + "sẽ được trả cho booking <a href='BookingView.aspx?NodeId=1&SectionId=15&bi=" + $("#lblLowwestReceivable").attr("data-bookingId") + "'>"
                        + $("#lblLowwestReceivable").attr("data-bookingCode") + "</a>. Hãy xác nhận",
                    buttons: {
                        confirm: {
                            text: 'Xác nhận',
                            btnClass: 'btn-blue',
                            action: function () {
                                __doPostBack("<%= btnPayment.UniqueID %>", "OnClick");
                            },
                        },
                        cancel: {
                            text: "Hủy",
                        }
                    }
                });
            }
        })--%>
    </script>
    <%--<% if (!AllowPaymentBooking)
       {%>
        <script>
            $("#<%= btnPayment.ClientID%>").attr({"disabled":"true","title":"Bạn không có quyền thanh toán đặt chỗ"})
        </script>
    <%}%>--%>
</asp:Content>
