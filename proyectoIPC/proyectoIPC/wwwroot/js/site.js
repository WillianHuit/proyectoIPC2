var turno = 0
$('#btnCargarArchivo').click(function () {
    $('#form-filtro').prop('submit', null);
    $('#form-filtro').unbind();
    $("#file-fichero").trigger('click');
});
function leerArchivo(e) {
    var archivo = e.target.files[0];
    if (!archivo) {
        return;
    }
    var lector = new FileReader();
    lector.onload = function (e) {
        var contenido = e.target.result;
        cargarPartida(contenido)
    };
    lector.readAsText(archivo);
}

document.getElementById('file')
    .addEventListener('change', leerArchivo, false);

function cargarPartida(archivoXML) {
    alert("Cargado")
    var url = '/Home/Juego';
    var form = $('<form action="' + url + '" method="post">' +
        '<input style="display: none" type="text" name="archivoXML" value="' + archivoXML + '" />' +
        '</form>');
    $('body').append(form);
    form.submit();
}
function guardarPartida() {
    var tablero = "";
    var id;
    var fila, columna;
    var element;
    
    for (i = 0; i < 8; i++) {
        for (j = 0; j < 8; j++) {
            fila = i + 1;
            columna = j + 1;
            id = fila + "" + columna;
            element = document.getElementById(id);
            if (element.className == "boton bg-dark") {
                tablero = tablero + "1,";
            } else if (element.className == "boton bg-light") {
                tablero = tablero + "2,";
            } else {
                tablero = tablero + "0,";
            }
        }
    }
    alert(tablero)
    var valor = 0
    var record = {
        cadena: tablero,
        turno: valor
    };
    $.ajax({

        url: '/Usuarios/generarXML',
        async: false,
        type: 'POST',
        data: record,
        beforeSend: function (xhr, opts) {
        },
        success: function (data) {
            if (data.textoXML != "") {

                var blob = new Blob([data.textoXML], { type: "text/xml" });
                saveAs(blob, "partidaGuardada.xml");


            }
        },
        error: function (data) {
            alert("Opss! Ocurrio un error :("+data)
        }
    })
}
function zoom(ele) {
    var id = ele.id;
    let element = document.getElementById(id);
    //Descomentar el if para validar fichas
/*if (element.className == "btn btn-outline-secondary boton") {*/
    if (true) {
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