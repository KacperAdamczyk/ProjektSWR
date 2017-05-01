"use strict";
var Quill = require("quill");
require("quill/dist/quill.snow.css");
function parseUsers(Jdata) {
    var i, line;
    for (i = 0; i < Jdata.length; i++) {
        line = '<option' + ' data-id="op' + (i + 1) + ' value="' + Jdata[i] + '">' + Jdata[i] + '</option>';
        $("#combobox").append(line);
    }
}
exports.parseUsers = parseUsers;
function loadContentInput() {
    exports.quill_editor = new Quill('#messageContent', {
        theme: 'snow'
    });
}
exports.loadContentInput = loadContentInput;
function loadTo() {
}
exports.loadTo = loadTo;
//# sourceMappingURL=newMessage_input.js.map