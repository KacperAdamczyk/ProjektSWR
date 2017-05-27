$(function () {
    $.getJSON("/Panel/Users", parseJSON2);


});

function parseJSON2(data2) {
    var i;
    for (i = 0; i < data2.length; i++) {
        var nr = i + 1
        var line = "<option value=" + "\"" + nr + "\">" + data2[i].LastName + " " + data2[i].FirstName + "</option>";
        $("#user_select").append(line);
    }
}