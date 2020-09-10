var turno = 0
function myFunction() {
    alert("Hola");
}
function zoom(ele) {
    var id = ele.id;
    let element = document.getElementById(id);
    if (element.className == "btn btn-outline-secondary boton") {
        if (turno == 0) {
            element.className = "boton bg-dark"
            turno = 1
        } else {
            element.className = "boton bg-light"
            turno = 0
        }
    } else {
        alert("No puedes seleccionar esta ficha")
    }


}
function Validate(usuario, clave) {
    var record = {
        nombreUsuario: usuario,
        claveUsuario: clave
    };
    $.ajax({

        url: '/Usuarios/GetUsuarios',
        async: false,
        type: 'POST',
        data: record,
        beforeSend: function (xhr, opts) {
        },
        success: function (data) {
            if (data.status == true) {
                alert("Bienvenido");
                document.location.href = '/Home/Inicio';
            } else if (data.status == false) {
                document.getElementById('alerta').style.display = 'block';

            }
        },
        error: function (data) {
            document.getElementById('alerta').style.display = 'block';
        }
    })
}