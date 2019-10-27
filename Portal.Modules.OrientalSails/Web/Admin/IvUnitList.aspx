<%@ Page Title="" Language="C#" MasterPageFile="MO.Master" AutoEventWireup="true" CodeBehind="IvUnitList.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.IvUnitList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="AdminContent" runat="server">
    <div class="page-header">
        <h3>Đơn vị tính</h3>
    </div>
    <div class="search-panel">
        <div class="form-group">
            <div class="row">
                <div class="col-xs-12">
                    <asp:Button runat="server" ID="btnAddNew" CssClass="btn btn-primary" OnClientClick="return addUnit();"
                        Text="Thêm mới"></asp:Button>
                </div>
            </div>
        </div>
    </div>
    <div>
        <div class="row">
            <div class="col-xs-12">
                <table class="table table-bordered table-hover">
                    <asp:Repeater ID="rptUnits" runat="server">
                        <HeaderTemplate>
                            <tr class="active">
                                <th>Tên
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
                                    <a href="javascript:;" onclick='editUnit(<%#Eval("Id") %>)'>Sửa</a>
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
        function addUnit() {
            var src = "/Modules/Sails/Admin/IvUnitAdd.aspx?NodeId=1&SectionId=15";
            $("#osModal iframe").attr('src', src);
            $("#osModal").modal();
            return false;
        } function editUnit(id) {
            var src = "/Modules/Sails/Admin/IvUnitAdd.aspx?NodeId=1&SectionId=15&id=" + id;
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
