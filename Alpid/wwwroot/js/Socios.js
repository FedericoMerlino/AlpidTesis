var localStorage = window.localStorage;
class Socios {
    constructor(Cuit, RazonSocial, Domicilio, Telefono, Email, FechaAlta, FechaBaja, MotivoBaja, action) {
        this.Cuit = Cuit;
        this.RazonSocial = RazonSocial;
        this.Domicilio = Domicilio;
        this.Telefono = Telefono;
        this.Email = Email;
        this.FechaAlta = FechaAlta;
        this.FechaBaja = FechaBaja;
        this.MotivoBaja = MotivoBaja;
        this.action = action
    }
    agregarSocio(ID, funcion) {
        var Cuit = this.Cuit;
        var RazonSocial = this.RazonSocial;
        var Domicilio = this.Domicilio;
        var Telefono = this.Telefono;
        var Email = this.Email;
        var FechaAlta = this.FechaAlta;
        var FechaBaja = this.FechaBaja;
        var MotivoBaja = this.MotivoBaja;
        var mensaje = '';
        $.ajax({
            type: "POST",
            url: 'Socios/guardarSocio',
            data: {
                ID, Cuit, RazonSocial, Domicilio, Telefono, Email, FechaAlta, FechaBaja, MotivoBaja, funcion
            },
            success: (response) => {
                $.each(response, (index, val) => {
                    mensaje = val.code;
                });
                if (mensaje === "Save") {
                    this.restablecer();
                    window.close();
                    location.reload();
                } else {
                    document.getElementById("mensaje").innerHTML = "No se puede guardar el Socio Nuevo";
                }
            }
        });
    }
    editarSocio(SocioID, funcion) {
        var action = this.action;
        var response = JSON.parse(localStorage.getItem("socios"));
        var Cuit =          response[0].Cuit;
        var RazonSocial =   response[0].RazonSocial;
        var Domicilio =     response[0].Domicilio;
        var Telefono =      response[0].Telefono;
        var Email =         response[0].Email;
        var FechaAlta =         response[0].FechaAlta;
        var FechaBaja =     response[0].FechaBaja;
        var MotivoBaja =    response[0].MotivoBaja;
        localStorage.removeItem("socios");
        $.ajax({
            type: "POST",
            url: action,
            data: { SocioID,Cuit, RazonSocial, Domicilio, Telefono, Email, FechaAlta, FechaBaja, MotivoBaja, funcion},
            success: (response) => {
                console.log(response);
                this.restablecer();
                window.close();
                location.reload();
            }
        });

    }
    restablecer() {
        document.getElementById("Cuit").value = "";
        document.getElementById("RazonSocial").value = "";
        document.getElementById("Domicilio").value = "";
        document.getElementById("Telefono").value = "";
        document.getElementById("Email").value = "";
        document.getElementById("FechaAlta").value = "";
        document.getElementById("FechaBaja").value = "";
        document.getElementById("MotivoBaja").value = "";
        document.getElementById("mensaje").innerHTML = "";
        $('#modalAC').modal('hide');
        $('#ModaEstado').modal('hide');
        filtrarDatos(1, "RazonSocial");
    }

}

