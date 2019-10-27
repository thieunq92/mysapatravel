<%@ Page Language="C#" MasterPageFile="MO.Master" AutoEventWireup="true"
    CodeBehind="AgencyView.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.AgencyView"
    Title="Agency View" %>

<%@ Import Namespace="Portal.Modules.OrientalSails.Domain" %>
<%@ Register Assembly="CMS.ServerControls" Namespace="CMS.ServerControls" TagPrefix="svc" %>
<asp:Content ID="Head" ContentPlaceHolderID="Head" runat="server">
    <link rel="stylesheet" type="text/css" href="/css/jqueryfileupload/v9.21.0/jquery.fileupload.css" />
    <link rel="stylesheet" type="text/css" href="/css/jqueryfileupload/v9.21.0/jquery.fileupload-ui.css" />
</asp:Content>
<asp:Content ID="AdminContent" ContentPlaceHolderID="AdminContent" runat="server">
    <div class="wrapper">
        <svc:Popup ID="popupManager" runat="server">
        </svc:Popup>
        <h2 class="page-header --text-bold">
            <asp:Literal runat="server" ID="litName1"></asp:Literal></h2>
        <div class="panel-agency">
            <div class="row">
                <div class="col-xs-6">
                    <div class="form-group">
                        <div class="row">
                            <div class="col-xs-4">
                                <label>Name</label>
                            </div>
                            <div class="col-xs-8">
                                <asp:Literal runat="server" ID="litName"></asp:Literal>
                                <% if (String.IsNullOrEmpty(litName.Text))
                                   { %> none <%} %>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <div class="col-xs-4">
                                <label>Agent level</label>
                            </div>
                            <div class="col-xs-8">
                                <asp:Literal runat="server" ID="litAgentLevel"></asp:Literal>
                                <% if (String.IsNullOrEmpty(litAgentLevel.Text))
                                   { %> none <%} %>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <div class="col-xs-4">
                                <label>Role</label>
                            </div>
                            <div class="col-xs-8">
                                <asp:Literal runat="server" ID="litRole"></asp:Literal>
                                <% if (String.IsNullOrEmpty(litRole.Text))
                                   { %> none <%} %>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <div class="col-xs-4">
                                <label>Address</label>
                            </div>
                            <div class="col-xs-8">
                                <asp:Literal runat="server" ID="litAddress"></asp:Literal>
                                <% if (String.IsNullOrEmpty(litAddress.Text))
                                   { %> none <%} %>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <div class="col-xs-4">
                                <label>Phone</label>
                            </div>
                            <div class="col-xs-8">
                                <asp:Literal runat="server" ID="litPhone"></asp:Literal>
                                <% if (String.IsNullOrEmpty(litPhone.Text))
                                   { %> none <%} %>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <div class="col-xs-4">
                                <label>Email</label>
                            </div>
                            <div class="col-xs-4">
                                <asp:HyperLink runat="server" ID="hplEmail"></asp:HyperLink>
                                <% if (String.IsNullOrEmpty(hplEmail.Text))
                                   { %> none <%} %>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <div class="col-xs-4">
                                <label>Website</label>
                            </div>
                            <div class="col-xs-8">
                                <asp:Literal runat="server" ID="litWebsite"></asp:Literal>
                                <% if (String.IsNullOrEmpty(litWebsite.Text))
                                   { %> none <%} %>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <div class="col-xs-4">
                                <label>Sales in charge</label>
                            </div>
                            <div class="col-xs-8">
                                <asp:Literal runat="server" ID="litSale"></asp:Literal>
                                <% if (String.IsNullOrEmpty(litSale.Text))
                                   { %> none <%} %>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <div class="col-xs-4">
                                <label>Tax code</label>
                            </div>
                            <div class="col-xs-8">
                                <asp:Literal runat="server" ID="litTax"></asp:Literal>
                                <% if (String.IsNullOrEmpty(litTax.Text))
                                   { %> none <%} %>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <div class="col-xs-4">
                                <label>Location</label>
                            </div>
                            <div class="col-xs-8">
                                <asp:Literal runat="server" ID="litLocation"></asp:Literal>
                                <% if (String.IsNullOrEmpty(litLocation.Text))
                                   { %> none <%} %>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <div class="col-xs-4">
                                <label>Payment</label>
                            </div>
                            <div class="col-xs-8">
                                <asp:Literal runat="server" ID="litPayment"></asp:Literal>
                                <% if (String.IsNullOrEmpty(litPayment.Text))
                                   { %> none <%} %>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <div class="col-xs-4">
                                <label>Other info</label>
                            </div>
                            <div class="col-xs-8">
                                <asp:Literal runat="server" ID="litNote"></asp:Literal>
                                <% if (String.IsNullOrEmpty(litNote.Text))
                                   { %> none <%} %>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-xs-6">
                    <fieldset>
                        <legend class="--text-bold --text-italic">Thông tin trong contract</legend>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-xs-4">
                                    <label>Tên giao dịch</label>
                                </div>
                                <div class="col-xs-8">
                                    <asp:Literal runat="server" ID="litTradingName"></asp:Literal>
                                    <% if (String.IsNullOrEmpty(litTradingName.Text))
                                       { %> none <%} %>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-xs-4">
                                    <label>Người đại diện</label>
                                </div>
                                <div class="col-xs-8">
                                    <asp:Literal runat="server" ID="litRepresentative"></asp:Literal>
                                    <% if (String.IsNullOrEmpty(litRepresentative.Text))
                                       { %> none <%} %>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-xs-4">
                                    <label>Chức vụ</label>
                                </div>
                                <div class="col-xs-8">
                                    <asp:Literal runat="server" ID="litRepresentativePosition"></asp:Literal>
                                    <% if (String.IsNullOrEmpty(litRepresentativePosition.Text))
                                       { %> none <%} %>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-xs-4">
                                    <label>Địa chỉ</label>
                                </div>
                                <div class="col-xs-8">
                                    <%= litAddress.Text%>
                                    <% if (String.IsNullOrEmpty(litAddress.Text))
                                       { %> none <%} %>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-xs-4">
                                    <label>Điện thoại</label>
                                </div>
                                <div class="col-xs-8">
                                    <%= litPhone.Text%>
                                    <% if (String.IsNullOrEmpty(litPhone.Text))
                                       { %> none <%} %>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-xs-4">
                                    <label>Mã số thuế</label>
                                </div>
                                <div class="col-xs-8">
                                    <%= litTax.Text %>
                                    <% if (String.IsNullOrEmpty(litTax.Text))
                                       { %>
                                    none
                                    <% } %>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-xs-4">
                                    <label>Người liên hệ</label>
                                </div>
                                <div class="col-xs-8">
                                    <asp:Literal runat="server" ID="litContact"></asp:Literal>
                                    <% if (String.IsNullOrEmpty(litContact.Text))
                                       { %>
                                    none <%} %>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-xs-4">
                                    <label>Địa chỉ</label>
                                </div>
                                <div class="col-xs-8">
                                    <asp:Literal runat="server" ID="litContactAddress"></asp:Literal>
                                    <% if (String.IsNullOrEmpty(litContactAddress.Text))
                                       { %> none <%} %>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-xs-4">
                                    <label>Chức vụ</label>
                                </div>
                                <div class="col-xs-8">
                                    <asp:Literal runat="server" ID="litContactPosition"></asp:Literal>
                                    <% if (String.IsNullOrEmpty(litContactPosition.Text))
                                       { %> none <%} %>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-xs-4">
                                    <label>Email</label>
                                </div>
                                <div class="col-xs-8">
                                    <asp:Literal runat="server" ID="litContactEmail"></asp:Literal>
                                    <% if (String.IsNullOrEmpty(litContactEmail.Text))
                                       { %> none <%} %>
                                </div>
                            </div>
                        </div>
                    </fieldset>
                </div>
            </div>
            <div class="row row__buttons">
                <div class="col-xs-12 col__buttons --width-auto">
                    <div class="btn-toolbar">
                        <asp:HyperLink runat="server" ID="hplEditAgency" CssClass="btn btn-primary">Edit agency</asp:HyperLink>
                        <asp:HyperLink runat="server" ID="hplBookingList" CssClass="btn btn-primary">Booking by agency</asp:HyperLink>
                        <div id="disableInform" style="display: none">
                            You don't have permission to use this function. If you want to use this function please contact administrator
                        </div>
                        <asp:HyperLink runat="server" ID="hplReceivable"
                            CssClass="btn btn-primary">Receivables (last 3 months)</asp:HyperLink>
                        <a href="SeriesManager.aspx?NodeId=1&SectionId=15&ai=<%= Request.QueryString["AgencyId"] %>" class="btn btn-primary">Series Booking</a>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-12 --no-padding-leftright" ng-controller="getAgencyNotesByIdController">
                    <h4 class="page-header --text-bold">Note</h4>
                    <table class="table table-bordered table-common">
                        <tr class="active">
                            <th>For role</th>
                            <th>Note</th>
                            <th style="width: 3%"></th>
                            <th style="width: 3%"></th>
                        </tr>
                        <asp:Repeater runat="server" ID="rptAgencyNotes" OnItemCommand="rptAgencyNotes_ItemCommand">
                            <ItemTemplate>
                                <tr>
                                    <td><%# ((AgencyNotes)Container.DataItem).Role.Name %></td>
                                    <td class="--text-left">
                                        <article>
                                            <p>
                                                <%# ((AgencyNotes)Container.DataItem).Note%>
                                            </p>
                                        </article>
                                    </td>
                                    <td>
                                        <a href="" data-toggle="modal" data-target="#addNoteModal" ng-click="getAgencyNotesById(<%#(((AgencyNotes)Container.DataItem).Id)%>)"
                                            onclick="$('#addNoteModal .modal-title').html('Edit note ');clearFormAgencyNotes()">
                                            <i class="fa fa-lg fa-edit icon icon__edit" data-toggle="tooltip" title="Edit"></i>
                                        </a>
                                        <td>
                                            <asp:LinkButton runat="server" ID="lbtDelete" OnClick="lbtDelete_Click" CommandArgument='<%#((AgencyNotes)Container.DataItem).Id%>' CommandName="Delete"
                                                OnClientClick="return confirm('Are you sure?')"><i class="fa fa-trash fa-lg text-danger" aria-hidden="true" title="" data-toggle="tooltip" data-placement="top" data-original-title="Delete"></i></asp:LinkButton></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                        <%
                            if (!IsPostBack)
                            {
                                if (((IEnumerable<AgencyNotes>)rptAgencyNotes.DataSource).Count() <= 0)
                                {
                        %>
                        <tr>
                            <td colspan="100%">No records found</td>
                        </tr>
                        <%
                                }
                            }
                        %>
                    </table>
                    <a href="" data-toggle="modal" data-target="#addNoteModal" class="btn btn-primary" onclick="$('#addNoteModal .modal-title').html('Add note ');clearFormAgencyNotes()">Add note</a>
                </div>
            </div>
            <div class="row row__contacts">
                <div class="panel-contacts">
                    <h4 class="page-header page-header__contacts --text-bold">Contacts</h4>
                    <asp:PlaceHolder runat="server" ID="plhContacts">
                        <table class="table table-bordered table-common">
                            <tr class="active">
                                <th>Name
                                </th>
                                <th>Position
                                </th>
                                <th style="width: 6%">Booker
                                </th>
                                <th>Phone
                                </th>
                                <th>Email
                                </th>
                                <th style="width: 10%">Birthday
                                </th>
                                <th>Note
                                </th>
                                <th style="width: 3%"></th>
                                <th style="width: 3%"></th>
                                <th style="width: 3%"></th>
                            </tr>
                            <asp:Repeater runat="server" ID="rptContacts" OnItemDataBound="rptContacts_ItemDataBound">
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:Literal runat="server" ID="ltrName"></asp:Literal>
                                        </td>
                                        <td>
                                            <asp:Literal runat="server" ID="litPosition"></asp:Literal>
                                        </td>
                                        <td>
                                            <asp:Literal runat="server" ID="litBooker"></asp:Literal>
                                        </td>
                                        <td>
                                            <asp:Literal runat="server" ID="litPhone"></asp:Literal>
                                        </td>
                                        <td>
                                            <asp:HyperLink runat="server" ID="hplEmail"></asp:HyperLink>
                                        </td>
                                        <td>
                                            <%# ((DateTime?)Eval("Birthday"))==null? "" : ((DateTime?)Eval("Birthday")).Value.ToString("dd/MM/yyyy")%>
                                        </td>
                                        <td>
                                            <%# DataBinder.Eval(Container.DataItem, "Note") %>
                                        </td>
                                        <td>
                                            <a href="" data-toggle="modal" data-target="#addMeetingModal"
                                                onclick="$('#addMeetingModal .modal-title').html('Add meeting / Report problem');clearFormMeeting();$('#<%= hidAgencyContactId.ClientID %>').val(<%#(((AgencyContact)Container.DataItem).Id)%>)">
                                                <i class="fa fa-lg fa-users" style="color: black" data-toggle="tooltip" title="Add meeting"></i>
                                            </a>
                                        </td>
                                        <td>
                                            <asp:HyperLink runat="server" ID="hplName"><i class="fa fa-edit fa-lg" aria-hidden="true" data-toggle="tooltip" data-placement="top" title="Edit"></i></asp:HyperLink>
                                        </td>
                                        <td>
                                            <asp:LinkButton runat="server" ID="lbtDelete" OnClick="lbtDelete_Click" CommandArgument='<%#Eval("Id")%>'
                                                OnClientClick="return confirm('Are you sure?')"><i class="fa fa-trash fa-lg text-danger" aria-hidden="true" title="" data-toggle="tooltip" data-placement="top" data-original-title="Delete"></i></asp:LinkButton>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                            <% if (((IList)rptContacts.DataSource).Count <= 0)
                               {%>
                            <tr>
                                <td colspan="100%">No records found</td>
                            </tr>
                            <% } %>
                        </table>
                        <div class="btn-toolbar">
                            <asp:HyperLink runat="server" ID="hplAddContact" CssClass="btn btn-primary">Add contact</asp:HyperLink>
                        </div>
                    </asp:PlaceHolder>
                    <asp:Label runat="server" ID="lblContacts" Text="You don't have permission to use this function. If you want to use this function please contact administrator"
                        Visible="False" />
                </div>
            </div>
            <div class="row row__meetings">
                <div class="panel-recentactivities" ng-controller="getActivityByIdController">
                    <h4 class="page-header page-header__meetings --text-bold">Recent meetings
                    </h4>
                    <asp:PlaceHolder runat="server" ID="plhActivities">
                        <table class="table table-bordered table-common">
                            <tr class="active">
                                <th style="width: 10%">Date meeting
                                </th>
                                <th>Sale
                                </th>
                                <th>Meeting with
                                </th>
                                <th>Position
                                </th>
                                <th>Note
                                </th>
                                <th style="width: 3%"></th>
                                <th style="width: 3%"></th>
                            </tr>
                            <asp:Repeater runat="server" ID="rptActivities" OnItemDataBound="rptActivities_ItemDataBound">
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:Literal runat="server" ID="ltrDateMeeting" />
                                        </td>
                                        <td>
                                            <asp:Literal runat="server" ID="ltrSale" />
                                        </td>
                                        <td>
                                            <asp:Literal runat="server" ID="ltrName" />
                                        </td>
                                        <td>
                                            <asp:Literal runat="server" ID="ltrPosition" />
                                        </td>
                                        <td class="--text-left">
                                            <article>
                                                <p>
                                                    <asp:Literal runat="server" ID="ltrNote" />
                                                </p>
                                            </article>
                                        </td>
                                        <td>
                                            <a href="" data-toggle="modal" data-target="#addMeetingModal" ng-click="getActivityById(<%#(((Activity)Container.DataItem).Id)%>)"
                                                onclick="$('#addMeetingModal .modal-title').html('Edit <%#(((Activity)Container.DataItem).Type)%>');clearFormMeeting()">
                                                <i class="fa fa-lg fa-edit icon icon__edit" data-toggle="tooltip" title="Edit"></i>
                                            </a>
                                        </td>
                                        <td>
                                            <asp:LinkButton runat="server" ID="lbtDeleteActivity" OnClick="lbtDeleteActivity_Click"
                                                CommandArgument='<%#Eval("Id")%>' OnClientClick="return confirm('Are you sure?')"><i class="fa fa-trash fa-lg text-danger" aria-hidden="true" title="" data-toggle="tooltip" data-placement="top" data-original-title="Delete"></i></asp:LinkButton>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                            <% if (((IEnumerable<Activity>)rptActivities.DataSource).Count() <= 0)
                               { %>
                            <tr>
                                <td colspan="100%">No records found</td>
                            </tr>
                            <% } %>
                        </table>
                    </asp:PlaceHolder>
                    <asp:Label runat="server" ID="lblActivities" Text="You don't have permission to use this function. If you want to use this function please contact administrator"
                        Visible="False" />
                </div>
            </div>
            <div class="row row__contracts">
                <div class="panel-contracts">
                    <h4 class="page-header page-header__contracts --text-bold">Contracts and quotations</h4>
                    <asp:PlaceHolder runat="server" ID="plhContracts">
                        <table class="table table-bordered table-common">
                            <tr class="active">
                                <th style="width: 10%">Valid From</th>
                                <th style="width: 10%">Valid To</th>
                                <th>Contract \ Quotation</th>
                                <th style="width: 15%">Status</th>
                                <th></th>
                            </tr>
                            <asp:Repeater runat="server" ID="rptContracts" OnItemDataBound="rptContracts_ItemDataBound" OnItemCommand="rptContracts_ItemCommand">
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <%# ((DateTime?)Eval("ContractValidFromDate")) == null ? "" : ((DateTime?)Eval("ContractValidFromDate")).Value.ToString("dd/MM/yyyy") %>
                                            <%# ((DateTime?)Eval("QuotationValidFromDate")) == null ? "" : ((DateTime?)Eval("QuotationValidFromDate")).Value.ToString("dd/MM/yyyy") %>
                                        </td>
                                        <td>
                                            <%# ((DateTime?)Eval("ContractValidToDate")) == null ? "" : ((DateTime?)Eval("ContractValidToDate")).Value.ToString("dd/MM/yyyy") %>
                                            <%# ((DateTime?)Eval("QuotationValidToDate")) == null ? "" : ((DateTime?)Eval("QuotationValidToDate")).Value.ToString("dd/MM/yyyy") %>
                                        </td>
                                        <td class="--text-left">
                                            <%# ((AgencyContract)Container.DataItem).ContractTemplateName %>
                                            <%# ((AgencyContract)Container.DataItem).QuotationTemplateName %>
                                        </td>
                                        <td></td>
                                        <td class="--text-left">
                                            <asp:HyperLink runat="server" ID="hplDownload" ToolTip="Download"></asp:HyperLink>
                                            <asp:LinkButton runat="server" ID="lbtDownloadContract" Text="<%# ((AgencyContract)Container.DataItem).ContractTemplateName %>" CommandName="DownloadContract" CommandArgument="<%# ((AgencyContract)Container.DataItem).Id %>"></asp:LinkButton>
                                            <asp:LinkButton runat="server" ID="lbtDownloadQuotation" Text="<%# ((AgencyContract)Container.DataItem).QuotationTemplateName %>" CommandName="DownloadQuotation" CommandArgument="<%# ((AgencyContract)Container.DataItem).Id %>"></asp:LinkButton>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                            <% if (((IEnumerable<AgencyContract>)rptContracts.DataSource).Count() <= 0)
                               { %>
                            <tr>
                                <td colspan="100%">No records found</td>
                            </tr>
                            <% }%>
                        </table>
                        <div class="btn-toolbar">
                            <button type="button" class="btn btn-primary" data-toggle="modal" data-target=".modal-issuecontract">Issue contract</button>
                            <button type="button" class="btn btn-primary" data-toggle="modal" data-target=".modal-issuequotation">Issue quotation</button>
                        </div>
                    </asp:PlaceHolder>
                    <asp:Label runat="server" ID="lblContracts" Text="You don't have permission to use this function. If you want to use this function please contact administrator"
                        Visible="False" />
                </div>
            </div>
        </div>
        <div class="modal fade modal-issuecontract" tabindex="-1" role="dialog" aria-labelledby="gridSystemModalLabel" data-backdrop="true" data-keyboard="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h3 class="modal-title">Issue Contract</h3>
                    </div>
                    <div class="modal-body">
                        <div class="form-group">
                            <div class="row">
                                <div class="col-xs-2 --no-padding-leftright">
                                    <label>Valid from</label>
                                </div>
                                <div class="col-xs-4 --no-padding-leftright">
                                    <asp:TextBox runat="server" ID="txtContractValidFromDate" CssClass="form-control" placeholder="Valid from (dd/mm/yyyy)" data-type="datetimepicker"></asp:TextBox>
                                </div>
                                <div class="col-xs-2 --no-padding-right">
                                    <label>Valid to</label>
                                </div>
                                <div class="col-xs-4 --no-padding-leftright">
                                    <asp:TextBox runat="server" ID="txtContractValidToDate" CssClass="form-control" placeholder="Valid to (dd/mm/yyyy)" data-type="datetimepicker"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-xs-2 --no-padding-leftright">
                                    <label>Select contract</label>
                                </div>
                                <div class="col-xs-4 --no-padding-leftright">
                                    <asp:DropDownList runat="server" ID="ddlContractTemplate" CssClass="form-control" AppendDataBoundItems="true">
                                        <asp:ListItem Text="-- Select contract --" Value="-1"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-xs-2 --no-padding-right">
                                    <label>Status</label>
                                </div>
                                <div class="col-xs-4 --no-padding-leftright">
                                    <asp:DropDownList runat="server" ID="ddlStatus" CssClass="form-control">
                                        <asp:ListItem Text="-- Select Status --" Value="-1"></asp:ListItem>
                                        <asp:ListItem Text="Contract sent" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Contract in valid" Value="2"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-xs-2 --no-padding-leftright">
                                    <label>Upload</label>
                                </div>
                                <div class="col-xs-10 --no-padding-leftright">
                                    <div class="row">
                                        <div class="col-xs-12 --no-padding-leftright" style="margin-bottom: 10px">
                                            <span class="btn btn-success fileinput-button">
                                                <i class="glyphicon glyphicon-plus"></i>
                                                <span>Add file</span>
                                                <input id="btnFileUploadContract" name="file" multiple="" type="file">
                                                <asp:HiddenField runat="server" ID="hifContractTemplatePath" />
                                                <asp:HiddenField runat="server" ID="hifContractTemplateName" />
                                            </span>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-12 --no-padding-leftright">
                                            <div class="progress">
                                                <div class="progress-bar progress-bar-success progress-bar-striped" role="progressbar" aria-valuenow="" aria-valuemin="0" aria-valuemax="100">
                                                    <span class="sr-only"></span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        <asp:Button runat="server" ID="btnIssueContract" CssClass="btn btn-primary" Text="Issue" OnClick="btnIssueContract_Click"></asp:Button>
                    </div>
                </div>
            </div>
        </div>
        <%--<div class="modal fade modal-issuequotation" tabindex="-1" role="dialog" aria-labelledby="gridSystemModalLabel" data-backdrop="true" data-keyboard="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h3 class="modal-title">Issue Quotation</h3>
                    </div>
                    <div class="modal-body">
                        <div class="form-group">
                            <div class="row">
                                <div class="col-xs-2 --no-padding-leftright">
                                    <label>Valid from</label>
                                </div>
                                <div class="col-xs-4 --no-padding-leftright">
                                    <asp:TextBox runat="server" ID="txtQuotationValidFromDate" CssClass="form-control" placeholder="Valid from (dd/mm/yyyy)" data-type="datetimepicker"></asp:TextBox>
                                </div>
                                <div class="col-xs-2 --no-padding-right">
                                    <label>Valid to</label>
                                </div>
                                <div class="col-xs-4 --no-padding-leftright">
                                    <asp:TextBox runat="server" ID="txtQuotationValidToDate" CssClass="form-control" placeholder="Valid to (dd/mm/yyyy)" data-type="datetimepicker"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="form-group">
                            <div class="row">
                                <div class="col-xs-2 --no-padding-leftright">
                                    <label>Select quotation</label>
                                </div>
                                <div class="col-xs-10 --no-padding-leftright">
                                    <asp:DropDownList ID="ddlQuotationTemplate" runat="server" CssClass="form-control" AppendDataBoundItems="True">
                                        <asp:ListItem Text="-- Select quotation --" Value="-1"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-xs-2 --no-padding-leftright">
                                    <label>Upload</label>
                                </div>
                                <div class="col-xs-10 --no-padding-leftright">
                                    <div class="row">
                                        <div class="col-xs-12 --no-padding-leftright" style="margin-bottom: 10px">
                                            <span class="btn btn-success fileinput-button">
                                                <i class="glyphicon glyphicon-plus"></i>
                                                <span>Add file</span>
                                                <input id="btnFileUploadQuotation" name="file" multiple="" type="file">
                                                <asp:HiddenField runat="server" ID="hifQuotationTemplatePath" />
                                                <asp:HiddenField runat="server" ID="hifQuotationTemplateName" />
                                            </span>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-12 --no-padding-leftright">
                                            <div class="progress">
                                                <div class="progress-bar progress-bar-success progress-bar-striped" role="progressbar" aria-valuenow="" aria-valuemin="0" aria-valuemax="100">
                                                    <span class="sr-only"></span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        <asp:Button runat="server" ID="btnIssueQuotation" CssClass="btn btn-primary" Text="Issue" OnClick="btnIssueQuotation_Click"></asp:Button>
                    </div>
                </div>
            </div>
        </div>--%>
    <div class="modal fade modal-issuequotation" tabindex="-1" role="dialog" aria-labelledby="gridSystemModalLabel" data-backdrop="true" data-keyboard="true">
        <div class="modal-dialog" role="document" style="width: 60vw; height: 50vh">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h3 class="modal-title">Issue Quotation</h3>
                </div>
                <div class="modal-body">
                    <iframe src="/Modules/Sails/Admin/QQuotationIssue.aspx?NodeId=1&SectionId=15" frameborder="0" width="90%" style="height: 45vh" scrolling="no"></iframe>
                </div>
            </div>
        </div>
    </div>
    </div>
    <div class="modal fade meeting-modal" id="addMeetingModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog meeting-modal__modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title --text-bold" id="myModalLabel">Add meeting / Problem report</h4>
                </div>
                <div class="modal-body">
                    <div data-hidactivityidclientid="<%= hidActivityId.ClientID %>" id="hidActivityIdClientId">
                        <asp:HiddenField runat="server" ID="hidActivityId" />
                    </div>
                    <div data-hidactivityidclientid="<%= hidAgencyContactId.ClientID %>" id="hidAgencyContactIdClientId">
                        <asp:HiddenField runat="server" ID="hidAgencyContactId" />
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <div class="col-xs-2 --no-padding-leftright">
                                Date meeting
                            </div>
                            <div class="col-xs-4 --no-padding-leftright">
                                <asp:TextBox runat="server" ID="txtDateMeeting" CssClass="form-control" placeholder="Date meeting" data-control="datetimepicker" data-id="txtDateMeeting" />
                            </div>
                            <div class="col-xs-2 --no-padding-left --text-right">
                                Type
                            </div>
                            <div class="col-xs-4 --no-padding-leftright">
                                <asp:DropDownList runat="server" ID="ddlType" CssClass="form-control" data-id="ddlType">
                                    <asp:ListItem Text="Meeting">Meeting</asp:ListItem>
                                    <asp:ListItem Text="Problem Report">Problem Report</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="form-group" id="problem-group">
                        <div class="row">
                            <div class="col-xs-2 --no-padding-leftright">
                                Cruise
                            </div>
                            <div class="col-xs-4 --no-padding-leftright">
                                <asp:DropDownList runat="server" ID="ddlCruise" AppendDataBoundItems="true" CssClass="form-control" data-id="ddlCruise">
                                    <asp:ListItem Value="0" Text="-- Cruise --"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-xs-offset-1 col-xs-5 --no-padding-leftright --text-right checkbox-group">
                                <fieldset class="--reset-this" style="padding-top: 5px; padding-bottom: 0">
                                    <legend class="--reset-this" style="line-height: 0">Problems</legend>
                                    <label for="<%= chkFood.ClientID %>" class="--text-normal">
                                        Food
                                <asp:CheckBox runat="server" ID="chkFood" Text="" CssClass="checkbox-group__horizontal" data-id="chkFood"></asp:CheckBox>
                                    </label>
                                    <label for="<%= chkCabin.ClientID %>" class="--text-normal">
                                        Cabin
                                <asp:CheckBox runat="server" ID="chkCabin" Text="" CssClass="checkbox-group__horizontal" data-id="chkCabin"></asp:CheckBox>
                                    </label>
                                    <label for="<%= chkGuide.ClientID %>" class="--text-normal">
                                        Guide
                                <asp:CheckBox runat="server" ID="chkGuide" Text="" CssClass="checkbox-group__horizontal" data-id="chkGuide"></asp:CheckBox>
                                    </label>
                                    <label for="<%= chkBus.ClientID %>" class="--text-normal">
                                        Bus
                                <asp:CheckBox runat="server" ID="chkBus" Text="" CssClass="checkbox-group__horizontal" data-id="chkBus"></asp:CheckBox>
                                    </label>
                                    <label for="<%= chkOthers.ClientID %>" class="--text-normal">
                                        Others
                                <asp:CheckBox runat="server" ID="chkOthers" Text="" CssClass="checkbox-group__horizontal" data-id="chkOthers"></asp:CheckBox>
                                    </label>
                                </fieldset>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <div class="col-xs-2 --no-padding-leftright">
                                Note
                            </div>
                            <div class="col-xs-10 --no-padding-leftright">
                                <asp:TextBox runat="server" ID="txtNote" CssClass="form-control" TextMode="MultiLine" Rows="12" placeholder="Note" Text="" data-id="txtNote" />
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <div class="col-xs-2 --no-padding-leftright">
                                Attachment
                            </div>
                            <div class="col-xs-10 --no-padding-leftright">
                                <asp:FileUpload runat="server" ID="fuAttachment"></asp:FileUpload>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <div class="col-xs-12 --no-padding-leftright --text-right">
                                <label for="<%= chkNeedManagerAttention.ClientID %>" class="--text-normal">Need manager's immediate attention</label>
                                <asp:CheckBox runat="server" ID="chkNeedManagerAttention" Text="" data-id="chkNeedManagerAttention"></asp:CheckBox>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    <asp:Button runat="server" ID="btnSave" OnClick="btnSave_Click" Text="Save" CssClass="btn btn-primary" OnClientClick="return checkDouble(this)" />
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade note-modal" id="addNoteModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog note-modal__modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title --text-bold" id="myModalLabel">Add note</h4>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <div class="row">
                            <div data-hidagencynotesidclientid="<%= hidAgencyNotesId.ClientID %>" id="hidAgencyNotesIdClientId">
                                <asp:HiddenField runat="server" ID="hidAgencyNotesId" />
                            </div>
                            <div class="col-xs-2 --no-padding-leftright">
                                For role
                            </div>
                            <div class="col-xs-10 --no-padding-leftright --width-auto">
                                <asp:DropDownList ID="ddlAgencyNotesRole" runat="server" CssClass="form-control" AppendDataBoundItems="true" data-id="ddlAgencyNotesRole">
                                    <asp:ListItem Value="0" Text="-- Role --"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <div class="col-xs-2 --no-padding-leftright">
                                Note
                            </div>
                            <div class="col-xs-10 --no-padding-leftright">
                                <asp:TextBox runat="server" ID="txtAgencyNotesNote" CssClass="form-control" TextMode="MultiLine" Rows="12" placeholder="Note" Text="" data-id="txtAgencyNotesNote" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    <asp:Button runat="server" ID="btnAgencyNotesSave" OnClick="btnAgencyNotesSave_Click" Text="Save" CssClass="btn btn-primary" OnClientClick="return checkDouble(this)" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="Scripts" runat="server">
    <script type="text/javascript" src="/modules/sails/admin/agencyviewcontroller.js"></script>
    <script type="text/javascript">
        $("[data-type='datetimepicker']").datetimepicker({
            timepicker: false,
            format: 'd/m/Y',
            scrollInput: false,
            scrollMonth: false
        });
    </script>

    <script>
        $(document).ready(function () {
            $("#aspnetForm").validate({
                rules: {
                    <%= txtContractValidFromDate.UniqueID%>: "required",
                    <%= txtContractValidToDate.UniqueID%>:"required",
      <%--              <%= txtQuotationValidFromDate.UniqueID%>:"required",
                    <%= txtQuotationValidToDate.UniqueID%>:"required",--%>
                    <%= txtDateMeeting.UniqueID%>:"required",
                    <%= txtNote.UniqueID%>:"required",
                },
                messages: {
                    <%= txtContractValidFromDate.UniqueID%>: "Yêu cầu chọn ngày valid from",
                    <%= txtContractValidToDate.UniqueID%>:"Yêu cầu chọn ngày valid to",
                    <%--<%= txtQuotationValidFromDate.UniqueID%>:"Yêu cầu chọn ngày valid from",
                    <%= txtQuotationValidToDate.UniqueID%>:"Yêu cầu chọn ngày valid to",--%>
                    <%= txtDateMeeting.UniqueID%>:"Yêu cầu chọn ngày",
                    <%= txtNote.UniqueID%>:"Yêu cầu điền Note",
                },
                errorElement: "em",
                errorPlacement: function (error, element) {
                    error.addClass("help-block");

                    if (element.prop("type") === "checkbox") {
                        error.insertAfter(element.parent("label"));
                    } else {
                        error.insertAfter(element);
                    }

                    if (element.siblings("span").prop("class") === "input-group-addon") {
                        error.insertAfter(element.parent()).css({ color: "#a94442" });
                    }
                },
                highlight: function (element, errorClass, validClass) {
                    $(element).closest("div").addClass("has-error").removeClass("has-success");
                },
                unhighlight: function (element, errorClass, validClass) {
                    $(element).closest("div").removeClass("has-error");
                }
            })
        })
    </script>
    <script>
        $('#<%=fuAttachment.ClientID%>').change(function (e) {
            var uploadSize = e.target.files[0].size;
            if (uploadSize >= 26214400) {
                e.target.value = "";
                alert("File upload too large. Please send file have size <= 25MB");
            }
        })
    </script>
    <script>
        $(document).ready(function () {
            $('#<%= ddlType.ClientID %>').change(function () {
                if ($(this).val() == 'Meeting') $('#problem-group').hide();
                if ($(this).val() == 'Problem Report') $('#problem-group').show();
            })
        })
    </script>
    <script>
        function clearFormMeeting() {
            clearForm($('#addMeetingModal .modal-content'));
            $('[data-id=ddlType]').val('Meeting');
            $('[data-id=ddlType]').trigger('change')
        }
    </script>
    <script>
        function clearFormAgencyNotes(){
            clearForm($('#addNoteModal .modal-content'));
        }
    </script>
</asp:Content>
