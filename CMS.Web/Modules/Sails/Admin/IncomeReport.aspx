<%@ Page Language="C#" MasterPageFile="MO.Master" AutoEventWireup="true"
    CodeBehind="IncomeReport.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.IncomeReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
    <div class="form-group">
        <div class="row">
            <div class="col-xs-1">
                From
            </div>
            <div class="col-xs-2">
                <asp:TextBox ID="txtFrom" runat="server" CssClass="form-control" placeholder="From (dd/MM/yyyy)"></asp:TextBox>
            </div>
            <div class="col-xs-1">
                To
            </div>
            <div class="col-xs-2">
                <asp:TextBox ID="txtTo" runat="server" CssClass="form-control" placeholder="To (dd/MM/yyyy)"></asp:TextBox>
            </div>
        </div>
    </div>
    <div class="form-group">
        <asp:Button ID="btnDisplay" runat="server" Text="Display" OnClick="btnDisplay_Click"
            CssClass="btn btn-primary" />
    </div>
    <div class="form-group">
        <asp:Repeater ID="rptCruises" runat="server" OnItemDataBound="rptCruises_ItemDataBound">
            <HeaderTemplate>
                <asp:HyperLink ID="hplCruises" runat="server" Text="All" CssClass="btn btn-default"></asp:HyperLink>
            </HeaderTemplate>
            <ItemTemplate>
                <asp:HyperLink ID="hplCruises" runat="server" CssClass="btn btn-default"></asp:HyperLink>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <fieldset>
        <div>
            <div class="data_table">
                <div class="data_grid">
                    <table class="table table-bordered table-common table-striped">
                        <asp:Repeater ID="rptBookingList" runat="server" OnItemDataBound="rptBookingList_ItemDataBound"
                            OnItemCommand="rptBookingList_ItemCommand">
                            <HeaderTemplate>
                                <tr class="header">
                                    <th>
                                        Date
                                    </th>
                                    <th>
                                        No of pax
                                    </th>
                                    <asp:Repeater ID="rptTrip" runat="server">
                                        <ItemTemplate>
                                            <th>
                                                <%# Eval("TripCode") %>
                                            </th>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <th>Total
                                    </th>
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr class="item">
                                    <td>
                                        <asp:HyperLink ID="hplDate" runat="server"></asp:HyperLink>
                                    </td>
                                    <td>
                                        <asp:Literal ID="litTotalPax" runat="server"></asp:Literal>
                                    </td>
                                    <asp:Repeater ID="rptTrip" runat="server" OnItemDataBound="rptItemTrip_ItemDataBound">
                                        <ItemTemplate>
                                            <td>
                                                <asp:Literal ID="litTotal" runat="server"></asp:Literal></td>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <td>
                                        <asp:Literal ID="litTotal" runat="server"></asp:Literal></td>
                                    <!--<td>
                                        <asp:Literal ID="litBar" runat="server"></asp:Literal></td>-->
                                </tr>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <tr class="item" style="background-color: #E9E9E9">
                                    <td>
                                        <asp:HyperLink ID="hplDate" runat="server"></asp:HyperLink>
                                    </td>
                                    <td>
                                        <asp:Literal ID="litTotalPax" runat="server"></asp:Literal>
                                    </td>
                                    <asp:Repeater ID="rptTrip" runat="server" OnItemDataBound="rptItemTrip_ItemDataBound">
                                        <ItemTemplate>
                                            <td>
                                                <asp:Literal ID="litTotal" runat="server"></asp:Literal></td>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <td>
                                        <asp:Literal ID="litTotal" runat="server"></asp:Literal></td>
                                    <!--<td>
                                        <asp:Literal ID="litBar" runat="server"></asp:Literal></td>-->
                                </tr>
                            </AlternatingItemTemplate>
                            <FooterTemplate>
                                <tr>
                                    <th>GRAND TOTAL</th>
                                    <th>
                                        <asp:Literal ID="litTotalPax" runat="server"></asp:Literal>
                                    </th>
                                    <asp:Repeater ID="rptTrip" runat="server" OnItemDataBound="rptFooterTrip_ItemDataBound">
                                        <ItemTemplate>
                                            <th>
                                                <asp:Literal ID="litTotal" runat="server"></asp:Literal></th>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <th>
                                        <asp:Literal ID="litTotal" runat="server"></asp:Literal></th>
                                    <!--<th>
                                        <asp:Literal ID="litBar" runat="server"></asp:Literal></th>-->
                                </tr>
                            </FooterTemplate>
                        </asp:Repeater>
                    </table>
                </div>
            </div>
        </div>
    </fieldset>
</asp:Content>
