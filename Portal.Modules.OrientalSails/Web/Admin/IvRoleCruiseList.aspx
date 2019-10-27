<%@ Page Title="" Language="C#" MasterPageFile="MO.Master" AutoEventWireup="true" CodeBehind="IvRoleCruiseList.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.IvRoleCruiseList" %>

<%@ Register Src="/Admin/Controls/UserSelector.ascx" TagPrefix="asp" TagName="UserSelector" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <title>Phân quyền quản lý theo tàu</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="AdminContent" runat="server">
    <fieldset>
        <legend>Phân quyền quản lý theo tàu</legend>
        <div class="row">
            <div class="col-xs-3">
                <div class="listbox">
                    <table class="table table-bordered table-hover">
                        <tr>
                            <td>Tên tàu</td>
                        </tr>
                        <asp:Repeater ID="rptCruises" runat="server" OnItemDataBound="rptCruises_OnItemDataBound">
                            <itemtemplate>
                        <tr>
                            <td><asp:HyperLink ID="hplCruise" runat="server"></asp:HyperLink></td>
                        </tr>
                    </itemtemplate>
                        </asp:Repeater>
                    </table>
                </div>
            </div>
            <div class="col-xs-9">
                <div class="wbox_content">
                    <div class="group">
                        <table class="table table-bordered table-hover">
                            <tr>
                                <td>Tên tàu</td>
                                <td>
                                    <asp:Label ID="txtCruiseName" runat="server"></asp:Label></td>
                            </tr>
                        </table>
                    </div>
                    <div class="group">
                        <h4>Danh sách users quản lý tàu</h4>
                        <table class="table table-bordered table-hover">
                            <tr>
                                <th>Tên đầy đủ</th>
                                <th>Tên đăng nhập</th>
                                <%--<th>Lần đăng nhập cuối</th>
                                <th>IP sử dụng</th>--%>
<%--                                <th></th>--%>
                            </tr>
                            <asp:Repeater ID="rptUsers" runat="server" OnItemDataBound="rptUsers_ItemDataBound" OnItemCommand="rptUsers_ItemCommand">
                                <itemtemplate>
                                <tr>
                                    <td><asp:Literal ID="litFullname" runat="server"></asp:Literal></td>
                                    <td><asp:HyperLink ID="litUsername" runat="server"></asp:HyperLink></td>
                                   <%-- <td><asp:Literal ID="litLastSignin" runat="server"></asp:Literal></td>
                                    <td><asp:Literal ID="litLastIP" runat="server"></asp:Literal></td>
                                    <td><asp:HyperLink ID="hplPermission" Visible="False" runat="server"></asp:HyperLink></td>--%>
                                    <td><asp:Button ID="btnDelete" runat="server" CommandName="delete" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' Text="Remove"/></td>
                                </tr>
                            </itemtemplate>
                            </asp:Repeater>
                            <tr>
                                <td colspan="2">
                                    <asp:UserSelector ID="userSelector" runat="server" UserSelectedChanged="userSelector_UserSelectedChanged">
                                    </asp:UserSelector>
                                    <asp:Button ID="btnAssignUser" runat="server" Text="Phân thêm user" CssClass="button"
                                                OnClick="btnAssignUser_Click" />
                                    <asp:Label runat="server" ID="lblMsg"  ForeColor="red"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        
                    </div>
                </div>
            </div>
        </div>
    </fieldset>
    <style>
        .group td {
            font-size: 15px;
            color: blue;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
