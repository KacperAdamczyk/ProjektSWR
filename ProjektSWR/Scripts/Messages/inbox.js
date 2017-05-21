"use strict";
var controller = require("./controller");
var messageContent_1 = require("./messageContent");
function prepareInboxDocument() {
    getMessages();
    $("#delete_selected_btn").click(function () { deleteMessages(); });
}
exports.prepareInboxDocument = prepareInboxDocument;
function getMessages() {
    $.getJSON("/Messages/MessageHeaders", parseMessages);
}
function parseMessages(data) {
    data = JSON.parse(data);
    console.log(data);
    var i, line;
    for (i = 0; i < data.length; i++) {
        var newMessage = false;
        var sentDate = new Date(data[i].SendDate).toLocaleString();
        if (data[i].ReceivedDate != null) {
            var receivedDate = new Date(data[i].ReceivedDate).toLocaleString();
        }
        else {
            var receivedDate = "Nie odczytano";
            newMessage = true;
        }
        line = "<tr id='" + data[i].Id + (newMessage ? "' class='new_message_row'>" : "'>") +
            "<td>" + "<input type='checkbox' id='cb" + data[i].Id + "'>" + "</td>" +
            "<td>" + data[i].Sender + "</td>" +
            "<td>" + data[i].Subject + "</td>" +
            "<td>" + sentDate + "</td>" +
            "</tr>";
        $(".inbox_table").append(line);
        var tr = $("#" + data[i].Id);
        tr.click(function () { messageContent_1.messageContent(this.id); });
        tr.first().children().first().click(function (e) { e.stopPropagation(); });
    }
}
function deleteMessages() {
    var selectedMessages = $("input:checkbox:checked");
    var selectedMessageIds = [];
    var i;
    for (i = 0; i < selectedMessages.length; i++) {
        selectedMessageIds.push(Number(selectedMessages[i].id.substr(2)));
    }
    console.log(selectedMessageIds);
    $.ajax({
        url: "/Messages/DeleteInbox",
        method: "POST",
        data: { "id": selectedMessageIds },
        success: function () { controller.loadInbox(); }
    });
}
