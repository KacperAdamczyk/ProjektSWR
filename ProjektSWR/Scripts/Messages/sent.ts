export function prepareSentDocument() {
    $.getJSON("/Messages/SentMessages", parseSentMessages);
}

function parseSentMessages(Jdata) {
    Jdata = JSON.parse(Jdata);
    console.log(Jdata);
    var i, line;
    for (i = 0; i < Jdata.length; i++) {
        line = "<tr id='" + Jdata[i].Id + "' onclick='messageDetails(this.id)'><td>" + Jdata[i].UserName + "</td><td>" + Jdata[i].Subject +
            "</td><td>" + Jdata[i].SendDate + "</td><td>" + Jdata[i].ReceivedDate + "</td></tr>";
        $(".sent_table").append(line);
    }
}