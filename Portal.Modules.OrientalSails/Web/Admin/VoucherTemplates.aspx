<%@ Page Title="Voucher Template" Language="C#" MasterPageFile="MO.Master" AutoEventWireup="true"
    CodeBehind="VoucherTemplates.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.VoucherTemplates" %>

<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
    <div class="page-header">
        <h3>Voucher template</h3>
    </div>
    <div class="row">
        <div class="col-xs-12">
            <table class="table table-bordered table-hover table-common">
                <tr class="active">
                    <th>File name</th>
                    <th></th>
                </tr>
                <asp:Repeater runat="server" ID="rptFiles" OnItemDataBound="rptFiles_ItemDataBound">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:Literal runat="server" ID="litFileName"></asp:Literal></td>
                            <td>
                                <asp:HyperLink runat="server" ID="hplDownload" CssClass="btn btn-primary">Download</asp:HyperLink>
                                <asp:LinkButton runat="server" ID="lbtDelete" OnClick="lbtDelete_Click" CssClass="btn btn-primary">Delete</asp:LinkButton>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
            <div class="row">
                <div class="col-xs-2">
                    <asp:FileUpload runat="server" ID="fileUploadTemplate" />
                </div>
                <div class="col-xs-1">
                    <asp:Button runat="server" ID="btnUpload" OnClick="btnUpload_Click" Text="Upload" CssClass="button" />
                </div>
            </div>
        </div>
    </div>

</asp:Content>
