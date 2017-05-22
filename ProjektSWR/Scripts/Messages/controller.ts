import * as $ from "jquery";
import * as inbox from "./inbox";
import * as sent from "./sent";
import * as new_message from "./newMessage";
import * as content from "./MessageContent";

export const globalContainer = "#content";

$(document).ready(function () {
    loadInbox(); // domyślna zakładka
    $("#new_message").click(function() { loadNewMessage(null, null); });
    $("#inbox").click(function() { loadInbox(); });
    $("#sent").click(function() { loadSent(); });
});

function changeActive(li) {
    var tags = ["inbox", "sent", "new_message"];
    const activeClass = "active";
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
            $(globalContainer).html(data);
            fun();
        }
    });
    
}

export function loadNewMessage(responseTo : string, responseToId : number) {
    load("new_message", "Create", function() { new_message.prepareNewMessageDocument(responseTo, responseToId) });
}

export function loadInbox() {
    load("inbox", "Inbox", inbox.prepareInboxDocument);
}

export function loadSent() {
    load("sent", "Sent", sent.prepareSentDocument);
}

export function loadContent(id : number, type : string) {
    load(null, "Content", function() { content.prepareMessageContentDocument(id, type); });
}