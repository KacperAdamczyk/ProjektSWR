$(function () {
    $.getJSON("/Cathedrals/Cathedrals", parseJSON);


});

function parseJSON(data) {
    data = JSON.parse(data);
    var i;
    for (i = 0; i < data.length; i++) {
        var line = "<option>" + data[i].Department + "</option>";
        $("#cathedrals_select").append(line);
    }
}