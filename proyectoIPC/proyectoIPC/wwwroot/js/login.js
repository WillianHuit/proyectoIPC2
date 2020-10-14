/*jQuery(document).ready(function ($) {
    
    $('#txtUsuario').focus();
    $("#btnEntrar").on("click", function () {
        alert("Hola");
        console("Entra a la clase");
        if ($("#txtUsuario").val != "" & $("#txtClave").val != "") {
           // Validate($("#txtUsuario").val(), $("#txtClave").val());
    }else {
            alert("Error")
    }
});
*/
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
                localStorage.setItem("usuario", usuario);
                localStorage.setItem("idUsuario", data.idUsr);
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

function llamar() {
    usuario = document.getElementById("txtUsuario").value;
    clave = document.getElementById("txtClave").value;
    if (usuario != "" & clave != "") {
        Validate(usuario, clave);
    } else {
        document.getElementById('alerta').style.display = 'block';
    }
}
function registrar() {
    nombre = document.getElementById("txtNombre").value;
    apellido = document.getElementById("txtApellido").value;
    user = document.getElementById("txtUser").value;
    clave = document.getElementById("txtPass").value;
    pais = document.getElementById("txtPais").value;
    correo = document.getElementById("txtCorreo").value;
    fecha = document.getElementById("txtFecha").value;
    if (nombre != "" & apellido != "" & user != "" & clave != "" & correo != "" & fecha != "" & pais != "") {
        Registrar(nombre, apellido, user, clave, pais, correo, fecha);
    } else {
        document.getElementById('alerta').style.display = 'block';
    }
}
function Registrar(nombre, apellido,usuario, clave, pais, correo, fecha) {
    var record = {
        nombre: nombre,
        apellido: apellido,
        usr: usuario,
        password: clave,
        pais: pais,
        fecha: fecha,
        correo: correo
        
    };
    $.ajax({

        url: '/Usuarios/insertarUsuario',
        async: false,
        type: 'POST',
        data: record,
        beforeSend: function (xhr, opts) {
        },
        success: function (data) {
            if (data.status == true) {
                alert("Bienvenido");
                document.location.href = '/Home/Login';
            } else if (data.status == false) {
                alert("Error");

            }
        },
        error: function (data) {
            alert("Error")
        }
    })
}