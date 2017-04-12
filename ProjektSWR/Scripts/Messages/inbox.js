var xhttp = new XMLHttpRequest();

xhttp.onreadystatechange = function () {
    if (this.readyState == 4 && this.status == 200) {
        var Jdata = JSON.parse(this.responseText);
        parseData(Jdata);
    }
}

$(document).ready(function () {
    changeActive("mail_box");
    xhttp.open("GET", "JgetMessages", true);
    xhttp.send();
});

function parseData(Jdata) {
    console.log(Jdata);
}