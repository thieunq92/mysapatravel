<%@ Page Language="C#" MasterPageFile="MO.Master" AutoEventWireup="true"
    CodeBehind="QuestionView.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.QuestionView"
    Title="Question Group Overview" %>

<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
    <div class="page-header">
        <h3>Question group overview</h3>
    </div>
    <fieldset>
        <asp:Repeater ID="rptGroups" runat="server" OnItemDataBound="rptGroups_ItemDataBound">
            <ItemTemplate>
                <div class="group">
                    <h4>
                        <asp:HiddenField ID="hiddenId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "Id") %>' />
                        <asp:Literal ID="litGroupName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Name") %>'></asp:Literal>
                        <asp:HyperLink ID="hplEdit" runat="server" CssClass="btn btn-primary">Edit</asp:HyperLink>
                        <asp:LinkButton ID="lbtDelete" runat="server" OnClick="lbtDelete_Click" CssClass="btn btn-primary">Delete</asp:LinkButton>
                    </h4>
                    <ul style="list-style-type:none">
                        <asp:Repeater ID="rptQuestions" runat="server">
                            <ItemTemplate>
                                <li><%# DataBinder.Eval(Container.DataItem, "Name") %></li>
                                <br />
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
            </ItemTemplate>
        </asp:Repeater>
        <h2>Dayboat Form</h2>
        <asp:Repeater ID="rptDayboatGroup" runat="server" OnItemDataBound="rptGroups_ItemDataBound">
            <ItemTemplate>
                <div class="group">
                    <h4>
                        <asp:HiddenField ID="hiddenId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "Id") %>' />
                        <asp:Literal ID="litGroupName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Name") %>'></asp:Literal>
                        <asp:HyperLink ID="hplEdit" runat="server" CssClass="btn btn-primary">Edit</asp:HyperLink>
                        <asp:LinkButton ID="lbtDelete" runat="server" OnClick="lbtDelete_Click" CssClass="btn btn-primary">Delete</asp:LinkButton>
                    </h4>
                    <ul style="list-style-type:none">
                        <asp:Repeater ID="rptQuestions" runat="server">
                            <ItemTemplate>
                                <li><%# DataBinder.Eval(Container.DataItem, "Name") %></li>
                                <br />
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </fieldset>
</asp:Content>
