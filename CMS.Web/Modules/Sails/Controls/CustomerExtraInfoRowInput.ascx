<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerExtraInfoRowInput.ascx.cs" Inherits="Portal.Modules.OrientalSails.Web.Controls.CustomerExtraInfoRowInput" %>
<input id="hiddenId" type="hidden" runat="server" class="hiddenId" />
<div class="col-xs-1 nopadding custom-name-width" title="Name">
    <asp:TextBox ID="txtName" runat="server" CssClass="acomplete form-control" placeholder="Name"></asp:TextBox>
</div>
<div class="col-xs-1 nopadding" title="Gender">
    <asp:DropDownList ID="ddlGender" runat="server" CssClass="form-control ddlGender">
        <asp:ListItem>--Gender--</asp:ListItem>
        <asp:ListItem>Male</asp:ListItem>
        <asp:ListItem>Female</asp:ListItem>
    </asp:DropDownList>
</div>
<div class="col-xs-1 nopadding custom-col-xs-1" title="Birthday">
    <asp:TextBox ID="txtBirthDay" runat="server" placeholder="Birthday" CssClass="form-control txtBirthday" data-control="datepicker"></asp:TextBox>
    <i class="fa fa-lg fa-calendar datetimepicker-trigger" aria-hidden="true"></i>
</div>
<div class="col-xs-1 nopadding custom-col-xs-1" title="Nationality">
    <asp:DropDownList ID="ddlNationalities" runat="server" CssClass="form-control ddlNationality" AppendDataBoundItems="true">
    </asp:DropDownList>
</div>
<div class="col-xs-1 nopadding custom-col-xs-1" title="Visa No">
    <asp:TextBox ID="txtVisaNo" runat="server" CssClass="form-control txtVisaNo" placeholder="Visa No"></asp:TextBox>
</div>
<div class="col-xs-1 nopadding custom-col-xs-1" title="Passport/CMTND">
    <asp:TextBox ID="txtPassport" runat="server" CssClass="form-control txtPassport" placeholder="Passport/CMTND"></asp:TextBox>
</div>
<div class="col-xs-1 nopadding custom-col-xs-1" title="Visa Expired">
    <asp:TextBox ID="txtVisaExpired" runat="server" CssClass="form-control txtVisaExpired" placeholder="Visa Expired" data-control="datepicker"></asp:TextBox>
    <i class="fa fa-lg fa-calendar datetimepicker-trigger" aria-hidden="true"></i>
</div>
<div class="col-xs-1 nopadding custom-col-xs-1" title="Nguyên Quán">
    <asp:TextBox ID="txtNguyenQuan" runat="server" CssClass="form-control txtNguyenQuan" placeholder="Nguyên Quán"></asp:TextBox>
</div>
<div class="col-xs-1 nopadding custom-col-xs-1">
    <div class="checkbox">
        <label style="display: none">
            <input id="chkVietKieu" runat="server" visible="False" type="checkbox" value="Viet Kieu" cssclass="chkVietKieu" />
        </label>
        <label>
            <input id="chkBaBy" runat="server" visible="False" type="checkbox" cssclass="chkVietKieu" /></label>
        <label>
            <input id="chkChild" runat="server" visible="False" type="checkbox" cssclass="chkVietKieu" />
        </label>
    </div>
</div>
<asp:TextBox ID="txtCode" runat="server" Width="30" CssClass="txtCode" Style="display: none"></asp:TextBox>
<asp:TextBox ID="txtTotal" runat="server" Width="60" Style="text-align: right; display: none"></asp:TextBox>
<input class='button btnOldBooking' value='Old Booking' style='width: 57px; display: none'>