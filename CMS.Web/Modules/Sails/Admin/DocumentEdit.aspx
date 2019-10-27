<%@ Page Title="" Language="C#" MasterPageFile="NewPopup.Master" AutoEventWireup="true" CodeBehind="DocumentEdit.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.DocumentEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="AdminContent" runat="server">
    <div class="col-xs-12">
        <div class="form-group">
            <div class="row">
                <div class="col-xs-1">
                    Document Name
                </div>
                <div class="col-xs-11">
                    <asp:TextBox ID="txtServiceName" runat="server" CssClass="form-control" placeholder="Name"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="form-group">
            <div class="row">
                <div class="col-xs-1">
                    Category
                </div>
                <div class="col-xs-11">
                    <asp:DropDownList ID="ddlSuppliers" runat="server" CssClass="form-control">
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="form-group">
            <div class="row">
                <div class="col-xs-1">
                    Document file
                </div>
                <div class="col-xs-11">
                    <asp:HyperLink runat="server" ID="hplCurrentFile" Visible="False"></asp:HyperLink><asp:FileUpload runat="server" ID="fileUpload"></asp:FileUpload>
                </div>
            </div>
        </div>
        <div class="form-group">
            <div class="row">
                <div class="col-xs-1">
                    Note
                </div>
                <div class="col-xs-11">
                    <asp:TextBox ID="txtNote" runat="server" CssClass="form-control" placeholder="Note" TextMode="MultiLine"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="form-group">
            <div class="row">
                <div class="col-xs-12">
                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" ValidationGroup="service"
                        CssClass="btn btn-primary" />
                    <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click"
                        CssClass="btn btn-primary" OnClientClick="return confirm('Are you sure ?')" />
                    <asp:Button ID="btnClose" runat="server" Text="Đóng" OnClientClick="closePoup();return false;" CssClass="btn btn-default" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
    <script>
        function closePoup() {
            window.parent.closePoup();
        }
    </script>
</asp:Content>
