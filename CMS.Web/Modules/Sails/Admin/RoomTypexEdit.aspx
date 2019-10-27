<%@ Page Language="C#" MasterPageFile="MO.Master" AutoEventWireup="true"
    CodeBehind="RoomTypexEdit.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.RoomTypexEdit"
    Title="Room Type Management" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
    <div class="page-header">
        <h3>Room type management</h3>
    </div>
    <div class="row">
        <div class="col-xs-5">
            <asp:UpdatePanel ID="updatePanelList" runat="server">
                <ContentTemplate>
                    <table class="table table-bordered table-hover table-common">
                        <asp:Repeater ID="rptRoomTypex" runat="server" OnItemDataBound="rptRoomTypex_ItemDataBound"
                            OnItemCommand="rptRoomTypex_ItemCommand">
                            <HeaderTemplate>
                                <tr class="active">
                                    <th>Name
                                    </th>
                                    <th>Capacity
                                    </th>
                                    <th></th>
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr class="item">
                                    <td>
                                        <asp:Label ID="label_Name" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="label_Capacity" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:LinkButton runat="server" ID="imageButtonEdit" CommandName="Edit"
                                            CommandArgument='<%# DataBinder.Eval(Container.DataItem,"Id") %>'>
                                             <i class="fa fa-edit fa-lg" aria-hidden="true" data-toggle="tooltip" data-placement="top" title="" data-original-title="Edit"></i>
                                        </asp:LinkButton>
                                        <asp:LinkButton runat="server" ID="imageButtonDelete" CommandName="Delete"
                                            CommandArgument='<%# DataBinder.Eval(Container.DataItem,"Id") %>' OnClientClick="return confirm('Are you sure?')">
                                            <i class="fa fa-window-close fa-lg text-danger" aria-hidden="true" title="" data-toggle="tooltip" data-placement="top" data-original-title="Delete"></i>
                                        </asp:LinkButton>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div class="col-xs-7">
            <asp:UpdatePanel ID="updatePanelEdit" runat="server">
                <ContentTemplate>
                    <div class="form-group">
                        <div class="row">
                            <div class="col-xs-2">
                                Name
                            </div>
                            <div class="col-xs-10">
                                <asp:TextBox ID="textBoxName" runat="server" CssClass="form-control" placeholder="Name"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <div class="col-xs-2">
                                Capacity
                            </div>
                            <div class="col-xs-10">
                                <asp:TextBox ID="textBoxCapacity" runat="server" CssClass="form-control" placeholder="Capacity"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <div class="col-xs-2">
                                Allow booking with diffirent people
                            </div>
                            <div class="col-xs-10">
                                <asp:CheckBox ID="chkAllowSingle" runat="server" />
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <div class="col-xs-2">
                                Is shared cabin 
                            </div>
                            <div class="col-xs-10">
                                <asp:CheckBox ID="chkShared" runat="server" />
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <div class="col-xs-12">
                                <asp:Button ID="buttonSubmit" runat="server" CssClass="btn btn-primary"
                                    OnClick="buttonSubmit_Click" />
                                <asp:Button ID="buttonCancel" runat="server" CssClass="btn btn-primary" OnClick="buttonCancel_Click" Text="Cancel" />
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
