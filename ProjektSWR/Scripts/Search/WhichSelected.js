$(function () {
    var list = "#list";
    var list_error = "list-error"
    var selected_id = null;
    $(list).on('input', function () {
        var opt = $("option").filter(function(){ return this.value==$(list).val(); })
        console.log(opt);
        if (opt.length == 0) {
            $(list).addClass(list_error);
            selected_id = null;
        } else {
            $(list).removeClass(list_error);
            selected_id = opt.first().attr("id");
        }
    });

    $("#search-user").click(function() {
        if ($(list).hasClass(list_error))
            return;
        window.location = "\\Profile?id=" + selected_id;
    });
});