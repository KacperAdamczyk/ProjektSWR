"use strict";
var input = require("./newMessage_input");
var controller = require("./controller");
function prepareNewMessageDocument() {
    input.loadContentInput();
    $.getJSON("/Messages/Users", input.parseUsers);
    $("#send_button").click(function () { sendMessage(); });
    $("#add_user").click(function () { input.createCombobox(); });
}
exports.prepareNewMessageDocument = prepareNewMessageDocument;
function sendMessage() {
    var recipients = getAllRecipients();
    var s = $("#Subject").val();
    var c = input.quill_editor.getContents();
    var message = { "UserName": recipients, "Subject": s, "Content": JSON.stringify(c) };
    $.ajax({
        url: "/Messages/CreateMessage",
        type: "POST",
        data: message,
        success: function () { controller.loadInbox(); },
        error: function () { console.log(this.textStatus); }
    });
}
function getAllRecipients() {
    var comboboxes = $(".users_combobox");
    var users = [];
    for (var i = 0; i < comboboxes.length; i++) {
        users.push($(comboboxes[i]).val());
    }
    return users;
}
