import * as Quill from "quill";
import * as input from "./newMessage_input";
import * as controller from "./controller";

export function prepareNewMessageDocument() {
    input.loadContentInput();
    input.loadTo();
    $.getJSON("/Messages/Users", input.parseUsers);
    $("#send_button").click(function(){sendMessage();});
}

function sendMessage() {
    var receiver = $('#users_combobox').val()
    var s = $("#Subject").val();
    var c = input.quill_editor.getContents();
    var message = { "UserName": receiver, "Subject": s, "Content": JSON.stringify(c) };
    $.ajax({
        url: "/Messages/CreateMessage",
        type: "POST",
        data: message,
        success: function() { controller.loadInbox(); },
        error: function() { console.log(this.textStatus); }
    });
}