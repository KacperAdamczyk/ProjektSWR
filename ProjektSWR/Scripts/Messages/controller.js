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
    var pos = tags.findIndex(function (element) { return element === li; });
    if (pos >= 0) {
        $("#" + li).addClass(activeClass);
    }
}

function load(label, url, fun) {
    changeActive(label);

    var xhttp = new XMLHttpRequest();
    xhttp.open("GET", "/Messages/" + url, true);
    xhttp.send();

    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            $(globalContainer).html(this.responseText);
            fun();
        }
    }
}

function loadNewMessage() {
    load("new_message", "Create", prepareNewMessageDocument);
}

function loadInbox() {
    onExitNewMessageDocument();
    load("inbox", "Inbox", prepareInboxDocument);
}

function loadSent() {

}