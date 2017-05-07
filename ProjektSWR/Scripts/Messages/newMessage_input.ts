import * as Quill from "quill";
import "quill/dist/quill.snow.css";

export var quill_editor;

export function parseUsers(Jdata) {
    var i, line;
    for (i = 0; i < Jdata.length; i++) {
        line = '<option' + ' data-id="op' + (i+1) + ' value="' + Jdata[i] + '">' + Jdata[i] + '</option>';
        $("#combobox").append(line);
    }
}

export function loadContentInput() {
    quill_editor = new Quill('#messageContent', {
    theme: 'snow'
  });
}

export function loadTo()  {
}