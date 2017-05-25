"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var controller = require("./controller");
var Quill = require("quill");
require("quill/dist/quill.snow.css");
var g_data;
var g_type;
function prepareMessageContentDocument(id, type) {
    g_type = type;
    switch (type) {
        case "inbox":
            $("#delete_selected_btn").click(function () { deleteMessageInbox(id); });
            break;
        case "sent":
            $("#delete_selected_btn").click(function () { deleteMessageSent(id); });
            $("#response_btn").hide();
            break;
    }
    $.getJSON("/Messages/MessageContent?id=" + id, parseContent);
}
exports.prepareMessageContentDocument = prepareMessageContentDocument;
function parseContent(data) {
    g_data = JSON.parse(data);
    switch (g_type) {
        case "inbox":
            $("#response_btn").click(function () { controller.loadNewMessage(g_data.Sender, g_data.Id); });
            if (g_data.ResponseId >= 0) {
                $("#go_to_response").click(function () { prepareMessageContentDocument(g_data.ResponseId, "inbox"); });
            }
            else {
                $("#go_to_response").hide();
            }
            break;
        case "sent":
            $("#response_btn").hide();
            if (g_data.ResponseId >= 0) {
                $("#go_to_response").click(function () { prepareMessageContentDocument(g_data.ResponseId, "sent"); });
            }
            else {
                $("#go_to_response").hide();
            }
            break;
    }
    dispalyContent();
}
function dispalyContent() {
    var toolbarOptions = [];
    var quill = new Quill('#messageContent', {
        theme: 'snow',
        modules: {
            toolbar: toolbarOptions
        }
    });
    $(".ql-toolbar").hide();
    quill.setContents(JSON.parse(g_data.Content));
    quill.disable();
    $(controller.transitor).addClass(controller.transitorAcrivated);
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
