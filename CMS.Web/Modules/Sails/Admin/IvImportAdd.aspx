<%@ Page Title="" Language="C#" MasterPageFile="NewPopup.Master" AutoEventWireup="true" CodeBehind="IvImportAdd.aspx.cs" EnableEventValidation="false"
    Inherits="Portal.Modules.OrientalSails.Web.Admin.IvImportAdd" %>

<%@ Register Assembly="CMS.ServerControls" Namespace="CMS.ServerControls" TagPrefix="svc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content2" ContentPlaceHolderID="AdminContent" runat="server">

    <fieldset>
        <legend>THÔNG TIN PHIẾU NHẬP</legend>
        <div class="basicinfo">
            <div class="form-group">
                <div class="row">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="col-xs-1">
                                Tàu
                            </div>
                            <div class="col-xs-3">
                                <asp:DropDownList ID="ddlCruise" OnSelectedIndexChanged="ddlCRuise_OnSelectedIndexChanged" AutoPostBack="True" CssClass="form-control" runat="server"></asp:DropDownList>
                            </div>
                            <div class="col-xs-1">
                                Kho
                            </div>
                            <div class="col-xs-3">
                                <asp:DropDownList ID="ddlStorage" CssClass="form-control" runat="server"></asp:DropDownList>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="form-group">
                <div class="row">

                    <div class="col-xs-1">
                        Tên phiếu (*)
                    </div>
                    <div class="col-xs-3">
                        <asp:TextBox ID="txtName" runat="server" CssClass="form-control" EnableViewState="false"></asp:TextBox>
                        <%--<asp:RequiredFieldValidator ID="requiredFieldValidator1" runat="server" ControlToValidate="txtName"
                            Display="Dynamic" ErrorMessage="Phải có tên phiếu"></asp:RequiredFieldValidator>--%>
                    </div>
                    <div class="col-xs-1">
                        Mã phiếu                   
                    </div>
                    <div class="col-xs-3">
                        <asp:TextBox ID="txtCode" runat="server" CssClass="form-control" EnableViewState="false"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="row">
                    <div class="col-xs-1">
                        Ngày nhập            
                    </div>
                    <div class="col-xs-3">
                        <asp:TextBox ID="txtImportedDate" Enabled="False" CssClass="form-control" runat="server" EnableViewState="false"></asp:TextBox>

                    </div>
                    <div class="col-xs-1">
                        Nguời nhập                   
                    </div>
                    <div class="col-xs-3">
                        <asp:TextBox ID="txtImportBy" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-xs-3">
                        <ajax:CalendarExtender ID="calendarExtender1" runat="server" TargetControlID="txtImportedDate"
                            Format="dd/MM/yyyy">
                        </ajax:CalendarExtender>
                        <svc:NumberValidator ID="numberValidator1" runat="server" ControlToValidate="txtImportedDate"
                            NumberType="Date" Display="Dynamic" ErrorMessage="Không đúng định dạng ngày tháng">
                        </svc:NumberValidator>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="row">
                    <div class="col-xs-1">
                        Tổng giá trị            
                    </div>
                    <div class="col-xs-3">
                        <asp:TextBox ID="txtTotal1" runat="server" CssClass="form-control" ReadOnly="true" EnableViewState="false"></asp:TextBox>

                    </div>
                    <div class="col-xs-1">
                        Nhập từ                   
                    </div>
                    <div class="col-xs-3">
                        <asp:DropDownList ID="ddlAgency" CssClass="form-control" runat="server"></asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="row">
                    <div class="col-xs-1">
                        Ghi chú              
                    </div>
                    <div class="col-xs-6">
                        <asp:TextBox ID="txtDetail" runat="server" CssClass="form-control" TextMode="MultiLine" Width="60%" Height="150px"
                            EnableViewState="false"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>
        <h4>
            <asp:Label ID="lblProductImport" runat="server" Text="Danh sách sản phẩm"></asp:Label>
        </h4>
        <asp:UpdatePanel ID="upPanael" runat="server">
            <ContentTemplate>
                <div class="form-group">
                    <asp:Button ID="btnAddProduct" runat="server" Text="Thêm sản phẩm" CssClass="btn btn-primary" OnClick="btnAddNewProduct_Click" />
                </div>
                <img id="ajaxloader" src="/images/ajax-loader.gif" alt="loading" style="visibility: hidden; height: 16px;" />
                <div class="basicinfo">
                    <div class="data_grid">
                        <table class="table table-bordered table-hover">
                            <asp:HiddenField runat="server" ID="rptProductListItems" />
                            <asp:Repeater ID="rptProductList" runat="server" OnItemCommand="rptProductList_ItemCommand"
                                OnItemDataBound="rptProductList_ItemDataBound">
                                <HeaderTemplate>
                                    <tr class="item">
                                        <th>
                                            <asp:Label ID="lblCode" runat="server" Text="Mã sản phẩm"></asp:Label>
                                        </th>
                                        <th>
                                            <asp:Label ID="lblName1" runat="server" Text="Tên sản phẩm"></asp:Label>
                                        </th>
                                        <th>
                                            <asp:Label ID="lblQuantity" runat="server" Text="Số luợng"></asp:Label>
                                        </th>
                                        <th>Đơn vị tính</th>
                                        <th>
                                            <asp:Label ID="lblUnitPrice" runat="server" Text="Đơn giá"></asp:Label>
                                        </th>
                                        <th>
                                            <asp:Label ID="lblTotal1" runat="server" Text="Thành tiền"></asp:Label>
                                        </th>
                                        <th>
                                            <asp:Label ID="lblAction" runat="server" Text="Thao tác"></asp:Label>
                                        </th>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr class="item">
                                        <td>
                                            <asp:TextBox ID="txtCodeProduct" runat="server" CssClass="form-control" Style="width: 60%; float: left; margin-right: 10px;" placeholder="Mã sản phẩm" Text="Mã sản phẩm"></asp:TextBox>
                                            <asp:HiddenField ID="hddProductImportId" runat="server" />
                                            <asp:HiddenField ID="hddProductId" runat="server" />
                                            <input type="button" value="Chọn SP" class="btn btn-default" onclick='buildUrl("<%#Container.FindControl("hddProductId").ClientID%>","<%#Container.FindControl("lblName").ClientID%>")'>
                                        </td>
                                        <td>
                                            <%--                                    <asp:TextBox ID="txtProductName" style="display:none" runat="server" placeholder="Tên sản phẩm" Text="Tên sản phẩm"></asp:TextBox>--%>
                                            <asp:Label ID="lblName" runat="server" placeholder="Tên sản phẩm" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control" placeholder="Số luợng" Text=""></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtQuantity" runat="server" ErrorMessage="***"></asp:RequiredFieldValidator>

                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlUnit" CssClass="form-control" runat="server"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="ddlUnit" runat="server" ErrorMessage="***"></asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtUnitPrice" runat="server" CssClass="form-control" placeholder="Đơn giá" Text=""></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtUnitPrice" runat="server" ErrorMessage="***"></asp:RequiredFieldValidator>

                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtTotal" CssClass="form-control" runat="server" placeholder="Thành tiền" Text=""></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:ImageButton runat="server" ID="btnDelete" ToolTip='Delete' ImageUrl="/Images/delete_file.gif"
                                                AlternateText='Delete' ImageAlign="AbsMiddle" CssClass="image_button16" CommandName="Delete"
                                                CommandArgument='<%# DataBinder.Eval(Container.DataItem,"Id") %>' />
                                            <ajax:ConfirmButtonExtender ID="ConfirmButtonExtenderDelete" runat="server" TargetControlID="btnDelete"
                                                ConfirmText='Are you sure want to delete this SaleProductImport ?'>
                                            </ajax:ConfirmButtonExtender>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                            <tr>
                                <td>Tổng giá trị phiếu nhập
                                </td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td>
                                    <asp:TextBox ID="txtGrandTotal" CssClass="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                    <asp:Label ID="lblTotal" runat="server"></asp:Label>
                                </td>
                                <td></td>
                            </tr>
                        </table>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:Button ID="btnSaveProductImport" runat="server" Text="Lưu" CssClass="btn btn-primary"
            OnClick="btnSaveProductImport_Click" />
        <asp:Button ID="btnInPhieu" runat="server" Text="In phiếu" CssClass="btn btn-success"
            OnClick="btnExportExcel_OnClick" />
        <asp:Button ID="btnCancel" runat="server" Text="Đóng" OnClientClick="closePoup(1);return false;" CssClass="btn btn-default" />
    </fieldset>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
    <script type="text/javascript">
        function GetProduct(ajaxLoaderId, txtCodeId, context) {
            var code = document.getElementById(txtCodeId).value;
            if (code && code.length > 0) {
                document.getElementById(ajaxLoaderId).style.visibility = "visible";
                UseCallback(txtCodeId, context);
            }
        }
        function LoadProduct(result, context) {
            ids = context.split(",");
            values = result.split("#");
            document.getElementById(ids[0]).innerHTML = values[0];
            document.getElementById(ids[1]).style.visibility = "hidden";
            document.getElementById(ids[2]).value = values[1];
            document.getElementById(ids[3]).value = values[2];
        }
        function Calculate(unitid, quantityid, totalid) {
            unitprice = parseFloat(document.getElementById(unitid).value);
            quantity = parseFloat(document.getElementById(quantityid).value);
            total = unitprice * quantity;
            document.getElementById(totalid).value = total;

            count = parseInt(document.getElementById('<%=rptProductListItems.ClientID%>').value);
            grandtotal = 0;
            for (i = 1; i <= count; i++) {
                if (i < 10) {
                    grandtotal += parseFloat(document.getElementById('ctl00_AdminContent_rptProductList_ctl0' + i + '_txtTotal').value);
                }
                else {
                    grandtotal += parseFloat(document.getElementById('ctl00_AdminContent_rptProductList_ctl' + i + '_txtTotal').value);
                }
            }
            document.getElementById('ctl00_AdminContent_txtGrandTotal').value = addCommas(grandtotal);
            document.getElementById('ctl00_AdminContent_txtTotal1').value = addCommas(grandtotal);
        }
        function addCommas(nStr) {
            nStr += '';
            x = nStr.split(',');
            x1 = x[0];
            x2 = x.length > 1 ? ',' + x[1] : '';
            var rgx = /(\d+)(\d{3})/;
            while (rgx.test(x1)) {
                x1 = x1.replace(rgx, '$1' + '.' + '$2');
            }
            return x1 + x2;
        }
        function buildUrl(clientid,clientNameId) {
            var storageId = document.getElementById('<%=ddlStorage.ClientID%>').value;
            openPopup('/Modules/Sails/Admin/IvProductSelectorPage.aspx?NodeId=1&SectionId=15&clientid=' + clientid + "&clientNameId=" + clientNameId + "&storageId=" + storageId, 'Product select', 600, 800);
        }
        function openPopup(pageURL, title, h, w) {
            var left = (screen.width / 2) - (w / 2);
            var top = (screen.height / 2) - (h / 2);
            var targetWin = window.open(pageURL, title, 'width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
            targetWin.focus();
            return targetWin;
        }
        function closePoup() {
            window.parent.closePoup(1);
        }
    </script>
</asp:Content>
