<%@ Page Language="C#" MasterPageFile="MO.Master" AutoEventWireup="true"
    CodeBehind="AgencyEdit.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.AgencyEdit" %>

<%@ Register Assembly="CMS.ServerControls" Namespace="CMS.ServerControls.FileUpload"
    TagPrefix="svc" %>
<%@ Register Assembly="CMS.ServerControls" Namespace="CMS.ServerControls" TagPrefix="svc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Head" ContentPlaceHolderID="Head" runat="server">
    <title>Agency</title>
</asp:Content>
<asp:Content ID="AdminContent" ContentPlaceHolderID="AdminContent" runat="server">
    <div class="wrapper">
        <div class="row">
            <div class="col-xs-6">
                <h4 class="page-header --text-bold">Agency information</h4>
                <div class="form-group">
                    <div class="row">
                        <div class="col-xs-4">
                            Name
                        </div>
                        <div class="col-xs-8">
                            <asp:TextBox ID="textBoxName" runat="server" CssClass="form-control" placeholder="Name"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="row">
                        <div class="col-xs-4">
                            Agent level
                        </div>
                        <div class="col-xs-8">
                            <asp:DropDownList ID="ddlAgentLevel" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="row">
                        <div class="col-xs-4">
                            Role
                        </div>
                        <div class="col-xs-8 --width-auto">
                            <asp:DropDownList runat="server" ID="ddlAgencyRoles" CssClass="form-control" AppendDataBoundItems="true">
                                <asp:ListItem Text="--Role--" Value="-1"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="row">
                        <div class="col-xs-4">
                            Address
                        </div>
                        <div class="col-xs-8">
                            <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" CssClass="form-control" placeholder="Address"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="row">
                        <div class="col-xs-4">
                            Phone
                        </div>
                        <div class="col-xs-8">
                            <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control" placeholder="Phone"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="row">
                        <div class="col-xs-4">
                            Email
                        </div>
                        <div class="col-xs-8">
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Email"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="row">
                        <div class="col-xs-4">
                            Website
                        </div>
                        <div class="col-xs-8">
                            <asp:TextBox ID="txtWebsite" runat="server" CssClass="form-control" placeholder="Website"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="row">
                        <div class="col-xs-4">
                            Tax code
                        </div>
                        <div class="col-xs-8">
                            <asp:TextBox ID="txtTaxCode" runat="server" CssClass="form-control" placeholder="Tax code"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="row">
                        <div class="col-xs-4">
                            Location
                        </div>
                        <div class="col-xs-8 --width-auto">
                            <asp:DropDownList ID="ddlLocations" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                <asp:ListItem Text="--Location--" Value="-1"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="row">
                        <div class="col-xs-4">
                            Payment
                        </div>
                        <div class="col-xs-8 --width-auto">
                            <asp:DropDownList ID="ddlPaymentPeriod" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                <asp:ListItem Value="None" Text="--Payment period--"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="row">
                        <div class="col-xs-4">
                            Other information
                        </div>
                        <div class="col-xs-8">
                            <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" CssClass="form-control" placeholder="Other information"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-xs-6">
                <h4 class="page-header --text-bold">Thông tin trong contract</h4>
                <div class="form-group">
                    <div class="row">
                        <div class="col-xs-4">
                            Tên giao dịch
                        </div>
                        <div class="col-xs-8">
                            <asp:TextBox ID="txtTradingName" runat="server" CssClass="form-control" placeholder="Tên giao dịch"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="row">
                        <div class="col-xs-4">
                            Người đại diện
                        </div>
                        <div class="col-xs-8">
                            <asp:TextBox ID="txtRepresentative" runat="server" CssClass="form-control" placeholder="Người đại diện"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="row">
                        <div class="col-xs-4">
                            Chức vụ
                        </div>
                        <div class="col-xs-8">
                            <asp:TextBox ID="txtRepresentativePosition" runat="server" CssClass="form-control" placeholder="Chức vụ"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="row">
                        <div class="col-xs-4">
                            Địa chỉ 
                        </div>
                        <div class="col-xs-8">
                            <asp:TextBox ID="txtContractAddress" runat="server" TextMode="MultiLine" CssClass="form-control" placeholder="Địa chỉ"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="row">
                        <div class="col-xs-4">
                            Điện thoại
                        </div>
                        <div class="col-xs-8">
                            <asp:TextBox ID="txtContractPhone" runat="server" CssClass="form-control" placeholder="Điện thoại"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="row">
                        <div class="col-xs-4">
                            Mã số thuế
                        </div>
                        <div class="col-xs-8">
                            <asp:TextBox ID="txtContractTaxCode" runat="server" CssClass="form-control" placeholder="Mã số thuế"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="row">
                        <div class="col-xs-4">
                            Người liên hệ
                        </div>
                        <div class="col-xs-8">
                            <asp:TextBox ID="txtContact" runat="server" CssClass="form-control" placeholder="Người liên hệ"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="row">
                        <div class="col-xs-4">
                            Địa chỉ
                        </div>
                        <div class="col-xs-8">
                            <asp:TextBox ID="txtContactAddress" runat="server" CssClass="form-control" placeholder="Địa chỉ"></asp:TextBox>
                        </div>
                    </div>
                </div>

                <div class="form-group">
                    <div class="row">
                        <div class="col-xs-4">
                            Email
                        </div>
                        <div class="col-xs-8">
                            <asp:TextBox ID="txtContactEmail" runat="server" CssClass="form-control" placeholder="Email"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="row">
                        <div class="col-xs-4">
                            Chức vụ
                        </div>
                        <div class="col-xs-8">
                            <asp:TextBox ID="txtContactPosition" runat="server" CssClass="form-control" placeholder="Chức vụ"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="form-group">
                <div class="row">
                    <div class="col-xs-2">
                    </div>
                    <div class="col-xs-10">
                        <asp:Literal ID="litCreated" runat="server"></asp:Literal>
                        <asp:Literal ID="litModified" runat="server"></asp:Literal>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="row">
                    <div class="col-xs-2">
                    </div>
                    <div class="col-xs-10">
                        <asp:Button runat="server" ID="buttonSave" CssClass="btn btn-primary"
                            Text="Save" OnClick="buttonSave_Click"></asp:Button>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <h4 class="page-header --text-bold">Sales in charge</h4>
            <div class="row">
                <div class="col-xs-12">
                    <div class="form-group">
                        <div class="row">
                            <div class="col-xs-2">
                                Sale in charge
                            </div>
                            <div class="col-xs-3 --width-auto">
                                <asp:DropDownList ID="ddlSales" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                    <asp:ListItem Text="--Sales in charge--" Value="-1"></asp:ListItem>
                                </asp:DropDownList>

                            </div>
                            <div class="col-xs-2 --no-padding-leftright">
                                <asp:TextBox ID="txtSaleStart" runat="server" CssClass="form-control" placeholder="Apply from" data-control="datetimepicker"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <div class="col-xs-2">
                                Sales in charge history
                            </div>
                            <div class="col-xs-8">
                                <table style="width: 100%;">
                                    <asp:Repeater ID="rptHistory" runat="server" OnItemDataBound="rptHistory_ItemDataBound">
                                        <ItemTemplate>
                                            <tr id="trLine" runat="server">
                                                <td>
                                                    <asp:Literal ID="litSale" runat="server"></asp:Literal>
                                                    apply from
                                                <asp:Literal ID="litSaleStart" runat="server"></asp:Literal>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="Scripts" runat="server">
    <script>
        $(document).ready(function () {
            var listToHide = ['Captain', 'Sales', 'Anonymous user', 'Authenticated user', 'Editor', 'Administrator', 'Agent $71', 'Agent $68', 'Agent $69', 'Agent $70', 'Agent $72', 'Agent $73', 'Agent $74', 'Agent $75'];
            for (var i = 0; i < listToHide.length; i++) {
                $('#<%=ddlAgencyRoles.ClientID%> option').each(function (k, v) {
                    if ($(v).text() == listToHide[i]) {
                        $(v).css('display', 'none');
                    }
                });
            }
        })
    </script>
    <script>
        $(document).ready(function () {
            $('#<%=txtAddress.ClientID%>').on('blur', function () {
                if ($('#<%= txtContractAddress.ClientID%>').val() == '')
                    $('#<%= txtContractAddress.ClientID%>').val($(this).val());
                else {
                    if ($('#<%=txtAddress.ClientID%>').val() != $('#<%= txtContractAddress.ClientID%>').val()) {
                        let isConfirmed = confirm("Do you want to update contract address?")
                        if (isConfirmed) $('#<%= txtContractAddress.ClientID%>').val($(this).val());
                    }
                }
            })
            $('#<%=txtPhone.ClientID%>').on('blur', function () {
                if ($('#<%= txtContractPhone.ClientID%>').val() == '')
                    $('#<%= txtContractPhone.ClientID%>').val($(this).val());
                else {
                    if ($('#<%=txtPhone.ClientID%>').val() != $('#<%= txtContractPhone.ClientID%>').val()) {
                        let isConfirmed = confirm("Do you want to update contract phone?")
                        if (isConfirmed) $('#<%= txtContractPhone.ClientID%>').val($(this).val());
                    }
                }
            })
            $('#<%=txtTaxCode.ClientID%>').on('blur', function () {
                if ($('#<%= txtContractTaxCode.ClientID%>').val() == '')
                    $('#<%= txtContractTaxCode.ClientID%>').val($(this).val());
                else {
                    if ($('#<%=txtTaxCode.ClientID%>').val() != $('#<%= txtContractTaxCode.ClientID%>').val()) {
                        let isConfirmed = confirm("Do you want to update contract tax code?")
                        if (isConfirmed) $('#<%= txtContractTaxCode.ClientID%>').val($(this).val());
                    }
                }
            })
            $('#<%= buttonSave.ClientID%>').click(function () {
                $('#<%=txtAddress.ClientID%>').trigger('blur');
                $('#<%=txtPhone.ClientID%>').trigger('blur');
                $('#<%=txtTaxCode.ClientID%>').trigger('blur');
            })
        })
    </script>
</asp:Content>
