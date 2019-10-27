<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GoldenDayCreateCampaign.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.GoldenDayCreateCampaign"
    MasterPageFile="MO.Master" Title="Create campaign" %>

<asp:Content ID="AdminContent" runat="server" ContentPlaceHolderID="AdminContent">
    <div class="wrapper wrapper__body wrapper__goldenday-create-campaign">
        <div id="createcampaign-panel" ng-controller="createCampaignController" ng-if="$root.campaign == null">
            <fieldset
                ng-init="
                month = '<%= DateTime.Today.AddMonths(1).Month.ToString() %>';
                year = '<%= DateTime.Today.Year %>'
            ">
                <legend>Create campaign </legend>
                <div class="form-group">
                    <div class="row">
                        <div class="col-xs-1 col__month-label --no-padding-left">
                            <label>Month</label>
                        </div>
                        <div class="col-xs-2 --no-padding-left col__month">
                            <asp:DropDownList runat="server" ID="ddlMonth" CssClass="form-control" ng-model="month">
                            </asp:DropDownList>
                        </div>
                        <div class="col-xs-1 col__year-label --no-padding-left">
                            <label>Year</label>
                        </div>
                        <div class="col-xs-2 --no-padding-left col__year">
                            <asp:DropDownList runat="server" ID="ddlYear" CssClass="form-control" ng-model="year">
                            </asp:DropDownList>
                        </div>
                        <div class="col-xs-1 --no-padding-left">
                            <asp:Button runat="server" ID="btnSave" Text="Save" CssClass="btn btn-primary" ng-click="save()" ng-disabled="buttonSaveDisabled" OnClick="btnSave_Click"
                                data-id="btnSave" data-uniqueid="<%# btnSave.UniqueID %>" data-clientid="<%# btnSave.ClientID%>"/>
                        </div>
                    </div>
                </div>
            </fieldset>
            <input type="hidden" value="{{month}}" name="month" />
            <input type="hidden" value="{{year}}" name="year" />
        </div>

        <div id="createdate-panel" ng-controller="createDateController" ng-if="$root.campaign != null">
            <input type="hidden" value="" name="month" ng-value="$root.campaign.Month"/>
            <input type="hidden" value="" name="year" ng-value="$root.campaign.Year"/>
            <input type="hidden" data-clientid="<%= btnSave.ClientID%>" data-uniqueid="<%= btnSave.UniqueID %>" data-id="btnSave">
            <fieldset>
                <legend>{{$root.campaign.Name}}</legend>
                <div class="row">
                    <div class="col-xs-6 col__cruise-avaibility --no-padding-left">
                        <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div id="upCruiseAvailable">
                                    <div class="row">
                                        <h4 class="--text-bold col-xs-7 --no-padding-leftright">Cruise availability in month</h4>
                                        <div class="col-xs-5 --no-padding-leftright">
                                        </div>
                                    </div>
                                    <table class="table table-bordered table-common table__cruise-avaiability" style="border-right: none; border-top: none; border-bottom: none">
                                        <asp:Repeater ID="rptCruiseAvaibility" runat="server" OnItemDataBound="rptCruiseAvaibility_ItemDataBound">
                                            <HeaderTemplate>
                                                <tr class="header active">
                                                    <th style="width: 16%">Date</th>
                                                    <asp:Literal ID="ltrHeader" runat="server"></asp:Literal>
                                                </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Literal ID="ltrRow" runat="server"></asp:Literal>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <tr>
                                        </tr>
                                    </table>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                    <div class="col-xs-6 col__create-date">
                        <div class="form-group">
                            <div class="row">
                                <div class="col-xs-2 col__add-date --no-padding-left">
                                    <label>Add date:</label>
                                </div>
                                <div class="col-xs-4 --no-padding-left">
                                    <input type="text" data-control="datetimepicker" class="form-control" placeholder="Select date(dd/mm/yyyy)" ng-model="date" />
                                </div>
                                <div class="col-xs-1 --no-padding-left">
                                    <button type="button" class="btn btn-primary" ng-click="add(date,null)">Add</button>
                                </div>
                            </div>
                        </div>
                        <div class="row__date" ng-repeat="goldenDay in $root.campaign.GoldenDays">
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-xs-12 col__button-date --no-padding-leftright">
                                        <a href="" ng-click="delete($index)"><i class="fa fa-lg fa-times text-danger --text-bold"></i></a>
                                        <a href="" class="a__button-add" ng-click="add(null,null)"><i class="fa fa-lg fa-plus text-success --text-bold"></i></a>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-xs-2 --no-padding-left">
                                        <label>Date {{$index + 1}}: </label>
                                    </div>
                                    <div class="col-xs-4 --no-padding-left">
                                        <input type="text" data-control="datetimepicker" class="form-control" placeholder="Select date(dd/mm/yyyy)" ng-model="goldenDay.DateAsString" name="date{{$index}}" />
                                    </div>
                                    <div class="col-xs-6 --no-padding-leftright">
                                        <textarea placeholder="Policy" class="form-control textbox__policy" ng-model="goldenDay.Policy" name="policy{{$index}}"></textarea>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="form-group" ng-show="$root.campaign.GoldenDays.length > 0">
                            <div class="row">
                                <div class="col-xs-12 col__button-save --no-padding-leftright">
                                    <button type="button" class="btn btn-primary" ng-click="save()" ng-disabled="buttonSaveDisabled">Save</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </fieldset>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Scripts" runat="server" ContentPlaceHolderID="Scripts">
    <script type="text/javascript" src="/modules/sails/admin/goldendaycreatecampaigncontroller.js"></script>
</asp:Content>
