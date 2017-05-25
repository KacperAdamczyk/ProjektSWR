import * as Quill from "quill";
import * as input from "./newMessage_input";
import * as controller from "./controller";
import * as alertifyjs from "alertifyjs";
import "alertifyjs/build/css/alertify.css";
import "alertifyjs/build/css/themes/bootstrap.css";

export function prepareNewMessageDocument(responseTo : string, responseToId : number) {
    input.loadContentInput();
    $.getJSON("/Messages/Users", function(data) { 
        if (responseTo == null)
            input.parseUsers(data, true);
        else
            input.parseUsers(data, false);
    });
    if (responseToId == null) {
        $("#send_button").click(function(){ sendMessage(-1); });
        $("#add_user").click(function(){ input.createCombobox(); });
    } else {
        $("#add_user").hide();
        $("#send_button").click(function(){ sendMessage(responseToId); });
        let c = "<input list='users" + "' class='users_combobox'>" +
            "<datalist id='users" + "'></datalist>";
        $("#comboboxes").append(c);
        let line = '<option' + ' data-id="user' + 0 + ' value="' + responseTo + '">' + responseTo + '</option>';
        $("#users").append(line);
        $(".users_combobox").first().val(responseTo);
    }
    $(controller.transitor).addClass(controller.transitorAcrivated);
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
        success: function() {
            alertifyjs.success("Wiadomość została wysłana");
            controller.loadInbox(); 
        },
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