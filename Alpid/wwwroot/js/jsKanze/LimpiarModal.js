
// aclaracion
// poner siempre como id al boton cerrar con el nombre 'Close'

//limpiar input
$(document).ready(function () {
    $('#Close').click(function () {
        $('input[type="text"]').val('');
        $('input[type="date"]').val('');
    });
});