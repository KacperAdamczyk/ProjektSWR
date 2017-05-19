import { globalContainer } from "./controller";

export function messageContent(id) {
    $.getJSON("/Messages/Content?id=" + id, parseDetails);
}

function parseDetails(data) {
    data = JSON.parse(data);
    console.log(data);
    //$(globalContainer).html(data);
}