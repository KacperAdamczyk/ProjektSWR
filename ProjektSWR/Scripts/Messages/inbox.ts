import * as controller from "./controller";

let g_data;

export function prepareInboxDocument() {
    $.getJSON("/Messages/MessageHeaders", parseMessages);
    $("#delete_selected_btn").click(function() { deleteMessages(); });
    $("#select_all").click(function() {
        if ($("#select_all").is(":checked"))
            $("input:checkbox").prop("checked", true);
        else
            $("input:checkbox").prop("checked", false);
    });
    var interval = setInterval(function() {
        updateHeaders();
    }, 5000);
}

function parseMessages(data) {
    data = JSON.parse(data);
    g_data = data;
    let i : number, line : string;
    if (data.length == 0) {
        line = "<tr>" + "<td colspan='4'>" + "Brak wiadomości" + "</td>" + "</tr>"
        $(".inbox_table").append(line);
    }

    for (i = 0; i < data.length; i++) {
        let newMessage : boolean = false;
        let sentDate : string = new Date(data[i].SendDate).toLocaleString();

        if (data[i].ReceivedDate[0] != null) {
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
         $(".inbox_table").append(line);
         var tr = $("#" + data[i].Id);
         tr.click(function() { console.log(1);controller.loadContent(this.id, "inbox"); });
         tr.first().children().first().click(function(e) { e.stopPropagation(); });
    }
    $(controller.transitor).addClass(controller.transitorAcrivated);
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
    $.ajax({
        url: "/Messages/DeleteInbox",
        method: "POST",
        data: {"id" : selectedMessageIds},
        success: function() { controller.loadInbox(); }
    });
}