"use strict";
var input = require("./newMessage_input");
var controller = require("./controller");
function prepareNewMessageDocument() {
    input.loadContentInput();
    input.loadTo();
    $.getJSON("/Messages/JgetUsers", input.parseUsers);
    $("#send_button").click(function () { sendMessage(); });
}
exports.prepareNewMessageDocument = prepareNewMessageDocument;
function onExitNewMessageDocument() {
    //tinymce.EditorManager.editors = [];
}
exports.onExitNewMessageDocument = onExitNewMessageDocument;
function sendMessage() {
    var receiver = $('#users_combobox').val();
    var s = $("#Subject").val();
    var c = input.quill_editor.getContents();
    var message = { "UserName": receiver, "Subject": s, "Content": JSON.stringify(c) };
    var cl = "";
    for (var x in message) {
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
    };
    xhttp.send();
}
