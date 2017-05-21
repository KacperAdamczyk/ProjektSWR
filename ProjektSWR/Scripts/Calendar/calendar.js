$(document).ready(function () {

    $('#calendar').fullCalendar({
        height: 400,
        header: {
            left: 'prev,next today',
            center: 'title',
            right: 'month,agendaWeek,agendaDay,listWeek'
        },
        defaultDate: Date.now(),
        locale: 'pl',
        navLinks: true, // can click day/week names to navigate views
        selectable: true,
        selectHelper: true,
        select: function (start, end) {
            var title = prompt('Tytuł nowego zdarzenia:');
            var eventData;
            if (title) {
                eventData = {
                    title: title,
                    start: start,
                    end: end
                };
                $('#calendar').fullCalendar('renderEvent', eventData, true); // stick? = true
            }
            $('#calendar').fullCalendar('unselect');
        },
        editable: true,
        eventLimit: true, // allow "more" link when too many events
        events: [
            {
                title: 'Zdarzenie',
                start: '2017-05-23T10:00:00',
                end: '2017-05-23T12:00:00'
            }
        ]
    });

});