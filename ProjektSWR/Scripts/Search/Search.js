($("#search-data").click(function () {
    document.getElementById("datalist").style.visibility = "visible";
}))

$(function () {
    $("#datalist").load("\Search");
});