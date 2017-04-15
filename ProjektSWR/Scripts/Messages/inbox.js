function prepareInboxDocument() {
    $.getJSON("/Messages/JgetMessageHeaders", parseMessages);
}

function parseMessages(Jdata) {
    Jdata = JSON.parse(Jdata);
    console.log(Jdata);
    var i, line;
    for (i = 0; i < Jdata.length; i++) {
        line = "<tr><td>" + "</td></tr>";
        $("#inbox_table").append(line);
    }
}