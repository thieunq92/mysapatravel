<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TransferRequestByDate.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.TransferRequestByDate"
    MasterPageFile="MO.Master" %>

<%@ Import Namespace="Portal.Modules.OrientalSails.Domain" %>
<asp:Content ID="Head" runat="server" ContentPlaceHolderID="Head">
    <title>Transfer request by date</title>
</asp:Content>
<asp:Content ID="AdminContent" runat="server" ContentPlaceHolderID="AdminContent">
    <%-- Phần chọn ngày hiển thị, route, bustype --%>
    <div class="form-group">
        <div class="row">
            <div class="col-xs-2 nopadding-right">
                <div class="input-group">
                    <asp:TextBox ID="txtDate" runat="server" CssClass="form-control"
                        data-control="datetimepicker" autocomplete="off"></asp:TextBox>
                    <span class="input-group-btn">
                        <asp:Button ID="btnDisplay" runat="server" Text="Display" OnClick="btnDisplay_Click"
                            CssClass="btn btn-primary" />
                    </span>
                </div>
            </div>
        </div>
    </div>
    <div class="form-group">
        <div class="row">
            <div class="col-xs-12">
                <asp:Repeater runat="server" ID="rptRoute">
                    <ItemTemplate>
                        <a class='btn btn-default <%# Route.Id == (int)Eval("Id") ? "active":""%>'
                            href='<%# GetCurrentPagePathWithoutQueryString() + QueryStringBuildByCriterionRoute((int)Eval("Id"))%>'>
                            <%# Eval("Name")%></a>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>
    <div class="form-group">
        <div class="row">
            <div class="col-xs-12">
                <a class="btn btn-primary <%= BusType.Id == -1 ? "active":""%>"
                    href='<%= GetCurrentPagePathWithoutQueryString() + QueryStringBuildByCriterionBusType(0) %>'>All</a>
                <asp:Repeater runat="server" ID="rptBusType">
                    <ItemTemplate>
                        <a class='btn btn-primary <%# BusType.Id == (int)Eval("Id")? "active":"" %>'
                            href='<%# GetCurrentPagePathWithoutQueryString() + QueryStringBuildByCriterionBusType((int)Eval("Id")) %>'>
                            <%# Eval("Name")%></a>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>
    <%----%>
    <%-- Phần bảng bus by date --%>
    <div class="busbydate"
        ng-controller="controllerTransferRequestByDate"
        ng-init="Date='<%= Date.HasValue? Date.Value.ToString("dd/MM/yyyy"):"" %>'
                ;BusTypeId = <%= BusType != null ? BusType.Id : -1%>
                ;BusTypeName = '<%= BusType != null && BusType.Id > 0 ? BusType.Name : "All Bus Type"%>'
                ;RouteId = <%= Route != null ? Route.Id : -1 %>
                ;transferRequestDTOGetByCriterion()
                ;LockingTransfer = <%=LockingTransferString%>
                ;Supplier_AgencyGetAllByRole()
                ">
        <%-- Tab all --%>
        <div class="row" id="all" ng-show="BusTypeId <= 0">
            <div class="col-xs-6 sticky" ng-repeat="routeDTO in $root.transferRequestDTO.ListRouteDTO" style="z-index: 999; background-color: #fff">
                <div class="form-group">
                    <div
                        class="col-xs-12 text-center" style="padding-right: 7px"
                        ng-class="routeDTO.Way == 'To' ? 'custom-success' : 'custom-danger'">
                        <h5>
                            <strong>{{routeDTO.Name}}</strong>
                            <a
                                data-toggle="collapse" href="#collapse<%=BusType.Id%><%=Route.Id%>{{$index}}"
                                style="float: right">
                                <i class="fa fa-minus" style="color: #fff" title="Hide"
                                    data-original-title="Hide"
                                    data-toggle="tooltip"></i>
                            </a>
                        </h5>
                    </div>
                </div>
                <div class="collapse in" id="collapse<%=BusType.Id%><%=Route.Id%>{{$index}}">
                    <div class="form-group"
                        ng-repeat-start="busTypeDTO in routeDTO.ListBusTypeDTO"
                        ng-class="{'ng-hide':busTypeDTO.HaveNoBooking}">
                        <div class="row">
                            <div class="col-xs-1 --no-padding-left">
                                <strong
                                    ng-class="{'custom-text-danger':(busTypeDTO.HaveBookingNoGroup && busTypeDTO.Name != 'Standard')
                                || (busTypeDTO.Name =='Standard' && !busTypeDTO.HaveNoBooking && busTypeDTO.HaveBookingNoGroup )}">{{busTypeDTO.Name}}
                                </strong>
                            </div>
                        </div>
                    </div>
                    <div class="form-group" ng-class="{'ng-hide':busTypeDTO.HaveNoBooking}" ng-repeat-end
                        ng-repeat="busByDateDTO in busTypeDTO.ListBusByDateDTO|filter:{Deleted:false}|orderBy:'Group'">
                        <div class="row">
                            <div class="col-xs-1 nopadding-left" style="width: 5%">
                                <span data-toggle="tooltip" data-placement="top" data-title="Group">{{$parent.busTypeDTO.Name[0].toString().toUpperCase() + busByDateDTO.Group}}
                                </span>
                            </div>
                            <div class="col-xs-2 nopadding-left nopadding-right" style="width: 10%">
                                <span data-toggle="tooltip" data-placement="top" data-title="Number of pax">{{busByDateDTO.PaxString}}
                                </span>
                            </div>
                            <div class="col-xs-2 nopadding-right nopadding-left text-center" style="width: 18%">
                                <span data-toggle="tooltip" data-placement="top" data-title="Driver name">{{busByDateDTO.Driver_Name}}</span>
                            </div>
                            <div class="col-xs-2 nopadding-right nopadding-left text-center" style="width: 14%">
                                <span class="phone" data-toggle="tooltip" data-placement="top" data-title="Driver phone">{{busByDateDTO.Driver_Phone}}</span>
                            </div>
                            <div class="col-xs-3 nopadding-right nopadding-left">
                                <div class="row" ng-repeat="busByDateGuideDTO in busByDateDTO.BusByDatesGuidesDTO">
                                    <div class="col-xs-12 text-center">
                                        <span data-toggle="tooltip" data-placement="top" data-title="Guide name"
                                            ng-init="guideName_AgencyGetById(busByDateGuideDTO.GuideDTO)">{{busByDateGuideDTO.GuideDTO.Name}}</span>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xs-2 nopadding-right nopadding-left">
                                <div class="row" ng-repeat="busByDateGuideDTO in busByDateDTO.BusByDatesGuidesDTO">
                                    <div class="col-xs-12 text-center">
                                        <span data-toggle="tooltip" data-placement="top" class="phone"
                                            data-title="Guide phone"
                                            ng-init="guidePhone_AgencyGetById(busByDateGuideDTO.GuideDTO)">{{busByDateGuideDTO.GuideDTO.Phone}}</span>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xs-1 nopadding-right" style="width: 11%">
                                <a href="" ng-click="busByDateExport(busByDateDTO)">
                                    <i class="fa fa-lg fa-file-export text-warning" data-toggle="tooltip" data-placement="top" title="Export tour"></i></a>
                                <a
                                    href=""
                                    data-toggle="modal" title="View"
                                    ng-click="$root.group = busByDateDTO.Group;$root.busTypeName = $parent.busTypeDTO.Name;"
                                    data-target=".modal-transportbooking"
                                    data-url="TransferRequestByDate_TransportBooking.aspx?NodeId=1&SectionId=15&r={{$parent.routeDTO.Id}}&w={{$parent.routeDTO.Way}}&bt={{$parent.busTypeDTO.Id}}&d=<%= Date.HasValue ? Date.Value.ToString("dd/MM/yyyy") : "" %>&gr={{busByDateDTO.Group}}&bbd={{busByDateDTO.Id}}">
                                    <i class="fa fa-lg fa-edit" data-placement="top" data-toggle="tooltip" title="View"></i></a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <%----%>
        <%-- Tab bus type--%>
        <div class="row" id="each" ng-show="BusTypeId > 0">
            <div class="col-xs-6 sticky" ng-repeat="routeDTO in $root.transferRequestDTO.ListRouteDTO" style="z-index: 999">
                <%-- Bảng busbydate --%>
                <div class="row">
                    <div id="busbydate-panel{{$index}}" class="col-xs-12 --no-padding-leftright" style="background-color: #fff">
                        <div class="row">
                            <div class="col-xs-12 text-center --no-padding-leftright" style="background-clip: content-box"
                                ng-class="routeDTO.Way == 'To' ? 'custom-success' : 'custom-danger'">
                                <h5 style="padding-right: 6px">
                                    <strong>{{routeDTO.Name}}</strong>
                                    <a data-toggle="collapse" href="#eachcollapse<%=BusType.Id%><%=Route.Id%>{{$index}}"
                                        style="float: right">
                                        <i class="fa fa-minus" style="color: #fff" title="Hide"
                                            data-original-title="Hide"
                                            data-toggle="tooltip"></i>
                                    </a>
                                </h5>
                            </div>
                        </div>
                        <div class="collapse in"
                            id="eachcollapse<%=BusType.Id%><%=Route.Id%>{{$index}}">
                            <div class="form-group" ng-repeat-start="busTypeDTO in routeDTO.ListBusTypeDTO">
                                <div class="row">
                                    <div class="col-xs-1">
                                        <strong></strong>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group" ng-class="(busTypeDTO.ListBusByDateDTO | filter:{Deleted:false}).length > 0 ? 'ng-hide':''">
                                <div class="row">
                                    <div class="col-xs-offset-10 col-xs-1 nopadding-right" style="margin-left: 92.6%; padding-top: 4px; width: 5%">
                                        <a href="" ng-click="LockingTransfer || addBusByDate(busTypeDTO)"><i class="fa fa-lg fa-plus-circle text-success" ng-class="{'text-disabled':LockingTransfer}"></i></a>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group"
                                ng-repeat-end="busTypeDTO in routeDTO.ListBusTypeDTO"
                                ng-repeat="busByDateDTO in busTypeDTO.ListBusByDateDTO|filter:{Deleted:false}|orderBy:'Group'">
                                <div class="row">
                                    <div class="col-xs-1 nopadding-left" style="width: 6%; padding-top: 3px; padding-bottom: 3px">
                                        <span data-toggle="tooltip" data-placement="top" data-title="Group">{{$parent.busTypeDTO.Name[0].toString().toUpperCase() + busByDateDTO.Group}}
                                        </span>
                                        <select ng-model="busByDateDTO.Group" class="form-control"
                                            ng-options="n.Group as n.Group for n in busTypeDTO.ListBusByDateDTO|filter:{Deleted:false}|orderBy:'Group'|unique:'Group'"
                                            style="padding: 0"
                                            ng-disabled="LockingTransfer" ng-hide="true">
                                        </select>
                                    </div>
                                    <div class="col-xs-2 nopadding-left nopadding-right" style="width: 13%; text-align: center; padding: 3px">
                                        {{busByDateDTO.PaxString}}  
                                    </div>
                                    <div class="col-xs-3 nopadding-left nopadding-right" style="width: 24%">
                                        <select ng-model="busByDateDTO.SupplierId" ng-show="{{$parent.routeDTO.Way == 'To'}}"
                                            ng-options="supplierDTO.Id as supplierDTO.Name group by supplierDTO.Group for supplierDTO in ListSupplierDTOTo 
                                | orderBy:['Name']"
                                            ng-disabled="LockingTransfer" ng-change="supplierBindBusInDate(busByDateDTO)" class="form-control"
                                            ng-init="$parent.routeDTO.Way != 'To'||supplierBindBusInDate(busByDateDTO)">
                                            <option value="" selected>--Supplier--</option>
                                        </select>
                                        <select ng-model="busByDateDTO.SupplierId" ng-show="{{$parent.routeDTO.Way == 'Back'}}"
                                            ng-options="supplierDTO.Id as supplierDTO.Name group by supplierDTO.Group for supplierDTO in ListSupplierDTOBack 
                                | orderBy:['Group','Name']"
                                            ng-disabled="LockingTransfer" class="form-control">
                                            <option value="" selected>--Supplier--</option>
                                        </select>
                                    </div>
                                    <div class="col-xs-3 nopadding-left nopadding-right" style="width: 25%">
                                        <input type="text" placeholder="Driver name" ng-model="busByDateDTO.Driver_Name" class="form-control"
                                            ng-readonly="LockingTransfer" />
                                    </div>
                                    <div class="col-xs-2 nopadding-left nopadding-right" style="width: 15%">
                                        <input type="text" placeholder="Driver phone" ng-model="busByDateDTO.Driver_Phone" class="form-control"
                                            ng-readonly="LockingTransfer" data-control="phoneinputmask" style="padding-left:5px;padding-right:3px"/>
                                    </div>
                                    <%-- Nút chức năng của bus --%>
                                    <div class="col-xs-2 nopadding-right" style="padding-top: 4px; width: 17%; padding-left: 4px" id="controlBusByDate">
                                        <a href="" ng-click="busByDateExport(busByDateDTO)">
                                            <i class="fa fa-lg fa-file-export text-warning"
                                                data-toggle="tooltip" data-placement="top" title="Export tour"></i></a>
                                        <a
                                            href=""
                                            title="View"
                                            data-toggle="modal"
                                            data-target=".modal-transportbooking"
                                            data-url="TransferRequestByDate_TransportBooking.aspx?NodeId=1&SectionId=15&r={{$parent.routeDTO.Id}}&w={{$parent.routeDTO.Way}}&bt={{$parent.busTypeDTO.Id}}&d=<%= Date.HasValue ? Date.Value.ToString("dd/MM/yyyy") : "" %>&gr={{busByDateDTO.Group}}&bbd={{busByDateDTO.Id}}"
                                            ng-click="$root.group = busByDateDTO.Group;$root.busTypeName = $parent.busTypeDTO.Name;
                                            busByDateCheckIsSaved(busByDateDTO)">
                                            <i class="fa fa-lg fa-edit" style="color: darkmagenta" data-placement="top" data-toggle="tooltip" title="View"></i></a>
                                        <a href="" ng-click="LockingTransfer || addGuide(busByDateDTO)">
                                            <i class="fa fa-lg fa-plus-square text-success"
                                                data-toggle="tooltip" data-placement="top" title="Add guide" style="color: cadetblue"></i></a>
                                        <a href="" ng-click="LockingTransfer || deleteBusByDate(busByDateDTO)">
                                            <i class="fa fa-lg fa-trash text-danger" ng-class="{'text-disabled':LockingTransfer}"
                                                data-toggle="tooltip" data-placement="top" title="Delete bus"></i>
                                        </a>
                                        <a href="" ng-click="LockingTransfer || addBusByDate(busTypeDTO)">
                                            <i class="fa fa-lg fa-plus-circle text-success" ng-class="{'text-disabled':LockingTransfer}"
                                                data-toggle="tooltip" data-placement="top" title="Add bus"></i></a>
                                    </div>
                                    <%----%>
                                    <%-- Phần guide--%>
                                    <div id="agl-repeater-guide" ng-repeat="busByDateGuideDTO in busByDateDTO.BusByDatesGuidesDTO">
                                        <div class="col-xs-3 col-xs-offset-5 nopadding-left nopadding-right" style="margin-left: 43%">
                                            <select
                                                class="form-control"
                                                ng-model="busByDateGuideDTO.GuideDTO.Id"
                                                ng-options="guideDTO.Id as guideDTO.Name group by guideDTO.Group for guideDTO in ListGuideDTOTo 
                                | orderBy:['Group','Name']"
                                                ng-show="{{$parent.routeDTO.Way == 'To'}}"
                                                ng-disabled="LockingTransfer"
                                                ng-change="guidePhone_AgencyGetById(busByDateGuideDTO.GuideDTO)"
                                                ng-init="Guide_AgencyGetAllByRole($parent.routeDTO.Way)
                                ;guidePhone_AgencyGetById(busByDateGuideDTO.GuideDTO)
                                ;guideName_AgencyGetById(busByDateGuideDTO.GuideDTO)">
                                                <option value="" selected>--Guide Name--</option>
                                            </select>

                                            <select
                                                class="form-control"
                                                ng-model="busByDateGuideDTO.GuideDTO.Id"
                                                ng-options="guideDTO.Id as guideDTO.Name group by guideDTO.Group for guideDTO in ListGuideDTOBack 
                                | orderBy:['Group','Name']"
                                                ng-show="{{$parent.routeDTO.Way == 'Back'}}"
                                                ng-disabled="LockingTransfer"
                                                ng-change="guidePhone_AgencyGetById(busByDateGuideDTO.GuideDTO)"
                                                ng-init="Guide_AgencyGetAllByRole($parent.routeDTO.Way)
                                ;guidePhone_AgencyGetById(busByDateGuideDTO.GuideDTO)
                                ;guideName_AgencyGetById(busByDateGuideDTO.GuideDTO)">
                                                <option value="" selected>--Guide Name--</option>
                                            </select>
                                        </div>
                                        <div class="col-xs-2 nopadding-left nopadding-right" style="width: 15%">
                                            <input type="text" placeholder="Guide phone" ng-model="busByDateGuideDTO.GuideDTO.Phone"
                                                readonly class="form-control"
                                                data-control="phoneinputmask" style="padding-left:5px;padding-right:3px" />
                                        </div>
                                        <%--Phần nút chức năng của guide--%>
                                        <div class="col-xs-2" style="padding-top: 4px; width: 15%; padding-left: 4px">
                                            <a href="" ng-click="LockingTransfer || addGuide(busByDateDTO)">
                                                <i class="fa fa-lg fa-plus-square text-success"
                                                    data-toggle="tooltip" data-placement="right" title="Add guide" style="color: cadetblue"></i></a>
                                            <a href="" ng-click="LockingTransfer || removeGuide(busByDateDTO,$index)">
                                                <i class="fa fa-lg fa-minus-square text-danger"
                                                    data-toggle="tooltip" data-placement="right" title="Delete guide"></i></a>
                                        </div>
                                        <%----%>
                                    </div>
                                    <%----%>
                                </div>
                            </div>
                            <%-- Các nút chức năng --%>
                            <div class="form-group" ng-controller="controllerFunction" ng-init="
        Date='<%= Date.HasValue ? Date.Value.ToString("dd/MM/yyyy"):"" %>'
        ;LockingTransfer = <%=LockingTransferString%>
        ;BusTypeId = <%= BusType != null ? BusType.Id : -1%>
        ;BusTypeName = '<%= BusType != null && BusType.Id > 0 ? BusType.Name : "All" %>'
        ;RouteId = <%= Route != null ? Route.Id : -1 %>
        ;RouteName = '<%= Route != null && Route.Id > 0 ? Route.Name.Replace("-","_") : "" %>'"
                                ng-show="{{routeDTO.Way == 'To'}}">
                                <div class="row">
                                    <div class="col-xs-12 --no-padding-left">
                                        <asp:Button ID="btnSave" runat="server" ng-hide="true" OnClick="btnSave_Click" />
                                        <button id="btnSave" type="button" class="btn btn-primary" data-uniqueid="<%= btnSave.UniqueID %>" ng-click="save()"
                                            ng-hide="<%= BusType == null || BusType.Id <= 0 ? "true" : "false"%>" ng-disabled="LockingTransfer">
                                            Save</button>
                                        <button type="button" class="btn btn-primary" ng-click="busByDateExport()">Export all</button>
                                        <asp:Button ID="btnLockDate" runat="server" class="btn btn-primary" Text="Lock date" OnClick="btnLockDate_Click"></asp:Button>
                                        <asp:Button ID="btnUnlockDate" runat="server" CssClass="btn btn-primary" Text="Unlock date" OnClick="btnUnlockDate_Click"></asp:Button>
                                    </div>
                                </div>
                            </div>
                            <%----%>
                        </div>
                    </div>
                    <%----%>
                </div>
            </div>
            <%----%>
        </div>
        <%----%>
        <div class="row">
            <asp:Repeater ID="rptRouteByWay" runat="server" OnItemDataBound="rptRouteByWay_ItemDataBound">
                <ItemTemplate>
                    <div class="col-xs-6">
                        <asp:HiddenField ID="hidRouteId" runat="server" Value="<%# ((Route)Container.DataItem).Id %>" />
                        <table class="table table-bordered table-common">
                            <tr class="<%# ((Route)Container.DataItem).Way == "To" ? "custom-success" :"" %>
                            <%# ((Route)Container.DataItem).Way == "Back" ? "custom-danger" :"" %>">
                                <th rowspan="2" style="width: 4%">No
                                </th>
                                <th rowspan="2" style="width: 20%">Name of pax</th>
                                <th rowspan="2" style="width: 11%">Group
                                </th>
                                <th colspan="3" style="width: 10%">Number of pax</th>
                                <th rowspan="2" style="width: 5%">Trip</th>
                                <th rowspan="2">Pickup address</th>
                                <th rowspan="2" style="width: 12%">Booking code</th>
                            </tr>
                            <tr class="<%# ((Route)Container.DataItem).Way == "To" ? "custom-success" :"" %>
                            <%# ((Route)Container.DataItem).Way == "Back" ? "custom-danger" :"" %>">
                                <th>Adult</th>
                                <th>Child</th>
                                <th>Baby</th>
                            </tr>
                            <asp:Repeater ID="rptBusType" runat="server" OnItemDataBound="rptBusType_ItemDataBound">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hidBusTypeId" runat="server" Value="<%# ((BusType)Container.DataItem).Id %>" />
                                    <tr runat="server" id="trBusType">
                                        <td colspan="100"><strong><%# ((BusType)Container.DataItem).Name%></strong></td>
                                    </tr>
                                    <asp:Repeater ID="rptTransportBooking" runat="server" OnItemDataBound="rptTransportBooking_ItemDataBound">
                                        <ItemTemplate>
                                            <tr class="<%# TableRowColorGetByGroup((Booking)Container.DataItem,(Route)((RepeaterItem)Container.Parent.Parent.Parent.Parent).DataItem) %>">
                                                <asp:HiddenField ID="hidBookingId" runat="server" Value="<%# ((Booking)Container.DataItem).Id %>" />
                                                <td><%# Container.ItemIndex + 1 %></td>
                                                <td class="--text-left"><%# ((Booking)Container.DataItem).CustomerName %>
                                                <td>
                                                    <asp:Label ID="lblGroup" runat="server" Visible="<%# BusType == null || BusType.Id <= 0 %>" />
                                                    <asp:DropDownList runat="server" ID="ddlGroup" AppendDataBoundItems="true"
                                                        CssClass="form-control" Enabled="<%# LockingTransfer == null ? true : false%>"
                                                        Visible="<%# BusType != null && BusType.Id > 0%>" style="width:auto">
                                                        <asp:ListItem Value="--" Text="--"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td><%# ((Booking)Container.DataItem).Adult%></td>
                                                <td><%# ((Booking)Container.DataItem).Child%></td>
                                                <td><%# ((Booking)Container.DataItem).Baby%></td>
                                                <td>
                                                    <%# ((Booking)Container.DataItem).Cruise != null ?((Booking)Container.DataItem).Cruise.Code :""%>
                                                    <br />
                                                    <%# ((Booking)Container.DataItem).Trip != null ? ((Booking)Container.DataItem).Trip.TripCode:""%>
                                                </td>
                                                <td class="--text-left">
                                                    <%# !String.IsNullOrEmpty(((Booking)Container.DataItem).Transfer_Note)
                                                ? ((Booking)Container.DataItem).Transfer_Note  + "<br/><br/>" : ""%>
                                                    <%# ((Booking)Container.DataItem).PickupAddress%>
                                                </td>
                                                <td><a href="BookingView.aspx?NodeId=1&SectionId=15&bi=<%# ((Booking)Container.DataItem).Id%>" target="_blank">
                                                    <%# ((Booking)Container.DataItem).BookingIdOS%></a>
                                                    <i
                                                        class="fa fa-lg fa-angle-double-up text-success"
                                                        data-toggle="tooltip"
                                                        data-placement="top"
                                                        title="Upgraded"
                                                        <%# ((Booking)Container.DataItem).Transfer_Upgraded ? "":"style='visibility:hidden'"%>></i>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <tr <%# ((List<Booking>)((Repeater)Container.Parent).DataSource).Count > 0 ? "style='display:none'": ""%>
                                                <%# (BusType == null || BusType.Id <= 0) && ((BusType)((RepeaterItem)Container.Parent.Parent).DataItem).Name != "Standard" 
                                            && ((List<Booking>)((Repeater)Container.Parent).DataSource).Count <= 0 ? "style='display:none'": "" %>>
                                                <td colspan="100%">No records found</td>
                                            </tr>
                                            <tr style="<%# ((List<Booking>)((Repeater)Container.Parent).DataSource).Count > 0 ? "": "display:none"%>">
                                                <td colspan="3"><strong>Total</strong></td>
                                                <td><strong><%# ((List<Booking>)((Repeater)Container.Parent).DataSource).Sum(x=>x.Adult)%></strong></td>
                                                <td><strong><%# ((List<Booking>)((Repeater)Container.Parent).DataSource).Sum(x=>x.Child)%></strong></td>
                                                <td><strong><%# ((List<Booking>)((Repeater)Container.Parent).DataSource).Sum(x=>x.Baby)%></strong></td>
                                                <td colspan="100%"></td>
                                            </tr>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <div ng-controller="controllerModalTransportBooking" class="modal fade modal-transportbooking" tabindex="-1" role="dialog" aria-labelledby="gridSystemModalLabel">
            <div class="modal-dialog modal-lg" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h3 class="modal-title">Transport booking - {{$root.busTypeName}} - Group {{$root.group}}</h3>
                    </div>
                    <div class="modal-body">
                        <iframe frameborder="0" width="100%" onload="resizeIframe(this)"></iframe>
                    </div>
                </div>
            </div>
        </div>
        <button id="btnmodalstandardrequest" type="button" class="hide" data-target=".modal-standardrequest" data-toggle="modal"></button>
        <div class="modal fade modal-standardrequest" tabindex="-1" role="dialog" aria-labelledby="gridSystemModalLabel">
            <div class="modal-dialog modal-lg" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h3 class="modal-title">Standard transfer request</h3>
                    </div>
                    <div class="modal-body">
                        <iframe frameborder="0" width="100%" onload="resizeIframe(this)"></iframe>
                    </div>
                </div>
            </div>
        </div>
</asp:Content>
<asp:Content ID="Scripts" runat="server" ContentPlaceHolderID="Scripts">
    <script type="text/javascript" src="/modules/sails/admin/transferrequestbydatecontroller.js"></script>
    <script>
        $(document).ready(function () {
            $('.modal-transportbooking').on('hidden.bs.modal', function () {
                if ($(".modal-transportbooking iframe")[0].contentWindow.btnSaveClicked == true) {
                    location.reload(true);
                }
            })
        });
        $(document).ready(function () {
            $('.modal-standardrequest').on('hidden.bs.modal', function () {
                if ($(".modal-standardrequest iframe")[0].contentWindow.btnSaveClicked == true) {
                    $(".modal-transportbooking iframe")[0].contentWindow.location.reload(true);
                }
            })
        });
    </script>
    <script>
        $(document).ready(function () {
            $('.sticky').stick_in_parent({ parent: '.container-fluid', recalc_every: 1 });
        });
    </script>
</asp:Content>
