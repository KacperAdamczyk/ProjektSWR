var xhttp = new XMLHttpRequest();

xhttp.onreadystatechange = function () {
    if (this.readyState == 4 && this.status == 200) {
        var Jdata = JSON.parse(this.responseText);
    }
}

$(document).ready(function () {
    changeActive("mail_box");
    xhttp.open("GET", "Json", true);
    xhttp.send();
});