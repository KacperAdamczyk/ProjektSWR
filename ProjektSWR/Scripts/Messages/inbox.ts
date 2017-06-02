import * as controller from "./controller";
import * as alertifyjs from 'alertifyjs';
import * as cookie from "js-cookie";

let g_data;
let new_msg_cnt = 0;
cookie.set("interval", "not set");

export function prepareInboxDocument() {
    $.getJSON("/Messages/MessageHeaders", parseMessages);
    $("#delete_selected_btn").click(function() { deleteMessages(); });
    $("#select_all").click(function() {
        if ($("#select_all").is(":checked"))
            $("input:checkbox").prop("checked", true);
        else
            $("input:checkbox").prop("checked", false);
    });
    if (cookie.get("interval") == "not set") {
        var interval = setInterval(function() {
            updateHeaders();
        }, 10000);
        cookie.set("interval", interval);
    }
}

function parseMessages(data) {
    data = JSON.parse(data);
    g_data = data;
    let i : number, line : string;
    if (data.length == 0) {
        line = "<tr>" + "<td colspan='4'>" + "Brak wiadomości" + "</td>" + "</tr>"
        $(".inbox_table").append(line);
    }
    new_msg_cnt = 0;
    for (i = 0; i < data.length; i++) {
        let newMessage : boolean = false;
        let sentDate : string = new Date(data[i].SendDate).toLocaleString();

        if (data[i].ReceivedDate[0] != null) {
           var receivedDate : string = new Date(data[i].ReceivedDate).toLocaleString()
        } else {
            var receivedDate : string = "Nie odczytano";
            newMessage = true;
            new_msg_cnt++;
        }

        line = "<tr id='" + data[i].Id + (newMessage ? "' class='new_message_row'>" : "'>") +
         "<td>" + "<input type='checkbox' id='cb" + data[i].Id + "'>" + "</td>" +
         "<td>" + data[i].Sender + "</td>" +
         "<td>" + data[i].Subject + "</td>" +
         "<td>" + sentDate + "</td>" +
         "</tr>";
         $(".inbox_table").append(line);
         var tr = $("#" + data[i].Id);
         tr.click(function() { controller.loadContent(this.id, "inbox"); });
         tr.first().children().first().click(function(e) { e.stopPropagation(); });
    }
    if (new_msg_cnt > 0)
        $("#inbox").html("Skrzynka odbiorcza (" + new_msg_cnt + ")");
    else 
        $("#inbox").html("Skrzynka odbiorcza");

        controller.enableTransition();
        controller.hideLoader();
}

function updateHeaders() {
    $.getJSON("/Messages/MessageHeaders", function(data) {
        let str_g_data = JSON.stringify(g_data);
        if(str_g_data !== data) {
            $(".inbox_table tr").not(":first-child").remove();
            parseMessages(data);
        }
    });
}

function deleteMessages() {
    let selectedMessages = $("input:checkbox:checked");
    let selectedMessageIds : Array<number> = [];
    var i : number;
    for (i = 0; i < selectedMessages.length; i++) {
        if (selectedMessages[i].id != "select_all")
            selectedMessageIds.push(Number(selectedMessages[i].id.substr(2)));
    }

    if (selectedMessageIds.length == 0)
        return;

    alertifyjs.confirm("Potwierdzenie", "Czy na pewno chcesz usunąć " + selectedMessageIds.length + (selectedMessageIds.length > 1 ? " wiadomości" : " wiadomość") + "?",
        function(){
            $.ajax({
            url: "/Messages/DeleteInbox",
            method: "POST",
            data: {"id" : selectedMessageIds},
            success: function() { controller.loadInbox(); }
            });
        }, function() {});
}