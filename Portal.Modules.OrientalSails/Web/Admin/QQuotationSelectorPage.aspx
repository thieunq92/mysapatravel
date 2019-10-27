<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="NewPopup.Master" CodeBehind="QQuotationSelectorPage.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.QQuotationSelectorPage" %>

<%@ Register Assembly="CMS.ServerControls" Namespace="CMS.ServerControls" TagPrefix="svc" %>
<asp:Content ID="AdminContent" ContentPlaceHolderID="AdminContent" runat="server">
    <div class="form-group">
        <div class="row">
            <%--<div class="col-xs-1">
                Name
            </div>
            <div class="col-xs-2">
                <asp:TextBox ID="txtName" runat="server" CssClass="form-control" placeholder="Name" ></asp:TextBox>
            </div>
            <div class="col-xs-1">
                Role
            </div>
            <div class="col-xs-2">
                <asp:DropDownList ID="ddlRoles" runat="server" CssClass="form-control"></asp:DropDownList>
            </div>
            <div class="col-xs-1 nopadding-right nopadding-left">
                Sales in charge
            </div>
            <div class="col-xs-2">
                <asp:DropDownList ID="ddlSales" runat="server" CssClass="form-control">
                </asp:DropDownList>
            </div>
            <div class="col-xs-1">
                Location
            </div>
            <div class="col-xs-2">
                <asp:DropDownList runat="server" ID="ddlLocations" CssClass="form-control" />
            </div>--%>
        </div>
    </div>
    <div class="form-group">
        <div class="row">
            <div class="col-xs-12">
               <%-- <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search"
                            CssClass="btn btn-primary" />--%>
            </div>
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
                </tr>
                <asp:Repeater runat="server" ID="rptQuotation" OnItemDataBound="rptQuotation_OnItemDataBound">
                    <ItemTemplate>
                        <tr>
                            <td><a id="aName" runat="server" data-id="txtName" data-agencyid='<%# Eval("Id")%>'></a></td>
                            <td><%# Eval("GroupCruise.Name") %></td>
                            <td><%# DataBinder.Eval(Container.DataItem,"Validfrom","{0:dd/MM/yyyy}") %></td>
                            <td><%# DataBinder.Eval(Container.DataItem,"Validto","{0:dd/MM/yyyy}") %>
                            </td>
                            <td><%#Eval("CreatedBy.UserName") %></td>
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
</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
