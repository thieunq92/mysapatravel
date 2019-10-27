<%@ Page Language="C#" MasterPageFile="MO.Master" AutoEventWireup="true"
    CodeBehind="DocumentManage.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.DocumentManage"
    Title="Document Management" %>

<%@ Register Assembly="CMS.ServerControls" Namespace="CMS.ServerControls" TagPrefix="svc" %>
<%@ Register Assembly="Portal.Modules.OrientalSails" Namespace="Portal.Modules.OrientalSails.Web.Controls"
    TagPrefix="orc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
    <%-- <div class="page-header">
        <h3>Document management</h3>
    </div>--%>
    <div class="row">
        <div class="col-xs-4">
            <ul style="list-style-type: none">
                <asp:Repeater ID="rptCategories" runat="server" OnItemDataBound="rptCategories_ItemDataBound">
                    <ItemTemplate>
                        <li>
                            <asp:HyperLink ID='hplEdit' runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Name")%>'
                                CommandName="edit">
                            </asp:HyperLink>
                            <ul style="list-style-type: none">
                                <asp:Repeater ID="rptChilds" runat="server" OnItemDataBound="rptChilds_ItemDataBound">
                                    <ItemTemplate>
                                        <li>
                                            <asp:HyperLink ID='hplEdit' runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Name")%>'
                                                CommandName="edit">
                                            </asp:HyperLink></li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                        </li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </div>
        <div class="col-xs-8">
            <div class="form-group">
                <div class="row">
                    <div class="col-xs-12">
                        <asp:Button ID="btnAddNew" runat="server" Text="Thêm mới document" CssClass="btn btn-primary" OnClientClick="popupDocument(0);return false;" />
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="row">
                    <div class="col-xs-1">
                        Name
                    </div>
                    <div class="col-xs-11">
                        <asp:TextBox ID="txtServiceName" runat="server" CssClass="form-control" placeholder="Name"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="row">
                    <div class="col-xs-1">
                        Parent Category
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
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="row">
                    <table class="document-table table table-bordered table-hover">
                        <tbody>
                            <tr class="active">
                                <th>Document Name</th>
                                <th>View</th>
                                <th>Download</th>
                                <th></th>

                            </tr>
                            <asp:Repeater ID="rptDocument" runat="server" OnItemDataBound="rptDocument_OnItemDataBound">
                                <ItemTemplate>
                                    <tr>
                                        <td><%#Eval("Name") %></td>
                                        <td>
                                            <asp:HyperLink ID="hplView" Target="_blank" runat="server"><i class="fas fa-eye"></i></asp:HyperLink>

                                        </td>
                                        <td>
                                            <asp:HyperLink ID="hplDownload" Target="_blank" runat="server"><i class="fas fa-download"></i></asp:HyperLink></td>
                                        <td>
                                            <a href="javascript:;" onclick='popupDocument(<%#Eval("Id") %>)'><i class="fa fa-edit fa-lg"></i></a>

                                        </td>

                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>
    <div id="osModal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="gridSystemModalLabel" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog" role="document" style="width: 850px">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close" onclick="closePoup();"><span aria-hidden="true">&times;</span></button>
                    <h3 class="modal-title">Document</h3>
                </div>
                <div class="modal-body">
                    <iframe frameborder="0" width="800" scrolling="no" onload="resizeIframe(this)"></iframe>
                </div>
            </div>
        </div>
    </div>
    <script>
        function popupDocument(id) {
            var query = "";
            if (id > 0) {
                query = "&docid=" + id;
            }
            $("#osModal iframe").attr('src', '/Modules/Sails/Admin/DocumentEdit.aspx?NodeId=1&SectionId=15&catid=' + <%=Request["docid"]%> +query);
            $("#osModal").modal();

        }
        function closePoup() {
            $('#osModal').modal('toggle');
            window.location.href = window.location.href;
        }
    </script>
</asp:Content>
