function changeActive(li) {
    var tags = ["mail_box", "sent"];
    var activeClass = "active";
    var i;
    for (i = 0; i < tags.length; i++) {
        $("#" + tags[i]).removeClass(activeClass);
    }
    var pos = tags.findIndex(function (element) { return element === li; });
    if (pos >= 0) {
        $("#" + li).addClass(activeClass);
    }
}