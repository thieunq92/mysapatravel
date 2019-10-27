<%@ Page Language="C#" MasterPageFile="MO.Master" AutoEventWireup="true"
CodeBehind="ReportDebtReceivables.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.ReportDebtReceivables"
Title="Receivable" %>

<%@ Register Assembly="CMS.ServerControls" Namespace="CMS.ServerControls" TagPrefix="svc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
    <asp:PlaceHolder ID="plhAdminContent" runat="server">
        <div class="form-group">
            <div class="row">
                <div class="col-xs-9">
                    <div class="form-group">
                        <div class="row">
                            <%--<div class="col-xs-1">
                                Từ ngày
                            </div>
                            <div class="col-xs-2">
                                <asp:TextBox ID="txtFrom" runat="server" CssClass="form-control" placeholder="From (dd/MM/yyyy)" data-control="datetimepicker" autocomplete="off"></asp:TextBox>
                            </div>--%>
                            <div class="col-xs-1">
                                Đến ngày
                            </div>
                            <div class="col-xs-2">
                                <asp:TextBox ID="txtTo" runat="server" CssClass="form-control" placeholder="To (dd/MM/yyyy)" data-control="datetimepicker" autocomplete="off" />
                            </div>
                            <div class="col-xs-1">
                                Đối tác
                            </div>
                            <div class="col-xs-2">
                                <asp:TextBox ID="agencySelectornameid" CssClass="form-control" Width="250px" placeholder="Select agency" ReadOnly="True" runat="server"></asp:TextBox>
                                <input id="agencySelector" type="hidden" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="form-group">
            <div class="row">
                <div class="col-xs-3">
                    <asp:Button ID="btnDisplay" runat="server" CssClass="btn btn-primary" Text="Hiển thị" OnClick="btnDisplay_Click" />
                    <asp:Button ID="btnExport" runat="server" CssClass="btn btn-primary" Text="Xuất file" OnClick="btnExport_OnClick" />
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-12">
                <table class="table table-bordered table-hover table-common">
                    <tr class="active">
                        <th>STT
                        </th>
                        <th>Tên đối tác
                        </th>
                        <th>Tổng nợ USD
                        </th>
                        <th>Tổng nợ VND
                        </th>
                        <th>Pay all
                        </th>
                    </tr>
                    <asp:Repeater runat="server" ID="rptReport" OnItemDataBound="rptReport_OnItemDataBound">
                        <ItemTemplate>
                            <tr>
                                <td><%#Container.ItemIndex + 1%>
                                </td>
                                <td>
                                    <asp:HyperLink ID="hplAgency" runat="server"></asp:HyperLink>
                                </td>
                                <td>
                                    <asp:Literal ID="litTotalReceivableUSD" runat="server"></asp:Literal>
                                </td>
                                <td>
                                    <asp:Literal ID="litTotalReceivableVND" runat="server"></asp:Literal>
                                </td>
                                <th></th>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            <tr>
                                <td></td>
                                <td></td>
                                <td>
                                    <asp:Literal ID="litTotalUSD" runat="server"></asp:Literal>
                                </td>
                                <td>
                                    <asp:Literal ID="litTotalVND" runat="server"></asp:Literal>
                                </td>
                                <th>                    <a class="btn btn-primary" onclick="payment();" data-toggle="modal" data-target=".modal-bookingselectedpayment">Payment all</a>
                                </th>
                            </tr>
                        </FooterTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </div>
        <div class="modal fade modal-bookingselectedpayment" tabindex="-1" role="dialog" aria-labelledby="gridSystemModalLabel" data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog" role="document" style="width: 1230px">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close" onclick="closePoup();"><span aria-hidden="true">&times;</span></button>
                        <h3 class="modal-title">Thanh toán tự động</h3>
                    </div>
                    <div class="modal-body">
                        <iframe frameborder="0" width="1200" scrolling="no" onload="resizeIframe(this)"></iframe>
                    </div>
                </div>
            </div>
        </div>
    </asp:PlaceHolder>
</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="Scripts" runat="server">
    <script>
        var datetimePicker = {
            configInputTo: function () {
                $("#<%=txtTo.ClientID%>").datetimepicker({
                    timepicker: false,
                    format: 'd/m/Y',
                    scrollInput: false,
                    scrollMonth: false,
                });
            }
        }

        $(function () {
            datetimePicker.configInputTo();
            colorbox.configPayment();
        })
        $('#<%=agencySelectornameid.ClientID%>').click(function () {
            var width = 800;
            var height = 600;
            window.open('/Modules/Sails/Admin/AgencySelectorPage.aspx?NodeId=1&SectionId=15&clientid=<%=agencySelector.ClientID%>', 'Agencyselect', 'width=' + width + ',height=' + height + ',top=' + ((screen.height / 2) - (height / 2)) + ',left=' + ((screen.width / 2) - (width / 2)));
        });
        function GotoReceivable(nodeId, sectionId, agencyId) {
            var to = $("#<%=txtTo.ClientID%>").val();
            window.open('PaymentReport.aspx?NodeId=' + nodeId + "&SectionId=" + sectionId + "&f=01/01/2017&ai=" + agencyId + "&spay=1" + "&t=" + to, '_blank');
        }

        function payment() {
            let day = $('#<%=txtTo.ClientID%>').val();
            let url = 'BookingSelectedPayment.aspx?NodeId=1&SectionId=15&day=' + day;
            let agencyId = $('#ctl00_ctl00_AdminContentMain_AdminContent_agencySelector').val();
            if (agencyId && agencyId.length > 0) {
                url += "&ai=" + agencyId;
            }
	
            $(".modal-bookingselectedpayment iframe").attr('src', url);
        }
        function closePoup() {
            $('.modal-bookingselectedpayment').modal('toggle');
            window.location.href = window.location.href;
        }

    </script>
    <% if (!AllowExportDebtReceivables)
       { %>
        <script>
            $("#<%=btnExport.ClientID%>").attr({ "disabled": "true", "title": "Bạn không có quyền xuất file báo cáo nợ phải thu" });
        </script>
    <% } %>
</asp:Content>
