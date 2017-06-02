$(document).ready(function () {

    $('#calendar').fullCalendar({
        height: 600,
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
        fixedWeekCount: false,
        eventClick: function (calEvent, jsEvent, view) {

            $('.delete-btn').removeClass('hidden');

            $('#myModalLabel').html("Edycja wydarzenia");
            $('[name="title"]').val(calEvent.title);
            $('[name="details"]').val(calEvent.details);
            $('[name="location"]').val(calEvent.location);
            $('[name="startDate"]').val(calEvent.start.format());
            $('[name="endDate"]').val(calEvent.end.format());

            temporaryEvent = calEvent;
            $('#myModal').modal('show');

        },
        editable: true,
        eventLimit: true, // allow "more" link when too many events
        events: eventsList
    });
});