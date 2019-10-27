<%@ Page Title="" Language="C#" MasterPageFile="MO.Master" AutoEventWireup="true" CodeBehind="IvImportReport.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.IvImportReport" %>

<%@ Register Assembly="CMS.ServerControls" Namespace="CMS.ServerControls" TagPrefix="svc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content2" ContentPlaceHolderID="AdminContent" runat="server">
    <fieldset>
        <legend>BÁO CÁO CHI PHÍ NHẬP</legend>
        <div class="group">
            <%--<div class="form-group">
                <div class="row">
                    <div class="col-xs-1">
                        Tàu
                    </div>
                    <div class="col-xs-2">
                        <asp:DropDownList ID="ddlCruise" AutoPostBack="True" CssClass="form-control" runat="server"></asp:DropDownList>
                    </div>
                    <div class="col-xs-1">
                        Kho
                    </div>
                    <div class="col-xs-2">
                        <asp:DropDownList ID="ddlStorage" CssClass="form-control" runat="server"></asp:DropDownList>
                    </div>
                </div>
            </div>--%>
            <div class="form-group">
                <div class="row">

                    <div class="col-xs-1">
                        Lọc theo ngày                 
                    </div>

                    <div class="col-xs-1">
                        Từ Ngày                   
                    </div>
                    <div class="col-xs-2">
                        <asp:TextBox ID="txtFromDay" autocomplete="off" data-control="datetimepicker" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-xs-1">
                        Đến ngày
                    </div>
                    <div class="col-xs-2">
                        <asp:TextBox ID="txtToDay" autocomplete="off" data-control="datetimepicker" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="row">
                    <div class="col-xs-1">
                        Lọc theo tháng                 
                    </div>

                    <div class="col-xs-1">
                        Tháng                  
                    </div>
                    <div class="col-xs-2">
                        <asp:DropDownList ID="drpMonth" CssClass="form-control" runat="server">
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem Value="1">Th&#225;ng 1</asp:ListItem>
                            <asp:ListItem Value="2">Th&#225;ng 2</asp:ListItem>
                            <asp:ListItem Value="3">Th&#225;ng 3</asp:ListItem>
                            <asp:ListItem Value="4">Th&#225;ng 4</asp:ListItem>
                            <asp:ListItem Value="5">Th&#225;ng 5</asp:ListItem>
                            <asp:ListItem Value="6">Th&#225;ng 6</asp:ListItem>
                            <asp:ListItem Value="7">Th&#225;ng 7</asp:ListItem>
                            <asp:ListItem Value="8">Th&#225;ng 8</asp:ListItem>
                            <asp:ListItem Value="9">Th&#225;ng 9</asp:ListItem>
                            <asp:ListItem Value="10">Th&#225;ng 10</asp:ListItem>
                            <asp:ListItem Value="11">Th&#225;ng 11</asp:ListItem>
                            <asp:ListItem Value="12">Th&#225;ng 12</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-xs-1">
                        Năm
                    </div>
                    <div class="col-xs-2">
                        <asp:DropDownList ID="drpMonthOfYear" CssClass="form-control" runat="server">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="row">
                    <div class="col-xs-1">
                        Lọc theo năm                 
                    </div>
                    <div class="col-xs-1">
                        Năm
                    </div>
                    <div class="col-xs-2">
                        <asp:DropDownList ID="drpYear" CssClass="form-control" runat="server">
                        </asp:DropDownList>
                    </div>

                </div>
            </div>
            <div class="form-group">

                <div class="row">
                    <div class="col-xs-12">
                        <asp:Button ID="btnSearch" CssClass="btn btn-primary" runat="server" Text="Search" OnClick="btnSearch_Click" />
                        <asp:Button ID="btnExportExcel" Visible="False" CssClass="btn btn-primary" runat="server" Text="Export To Excel File" OnClick="btnExportExcel_Click" />

                    </div>
                </div>
            </div>
        </div>
        <div>
        </div>
        <h4></h4>
        <div class="basicinfo">
            <div class="data_grid">
                <table class="table table-bordered table-hover">
                    <asp:Repeater ID="rptProductList" runat="server" OnItemDataBound="rptProductList_ItemDataBound">
                        <HeaderTemplate>
                            <tr class="item">
                                <th style="width: 8%">
                                    <asp:Label ID="lbllName" runat="server" Text="Thời gian"></asp:Label>
                                </th>
                                <th style="width: 8%">Tổng tiền
                                </th>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr class="item">

                                <td scope="row">
                                    <asp:Label ID="lblDate" runat="server"></asp:Label>
                                </td>
                                <td scope="row">
                                    <asp:Label ID="lblTotal" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            <tr class="item">
                                <th>Tổng giá trị
                                </th>
                                <th>
                                    <asp:Label ID="lblSumTotal" runat="server"></asp:Label>
                                </th>
                            </tr>
                        </FooterTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </div>
        <div class="pager">
        </div>
    </fieldset>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
    <script>
        var filterByDate = function () {
            let txtFromDay = document.getElementById('<%=txtFromDay.ClientID%>').value;
            let txtToDay = document.getElementById('<%=txtToDay.ClientID%>').value;
            if (txtFromDay.length > 0 || txtToDay.length > 0) {
                document.getElementById('<%=drpMonth.ClientID%>').value = "";
                document.getElementById('<%=drpMonthOfYear.ClientID%>').value = "";
                document.getElementById('<%=drpYear.ClientID%>').value = "";
            }
        };
        var setEmptyDateAndYear = function () {
            document.getElementById('<%=txtFromDay.ClientID%>').value = '';
            document.getElementById('<%=txtToDay.ClientID%>').value = '';
            document.getElementById('<%=drpYear.ClientID%>').value = "";
        };
        var setEmptyDateAndMonth = function () {
            document.getElementById('<%=txtFromDay.ClientID%>').value = '';
            document.getElementById('<%=txtToDay.ClientID%>').value = '';
            document.getElementById('<%=drpMonth.ClientID%>').value = "";
            document.getElementById('<%=drpMonthOfYear.ClientID%>').value = "";
        };
        $(document).ready(function () {

            document.getElementById('<%=txtFromDay.ClientID%>').addEventListener("keyup", filterByDate());
            document.getElementById('<%=txtToDay.ClientID%>').addEventListener("keyup", filterByDate());
            document.getElementById('<%=drpMonth.ClientID%>').onchange = function () {
                setEmptyDateAndYear();
            }
            document.getElementById('<%=drpMonthOfYear.ClientID%>').onchange = function () {
                setEmptyDateAndYear();
            }
            document.getElementById('<%=drpYear.ClientID%>').onchange = function () {
                setEmptyDateAndMonth();
            }
        });
    </script>
</asp:Content>
