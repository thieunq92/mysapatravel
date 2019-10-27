<%@ Page Title="" Language="C#" MasterPageFile="NewPopup.Master" AutoEventWireup="true" CodeBehind="IvExportAdd.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.IvExportAdd" %>

<%@ Register Assembly="CMS.ServerControls" Namespace="CMS.ServerControls" TagPrefix="svc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content2" ContentPlaceHolderID="AdminContent" runat="server">
    <fieldset>
        <legend>THÔNG TIN PHIẾU XUẤT</legend>
        <asp:PlaceHolder ID="phInfoRoom" Visible="False" runat="server">
            <div class="basicinfo">
                <div class="form-group">
                    <div class="row">
                        <div class="col-xs-1">
                            Thông tin phòng           
                        </div>
                        <div class="col-xs-3">
                            <%--<asp:TextBox ID="txtInfo" runat="server" TextMode="MultiLine" CssClass="form-control" Width="80%" Height="150px"
                                EnableViewState="false"></asp:TextBox>--%>
                            <b>
                                <asp:Literal runat="server" ID="litRoom"></asp:Literal></b>
                        </div>
                    </div>
                </div>
            </div>
        </asp:PlaceHolder>
        <div class="basicinfo">
            <div class="form-group">
                <div class="row">
                    <%--<div class="col-xs-1">
                        Kho            
                    </div>
                    <div class="col-xs-3">
                        <asp:DropDownList ID="ddlStorage" CssClass="form-control" runat="server"></asp:DropDownList>
                    </div>--%>
                    <div class="col-xs-1">
                        Ngày xuất
                    </div>
                    <div class="col-xs-3">
                        <asp:TextBox ID="txtExportedDate" CssClass="form-control" Enabled="False" runat="server"></asp:TextBox>
                        <ajax:CalendarExtender ID="calendarExtender1" runat="server" TargetControlID="txtExportedDate"
                            Format="dd/MM/yyyy">
                        </ajax:CalendarExtender>
                        <svc:NumberValidator ID="numberValidator1" runat="server" ControlToValidate="txtExportedDate"
                            NumberType="Date" Display="Dynamic" ErrorMessage="Không đúng định dạng ngày tháng">
                        </svc:NumberValidator>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="row">
                    <div class="col-xs-1">
                        Tên phiếu (*)
                    </div>
                    <div class="col-xs-3">
                        <asp:TextBox ID="txtName" runat="server" CssClass="form-control" EnableViewState="false"></asp:TextBox>
                        <%-- <asp:RequiredFieldValidator ID="requiredFieldValidator1" runat="server" ControlToValidate="txtName"
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
                        Tên khách hàng                   
                    </div>
                    <div class="col-xs-3">
                        <asp:TextBox ID="txtCustomerName" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-xs-1">
                        Nguời xuất
                    </div>
                    <div class="col-xs-3">
                        <asp:TextBox ID="txtExportBy" ReadOnly="true" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="row">
                    <div class="col-xs-1">
                        Tổng giá trị                   
                    </div>
                    <div class="col-xs-3">
                        <asp:TextBox ID="txtTotal" CssClass="form-control" runat="server" ReadOnly="true" EnableViewState="false"></asp:TextBox>
                    </div>
                    <div class="col-xs-1">
                        Thanh toán                  
                    </div>
                    <div class="col-xs-3">
                        <asp:TextBox ID="txtPay" CssClass="form-control" runat="server" EnableViewState="false"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="row">
                    <div class="col-xs-1">
                        Ghi chú                
                    </div>
                    <div class="col-xs-4">
                        <asp:TextBox ID="txtDetail" runat="server" TextMode="MultiLine" CssClass="form-control" Width="80%" Height="150px"
                            EnableViewState="false"></asp:TextBox>
                    </div>
                    <div class="col-xs-1">
                        <asp:CheckBox ID="chkIsDebt" Text="Là công nợ" runat="server" />
                    </div>
                    <div class="col-xs-2">
                        <asp:TextBox ID="txtAgency" CssClass="form-control" runat="server" placeholder="Đối tác" EnableViewState="false"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>
        <h4>
            <asp:Label ID="lblProductExport" runat="server" Text="Danh sách sản phẩm"></asp:Label>
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
                                            <asp:Label ID="lblName" runat="server" Text="Tên sản phẩm"></asp:Label>
                                        </th>
                                        <th>
                                            <asp:Label ID="lblQuantity" runat="server" Text="Số luợng"></asp:Label>
                                        </th>
                                        <th>Đơn vị tính</th>
                                        <th>
                                            <asp:Label ID="lblUnitPrice" runat="server" Text="Đơn giá"></asp:Label>
                                        </th>
                                        <th>Khuyến mãi
                                        </th>
                                        <th>
                                            <asp:Label ID="lblTotal" runat="server" Text="Thành tiền"></asp:Label>
                                        </th>
                                        <th>
                                            <asp:Label ID="lblAction" runat="server" Text="Thao tác"></asp:Label>
                                        </th>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr class="item">
                                        <td scope="row">
                                            <asp:TextBox ID="txtCodeProduct" placeholder="Mã sản phẩm" CssClass="form-control" Style="width: 50%; float: left; margin-right: 10px;" runat="server" Text="Mã sản phẩm"></asp:TextBox>
                                            <asp:HiddenField ID="hddProductExportId" runat="server" />
                                            <asp:HiddenField ID="hddProductId" runat="server" />

                                            <input type="button" class="btn btn-default" value="Chọn" onclick='buildUrl("<%#Container.FindControl("hddProductId").ClientID%>","<%#Container.FindControl("lblName").ClientID%>")'>
                                        </td>
                                        <td scope="row">
                                            <asp:Label ID="lblName" runat="server"></asp:Label>
                                        </td>
                                        <td scope="row">
                                            <asp:TextBox ID="txtQuantity" CssClass="form-control" placeholder="Số luợng" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlUnit" CssClass="form-control" runat="server"></asp:DropDownList>
                                        </td>
                                        <td scope="row">
                                            <asp:TextBox ID="txtUnitPrice" CssClass="form-control" placeholder="Đơn giá" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDiscount" CssClass="form-control" placeholder="Khuyến mãi" Style="width: 40%; float: left; margin-right: 10px;" runat="server"></asp:TextBox>
                                            <asp:DropDownList runat="server" CssClass="form-control" Style="width: 75px" ID="ddlDiscountType">
                                                <asp:ListItem Value="0">%</asp:ListItem>
                                                <asp:ListItem Value="1">VND</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td scope="row">
                                            <asp:TextBox ID="txtTotal" runat="server" CssClass="form-control" placeholder="Thành tiền" ReadOnly="true"></asp:TextBox>
                                        </td>
                                        <td scope="row">
                                            <asp:ImageButton runat="server" ID="btnDelete" ToolTip='Delete' ImageUrl="/Images/delete_file.gif"
                                                AlternateText='Delete' ImageAlign="AbsMiddle" CssClass="image_button16" CommandName="Delete"
                                                CommandArgument='<%# Eval("Id") %>' />
                                            <ajax:ConfirmButtonExtender ID="ConfirmButtonExtenderDelete" runat="server" TargetControlID="btnDelete"
                                                ConfirmText='Are you sure want to delete this ProductExport ?'>
                                            </ajax:ConfirmButtonExtender>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                            <tr>
                                <td>Tổng giá trị phiếu xuất
                                </td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td>
                                    <asp:TextBox ID="txtGrandTotal" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                    <asp:Label ID="Label2" runat="server"></asp:Label>
                                </td>
                                <td></td>
                            </tr>
                        </table>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:Button ID="btnSaveProductExport" CssClass="btn btn-primary" runat="server" Text="Lưu"
            OnClick="btnSaveProductExport_Click" />
        <asp:Button ID="btnInPhieu" runat="server" Text="In phiếu" CssClass="btn btn-success"
            OnClick="btnExportExcel_OnClick" />
        <asp:Button ID="btnCancel" runat="server" Text="Đóng" OnClientClick="closePoup();return false;" CssClass="btn btn-default" />
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
            let ids = context.split(",");
            let values = result.split("#");
            document.getElementById(ids[0]).innerHTML = values[0];
            document.getElementById(ids[1]).style.visibility = "hidden";
            document.getElementById(ids[2]).value = values[1];
        }
        function Calculate(unitid, quantityid, totalid,discountId,discountTypeId) {
            let unitprice = parseFloat(document.getElementById(unitid).value);
            let quantity = parseFloat(document.getElementById(quantityid).value);
            let total = unitprice * quantity;
            let discount = parseFloat(document.getElementById(discountId).value);
            if (discount > 0) {
                let type = document.getElementById(discountTypeId);
                let value = type.options[type.selectedIndex].value;
                if (value === "0") {
                    total = total - ((total / 100) * discount);
                } else {
                    total = total - discount;
                }
            }
            document.getElementById(totalid).value = total;

            let count = parseInt(document.getElementById('<%=rptProductListItems.ClientID%>').value);
            let grandtotal = 0;
            for (i = 1; i <= count; i++) {
                if (i < 10) {
                    grandtotal += parseFloat(document.getElementById('ctl00_AdminContent_rptProductList_ctl0' + i + '_txtTotal').value);
                }
                else {
                    grandtotal += parseFloat(document.getElementById('ctl00_AdminContent_rptProductList_ctl' + i + '_txtTotal').value);
                }
            }
            document.getElementById('ctl00_AdminContent_txtGrandTotal').value = addCommas(grandtotal);
            <%--var discount = document.getElementById('<%=txtDiscount.ClientID%>').value;
            if (discount) {
                grandtotal = grandtotal - parseFloat(discount);
            }--%>
            document.getElementById('ctl00_AdminContent_txtTotal').value = addCommas(grandtotal);
        }
        function addCommas(nStr) {
            nStr += '';
            x = nStr.split('.');
            x1 = x[0];
            x2 = x.length > 1 ? '.' + x[1] : '';
            var rgx = /(\d+)(\d{3})/;
            while (rgx.test(x1)) {
                x1 = x1.replace(rgx, '$1' + ',' + '$2');
            }
            return x1 + x2;
        }
        function buildUrl(clientid, clientNameId) {
<%--            var storageId = document.getElementById('<%=ddlStorage.ClientID%>').value;--%>
            openPopup('/Modules/Sails/Admin/IvProductInStockSelectPage.aspx?NodeId=1&SectionId=15&clientid=' + clientid + "&clientNameId=" + clientNameId , 'Product select', 600, 1000);
        }
        function openPopup(pageUrl, title, h, w) {
            var left = (screen.width / 2) - (w / 2);
            var top = (screen.height / 2) - (h / 2);
            var targetWin = window.open(pageUrl, title, 'width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
            targetWin.focus();
            return targetWin;
        }
        function closePoup() {
            window.parent.closePoup(1);
        }
    </script>
</asp:Content>
