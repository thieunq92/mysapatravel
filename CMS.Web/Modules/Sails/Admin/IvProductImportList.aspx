<%@ Page Title="" Language="C#" MasterPageFile="MO.Master" AutoEventWireup="true" CodeBehind="IvProductImportList.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.IvProductImportList" %>

<%@ Register Assembly="CMS.ServerControls" Namespace="CMS.ServerControls" TagPrefix="svc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="AdminContent" runat="server">
    <fieldset>
        <legend>QUẢN LÝ PHIẾU NHẬP</legend>
        <div class="search_panel">
            <div class="form-group">
                <div class="row">
                    <asp:UpdatePanel ID="upPanael" runat="server">
                        <ContentTemplate>
                            <div class="col-xs-1">
                                Tàu
                            </div>
                            <div class="col-xs-2">
                                <asp:DropDownList ID="ddlCruise" OnSelectedIndexChanged="ddlCRuise_OnSelectedIndexChanged" AutoPostBack="True" CssClass="form-control" runat="server"></asp:DropDownList>
                            </div>
                            <div class="col-xs-1">
                                Kho
                            </div>
                            <div class="col-xs-2">
                                <asp:DropDownList ID="ddlStorage" CssClass="form-control" runat="server"></asp:DropDownList>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="col-xs-1">
                        Tên phiếu
                    </div>
                    <div class="col-xs-2">
                        <asp:TextBox ID="txtNameSearch" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>

                </div>
            </div>
            <div class="form-group">
                <div class="col-xs-1">
                    Mã phiếu                   
                </div>
                <div class="col-xs-2">
                    <asp:TextBox ID="txtCodeSearch" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="row">
                    <div class="col-xs-1">
                        Từ ngày            
                    </div>
                    <div class="col-xs-2">
                        <asp:TextBox ID="txtFromDate" autocomplete="off" data-control="datetimepicker" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-xs-1">
                        Đến ngày
                    </div>
                    <div class="col-xs-2">
                        <asp:TextBox ID="txtToDate" autocomplete="off" data-control="datetimepicker" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="form-group">

                <div class="row">
                    <div class="col-xs-12">
                        <asp:Button ID="btnSearch" CssClass="btn btn-primary" runat="server" Text="Search" OnClick="btnSearch_Click" />
                        <asp:Button ID="btnAddNew" CssClass="btn btn-primary" runat="server" Text="Thêm phiếu nhập" OnClientClick="return addNew();" />
                    </div>
                </div>
            </div>
        </div>

        <div class="basicinfo">
            <div class="data_grid">
                <table class="table table-bordered table-hover">
                    <asp:Repeater ID="rptImportList" runat="server" OnItemCommand="rptImportList_ItemCommand"
                        OnItemDataBound="rptImportList_ItemDataBound">
                        <HeaderTemplate>
                            <tr class="item">
                                <th style="width: 15%">
                                    <asp:HyperLink ID="hplName" runat="server" Text="Tên phiếu nhập"></asp:HyperLink>
                                </th>
                                <th style="">Kho                                </th>
                                <th style="width: 10%">
                                    <asp:HyperLink ID="hplCode" runat="server" Text="Mã phiếu"></asp:HyperLink>
                                </th>
                                <th style="width: 8%">
                                    <asp:HyperLink ID="hplImportDate" runat="server" Text="Ngày nhập"></asp:HyperLink>
                                </th>
                                <th style="width: 8%">
                                    <asp:HyperLink ID="hplImportedBy" runat="server" Text="Nguời nhập"></asp:HyperLink>
                                </th>
                                <th style="width: 8%">
                                    <asp:HyperLink ID="hplTotal" runat="server" Text="Giá trị"></asp:HyperLink>
                                </th>
                                <th style="width: 8%">
                                    <asp:HyperLink ID="hplImportFrom" runat="server" Text="Nhà cung cấp"></asp:HyperLink>
                                </th>
                                <th style="width: 5%">
                                    <asp:Label ID="lblAction" runat="server" Text="Thao tác"></asp:Label>
                                </th>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr class="item">
                                <td scope="row">
                                    <%--<asp:HyperLink ID="hplName" runat="server"></asp:HyperLink>--%>
                                    <a href="javascript:;" onclick='editImport(<%#Eval("Id") %>)'><%#Eval("Name") %></a>
                                </td>
                                <td>
                                    <asp:Label ID="lblStorage" runat="server" Text="Kho"></asp:Label>
                                </td>
                                <td scope="row">
                                    <asp:Label ID="lblCode" runat="server"></asp:Label>
                                </td>
                                <td scope="row">
                                    <asp:Label ID="lblImportDate" runat="server"></asp:Label>
                                </td>
                                <td scope="row">
                                    <asp:Label ID="lblImportedBy" runat="server"></asp:Label>
                                </td>
                                <td scope="row">
                                    <asp:Label ID="lblTotal" runat="server"></asp:Label>
                                </td>
                                <td scope="row">
                                    <asp:Label ID="lblSupp" runat="server"></asp:Label>
                                </td>
                                <td scope="row">
                                    <%--<asp:HyperLink runat="server" ID="hplEdit"  ToolTip='Edit'><img class="image_button16" align="absmiddle" src="/Images/edit.gif" alt='delete'/></asp:HyperLink>--%>
                                    <a href="javascript:;" onclick='editImport(<%#Eval("Id") %>)'>
                                        <img class="image_button16" align="absmiddle" src="/Images/edit.gif" alt='delete' /></a>
                                    <asp:ImageButton runat="server" ID="btnDelete" ToolTip='Delete' ImageUrl="/Images/delete_file.gif"
                                        AlternateText='Delete' ImageAlign="AbsMiddle" CssClass="image_button16" CommandName="Delete"
                                        CommandArgument='<%# DataBinder.Eval(Container.DataItem,"Id") %>' OnClientClick="return confirm('Are you sure?');" />
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            <tr class="item">
                                <td>Tổng giá trị
                                </td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td>
                                    <asp:Label ID="lblTotalMonth" runat="server"></asp:Label>
                                </td>
                                <td></td>
                            </tr>
                        </FooterTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </div>
        <div class="pager">
            <svc:Pager ID="pagerProduct" runat="server" PagerLinkMode="HyperLinkQueryString"
                HideWhenOnePage="false" ControlToPage="rptImportList"></svc:Pager>
        </div>
    </fieldset>
    <div class="modal fade" id="osModal" tabindex="-1" role="dialog" aria-labelledby="gridSystemModalLabel" data-backdrop="static" data-keyboard="true">
        <div class="modal-dialog" role="document" style="width: 85vw; height: 80vh">
            <div class="modal-content">
                <div class="modal-header">
                    <%--                    <span>Add booking</span>--%>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                </div>
                <div class="modal-body">
                    <iframe frameborder="0" width="100%" style="height: 80vh" scrolling="yes"></iframe>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
    <script>
        function addNew() {
            var src = "/Modules/Sails/Admin/IvImportAdd.aspx?NodeId=1&SectionId=15";
            $("#osModal iframe").attr('src', src);
            $("#osModal").modal();
            return false;
        }
        function editImport(id) {
            var src = "/Modules/Sails/Admin/IvImportAdd.aspx?NodeId=1&SectionId=15&ImportId=" + id;
            $("#osModal iframe").attr('src', src);
            $("#osModal").modal();
            return false;
        }
        function closePoup(refesh) {
            $("#osModal").modal('hide');
            if (refesh === 1) {
                window.location.href = window.location.href;
            }
        }
    </script>

</asp:Content>
