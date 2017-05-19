import * as Quill from "quill";
import * as input from "./newMessage_input";
import * as controller from "./controller";

export function prepareNewMessageDocument() {
    input.loadContentInput();
    input.loadTo();
    $.getJSON("/Messages/Users", input.parseUsers);
    $("#send_button").click(function(){sendMessage();});
}

export function onExitNewMessageDocument() {
    //tinymce.EditorManager.editors = [];
}

function sendMessage() {
    var receiver = $('#users_combobox').val()
    var s = $("#Subject").val();
    var c = input.quill_editor.getContents();
    var message = { "UserName": receiver, "Subject": s, "Content": JSON.stringify(c) };
    var cl = "";
    for (let x in message) {
        cl += x + "=" + message[x] + "&";
    }
    cl = cl.substr(0, cl.length - 1);
    console.log(cl);
    var xhttp = new XMLHttpRequest();
    xhttp.open("POST", "/Messages/CreateMessage?" + cl, true);
    xhttp.onreadystatechange = function () {
        if (this.readyState === 4 && this.status === 200) {
            console.log(this.responseText);
            if (this.responseText === "True") {
                controller.loadInbox();
            }
        }
    }
    xhttp.send();
}