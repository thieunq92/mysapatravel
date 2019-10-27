<%@ Page Language="C#" MasterPageFile="MO-NoScriptManager.Master" AutoEventWireup="true" CodeBehind="DocumentView.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.DocumentView" Title="Document view" %>
<asp:Content ID="AdminContent" ContentPlaceHolderID="AdminContent" runat="server">
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
                    <table class="document-table table table-bordered table-hover">
                        <tbody>
                        <tr class="active">
                            <th>Document Name</th>
                            <th>View</th>
                            <th>Download</th>
                        </tr>
                        <asp:Repeater ID="rptDocument" runat="server" OnItemDataBound="rptDocument_OnItemDataBound" >
                            <ItemTemplate>
                                <tr>
                                    <td><%#Eval("Name") %></td>
                                    <td>
                                        <asp:HyperLink ID="hplView" Target="_blank" runat="server"><i class="fas fa-eye"></i></asp:HyperLink>

                                    </td>
                                    <td>
                                        <asp:HyperLink ID="hplDownload" Target="_blank" runat="server"><i class="fas fa-download"></i></asp:HyperLink></td>
                                    
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
