<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="QAgentLevelConfig.ascx.cs" Inherits="Portal.Modules.OrientalSails.Web.Controls.QAgentLevelConfig" %>
<%@ Register TagPrefix="ucQuotationPrice" TagName="customer_1" Src="QuotationPriceConfig.ascx" %>
<h4>
    <asp:Literal runat="server" ID="litGroupName"></asp:Literal><asp:HiddenField runat="server" ID="hidGroupId" Value='<%#Eval("Id") %>' />
</h4>
<ul class="nav nav-tabs" role="tablist">
    <li role="presentation" class="active"><a href='#2d1n<%=AgentLevelCode %>' role="tab" data-toggle="tab">2 NGÀY / 1 ĐÊM</a></li>
    <li role="presentation"><a href="#3d2n<%=AgentLevelCode %>" role="tab" data-toggle="tab">3 NGÀY / 2 ĐÊM</a></li>
</ul>
<div class="tab-content">
    <div role="tabpanel" class="tab-pane active" id="2d1n<%=AgentLevelCode %>">
        <ucQuotationPrice:customer_1 ID="quotationPrice2Day" runat="server" />

    </div>
    <div role="tabpanel" class="tab-pane" id="3d2n<%=AgentLevelCode %>">
        <ucQuotationPrice:customer_1 ID="quotationPrice3Day" runat="server" />
    </div>
</div>
<style>
    .input-group input[type="text"]{width: 90px}
</style>