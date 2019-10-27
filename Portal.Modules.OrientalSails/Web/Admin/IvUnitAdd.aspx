<%@ Page Title="" Language="C#" MasterPageFile="NewPopup.Master" AutoEventWireup="true" CodeBehind="IvUnitAdd.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.IvUnitAdd" %>

<asp:Content ID="Content2" ContentPlaceHolderID="AdminContent" runat="server">
    <script>
        function RefreshParentPage() {
            window.parent.closePoup(1);
        }
        function closePoup() {
            window.parent.closePoup(0);
        }
    </script>
    <div class="row">
        <div class="form-group">
            <div class="row">
                <div class="col-xs-4">
                    Tên đơn vị tính
                </div>
                <div class="col-xs-6">
                    <asp:TextBox ID="txtName" runat="server" CssClass="form-control" placeholder="Tên"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="form-group">
            <div class="row">
                <div class="col-xs-4">
                    Thuộc đơn vị tính  
                </div>
                <div class="col-xs-6">
                    <asp:DropDownList runat="server" ID="ddlParent" CssClass="form-control" />
                </div>
            </div>
        </div>
        <div class="form-group">
            <div class="row">
                <div class="col-xs-4">
                    Hệ số quy đổi so với đơn vị tính chính
                </div>
                <div class="col-xs-6">
                    <asp:TextBox ID="txtRate" runat="server" CssClass="form-control" placeholder="Tỷ lệ"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="form-group">
            <div class="row">
                <div class="col-xs-4">
                    Toán tử
                </div>
                <div class="col-xs-6">
                    <asp:DropDownList runat="server" ID="ddlMath" CssClass="form-control">
                        <asp:ListItem Value="/" Text="Chia"></asp:ListItem>
                        <asp:ListItem Value="*" Text="Nhân"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="form-group">
            <div class="row">
                <div class="col-xs-4">
                    Ghi chú
                </div>
                <div class="col-xs-6">
                    <asp:TextBox ID="txtNote" runat="server" TextMode="MultiLine" CssClass="form-control" placeholder="Ghi chú"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="form-group">
            <div class="row">
                <div class="col-xs-12">
                    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" CssClass="btn btn-primary" /><asp:Button ID="btnCancel" runat="server" Text="Đóng" OnClientClick="closePoup();return false;" CssClass="btn btn-default" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
