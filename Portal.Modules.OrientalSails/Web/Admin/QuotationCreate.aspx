<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuotationCreate.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.QuotationCreate"
    MasterPageFile="MO-NoScriptManager.Master" Title="Create Quotation" %>

<asp:Content ID="AdminContent" ContentPlaceHolderID="AdminContent" runat="server">
    <h3 class="page-header">Create Quotation</h3>
    <div class="quotation-panel">
        <div class="form-group">
            <div class="row">
                <div class="col-xs-1">
                    Name
                </div>
                <div class="col-xs-2">
                    <asp:TextBox ID="txtName" runat="server" CssClass="form-control" placeholder="Name" />
                </div>
                <div class="col-xs-1">
                    Currency
                </div>
                <div class="col-xs-2">
                    <asp:DropDownList ID="ddlCurrency" runat="server" CssClass="form-control">
                        <asp:ListItem Text="USD" Value="1"></asp:ListItem>
                        <asp:ListItem Text="VND" Value="2"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-xs-1">
                    Valid From
                </div>
                <div class="col-xs-2">
                    <asp:TextBox runat="server" ID="txtValidFrom" CssClass="form-control" placeholder="Valid From (dd/mm/yyyy)" />
                </div>
                <div class="col-xs-1">
                    Valid To
                </div>
                <div class="col-xs-2">
                    <asp:TextBox runat="server" ID="txtValidTo" CssClass="form-control" placeholder="Valid To (dd/mm/yyyy)" />
                </div>
            </div>
        </div>
        <br />
        <div>
            <ul class="nav nav-tabs" role="tablist">
                <li role="presentation" class="active"><a href="#2d1n" role="tab" data-toggle="tab">2 NGÀY / 1 ĐÊM</a></li>
                <li role="presentation"><a href="#3d2n" role="tab" data-toggle="tab">3 NGÀY / 2 ĐÊM</a></li>
            </ul>
            <div class="tab-content">
                <div role="tabpanel" class="tab-pane active" id="2d1n">
                    <h4>DELUXE ORIENTAL SAILS (18 cabins & 8 cabins)</h4>
                    <div class="row">
                        <div class="col-xs-12">
                            <table class="table table-bordered table-hover" style="text-align: center; margin-bottom: 0">
                                <tr>
                                    <td rowspan="2" style="vertical-align: middle"><strong>LOẠI PHÒNG</strong></td>
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
                                        <div class="input-group">
                                            <span class="input-group-addon">USD</span>
                                            <asp:TextBox runat="server" ID="txtOs2d1nDouble" CssClass="form-control" data-inputmask="'alias': 'numeric', 'groupSeparator': ',', 'autoGroup': true, 'digits': 2, 'digitsOptional': true, 'placeholder': '0', 'rightAlign':false" Text="0"></asp:TextBox>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="input-group">
                                            <span class="input-group-addon">USD</span>
                                            <asp:TextBox runat="server" ID="txtOs2d1nSingle" CssClass="form-control" data-inputmask="'alias': 'numeric', 'groupSeparator': ',', 'autoGroup': true, 'digits': 2, 'digitsOptional': true, 'placeholder': '0', 'rightAlign':false" Text="0"></asp:TextBox>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="input-group">
                                            <span class="input-group-addon">USD</span>
                                            <asp:TextBox runat="server" ID="txtOs2d1nChildren6to11" CssClass="form-control" data-inputmask="'alias': 'numeric', 'groupSeparator': ',', 'autoGroup': true, 'digits': 2, 'digitsOptional': true, 'placeholder': '0', 'rightAlign':false" Text="0"></asp:TextBox>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                            <table class="table table-bordered table-hover" style="text-align: center; border-top: none">
                                <tr>
                                    <td colspan="5" style="border-top: none"><strong>THUÊ TRỌN TÀU</strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Orientalsails Charter 18 cabins</td>
                                    <td colspan="4">
                                        <div class="input-group">
                                            <span class="input-group-addon">USD</span>
                                            <asp:TextBox runat="server" ID="txtOs12d1nCharter" CssClass="form-control" data-inputmask="'alias': 'numeric', 'groupSeparator': ',', 'autoGroup': true, 'digits': 2, 'digitsOptional': true, 'placeholder': '0', 'rightAlign':false" Text="0"></asp:TextBox>
                                        </div>
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
                                        <div class="input-group">
                                            <span class="input-group-addon">USD</span>
                                            <asp:TextBox runat="server" ID="txtOs22d1nCharter1to4passenger" CssClass="form-control" data-inputmask="'alias': 'numeric', 'groupSeparator': ',', 'autoGroup': true, 'digits': 2, 'digitsOptional': true, 'placeholder': '0', 'rightAlign':false" Text="0"></asp:TextBox>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="input-group">
                                            <span class="input-group-addon">USD</span>
                                            <asp:TextBox runat="server" ID="txtOs22d1nCharter5to8passenger" CssClass="form-control" data-inputmask="'alias': 'numeric', 'groupSeparator': ',', 'autoGroup': true, 'digits': 2, 'digitsOptional': true, 'placeholder': '0', 'rightAlign':false" Text="0"></asp:TextBox>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="input-group">
                                            <span class="input-group-addon">USD</span>
                                            <asp:TextBox runat="server" ID="txtOs22d1nCharter9to12passenger" CssClass="form-control" data-inputmask="'alias': 'numeric', 'groupSeparator': ',', 'autoGroup': true, 'digits': 2, 'digitsOptional': true, 'placeholder': '0', 'rightAlign':false" Text="0"></asp:TextBox>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="input-group">
                                            <span class="input-group-addon">USD</span>
                                            <asp:TextBox runat="server" ID="txtOs22d1nCharter13to17passenger" CssClass="form-control" data-inputmask="'alias': 'numeric', 'groupSeparator': ',', 'autoGroup': true, 'digits': 2, 'digitsOptional': true, 'placeholder': '0', 'rightAlign':false" Text="0"></asp:TextBox>
                                        </div>
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
                                    <td rowspan="2" style="vertical-align: middle"><strong>LOẠI PHÒNG</strong></td>
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
                                        <div class="input-group">
                                            <span class="input-group-addon">USD</span>
                                            <asp:TextBox runat="server" ID="txtCls2d1nDouble" CssClass="form-control" data-inputmask="'alias': 'numeric', 'groupSeparator': ',', 'autoGroup': true, 'digits': 2, 'digitsOptional': true, 'placeholder': '0', 'rightAlign':false" Text="0"></asp:TextBox>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="input-group">
                                            <span class="input-group-addon">USD</span>
                                            <asp:TextBox runat="server" ID="txtCls2d1nSingle" CssClass="form-control" data-inputmask="'alias': 'numeric', 'groupSeparator': ',', 'autoGroup': true, 'digits': 2, 'digitsOptional': true, 'placeholder': '0', 'rightAlign':false" Text="0"></asp:TextBox>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="input-group">
                                            <span class="input-group-addon">USD</span>
                                            <asp:TextBox runat="server" ID="txtCls2d1nChildren6to11" CssClass="form-control" data-inputmask="'alias': 'numeric', 'groupSeparator': ',', 'autoGroup': true, 'digits': 2, 'digitsOptional': true, 'placeholder': '0', 'rightAlign':false" Text="0"></asp:TextBox>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                            <table class="table table-bordered table-hover" style="text-align: center; border-top: none">
                                <tr>
                                    <td colspan="5" style="border-top: none"><strong>THUÊ TRỌN TÀU</strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Calypso Cruise Charter</td>
                                    <td>
                                        <div class="input-group">
                                            <span class="input-group-addon">USD</span>
                                            <asp:TextBox runat="server" ID="txtCls2d1nCharter" CssClass="form-control" data-inputmask="'alias': 'numeric', 'groupSeparator': ',', 'autoGroup': true, 'digits': 2, 'digitsOptional': true, 'placeholder': '0', 'rightAlign':false" Text="0"></asp:TextBox>
                                        </div>
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
                                    <td rowspan="2" style="vertical-align: middle"><strong>LOẠI PHÒNG</strong></td>
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
                                        <div class="input-group">
                                            <span class="input-group-addon">USD</span>
                                            <asp:TextBox runat="server" ID="txtStl2d1nDeluxeDouble" CssClass="form-control" data-inputmask="'alias': 'numeric', 'groupSeparator': ',', 'autoGroup': true, 'digits': 2, 'digitsOptional': true, 'placeholder': '0', 'rightAlign':false" Text="0"></asp:TextBox>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="input-group">
                                            <span class="input-group-addon">USD</span>
                                            <asp:TextBox runat="server" ID="txtStl2d1nDeluxeSingle" CssClass="form-control" data-inputmask="'alias': 'numeric', 'groupSeparator': ',', 'autoGroup': true, 'digits': 2, 'digitsOptional': true, 'placeholder': '0', 'rightAlign':false" Text="0"></asp:TextBox>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="input-group">
                                            <span class="input-group-addon">USD</span>
                                            <asp:TextBox runat="server" ID="txtStl2d1nDeluxeExtrabed" CssClass="form-control" data-inputmask="'alias': 'numeric', 'groupSeparator': ',', 'autoGroup': true, 'digits': 2, 'digitsOptional': true, 'placeholder': '0', 'rightAlign':false" Text="0"></asp:TextBox>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Executive
                                    </td>
                                    <td>
                                        <div class="input-group">
                                            <span class="input-group-addon">USD</span>
                                            <asp:TextBox runat="server" ID="txtStl2d1nExecutiveDouble" CssClass="form-control" data-inputmask="'alias': 'numeric', 'groupSeparator': ',', 'autoGroup': true, 'digits': 2, 'digitsOptional': true, 'placeholder': '0', 'rightAlign':false" Text="0"></asp:TextBox>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="input-group">
                                            <span class="input-group-addon">USD</span>
                                            <asp:TextBox runat="server" ID="txtStl2d1nExecutiveSingle" CssClass="form-control" data-inputmask="'alias': 'numeric', 'groupSeparator': ',', 'autoGroup': true, 'digits': 2, 'digitsOptional': true, 'placeholder': '0', 'rightAlign':false" Text="0"></asp:TextBox>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="input-group">
                                            <span class="input-group-addon">USD</span>
                                            <asp:TextBox runat="server" ID="txtStl2d1nExecutiveExtrabed" CssClass="form-control" data-inputmask="'alias': 'numeric', 'groupSeparator': ',', 'autoGroup': true, 'digits': 2, 'digitsOptional': true, 'placeholder': '0', 'rightAlign':false" Text="0"></asp:TextBox>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Suite
                                    </td>
                                    <td>
                                        <div class="input-group">
                                            <span class="input-group-addon">USD</span>
                                            <asp:TextBox runat="server" ID="txtStl2d1nSuiteDouble" CssClass="form-control" data-inputmask="'alias': 'numeric', 'groupSeparator': ',', 'autoGroup': true, 'digits': 2, 'digitsOptional': true, 'placeholder': '0', 'rightAlign':false" Text="0"></asp:TextBox>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="input-group">
                                            <span class="input-group-addon">USD</span>
                                            <asp:TextBox runat="server" ID="txtStl2d1nSuiteSingle" CssClass="form-control" data-inputmask="'alias': 'numeric', 'groupSeparator': ',', 'autoGroup': true, 'digits': 2, 'digitsOptional': true, 'placeholder': '0', 'rightAlign':false" Text="0"></asp:TextBox>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="input-group">
                                            <span class="input-group-addon">USD</span>
                                            <asp:TextBox runat="server" ID="txtStl2d1nSuiteExtrabed" CssClass="form-control" data-inputmask="'alias': 'numeric', 'groupSeparator': ',', 'autoGroup': true, 'digits': 2, 'digitsOptional': true, 'placeholder': '0', 'rightAlign':false" Text="0"></asp:TextBox>
                                        </div>
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
                                    <td><strong>1-40 khách</strong></td>
                                    <td><strong>41-50 khách</strong></td>
                                    <td><strong>51-63 khách</strong></td>
                                </tr>
                                <tr>
                                    <td>Charter 32 cabins</td>
                                    <td>
                                        <div class="input-group">
                                            <span class="input-group-addon">USD</span>
                                            <asp:TextBox runat="server" ID="txtStl2d1nCharter1to40passenger" CssClass="form-control" data-inputmask="'alias': 'numeric', 'groupSeparator': ',', 'autoGroup': true, 'digits': 2, 'digitsOptional': true, 'placeholder': '0', 'rightAlign':false" Text="0"></asp:TextBox>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="input-group">
                                            <span class="input-group-addon">USD</span>
                                            <asp:TextBox runat="server" ID="txtStl2d1nCharter41to50passenger" CssClass="form-control" data-inputmask="'alias': 'numeric', 'groupSeparator': ',', 'autoGroup': true, 'digits': 2, 'digitsOptional': true, 'placeholder': '0', 'rightAlign':false" Text="0"></asp:TextBox>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="input-group">
                                            <span class="input-group-addon">USD</span>
                                            <asp:TextBox runat="server" ID="txtStl2d1nCharter51to63passenger" CssClass="form-control" data-inputmask="'alias': 'numeric', 'groupSeparator': ',', 'autoGroup': true, 'digits': 2, 'digitsOptional': true, 'placeholder': '0', 'rightAlign':false" Text="0"></asp:TextBox>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
                <div role="tabpanel" class="tab-pane" id="3d2n">
                    <h4>DELUXE ORIENTAL SAILS (18 cabins & 8 cabins)</h4>
                    <div class="row">
                        <div class="col-xs-12">
                            <table class="table table-bordered table-hover" style="text-align: center; margin-bottom: 0">
                                <tr>
                                    <td rowspan="2" style="vertical-align: middle"><strong>LOẠI PHÒNG</strong></td>
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
                                        <div class="input-group">
                                            <span class="input-group-addon">USD</span>
                                            <asp:TextBox runat="server" ID="txtOs3d2nDouble" CssClass="form-control" data-inputmask="'alias': 'numeric', 'groupSeparator': ',', 'autoGroup': true, 'digits': 2, 'digitsOptional': true, 'placeholder': '0', 'rightAlign':false" Text="0"></asp:TextBox>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="input-group">
                                            <span class="input-group-addon">USD</span>
                                            <asp:TextBox runat="server" ID="txtOs3d2nSingle" CssClass="form-control" data-inputmask="'alias': 'numeric', 'groupSeparator': ',', 'autoGroup': true, 'digits': 2, 'digitsOptional': true, 'placeholder': '0', 'rightAlign':false" Text="0"></asp:TextBox>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="input-group">
                                            <span class="input-group-addon">USD</span>
                                            <asp:TextBox runat="server" ID="txtOs3d2nChildren6to11" CssClass="form-control" data-inputmask="'alias': 'numeric', 'groupSeparator': ',', 'autoGroup': true, 'digits': 2, 'digitsOptional': true, 'placeholder': '0', 'rightAlign':false" Text="0"></asp:TextBox>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                            <table class="table table-bordered table-hover" style="text-align: center; border-top: none">
                                <tr>
                                    <td colspan="5" style="border-top: none"><strong>THUÊ TRỌN TÀU</strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Orientalsails Charter 18 cabins</td>
                                    <td colspan="4">
                                        <div class="input-group">
                                            <span class="input-group-addon">USD</span>
                                            <asp:TextBox runat="server" ID="txtOs13d2nCharter" CssClass="form-control" data-inputmask="'alias': 'numeric', 'groupSeparator': ',', 'autoGroup': true, 'digits': 2, 'digitsOptional': true, 'placeholder': '0', 'rightAlign':false" Text="0"></asp:TextBox>
                                        </div>
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
                                        <div class="input-group">
                                            <span class="input-group-addon">USD</span>
                                            <asp:TextBox runat="server" ID="txtOs23d2nCharter1to4passenger" CssClass="form-control" data-inputmask="'alias': 'numeric', 'groupSeparator': ',', 'autoGroup': true, 'digits': 2, 'digitsOptional': true, 'placeholder': '0', 'rightAlign':false" Text="0"></asp:TextBox>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="input-group">
                                            <span class="input-group-addon">USD</span>
                                            <asp:TextBox runat="server" ID="txtOs23d2nCharter5to8passenger" CssClass="form-control" data-inputmask="'alias': 'numeric', 'groupSeparator': ',', 'autoGroup': true, 'digits': 2, 'digitsOptional': true, 'placeholder': '0', 'rightAlign':false" Text="0"></asp:TextBox>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="input-group">
                                            <span class="input-group-addon">USD</span>
                                            <asp:TextBox runat="server" ID="txtOs23d2nCharter9to12passenger" CssClass="form-control" data-inputmask="'alias': 'numeric', 'groupSeparator': ',', 'autoGroup': true, 'digits': 2, 'digitsOptional': true, 'placeholder': '0', 'rightAlign':false" Text="0"></asp:TextBox>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="input-group">
                                            <span class="input-group-addon">USD</span>
                                            <asp:TextBox runat="server" ID="txtOs23d2nCharter13to17passenger" CssClass="form-control" data-inputmask="'alias': 'numeric', 'groupSeparator': ',', 'autoGroup': true, 'digits': 2, 'digitsOptional': true, 'placeholder': '0', 'rightAlign':false" Text="0"></asp:TextBox>
                                        </div>
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
                                    <td rowspan="2" style="vertical-align: middle"><strong>LOẠI PHÒNG</strong></td>
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
                                        <div class="input-group">
                                            <span class="input-group-addon">USD</span>
                                            <asp:TextBox runat="server" ID="txtCls3d2nDouble" CssClass="form-control" data-inputmask="'alias': 'numeric', 'groupSeparator': ',', 'autoGroup': true, 'digits': 2, 'digitsOptional': true, 'placeholder': '0', 'rightAlign':false" Text="0"></asp:TextBox>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="input-group">
                                            <span class="input-group-addon">USD</span>
                                            <asp:TextBox runat="server" ID="txtCls3d2nSingle" CssClass="form-control" data-inputmask="'alias': 'numeric', 'groupSeparator': ',', 'autoGroup': true, 'digits': 2, 'digitsOptional': true, 'placeholder': '0', 'rightAlign':false" Text="0"></asp:TextBox>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="input-group">
                                            <span class="input-group-addon">USD</span>
                                            <asp:TextBox runat="server" ID="txtCls3d2nChildren6to11" CssClass="form-control" data-inputmask="'alias': 'numeric', 'groupSeparator': ',', 'autoGroup': true, 'digits': 2, 'digitsOptional': true, 'placeholder': '0', 'rightAlign':false" Text="0"></asp:TextBox>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                            <table class="table table-bordered table-hover" style="text-align: center; border-top: none">
                                <tr>
                                    <td colspan="5" style="border-top: none"><strong>THUÊ TRỌN TÀU</strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Calypso Cruise Charter</td>
                                    <td>
                                        <div class="input-group">
                                            <span class="input-group-addon">USD</span>
                                            <asp:TextBox runat="server" ID="txtCls3d2nCharter" CssClass="form-control" data-inputmask="'alias': 'numeric', 'groupSeparator': ',', 'autoGroup': true, 'digits': 2, 'digitsOptional': true, 'placeholder': '0', 'rightAlign':false" Text="0"></asp:TextBox>
                                        </div>
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
                                    <td rowspan="2" style="vertical-align: middle"><strong>LOẠI PHÒNG</strong></td>
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
                                        <div class="input-group">
                                            <span class="input-group-addon">USD</span>
                                            <asp:TextBox runat="server" ID="txtStl3d2nDeluxeDouble" CssClass="form-control" data-inputmask="'alias': 'numeric', 'groupSeparator': ',', 'autoGroup': true, 'digits': 2, 'digitsOptional': true, 'placeholder': '0', 'rightAlign':false" Text="0"></asp:TextBox>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="input-group">
                                            <span class="input-group-addon">USD</span>
                                            <asp:TextBox runat="server" ID="txtStl3d2nDeluxeSingle" CssClass="form-control" data-inputmask="'alias': 'numeric', 'groupSeparator': ',', 'autoGroup': true, 'digits': 2, 'digitsOptional': true, 'placeholder': '0', 'rightAlign':false" Text="0"></asp:TextBox>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="input-group">
                                            <span class="input-group-addon">USD</span>
                                            <asp:TextBox runat="server" ID="txtStl3d2nDeluxeExtrabed" CssClass="form-control" data-inputmask="'alias': 'numeric', 'groupSeparator': ',', 'autoGroup': true, 'digits': 2, 'digitsOptional': true, 'placeholder': '0', 'rightAlign':false" Text="0"></asp:TextBox>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Executive
                                    </td>
                                    <td>
                                        <div class="input-group">
                                            <span class="input-group-addon">USD</span>
                                            <asp:TextBox runat="server" ID="txtStl3d2nExecutiveDouble" CssClass="form-control" data-inputmask="'alias': 'numeric', 'groupSeparator': ',', 'autoGroup': true, 'digits': 2, 'digitsOptional': true, 'placeholder': '0', 'rightAlign':false" Text="0"></asp:TextBox>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="input-group">
                                            <span class="input-group-addon">USD</span>
                                            <asp:TextBox runat="server" ID="txtStl3d2nExecutiveSingle" CssClass="form-control" data-inputmask="'alias': 'numeric', 'groupSeparator': ',', 'autoGroup': true, 'digits': 2, 'digitsOptional': true, 'placeholder': '0', 'rightAlign':false" Text="0"></asp:TextBox>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="input-group">
                                            <span class="input-group-addon">USD</span>
                                            <asp:TextBox runat="server" ID="txtStl3d2nExecutiveExtrabed" CssClass="form-control" data-inputmask="'alias': 'numeric', 'groupSeparator': ',', 'autoGroup': true, 'digits': 2, 'digitsOptional': true, 'placeholder': '0', 'rightAlign':false" Text="0"></asp:TextBox>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Suite
                                    </td>
                                    <td>
                                        <div class="input-group">
                                            <span class="input-group-addon">USD</span>
                                            <asp:TextBox runat="server" ID="txtStl3d2nSuiteDouble" CssClass="form-control" data-inputmask="'alias': 'numeric', 'groupSeparator': ',', 'autoGroup': true, 'digits': 2, 'digitsOptional': true, 'placeholder': '0', 'rightAlign':false" Text="0"></asp:TextBox>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="input-group">
                                            <span class="input-group-addon">USD</span>
                                            <asp:TextBox runat="server" ID="txtStl3d2nSuiteSingle" CssClass="form-control" data-inputmask="'alias': 'numeric', 'groupSeparator': ',', 'autoGroup': true, 'digits': 2, 'digitsOptional': true, 'placeholder': '0', 'rightAlign':false" Text="0"></asp:TextBox>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="input-group">
                                            <span class="input-group-addon">USD</span>
                                            <asp:TextBox runat="server" ID="txtStl3d2nSuiteExtrabed" CssClass="form-control" data-inputmask="'alias': 'numeric', 'groupSeparator': ',', 'autoGroup': true, 'digits': 2, 'digitsOptional': true, 'placeholder': '0', 'rightAlign':false" Text="0"></asp:TextBox>
                                        </div>
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
                                    <td><strong>1-40 khách</strong></td>
                                    <td><strong>41-50 khách</strong></td>
                                    <td><strong>51-63 khách</strong></td>
                                </tr>
                                <tr>
                                    <td>Charter 32 cabins</td>
                                    <td>
                                        <div class="input-group">
                                            <span class="input-group-addon">USD</span>
                                            <asp:TextBox runat="server" ID="txtStl3d2nCharter1to40passenger" CssClass="form-control" data-inputmask="'alias': 'numeric', 'groupSeparator': ',', 'autoGroup': true, 'digits': 2, 'digitsOptional': true, 'placeholder': '0', 'rightAlign':false" Text="0"></asp:TextBox>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="input-group">
                                            <span class="input-group-addon">USD</span>
                                            <asp:TextBox runat="server" ID="txtStl3d2nCharter41to50passenger" CssClass="form-control" data-inputmask="'alias': 'numeric', 'groupSeparator': ',', 'autoGroup': true, 'digits': 2, 'digitsOptional': true, 'placeholder': '0', 'rightAlign':false" Text="0"></asp:TextBox>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="input-group">
                                            <span class="input-group-addon">USD</span>
                                            <asp:TextBox runat="server" ID="txtStl3d2nCharter51to63passenger" CssClass="form-control" data-inputmask="'alias': 'numeric', 'groupSeparator': ',', 'autoGroup': true, 'digits': 2, 'digitsOptional': true, 'placeholder': '0', 'rightAlign':false" Text="0"></asp:TextBox>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <asp:Button runat="server" ID="btnCreateQuotation" OnClick="btnCreateQuotation_Click" Text="Create quotation" CssClass="btn btn-primary" />
    </div>
