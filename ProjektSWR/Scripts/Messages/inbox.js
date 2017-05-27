"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var controller = require("./controller");
var alertifyjs = require("alertifyjs");
var g_data;
var new_msg_cnt = 0;
function prepareInboxDocument() {
    $.getJSON("/Messages/MessageHeaders", parseMessages);
    $("#delete_selected_btn").click(function () { deleteMessages(); });
    $("#select_all").click(function () {
        if ($("#select_all").is(":checked"))
            $("input:checkbox").prop("checked", true);
        else
            $("input:checkbox").prop("checked", false);
    });
    var interval = setInterval(function () {
        updateHeaders();
    }, 5000);
}
exports.prepareInboxDocument = prepareInboxDocument;
function parseMessages(data) {
    data = JSON.parse(data);
    g_data = data;
    var i, line;
    if (data.length == 0) {
        line = "<tr>" + "<td colspan='4'>" + "Brak wiadomości" + "</td>" + "</tr>";
        $(".inbox_table").append(line);
    }
    new_msg_cnt = 0;
    for (i = 0; i < data.length; i++) {
        var newMessage = false;
        var sentDate = new Date(data[i].SendDate).toLocaleString();
        if (data[i].ReceivedDate[0] != null) {
            var receivedDate = new Date(data[i].ReceivedDate).toLocaleString();
        }
        else {
            var receivedDate = "Nie odczytano";
            newMessage = true;
            new_msg_cnt++;
        }
        line = "<tr id='" + data[i].Id + (newMessage ? "' class='new_message_row'>" : "'>") +
            "<td>" + "<input type='checkbox' id='cb" + data[i].Id + "'>" + "</td>" +
            "<td>" + data[i].Sender + "</td>" +
            "<td>" + data[i].Subject + "</td>" +
            "<td>" + sentDate + "</td>" +
            "</tr>";
        $(".inbox_table").append(line);
        var tr = $("#" + data[i].Id);
        tr.click(function () { controller.loadContent(this.id, "inbox"); });
        tr.first().children().first().click(function (e) { e.stopPropagation(); });
    }
    $(controller.transitor).addClass(controller.transitorAcrivated);
    if (new_msg_cnt > 0)
        $("#inbox").html("Skrzynka odbiorcza (" + new_msg_cnt + ")");
    else
        $("#inbox").html("Skrzynka odbiorcza");
}
function updateHeaders() {
    $.getJSON("/Messages/MessageHeaders", function (data) {
        var str_g_data = JSON.stringify(g_data);
        if (str_g_data !== data) {
            $(".inbox_table tr").not(":first-child").remove();
            parseMessages(data);
        }
    });
}
function deleteMessages() {
    var selectedMessages = $("input:checkbox:checked");
    var selectedMessageIds = [];
    var i;
    for (i = 0; i < selectedMessages.length; i++) {
        if (selectedMessages[i].id != "select_all")
            selectedMessageIds.push(Number(selectedMessages[i].id.substr(2)));
    }
    if (selectedMessageIds.length == 0)
        return;
    alertifyjs.confirm("Potwierdzenie", "Czy na pewno chcesz usunąć " + selectedMessageIds.length + (selectedMessageIds.length > 1 ? " wiadomości" : " wiadomość") + "?", function () {
        $.ajax({
            url: "/Messages/DeleteInbox",
            method: "POST",
            data: { "id": selectedMessageIds },
            success: function () { controller.loadInbox(); }
        });
    }, function () { });
}
