<%@ Page Language="C#" MasterPageFile="MO.Master" AutoEventWireup="true" ValidateRequest="false"
    CodeBehind="CruisesEdit.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.CruisesEdit" Title="Cruise Adding" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<%@ Register Assembly="CMS.ServerControls" Namespace="CMS.ServerControls.FileUpload"
    TagPrefix="svc" %>
<%@ Register Assembly="CMS.ServerControls" Namespace="CMS.ServerControls" TagPrefix="svc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
    <div class="page-header">
        <h3>Cruise adding</h3>
    </div>
    <div class="form-group">
        <div class="row">
            <div class="col-xs-1">
                Name
            </div>
            <div class="col-xs-2">
                <asp:TextBox ID="textBoxName" runat="server" CssClass="form-control" placeholder="Name"></asp:TextBox>
            </div>
            <div class="col-xs-1">
                Số hiệu tàu
            </div>
            <div class="col-xs-2">
                <asp:TextBox ID="txtCruiseCode" runat="server" CssClass="form-control" placeholder="Số hiệu tàu"></asp:TextBox>
            </div>
            <div class="col-xs-1">
                Cruise code
            </div>
            <div class="col-xs-2">
                <asp:TextBox ID="txtCode" runat="server" CssClass="form-control" placeholder="Cruise code"></asp:TextBox>
            </div>
            
        </div>
    </div>
    <div class="form-group">
        <div class="row">
            <div class="col-xs-1">
                Number of floors
            </div>
            <div class="col-xs-1">
                <asp:TextBox ID="txtFloor" runat="server" CssClass="form-control" placeholder="Number of floors"></asp:TextBox>
            </div>
            <div class="col-xs-1">
                <asp:HyperLink ID="hplRoomPlan" runat="server" Text="Current room plan" Visible="false"></asp:HyperLink>
                <asp:Literal ID="litRoomPlan" runat="server" Text="Upload room plan" Visible="true"></asp:Literal>
            </div>
            <div class="col-xs-2">
                <asp:FileUpload ID="fileRoomPlan" runat="server" />
            </div>
            <div class="col-xs-1">
                Group
            </div>
            <div class="col-xs-1">
                <asp:DropDownList ID="ddlGroup" runat="server" CssClass="form-control" Width="390px">
                </asp:DropDownList>          
            </div>
        </div>
    </div>
    <div class="form-group">
        <div class="row">
            <div class="col-xs-12">
                <asp:Repeater ID="rptTrips" runat="server" OnItemDataBound="rptTrips_ItemDataBound">
                    <ItemTemplate>
                        <div class="row">
                            <div class="col-xs-12">
                                <div class="checkbox">
                                    <label>
                                        <asp:CheckBox ID="chkTrip" runat="server" />
                                    </label>
                                    <asp:HiddenField ID="hiddenId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "Id") %>' />
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>
    <div class="form-group">
        <div class="row">
            <div class="col-xs-12">
                Description
            </div>
        </div>
        <div class="row">
            <div class="col-xs-12">
                <asp:TextBox runat="server" ID="txtDescription" placeholder="Description" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
    </div>
    <div class="form-group">
        <div class="row">
            <div class="col-xs-12">
                <asp:Button ID="buttonSave" runat="server" OnClick="buttonSave_Click" CssClass="btn btn-primary" />
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
