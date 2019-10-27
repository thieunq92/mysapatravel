<%@ Page Language="C#" MasterPageFile="MO.Master" AutoEventWireup="true"
    CodeBehind="RoomClassEdit.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.RoomClassEdit"
    Title="Room Class Management" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
    <div class="page-header">
        <h3>Room class management</h3>
    </div>
    <div class="row">
        <div class="col-xs-5">
            <asp:UpdatePanel ID="updatePanelList" runat="server">
                <ContentTemplate>
                    <table class="table table-bordered table-hover table-common">
                        <asp:Repeater ID="rptRoomClass" runat="server" OnItemDataBound="rptRoomClass_ItemDataBound"
                            OnItemCommand="rptRoomClass_ItemCommand">
                            <HeaderTemplate>
                                <tr class="active">
                                    <th>Name
                                    </th>
                                    <th>Description
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
                                        <asp:Label ID="label_Description" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:LinkButton runat="server" ID="imageButtonEdit" CommandName="Edit" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"Id") %>'>
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
                            <div class="col-xs-1">
                                Name
                            </div>
                            <div class="col-xs-11">
                                <asp:TextBox ID="textBoxName" runat="server" CssClass="form-control" placeholder="Name"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <div class="col-xs-1">
                                Description
                            </div>
                            <div class="col-xs-11">
                                <asp:TextBox ID="textBoxDescription" runat="server" TextMode="MultiLine" CssClass="form-control" placeholder="Description"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <div class="col-xs-1">
                                Cruise
                            </div>
                            <div class="col-xs-11">
                                <asp:DropDownList ID="ddlCruises" runat="server" CssClass="form-control"></asp:DropDownList>
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
