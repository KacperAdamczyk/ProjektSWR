"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
function prepareInboxDocument() {
    $.getJSON("/Messages/JgetMessageHeaders", parseMessages);
}
exports.prepareInboxDocument = prepareInboxDocument;
function parseMessages(Jdata) {
    Jdata = JSON.parse(Jdata);
    console.log(Jdata);
    var i, line;
    for (i = 0; i < Jdata.length; i++) {
        line = "<tr><td>" + Jdata[i].UserName + "</td><td>" + Jdata[i].Subject + "</td></tr>";
        $(".is_table").append(line);
    }
}
