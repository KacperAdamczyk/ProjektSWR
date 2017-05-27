﻿import * as controller from "./controller";
import { quill_editor } from './newMessage_input';
import { globalContainer } from "./controller";
import * as alertifyjs from 'alertifyjs';
import * as Quill from "quill";
import "quill/dist/quill.snow.css";

let g_data;
let g_type;

export function prepareMessageContentDocument(id : number, type : string) {
    g_type = type;
    switch(type) {
        case "inbox":
            $("#delete_selected_btn").click(function (){ deleteMessageInbox(id); });
        break;
        case "sent":
            $("#delete_selected_btn").click(function (){ deleteMessageSent(id); });
            $("#response_btn").hide();
        break;
    }
    
    $.getJSON("/Messages/MessageContent?id=" + id, parseContent);
}

function parseContent(data) {
    g_data = JSON.parse(data);
    switch(g_type) {
        case "inbox":
                $("#response_btn").click( function() { controller.loadNewMessage(g_data.Sender, g_data.Id); });
            if (g_data.ResponseId >= 0) {
                $("#go_to_response").click(function() { prepareMessageContentDocument(g_data.ResponseId, "inbox"); })
            } else {
                $("#go_to_response").hide();
            }
        break;
        case "sent":
            $("#response_btn").hide();
            if (g_data.ResponseId >= 0) {
                $("#go_to_response").click(function() { prepareMessageContentDocument(g_data.ResponseId, "sent"); })
            } else {
                $("#go_to_response").hide();
            }
        break;
    }

    dispalyContent();
}

function dispalyContent() {
    $("#content_sender").val(g_data.Sender);
    $("#content_subject").val(g_data.Subject);

    let quill = new Quill('#messageContent', {
    theme: 'snow',
    modules: {
        toolbar: false
    }
  });
  quill.setContents(JSON.parse(g_data.Content));
  quill.disable();

  $(controller.transitor).addClass(controller.transitorAcrivated);
}

function deleteMessageInbox(id : number) {
    alertifyjs.confirm("Potwierdzenie", "Czy na pewno chcesz usunąć tę wiadomość?",
        function(){
            $.ajax({
        url: "/Messages/DeleteInbox",
        method: "POST",
        data: {"id" : id},
        success: function() { controller.loadInbox(); }
            });
        }, function() {});
}
function deleteMessageSent(id : number) {
    alertifyjs.confirm("Potwierdzenie", "Czy na pewno chcesz usunąć tę wiadomość?",
        function(){
           $.ajax({
        url: "/Messages/DeleteSent",
        method: "POST",
        data: {"id" : id},
        success: function() { controller.loadSent(); }
            });
        }, function() {});
    
}