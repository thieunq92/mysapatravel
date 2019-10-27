<%@ Page Language="C#" MasterPageFile="MO.Master" AutoEventWireup="true"
    CodeBehind="FeedbackResponse.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.FeedbackResponse" Title="Feedback Response" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="CMS.ServerControls" Namespace="CMS.ServerControls" TagPrefix="svc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
    <div class="page-header">
        <h3>Feedback response
        </h3>
    </div>
    <div class="form-group">
        <div class="row">
            <div class="col-xs-1">
                Date
            </div>
            <div class="col-xs-2">
                <asp:TextBox ID="txtDate" runat="server" CssClass="form-control" data-control="datetimepicker" placeholder="Date (dd/mm/yyyy)"></asp:TextBox>
            </div>
            <div class="col-xs-1">
                <asp:Button runat="server" ID="btnSelect" CssClass="btn btn-primary" Text="Select" OnClick="btnSelect_Click" />
            </div>
        </div>
    </div>
    <asp:PlaceHolder runat="server" ID="plhSummary" Visible="false">
        <div class="form-group">
            <div class="row">
                <div class="col-xs-2">
                    Number of booking:<asp:Literal runat="server" ID="litNumberOfBookings"></asp:Literal>
                </div>
                <div class="col-xs-2">
                    Number of feedback:<asp:Literal runat="server" ID="litNumberOfFeedback"></asp:Literal>
                </div>
            </div>
        </div>
    </asp:PlaceHolder>
    <br />
    <div class="row">
        <div class="col-xs-12">
            <table class="table table-bordered table-hover table-common">
                <tr>
                    <td>Booking code</td>
                    <td>Customer</td>
                    <td>Overall</td>
                    <td>Status</td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <asp:Repeater runat="server" ID="rptFeedbacks" OnItemDataBound="rptFeedbacks_ItemDataBound">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:Literal runat="server" ID="litCode"></asp:Literal><asp:HyperLink runat="server" ID="hplBookingCode"></asp:HyperLink></td>
                            <td>
                                <asp:Literal runat="server" ID="litName"></asp:Literal></td>
                            <td>
                                <asp:Literal runat="server" ID="litOverall"></asp:Literal></td>
                            <td>
                                <asp:Literal runat="server" ID="litStatus"></asp:Literal></td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlSend" CssClass="form-control">
                                    <asp:ListItem Value="1">Send or resend</asp:ListItem>
                                    <asp:ListItem Value="0">Don't send</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlTemplates" CssClass="form-control"></asp:DropDownList>
                            </td>
                            <td>
                                <asp:Button runat="server" ID="btnSend" OnClick="btnSend_Click" Text="Send" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' CssClass="btn btn-primary" /></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
    </div>
    <div class="row">
        <div class="col-xs-12">
            <asp:Button runat="server" ID="btnSendAll" Text="Send All" OnClick="btnSendAll_Click" CssClass="btn btn-primary" />
        </div>
    </div>
</asp:Content>
