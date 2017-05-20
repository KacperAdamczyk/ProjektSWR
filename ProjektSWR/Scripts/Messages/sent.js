"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var messageContent_1 = require("./messageContent");
function prepareSentDocument() {
    $.getJSON("/Messages/SentMessageHeaders", parseSentMessages);
}
exports.prepareSentDocument = prepareSentDocument;
function parseSentMessages(Jdata) {
    Jdata = JSON.parse(Jdata);
    console.log(Jdata);
    var i, line;
    for (i = 0; i < Jdata.length; i++) {
        var sentDate = new Date(Jdata[i].SendDate).toLocaleString();
        if (Jdata[i].ReceivedDate != null) {
            var receivedDate = new Date(Jdata[i].ReceivedDate).toLocaleString();
        }
        else {
            var receivedDate = "Nie odczytano";
        }
        line = "<tr id='" + Jdata[i].Id + "'><td>" + Jdata[i].UserName + "</td><td>" + Jdata[i].Subject +
            "</td><td>" + sentDate + "</td><td>" + receivedDate + "</td></tr>";
        $(".sent_table").append(line);
        $("#" + Jdata[i].Id).click(function () { messageContent_1.messageContent(this.id); });
    }
}
