<%@ Page Title="Location Management" Language="C#" MasterPageFile="MO.Master" AutoEventWireup="true"
    CodeBehind="AgencyLocations.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.AgencyLocations" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
    <div class="page-header">
        <h3>Location management</h3>
    </div>
    <div class="row">
        <div class="col-xs-3">
            <ul style="list-style-type: none">
                <asp:Repeater ID="rptServices" runat="server" OnItemDataBound="rptServices_ItemDataBound"
                    OnItemCommand="rptServices_ItemCommand">
                    <ItemTemplate>
                        <li>
                            <asp:LinkButton ID='lbtEdit' runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Name")%>'
                                CommandName="edit">
                            </asp:LinkButton>
                            <ul style="list-style-type: none">
                                <asp:Repeater runat="server" ID="rptChild" OnItemDataBound="rptServices_ItemDataBound"
                                    OnItemCommand="rptServices_ItemCommand">
                                    <ItemTemplate>
                                        <li>
                                            <asp:LinkButton ID='lbtEdit' runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Name")%>'
                                                CommandName="edit">
                                            </asp:LinkButton></li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                        </li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
            <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" CssClass="btn btn-primary" Text="New service" />
        </div>
        <div class="col-xs-9">
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
                        Parent
                    </div>
                    <div class="col-xs-11">
                        <asp:DropDownList ID="ddlSuppliers" runat="server" CssClass="form-control">
                        </asp:DropDownList>
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
        </div>
    </div>
</asp:Content>
