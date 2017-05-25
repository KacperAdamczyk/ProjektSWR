$(document).ready(function () {

    var temporaryEvent = null;

    $('[name="saveChanges"]').on('click', function () {

        var eventData;
        var title = $('[name="title"]').val();
        var details = $('[name="details"]').val();
        var location = $('[name="location"]').val();
        var start = $('[name="startDate"]').val();
        var end = $('[name="endDate"]').val();

        if (title) {

            if (temporaryEvent == null) {
                eventData = {
                    id: title + location + start, //ID needs to be unique best way would be to generate uuid and save it to db
                    title: title,
                    details: details,
                    location: location,
                    start: start,
                    end: end
                };

                //Using structure valid with model in Events/Create
                var eventDataPost = {
                    '__RequestVerificationToken': $('[name="__RequestVerificationToken"]').val(),
                    Title: title,
                    Details: details,
                    Location: location,
                    startDate: start,
                    endDate: end
                };

                //Sending POST request to action Events/Create
                $.post("/Events/Create", eventDataPost, function (response) {
                    console.log(response);
                });

                $('#calendar').fullCalendar('renderEvent', eventData, true); // stick? = true
            } else {
                temporaryEvent.title = title;
                temporaryEvent.details = details;
                temporaryEvent.location = location;
                temporaryEvent.start = start;
                temporaryEvent.end = end;

                $('#calendar').fullCalendar('updateEvent', temporaryEvent);
            }
        }
        else {
            alert("Tytuł wydarzenia jest obowiązkowy.");
        }
        $('#calendar').fullCalendar('unselect');

    });

    $(document).on('click', '.delete-btn', function () {
        $('#calendar').fullCalendar('removeEvents', temporaryEvent.id);
    });

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
        select: function (start, end) {
            $('.delete-btn').addClass('hidden');

            temporaryEvent = null;
            $('#myModalLabel').html("Dodanie wydarzenia");
            $('[name="title"]').val("");
            $('[name="details"]').val("");
            $('[name="location"]').val("");
            $('[name="startDate"]').val(start.format());
            $('[name="endDate"]').val(end.format());
            $('#myModal').modal('show');

        },
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
        //[
        //    {
        //        title: 'Zdarzenie',
        //        start: '2017-05-23T10:00:00',
        //        end: '2017-05-23T12:00:00'
        //    }
        //]
    });

});