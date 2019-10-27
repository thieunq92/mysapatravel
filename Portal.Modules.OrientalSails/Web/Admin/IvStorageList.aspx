<%@ Page Title="" Language="C#" MasterPageFile="MO.Master" AutoEventWireup="true" CodeBehind="IvStorageList.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.IvStorageList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="AdminContent" runat="server">
    <div class="page-header">
        <h3>Danh sách kho</h3>
    </div>
    <div class="search-panel">
        <div class="form-group">
            <div class="row">
                <div class="col-xs-1">
                    Tên
                </div>
                <div class="col-xs-3">
                    <asp:TextBox ID="txtName" runat="server" CssClass="form-control" placeholder="Name"></asp:TextBox>
                </div>
                <div class="col-xs-1">
                    Kho cha
                </div>
                <div class="col-xs-3">
                    <asp:DropDownList ID="ddlParent" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="form-group">
            <div class="row">
                <div class="col-xs-12">
                    <asp:Button runat="server" ID="btnSearch" CssClass="btn btn-primary"
                        OnClick="btnSearch_Click" Text="Tìm kiếm"></asp:Button>
                    <asp:Button runat="server" ID="btnAddNew" CssClass="btn btn-primary" OnClientClick="return addStorage();"
                        Text="Thêm mới kho"></asp:Button>
                </div>
            </div>
        </div>
    </div>
    <div>
        <div class="row">
            <div class="col-xs-12">
                <table class="table table-bordered table-hover">
                    <asp:Repeater ID="rptStorage" runat="server" OnItemDataBound="rptStorage_OnItemDataBound">
                        <HeaderTemplate>
                            <tr class="active">
                                <th>Tên
                                </th>
                                <th>Kho cha
                                </th>
                                <th>Tàu
                                </th>
                                <th></th>
                                <th></th>
                                <th></th>
                                <th></th>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr id="trItem" runat="server" class="item">
                                <td>
                                    <%#Eval("NameTree") %>
                                </td>
                                <td>
                                    <asp:Literal ID="litParent" runat="server"></asp:Literal>
                                </td>
                                <td>
                                    <%#Eval("Cruise.Name") %>
                                </td>
                                <td>
                                    <a href="javascript:;" onclick='editWarningLimit("<%#Eval("Id") %>")'>SL SP tối thiểu</a>
                                </td>
                                <td>
                                    <a href="/Modules/Sails/Admin/IvProductInStock.aspx?NodeId=1&SectionId=15&storageId=<%#Eval("Id") %>" >Sản phẩm trong kho</a>
                                </td>
                                <td>
                                    <a href="javascript:;" onclick='editProductPrice("<%#Eval("Id") %>")'>Cấu hình giá</a>
                                </td>
                                <td>
                                    <a href="javascript:;" onclick='editStorage(<%#Eval("Id") %>)'>Sửa</a>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>

            </div>
        </div>
    </div>
    <div class="modal fade" id="osModal" tabindex="-1" role="dialog" aria-labelledby="gridSystemModalLabel" data-backdrop="static" data-keyboard="true">
        <div class="modal-dialog" role="document" style="width: 85vw; height: 80vh">
            <div class="modal-content">
                <div class="modal-header">
                    <%--                    <span>Add booking</span>--%>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                </div>
                <div class="modal-body">
                    <iframe frameborder="0" width="100%" style="height: 80vh" scrolling="no"></iframe>
                </div>
            </div>
        </div>
    </div>
    <style>
        .table tr td {
            text-align: left !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
    <script>
        function addStorage() {
            var src = "/Modules/Sails/Admin/IvStorageAdd.aspx?NodeId=1&SectionId=15";
            $("#osModal iframe").attr('src', src);
            $("#osModal").modal();
            return false;
        }
        function editWarningLimit(id) {
            var src = "/Modules/Sails/Admin/IvProductWarningList.aspx?NodeId=1&SectionId=15&storageId=" + id;
            $("#osModal iframe").attr('src', src);
            $("#osModal").modal();
            return false;
        }
        function editProductPrice(id) {
            var src = "/Modules/Sails/Admin/IvProductPriceList.aspx?NodeId=1&SectionId=15&storageId=" + id;
            $("#osModal iframe").attr('src', src);
            $("#osModal").modal();
            return false;
        }
        function editStorage(id) {
            var src = "/Modules/Sails/Admin/IvStorageAdd.aspx?NodeId=1&SectionId=15&id=" + id;
            $("#osModal iframe").attr('src', src);
            $("#osModal").modal();
            return false;
        }
        function closePoup(refesh) {
            $("#osModal").modal('hide');
            if (refesh === 1) {
                window.location.href = window.location.href;
            }
        }
    </script>
</asp:Content>
