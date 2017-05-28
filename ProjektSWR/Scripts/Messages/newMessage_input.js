"use strict";
var Quill = require("quill");
require("quill/dist/quill.snow.css");
exports.users = [];
exports.combobox_cnt = 0;
function parseUsers(data, create) {
    exports.users = data;
    if (create)
        createCombobox();
}
exports.parseUsers = parseUsers;
function loadContentInput() {
    var toolbarOptions = [
        ['bold', 'italic', 'underline', 'strike'],
        ['blockquote', 'code-block'],
        [{ 'header': 1 }, { 'header': 2 }],
        [{ 'list': 'ordered' }, { 'list': 'bullet' }],
        [{ 'script': 'sub' }, { 'script': 'super' }],
        [{ 'indent': '-1' }, { 'indent': '+1' }],
        [{ 'direction': 'rtl' }],
        [{ 'size': ['small', false, 'large', 'huge'] }],
        [{ 'header': [1, 2, 3, 4, 5, 6, false] }],
        [{ 'color': [] }, { 'background': [] }],
        [{ 'font': [] }],
        [{ 'align': [] }],
        ['clean'] // remove formatting button
    ];
    exports.quill_editor = new Quill('#messageContent', {
        modules: {
            toolbar: toolbarOptions
        },
        theme: 'snow'
    });
}
exports.loadContentInput = loadContentInput;
function createCombobox() {
    var c = "<input list='users" + exports.combobox_cnt + "' class='users_combobox'>" +
        "<datalist id='users" + exports.combobox_cnt + "'></datalist>";
    $("#comboboxes").append(c);
    var i, line;
    for (i = 0; i < exports.users.length; i++) {
        line = '<option' + ' data-id="user' + (i + 1) + ' value="' + exports.users[i] + '">' + exports.users[i] + '</option>';
        $("#users" + exports.combobox_cnt).append(line);
    }
    exports.combobox_cnt++;
}
exports.createCombobox = createCombobox;
function removeLastCombobox() {
    if (exports.combobox_cnt > 1) {
        $("input.users_combobox").last().remove();
        exports.combobox_cnt--;
    }
}
exports.removeLastCombobox = removeLastCombobox;
