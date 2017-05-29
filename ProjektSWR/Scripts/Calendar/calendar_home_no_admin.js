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
        //select: function (start, end) {
        //    $('.delete-btn').addClass('hidden');
            
        //    $('#myModalLabel').html("Dodanie wydarzenia");
        //    $('[name="title"]').val("");
        //    $('[name="details"]').val("");
        //    $('[name="location"]').val("");
        //    $('[name="startDate"]').val(start.format());
        //    $('[name="endDate"]').val(end.format());
        //    $('[name="saveChanges"]').addClass('hidden');

        //    $('#myModal').modal('show');
     
        //},
        eventClick: function (calEvent, jsEvent, view) {
            $('.delete-btn').addClass('hidden');

            $('#myModalLabel').html("Szczegóły wydarzenia");
            $('[name="title"]').val(calEvent.title).attr('disabled', true);
            $('[name="details"]').val(calEvent.details).attr('disabled', true);
            $('[name="location"]').val(calEvent.location).attr('disabled', true);
            $('[name="startDate"]').val(calEvent.start.format()).attr('disabled', true);
            $('[name="endDate"]').val(calEvent.end.format()).attr('disabled', true);
            $('[name="saveChanges"]').addClass('hidden');

            temporaryEvent = calEvent;
            $('#myModal').modal('show');

        },       
        editable: true,
        eventLimit: true, // allow "more" link when too many events
        events: eventsList
    });
});