</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="Scripts" runat="server">
    <script>
        $("#<%= txtValidFrom.ClientID %>").datetimepicker({
            timepicker: false,
            format: 'd/m/Y',
            scrollInput: false,
            scrollMonth: false,
        })

        $("#<%= txtValidTo.ClientID %>").datetimepicker({
            timepicker: false,
            format: 'd/m/Y',
            scrollInput: false,
            scrollMonth: false,
        })
    </script>
    <script>
        $("#<%= ddlCurrency.ClientID%>").change(function () {
            $(".input-group-addon").html($(this).children("option:selected").html());
        })
    </script>
    <script>
        $("#aspnetForm").validate({
            rules: {
                <%=txtName.UniqueID %> : {
                    required : true,
                } ,   
                <%=txtValidFrom.UniqueID%> : {
                    required : true,
                },
                <%=txtValidTo.UniqueID%> : {
                    required : true
                },
            },
            messages : {
                <%=txtName.UniqueID %> : {
                    required : "Yêu cầu nhập tên Quotation",
                } ,   
                <%=txtValidFrom.UniqueID%> : {
                    required : "Yêu cầu chọn ngày Valid from",
                },
                <%=txtValidTo.UniqueID%> : {
                    required : "Yêu cầu chọn ngày Valid to"
                },
            },  errorElement: "em",
            errorPlacement: function (error, element) {
                error.addClass("help-block");

                if (element.prop("type") === "checkbox") {
                    error.insertAfter(element.parent("label"));
                } else {
                    error.insertAfter(element);
                }

                if (element.siblings("span").prop("class") === "input-group-addon") {
                    error.insertAfter(element.parent()).css({ color: "#a94442" });
                }
            },
            highlight: function (element, errorClass, validClass) {
                $(element).closest("div").addClass("has-error").removeClass("has-success");
            },
            unhighlight: function (element, errorClass, validClass) {
                $(element).closest("div").removeClass("has-error");
            }
        })
    </script>
</asp:Content>
