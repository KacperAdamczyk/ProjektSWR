"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var controller = require("./controller");
var Quill = require("quill");
require("quill/dist/quill.snow.css");
var g_data;
function messageContent(id, type) {
    switch (type) {
        case "inbox":
            $("#delete_selected_btn").click(function () { deleteMessageInbox(id); });
            break;
        case "sent":
            $("#delete_selected_btn").click(function () { deleteMessageSent(id); });
            break;
    }
    $.getJSON("/Messages/MessageContent?id=" + id, parseContent);
}
exports.messageContent = messageContent;
function parseContent(data) {
    g_data = JSON.parse(data);
    g_data = JSON.parse(g_data);
    dispalyContent();
}
function getData() {
    return g_data;
}
exports.getData = getData;
function dispalyContent() {
    var toolbarOptions = [];
    var quill = new Quill('#messageContent', {
        theme: 'snow',
        modules: {
            toolbar: toolbarOptions
        }
    });
    console.log(g_data);
    quill.setContents(g_data);
    quill.disable();
}
function deleteMessageInbox(id) {
    $.ajax({
        url: "/Messages/DeleteInbox",
        method: "POST",
        data: { "id": id },
        success: function () { controller.loadInbox(); }
    });
}
function deleteMessageSent(id) {
    $.ajax({
        url: "/Messages/DeleteSent",
        method: "POST",
        data: { "id": id },
        success: function () { controller.loadSent(); }
    });
}
