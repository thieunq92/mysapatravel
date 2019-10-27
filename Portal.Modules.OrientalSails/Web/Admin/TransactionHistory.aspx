<%@ Page Title="" Language="C#" MasterPageFile="MO-Main.Master" AutoEventWireup="true" CodeBehind="TransactionHistory.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.TransactionHistory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadMain" runat="server">
    <title>Payment history
    </title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptManagerMain" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="AdminContentMain" runat="server">
    <asp:PlaceHolder ID="plhHistory" runat="server">
        <h2>Payment history</h2>
        <div class="search_panel">
            <div class="form-group">
                <div class="row">
                    <div class="col-md-1">
                        From
                    </div>
                    <div class="col-md-1">
                        <asp:TextBox ID="txtFrom" runat="server" CssClass="form-control" placeholder="From date (dd/mm/yyyy)"></asp:TextBox>
                    </div>
                    <div class="col-md-1">
                        To
                    </div>
                    <div class="col-md-1">
                        <asp:TextBox ID="txtTo" runat="server" CssClass="form-control" placeholder="To date (dd/mm/yyyy)"></asp:TextBox>
                    </div>
                    <div class="col-md-1">
                        Transaction code
                    </div>
                    <div class="col-md-1">
                        <asp:TextBox ID="txtGroupCode" runat="server" placeholder="group code" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-1">
                        <asp:Button ID="btnDisplay" runat="server" Text="Display" OnClick="btnDisplay_Click"
                                    CssClass="btn btn-primary" />
                    </div>
                </div>
            </div>
        </div>
       
        <div class="paymenthistory-table">
            <div class="table-responsive">
                <table class="table table-bordered table-hover">
                    <tr>
                        <th rowspan="2">Time</th>
                        <th rowspan="2">Pay by</th>
                        <th colspan="2">Amount</th>
                        <th rowspan="2">Rate</th>
                        <th rowspan="2">Code</th>
                        <th rowspan="2">Created by</th>
                        <th rowspan="2">Note</th>
                    </tr>
                    <tr>
                        <th>USD</th>
                        <th>VND</th>
                    </tr>
                    <asp:Repeater ID="rptPaymentHistory" runat="server" OnItemDataBound="rptPaymentHistory_ItemDataBound">
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <asp:Literal ID="litTime" runat="server"></asp:Literal></td>
                                <td>
                                    <asp:Literal ID="litPayBy" runat="server"></asp:Literal></td>
                                <td>
                                    <asp:Literal ID="litAmountUSD" runat="server"></asp:Literal></td>
                                <td>
                                    <asp:Literal ID="litAmountVND" runat="server"></asp:Literal></td>
                                <td>
                                    <asp:Literal ID="litRate" runat="server"></asp:Literal></td>
                                <td>
                                    <asp:HyperLink runat="server" ID="hplGroupCode"></asp:HyperLink></td>
                                <td>
                                    <asp:Literal ID="litCreatedBy" runat="server"></asp:Literal></td>
                                <td>
                                    <asp:Literal ID="litNote" runat="server"></asp:Literal></td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            <tr>
                                <th colspan="2">Total</th>
                                <th>
                                    <asp:Literal ID="litTotalUSD" runat="server"></asp:Literal></th>
                                <th>
                                    <asp:Literal ID="litTotalVND" runat="server"></asp:Literal></th>
                            </tr>
                        </FooterTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </div>
    </asp:PlaceHolder>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ScriptsMain" runat="server">
    <script>
        var datetimePicker = {
            configInputFrom: function () {
                $("#<%=txtFrom.ClientID%>").datetimepicker({
                    timepicker: false,
                    format: 'd/m/Y',
                    scrollInput: false,
                    scrollMonth: false,
                });
            },

            configInputTo: function () {
                $("#<%=txtTo.ClientID%>").datetimepicker({
                    timepicker: false,
                    format: 'd/m/Y',
                    scrollInput: false,
                    scrollMonth: false,
                });
            }
        }

      
        $(function() {
            datetimePicker.configInputFrom();
            datetimePicker.configInputTo();
        });
       
    </script>
</asp:Content>
