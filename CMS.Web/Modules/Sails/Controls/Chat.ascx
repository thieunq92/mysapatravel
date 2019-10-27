<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Chat.ascx.cs" Inherits="Portal.Modules.OrientalSails.Web.Controls.Chat" %>
<link rel="stylesheet" href="/css/FontAwesome/v4.7.0/font-awesome.min.css">
<link rel="stylesheet" href="/Chat/css/chat.css" />
<script type="text/javascript" src="/Chat/js/emoji/emojionearea.js"></script>
<script src="/Chat/js/emoji/emojione.js"></script>
<link rel="stylesheet" href="/Chat/js/emoji/emojionearea.min.css" />
<link rel="stylesheet" href="/Chat/js/emoji/emojione.css" />
<script src="/Chat/js/ajaxChat.js"></script>
<div id="chatList" class="chat_box">
    <div class="chat_head">
        <span class="msg_name">Chat list</span>
        <span id="addNewGroup" title="Thêm nhóm" class="msg_action"><i class="fa fa-users"></i></span>
    </div>
    <div id="divusers" class="chat_body">
    </div>
</div>
<input id="hdConnectionId" type="hidden" />
<input id="hdFullName" type="hidden" />
<input id="hdUserId" type="hidden" />
<div id="groupList" class="draggable" style="display: none">
    <div class="group-head">
        <span class="group-name" id="groupname"></span>
        <span id="closedGroupSetting" title="Đóng" class="msg_action"><i class="fa fa-times fa-2"></i></span>
        <span id="editGroupName" title="Sửa tên nhóm" class="msg_action"><i class="fa fa-edit"></i></span>

    </div>
    <div class="group-body">
        <div id="memInGroup">
            <div class="mem_grout_title"><span class="msg_name">Thành viên trong nhóm</span></div>
            <div id="inGrouUser">
            </div>
        </div>
        <div id="memOutGroup">
            <div class="mem_grout_title"><span class="msg_name">Thêm thành viên vào nhóm</span></div>
            <div id="outGrouUser">
            </div>
        </div>
    </div>
</div>