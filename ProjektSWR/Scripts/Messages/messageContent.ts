import * as controller from "./controller";
import { quill_editor } from './newMessage_input';
import { globalContainer } from "./controller";
import * as Quill from "quill";
import "quill/dist/quill.snow.css";

let g_data;

export function messageContent(id : number, type : string) {
    switch(type) {
        case "inbox":
            $("#delete_selected_btn").click(function (){ deleteMessageInbox(id); });
        break;
        case "sent":
        $("#delete_selected_btn").click(function (){ deleteMessageSent(id); });
        break;
    }
    
    $.getJSON("/Messages/MessageContent?id=" + id, parseContent);
}

function parseContent(data) {
    g_data = JSON.parse(data);
    g_data = JSON.parse(g_data);
    dispalyContent();
}

export function getData() {
    return g_data;
}

function dispalyContent() {
    var toolbarOptions = [];
    let quill = new Quill('#messageContent', {
    theme: 'snow',
    modules: {
        toolbar: toolbarOptions
    }
  });
  quill.setContents(g_data);
  quill.disable();
}

function deleteMessageInbox(id : number) {
    $.ajax({
        url: "/Messages/DeleteInbox",
        method: "POST",
        data: {"id" : id},
        success: function() { controller.loadInbox(); }
    });
}
function deleteMessageSent(id : number) {
    $.ajax({
        url: "/Messages/DeleteSent",
        method: "POST",
        data: {"id" : id},
        success: function() { controller.loadSent(); }
    });
}