<%@ Page Language="C#" MasterPageFile="MO-NoScriptManager.Master" Title="Quotation Management" AutoEventWireup="true" CodeBehind="QQuotationList.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.QQuotationList" %>
<%@ Register TagPrefix="svc" Namespace="CMS.ServerControls" Assembly="CMS.ServerControls" %>

<asp:Content ID="AdminContent" ContentPlaceHolderID="AdminContent" runat="server">
    <h3 class="page-header">Quotation Management</h3>
    <div class="quotation-table">
        <div class="row">
            <div class="col-xs-12">
                <a href="QQuotationEdit.aspx?NodeId=1&SectionId=15" class="btn btn-primary">Create Quotation</a>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-12">
                <table class="table table-bordered table-hover table-common" style="text-align: center">
                    <tr class="active">
                        <th>Quotation</th>
                        <th>Group</th>
                        <th>From</th>
                        <th>To</th>
                        <th>Created by</th>
                        <%--<th>Export</th>--%>
                    </tr>
                    <asp:Repeater runat="server" ID="rptQuotation" OnItemDataBound="rptQuotation_OnItemDataBound">
                        <ItemTemplate>
                            <tr>
                                <td><a href="QQuotationEdit.aspx?NodeId=1&SectionId=15&qid=<%# Eval("Id") %>"><%# DataBinder.Eval(Container.DataItem,"Validfrom","{0:dd/MM/yyyy}") %>-<%# DataBinder.Eval(Container.DataItem,"Validto","{0:dd/MM/yyyy}") %>-<%# Eval("GroupCruise.Name") %></a></td>
                                <td><%# Eval("GroupCruise.Name") %></td>
                                <td><%# DataBinder.Eval(Container.DataItem,"Validfrom","{0:dd/MM/yyyy}") %></td>
                                <td><%# DataBinder.Eval(Container.DataItem,"Validto","{0:dd/MM/yyyy}") %>
                                </td>
                                <td><%#Eval("CreatedBy.UserName") %></td>
                                <%--                                <td><asp:LinkButton runat="server" Text="Export" CommandArgument='<%#Eval("Id") %>' CommandName="export"></asp:LinkButton></td>--%>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </div>
        <div class="pager">
            <svc:Pager ID="pagerProduct" runat="server" PagerLinkMode="HyperLinkQueryString"
                       HideWhenOnePage="false" ControlToPage="rptQuotation"></svc:Pager>
        </div>
    </div>
</asp:Content>
