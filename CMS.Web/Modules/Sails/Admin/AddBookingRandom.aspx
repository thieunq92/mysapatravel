<%@ Page Language="C#" MasterPageFile="MO.Master" AutoEventWireup="true"
    CodeBehind="AddBookingRandom.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.AddBookingRandom" Title="Booking Adding Random" %>

<%@ Register Assembly="CMS.ServerControls" Namespace="CMS.ServerControls" TagPrefix="svc" %>
<%@ Register Assembly="Portal.Modules.OrientalSails" Namespace="Portal.Modules.OrientalSails.Web.Controls" TagPrefix="orc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Head" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="AdminContent" ContentPlaceHolderID="AdminContent" runat="server">
    <div class="row">
        <div class="col-xs-12">
            <table class="table table-borderless table-common">
                <tr>
                    <asp:PlaceHolder ID="plhTrip" runat="server">
                        <td class="--text-left" style="width: 10%">Trip
                        </td>
                        <td class="--text-left" style="width: 40%">
                            <asp:DropDownList runat="server" ID="ddlTrips" CssClass="form-control" Style="width: auto; display: inline-block" />
                            <asp:DropDownList ID="ddlOptions" runat="server" Style="display: none;">
                                <asp:ListItem>Option 1</asp:ListItem>
                                <asp:ListItem>Option 2</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </asp:PlaceHolder>
                    <td class="--text-left" style="width: 10%">Start date*</td>
                    <td>
                        <asp:TextBox ID="txtDate" runat="server" CssClass="form-control"
                            data-control="datetimepicker" placeholder="Start date(dd/mm/yyyy)" Style="width: 60%"></asp:TextBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="--text-left">Number of pax</td>
                    <td colspan="3" class="--text-left">
                        <asp:DropDownList ID="ddlAdult" runat="server" CssClass="form-control" Style="width: auto; display: inline-block; padding-left: 10px"></asp:DropDownList>
                        <asp:DropDownList ID="ddlChild" runat="server" CssClass="form-control" Style="width: auto; display: inline-block; margin-left: 1px"></asp:DropDownList>
                        <asp:DropDownList ID="ddlBaby" runat="server" CssClass="form-control" Style="width: auto; display: inline-block; margin-left: 1px"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="--text-left">Agency
                    </td>
                    <td class="--text-left">
                        <orc:AgencySelector ID="agencySelector" runat="server" CssClass="form-control" />
                        <asp:TextBox ID="txtAgencyCode" runat="server" CssClass="form-control" placeholder="TA code" Style="width: auto; display: inline-block"></asp:TextBox>
                    </td>
                    <td colspan="2" class="--text-left">
                        <asp:Repeater ID="rptExtraServices" runat="server" OnItemDataBound="rptExtraServices_ItemDataBound">
                            <ItemTemplate>
                                <div class="checkbox" style="margin-left: 0">
                                    <label>
                                        <input id="chkService" runat="server" type="checkbox" /><%#Eval("Name") %></label>
                                </div>
                                <asp:HiddenField ID="hiddenId" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "Id") %>' />
                                <asp:HiddenField ID="hiddenValue" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "Price") %>' />
                            </ItemTemplate>
                        </asp:Repeater>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div class="row">
        <div class="col-xs-12">
            <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" CssClass="btn btn-primary" />
        </div>
    </div>
</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="Scripts" runat="server">
    <script type="text/javascript">
        function toggleVisible(id) {
            item = document.getElementById(id);
            if (item.style.display == "") {
                item.style.display = "none";
            }
            else {
                item.style.display = "";
            }
        }

        function setVisible(id, visible) {
            control = document.getElementById(id);
            if (visible)
            { control.style.display = ""; }
            else
            {
                control.style.display = "none";
            }

        }

        function ddltype_changed(id, optionid, vids) {
            ddltype = document.getElementById(id);
            if (vids.indexOf('#' + ddltype.options[ddltype.selectedIndex].value + '#') >= 0) {
                setVisible(optionid, true);
            }
            else {
                setVisible(optionid, false);
            }
        }

        function ddlagency_changed(id, codeid) {
            ddltype = document.getElementById(id);
            switch (ddltype.selectedIndex) {
                case 0:
                    setVisible(codeid, false);
                    break;
                default:
                    setVisible(codeid, true);
                    break;
            }
        }
    </script>
</asp:Content>
