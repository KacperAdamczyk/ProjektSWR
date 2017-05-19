import * as $ from "jquery";
import * as inbox from "./inbox";
import * as sent from "./sent";
import * as new_message from "./newMessage";

export const globalContainer = "#content";

$(document).ready(function () {
    loadInbox(); // domyślna zakładka
    $("#new_message").click(function() {loadNewMessage();})
    $("#inbox").click(function() {loadInbox();})
    $("#sent").click(function() {loadSent();})
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
    changeActive(label);
    $.ajax({
        url: "/Messages/" + url,
        success: function (data) {
            $(globalContainer).html(data);
            fun();
        }
    });
    
}

export function loadNewMessage() {
    load("new_message", "Create", new_message.prepareNewMessageDocument);
}

export function loadInbox() {
    new_message.onExitNewMessageDocument();
    load("inbox", "Inbox", inbox.prepareInboxDocument);
}

export function loadSent() {
    new_message.onExitNewMessageDocument();
    load("sent", "Sent", sent.prepareSentDocument);
}