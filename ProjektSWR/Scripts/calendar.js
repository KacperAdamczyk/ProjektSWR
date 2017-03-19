$(document).ready(function () {

    $('#calendar').fullCalendar({
        header: {
            left: 'prev,next today',
            center: 'title',
            right: 'month,agendaWeek,agendaDay'
        },
        defaultDate: '2017-03-20',
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
                title: 'Bazy danych \n (F2) - W30 \n Wykład \n Trybus Bartosz (EA)',
                start: '2017-03-21T08:00:00',
                end: '2017-03-21T10:00:00'
            },
            {
                title: 'Sieci Komputerowe I \n (F2) - W30 \n Wykład \n Bolanowski Marek (EE)',
                start: '2017-03-21T10:00:00',
                end: '2017-03-21T12:00:00'
            },
            {
                title: 'Podstawy telekomunikacji \n (F2) - W15 \n Wykład \n Gotfryd Marek (EU) \n Pawlowicz Bartosz (EU)',
                start: '2017-03-21T12:00:00',
                end: '2017-03-21T14:00:00'
            },
            {
                title: 'Sztuczna inteligencja \n (F2) - W30 \n Wykład \n Kluska Jacek (EA)',
                start: '2017-03-21T14:00:00',
                end: '2017-03-21T16:00:00'
            },
            {
                title: 'Grafika komputerowa \n (F2) - W30 \n Wykład \n Leniowski Ryszard (EA)',
                start: '2017-03-23T08:00:00',
                end: '2017-03-23T10:00:00'
            },
            {
                title: 'Inżynieria oprogramowania \n (F2) - W30 \n Wykład \n Dec Grzegorz (EA)',
                start: '2017-03-23T10:00:00',
                end: '2017-03-23T12:00:00'
            }
        ]
    });

});