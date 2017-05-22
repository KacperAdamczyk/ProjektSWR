import * as Quill from "quill";
import * as input from "./newMessage_input";
import * as controller from "./controller";

export function prepareNewMessageDocument() {
    input.loadContentInput();
    $.getJSON("/Messages/Users", input.parseUsers);
    $("#send_button").click(function(){ sendMessage(-1); });
    $("#add_user").click(function(){ input.createCombobox(); });
}

function sendMessage(responseId : number) {
    var recipients : Array<string> = getAllRecipients();
    var s : string = $("#Subject").val();
    var c : string = input.quill_editor.getContents();
    var message = { "UserName": recipients, "Subject": s, "Content": JSON.stringify(c), "ResponseId": responseId };
    $.ajax({
        url: "/Messages/CreateMessage",
        type: "POST",
        data: message,
        success: function() { controller.loadInbox(); },
        error: function() { console.log(this.textStatus); }
    });
}

function getAllRecipients() {
   let comboboxes = $(".users_combobox");
   let users : Array<string> = [];
   for (var i = 0; i < comboboxes.length; i++) {
       users.push($(comboboxes[i]).val());
   }
    return users;
}