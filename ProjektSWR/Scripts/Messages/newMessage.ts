import 'alertifyjs/build/css/alertify.css';
import 'alertifyjs/build/css/themes/bootstrap.css';
import * as controller from './controller';
import * as input from './newMessage_input';
import * as alertifyjs from 'alertifyjs';

let subject_id = "#subject";

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
        $("#remove_user").click(function(){ input.removeLastCombobox(); });
    } else {
        $("#add_user").hide();
        $("#remove_user").hide();
        $("#send_button").click(function(){ sendMessage(responseToId); });
        let c = "<input list='users" + "' class='users_combobox'>" +
            "<datalist id='users" + "'></datalist>";
        $("#comboboxes").append(c);
        let line = '<option' + ' data-id="user' + 0 + ' value="' + responseTo + '">' + responseTo + '</option>';
        $("#users").append(line);
        $(".users_combobox").first().val(responseTo);
    }
    $(subject_id).change(function() { $(subject_id).css("border", "solid 1px black"); });
}

function sendMessage(responseId : number) {
    var recipients : Array<string> = getAllRecipients();
    if (recipients == null)
        return;

    var s : string = $(subject_id).val();
    if (s.length == 0) {
        alertifyjs.error("Uzupełnij pole z tematem");
        $(subject_id).css("border", "solid 1px red");
        return;
    }
        
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
        error: function() { alertifyjs.error("Nie udało się wysłać wiadomości"); }
    });
}

function getAllRecipients() {
   let comboboxes = $(".users_combobox");
   let users : Array<string> = [];
   let error : number = 0;
   for (var i = 0; i < comboboxes.length; i++) {
       let str : string = $(comboboxes[i]).val();
       if (input.users.indexOf(str) >= 0) {
           $(comboboxes[i]).removeClass("input_error");
            users.push(str);
       } else {
           error++;
           $(comboboxes[i]).addClass("input_error");
       }
   }
   if (error > 0) {
       if (error == 1)
            alertifyjs.error("Nie znaleziono " + error + " odbiorcy");
        else
            alertifyjs.error("Nie znaleziono " + error + " odbiorców");

       return;
   }
    return users;
}