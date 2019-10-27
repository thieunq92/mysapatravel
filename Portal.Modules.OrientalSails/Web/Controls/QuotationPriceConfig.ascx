<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="QuotationPriceConfig.ascx.cs" Inherits="Portal.Modules.OrientalSails.Web.Controls.QuotationPriceConfig" %>
<%@ Register TagPrefix="ucCruisePrice" TagName="cruise" Src="CruiseCharterConfigPrice.ascx" %>

<asp:HiddenField runat="server" ID="hidTrip" />
<asp:UpdatePanel ID="upPanael" runat="server">
    <ContentTemplate>
        <div class="row">
            <div class="col-xs-12">
                <table class="table table-bordered table-hover" style="text-align: center; margin-bottom: 0">
                    <tr>
                        <td colspan="6">
                            <h3><strong>Giá theo phòng
                                <asp:Button runat="server" CssClass="btn btn-primary" CausesValidation="False" Style="float: left" Text="Thêm phòng" ID="btnAddRoomType" OnClick="btnAddRoomType_OnClick" /></strong></h3>
                        </td>
                    </tr>
                    <tr>
                        <td rowspan="2" style="vertical-align: middle"><strong>LOẠI PHÒNG</strong></td>
                    </tr>
                    <tr>

                        <td>
                            <strong>Phòng đôi
                            </strong>
                        </td>
                        <td>
                            <strong>Phòng đơn
                            </strong>
                        </td>
                        <td>
                            <strong>Giường/Đệm phụ
                            </strong>
                        </td>
                        <td>
                            <strong>Trẻ em
                            </strong>
                        </td>
                        <td>
                            <strong>Xóa
                            </strong>
                        </td>
                    </tr>
                    <asp:Repeater runat="server" ID="rptRoomPrice" OnItemDataBound="rptRoomPrice_OnItemDataBound">
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <strong>
                                        <asp:HiddenField runat="server" ID="hidRoomPriceId" Value='<%#Eval("Id") %>' />
                                        <asp:TextBox runat="server" ID="txtRoomType" CssClass="form-control" Text='<%#Eval("RoomType")%>'></asp:TextBox>
                                    </strong>
                                </td>
                                <td>
                                    <div class="input-group">
                                        <span class="input-group-addon">USD</span>
                                        <asp:TextBox runat="server" ID="txtPriceDoubleUsd" CssClass="form-control" data-inputmask="'alias': 'numeric', 'groupSeparator': ',', 'autoGroup': true, 'digits': 2, 'digitsOptional': true, 'placeholder': '0', 'rightAlign':false" Text="0"></asp:TextBox>
                                    </div>
                                    <div class="input-group">
                                        <span class="input-group-addon">VND</span>
                                        <asp:TextBox runat="server" ID="txtPriceDoubleVnd" CssClass="form-control" data-inputmask="'alias': 'numeric', 'groupSeparator': ',', 'autoGroup': true, 'digits': 2, 'digitsOptional': true, 'placeholder': '0', 'rightAlign':false" Text="0"></asp:TextBox>
                                    </div>
                                </td>
                                <td>
                                    <div class="input-group">
                                        <span class="input-group-addon">USD</span>
                                        <asp:TextBox runat="server" ID="txtPriceTwinUsd" CssClass="form-control" data-inputmask="'alias': 'numeric', 'groupSeparator': ',', 'autoGroup': true, 'digits': 2, 'digitsOptional': true, 'placeholder': '0', 'rightAlign':false" Text="0"></asp:TextBox>
                                    </div>
                                    <div class="input-group">
                                        <span class="input-group-addon">VND</span>
                                        <asp:TextBox runat="server" ID="txtPriceTwinVnd" CssClass="form-control" data-inputmask="'alias': 'numeric', 'groupSeparator': ',', 'autoGroup': true, 'digits': 2, 'digitsOptional': true, 'placeholder': '0', 'rightAlign':false" Text="0"></asp:TextBox>
                                    </div>
                                </td>
                                <td>
                                    <div class="input-group">
                                        <span class="input-group-addon">USD</span>
                                        <asp:TextBox runat="server" ID="txtPriceExtraUsd" CssClass="form-control" data-inputmask="'alias': 'numeric', 'groupSeparator': ',', 'autoGroup': true, 'digits': 2, 'digitsOptional': true, 'placeholder': '0', 'rightAlign':false" Text="0"></asp:TextBox>
                                    </div>
                                    <div class="input-group">
                                        <span class="input-group-addon">VND</span>
                                        <asp:TextBox runat="server" ID="txtPriceExtraVnd" CssClass="form-control" data-inputmask="'alias': 'numeric', 'groupSeparator': ',', 'autoGroup': true, 'digits': 2, 'digitsOptional': true, 'placeholder': '0', 'rightAlign':false" Text="0"></asp:TextBox>
                                    </div>
                                </td>
                                <td>
                                    <div class="input-group">
                                        <span class="input-group-addon">USD</span>
                                        <asp:TextBox runat="server" ID="txtPriceChildUsd" CssClass="form-control" data-inputmask="'alias': 'numeric', 'groupSeparator': ',', 'autoGroup': true, 'digits': 2, 'digitsOptional': true, 'placeholder': '0', 'rightAlign':false" Text="0"></asp:TextBox>
                                    </div>
                                    <div class="input-group">
                                        <span class="input-group-addon">VND</span>
                                        <asp:TextBox runat="server" ID="txtPriceChildVnd" CssClass="form-control" data-inputmask="'alias': 'numeric', 'groupSeparator': ',', 'autoGroup': true, 'digits': 2, 'digitsOptional': true, 'placeholder': '0', 'rightAlign':false" Text="0"></asp:TextBox>
                                    </div>
                                </td>
                                <td>
                                    <asp:CheckBox runat="server" ID="checkIsDelete" />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
                <table class="table table-bordered table-hover" style="text-align: center; border-top: none">
                    <tr>
                        <td colspan="5" style="border-top: none">
                            <h3><strong>THUÊ TRỌN TÀU</strong></h3>
                        </td>
                    </tr>
                    <asp:Repeater runat="server" ID="rptCruise" OnItemDataBound="rptCruise_OnItemDataBound">
                        <ItemTemplate>
                            <asp:HiddenField runat="server" ID="hidCruise" Value='<%#Eval("Id") %>' />
                            <ucCruisePrice:cruise runat="server" ID="cruiseprice" />
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
