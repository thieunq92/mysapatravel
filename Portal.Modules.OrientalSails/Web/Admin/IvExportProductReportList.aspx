<%@ Page Title="" Language="C#" MasterPageFile="MO.Master" AutoEventWireup="true" CodeBehind="IvExportProductReportList.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.IvExportProductReportList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="AdminContent" runat="server">
    <fieldset>
        <legend>BÁO CÁO SẢN PHẨM XUẤT THEO THỜI GIAN</legend>
        <div class="group">
            <div class="form-group">
                <div class="row">
                    <div class="col-xs-1">
                        Chọn tàu
                    </div>
                    <div class="col-xs-1">
                        Tàu
                    </div>
                    <div class="col-xs-2">
                        <asp:DropDownList ID="ddlCruise" CssClass="form-control" runat="server"></asp:DropDownList>
                    </div>
                    <%--<div class="col-xs-1">
                        Kho
                    </div>
                    <div class="col-xs-2">
                        <asp:DropDownList ID="ddlStorage" CssClass="form-control" runat="server"></asp:DropDownList>
                    </div>--%>
                </div>
            </div>
            <div class="form-group">
                <div class="row">

                    <div class="col-xs-1">
                        Lọc theo ngày                 
                    </div>

                    <div class="col-xs-1">
                        Từ Ngày                   
                    </div>
                    <div class="col-xs-2">
                        <asp:TextBox ID="txtFromDay" autocomplete="off" data-control="datetimepicker" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-xs-1">
                        Đến ngày
                    </div>
                    <div class="col-xs-2">
                        <asp:TextBox ID="txtToDay" autocomplete="off" data-control="datetimepicker" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="form-group">

                <div class="row">
                    <div class="col-xs-12">
                        <asp:Button ID="btnSearch" CssClass="btn btn-primary" runat="server" Text="Search" OnClick="btnSearch_Click" />
                        <asp:Button ID="btnExportExcel" Visible="False" CssClass="btn btn-primary" runat="server" Text="Export To Excel File" OnClick="btnExportExcel_Click" />

                    </div>
                </div>
            </div>
        </div>
        <div>
        </div>
        <h4></h4>
        <div class="basicinfo">
            <div class="data_grid">
                <table class="table table-bordered table-hover">
                    <asp:Repeater ID="rptProductList" runat="server" OnItemDataBound="rptProductList_ItemDataBound">
                        <HeaderTemplate>
                            <tr class="item">
                                <th style="width: 8%">Tên
                                </th>
                                <th style="width: 8%">Số lượng
                                </th>
                                <th style="width: 8%">Đơn vị
                                </th>
                                <th style="width: 8%">Ngày xuất
                                </th>
                                <th style="width: 8%">Kho
                                </th>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr class="item">
                                <td scope="row">
                                    <%#Eval("Name") %>
                                </td>
                                <td scope="row">
                                    <%#Eval("Total") %>
                                </td>
                                <td scope="row">
                                    <%#Eval("Unit") %>
                                </td>
                                <td><%# string.Format("{0:dd-MM-yyyy}",Eval("ExportDate")) %></td>
                                <td><%#Eval("StorageName") %>
                                </td>

                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </div>
        <div class="pager">
        </div>
    </fieldset>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
