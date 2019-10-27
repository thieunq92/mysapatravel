<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerInfoRowInput.ascx.cs"
    Inherits="Portal.Modules.OrientalSails.Web.Controls.CustomerInfoRowInput" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<td style="padding-left: 0!important">
    <asp:HiddenField ID="hiddenId" runat="server" />
    <asp:TextBox ID="txtName" runat="server" CssClass="form-control" placeholder="Name"></asp:TextBox>
</td>
<td>
    <asp:DropDownList ID="ddlGender" runat="server" CssClass="form-control" Style="padding-left: 0px; padding-right: 0px">
        <asp:ListItem>-- Gender --</asp:ListItem>
        <asp:ListItem>Male</asp:ListItem>
        <asp:ListItem>Female</asp:ListItem>
    </asp:DropDownList>
</td>
<td>
    <asp:DropDownList ID="ddlCustomerType" runat="server" CssClass="form-control" Style="padding-left: 0px; padding-right: 0px">
        <asp:ListItem>Adult</asp:ListItem>
        <asp:ListItem>Child</asp:ListItem>
        <asp:ListItem>Baby</asp:ListItem>
    </asp:DropDownList>
</td>
<td>
    <asp:TextBox ID="txtBirthDay" runat="server" CssClass="form-control" placeholder="Birthday(dd/MM/yyyy)" data-control="datetimepicker"></asp:TextBox>
</td>
<asp:TextBox ID="txtNationality" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>
<td>
    <asp:DropDownList ID="ddlNationalities" runat="server" CssClass="form-control" Style="padding-left: 0px; padding-right: 0px"></asp:DropDownList>
</td>
<td>
    <asp:TextBox ID="txtVisaNo" runat="server" CssClass="form-control" placeholder="Visa No"></asp:TextBox>
</td>
<td>
    <asp:TextBox ID="txtPassport" runat="server" CssClass="form-control" placeholder="Passport No"></asp:TextBox>
</td>
<td>
    <asp:TextBox ID="txtVisaExpired" runat="server" CssClass="form-control" placeholder="Visa expired(dd/MM/yyyy)" data-control="datetimepicker"></asp:TextBox>
</td>
<td>
    <asp:PlaceHolder ID="plhChild" runat="server">
        <asp:CheckBox ID="chkChild" runat="server" CssClass="checkbox" Text="Child" />
    </asp:PlaceHolder>
    <asp:CheckBox ID="chkVietKieu" runat="server" Text="" />
</td>
<td>
    <asp:TextBox ID="txtCode" Visible="false" runat="server" CssClass="form-control"></asp:TextBox>
</td>
<td>
    <asp:TextBox ID="txtTotal" runat="server" CssClass="form-control" Style="text-align: right;"></asp:TextBox>
</td>
