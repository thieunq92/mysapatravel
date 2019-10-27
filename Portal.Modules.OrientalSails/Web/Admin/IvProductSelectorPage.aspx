<%@ Page Title="" Language="C#" MasterPageFile="NewPopup.Master" AutoEventWireup="true" CodeBehind="IvProductSelectorPage.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.IvProductSelectorPage" %>

<%@ Register Assembly="CMS.ServerControls" Namespace="CMS.ServerControls" TagPrefix="svc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="AdminContent" runat="server">
    <fieldset>
        <div class="settinglist">
            <div class="search_panel">
                <table class="">
                    <tr>
                        <td style="width: 2%">Tên sản phẩm
                        </td>
                        <td style="width: 4%">
                            <asp:TextBox ID="txtNameSearch" CssClass="form-control" runat="server"></asp:TextBox>
                        </td>
                        <td style="width: 2%">Mã sản phẩm
                        </td>
                        <td style="width: 4%">
                            <asp:TextBox ID="txtCodeSearch" CssClass="form-control" runat="server" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Nhóm
                        </td>
                        <td>
                            <asp:DropDownList ID="drpGroup" CssClass="form-control" runat="server"></asp:DropDownList>
                        </td>
                    </tr>

                </table>
            </div>
            <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search"
                CssClass="btn btn-primary" />
        </div>
        <div class="data_table">
            <div class="data_grid">
                <table class="table table-bordered table-hover">
                    <asp:Repeater ID="rptProductList" runat="server" OnItemCommand="rptProductList_ItemCommand"
                        OnItemDataBound="rptProductList_ItemDataBound">
                        <HeaderTemplate>
                            <tr class="item">
                                <th style="width: 15%">
                                    <asp:HyperLink ID="hplName" runat="server" Text="Tên sản phẩm"></asp:HyperLink>
                                </th>
                                <th style="width: 10%">
                                    <asp:HyperLink ID="hplBarCode" runat="server" Text="Mã sản phẩm"></asp:HyperLink>
                                </th>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr class="item">
                                <td scope="row">
                                    <asp:HyperLink ID="hplName" runat="server" Text="Tên sản phẩm"></asp:HyperLink>
                                </td>
                                <td scope="row">
                                    <asp:Label ID="lblCode" runat="server"></asp:Label>
                                </td>
                                
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
            <div class="pager">
                <svc:Pager ID="pagerProduct" runat="server" PageSize="20" PagerLinkMode="HyperLinkQueryString"
                    HideWhenOnePage="false" ControlToPage="rptProductList"></svc:Pager>
            </div>
        </div>
    </fieldset>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
