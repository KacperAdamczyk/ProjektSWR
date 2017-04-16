function prepareNewMessageDocument() {
    loadContentInput();
    loadTo();

    $.getJSON("/Messages/JgetUsers", parseUsers);
}

function onExitNewMessageDocument() {
    tinymce.remove();
}

function sendMessage() {
    var receiver = $("#combobox").find('option:selected').text();
    var s = $("#Subject").val();
    var c = (((tinyMCE.activeEditor.getContent()).replace(/(&nbsp;)*/g, "")).replace(/(<p>)*/g, "")).replace(/<(\/)?p[^>]*>/g, ""); // usuwa wszystko
    var message = { "UserName": receiver, "Subject": s, "Content": c };
    var cl = "";
    for (x in message) {
        cl += x + "=" + message[x] + "&";
    }
    cl = cl.substr(0, cl.length - 1);

    var xhttp = new XMLHttpRequest();
    xhttp.open("POST", "/Messages/CreateMessage?" + cl, true);
    xhttp.onreadystatechange = function () {
        if (this.readyState === 4 && this.status === 200) {
            if (this.responseText === "True") {
                loadInbox();
            }
        }
    }
    xhttp.send();
}