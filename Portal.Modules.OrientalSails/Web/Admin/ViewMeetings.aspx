<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="MO.Master"
    CodeBehind="ViewMeetings.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.ViewMeetings" %>

<%@ Register Assembly="CMS.ServerControls" Namespace="CMS.ServerControls" TagPrefix="svc" %>
<%@ Import Namespace="Portal.Modules.OrientalSails.Domain" %>
<asp:Content ID="Head" ContentPlaceHolderID="Head" runat="server">
    <title>View meeting</title>
</asp:Content>
<asp:Content ID="AdminContent" ContentPlaceHolderID="AdminContent" runat="server">
    <div class="search-panel">
        <div class="form-group">
            <div class="row">
                <div class="col-xs-1 --no-padding-right --width-auto">
                    From   
                </div>
                <div class="col-xs-2 --no-padding-right --width-auto">
                    <asp:TextBox ID="txtFrom" runat="server" CssClass="form-control" placeholder="From (dd/mm/yyyy)" data-toggle="tooltip" data-replacement="top" title="Date meeting"> 
                    </asp:TextBox>
                </div>
                <div class="col-xs-1 --no-padding-right --width-auto">
                    To
                </div>
                <div class="col-xs-2 --no-padding-right --width-auto">
                    <asp:TextBox ID="txtTo" runat="server" CssClass="form-control" placeholder="To (dd/mm/yyyy)" data-toggle="tooltip" data-replacement="top" title="Date meeting">
                    </asp:TextBox>
                </div>
                <asp:PlaceHolder runat="server" ID="plhSales">
                    <div class="col-xs-1 --no-padding-right --width-auto">
                        Sales
                    </div>
                    <div class="col-xs-3 --width-auto">
                        <asp:DropDownList runat="server" ID="ddlSales" CssClass="form-control" />
                    </div>
                </asp:PlaceHolder>
            </div>
        </div>
        <div class="form-group">
            <div class="row">
                <div class="col-xs-1 --no-padding-right --width-auto">
                    <asp:Button runat="server" ID="btnView" CssClass="btn btn-primary"
                        Text="Search" OnClick="btnView_OnClick"></asp:Button>
                </div>
                <div class="col-xs-2 --no-padding-leftright --width-auto">
                    <asp:TextBox ID="txtPageSize" runat="server" CssClass="form-control" Style="display: inline-block; width: 20%"></asp:TextBox>
                    meetings/page
                </div>
            </div>
        </div>
    </div>
    <div class="meeting-panel">
        <div class="row">
            <div class="col-xs-12" ng-controller="getActivityByIdController">
                <table class="table table-bordered table-common">
                    <tbody>
                        <asp:Repeater ID="rptMeetings" runat="server" OnItemDataBound="rptMeetings_OnItemDataBound" OnItemCommand="rptMeetings_ItemCommand">
                            <HeaderTemplate>
                                <tr class="active">
                                    <th>No</th>
                                    <th>
                                        <asp:LinkButton runat="server" ID="lbtDateMeeting" OnClick="lbtDateMeeting_OnClick"
                                            ToolTip="Click to sort descending or ascending">Date</asp:LinkButton>
                                        <asp:Image runat="server" ID="imgSortDmStatus" Width="8px" Visible="False" />
                                    </th>
                                    <th style="width: 10%" runat="server" id="thSales">Sales
                                    </th>
                                    <th style="width: 8%">Type</th>
                                    <th>Agency
                                    </th>
                                    <th>Contact
                                    </th>
                                    <th>Position
                                    </th>
                                    <th>View meeting
                                    </th>
                                    <th style="width: 7%">
                                        <asp:LinkButton runat="server" ID="lbtUpdateTime" OnClick="lbtUpdateTime_OnClick"
                                            ToolTip="Click to sort descending or ascending">Update time</asp:LinkButton>
                                        <asp:Image runat="server" ID="imgSortUtStatus" Width="8px" Visible="False" />
                                    </th>
                                    <th style="width: 4%"></th>
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td><%# Container.ItemIndex + 1 %></td>
                                    <td>
                                        <asp:Literal runat="server" ID="ltrUpdateTime" />
                                    </td>

                                    <td runat="server" id="tdSales">
                                        <asp:Literal runat="server" ID="ltrSale" />
                                    </td>
                                    <td><%# ((Activity)Container.DataItem).Type%></td>
                                    <td><a href="AgencyView.aspx?NodeId=1&SectionId=15&agencyid=<%#((Activity)Container.DataItem).Params %>">
                                        <%# AgencyGetById(((Activity)Container.DataItem).Params)!= null ? AgencyGetById(((Activity)Container.DataItem).Params).Name : "" %></td>
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
                                        <asp:Literal runat="server" ID="ltrDateMeeting" />
                                    </td>
                                    <td class="--text-right">
                                        <div class="button-group">
                                            <asp:LinkButton runat="server" ID="lbtDownload" Visible="<%# !String.IsNullOrEmpty(((Activity)Container.DataItem).Attachment) ? true : false %>" CommandName="Download" CommandArgument="<%#((Activity)Container.DataItem).Attachment + ',' + ((Activity)Container.DataItem).AttachmentContentType %>">
                                                        <i class="fa fa-lg fa-file-download icon icon__download" data-toggle="tooltip" title="Download"></i></asp:LinkButton>
                                            <a href="" data-toggle="modal" data-target="#addMeetingModal" ng-click="getActivityById(<%#(((Activity)Container.DataItem).Id)%>)"
                                                onclick="$('#addMeetingModal .modal-title').html('Edit <%#(((Activity)Container.DataItem).Type)%>');clearFormMeeting()">
                                                <i class="fa fa-lg fa-edit icon icon__edit" data-toggle="tooltip" title="Edit"></i>
                                            </a>
                                        </div>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </div>
        </div>
        <div class="form-group">
            <div class="row">
                <div class="col-xs-2">
                    <asp:Button runat="server"
                        Text="Export"
                        OnClick="btnExportMeetings_OnClick" CssClass="btn btn-primary"></asp:Button>
                </div>
                <div class="col-xs-8">
                    <div class="pager">
                        <svc:Pager ID="pagerMeetings" runat="server" HideWhenOnePage="True" ShowTotalPages="True"
                            ControlToPage="rptMeetings" OnPageChanged="pagerMeetings_OnPageChanged" PageSize="20" />
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
                    <div class="form-group">
                        <div class="row" data-hidactivityidclientid="<%= hidActivityId.ClientID %>" id="hidActivityIdClientId">
                            <asp:HiddenField runat="server" ID="hidActivityId" />
                            <div class="col-xs-2 --no-padding-leftright">
                                Agency
                            </div>
                            <div class="col-xs-10 --no-padding-leftright">
                                <input type="text" data-id="hidGuideId" style="display: none" name="txtAgencyId" />
                                <input type="text" placeholder="Select agency" readonly class="form-control" data-toggle="modal" data-target=".modal-selectGuide"
                                    data-url="AgencySelectorPage.aspx?NodeId=1&SectionId=15" onclick="setTxtGuideClicked(this)"
                                    data-id="txtName" name="txtAgency">
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <div class="col-xs-2 --no-padding-leftright">
                                Contact
                            </div>
                            <div class="col-xs-4 --no-padding-leftright">
                                <select class="form-control" name="ddlContact" data-id="ddlContact">
                                    <option value="0">-- Contact --</option>
                                </select>
                            </div>
                            <div class="col-xs-2 --no-padding-left --text-right">
                                Position
                            </div>
                            <div class="col-xs-4 --no-padding-leftright">
                                <asp:TextBox runat="server" ID="txtPosition" CssClass="form-control" placeholder="Position" disabled="disabled" data-id="txtPosition" />
                            </div>
                        </div>
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
    <div class="modal fade modal-selectGuide" tabindex="-1" role="dialog" aria-labelledby="gridSystemModalLabel">
        <div class="modal-dialog" role="document" style="width: 1230px">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h3 class="modal-title">Select agency</h3>
                </div>
                <div class="modal-body">
                    <iframe frameborder="0" width="1200" scrolling="no" onload="resizeIframe(this)" src=""></iframe>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="Scripts" runat="server">
    <script type="text/javascript" src="/modules/sails/admin/viewmeetingscontroller.js"></script>
    <script>
        $("#<%= txtFrom.ClientID %>").datetimepicker({
            timepicker: false,
            format: 'd/m/Y',
            scrollInput: false,
            scrollMonth: false,
        })

        $("#<%= txtTo.ClientID %>").datetimepicker({
            timepicker: false,
            format: 'd/m/Y',
            scrollInput: false,
            scrollMonth: false,
        })
    </script>
    <script>
        $('.modal-selectGuide').on('shown.bs.modal', function () {
            $(".modal-selectGuide iframe").attr('src', 'AgencySelectorPage.aspx?NodeId=1&SectionId=15')
        })

        var txtGuideClicked = null;
        var rowGuideSelected = null;
        var txtGuideNameSelected = null;
        var txtPhoneSelected = null;
        var hidGuideIdSelected = null;
        function setTxtGuideClicked(txtGuide) {
            txtGuideClicked = txtGuide;
            if (typeof (txtGuideClicked) != "undefined") {
                rowGuideSelected = $(txtGuideClicked).closest(".row");
            }
        }

        var selectGuideIframe = $(".modal-selectGuide iframe");
        selectGuideIframe.on("load", function () {
            //giữ vị trí của scroll khi sang trang mới -- chức năng của phần selectguide
            if (window.name.search('^' + location.hostname + '_(\\d+)_(\\d+)_') == 0) {
                var name = window.name.split('_');
                $(".modal-selectGuide").scrollLeft(name[1]);
                $(".modal-selectGuide").scrollTop(name[2]);
                window.name = name.slice(3).join('_');
            }
            $(".pager a", selectGuideIframe.contents()).click(function () {
                window.name = location.hostname + "_" + $(".modal-selectGuide").scrollLeft() + "_" + $(".modal-selectGuide").scrollTop() + "_";
            })
            //--

            //chức năng select agency bằng popup
            $("[data-id = 'txtName']", selectGuideIframe.contents()).click(function () {
                if (typeof (txtGuideClicked) != "undefined") {
                    $(txtGuideClicked).val($(this).text())
                }
                if (typeof (rowGuideSelected) != "undefined") {
                    txtGuideNameSelected = $(rowGuideSelected).find("[data-id='txtName']");
                    txtPhoneSelected = $(rowGuideSelected).find("[data-id='txtPhone']");
                    hidGuideIdSelected = $(rowGuideSelected).find("[data-id='hidGuideId']");
                }
                if (typeof (txtPhoneSelected) != "undefined") {
                    $(txtPhoneSelected).val($(this).attr("data-phone"))
                }
                if (typeof (hidGuideIdSelected) != "undefined") {
                    $(hidGuideIdSelected).val($(this).attr("data-agencyid"));
                }
                if (typeof (txtGuideNameSelected) != "undefined") {
                    $(txtGuideNameSelected).val($(this).text())
                }
                $('.modal-selectGuide').modal('hide')
                $(hidGuideIdSelected).trigger('input');
                $(hidGuideIdSelected).trigger('change');
                $(txtGuideNameSelected).trigger('input');
                $(txtGuideNameSelected).trigger('change');
            });
            //--
        })
    </script>
    <script>
        $("[name = txtAgencyId]").change(function () {
            $('[name = ddlContact]').find('option:not(:first)').remove();
            $.ajax({
                type: 'POST',
                url: 'WebMethod/DashBoardWebService.asmx/AgencyContactGetByAgencyId',
                data: "{ 'ai': '" + $(this).val() + "'}",
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
            }).done(function (data) {
                var agencyContacts = JSON.parse(data.d);
                $.each(agencyContacts, function (i, agencyContact) {
                    $('[name = ddlContact]')
                        .append($('<option>', {
                            value: agencyContact.Id,
                            text: agencyContact.Name,
                            _position: agencyContact.Position,
                        }));
                });
            })
        })
        $('[name = ddlContact]').change(function () {
            $('#<%= txtPosition.ClientID %>').val($(this).find('option:selected').attr('_position'));
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
        $(document).ready(function () {
            $("#aspnetForm").validate({
                rules: {
                    txtAgency: "required",
                    <%= txtDateMeeting.UniqueID%>: "required",
                     <%= txtNote.UniqueID%> : "required",
                 },
                 messages: {
                     txtAgency: "Yêu cầu chọn một Agency",
                     <%= txtDateMeeting.UniqueID%>: "Yêu cầu chọn ngày",
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
</asp:Content>
