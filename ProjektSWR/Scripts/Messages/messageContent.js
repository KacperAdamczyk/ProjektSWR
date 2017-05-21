"use strict";
function messageContent(id) {
    $.getJSON("/Messages/Content?id=" + id, parseDetails);
}
exports.messageContent = messageContent;
function parseDetails(data) {
    data = JSON.parse(data);
    console.log(data);
    //$(globalContainer).html(data);
}
