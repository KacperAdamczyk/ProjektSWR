"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var controller = require("./controller");
function prepareInboxDocument() {
    $.getJSON("/Messages/MessageHeaders", parseMessages);
    $("#delete_selected_btn").click(function () { deleteMessages(); });
    $("#select_all").click(function () {
        if ($("#select_all").is(":checked"))
            $("input:checkbox").prop("checked", true);
        else
            $("input:checkbox").prop("checked", false);
    });
}
exports.prepareInboxDocument = prepareInboxDocument;
function parseMessages(data) {
    data = JSON.parse(data);
    var i, line, full;
    if (data.length == 0) {
        line = "<tr>" + "<td colspan='4'>" + "Brak wiadomości" + "</td>" + "</tr>";
        $(".inbox_table").append(line);
    }
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
        full += line;
        var tr = $("#" + data[i].Id);
        tr.click(function () { controller.loadContent(this.id, "inbox"); });
        tr.first().children().first().click(function (e) { e.stopPropagation(); });
    }
    $(".inbox_table").append(full);
}
function deleteMessages() {
    var selectedMessages = $("input:checkbox:checked");
    var selectedMessageIds = [];
    var i;
    for (i = 1; i < selectedMessages.length; i++) {
        selectedMessageIds.push(Number(selectedMessages[i].id.substr(2)));
    }
    $.ajax({
        url: "/Messages/DeleteInbox",
        method: "POST",
        data: { "id": selectedMessageIds },
        success: function () { controller.loadInbox(); }
    });
}
