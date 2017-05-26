$(function () {
    $.getJSON("/Panel/Katedry", parseJSON3);


});

function parseJSON3(data3) {
    //data2 = JSON.parse(data2);
    var i;
    for (i = 0; i < data3.length; i++) {
        var nr = i + 1
        var line = "<option>" + data3[i].Department + "</option>";
        //var line = "<option value=" + "\"" + nr + "\">" +  " "  + "</option>";
        $("#cathedrals_select").append(line);
    }
}