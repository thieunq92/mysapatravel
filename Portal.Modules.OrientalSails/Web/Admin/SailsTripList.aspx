<%@ Page Language="C#" MasterPageFile="MO.Master" AutoEventWireup="true" CodeBehind="SailsTripList.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.SailsTripList" Title="Trip Management" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
    <div class="page-header">
        <h3>
            <asp:Label ID="titleSailsTripList" runat="server"></asp:Label></h3>
    </div>
    <div class="row">
        <div class="col-xs-12">
            <table class="table table-bordered table-hover table-common">
                <asp:Repeater ID="rptTripList" runat="server" OnItemDataBound="rptTripList_ItemDataBound" OnItemCommand="rptTripList_ItemCommand">
                    <headertemplate>
                            <tr class="active">
                                <th>
                                    Name
                                </th>
                                <th>
                                    Number of day
                                </th>
                                <th>
                                    Number of option
                                </th>
                                <th>
                                    Price
                                </th>
                                <th>                                 
                                </th>
                            </tr>
                        </headertemplate>
                    <itemtemplate>
                            <tr class="item">
                                <td>
                                    <asp:HyperLink ID="hyperLink_Name" runat="server"></asp:HyperLink>                                
                                </td>
                                <td>
                                    <asp:Label ID="label_NumberOfDays" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="label_NumberofOption" runat="server"></asp:Label>
                                </td>      
                                <td>
                                    <table style="width:auto;">                                    
                                    <asp:Repeater ID="rptOptions" runat="server" OnItemDataBound="rptOptions_ItemDataBound">
                                        <ItemTemplate>
                                            <tr>
                                                <td><asp:Literal ID="litOption" runat="server"></asp:Literal></td>
                                                <asp:Repeater ID="rptCruises" runat="server" OnItemDataBound="rptCruises_ItemDataBound">
                                                    <ItemTemplate>
                                                        <td><asp:HyperLink ID="hplCruise" runat="server"></asp:HyperLink></td>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    </table>
                                    <!--<asp:DropDownList ID="ddlOption" runat="server"></asp:DropDownList>
                                    <asp:ImageButton ID="imageButtonPrice" runat="server" ToolTip="Price" AlternateText="Price" ImageAlign="AbsMiddle" CssClass="image_button16" CommandName="Price" ImageUrl="../Images/price.gif" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"Id") %>'/>-->
                                </td>                          
                                <td>
                                    <asp:HyperLink ID="hyperLinkEdit" runat="server">
                                        <i class="fa fa-edit fa-lg" aria-hidden="true" data-toggle="tooltip" data-placement="top" title="" data-original-title="Edit"></i>
                                    </asp:HyperLink>
                                    <asp:LinkButton runat="server" ID="imageButtonDelete" CommandName="Delete" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"Id") %>' OnClientClick="javascript: return confirm('Are you sure?')">
                                        <i class="fa fa-window-close fa-lg text-danger" aria-hidden="true" title="" data-toggle="tooltip" data-placement="top" data-original-title="Delete"></i>
                                    </asp:LinkButton>       
                                </td>
                            </tr>
                        </itemtemplate>
                </asp:Repeater>
            </table>
        </div>
    </div>
</asp:Content>
