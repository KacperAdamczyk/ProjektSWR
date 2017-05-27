﻿import * as Quill from "quill";
import * as controller from './controller';
import "quill/dist/quill.snow.css";

export let quill_editor;
export let users : Array<string> = [];
export let combobox_cnt = 0;

export function parseUsers(data, create : boolean) {
    users = data;
    if (create)
        createCombobox();
    controller.enableTransition();
    controller.hideLoader();
}

export function loadContentInput() {

    var toolbarOptions = [
  ['bold', 'italic', 'underline', 'strike'],
  ['blockquote', 'code-block'],

  [{ 'header': 1 }, { 'header': 2 }],
  [{ 'list': 'ordered'}, { 'list': 'bullet' }],
  [{ 'script': 'sub'}, { 'script': 'super' }],
  [{ 'indent': '-1'}, { 'indent': '+1' }],
  [{ 'direction': 'rtl' }],

  [{ 'size': ['small', false, 'large', 'huge'] }],
  [{ 'header': [1, 2, 3, 4, 5, 6, false] }],

  [{ 'color': [] }, { 'background': [] }],
  [{ 'font': [] }],
  [{ 'align': [] }],

  ['clean']
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