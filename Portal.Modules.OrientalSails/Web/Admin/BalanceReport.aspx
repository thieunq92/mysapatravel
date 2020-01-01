<%@ Page Language="C#" MasterPageFile="MO.Master" AutoEventWireup="true"
    CodeBehind="BalanceReport.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.BalanceReport"
    Title="Báo cáo thu chi" %>

<%@ Import Namespace="Portal.Modules.OrientalSails.Domain" %>
<asp:Content ID="AdminContent" ContentPlaceHolderID="AdminContent" runat="server">
    <div class="row">
        <div class="col-xs-1 --text-right">
            Từ ngày 
        </div>
        <div class="col-xs-2 --no-padding-left">
            <asp:TextBox ID="txtTuNgay" CssClass="form-control" data-control="datetimepicker" placeholder="Từ ngày (dd/mm/yyyy)" runat="server"></asp:TextBox>
        </div>
        <div class="col-xs-1 --text-right">
            Đến ngày
        </div>
        <div class="col-xs-2 --no-padding-left">
            <asp:TextBox ID="txtDenNgay" CssClass="form-control" data-control="datetimepicker" placeholder="Đến ngày (dd/mm/yyyy)" runat="server"></asp:TextBox>
        </div>
        <div class="col-xs-1 --no-padding-left">
            <asp:Button runat="server" ID="btnHienThi" CssClass="btn btn-primary" Text="Hiển thị" OnClick="btnHienThi_Click" />
        </div>
    </div>
    <br />
    <div class="row">
        <div class="col-xs-12">
            <table class="table table-bordered table-common table-total">
                <tr class="active">
                    <th rowspan="2">Booking code</th>
                    <th rowspan="2">Tổng thu</th>
                    <th rowspan="2">Tổng chi</th>
                    <th rowspan="2">Lợi nhuận</th>
                    <th colspan="8">Chi phí</th>
                </tr>
                <tr class="active">
                    <th>Guide</th>
                    <th>Ăn</th>
                    <th>Hotel</th>
                    <th>Vé thắng cảnh</th>
                    <th>Xe tại Sapa</th>
                    <th>Xe Limousine</th>
                    <th>Xe Hà Nội - Sapa</th>
                    <th>Commission</th>
                </tr>
                <asp:Repeater ID="rptBooking" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td><a href="BookingView.aspx?NodeId=1&SectionId=15&bi=<%#((Booking)Container.DataItem).Id%>"><%#string.Format("MYS{0:00000}",((Booking)Container.DataItem).Id)%></td>
                            <td class="--text-right"><%#((Booking)Container.DataItem).Total.ToString("#,0.##") %></td>
                            <td class="--text-right"><%#(((Booking)Container.DataItem).Expense_Guide 
                                                     + ((Booking)Container.DataItem).Expense_Meal 
                                                     + ((Booking)Container.DataItem).Expense_Hotel
                                                     + ((Booking)Container.DataItem).Expense_Ticket
                                                     + ((Booking)Container.DataItem).Expense_Bus
                                                     + ((Booking)Container.DataItem).Expense_Limousine
                                                     + ((Booking)Container.DataItem).Expense_Bus_HanoiSapa
                                                     + ((Booking)Container.DataItem).Expense_Commission).ToString("#,0.##")%></td>
                            <td class="--text-right"><%# (((Booking)Container.DataItem).Total - (((Booking)Container.DataItem).Expense_Guide 
                                                     + ((Booking)Container.DataItem).Expense_Meal 
                                                     + ((Booking)Container.DataItem).Expense_Hotel
                                                     + ((Booking)Container.DataItem).Expense_Ticket
                                                     + ((Booking)Container.DataItem).Expense_Bus
                                                     + ((Booking)Container.DataItem).Expense_Limousine
                                                     + ((Booking)Container.DataItem).Expense_Bus_HanoiSapa
                                                     + ((Booking)Container.DataItem).Expense_Commission)).ToString("#,0.##")%></td>
                            <td class="--text-right"><%#((Booking)Container.DataItem).Expense_Guide.ToString("#,0.##") %></td>
                            <td class="--text-right"><%#((Booking)Container.DataItem).Expense_Meal.ToString("#,0.##") %></td>
                            <td class="--text-right"><%#((Booking)Container.DataItem).Expense_Hotel.ToString("#,0.##") %></td>
                            <td class="--text-right"><%#((Booking)Container.DataItem).Expense_Ticket.ToString("#,0.##") %></td>
                            <td class="--text-right"><%#((Booking)Container.DataItem).Expense_Bus.ToString("#,0.##") %></td>
                            <td class="--text-right"><%#((Booking)Container.DataItem).Expense_Limousine.ToString("#,0.##") %></td>
                            <td class="--text-right"><%#((Booking)Container.DataItem).Expense_Bus_HanoiSapa.ToString("#,0.##") %></td>
                            <td class="--text-right"><%#((Booking)Container.DataItem).Expense_Commission.ToString("#,0.##") %></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <tr class="active">
                    <td class="--text-bold">Tổng cộng</td>
                    <td class="--text-bold --text-right"><%= ((List<Booking>)rptBooking.DataSource).Sum(b=>b.Total).ToString("#,0.##") %></td>
                    <td class="--text-bold --text-right"><%= (((List<Booking>)rptBooking.DataSource).Sum(b=>b.Expense_Guide)
                                                         +((List<Booking>)rptBooking.DataSource).Sum(b=>b.Expense_Meal)
                                                         +((List<Booking>)rptBooking.DataSource).Sum(b=>b.Expense_Hotel)
                                                         +((List<Booking>)rptBooking.DataSource).Sum(b=>b.Expense_Ticket)
                                                         +((List<Booking>)rptBooking.DataSource).Sum(b=>b.Expense_Bus)
                                                         +((List<Booking>)rptBooking.DataSource).Sum(b=>b.Expense_Limousine)
                                                         +((List<Booking>)rptBooking.DataSource).Sum(b=>b.Expense_Bus_HanoiSapa)
                                                         +((List<Booking>)rptBooking.DataSource).Sum(b=>b.Commission)).ToString("#,0.##")%></td>
                    <td class="--text-bold --text-right"><%= (((List<Booking>)rptBooking.DataSource).Sum(b=>b.Total) - (((List<Booking>)rptBooking.DataSource).Sum(b=>b.Expense_Guide)
                                                         +((List<Booking>)rptBooking.DataSource).Sum(b=>b.Expense_Meal)
                                                         +((List<Booking>)rptBooking.DataSource).Sum(b=>b.Expense_Hotel)
                                                         +((List<Booking>)rptBooking.DataSource).Sum(b=>b.Expense_Ticket)
                                                         +((List<Booking>)rptBooking.DataSource).Sum(b=>b.Expense_Bus)
                                                         +((List<Booking>)rptBooking.DataSource).Sum(b=>b.Expense_Limousine)
                                                         +((List<Booking>)rptBooking.DataSource).Sum(b=>b.Expense_Bus_HanoiSapa)
                                                         +((List<Booking>)rptBooking.DataSource).Sum(b=>b.Commission))).ToString("#,0.##")%></td>
                    <td class="--text-bold --text-right"><%= ((List<Booking>)rptBooking.DataSource).Sum(b=>b.Expense_Guide).ToString("#,0.##") %></td>
                    <td class="--text-bold --text-right"><%= ((List<Booking>)rptBooking.DataSource).Sum(b=>b.Expense_Meal).ToString("#,0.##") %></td>
                    <td class="--text-bold --text-right"><%= ((List<Booking>)rptBooking.DataSource).Sum(b=>b.Expense_Hotel).ToString("#,0.##") %></td>
                    <td class="--text-bold --text-right"><%= ((List<Booking>)rptBooking.DataSource).Sum(b=>b.Expense_Ticket).ToString("#,0.##") %></td>
                    <td class="--text-bold --text-right"><%= ((List<Booking>)rptBooking.DataSource).Sum(b=>b.Expense_Bus).ToString("#,0.##") %></td>
                    <td class="--text-bold --text-right"><%= ((List<Booking>)rptBooking.DataSource).Sum(b=>b.Expense_Limousine).ToString("#,0.##") %></td>
                    <td class="--text-bold --text-right"><%= ((List<Booking>)rptBooking.DataSource).Sum(b=>b.Expense_Bus_HanoiSapa).ToString("#,0.##") %></td>
                    <td class="--text-bold --text-right"><%= ((List<Booking>)rptBooking.DataSource).Sum(b=>b.Expense_Commission).ToString("#,0.##") %></td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
