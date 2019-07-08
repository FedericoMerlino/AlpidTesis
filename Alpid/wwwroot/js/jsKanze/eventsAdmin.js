var eventId = -1;

$(document).ready(function () {

    // Activate tooltip
    $('[data-toggle="tooltip"]').tooltip();

    // Select/Deselect checkboxes
    var checkbox = $('table tbody input[type="checkbox"]');
    $("#selectAll").click(function () {
        if (this.checked) {
            checkbox.each(function () {
                this.checked = true;
            });
        } else {
            checkbox.each(function () {
                this.checked = false;
            });
        }
    });
    checkbox.click(function () {
        checkbox.each(function () {
            this.checked = false;
        });

        this.checked = true;
        eventId = -1;

        var table = document.getElementById("eventsDetail").getElementsByTagName("input");

        $.each(table, function (i, v) {
            if (v.checked) {
                eventId = v.parentNode.parentNode.parentNode.cells[1].innerHTML;
                //console.log(eventId);
            }
        });

    });

    $("#userInEvents").on('shown.bs.modal', function () {
        getUserInEvent();
    });

});



function getUserInEvent() {
    console.log(eventId);
    if (eventId !== -1) {
        $.ajax({
            async: false,
            url: '/Events/GetUsersInEvent?eventId=' + eventId,
            success: function (result) {
                $("#usersInevent").find("tr:gt(0)").remove();
                let tds = '';
                $.each(result, function (i, v) {
                    tds += `<tr><td>${v.FirstName}</td><td>${v.LastName}</td><td>${v.Mail}</td></tr>`;
                });
                console.log(tds);
                $('#usersInevent').append(
                    `${tds}`
                );
            }
        });
    }
}

function deleteEvent() {
    console.log(eventId);
    if (eventId !== -1) {
        $.ajax({
            async: false,
            url: '/Events/DeleteEvent?id=' + eventId
        });
        window.location = '/Events/Events';
    }
}

function updateEvent(eventId) {
    if (eventId !== undefined && eventId !== null) {
        $.ajax({
            async: false,
            url: '/Events/EditEvent?id=' + eventId,
            success: function (data) {
                $('#editModalWrapper').html(data);
                $('#editEmployeeModal').modal('show');
            }
        });
    }
}