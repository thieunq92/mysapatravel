<%@ Page Title="" Language="C#" MasterPageFile="NewPopup.Master" AutoEventWireup="true" CodeBehind="IvProductInStockSelectPage.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.IvProductInStockSelectPage" %>

<%@ Register Assembly="CMS.ServerControls" Namespace="CMS.ServerControls" TagPrefix="svc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="AdminContent" runat="server">
    <fieldset>
        <legend>DANH SÁCH HÀNG TRONG KHO</legend>
        <div class="search_panel">
            <div class="search_panel">
                <div class="form-group">
                    <div class="row">
                        <asp:UpdatePanel ID="upPanael" runat="server">
                            <ContentTemplate>
                                <div class="col-xs-2">
                                    Tàu
                                </div>
                                <div class="col-xs-3">
                                    <asp:DropDownList ID="ddlCruise" OnSelectedIndexChanged="ddlCRuise_OnSelectedIndexChanged" AutoPostBack="True" CssClass="form-control" runat="server"></asp:DropDownList>
                                </div>
                                <div class="col-xs-2">
                                    Kho
                                </div>
                                <div class="col-xs-3">
                                    <asp:DropDownList ID="ddlStorage" CssClass="form-control" runat="server"></asp:DropDownList>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="form-group">
                    <div class="row">
                        <div class="col-xs-2">
                            Tên sản phẩm
                        </div>
                        <div class="col-xs-3">
                            <asp:TextBox ID="txtNameSearch" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-xs-2">
                            Mã sản phẩm
                        </div>
                        <div class="col-xs-3">
                            <asp:TextBox ID="txtCodeSearch" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="row">
                        <div class="col-xs-1">
                            <asp:Button ID="btnSearch" CssClass="btn btn-primary" runat="server" Text="Search" OnClick="btnSearch_Click" />
                        </div>
                        <div class="col-xs-3">
                        </div>
                    </div>
                </div>
            </div>

            <%--<tr>
                    <td style="width: 2%">Giá từ
                    </td>
                    <td style="width: 4%">
                        <asp:TextBox ID="txtPriceF" CssClass="form-control"  runat="server"></asp:TextBox>
                    </td>
                    <td style="width: 2%">Đến
                    </td>
                    <td style="width: 10%">
                        <asp:TextBox ID="txtPriceT" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 2%"></td>
                    <td style="width: 4%">
                        <svc:NumberValidator ID="numberValidator1" runat="server" ControlToValidate="txtPriceF"
                            Display="Dynamic" ErrorMessage="Chỉ đc nhập số" NumberType="Integer"></svc:NumberValidator>
                    </td>
                    <td style="width: 2%"></td>
                    <td style="width: 10%">
                        <svc:NumberValidator ID="numberValidator2" runat="server" ControlToValidate="txtPriceT"
                            Display="Dynamic" ErrorMessage="Chỉ đc nhập số" NumberType="Integer"></svc:NumberValidator>
                    </td>
                </tr>--%>
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
                                <th style="width: 8%">Đơn vị tính</th>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr class="item" id="trItem" runat="server">
                                <td scope="row">
                                    <asp:HyperLink ID="hplName" runat="server" ></asp:HyperLink>
                                </td>
                                <td scope="row">
                                    <asp:Label ID="lblCode" runat="server"></asp:Label>
                                </td>
                                <td scope="row">
                                    <asp:Label ID="lblUnitInstock" runat="server"></asp:Label>
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
        <div class="pager">
            <svc:Pager ID="pagerProduct" runat="server" PagerLinkMode="HyperLinkQueryString"
                HideWhenOnePage="false" ControlToPage="rptProductList"></svc:Pager>
        </div>
    </fieldset>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
