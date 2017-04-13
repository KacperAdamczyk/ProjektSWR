function prepareInboxDocument() {
    var xhttp = new XMLHttpRequest();
    xhttp.open("GET", "/Messages/JgetMessages", true);
    xhttp.send();

    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            var Jdata = JSON.parse(this.responseText);
            parseUsers(Jdata);
        }
    }
}

function parseMessages(Jdata) {
    console.log(Jdata);
}