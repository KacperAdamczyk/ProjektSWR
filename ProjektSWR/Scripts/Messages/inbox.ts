export function prepareInboxDocument() {
    $.getJSON("/Messages/JgetMessageHeaders", parseMessages);
}

function parseMessages(Jdata) {
    Jdata = JSON.parse(Jdata);
    console.log(Jdata);
    var i, line;
    for (i = 0; i < Jdata.length; i++) {
        line = "<tr><td>" + Jdata[i].UserName + "</td><td>" + Jdata[i].Subject + "</td></tr>";
        $(".is_table").append(line);
    }
}