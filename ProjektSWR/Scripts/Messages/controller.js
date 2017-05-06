const globalContainer = "#content";

$(document).ready(function () {
    loadInbox(); // domyślna zakładka
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

function loadNewMessage() {
    load("new_message", "Create", prepareNewMessageDocument);
}

function loadInbox() {
    onExitNewMessageDocument();
    load("inbox", "Inbox", prepareInboxDocument);
}

function loadSent() {
    onExitNewMessageDocument();
    load("sent", "Sent", prepareSentDocument);
}