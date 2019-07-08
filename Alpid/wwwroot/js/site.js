// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
//$('#modalEditar').on('shown.bs.modal', function () {
//    $('#myInput').focus();
//});
//$('#modalAC').on('shown.bs.modal', function () {
//    $('#Nombre').focus();
//});
//var SociosID, funcion = 0;
////Socios
//var agregarSocio = () => {
//    var Cuit = document.getElementById("Cuit").value;
//    var RazonSocial = document.getElementById("RazonSocial").value;
//    var Domicilio = document.getElementById("Domicilio").value;
//    var Telefono = document.getElementById("Telefono").value;
//    var Email = document.getElementById("Email").value;
//    var FechaAlta = '20180101';// document.getElementById("FechaAlta").value;
//    var FechaBaja = '20180101'; //document.getElementById("FechaBaja").value;
//    var MotivoBaja ='pureba'// document.getElementById("MotivoBaja").value;
//    document.getElementById("mensaje").innerHTML;
//    if (funcion == 0) {
//        var action = 'Socios/guardarSocio';
//    } else {
//        var action = 'Socios/editarSocio';
//    }
//    var socios = new Socios(Cuit, RazonSocial, Domicilio, Telefono, Email, FechaAlta, FechaBaja, MotivoBaja, action);
//    socios.agregarSocio(SociosID, funcion);
//    funcion = 0;
//}
//var filtrarDatos = (numPagina, order) => {
//    var valor = document.getElementById("filtrar").value;
//    var action = 'Socios/filtrarDatos';
//    var socios = new Socios("",valor,"","","","", "", "", action);
//    socios.filtrarDatos(numPagina, order);
//}

//var editarSocio = () => {
//    var action = 'Socios/editarSocio';
//    var socios = new Socios("", "", "","","","","","", action);
//    socios.editarSocio(1, 1);
//}