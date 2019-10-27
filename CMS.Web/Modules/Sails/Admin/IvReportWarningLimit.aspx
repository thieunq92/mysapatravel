<%@ Page Title="" Language="C#" MasterPageFile="MO.Master" AutoEventWireup="true" CodeBehind="IvReportWarningLimit.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.IvReportWarningLimit" %>

<%@ Register Assembly="CMS.ServerControls" Namespace="CMS.ServerControls" TagPrefix="svc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="AdminContent" runat="server">
    <fieldset>
        <legend>DANH SÁCH HÀNG SẮP HẾT</legend>
        <div class="search_panel">
            <div class="form-group">
                <div class="row">
                    <div class="col-xs-1">
                        Kho
                    </div>
                    <div class="col-xs-3">
                        <asp:DropDownList ID="ddlStorage" CssClass="form-control" runat="server"></asp:DropDownList>

                    </div>
                    <div class="col-xs-1">
                        <asp:Button ID="btnSearch" CssClass="btn btn-primary" runat="server" Text="Search" OnClick="btnSearch_Click" />
                    </div>
                    <div class="col-xs-3">
                    </div>
                </div>
            </div>
        </div>
        <div class="basicinfo">
            <div class="data_grid">
                <table class="table table-bordered table-hover">
                    <asp:Repeater ID="rptProductList" runat="server"
                        OnItemDataBound="rptProductList_ItemDataBound">
                        <HeaderTemplate>
                            <tr class="item">
                                <th style="width: 15%">
                                    <asp:HyperLink ID="hplName" runat="server" Text="Tên sản phẩm"></asp:HyperLink>
                                </th>
                                <th style="width: 10%">
                                    <asp:HyperLink ID="hplBarCode" runat="server" Text="Mã sản phẩm"></asp:HyperLink>
                                </th>
                                <th style="width: 8%">
                                    <asp:Label ID="lblProductInStock" runat="server" Text="Số luợng tồn kho"></asp:Label>
                                </th>
                                <th style="width: 5%">Số lượng tối thiểu
                                </th>
                                <th style="width: 8%">Đơn vị tính</th>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr class="item" id="trItem" runat="server">
                                <td scope="row">
                                    <%--                                    <asp:HyperLink ID="hplName" runat="server"></asp:HyperLink>--%>
                                    <asp:Label ID="lblName" runat="server"></asp:Label>

                                </td>
                                <td scope="row">
                                    <asp:Label ID="lblCode" runat="server"></asp:Label>
                                </td>
                                <td scope="row">
                                    <asp:Label ID="lblUnitInstock" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblWarningLimit" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblUnit" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </div>
    </fieldset>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
