import * as Quill from "quill";
import "quill/dist/quill.snow.css";

export let quill_editor;
let users;
export let combobox_cnt = 0;

export function parseUsers(data) {
    users = data;
    createCombobox();
}

export function loadContentInput() {
    quill_editor = new Quill('#messageContent', {
    theme: 'snow'
  });
}

export function createCombobox() {
    let c = "<input list='users" + combobox_cnt + "' class='users_combobox'>" +
            "<datalist id='users" + combobox_cnt + "'></datalist>";
            $("#comboboxes").append(c);

    var i, line;
    for (i = 0; i < users.length; i++) {
        line = '<option' + ' data-id="user' + (i+1) + ' value="' + users[i] + '">' + users[i] + '</option>';
        $("#users" + combobox_cnt).append(line);
    }
    combobox_cnt++;
}