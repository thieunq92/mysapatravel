<%@ Page Title="Inspection Report" Language="C#" MasterPageFile="MO.Master"
    AutoEventWireup="true" CodeBehind="InspectionReport.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.InspectionReport" %>

<%@ Register TagPrefix="ajax" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=1.0.20229.20821, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<%@ Register TagPrefix="svc" Namespace="CMS.ServerControls" Assembly="CMS.ServerControls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("#<%=txtFrom.ClientID%>").datepicker({
                dateFormat: "dd/mm/yy",
                showOn: "both",
                buttonImageOnly: true,
                buttonImage: "/images/calendar.gif",
                changeMonth: true,
                changeYear: true,
            });

            $("#<%=txtTo.ClientID%>").datepicker({
                dateFormat: "dd/mm/yy",
                showOn: "both",
                buttonImageOnly: true,
                buttonImage: "/images/calendar.gif",
                changeMonth: true,
                changeYear: true,
            });

            $(".imgViewNote").click(function () {
                var noteid = $(this).attr("noteid");
                $(".divViewNote").each(function (index, element) {
                    if ($(element).attr("noteid") == noteid) {
                        $(element).dialog({
                            autoOpen: false,
                            modal: true,
                            title: "Full note",
                            width: 500
                        });
                        $(element).dialog("open");
                    }
                }
                );
            });
        });
    </script>
    <style>
        #ui-datepicker-div {
            width: 197px;
            left: 50.3%;
            font-size: 10px;
        }

        .ui-datepicker-next:hover {
            left: 87%;
        }

        .ui-datepicker-next {
            cursor: pointer;
        }

        .ui-datepicker-prev {
            cursor: pointer;
        }

        .ui-datepicker-trigger {
            position: relative;
            top: 5px;
            width: 19px;
        }
    </style>
    <div class="page-header">
        <h3>Inspection report</h3>
    </div>
    <div class="form-group">
        <div class="row">
            <div class="col-xs-1">
                Start date from
            </div>
            <div class="col-xs-2">
                <asp:TextBox ID="txtFrom" runat="server" CssClass="form-control" data-control="datetimepicker" placeholder="From (dd/mm/yyyy)">
                </asp:TextBox>
            </div>
            <div class="col-xs-1">
                To
            </div>
            <div class="col-xs-2">
                <asp:TextBox ID="txtTo" runat="server" CssClass="form-control" data-control="datetimepicker" placeholder="To (dd/mm/yyyy)">
                </asp:TextBox>
            </div>
            <div class="col-xs-1">
                <asp:Button runat="server" ID="btnView" CssClass="btn btn-primary"
                    Text="Search"></asp:Button>
            </div>
        </div>
    </div>
    <br />
    <div class="row">
        <div class="col-xs-12">
            <table class="table table-bordered table-hover table-common">
                <asp:Repeater ID="rptBookingList" runat="server" OnItemDataBound="rptBookingList_ItemDataBound">
                    <HeaderTemplate>
                        <tr class="active">
                            <th>Booking code
                            </th>
                            <th>Trip
                            </th>
                            <th>Cruise
                            </th>
                            <th>Number of pax
                            </th>
                            <th id="columnCustomerName" runat="server">Customer name
                            </th>
                            <th>Partner 
                            </th>
                            <th>TA code
                            </th>
                            <th>Status
                            </th>
                            <th>Last edit
                            </th>
                            <th>Start date
                            </th>
                            <th>Action
                            </th>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr id="trItem" runat="server" class="item">
                            <td>
                                <asp:HyperLink ID="hplCode" runat="server"></asp:HyperLink>
                            </td>
                            <td>
                                <asp:HyperLink ID="hyperLink_Trip" runat="server"></asp:HyperLink>
                            </td>
                            <td>
                                <asp:HyperLink ID="hplCruise" runat="server"></asp:HyperLink>
                            </td>
                            <td>
                                <asp:Literal ID="litPax" runat="server"></asp:Literal>
                            </td>
                            <td id="columnCustomerName" runat="server">
                                <asp:Label ID="labelName" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:HyperLink ID="hplAgency" runat="server"></asp:HyperLink>
                            </td>
                            <td>
                                <asp:Literal ID="litAgencyCode" runat="server"></asp:Literal>
                            </td>
                            <td>
                                <asp:Label ID="label_Status" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="labelConfirmBy" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="label_startDate" runat="server"></asp:Label>
                            </td>
                            <td>
                                <a href="BookingView.aspx?NodeId=1&SectionId=15&bi=<%# Eval("Id") %>">
                                    <i class="fa fa-edit fa-lg" aria-hidden="true" data-toggle="tooltip" data-placement="top" title="Edit"></i>
                                </a>
                                <asp:PlaceHolder runat="server" ID="plhNote">
                                    <i class="fa fa-info-circle fa-lg" aria-hidden="true" data-toggle="tooltip" data-placement="top" title="<%#Eval("Note") %>"></i>
                                </asp:PlaceHolder>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
    </div>
    <fieldset>

        <div class="data_table">
            <div class="data_grid">
            </div>
            <div class="pager">
                <svc:Pager ID="pagerBookings" runat="server" HideWhenOnePage="true" ControlToPage="rptBookingList"
                    PagerLinkMode="HyperLinkQueryString" PageSize="20" />
            </div>
        </div>
    </fieldset>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#<%=txtFrom.ClientID%>").datepicker({
                dateFormat: "dd/mm/yy",
                showOn: "both",
                buttonImageOnly: true,
                buttonImage: "/images/calendar.gif",
                changeMonth: true,
                changeYear: true,
            });

            $("#<%=txtTo.ClientID%>").datepicker({
                dateFormat: "dd/mm/yy",
                showOn: "both",
                buttonImageOnly: true,
                buttonImage: "/images/calendar.gif",
                changeMonth: true,
                changeYear: true,
            });

            $(".imgViewNote").click(function () {
                var noteid = $(this).attr("noteid");
                $(".divViewNote").each(function (index, element) {
                    if ($(element).attr("noteid") == noteid) {
                        $(element).dialog({
                            autoOpen: false,
                            modal: true,
                            title: "Full note",
                            width: 500
                        });
                        $(element).dialog("open");
                    }
                }
                );
            });
        });
    </script>
    <style>
        #ui-datepicker-div {
            width: 197px;
            left: 50.3%;
            font-size: 10px;
        }

        .ui-datepicker-next:hover {
            left: 87%;
        }

        .ui-datepicker-next {
            cursor: pointer;
        }

        .ui-datepicker-prev {
            cursor: pointer;
        }

        .ui-datepicker-trigger {
            position: relative;
            top: 5px;
            width: 19px;
        }
    </style>
</asp:Content>
