"use strict";
var input = require("./newMessage_input");
var controller = require("./controller");
var alertifyjs = require("alertifyjs");
require("alertifyjs/build/css/alertify.css");
require("alertifyjs/build/css/themes/bootstrap.css");
function prepareNewMessageDocument(responseTo, responseToId) {
    input.loadContentInput();
    $.getJSON("/Messages/Users", function (data) {
        if (responseTo == null)
            input.parseUsers(data, true);
        else
            input.parseUsers(data, false);
    });
    if (responseToId == null) {
        $("#send_button").click(function () { sendMessage(-1); });
        $("#add_user").click(function () { input.createCombobox(); });
    }
    else {
        $("#add_user").hide();
        $("#send_button").click(function () { sendMessage(responseToId); });
        var c = "<input list='users" + "' class='users_combobox'>" +
            "<datalist id='users" + "'></datalist>";
        $("#comboboxes").append(c);
        var line = '<option' + ' data-id="user' + 0 + ' value="' + responseTo + '">' + responseTo + '</option>';
        $("#users").append(line);
        $(".users_combobox").first().val(responseTo);
    }
    $(controller.transitor).addClass(controller.transitorAcrivated);
}
exports.prepareNewMessageDocument = prepareNewMessageDocument;
function sendMessage(responseId) {
    var recipients = getAllRecipients();
    var s = $("#Subject").val();
    var c = input.quill_editor.getContents();
    var message = { "UserName": recipients, "Subject": s, "Content": JSON.stringify(c), "ResponseId": responseId };
    $.ajax({
        url: "/Messages/CreateMessage",
        type: "POST",
        data: message,
        success: function () {
            alertifyjs.success("Wiadomość została wysłana");
            controller.loadInbox();
        },
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
