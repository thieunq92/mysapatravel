<%@ Page Language="C#" MasterPageFile="MO.Master" AutoEventWireup="true"
    CodeBehind="CruisesList.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.CruisesList"
    Title="Cruise Management" %>

<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
    <div class="page-header">
        <h3>Cruise management</h3>
    </div>
    <div class="row">
        <div class="col-xs-12">
            <table class="table table-bordered table-hover table-common">
                <tr class="active">
                    <th>Cruise code</th>
                    <th>Name</th>
                    <th>Group</th>
                    <th></th>
                    <th></th>
                </tr>
                <asp:Repeater ID="rptCruises" runat="server" OnItemDataBound="rptCruises_ItemDataBound">
                    <ItemTemplate>
                        <tr>
                            <td><%# DataBinder.Eval(Container.DataItem, "Code") %>
                                <asp:HiddenField runat="server" ID="cid" Value='<%#Eval("Id") %>'/>
                            </td>
                            <td>
                                <asp:HyperLink ID="hplCruise" runat="server"></asp:HyperLink></td>
                            <td><asp:DropDownList runat="server" CssClass="form-control" ID="ddlGroup"/></td>
                            <td>
                                <asp:HyperLink ID="hplRoomClasses" runat="server">Room class</asp:HyperLink></td>
                            <td>
                                <asp:HyperLink ID="hplRooms" runat="server">Rooms</asp:HyperLink></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <tr>
                    <td></td>
                    <td></td>
                    <td><asp:Button runat="server" CssClass="btn btn-primary" Text="Save" OnClick="OnClick"/></td>
                    <td></td>
                    <td></td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
