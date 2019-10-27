<%@ Page Title="Create Quotation" Language="C#" MasterPageFile="MO.Master" AutoEventWireup="true" CodeBehind="QQuotationEdit.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.QQuotationEdit" %>

<%@ Register TagPrefix="ucAgentLevelConfig" TagName="agent" Src="../Controls/QAgentLevelConfig.ascx" %>

<asp:Content ID="AdminContent" ContentPlaceHolderID="AdminContent" runat="server">
    <h3 class="page-header">Create Quotation</h3>
    <div class="quotation-panel">
        <div class="form-group">
            <div class="row">
                <div class="col-xs-1">
                    Group cruise
                </div>
                <div class="col-xs-4">
                    <asp:DropDownList AutoPostBack="True" OnSelectedIndexChanged="ddlGroupCruise_OnSelectedIndexChanged" ID="ddlGroupCruise" runat="server" CssClass="form-control">
                    </asp:DropDownList>
                </div>
                <div class="col-xs-1">
                    Valid From
                </div>
                <div class="col-xs-2">
                    <asp:TextBox runat="server" ID="txtValidFrom" CssClass="form-control" autocomplete="off" placeholder="Valid From (dd/mm/yyyy)" />
                </div>
                <div class="col-xs-1">
                    Valid To
                </div>
                <div class="col-xs-2">
                    <asp:TextBox runat="server" ID="txtValidTo" CssClass="form-control" autocomplete="off" placeholder="Valid To (dd/mm/yyyy)" />
                </div>
            </div>
        </div>
        <br />
        <div>
            <ul class="nav nav-tabs" role="tablist">
                <li role="presentation" class="active"><a href="#agentlv1" role="tab" data-toggle="tab">Agent lv 1</a></li>
                <li role="presentation"><a href="#agentlv2" role="tab" data-toggle="tab">Agent lv 2</a></li>
                <li role="presentation"><a href="#agentlv3" role="tab" data-toggle="tab">Agent lv 3</a></li>
            </ul>
            <div class="tab-content">
                <div role="tabpanel" class="tab-pane active" id="agentlv1">
                    <ucAgentLevelConfig:agent ID="ucAgentLevel1" runat="server" />

                </div>
                <div role="tabpanel" class="tab-pane" id="agentlv2">
                    <ucAgentLevelConfig:agent ID="ucAgentLevel2" runat="server" />
                </div>
                <div role="tabpanel" class="tab-pane" id="agentlv3">
                    <ucAgentLevelConfig:agent ID="ucAgentLevel3" runat="server" />
                </div>
            </div>
        </div>
        <asp:Button runat="server" ID="btnCreateQuotation" OnClick="btnCreateQuotation_OnClick" Text="Save quotation" CssClass="btn btn-primary" />
    </div>
</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="Scripts" runat="server">
    <script>
        $("#<%= txtValidFrom.ClientID %>").datetimepicker({
            timepicker: false,
            format: 'd/m/Y',
            scrollInput: false,
            scrollMonth: false,
        })

        $("#<%= txtValidTo.ClientID %>").datetimepicker({
            timepicker: false,
            format: 'd/m/Y',
            scrollInput: false,
            scrollMonth: false,
        })
        $("#aspnetForm").validate({
            rules: {
                <%=ddlGroupCruise.UniqueID %> : {
                    required : true,
                } ,   
                <%=txtValidFrom.UniqueID%> : {
                    required : true,
                },
                <%=txtValidTo.UniqueID%> : {
                    required : true
                },
            },
            messages : {
                <%=ddlGroupCruise.UniqueID %> : {
                    required : "Yêu cầu chọn nhóm tàu",
                } ,   
                <%=txtValidFrom.UniqueID%> : {
                    required : "Yêu cầu chọn ngày Valid from",
                },
                <%=txtValidTo.UniqueID%> : {
                    required : "Yêu cầu chọn ngày Valid to"
                },
            },  errorElement: "em",
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
    </script>
    <style>
        .nav-tabs > li > a {
            background: #cdcdcd;
        }
    </style>
</asp:Content>

