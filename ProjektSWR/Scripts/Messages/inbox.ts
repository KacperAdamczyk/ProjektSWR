import * as controller from "./controller";

export function prepareInboxDocument() {
    $.getJSON("/Messages/MessageHeaders", parseMessages);
    $("#delete_selected_btn").click(function() { deleteMessages(); });
    $("#select_all").click(function() {
        if ($("#select_all").is(":checked"))
            $("input:checkbox").prop("checked", true);
        else
            $("input:checkbox").prop("checked", false);
    });
}

function parseMessages(data) {
    data = JSON.parse(data);
    let i : number, line : string, full : string;
    if (data.length == 0) {
        line = "<tr>" + "<td colspan='4'>" + "Brak wiadomości" + "</td>" + "</tr>"
        $(".inbox_table").append(line);
    }

    for (i = 0; i < data.length; i++) {
        let newMessage : boolean = false;
        let sentDate : string = new Date(data[i].SendDate).toLocaleString();

        if (data[i].ReceivedDate != null) {
           var receivedDate : string = new Date(data[i].ReceivedDate).toLocaleString()
        } else {
            var receivedDate : string = "Nie odczytano";
            newMessage = true;
        }

        line = "<tr id='" + data[i].Id + (newMessage ? "' class='new_message_row'>" : "'>") +
         "<td>" + "<input type='checkbox' id='cb" + data[i].Id + "'>" + "</td>" +
         "<td>" + data[i].Sender + "</td>" +
         "<td>" + data[i].Subject + "</td>" +
         "<td>" + sentDate + "</td>" +
         "</tr>";
         full += line;
         var tr = $("#" + data[i].Id);
         tr.click(function() { controller.loadContent(this.id, "inbox"); });
         tr.first().children().first().click(function(e) { e.stopPropagation(); });
    }
    $(".inbox_table").append(full);
}

function deleteMessages() {
    let selectedMessages = $("input:checkbox:checked");
    let selectedMessageIds : Array<number> = [];
    var i : number;
    for (i = 1; i < selectedMessages.length; i++) {
        selectedMessageIds.push(Number(selectedMessages[i].id.substr(2)));
    }
    $.ajax({
        url: "/Messages/DeleteInbox",
        method: "POST",
        data: {"id" : selectedMessageIds},
        success: function() { controller.loadInbox(); }
    });
}