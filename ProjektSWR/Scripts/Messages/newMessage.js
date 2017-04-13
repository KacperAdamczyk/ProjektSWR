function prepareNewMessageDocument() {
    loadContentInput();
    loadTo();

    var xhttp = new XMLHttpRequest();
    xhttp.open("GET", "/Messages/JgetUsers", true);
    xhttp.send();

    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
                var Jdata = JSON.parse(this.responseText);
                parseUsers(Jdata);
            }
    }
}

function onExitNewMessageDocument() {
    tinymce.remove();
}

function sendMessage() {
    var receiver = $("#combobox").find('option:selected').text();
    var s = $("#Subject").val();
    var c = (((tinyMCE.activeEditor.getContent()).replace(/(&nbsp;)*/g, "")).replace(/(<p>)*/g, "")).replace(/<(\/)?p[^>]*>/g, "");
    var message = { "userName": receiver, "Temat": s, "Tresc": c };
    var cl = "";
    for (x in message) {
        cl += x + "=" + message[x] + "&";
    }
    cl = cl.substr(0, cl.length - 1);

    operation = "POST";
    var xhttp = new XMLHttpRequest();
    xhttp.open("POST", "/Messages/CreateMessage?" + cl, true);
    xhttp.send();

    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            if (this.responseText == true) {
                document.location.href = "/Messages";
            }
        }
    }
}