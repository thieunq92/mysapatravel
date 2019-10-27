<%@ Page Title="Voucher Batch Creating" Language="C#" MasterPageFile="MO.Master" AutoEventWireup="true"
    CodeBehind="VoucherEdit.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.VoucherEdit" %>

<%@ Register Assembly="Portal.Modules.OrientalSails" Namespace="Portal.Modules.OrientalSails.Web.Controls"
    TagPrefix="orc" %>
<%@ Register TagPrefix="ajax" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=1.0.20229.20821, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
    <div class="page-header">
        <h3>Voucher batch creating</h3>
    </div>
    <div class="form-group">
        <div class="row">
            <div class="col-xs-1">
                Program name
            </div>
            <div class="col-xs-4">
                <asp:TextBox runat="server" ID="txtName" CssClass="form-control" placeholder="Program name"></asp:TextBox>
            </div>
        </div>
    </div>
    <div class="form-group">
        <div class="row">
            <div class="col-xs-1">
                Vouchers issue for
            </div>
            <div class="col-xs-4">
                <input type="text" name="txtAgency" id="ctl00_ctl00_AdminContentMain_AdminContent_agencySelectornameid" class="form-control"
                    readonly placeholder="Click to select agency" value="<%= GetAgencyName() %>" />
                <input id="agencySelector" type="hidden" runat="server" />
            </div>
        </div>
    </div>
    <div class="form-group">
        <div class="row">
            <div class="col-xs-1">
                Quantity
            </div>
            <div class="col-xs-4">
                <asp:TextBox runat="server" ID="txtVoucher" CssClass="form-control" placeholder="Quantity"></asp:TextBox>
            </div>
        </div>
    </div>
    <div class="form-group">
        <div class="row">
            <div class="col-xs-1">
                Apply for
            </div>
            <div class="col-xs-4">
                <asp:DropDownList runat="server" ID="ddlPersons" CssClass="form-control">
                    <asp:ListItem Text="1 person" Value="1"></asp:ListItem>
                    <asp:ListItem Text="2 people (1 cabin)" Value="2"></asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
    </div>
    <div class="form-group">
        <div class="row">
            <div class="col-xs-1">
                Cruise
            </div>
            <div class="col-xs-4">
                <asp:DropDownList runat="server" ID="ddlCruises" CssClass="form-control" />
            </div>
        </div>
    </div>
    <div class="form-group">
        <div class="row">
            <div class="col-xs-1">
                Trip
            </div>
            <div class="col-xs-4">
                <asp:DropDownList runat="server" ID="ddlTrips" CssClass="form-control" />
            </div>
        </div>
    </div>
    <div class="form-group">
        <div class="row">
            <div class="col-xs-1">
                Value (leave or input 0 to auto calculate)
            </div>
            <div class="col-xs-4">
                <asp:TextBox runat="server" ID="txtValue" placeholder="Value" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
    </div>
    <div class="form-group">
        <div class="row">
            <div class="col-xs-1">
                Valid until
            </div>
            <div class="col-xs-4">
                <asp:TextBox runat="server" ID="txtValidUntil" data-control="datetimepicker" placeholder="Valid until (dd/mm/yyyy)" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
    </div>
    <div class="form-group">
        <div class="row">
            <div class="col-xs-1">
                Issue date
            </div>
            <div class="col-xs-4">
                <asp:TextBox runat="server" ID="txtIssueDate" data-control="datetimepicker" CssClass="form-control" placeholder="Issue date (dd/mm/yyyy)"></asp:TextBox>
            </div>
        </div>
    </div>
    <div class="form-group">
        <div class="row">
            <div class="col-xs-1">
                Voucher template
            </div>
            <div class="col-xs-4">
                <asp:DropDownList runat="server" ID="ddlTemplates" CssClass="form-control" />
            </div>
        </div>
    </div>
    <div class="form-group">
        <div class="row">
            <div class="col-xs-1">
                Contract file (if any)
            </div>
            <div class="col-xs-4">
                <asp:HyperLink runat="server" ID="hplContract" Visible="false"></asp:HyperLink>
                <asp:FileUpload runat="server" ID="fileuploadContract" />
            </div>
        </div>
    </div>
    <div class="form-group">
        <div class="row">
            <div class="col-xs-1">
                Voucher code (after save):
            </div>
            <div class="col-xs-4">
                <asp:Repeater runat="server" ID="rptVouchers" OnItemDataBound="rptVouchers_ItemDataBound">
                    <ItemTemplate>
                        <asp:Literal runat="server" ID="litCode"></asp:Literal>;
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>
    <div class="form-group">
        <div class="row">
            <div class="col-xs-1">
                Note
            </div>
            <div class="col-xs-4">
                <asp:TextBox runat="server" TextMode="MultiLine" ID="txtNote" CssClass="form-control" placeholder="Note"></asp:TextBox>
            </div>
        </div>
    </div>
    <asp:Button ID="buttonSave" runat="server" OnClick="buttonSave_Click" CssClass="btn btn-primary"
        ValidationGroup="valid" />
    <asp:Button ID="buttonIssue" Text="Issue (Word)" runat="server" OnClick="buttonIssue_Click"
        CssClass="btn btn-primary" ValidationGroup="valid" />
    <asp:Button ID="buttonIssuePDF" Text="Issue (PDF)" runat="server" OnClick="buttonIssuePDF_Click"
        CssClass="btn btn-primary" ValidationGroup="valid" />
    <fieldset>
        <div class="settinglist">
        </div>
    </fieldset>
</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="Scripts" runat="server">
    <script>
        $("#ctl00_ctl00_AdminContentMain_AdminContent_agencySelectornameid").click(function () {
            var width = 800;
            var height = 600;
            window.open('/Modules/Sails/Admin/AgencySelectorPage.aspx?NodeId=1&SectionId=15&clientid=<%=agencySelector.ClientID%>', 'Agencyselect', 'width=' + width + ',height=' + height + ',top=' + ((screen.height / 2) - (height / 2)) + ',left=' + ((screen.width / 2) - (width / 2)));
        });
    </script>
</asp:Content>
