<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuotationView.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.QuotationView"
    Title="Quotation View" MasterPageFile="MO-NoScriptManager.Master" %>

<asp:Content ID="AdminContent" ContentPlaceHolderID="AdminContent" runat="server">
    <div class="container">
        <h3 class="page-header">Quotation View</h3>
        <div class="quotation-panel">
            <h4>DELUXE ORIENTAL SAILS (18 cabins & 8 cabins)</h4>
            <div class="row">
                <div class="col-xs-12">
                    <table class="table table-bordered table-hover" style="text-align: center; margin-bottom: 0">
                        <tr>
                            <td colspan="4"><strong>2 NGÀY / 1 ĐÊM</strong>
                            </td>
                        </tr>
                        <tr>
                            <td rowspan="2" style="vertical-align: middle"><strong>LOẠI PHÒNG</strong></td>
                            <td colspan="3">
                                <strong><%= GetValidFrom() %> - <%= GetValidTo() %></strong>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <strong>Phòng đôi</strong>
                            </td>
                            <td>
                                <strong>Phòng đơn</strong>
                            </td>
                            <td>
                                <strong>Trẻ em 6 - 11 tuổi</strong>
                            </td>
                        </tr>
                        <tr>
                            <td>Deluxe
                            </td>
                            <td>
                                <asp:Literal runat="server" ID="txtOs2d1nDouble"></asp:Literal>
                            </td>
                            <td>
                                <asp:Literal runat="server" ID="txtOs2d1nSingle"></asp:Literal>
                            </td>
                            <td>
                                <asp:Literal runat="server" ID="txtOs2d1nChildren6to11"></asp:Literal>
                            </td>
                        </tr>
                    </table>
                    <table class="table table-bordered table-hover" style="text-align: center; border-top: none">
                        <tr>
                            <td colspan="5" style="border-top: none"><strong>THUÊ TRỌN TÀU</strong>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td colspan="4"><strong><%= GetValidFrom() %> - <%= GetValidTo() %></strong>
                            </td>
                        </tr>
                        <tr>
                            <td>Orientalsails Charter 18 cabins</td>
                            <td colspan="4">
                                <asp:Literal runat="server" ID="txtOs12d1nCharter"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td rowspan="2">Orientalsails Charter 8 cabins</td>
                            <td><strong>1-4 khách </strong></td>
                            <td><strong>5-8 khách </strong></td>
                            <td><strong>9-12 khách </strong></td>
                            <td><strong>13-17 khách </strong></td>
                        </tr>
                        <tr>
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
                                <asp:Literal runat="server" ID="txtOs22d1nCharter13to17passenger"></asp:Literal>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-12">
                    <table class="table table-bordered table-hover" style="text-align: center; margin-bottom: 0">
                        <tr>
                            <td colspan="4"><strong>3 NGÀY / 2 ĐÊM</strong>
                            </td>
                        </tr>
                        <tr>
                            <td rowspan="2" style="vertical-align: middle"><strong>LOẠI PHÒNG</strong></td>
                            <td colspan="3">
                                <strong><%= GetValidFrom() %> - <%= GetValidTo() %></strong>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <strong>Phòng đôi</strong>
                            </td>
                            <td>
                                <strong>Phòng đơn</strong>
                            </td>
                            <td>
                                <strong>Trẻ em 6 - 11 tuổi</strong>
                            </td>
                        </tr>
                        <tr>
                            <td>Deluxe
                            </td>
                            <td>
                                <asp:Literal runat="server" ID="txtOs3d2nDouble"></asp:Literal>
                            </td>
                            <td>
                                <asp:Literal runat="server" ID="txtOs3d2nSingle"></asp:Literal>
                            </td>
                            <td>
                                <asp:Literal runat="server" ID="txtOs3d2nChildren6to11"></asp:Literal>
                            </td>
                        </tr>
                    </table>
                    <table class="table table-bordered table-hover" style="text-align: center; border-top: none">
                        <tr>
                            <td colspan="5" style="border-top: none"><strong>THUÊ TRỌN TÀU</strong>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td colspan="4"><strong><%= GetValidFrom() %> - <%= GetValidTo() %></strong>
                            </td>
                        </tr>
                        <tr>
                            <td>Orientalsails Charter 18 cabins</td>
                            <td colspan="4">
                                <asp:Literal runat="server" ID="txtOs13d2nCharter"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td rowspan="2">Orientalsails Charter 8 cabins</td>
                            <td><strong>1-4 khách </strong></td>
                            <td><strong>5-8 khách </strong></td>
                            <td><strong>9-12 khách </strong></td>
                            <td><strong>13-17 khách </strong></td>
                        </tr>
                        <tr>
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
                                <asp:Literal runat="server" ID="txtOs23d2nCharter13to17passenger"></asp:Literal>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <h4>DELUXE CALYPSO CRUISER (12 cabins)</h4>
            <div class="row">
                <div class="col-xs-12">
                    <table class="table table-bordered table-hover" style="text-align: center; margin-bottom: 0">
                        <tr>
                            <td colspan="4"><strong>2 NGÀY / 1 ĐÊM</strong>
                            </td>
                        </tr>
                        <tr>
                            <td rowspan="2" style="vertical-align: middle"><strong>LOẠI PHÒNG</strong></td>
                            <td colspan="3">
                                <strong><%= GetValidFrom() %> - <%= GetValidTo() %></strong>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <strong>Phòng đôi</strong>
                            </td>
                            <td>
                                <strong>Phòng đơn</strong>
                            </td>
                            <td>
                                <strong>Trẻ em 6 - 11 tuổi</strong>
                            </td>
                        </tr>
                        <tr>
                            <td>Deluxe
                            </td>
                            <td>
                                <asp:Literal runat="server" ID="txtCls2d1nDouble"></asp:Literal>
                            </td>
                            <td>
                                <asp:Literal runat="server" ID="txtCls2d1nSingle"></asp:Literal>
                            </td>
                            <td>
                                <asp:Literal runat="server" ID="txtCls2d1nChildren6to11"></asp:Literal>
                            </td>
                        </tr>
                    </table>
                    <table class="table table-bordered table-hover" style="text-align: center; border-top: none">
                        <tr>
                            <td colspan="5" style="border-top: none"><strong>THUÊ TRỌN TÀU</strong>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td colspan="4"><strong><%= GetValidFrom() %> - <%= GetValidTo() %></strong>
                            </td>
                        </tr>
                        <tr>
                            <td>Calypso Cruise Charter</td>
                            <td>
                                <asp:Literal runat="server" ID="txtCls2d1nCharter"></asp:Literal>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-12">
                    <table class="table table-bordered table-hover" style="text-align: center; margin-bottom: 0">
                        <tr>
                            <td colspan="4"><strong>3 NGÀY / 2 ĐÊM</strong>
                            </td>
                        </tr>
                        <tr>
                            <td rowspan="2" style="vertical-align: middle"><strong>LOẠI PHÒNG</strong></td>
                            <td colspan="3">
                                <strong><%= GetValidFrom() %> - <%= GetValidTo() %></strong>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <strong>Phòng đôi</strong>
                            </td>
                            <td>
                                <strong>Phòng đơn</strong>
                            </td>
                            <td>
                                <strong>Trẻ em 6 - 11 tuổi</strong>
                            </td>
                        </tr>
                        <tr>
                            <td>Deluxe
                            </td>
                            <td>
                                <asp:Literal runat="server" ID="txtCls3d2nDouble"></asp:Literal>
                            </td>
                            <td>
                                <asp:Literal runat="server" ID="txtCls3d2nSingle"></asp:Literal>
                            </td>
                            <td>
                                <asp:Literal runat="server" ID="txtCls3d2nChildren6to11"></asp:Literal>
                            </td>
                        </tr>
                    </table>
                    <table class="table table-bordered table-hover" style="text-align: center; border-top: none">
                        <tr>
                            <td colspan="5" style="border-top: none"><strong>THUÊ TRỌN TÀU</strong>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td colspan="4"><strong><%= GetValidFrom() %> - <%= GetValidTo() %></strong>
                            </td>
                        </tr>
                        <tr>
                            <td>Calypso Cruise Charter</td>
                            <td>
                                <asp:Literal runat="server" ID="txtCls3d2nCharter"></asp:Literal>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <h4>LUXURY STARLIGHT CRUISE (32 Cabins)</h4>
            <div class="row">
                <div class="col-xs-12">
                    <table class="table table-bordered table-hover" style="text-align: center; margin-bottom: 0">
                        <tr>
                            <td colspan="4"><strong>2 NGÀY / 1 ĐÊM</strong>
                            </td>
                        </tr>
                        <tr>
                            <td rowspan="2" style="vertical-align: middle"><strong>LOẠI PHÒNG</strong></td>
                            <td colspan="3">
                                <strong><%= GetValidFrom() %> - <%= GetValidTo() %></strong>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <strong>Phòng đôi</strong>
                            </td>
                            <td>
                                <strong>Phòng đơn</strong>
                            </td>
                            <td>
                                <strong>Giường phụ/đệm</strong>
                            </td>
                        </tr>
                        <tr>
                            <td>Deluxe
                            </td>
                            <td>
                                <asp:Literal runat="server" ID="txtStl2d1nDeluxeDouble"></asp:Literal>
                            </td>
                            <td>
                                <asp:Literal runat="server" ID="txtStl2d1nDeluxeSingle"></asp:Literal>
                            </td>
                            <td>
                                <asp:Literal runat="server" ID="txtStl2d1nDeluxeExtrabed"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td>Executive
                            </td>
                            <td>
                                <asp:Literal runat="server" ID="txtStl2d1nExecutiveDouble"></asp:Literal>
                            </td>
                            <td>
                                <asp:Literal runat="server" ID="txtStl2d1nExecutiveSingle"></asp:Literal>
                            </td>
                            <td>
                                <asp:Literal runat="server" ID="txtStl2d1nExecutiveExtrabed"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td>Suite
                            </td>
                            <td>
                                <asp:Literal runat="server" ID="txtStl2d1nSuiteDouble"></asp:Literal>
                            </td>
                            <td>
                                <asp:Literal runat="server" ID="txtStl2d1nSuiteSingle"></asp:Literal>
                            </td>
                            <td>
                                <asp:Literal runat="server" ID="txtStl2d1nSuiteExtrabed"></asp:Literal>
                            </td>
                        </tr>
                    </table>
                    <table class="table table-bordered table-hover" style="text-align: center; border-top: none">
                        <tr>
                            <td colspan="5" style="border-top: none"><strong>THUÊ TRỌN TÀU</strong>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td colspan="4"><strong><%= GetValidFrom() %> - <%= GetValidTo() %></strong>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td><strong>1-40 khách</strong></td>
                            <td><strong>41-50 khách</strong></td>
                            <td><strong>51-63 khách</strong></td>
                        </tr>
                        <tr>
                            <td>Charter 32 cabins</td>
                            <td>
                                <asp:Literal runat="server" ID="txtStl2d1nCharter1to40passenger"></asp:Literal>
                            </td>
                            <td>
                                <asp:Literal runat="server" ID="txtStl2d1nCharter41to50passenger"></asp:Literal>
                            </td>
                            <td>
                                <asp:Literal runat="server" ID="txtStl2d1nCharter51to63passenger"></asp:Literal>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-12">
                    <table class="table table-bordered table-hover" style="text-align: center; margin-bottom: 0">
                        <tr>
                            <td colspan="4"><strong>3 NGÀY / 2 ĐÊM</strong>
                            </td>
                        </tr>
                        <tr>
                            <td rowspan="2" style="vertical-align: middle"><strong>LOẠI PHÒNG</strong></td>
                            <td colspan="3">
                                <strong><%= GetValidFrom() %> - <%= GetValidTo() %></strong>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <strong>Phòng đôi</strong>
                            </td>
                            <td>
                                <strong>Phòng đơn</strong>
                            </td>
                            <td>
                                <strong>Giường phụ/đệm</strong>
                            </td>
                        </tr>
                        <tr>
                            <td>Deluxe
                            </td>
                            <td>
                                <asp:Literal runat="server" ID="txtStl3d2nDeluxeDouble"></asp:Literal>
                            </td>
                            <td>
                                <asp:Literal runat="server" ID="txtStl3d2nDeluxeSingle"></asp:Literal>
                            </td>
                            <td>
                                <asp:Literal runat="server" ID="txtStl3d2nDeluxeExtrabed"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td>Executive
                            </td>
                            <td>
                                <asp:Literal runat="server" ID="txtStl3d2nExecutiveDouble"></asp:Literal>
                            </td>
                            <td>
                                <asp:Literal runat="server" ID="txtStl3d2nExecutiveSingle"></asp:Literal>
                            </td>
                            <td>
                                <asp:Literal runat="server" ID="txtStl3d2nExecutiveExtrabed"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td>Suite
                            </td>
                            <td>
                                <asp:Literal runat="server" ID="txtStl3d2nSuiteDouble"></asp:Literal>
                            </td>
                            <td>
                                <asp:Literal runat="server" ID="txtStl3d2nSuiteSingle"></asp:Literal>
                            </td>
                            <td>
                                <asp:Literal runat="server" ID="txtStl3d2nSuiteExtrabed"></asp:Literal>
                            </td>
                        </tr>
                    </table>
                    <table class="table table-bordered table-hover" style="text-align: center; border-top: none">
                        <tr>
                            <td colspan="5" style="border-top: none"><strong>THUÊ TRỌN TÀU</strong>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td colspan="4"><strong><%= GetValidFrom() %> - <%= GetValidTo() %></strong>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td><strong>1-40 khách</strong></td>
                            <td><strong>41-50 khách</strong></td>
                            <td><strong>51-63 khách</strong></td>
                        </tr>
                        <tr>
                            <td>Charter 32 cabins</td>
                            <td>
                                <asp:Literal runat="server" ID="txtStl3d2nCharter1to40passenger"></asp:Literal>
                            </td>
                            <td>
                                <asp:Literal runat="server" ID="txtStl3d2nCharter41to50passenger"></asp:Literal>
                            </td>
                            <td>
                                <asp:Literal runat="server" ID="txtStl3d2nCharter51to63passenger"></asp:Literal>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
