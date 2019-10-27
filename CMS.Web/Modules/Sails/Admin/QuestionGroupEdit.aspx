<%@ Page Language="C#" MasterPageFile="MO.Master" AutoEventWireup="true"
    CodeBehind="QuestionGroupEdit.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.QuestionGroupEdit"
    Title="Question Group Adding" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
    <div class="page-header">
        <h3>Question group adding</h3>
    </div>
    <div class="form-group">
        <div class="row">
            <div class="col-xs-1">
                Priority
            </div>
            <div class="col-xs-1">
                <asp:TextBox runat="server" ID="txtPriority" CssClass="form-control" placeholder="Priority"></asp:TextBox>
            </div>
            <div class="col-xs-1">
                Subject
            </div>
            <div class="col-xs-2">
                <asp:TextBox ID="txtSubject" runat="server" CssClass="form-control" placeholder="Subject"></asp:TextBox>
            </div>
        </div>
    </div>
    <div class="form-group">
        <div class="row">
            <div class="col-xs-2">
                Selection allowed:
            </div>
        </div>
    </div>
    <div class="form-group">
        <div class="row">
            <div class="col-xs-1">
                Selection 1
            </div>
            <div class="col-xs-2">
                <asp:TextBox runat="server" ID="txtSelection1" CssClass="form-control" placeholder="Selection 1"></asp:TextBox>
            </div>
        </div>
    </div>
    <div class="form-group">
        <div class="row">
            <div class="col-xs-1">
                Selection 2
            </div>
            <div class="col-xs-2">
                <asp:TextBox runat="server" ID="txtSelection2" CssClass="form-control" placeholder="Selection 2"></asp:TextBox>
            </div>
        </div>
    </div>
    <div class="form-group">
        <div class="row">
            <div class="col-xs-1">
                Selection 3
            </div>
            <div class="col-xs-2">
                <asp:TextBox runat="server" ID="txtSelection3" CssClass="form-control" placeholder="Selection 3"></asp:TextBox>
            </div>
        </div>
    </div>
    <div class="form-group">
        <div class="row">
            <div class="col-xs-1">
                Selection 4
            </div>
            <div class="col-xs-2">
                <asp:TextBox runat="server" ID="txtSelection4" CssClass="form-control" placeholder="Selection 4"></asp:TextBox>
            </div>
        </div>
    </div>
    <div class="form-group">
        <div class="row">
            <div class="col-xs-1">
                Selection 5
            </div>
            <div class="col-xs-2">
                <asp:TextBox runat="server" ID="txtSelection5" CssClass="form-control" placeholder="Selection 5"></asp:TextBox>
            </div>
        </div>
    </div>
    <div class="form-group">
        <div class="row">
            <div class="col-xs-1">
                Good choice
            </div>
            <div class="col-xs-2">
                <asp:DropDownList runat="server" ID="ddlGoodChoice" CssClass="form-control">
                    <asp:ListItem Value="1">Selection 1</asp:ListItem>
                    <asp:ListItem Value="2">Selection 2</asp:ListItem>
                    <asp:ListItem Value="3">Selection 3</asp:ListItem>
                    <asp:ListItem Value="4">Selection 4</asp:ListItem>
                    <asp:ListItem Value="5">Selection 5</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-xs-2">
                <div class="form-check">
                    <label for="chkIsInDayboatForm" style="font-weight: normal">
                        <input type="checkbox" runat="server" id="chkIsInDayboatForm" />
                        InDayboatForm</label>
                </div>
            </div>
        </div>
    </div>
    <div class="form-group">
        <div class="row">
            <div class="col-xs-1">
                Sub-category
            </div>
            <div class="col-xs-11">
                <asp:Repeater ID="rptSubCategory" runat="server" OnItemDataBound="rptSubCategory_ItemDataBound">
                    <ItemTemplate>
                        <div class="form-group">
                            <div class="col-xs-1">
                                <asp:HiddenField ID="hiddenId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "Id") %>' />
                                Name
                            </div>
                            <div class="col-xs-2">
                                <asp:TextBox ID="txtName" runat="server" CssClass="form-control" placeholder="Name"></asp:TextBox>
                            </div>
                            <div class="col-xs-1">
                                Description
                            </div>
                            <div class="col-xs-5">
                                <asp:TextBox ID="txtContent" runat="server" CssClass="form-control" TextMode="MultiLine" placeholder="Description"></asp:TextBox>
                            </div>
                            <div class="col-xs-2">
                                <asp:Button ID="btnRemove" runat="server" CssClass="btn btn-primary" Text="Remove" OnClick="btnRemove_Click" />
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>
    <div class="form-group">
        <div class="row">
            <div class="col-xs-12">
                <asp:Button ID="btnAddSubCategory" runat="server" CssClass="btn btn-primary" Text="Add Sub-category" OnClick="btnAddSubCategory_Click" />
            </div>
        </div>
    </div>
    <div class="form-group">
        <div class="row">
            <div class="col-xs-12">
                  <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="btnSave_Click" />
            </div>
        </div>
    </div>
</asp:Content>
