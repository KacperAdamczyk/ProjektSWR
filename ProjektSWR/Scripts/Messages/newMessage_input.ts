import * as Quill from "quill";
import "quill/dist/quill.snow.css";

export let quill_editor;
export let users : Array<string> = [];
export let combobox_cnt = 0;

export function parseUsers(data, create : boolean) {
    users = data;
    if (create)
        createCombobox();
}

export function loadContentInput() {

    var toolbarOptions = [
  ['bold', 'italic', 'underline', 'strike'],        // toggled buttons
  ['blockquote', 'code-block'],

  [{ 'header': 1 }, { 'header': 2 }],               // custom button values
  [{ 'list': 'ordered'}, { 'list': 'bullet' }],
  [{ 'script': 'sub'}, { 'script': 'super' }],      // superscript/subscript
  [{ 'indent': '-1'}, { 'indent': '+1' }],          // outdent/indent
  [{ 'direction': 'rtl' }],                         // text direction

  [{ 'size': ['small', false, 'large', 'huge'] }],  // custom dropdown
  [{ 'header': [1, 2, 3, 4, 5, 6, false] }],

  [{ 'color': [] }, { 'background': [] }],          // dropdown with defaults from theme
  [{ 'font': [] }],
  [{ 'align': [] }],

  ['clean']                                         // remove formatting button
];

    quill_editor = new Quill('#messageContent', {
         modules: {
            toolbar: toolbarOptions
        },
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

export function removeLastCombobox() {
    if (combobox_cnt > 1) {
        $("input.users_combobox").last().remove();
        combobox_cnt--;
    }
}