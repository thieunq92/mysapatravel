<%@ Page Title="" Language="C#" MasterPageFile="NewPopup.Master" AutoEventWireup="true" CodeBehind="IvProductAdd.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.IvProductAdd" %>

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
                    Tên
                </div>
                <div class="col-xs-6">
                    <asp:TextBox ID="txtName" runat="server" CssClass="form-control" placeholder="Tên"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="form-group">
            <div class="row">
                <div class="col-xs-4">
                    Mã SP
                </div>
                <div class="col-xs-6">
                    <asp:TextBox ID="txtCode" runat="server" CssClass="form-control" placeholder="Mã SP"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="form-group">
            <div class="row">
                <div class="col-xs-4">
                    Danh mục
                </div>
                <div class="col-xs-6">
                    <asp:DropDownList runat="server" CssClass="form-control" ID="ddlCategory" />
                </div>
            </div>
        </div>
        <div class="form-group">
            <div class="row">
                <div class="col-xs-4">
                    Đơn vị tính chính trong kho ( dùng khi nhập kho, báo cáo hàng tồn) 
                </div>
                <div class="col-xs-6">
                    <asp:DropDownList ID="ddlUnit" CssClass="form-control" runat="server"></asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="form-group">
            <div class="row">
                <div class="col-xs-4">
                    Sử dụng trong phòng
                </div>
                <div class="col-xs-6">
                    <asp:CheckBox ID="ckInRoomService" runat="server"></asp:CheckBox>
                </div>
            </div>
        </div>
        <div class="form-group">
            <div class="row">
                <div class="col-xs-4">
                    Là đồ đạc , công cụ
                </div>
                <div class="col-xs-6">
                    <asp:CheckBox ID="chkIsTool" runat="server"></asp:CheckBox>
                </div>
            </div>
        </div>
        <div class="form-group">
            <div class="row">
                <div class="col-xs-4">
                    Ghi chú
                </div>
                <div class="col-xs-6">
                    <asp:TextBox ID="txtNote" TextMode="MultiLine" runat="server" CssClass="form-control" placeholder="Note"></asp:TextBox>
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
