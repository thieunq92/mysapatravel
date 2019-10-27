<%@ Page Title="" Language="C#" MasterPageFile="MO.Master" AutoEventWireup="true" CodeBehind="IvProductExportList.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.IvProductExportList" %>

<%@ Register TagPrefix="svc" Namespace="CMS.ServerControls" Assembly="CMS.ServerControls" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="AdminContent" runat="server">
    <fieldset>
        <legend>DANH SÁCH PHIẾU XUẤT</legend>
        <div class="search_panel">
            <div class="form-group">
                <div class="row">
                    <div class="col-xs-1">
                        Kho            
                    </div>
                    <div class="col-xs-2">
                        <asp:DropDownList ID="ddlStorage" CssClass="form-control" runat="server"></asp:DropDownList>
                    </div>
                    <div class="col-xs-1">
                        Tên phiếu
                    </div>
                    <div class="col-xs-2">
                        <asp:TextBox ID="txtNameSearch" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-xs-1">
                        Mã phiếu                   
                    </div>
                    <div class="col-xs-2">
                        <asp:TextBox ID="txtCodeSearch" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="form-group">
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
                    <div class="col-xs-1">
                        Công nợ
                    </div>
                    <div class="col-xs-2">
                        <asp:DropDownList ID="ddlDebt" CssClass="form-control" runat="server">
                            <asp:ListItem Value="">Tất cả</asp:ListItem>
                            <asp:ListItem Value="0">Không tính công nợ</asp:ListItem>
                            <asp:ListItem Value="1">Chỉ tính công nợ</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="row">
                    <div class="col-xs-12">
                        <asp:Button ID="btnSearch" CssClass="btn btn-primary" runat="server" Text="Search" OnClick="btnSearch_Click" />
                        <asp:Button ID="btnAddNew" CssClass="btn btn-primary" runat="server" Text="Thêm phiếu xuất" OnClientClick="return addNew();" />
                    </div>
                </div>
            </div>
        </div>
        <div class="basicinfo">
            <div class="data_grid">
                <table class="table table-bordered table-hover">
                    <asp:Repeater ID="rptExportList" runat="server" OnItemCommand="rptExportList_ItemCommand"
                        OnItemDataBound="rptExportList_ItemDataBound">
                        <HeaderTemplate>
                            <tr class="item">
                                <th style="width: 5%">
                                    <asp:HyperLink ID="hplExportDate" runat="server" Text="Ngày xuất"></asp:HyperLink>
                                </th>
                                <th style="width: 15%">
                                    <asp:HyperLink ID="hplName" runat="server" Text="Tên phiếu xuất"></asp:HyperLink>
                                </th>
                                <th style="width: 10%">
                                    <asp:HyperLink ID="hplCode" runat="server" Text="Mã phiếu"></asp:HyperLink>
                                </th>
                                <th style="width: 5%">Phòng
                                </th>
                                <th style="width: 5%">Tên khách
                                </th>
                               

                                <th style="width: 5%">
                                    <asp:HyperLink ID="hplTotal" runat="server" Text="Tổng tiền"></asp:HyperLink>
                                </th>
                                <th style="width: 5%">Thực thu
                                </th>
                                <th style="width: 5%">Trạng thái
                                </th>
                               <%-- <th style="width: 8%">
                                    <asp:HyperLink ID="hplExportBy" runat="server" Text="Nguời xuất"></asp:HyperLink>
                                </th>
                                <th style="width: 8%">
                                    <asp:HyperLink ID="hplExportTo" runat="server" Text="Khách hàng"></asp:HyperLink>
                                </th>--%>
                                <th style="width: 5%">
                                    <asp:Label ID="lblAction" runat="server" Text="Thao tác"></asp:Label>
                                </th>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr class="item">
                                <td scope="row">
                                    <asp:Label ID="lblExportDate" runat="server"></asp:Label>
                                </td>
                                <td scope="row">
                                    <%--<asp:HyperLink ID="hplName" runat="server"></asp:HyperLink>--%>
                                    <a href="javascript:;" onclick='editExport(<%#Eval("Id") %>)'><%#Eval("Id") %>.<%#Eval("Name") %></a>
                                </td>
                                <td scope="row">
                                    <asp:Label ID="lblCode" runat="server"></asp:Label>
                                </td>
                                <td scope="row">
                                    <asp:Label ID="lblRoom" runat="server"></asp:Label>
                                </td>
                                <td scope="row">
                                    <asp:Label ID="lblCustomer" runat="server"></asp:Label>
                                </td>
                                <td scope="row">
                                    <asp:Label ID="lblTotal" runat="server"></asp:Label>
                                </td>
                                <td scope="row">
                                    <asp:Label ID="lblPay" runat="server"></asp:Label>
                                </td>
                                <td scope="row">
                                    <asp:Label ID="lblStatus" runat="server"></asp:Label>
                                </td>
                               <%-- <td scope="row">
                                    <asp:Label ID="lblExportBy" runat="server"></asp:Label>
                                </td>
                                <td scope="row">
                                    <asp:Label ID="lblExportTo" runat="server"></asp:Label>
                                </td>--%>
                                <td scope="row">
                                    <%--<asp:HyperLink runat="server" ID="hplEdit" ToolTip='Edit'><img class="image_button16" align="absmiddle" src="/Images/edit.gif" alt='delete'/></asp:HyperLink>--%>
                                    <a href="javascript:;" onclick='editExport(<%#Eval("Id") %>)'>
                                        <img class="image_button16" align="absmiddle" src="/Images/edit.gif" alt='delete' /></a>
                                    <asp:ImageButton runat="server" ID="btnDelete" Visible="False" ToolTip='Delete' ImageUrl="/Images/delete_file.gif"
                                        AlternateText='Delete' ImageAlign="AbsMiddle" CssClass="image_button16" CommandName="Delete"
                                        CommandArgument='<%# DataBinder.Eval(Container.DataItem,"Id") %>' OnClientClick="return confirm('Are you sure?');" />
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            <tr class="item">
                                <th>Tổng giá trị
                                </th>
                                <th></th>
                                <th></th>
                                <th>
                                    <asp:Label ID="lblsumTotal" runat="server"></asp:Label>
                                </th>

                            </tr>
                        </FooterTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </div>
        <div class="pager">
            <svc:pager id="pagerProduct" runat="server" pagerlinkmode="HyperLinkQueryString"
                hidewhenonepage="false" controltopage="rptExportList"></svc:pager>
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
            var src = "/Modules/Sails/Admin/IvExportAdd.aspx?NodeId=1&SectionId=15";
            $("#osModal iframe").attr('src', src);
            $("#osModal").modal();
            return false;
        }
        function editExport(id) {
            var src = "/Modules/Sails/Admin/IvExportAdd.aspx?NodeId=1&SectionId=15&ExportId=" + id;
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
