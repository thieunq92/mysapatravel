<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ContractView.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.ContractView"
    MasterPageFile="MO-NoScriptManager.Master" Title="Contract View" %>

<asp:Content ID="AdminContent" ContentPlaceHolderID="AdminContent" runat="server">
    <h3 class="page-header">Contract View</h3>
    <div class="container">
        <h4>DELUXE ORIENTAL SAILS (18 cabins & 8 cabins)</h4>
        <div class="row">
            <div class="col-xs-12">
                <table class="table table-bordered table-hover" style="text-align: center; margin-bottom: 0">
                    <tr>
                        <td colspan="100%">
                            <strong>2 NGÀY / 1 ĐÊM</strong>
                        </td>
                    </tr>
                    <tr>
                        <td rowspan="2" style="vertical-align: middle"><strong>LOẠI PHÒNG</strong></td>
                        <asp:Repeater runat="server" ID="rptValidTimeOs2d1n">
                            <ItemTemplate>
                                <td colspan="3"><strong><%# DataBinder.Eval(Container.DataItem, "ValidFrom", "{0:dd/MM/yyyy}") %> - <%# DataBinder.Eval(Container.DataItem, "ValidTo", "{0:dd/MM/yyyy}")%></strong></td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                    <tr>
                        <asp:Repeater runat="server" ID="rptOs2d1nHeader">
                            <ItemTemplate>
                                <td>
                                    <strong>Phòng đôi</strong>
                                </td>
                                <td>
                                    <strong>Phòng đơn</strong>
                                </td>
                                <td>
                                    <strong>Trẻ em 6 - 11 tuổi</strong>
                                </td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                    <tr>
                        <td>Deluxe
                        </td>
                        <asp:Repeater runat="server" ID="rptPriceOs2d1n" OnItemDataBound="rptPriceOs2d1n_ItemDataBound">
                            <ItemTemplate>
                                <td>
                                    <asp:Literal runat="server" ID="txtOs2d1nDouble"></asp:Literal>
                                </td>
                                <td>
                                    <asp:Literal runat="server" ID="txtOs2d1nSingle"></asp:Literal>
                                </td>
                                <td>
                                    <asp:Literal runat="server" ID="txtOs2d1nChildren6to11"></asp:Literal>
                                </td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                </table>
                <table class="table table-bordered table-hover" style="text-align: center; border-top: none">
                    <tr>
                        <td style="border-top: none" colspan="100%"><strong>THUÊ TRỌN TÀU</strong>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <asp:Repeater runat="server" ID="rptValidTimeOs2d1nCharter">
                            <ItemTemplate>
                                <td colspan="4"><strong><%# DataBinder.Eval(Container.DataItem, "ValidFrom", "{0:dd/MM/yyyy}") %> - <%# DataBinder.Eval(Container.DataItem, "ValidTo", "{0:dd/MM/yyyy}")%></strong></td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                    <tr>
                        <td>Orientalsails Charter 18 cabins</td>
                        <asp:Repeater runat="server" ID="rptPriceOs12d1nCharter" OnItemDataBound="rptPriceOs12d1nCharter_ItemDataBound">
                            <ItemTemplate>
                                <td colspan="4">
                                    <asp:Literal runat="server" ID="txtOs12d1nCharter"></asp:Literal>
                                </td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                    <tr>
                        <td rowspan="2">Orientalsails Charter 8 cabins</td>
                        <asp:Repeater runat="server" ID="rptOs22d1nCharterHeader">
                            <ItemTemplate>
                                <td><strong>1-4 khách </strong></td>
                                <td><strong>5-8 khách </strong></td>
                                <td><strong>9-12 khách </strong></td>
                                <td><strong>13-16 khách </strong></td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                    <tr>
                        <asp:Repeater runat="server" ID="rptPriceOs22d1nCharter" OnItemDataBound="rptPriceOs22d1nCharter_ItemDataBound">
                            <ItemTemplate>
                                <td>
                                    <asp:Literal runat="server" ID="txtOs22d1nCharter1to4passenger"></asp:Literal>
                                </td>
                                <td>
                                    <asp:Literal runat="server" ID="txtOs22d1nCharter5to8passenger"></asp:Literal>
                                </td>
                                <td>
                                    <asp:Literal runat="server" ID="txtOs22d1nCharter9to12passenger"></asp:Literal>
                                </td>
                                <td>
                                    <asp:Literal runat="server" ID="txtOs22d1nCharter13to16passenger"></asp:Literal>
                                </td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                </table>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-12">
                <table class="table table-bordered table-hover" style="text-align: center; margin-bottom: 0">
                    <tr>
                        <td colspan="100%">
                            <strong>3 NGÀY / 2 ĐÊM</strong>
                        </td>
                    </tr>
                    <tr>
                        <td rowspan="2" style="vertical-align: middle"><strong>LOẠI PHÒNG</strong></td>
                        <asp:Repeater runat="server" ID="rptValidTimeOs3d2n">
                            <ItemTemplate>
                                <td colspan="3"><strong><%# DataBinder.Eval(Container.DataItem, "ValidFrom", "{0:dd/MM/yyyy}") %> - <%# DataBinder.Eval(Container.DataItem, "ValidTo", "{0:dd/MM/yyyy}")%></strong></td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                    <tr>
                        <asp:Repeater runat="server" ID="rptOs3d2nHeader">
                            <ItemTemplate>
                                <td>
                                    <strong>Phòng đôi</strong>
                                </td>
                                <td>
                                    <strong>Phòng đơn</strong>
                                </td>
                                <td>
                                    <strong>Trẻ em 6 - 11 tuổi</strong>
                                </td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                    <tr>
                        <td>Deluxe
                        </td>
                        <asp:Repeater runat="server" ID="rptPriceOs3d2n" OnItemDataBound="rptPriceOs3d2n_ItemDataBound">
                            <ItemTemplate>
                                <td>
                                    <asp:Literal runat="server" ID="txtOs3d2nDouble"></asp:Literal>
                                </td>
                                <td>
                                    <asp:Literal runat="server" ID="txtOs3d2nSingle"></asp:Literal>
                                </td>
                                <td>
                                    <asp:Literal runat="server" ID="txtOs3d2nChildren6to11"></asp:Literal>
                                </td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                </table>
                <table class="table table-bordered table-hover" style="text-align: center; border-top: none">
                    <tr>
                        <td style="border-top: none" colspan="100%"><strong>THUÊ TRỌN TÀU</strong>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <asp:Repeater runat="server" ID="rptValidTimeOs3d2nCharter">
                            <ItemTemplate>
                                <td colspan="4"><strong><%# DataBinder.Eval(Container.DataItem, "ValidFrom", "{0:dd/MM/yyyy}") %> - <%# DataBinder.Eval(Container.DataItem, "ValidTo", "{0:dd/MM/yyyy}")%></strong></td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                    <tr>
                        <td>Orientalsails Charter 18 cabins</td>
                        <asp:Repeater runat="server" ID="rptPriceOs13d2nCharter" OnItemDataBound="rptPriceOs13d2nCharter_ItemDataBound">
                            <ItemTemplate>
                                <td colspan="4">
                                    <asp:Literal runat="server" ID="txtOs13d2nCharter"></asp:Literal>
                                </td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                    <tr>
                        <td rowspan="2">Orientalsails Charter 8 cabins</td>
                        <asp:Repeater runat="server" ID="rptOs23d2nCharterHeader">
                            <ItemTemplate>
                                <td><strong>1-4 khách </strong></td>
                                <td><strong>5-8 khách </strong></td>
                                <td><strong>9-12 khách </strong></td>
                                <td><strong>13-16 khách </strong></td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                    <tr>
                        <asp:Repeater runat="server" ID="rptPriceOs23d2nCharter" OnItemDataBound="rptPriceOs23d2nCharter_ItemDataBound">
                            <ItemTemplate>
                                <td>
                                    <asp:Literal runat="server" ID="txtOs23d2nCharter1to4passenger"></asp:Literal>
                                </td>
                                <td>
                                    <asp:Literal runat="server" ID="txtOs23d2nCharter5to8passenger"></asp:Literal>
                                </td>
                                <td>
                                    <asp:Literal runat="server" ID="txtOs23d2nCharter9to12passenger"></asp:Literal>
                                </td>
                                <td>
                                    <asp:Literal runat="server" ID="txtOs23d2nCharter13to16passenger"></asp:Literal>
                                </td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                </table>
            </div>
        </div>
        <h4>DELUXE CALYPSO CRUISER (12 cabins)</h4>
        <div class="row">
            <div class="col-xs-12">
                <table class="table table-bordered table-hover" style="text-align: center; margin-bottom: 0">
                    <tr>
                        <td colspan="100%"><strong>2 NGÀY / 1 ĐÊM</strong></td>
                    </tr>
                    <tr>
                        <td rowspan="2" style="vertical-align: middle"><strong>LOẠI PHÒNG</strong></td>
                        <asp:Repeater runat="server" ID="rptValidTimeCls2d1n">
                            <ItemTemplate>
                                <td colspan="3"><strong><%# DataBinder.Eval(Container.DataItem, "ValidFrom", "{0:dd/MM/yyyy}") %> - <%# DataBinder.Eval(Container.DataItem, "ValidTo", "{0:dd/MM/yyyy}") %></strong></td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                    <tr>
                        <asp:Repeater runat="server" ID="rptCls2d1nHeader">
                            <ItemTemplate>
                                <td>
                                    <strong>Phòng đôi</strong>
                                </td>
                                <td>
                                    <strong>Phòng đơn</strong>
                                </td>
                                <td>
                                    <strong>Trẻ em 6 - 11 tuổi</strong>
                                </td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                    <tr>
                        <td>Deluxe
                        </td>
                        <asp:Repeater runat="server" ID="rptPriceCls2d1n" OnItemDataBound="rptPriceCls2d1n_ItemDataBound">
                            <ItemTemplate>
                                <td>
                                    <asp:Literal runat="server" ID="txtCls2d1nDouble"></asp:Literal>
                                </td>
                                <td>
                                    <asp:Literal runat="server" ID="txtCls2d1nSingle"></asp:Literal>
                                </td>
                                <td>
                                    <asp:Literal runat="server" ID="txtCls2d1nChildren6to11"></asp:Literal>
                                </td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                </table>
                <table class="table table-bordered table-hover" style="text-align: center; border-top: none">
                    <tr>
                        <td colspan="100%" style="border-top: none"><strong>THUÊ TRỌN TÀU</strong>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <asp:Repeater runat="server" ID="rptValidTimeCls2d1nCharter">
                            <ItemTemplate>
                                <td><strong><%# DataBinder.Eval(Container.DataItem, "ValidFrom", "{0:dd/MM/yyyy}") %> - <%# DataBinder.Eval(Container.DataItem, "ValidTo", "{0:dd/MM/yyyy}") %></strong></td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                    <tr>
                        <td>Calypso Cruise Charter</td>
                        <asp:Repeater runat="server" ID="rptPriceCls2d1nCharter" OnItemDataBound="rptPriceCls2d1nCharter_ItemDataBound">
                            <ItemTemplate>
                                <td>
                                    <asp:Literal runat="server" ID="txtCls2d1nCharter"></asp:Literal>
                                </td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                </table>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-12">
                <table class="table table-bordered table-hover" style="text-align: center; margin-bottom: 0">
                    <tr>
                        <td colspan="100%"><strong>3 NGÀY / 2 ĐÊM</strong></td>
                    </tr>
                    <tr>
                        <td rowspan="2" style="vertical-align: middle"><strong>LOẠI PHÒNG</strong></td>
                        <asp:Repeater runat="server" ID="rptValidTimeCls3d2n">
                            <ItemTemplate>
                                <td colspan="3"><strong><%# DataBinder.Eval(Container.DataItem, "ValidFrom", "{0:dd/MM/yyyy}") %> - <%# DataBinder.Eval(Container.DataItem, "ValidTo", "{0:dd/MM/yyyy}") %></strong></td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                    <tr>
                        <asp:Repeater runat="server" ID="rptCls3d2nHeader">
                            <ItemTemplate>
                                <td>
                                    <strong>Phòng đôi</strong>
                                </td>
                                <td>
                                    <strong>Phòng đơn</strong>
                                </td>
                                <td>
                                    <strong>Trẻ em 6 - 11 tuổi</strong>
                                </td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                    <tr>
                        <td>Deluxe
                        </td>
                        <asp:Repeater runat="server" ID="rptPriceCls3d2n" OnItemDataBound="rptPriceCls3d2n_ItemDataBound">
                            <ItemTemplate>
                                <td>
                                    <asp:Literal runat="server" ID="txtCls3d2nDouble"></asp:Literal>
                                </td>
                                <td>
                                    <asp:Literal runat="server" ID="txtCls3d2nSingle"></asp:Literal>
                                </td>
                                <td>
                                    <asp:Literal runat="server" ID="txtCls3d2nChildren6to11"></asp:Literal>
                                </td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                </table>
                <table class="table table-bordered table-hover" style="text-align: center; border-top: none">
                    <tr>
                        <td colspan="100%" style="border-top: none"><strong>THUÊ TRỌN TÀU</strong>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <asp:Repeater runat="server" ID="rptValidTimeCls3d2nCharter">
                            <ItemTemplate>
                                <td><strong><%# DataBinder.Eval(Container.DataItem, "ValidFrom", "{0:dd/MM/yyyy}") %> - <%# DataBinder.Eval(Container.DataItem, "ValidTo", "{0:dd/MM/yyyy}") %></strong></td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                    <tr>
                        <td>Calypso Cruise Charter</td>
                        <asp:Repeater runat="server" ID="rptPriceCls3d2nCharter" OnItemDataBound="rptPriceCls3d2nCharter_ItemDataBound">
                            <ItemTemplate>
                                <td>
                                    <asp:Literal runat="server" ID="txtCls3d2nCharter"></asp:Literal>
                                </td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                </table>
            </div>
        </div>
        <h4>LUXURY STARLIGHT CRUISE (32 Cabins)</h4>
        <div class="row">
            <div class="col-xs-12">
                <table class="table table-bordered table-hover" style="text-align: center; margin-bottom: 0">
                    <tr>
                        <td colspan="100%"><strong>2 NGÀY / 1 ĐÊM</strong></td>
                    </tr>
                    <tr>
                        <td rowspan="2" style="vertical-align: middle"><strong>LOẠI PHÒNG</strong></td>
                        <asp:Repeater runat="server" ID="rptValidTimeStl2d1n">
                            <ItemTemplate>
                                <td colspan="3"><strong><%# DataBinder.Eval(Container.DataItem, "ValidFrom", "{0:dd/MM/yyyy}") %> - <%# DataBinder.Eval(Container.DataItem, "ValidTo", "{0:dd/MM/yyyy}") %></strong></td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                    <tr>
                        <asp:Repeater runat="server" ID="rptStl2d1nHeader">
                            <ItemTemplate>
                                <td>
                                    <strong>Phòng đôi</strong>
                                </td>
                                <td>
                                    <strong>Phòng đơn</strong>
                                </td>
                                <td>
                                    <strong>Giường phụ/đệm</strong>
                                </td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                    <tr>
                        <td>Deluxe
                        </td>
                        <asp:Repeater runat="server" ID="rptPriceStl2d1nDeluxe" OnItemDataBound="rptPriceStl2d1nDeluxe_ItemDataBound">
                            <ItemTemplate>
                                <td>
                                    <asp:Literal runat="server" ID="txtStl2d1nDeluxeDouble"></asp:Literal>
                                </td>
                                <td>
                                    <asp:Literal runat="server" ID="txtStl2d1nDeluxeSingle"></asp:Literal>
                                </td>
                                <td>
                                    <asp:Literal runat="server" ID="txtStl2d1nDeluxeExtrabed"></asp:Literal>
                                </td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                    <tr>
                        <td>Executive
                        </td>
                        <asp:Repeater runat="server" ID="rptPriceStl2d1nExecutive" OnItemDataBound="rptPriceStl2d1nExecutive_ItemDataBound">
                            <ItemTemplate>
                                <td>
                                    <asp:Literal runat="server" ID="txtStl2d1nExecutiveDouble"></asp:Literal>
                                </td>
                                <td>
                                    <asp:Literal runat="server" ID="txtStl2d1nExecutiveSingle"></asp:Literal>
                                </td>
                                <td>
                                    <asp:Literal runat="server" ID="txtStl2d1nExecutiveExtrabed"></asp:Literal>
                                </td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                    <tr>
                        <td>Suite
                        </td>
                        <asp:Repeater runat="server" ID="rptPriceStl2d1nSuite" OnItemDataBound="rptPriceStl2d1nSuite_ItemDataBound">
                            <ItemTemplate>
                                <td>
                                    <asp:Literal runat="server" ID="txtStl2d1nSuiteDouble"></asp:Literal>
                                </td>
                                <td>
                                    <asp:Literal runat="server" ID="txtStl2d1nSuiteSingle"></asp:Literal>
                                </td>
                                <td>
                                    <asp:Literal runat="server" ID="txtStl2d1nSuiteExtrabed"></asp:Literal>
                                </td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                </table>
                <table class="table table-bordered table-hover" style="text-align: center; border-top: none">
                    <tr>
                        <td colspan="100%" style="border-top: none"><strong>THUÊ TRỌN TÀU</strong>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td><strong>1-30 khách</strong></td>
                        <td><strong>31-40 khách</strong></td>
                        <td><strong>41-50 khách</strong></td>
                        <td><strong>51-64 khách</strong></td>
                    </tr>
                    <asp:Repeater runat="server" ID="rptPriceStl2d1nCharter" OnItemDataBound="rptPriceStl2d1nCharter_ItemDataBound">
                        <ItemTemplate>
                            <tr>
                                <td><strong><%# DataBinder.Eval(Container.DataItem, "ValidFrom", "{0:dd/MM/yyyy}") %> - <%# DataBinder.Eval(Container.DataItem, "ValidTo", "{0:dd/MM/yyyy}") %></strong></td>
                                <td>
                                    <asp:Literal runat="server" ID="txtStl2d1nCharter1to30passenger"></asp:Literal>
                                </td>
                                <td>
                                    <asp:Literal runat="server" ID="txtStl2d1nCharter31to40passenger"></asp:Literal>
                                </td>
                                <td>
                                    <asp:Literal runat="server" ID="txtStl2d1nCharter41to50passenger"></asp:Literal>
                                </td>
                                <td>
                                    <asp:Literal runat="server" ID="txtStl2d1nCharter51to64passenger"></asp:Literal>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-12">
                <table class="table table-bordered table-hover" style="text-align: center; margin-bottom: 0">
                    <tr>
                        <td colspan="100%"><strong>3 NGÀY / 2 ĐÊM</strong></td>
                    </tr>
                    <tr>
                        <td rowspan="2" style="vertical-align: middle"><strong>LOẠI PHÒNG</strong></td>
                        <asp:Repeater runat="server" ID="rptValidTimeStl3d2n">
                            <ItemTemplate>
                                <td colspan="3"><strong><%# DataBinder.Eval(Container.DataItem, "ValidFrom", "{0:dd/MM/yyyy}") %> - <%# DataBinder.Eval(Container.DataItem, "ValidTo", "{0:dd/MM/yyyy}") %></strong></td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                    <tr>
                        <asp:Repeater runat="server" ID="rptStl3d2nHeader">
                            <ItemTemplate>
                                <td>
                                    <strong>Phòng đôi</strong>
                                </td>
                                <td>
                                    <strong>Phòng đơn</strong>
                                </td>
                                <td>
                                    <strong>Giường phụ/đệm</strong>
                                </td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                    <tr>
                        <td>Deluxe
                        </td>
                        <asp:Repeater runat="server" ID="rptPriceStl3d2nDeluxe" OnItemDataBound="rptPriceStl3d2nDeluxe_ItemDataBound">
                            <ItemTemplate>
                                <td>
                                    <asp:Literal runat="server" ID="txtStl3d2nDeluxeDouble"></asp:Literal>
                                </td>
                                <td>
                                    <asp:Literal runat="server" ID="txtStl3d2nDeluxeSingle"></asp:Literal>
                                </td>
                                <td>
                                    <asp:Literal runat="server" ID="txtStl3d2nDeluxeExtrabed"></asp:Literal>
                                </td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                    <tr>
                        <td>Executive
                        </td>
                        <asp:Repeater runat="server" ID="rptPriceStl3d2nExecutive" OnItemDataBound="rptPriceStl3d2nExecutive_ItemDataBound">
                            <ItemTemplate>
                                <td>
                                    <asp:Literal runat="server" ID="txtStl3d2nExecutiveDouble"></asp:Literal>
                                </td>
                                <td>
                                    <asp:Literal runat="server" ID="txtStl3d2nExecutiveSingle"></asp:Literal>
                                </td>
                                <td>
                                    <asp:Literal runat="server" ID="txtStl3d2nExecutiveExtrabed"></asp:Literal>
                                </td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                    <tr>
                        <td>Suite
                        </td>
                        <asp:Repeater runat="server" ID="rptPriceStl3d2nSuite" OnItemDataBound="rptPriceStl3d2nSuite_ItemDataBound">
                            <ItemTemplate>
                                <td>
                                    <asp:Literal runat="server" ID="txtStl3d2nSuiteDouble"></asp:Literal>
                                </td>
                                <td>
                                    <asp:Literal runat="server" ID="txtStl3d2nSuiteSingle"></asp:Literal>
                                </td>
                                <td>
                                    <asp:Literal runat="server" ID="txtStl3d2nSuiteExtrabed"></asp:Literal>
                                </td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                </table>
                <table class="table table-bordered table-hover" style="text-align: center; border-top: none">
                    <tr>
                        <td colspan="100%" style="border-top: none"><strong>THUÊ TRỌN TÀU</strong>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td><strong>1-30 khách</strong></td>
                        <td><strong>31-40 khách</strong></td>
                        <td><strong>41-50 khách</strong></td>
                        <td><strong>51-64 khách</strong></td>
                    </tr>
                    <asp:Repeater runat="server" ID="rptPriceStl3d2nCharter" OnItemDataBound="rptPriceStl3d2nCharter_ItemDataBound">
                        <ItemTemplate>
                            <tr>
                                <td><strong><%# DataBinder.Eval(Container.DataItem, "ValidFrom", "{0:dd/MM/yyyy}") %> - <%# DataBinder.Eval(Container.DataItem, "ValidTo", "{0:dd/MM/yyyy}") %></strong></td>
                                <td>
                                    <asp:Literal runat="server" ID="txtStl3d2nCharter1to30passenger"></asp:Literal>
                                </td>
                                <td>
                                    <asp:Literal runat="server" ID="txtStl3d2nCharter31to40passenger"></asp:Literal>
                                </td>
                                <td>
                                    <asp:Literal runat="server" ID="txtStl3d2nCharter41to50passenger"></asp:Literal>
                                </td>
                                 <td>
                                    <asp:Literal runat="server" ID="txtStl3d2nCharter51to64passenger"></asp:Literal>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
