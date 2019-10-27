<%@ Page Language="C#" MasterPageFile="MO.Master" AutoEventWireup="true"
    CodeBehind="FeedbackReport.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.FeedbackReport"
    Title="Feedback Report" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="CMS.ServerControls" Namespace="CMS.ServerControls" TagPrefix="svc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
    <div class="page-header">
        <h3>Feedback report</h3>
    </div>
    <div class="form-group">
        <div class="row">
            <div class="col-xs-1">
                Group
            </div>
            <div class="col-xs-3">
                <asp:DropDownList ID="ddlGroups" runat="server" CssClass="form-control">
                </asp:DropDownList>
            </div>
            <div class="col-xs-1">
                From
            </div>
            <div class="col-xs-2">
                <asp:TextBox ID="txtFrom" runat="server" CssClass="form-control" placeholder="From (dd/mm/yyyy)" data-control="datetimepicker"></asp:TextBox>
            </div>
            <div class="col-xs-1">
                To
            </div>
            <div class="col-xs-2">
                <asp:TextBox runat="server" ID="txtTo" CssClass="form-control" placeholder="To (dd/mm/yyyy)" data-control="datetimepicker"></asp:TextBox>
            </div>
        </div>
    </div>
    <div class="form-group">
        <div class="row">
            <div class="col-xs-1">
                Cruise
            </div>
            <div class="col-xs-3">
                <asp:DropDownList ID="ddlCruises" runat="server" CssClass="form-control">
                </asp:DropDownList>
            </div>
        </div>
    </div>
    <div class="form-group">
        <div class="row">
            <div class="col-xs-12">
                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click"
                    CssClass="btn btn-primary" />
                <asp:Button ID="btnExportAll" runat="server" Text="Export all" OnClick="btnExportAll_Click"
                    CssClass="btn btn-primary" />
            </div>
        </div>
    </div>
    <br />
    <div class="row">
        <div class="col-xs-12">
            <asp:Repeater ID="rptGroups" runat="server" OnItemDataBound="rptGroups_ItemDataBound">
                <ItemTemplate>
                    <asp:HyperLink ID="hplGroup" runat="server" CssClass="btn btn-default"></asp:HyperLink>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
    <br />
    <div class="row">
        <div class="col-xs-12">
            <table class="table table-bordered table-hover table-common">
                <tr class="active">
                    <th>Tiêu chí
                    </th>
                    <asp:Repeater ID="rptAnswers" runat="server">
                        <ItemTemplate>
                            <th colspan="2">
                                <%# DataBinder.Eval(Container, "DataItem") %>
                            </th>
                        </ItemTemplate>
                    </asp:Repeater>
                </tr>
                <asp:Repeater ID="rptQuestionsReport" runat="server" OnItemDataBound="rptQuestionsReport_ItemDataBound">
                    <ItemTemplate>
                        <tr>
                            <th>
                                <asp:Literal ID="litQuestion" runat="server"></asp:Literal>
                            </th>
                            <asp:Repeater ID="rptAnswerData" runat="server" OnItemDataBound="rptAnswerData_ItemDataBound">
                                <ItemTemplate>
                                    <td>
                                        <asp:Literal ID="litCount" runat="server"></asp:Literal>
                                    </td>
                                    <td>
                                        <asp:Literal ID="litPercentage" runat="server"></asp:Literal>
                                    </td>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
    </div>
    <br />
    <div class="row">
        <div class="col-xs-12">
            <table class="table table-bordered table-hover table-common">
                <tr class="active">
                    <th>No.
                    </th>
                    <th>Date
                    </th>
                    <th>Booking
                    </th>
                    <th>Name
                    </th>
                    <th>Cruise
                    </th>
                    <th>Room
                    </th>
                    <th>Trip
                    </th>
                    <th>Guide
                    </th>
                    <th>Driver
                    </th>
                    <th>Address
                    </th>
                    <th>Email
                    </th>
                    <asp:Repeater ID="rptQuestions" runat="server">
                        <ItemTemplate>
                            <th>
                                <%# DataBinder.Eval(Container.DataItem, "Name") %>
                            </th>
                        </ItemTemplate>
                    </asp:Repeater>
                    <th>Note
                    </th>
                    <th></th>
                </tr>
                <asp:Repeater ID="rptFeedback" runat="server" OnItemDataBound="rptFeedback_ItemDataBound">
                    <ItemTemplate>
                        <asp:Literal ID="trItem" runat="server" Text="<tr>"></asp:Literal>
                        <td>
                            <asp:Literal ID="litIndex" runat="server"></asp:Literal>
                        </td>
                        <td>
                            <asp:Literal ID="litDate" runat="server"></asp:Literal>
                        </td>
                        <td>
                            <asp:HyperLink ID="hplBooking" runat="server"></asp:HyperLink>
                        </td>
                        <td>
                            <asp:Literal ID="litName" runat="server"></asp:Literal>
                        </td>
                        <td>
                            <asp:HyperLink ID="hplCruise" runat="server"></asp:HyperLink>
                            <%--<asp:Literal ID="litCruise" runat="server"></asp:Literal>--%>
                        </td>
                        <td>
                            <asp:Literal ID="litRoom" runat="server"></asp:Literal>
                        </td>
                        <td>
                            <asp:Literal ID="litTrip" runat="server"></asp:Literal>
                        </td>
                        <td>
                            <asp:HyperLink ID="hplGuide" runat="server"></asp:HyperLink>
                        </td>
                        <td>
                            <asp:HyperLink ID="hplDriver" runat="server"></asp:HyperLink>
                        </td>
                        <td>
                            <asp:Literal ID="litAddress" runat="server"></asp:Literal>
                        </td>
                        <td>
                            <asp:Literal ID="litEmail" runat="server"></asp:Literal>
                        </td>
                        <asp:Repeater ID="rptOptions" runat="server" OnItemDataBound="rptOptions_ItemDataBound">
                            <ItemTemplate>
                                <td>
                                    <asp:Literal ID="litOption" runat="server"></asp:Literal>
                                </td>
                            </ItemTemplate>
                        </asp:Repeater>
                        <td>
                            <asp:Literal ID="litNote" runat="server"></asp:Literal>
                        </td>
                        <td>
                            <a id="anchorFeedback" runat="server" href="javascript:void(0)"><i class="fa fa-edit fa-lg" aria-hidden="true" data-toggle="tooltip" data-placement="top" title="" data-original-title="Edit"></i></a>
                            <a id="anchorEmail" runat="server" href="javascript:void(0)"><i class="fa fa-envelope-o fa-lg text-success" aria-hidden="true" data-toggle="tooltip" data-placement="top" title="" data-original-title="Email"></i></a>
                            <asp:LinkButton ID="lbtDelete" runat="server" OnClick="lbtDelete_Click" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "Id") %>'>
                                <i class="fa fa-window-close fa-lg text-danger" aria-hidden="true" title="" data-toggle="tooltip" data-placement="top" data-original-title="Delete"></i></asp:LinkButton>
                        </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
            <div class="pager">
                <svc:Pager ID="pagerFeedback" runat="server" ControlToPage="rptFeedback" PagerLinkMode="HyperLinkQueryString"
                    PageSize="30"></svc:Pager>
            </div>
        </div>
    </div>
    <fieldset>
        <style>
            .sent {
                background-color: #92D050;
            }
        </style>
        <svc:Popup runat="server" ID="popupManager">
        </svc:Popup>
    </fieldset>
</asp:Content>
