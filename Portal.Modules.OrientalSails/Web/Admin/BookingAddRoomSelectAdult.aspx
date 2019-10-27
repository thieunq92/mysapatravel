<%@ Page Title="" Language="C#" MasterPageFile="NewPopup.Master" AutoEventWireup="true" CodeBehind="BookingAddRoomSelectAdult.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.BookingAddRoomSelectAdult" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="AdminContent" runat="server">
      <script>
        function RefreshParentPage() {
            window.parent.RefreshParentPage();
        }
        function closePoup() {
            window.parent.closePoup(0);
        }
    </script>
    <div class="form-group">
        <div class="row">
            <div class="col-xs-12">
                <asp:Label ID="lblMsg" ForeColor="red" runat="server"></asp:Label>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-xs-12">
            <em>Thông tin phòng</em>
            <table class="table table-bordered table-hover table-common">
                <asp:Repeater ID="rptRoom" runat="server" OnItemDataBound="rptRoom_ItemDataBound">
                    <HeaderTemplate>
                        <tr class="active">
                            <th>No
                            </th>
                            <th>Name
                            </th>
                            <th>Room Type
                            </th>
                            <th>Room Class
                            </th>
                            <th>Cruise
                            </th>
                            <th>Floor
                            </th>
                            <th>Adult
                            </th>
                            <th>Child
                            </th>
                            <th>Baby
                            </th>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr class="item">
                            <td><%#Container.ItemIndex + 1%>
                            </td>
                            <td>
                                <asp:HiddenField Value='<%# Eval("Id")%>' ID="hid" runat="server" />
                                <asp:HyperLink ID="hyperLink_Name" runat="server"></asp:HyperLink>
                            </td>
                            <td>
                                <asp:Label ID="label_RoomType" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="label_RoomClass" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="labelCruise" runat="server"></asp:Label>
                            </td>
                            <td><%#Eval("Floor") %></td>
                            <td>
                                <asp:DropDownList ID="ddlAdults" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="2">2 adult(s)</asp:ListItem>
                                    <asp:ListItem Value="1">1 adult(s)</asp:ListItem>
                                </asp:DropDownList></td>
                            <td>
                                <asp:DropDownList ID="ddlChild" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="0">0 child(s)</asp:ListItem>
                                    <asp:ListItem Value="1">1 child(s)</asp:ListItem>
                                </asp:DropDownList></td>
                            <td>
                                <asp:DropDownList ID="ddlBaby" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="0">0 baby(s)</asp:ListItem>
                                    <asp:ListItem Value="1">1 baby(s)</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
    </div>
    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" CssClass="btn btn-primary" /><asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClientClick="closePoup();return false;" CssClass="btn btn-default" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
