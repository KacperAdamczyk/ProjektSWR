"use strict";
var controller = require("./controller");
var alertifyjs = require("alertifyjs");
function prepareSentDocument() {
    $.getJSON("/Messages/SentMessageHeaders", parseSentMessages);
    $("#delete_selected_btn").click(function () { deleteMessages(); });
    $("#select_all").click(function () {
        if ($("#select_all").is(":checked"))
            $("input:checkbox").prop("checked", true);
        else
            $("input:checkbox").prop("checked", false);
    });
    setInterval(function () {
    }, 1000);
}
exports.prepareSentDocument = prepareSentDocument;
function parseSentMessages(data) {
    data = JSON.parse(data);
    var i, j, line;
    if (data.length == 0) {
        line = "<tr>" + "<td colspan='5'>" + "Brak wiadomości" + "</td>" + "</tr>";
        $(".sent_table").append(line);
    }
    for (i = 0; i < data.length; i++) {
        var sentDate = new Date(data[i].SendDate).toLocaleString();
        var recipients = "";
        var receivedDate = "";
        for (j = 0; j < data[i].Recipient.length; j++) {
            recipients += data[i].Recipient[j] + "<br />";
            if (data[i].ReceivedDate[j] != null) {
                receivedDate += new Date(data[i].ReceivedDate[j]).toLocaleString() + "<br />";
            }
            else {
                receivedDate += "Nie odczytano" + "<br />";
            }
        }
        line = "<tr id='" + data[i].Id + "'>" +
            "<td>" + "<input type='checkbox' id='cb" + data[i].Id + "'>" + "</td>" +
            "<td>" + recipients + "</td>" +
            "<td>" + data[i].Subject + "</td>" +
            "<td>" + sentDate + "</td>" +
            "<td>" + receivedDate + "</td>" +
            "</tr>";
        $(".sent_table").append(line);
        var tr = $("#" + data[i].Id);
        tr.click(function () { controller.loadContent(this.id, "sent"); });
        tr.first().children().first().click(function (e) { e.stopPropagation(); });
    }
    $(controller.transitor).addClass(controller.transitorAcrivated);
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
            url: "/Messages/DeleteSent",
            method: "POST",
            data: { "id": selectedMessageIds },
            success: function () { controller.loadSent(); }
        });
    }, function () { });
}
