<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuotationManagement.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.QuotationManagement"
    MasterPageFile="MO-NoScriptManager.Master" Title="Quotation Management" %>

<asp:Content ID="AdminContent" ContentPlaceHolderID="AdminContent" runat="server">
    <h3 class="page-header">Quotation Management</h3>
    <div class="quotation-table">
        <div class="row">
            <div class="col-xs-12">
                <table class="table table-bordered table-hover table-common" style="text-align: center">
                    <tr class="active">
                        <th>Created date</th>
                        <th>Created by</th>
                        <th>Currency</th>
                        <th>Quotation</th>
                    </tr>
                    <asp:Repeater runat="server" ID="rptQuotation">
                        <ItemTemplate>
                            <tr>
                                <td><%# DataBinder.Eval(Container.DataItem,"CreatedDate","{0:dd/MM/yyyy}") %></td>
                                <td><%# Eval("CreatedBy.FullName")%></td>
                                <td><%# GetCurrency((int)Eval("Currency")) %></td>
                                <td><a href="QuotationView.aspx?NodeId=1&SectionId=15&qi=<%# Eval("Id") %>"><%# Eval("Name")%></i>
                                </a></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-12">
                <a href="QuotationCreate.aspx?NodeId=1&SectionId=15" class="btn btn-primary">Create Quotation</a>
            </div>
        </div>
    </div>
</asp:Content>
