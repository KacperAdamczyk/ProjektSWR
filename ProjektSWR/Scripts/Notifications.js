var notificationsData = $('<div class="notifications"></div>').load('/Notifications', function () {
    if (notificationsData.find('.notification').length == 0) {
        $('.show-notifications').attr('disabled', true);
    }
    $('.show-notifications')
        .popover({
            html: true,
            container: $('body'),
            placement: 'bottom',
            title: 'Powiadomienia',
            content: function () {
                return notificationsData.html();
            }
        });
});

$('body').on('click', '.notification-mark-read', function (e) {
    var clicked = $(e.target).closest('.notification');
    var id = clicked.attr('data-id');
    if (!id) return;
    $.ajax({
        type: 'POST',
        url: '/Notifications/MarkRead/' + id,
        success: function () {
            clicked.removeClass('notification-unread').addClass('notification-read');
        }
    });
});