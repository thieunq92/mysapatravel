<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CruiseCharterConfigPrice.ascx.cs" Inherits="Portal.Modules.OrientalSails.Web.Controls.CruiseCharterConfigPrice" %>
<tr>
    <td>
        <table class="table table-bordered table-hover" style="text-align: center; border-top: none">
            <tr>
                <td colspan="2">

                    <h4><strong>
                        <asp:Literal runat="server" ID="litCruiseName"></asp:Literal>
                        <asp:Button runat="server" Style="float: left" ID="btnAddRange" CssClass="btn btn-primary" Text="Thêm số khách" OnClick="btnAddRange_OnClick" />
                    </strong></h4>

                </td>
                <td colspan="2"></td>
            </tr>
            <tr>
                <td>Số khách từ</td>
                <td>Số khách đến</td>
                <td>Giá</td>
                <td>Xóa</td>
            </tr>
            <asp:Repeater ID="rptCharterRanger" OnItemDataBound="rptCharterRanger_OnItemDataBound" runat="server">
                <ItemTemplate>
                    <tr>
                        <td>
                            <asp:HiddenField runat="server" ID="hidCharterPriceId" Value='<%#Eval("Id") %>' />
                            <asp:TextBox runat="server" ID="txtValidFrom" Text='<%#Eval("Validfrom") %>' CssClass="form-control" placeholder="From"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtValidTo" Text='<%#Eval("Validto") %>' CssClass="form-control" placeholder="To"></asp:TextBox></td>
                        <td>
                            <div class="input-group">
                                <span class="input-group-addon">USD</span>
                                <asp:TextBox runat="server" ID="txtPriceUSD" CssClass="form-control" data-inputmask="'alias': 'numeric', 'groupSeparator': ',', 'autoGroup': true, 'digits': 2, 'digitsOptional': true, 'placeholder': '0', 'rightAlign':false" Text="0"></asp:TextBox>
                            </div>
                            <div class="input-group">
                                <span class="input-group-addon">VND</span>
                                <asp:TextBox runat="server" ID="txtPriceVND" CssClass="form-control" data-inputmask="'alias': 'numeric', 'groupSeparator': ',', 'autoGroup': true, 'digits': 2, 'digitsOptional': true, 'placeholder': '0', 'rightAlign':false" Text="0"></asp:TextBox>
                            </div>
                        </td>
                        <td>
                            <asp:CheckBox runat="server" ID="checkIsDelete" />
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </td>
</tr>
