<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="NewPopup.Master" CodeBehind="QQuotationIssue.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.QQuotationIssue" %>

<asp:Content ID="Content2" ContentPlaceHolderID="AdminContent" runat="server">
    <script>
        function RefreshParentPage() {
            window.parent.closePoup(1);
        }
        function closePoup() {
            window.parent.closePoup(0);
        }
    </script>

    <div class="form-group">
        <div class="row">
            <div class="col-xs-2 --no-padding-leftright">
                <label>Select quotation</label>
            </div>
            <div class="col-xs-6 --no-padding-leftright">
                <input type="text" name="txtAgency" id="<%= quotationSelector.ClientID %>nameid" class="form-control"
                    readonly placeholder="Click to select quotation" />
                <input id="quotationSelector" type="hidden" runat="server" />
            </div>
        </div>
    </div>
    <div class="form-group">
        <div class="row">
            <div class="col-xs-2 --no-padding-right">
                <label>Agent level</label>
            </div>
            <div class="col-xs-4 --no-padding-leftright">
                <asp:DropDownList runat="server" CssClass="form-control" ID="ddlAgentLevel">
                    <asp:ListItem Value="AL1" Text="Agent level 1"></asp:ListItem>
                    <asp:ListItem Value="AL2" Text="Agent level 2"></asp:ListItem>
                    <asp:ListItem Value="AL3" Text="Agent level 3"></asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
    </div>
    <div class="form-group">
        <div class="row">
            <div class="col-xs-2 --no-padding-right">
                <button type="button" class="btn btn-default" data-dismiss="modal" onclick="closePoup();">Close</button>
                <asp:Button runat="server" ID="btnIssue" CssClass="btn btn-primary" Text="Issue" OnClick="btnIssue_Click"></asp:Button>
            </div>
        </div>
    </div>
    <script>

        document.getElementById("<%=quotationSelector.ClientID%>nameid").addEventListener("click", function () {
            var width = 800;
            var height = 600;
            window.open('/Modules/Sails/Admin/QQuotationSelectorPage.aspx?NodeId=1&SectionId=15&clientid=<%= quotationSelector.ClientID%>', 'Quotationselect', 'width=' + width + ',height=' + height + ',top=' + ((screen.height / 2) - (height / 2)) + ',left=' + ((screen.width / 2) - (width / 2)));
        });
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
