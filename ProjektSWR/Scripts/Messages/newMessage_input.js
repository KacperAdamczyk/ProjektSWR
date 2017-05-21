"use strict";
var Quill = require("quill");
require("quill/dist/quill.snow.css");
var users;
exports.combobox_cnt = 0;
function parseUsers(data) {
    users = data;
    createCombobox();
}
exports.parseUsers = parseUsers;
function loadContentInput() {
    exports.quill_editor = new Quill('#messageContent', {
        theme: 'snow'
    });
}
exports.loadContentInput = loadContentInput;
function createCombobox() {
    var c = "<input list='users" + exports.combobox_cnt + "' class='users_combobox'>" +
        "<datalist id='users" + exports.combobox_cnt + "'></datalist>";
    $("#comboboxes").append(c);
    var i, line;
    for (i = 0; i < users.length; i++) {
        line = '<option' + ' data-id="user' + (i + 1) + ' value="' + users[i] + '">' + users[i] + '</option>';
        $("#users" + exports.combobox_cnt).append(line);
    }
    exports.combobox_cnt++;
}
exports.createCombobox = createCombobox;
