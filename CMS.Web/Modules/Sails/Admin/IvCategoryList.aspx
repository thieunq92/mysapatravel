<%@ Page Title="" Language="C#" MasterPageFile="MO.Master" AutoEventWireup="true" CodeBehind="IvCategoryList.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.IvCategoryList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="AdminContent" runat="server">
    <div class="page-header">
        <h3>Danh mục</h3>
    </div>
    <div class="search-panel">
        <div class="form-group">
            <div class="row">
                <div class="col-xs-1">
                    Tên
                </div>
                <div class="col-xs-3">
                    <asp:TextBox ID="txtName" runat="server" CssClass="form-control" placeholder="Tên"></asp:TextBox>
                </div>
                <div class="col-xs-1">
                    Danh mục cha
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
                    <asp:Button runat="server" ID="btnAddNew" CssClass="btn btn-primary" OnClientClick="return addCategory();"
                        Text="Thêm mới"></asp:Button>
                </div>
            </div>
        </div>
    </div>
    <div>
        <div class="row">
            <div class="col-xs-12">
                <table class="table table-bordered table-hover">
                    <asp:Repeater ID="rptCategory" runat="server" OnItemDataBound="rptCategory_OnItemDataBound">
                        <HeaderTemplate>
                            <tr class="active">
                                <th>Tên
                                </th>
                                <th>Danh mục cha
                                </th>

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
                                    <a href="javascript:;" onclick='editCategory(<%#Eval("Id") %>)'>Sửa</a>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>

            </div>
        </div>
    </div>
    <div class="modal fade" id="osModal" tabindex="-1" role="dialog" aria-labelledby="gridSystemModalLabel" data-backdrop="static" data-keyboard="true">
        <div class="modal-dialog" role="document" style="width: 60vw; height: 50vh">
            <div class="modal-content">
                <div class="modal-header">
                    <%--                    <span>Add booking</span>--%>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                </div>
                <div class="modal-body">
                    <iframe frameborder="0" width="90%" style="height: 45vh" scrolling="no"></iframe>
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
        function addCategory() {
            var src = "/Modules/Sails/Admin/IvCategoryAdd.aspx?NodeId=1&SectionId=15";
            $("#osModal iframe").attr('src', src);
            $("#osModal").modal();
            return false;
        }
        function editCategory(id) {
            var src = "/Modules/Sails/Admin/IvCategoryAdd.aspx?NodeId=1&SectionId=15&id=" + id;
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
