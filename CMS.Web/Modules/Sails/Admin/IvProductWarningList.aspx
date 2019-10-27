<%@ Page Title="" Language="C#" MasterPageFile="NewPopup.Master" AutoEventWireup="true" CodeBehind="IvProductWarningList.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.IvProductWarningList" %>

<%@ Register TagPrefix="svc" Namespace="CMS.ServerControls" Assembly="CMS.ServerControls" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="AdminContent" runat="server">
    <div class="page-header">
        <h3>Cảnh báo số lượng sản phẩm tối thiểu</h3>
    </div>
    <div class="row">
        <div class="search-panel">
            <div class="form-group">
                <div class="row">
                    <div class="col-xs-3">
                        <asp:Literal ID="litStorage" runat="server"></asp:Literal>
                    </div>
                    <div class="col-xs-1">
                        Tên
                    </div>
                    <div class="col-xs-3">
                        <asp:TextBox ID="txtName" runat="server" CssClass="form-control" placeholder="Tên"></asp:TextBox>
                    </div>
                    <div class="col-xs-1">
                        Danh mục
                    </div>
                    <div class="col-xs-3">
                        <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="row">
                    <div class="col-xs-10">
                        <asp:Button runat="server" ID="btnSearch" CssClass="btn btn-primary"
                            OnClick="btnSearch_Click" Text="Tìm kiếm"></asp:Button>
                    </div>
                    <div class="col-xs-2">
                        <asp:Button ID="btnSaveLimit" runat="server" OnClick="btnSaveLimit_OnClick" Text="Lưu" CssClass="btn btn-primary" />
                        <asp:Button ID="btnCancel" runat="server" Text="Đóng" OnClientClick="closePoup();return false;" CssClass="btn btn-default" />
                    </div>
                </div>
            </div>
        </div>
        <div>
            <div class="row">
               
                <div class="col-xs-12">
                    <table class="table table-bordered table-hover">
                        <asp:Repeater ID="rptProduct" runat="server" OnItemDataBound="rptProduct_OnItemDataBound">
                            <HeaderTemplate>
                                <tr class="active">
                                    <th>Tên
                                    </th>
                                    <th>Danh mục
                                    </th>
                                    <th>Mã SP
                                    </th>
                                    <th>Đơn vị tính chính trong kho</th>
                                    <th>Cảnh báo số lượng tối thiểu</th>
                                   <%-- <th></th>--%>
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr id="trItem" runat="server" class="item">
                                    <td>
                                        <%#Eval("Name") %>
                                        <asp:HiddenField ID="hdproductId" Value='<%#Eval("Id") %>' runat="server" />
                                        <asp:HiddenField ID="hdProductWarningId" Value='' runat="server" />

                                    </td>
                                    <td>
                                        <asp:Literal ID="litCategory" runat="server"></asp:Literal>
                                    </td>
                                    <td>
                                        <%#Eval("Code") %>
                                    </td>
                                    <td>
                                        <asp:Literal ID="litUnit" runat="server"></asp:Literal>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlWarning" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </td>
                                    <%--<td>
                                        <a href="javascript:;" onclick='editProduct(<%#Eval("Id") %>)'>Edit</a>
                                    </td>--%>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </div>
                <div class="pager">
                    <svc:Pager ID="pagerProduct" runat="server" PagerLinkMode="HyperLinkQueryString"
                        HideWhenOnePage="false" ControlToPage="rptProduct"></svc:Pager>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
    <script>
        function closePoup() {
            window.parent.closePoup(1);
        }
    </script>
</asp:Content>
