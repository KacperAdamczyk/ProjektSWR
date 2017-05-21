"use strict";
var $ = require("jquery");
var inbox = require("./inbox");
var sent = require("./sent");
var new_message = require("./newMessage");
var content = require("./MessageContent");
exports.globalContainer = "#content";
$(document).ready(function () {
    loadInbox(); // domyślna zakładka
    $("#new_message").click(function () { loadNewMessage(); });
    $("#inbox").click(function () { loadInbox(); });
    $("#sent").click(function () { loadSent(); });
});
function changeActive(li) {
    var tags = ["inbox", "sent", "new_message"];
    var activeClass = "active";
    var i;
    for (i = 0; i < tags.length; i++) {
        $("#" + tags[i]).removeClass(activeClass);
    }
    var pos = tags.indexOf(li);
    if (pos >= 0) {
        $("#" + li).addClass(activeClass);
    }
}
function load(label, url, fun) {
    if (label != null)
        changeActive(label);
    $.ajax({
        url: "/Messages/" + url,
        success: function (data) {
            $(exports.globalContainer).html(data);
            fun();
        }
    });
}
function loadNewMessage() {
    load("new_message", "Create", new_message.prepareNewMessageDocument);
}
exports.loadNewMessage = loadNewMessage;
function loadInbox() {
    load("inbox", "Inbox", inbox.prepareInboxDocument);
}
exports.loadInbox = loadInbox;
function loadSent() {
    load("sent", "Sent", sent.prepareSentDocument);
}
exports.loadSent = loadSent;
function loadContent(id, type) {
    load(null, "Content", function () { content.messageContent(id, type); });
}
exports.loadContent = loadContent;
