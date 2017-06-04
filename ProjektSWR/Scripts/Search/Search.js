$(function () {
    var trans_time = 300;
    $("#datalist").load("\\Search");

    $("#search-data").click(function (e) {
    if ($("#datalist").is(":visible"))
        $("#datalist").hide(trans_time);
    else
        $("#datalist").show(trans_time);

    e.stopPropagation();
});
    $("#datalist").click(function(e){ e.stopPropagation(); })
    $(document).click(function() { $("#datalist").hide(trans_time); });
});