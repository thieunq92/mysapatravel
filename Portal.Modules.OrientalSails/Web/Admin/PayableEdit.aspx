<%@ Page Language="C#" MasterPageFile="SailsMaster.Master" AutoEventWireup="true"
    Codebehind="PayableEdit.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.PayableEdit"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
<script type="text/javascript">
    function Paid(costid, newid)
    {
        costcontrol = document.getElementById(costid);
        newcontrol = document.getElementById(newid);
        newcontrol.value = costcontrol.firstChild.data.replace(',','');
    }
</script>
    <fieldset>
        <legend>
            <img alt="Room" src="../Images/sails.gif" align="absMiddle" />
            Payables
        </legend>
        <div class="advancedinfo">
            <table>
                <tr>
                    <td>
                        <asp:Literal ID="litDate" runat="server"></asp:Literal>
                    </td>
                    <td>
                        Total cost</td>
                    <td>
                        Old payment</td>
                    <td>
                    </td>
                    <td>
                        Current payment</td>
                    <td>
                    <input type="button" id="btnAllPaid" runat="server" class="button" value="Paid All"/>
                    </td>
                </tr>
                <tr>
                    <td>
                        (Tiền xe)</td>
                    <td>
                        <asp:Label ID="lblTransferCost" runat="server"></asp:Label></td>
                    <td>
                        <asp:Label ID="lblTransferOld" runat="server"></asp:Label></td>
                    <td>
                        --></td>
                    <td>
                        <asp:TextBox ID="txtTransferNew" runat="server"></asp:TextBox></td>
                    <td>
                    <input type="button" id="btnTransferPaid" runat="server" class="button" value="Paid"/>
                    </td>
                </tr>
                <tr>
                    <td>
                        Ticket</td>
                    <td>
                        <asp:Label ID="lblTicketCost" runat="server"></asp:Label></td>
                    <td>
                        <asp:Label ID="lblTicketOld" runat="server"></asp:Label></td>
                    <td>
                        --></td>
                    <td>
                        <asp:TextBox ID="txtTicketNew" runat="server"></asp:TextBox></td>
                    <td>
                    <input type="button" id="btnTicketPaid" runat="server" class="button" value="Paid"/>
                    </td>
                </tr>
                <tr>
                    <td>
                        Guide</td>
                    <td>
                        <asp:Label ID="lblGuideCost" runat="server"></asp:Label></td>
                    <td>
                        <asp:Label ID="lblGuideOld" runat="server"></asp:Label></td>
                    <td>
                        --></td>
                    <td>
                        <asp:TextBox ID="txtGuideNew" runat="server"></asp:TextBox></td>
                    <td>
                    <input type="button" id="btnGuidePaid" runat="server" class="button" value="Paid"/>
                    </td>
                </tr>
                <tr>
                    <td>
                        (Tiền ăn)</td>
                    <td>
                        <asp:Label ID="lblMealCost" runat="server"></asp:Label></td>
                    <td>
                        <asp:Label ID="lblMealOld" runat="server"></asp:Label></td>
                    <td>
                        --></td>
                    <td>
                        <asp:TextBox ID="txtMealNew" runat="server"></asp:TextBox></td>
                    <td>
                    <input type="button" id="btnMealPaid" runat="server" class="button" value="Paid"/>
                    </td>
                </tr>
                <tr>
                    <td>
                        Kayaing</td>
                    <td>
                        <asp:Label ID="lblKayaingCost" runat="server"></asp:Label></td>
                    <td>
                        <asp:Label ID="lblKayaingOld" runat="server"></asp:Label></td>
                    <td>
                        --></td>
                    <td>
                        <asp:TextBox ID="txtKayaingNew" runat="server"></asp:TextBox></td>
                    <td>
                    <input type="button" id="btnKayaingPaid" runat="server" class="button" value="Paid"/>
                    </td>
                </tr>
                <tr>
                    <td>
                        (Hỗ trợ dịch vụ)
                    </td>
                    <td>
                        <asp:Label ID="lblServiceCost" runat="server"></asp:Label></td>
                    <td>
                        <asp:Label ID="lblServiceOld" runat="server"></asp:Label></td>
                    <td>
                        --></td>
                    <td>
                        <asp:TextBox ID="txtServiceNew" runat="server"></asp:TextBox></td>
                    <td>
                    <input type="button" id="btnServicePaid" runat="server" class="button" value="Paid"/>
                    </td>
                </tr>
                <tr>
                    <td>
                        (Tiền tàu)
                    </td>
                    <td>
                        <asp:Label ID="lblCruiseCost" runat="server"></asp:Label></td>
                    <td>
                        <asp:Label ID="lblCruiseOld" runat="server"></asp:Label></td>
                    <td>
                        --></td>
                    <td>
                        <asp:TextBox ID="txtCruiseNew" runat="server"></asp:TextBox></td>
                    <td>
                    <input type="button" id="btnCruisePaid" runat="server" class="button" value="Paid"/>
                    </td>
                </tr>
            </table>
        </div>
        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="button" />
        <input type="button" id="btnReturn" runat="server" class="button" value="Return"/>        
    </fieldset>
</asp:Content>
