<%@ Page Title="Voucher Batch Management" Language="C#" MasterPageFile="MO.Master" AutoEventWireup="true"
    CodeBehind="VoucherList.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.VoucherList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="CMS.ServerControls" Namespace="CMS.ServerControls" TagPrefix="svc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
    <div class="page-header">
        <h3>Voucher batch management</h3>
    </div>
    <div class="row">
        <div class="col-xs-12">
            <table class="table table-bordered table-hover table-common">
                <tr class="active">
                    <th>Name
                    </th>
                    <th>Agency
                    </th>
                    <th>Cruise
                    </th>
                    <th>Apply for
                    </th>
                    <th>Trip
                    </th>
                    <th>Total
                    </th>
                    <th>Remain
                    </th>
                    <th>Valid until
                    </th>
                    <th>Note
                    </th>
                    <th>Contract
                    </th>
                    <th></th>
                </tr>
                <asp:Repeater ID="rptVoucherList" runat="server" OnItemDataBound="rptVoucherList_ItemDataBound">
                    <ItemTemplate>
                        <tr <%#DateTime.Parse(Eval("ValidUntil").ToString()) < DateTime.Now ? "class='danger'":""%>>
                            <td>
                                <asp:HyperLink runat="server" ID="hplName"></asp:HyperLink>
                            </td>
                            <td>
                                <asp:Literal runat="server" ID="litAgency"></asp:Literal>
                            </td>
                            <td>
                                <%#Eval("Cruise.Name") %>
                            </td>
                            <td>
                                <%#Eval("NumberOfPerson")%>
                            </td>
                            <td>
                                <%#Eval("Trip.TripCode") %>
                            </td>
                            <td>
                                <asp:Literal runat="server" ID="litTotal"></asp:Literal>
                            </td>
                            <td>
                                <asp:Literal runat="server" ID="litRemain"></asp:Literal>
                                <asp:HyperLink runat="server" ID="hplBookings" Visible="False">(list bookings)</asp:HyperLink>
                            </td>
                            <td>
                                <asp:Literal runat="server" ID="litValidUntil"></asp:Literal>
                            </td>
                            <td>
                                <%#Eval("Note")%>
                            </td>
                            <td>
                                <asp:HyperLink runat="server" ID="hplContract"></asp:HyperLink>
                            </td>
                            <td>
                                <asp:HyperLink ID="hplEdit" runat="server">
                                        <i class="fa fa-edit fa-lg" aria-hidden="true" data-toggle="tooltip" data-placement="top" title="Edit"></i>                
                                </asp:HyperLink>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
            <div class="pager">
                <svc:Pager ID="pagerVoucher" runat="server" HideWhenOnePage="true" ControlToPage="rptVoucherList"
                    PagerLinkMode="HyperLinkQueryString" PageSize="20" />
            </div>
        </div>
    </div>
</asp:Content>
