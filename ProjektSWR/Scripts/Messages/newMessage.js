"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var input = require("./newMessage_input");
var controller = require("./controller");
function prepareNewMessageDocument() {
    input.loadContentInput();
    input.loadTo();
    $.getJSON("/Messages/Users", input.parseUsers);
    $("#send_button").click(function () { sendMessage(); });
}
exports.prepareNewMessageDocument = prepareNewMessageDocument;
function sendMessage() {
    var receiver = $('#users_combobox').val();
    var s = $("#Subject").val();
    var c = input.quill_editor.getContents();
    var message = { "UserName": receiver, "Subject": s, "Content": JSON.stringify(c) };
    $.ajax({
        url: "/Messages/CreateMessage",
        type: "POST",
        data: message,
        success: function () { controller.loadInbox(); },
        error: function () { console.log(this.textStatus); }
    });
}
