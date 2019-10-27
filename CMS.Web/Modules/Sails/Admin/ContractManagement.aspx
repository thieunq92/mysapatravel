<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ContractManagement.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.ContractManagement"
    MasterPageFile="MO-NoScriptManager.Master" Title="Contract Management" %>

<asp:Content ID="AdminContent" ContentPlaceHolderID="AdminContent" runat="server">
    <h3 class="page-header">Contract Management</h3>
    <div class="quotation-table">
        <div class="row">
            <div class="col-xs-12">
                <table class="table table-bordered table-hover table-common" style="text-align: center">
                    <tr class="active">
                        <th>Created date</th>
                        <th>Created by</th>
                        <th>Currency</th>
                        <th>Contract</th>
                    </tr>
                    <asp:Repeater runat="server" ID="rptContract">
                        <ItemTemplate>
                            <tr>
                                <td><%# DataBinder.Eval(Container.DataItem,"CreatedDate","{0:dd/MM/yyyy}") %></td>
                                <td><%# Eval("CreatedBy.FullName")%></td>
                                <td><%# GetCurrency((int)Eval("Currency")) %></td>
                                <td><a href="ContractView.aspx?NodeId=1&SectionId=15&ci=<%# Eval("Id") %>"><%# Eval("Name")%></i>
                                </a></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-12">
                <a href="ContractCreate.aspx?NodeId=1&SectionId=15" class="btn btn-primary">Create Contract</a>
            </div>
        </div>
    </div>
</asp:Content>
