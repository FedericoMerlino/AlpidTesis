
var bookId = -1;

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
        bookId = -1;

        var table = document.getElementById("booksDetail").getElementsByTagName("input");

        $.each(table, function (i, v) {
            if (v.checked) {
                bookId = v.parentNode.parentNode.parentNode.cells[1].innerHTML;
            }
        });

    });


});


function deleteBook() {
    if (bookId !== -1) {
        $.ajax({
            async: false,
            url: '/Books/DeleteBook?id=' + bookId
        });
        window.location = '/Books/GetBooks';
    }
}

function updateBook(bookId) {
    if (bookId !== undefined && bookId !== null) {
        $.ajax({
            async: false,
            url: '/Books/EditBook?id=' + bookId,
            success: function (data) {
                $('#editModalWrapper').html(data);
                $('#editEmployeeModal').modal('show');
            }
        });
    }
}