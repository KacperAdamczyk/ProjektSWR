$(function () {
    $('#list').on('input', function () {
        var opt = $('option:selected');
        console.log(opt);
        console.log($(this).val())
    });
});