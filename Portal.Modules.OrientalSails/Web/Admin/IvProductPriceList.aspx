<%@ Page Title="" Language="C#" MasterPageFile="NewPopup.Master" AutoEventWireup="true" CodeBehind="IvProductPriceList.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.IvProductPriceList" %>

<%@ Register TagPrefix="svc" Namespace="CMS.ServerControls" Assembly="CMS.ServerControls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="AdminContent" runat="server">
    <fieldset>
        <legend>DANH SÁCH HÀNG TRONG KHO</legend>
        <div class="search_panel">
            <div class="search_panel">
                <div class="form-group">
                    <div class="row">
                        <div class="col-xs-2">
                            Kho
                        </div>
                        <div class="col-xs-3">
                            <asp:DropDownList ID="ddlStorage" CssClass="form-control" runat="server"></asp:DropDownList>
                        </div>
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
                        <div class="col-xs-1">
                            Mã sản phẩm
                        </div>
                        <div class="col-xs-2">
                            <asp:TextBox ID="txtCodeSearch" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="row">
                        <div class="col-xs-3">
                            <asp:Button ID="btnSearch" CssClass="btn btn-primary" runat="server" Text="Search" OnClick="btnSearch_Click" />
                            <asp:Button ID="btnSavePrice" CssClass="btn btn-success" runat="server" Text="Lưu giá bán" OnClick="btnSavePrice_OnClick" />

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
                                <th style="width: 8%">
                                    <asp:HyperLink ID="hplBarCode" runat="server" Text="Mã sản phẩm"></asp:HyperLink>
                                </th>
                                <th style="width: 70%">Giá bán theo từng đơn vị tính</th>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr class="item" id="trItem" runat="server">
                                <td scope="row">
                                    <asp:Label ID="lblName" runat="server"></asp:Label>
                                </td>
                                <td scope="row">
                                    <asp:Label ID="lblCode" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Repeater ID="rptPrice" runat="server">
                                        <HeaderTemplate>
                                            <table class="table table-bordered table-hover">
                                                <%--<tr>
                                                    <th style="width: 30%">Đơn vị tính
                                                    </th>
                                                    <th style="width: 20%">Giá bán
                                                    </th>
                                                </tr>--%>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td style="width: 5%">
                                                    <%#Eval("Unit.Name") %>
                                                    <asp:HiddenField ID="hidProductPriceId" Value='<%#Eval("Id") %>' runat="server" />
                                                    <asp:HiddenField ID="hidProductId" Value='<%#Eval("Product.Id") %>' runat="server" />
                                                    <asp:HiddenField ID="hidUnitId" Value='<%#Eval("Unit.Id") %>' runat="server" />
                                                    <asp:HiddenField ID="hidStorageId" Value='<%#Eval("Storage.Id") %>' runat="server" />
                                                </td>
                                                <td style="width: 5%">
                                                    <asp:TextBox runat="server" ID="txtPrice" CssClass="form-control" Text='<%#Eval("Price") %>' placeholder="Giá bán"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </table>
                                        </FooterTemplate>
                                    </asp:Repeater>
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
