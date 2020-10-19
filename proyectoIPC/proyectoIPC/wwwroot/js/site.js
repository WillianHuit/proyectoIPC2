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

function jugadorJugador() {
    var url = '/Home/FormJugador';
    var nombreUserSesion = localStorage.getItem("usuario");
    var idUserSesion = localStorage.getItem("idUsuario");
    var form = $('<form action="' + url + '" method="post">' +
        '<input style="display: none" type="text" name="id" value="' + idUserSesion + '" />' +
        '<input style="display: none" type="text" name="userName" value="' + nombreUserSesion + '" />' +
        '<input style="display: none" type="text" name="idJugadoB" value="2002" />' +
        '</form>');
    $('body').append(form);
    form.submit();

}
function jugadorMaquina() {
    var nombreUserSesion = localStorage.getItem("usuario");
    var idUserSesion = localStorage.getItem("idUsuario");
    var activarMaquina = false;
    var jugadorA = "";
    var jugadorB = "";
    var idJugadorA = -1;
    var idJugadorB = -1;
    swal({
        title: "Escoge tu turno",
        text: "¿Que ficha deseas usar?",
        icon: "https://i.gifer.com/origin/14/14b9f4265321c8ed068339bb9708ebc5_w200.gif",
        buttons: {
            Si: {
                text: "Negras",
                value: "negro"
            },
            No: {
                text: "Blancas",
                value: "blanco",
            },
        },
        dangerMode: true,
    })
        .then((retornado) => {
            if (retornado == "negro") {
                jugadorA = nombreUserSesion;
                jugadorB = "Maquina";
                idJugadorA = idUserSesion;
                idJugadorB = 2003;
                enviar("noXML",jugadorA, jugadorB, idJugadorA, idJugadorB);
            } else {
                jugadorB = nombreUserSesion;
                jugadorA = "Maquina";
                idJugadorB = idUserSesion;
                idJugadorA = 2002;
                enviar("noXML",jugadorA, jugadorB, idJugadorA, idJugadorB);
            }
        });

}
function cargarPartida(archivoXML) {
    var nombreUserSesion = localStorage.getItem("usuario");
    var idUserSesion = localStorage.getItem("idUsuario");
    var activarMaquina = false;
    var jugadorA = "";
    var jugadorB = "";
    var idJugadorA = -1;
    var idJugadorB = -1;
    swal({
        title: "Jugar contra maquina",
        text: "¿Deseas Jugar contra una maquina esta partida?",
        icon: "https://i.gifer.com/origin/14/14b9f4265321c8ed068339bb9708ebc5_w200.gif",
        buttons: {
            Si: true,
            No: {
                text: "No",
                value: false,
            },
        },
        dangerMode: false,
    })
        .then((retornado) => {
            if (retornado) {
                swal({
                    title: "Escoge tu turno",
                    text: "¿Que ficha deseas usar?",
                    icon: "info",
                    buttons: {
                        Si: {
                            text: "Negras",
                            value: "negro",
                        },
                        No: {
                            text: "Blancas",
                            value: "blanco",
                        },
                    },
                    dangerMode: true,
                })
                    .then((retornado) => {
                        if (retornado == "negro") {
                            jugadorA = nombreUserSesion;
                            jugadorB = "Maquina";
                            idJugadorA = idUserSesion;
                            idJugadorB = 2003;
                            enviar(archivoXML, jugadorA, jugadorB, idJugadorA, idJugadorB);
                        } else {
                            jugadorB = nombreUserSesion;
                            jugadorA = "Maquina";
                            idJugadorB = idUserSesion;
                            idJugadorA = 2003;
                            enviar(archivoXML, jugadorA, jugadorB, idJugadorA, idJugadorB);
                        }
                    });
            } else {
                swal({
                    title: "Escoge tu turno",
                    text: "¿Que ficha deseas usar?",
                    icon: "info",
                    buttons: {
                        Si: {
                            text: "Negras",
                            value: "negro",
                        },
                        No: {
                            text: "Blancas",
                            value: "blanco",
                        },
                    },
                    dangerMode: true,
                })
                    .then((retornado) => {
                        if (retornado == "negro") {
                            jugadorA = nombreUserSesion;
                            idJugadorA = idUserSesion;//Cambiar por valores reales
                            idJugadorB = 2002;
                            swal("Ingresa el nombre de tu invitado:", {
                                content: "input",
                            })
                                .then((value) => {
                                    jugadorB = value;
                                    enviar(archivoXML, jugadorA, jugadorB, idJugadorA, idJugadorB);
                                });
                            
                        } else {
                            jugadorB = nombreUserSesion;
                            idJugadorB = idUserSesion;//Cambiar por valores reales
                            idJugadorA = 2002;
                            swal("Ingresa el nombre de tu invitado:", {
                                content: "input",
                            })
                                .then((value) => {
                                    jugadorA = value;
                                    enviar(archivoXML, jugadorA, jugadorB, idJugadorA, idJugadorB);
                                });
                        }
                    });
            }
        });
    if (activarMaquina) {
        
    }
    /*var url = '/Home/Juego';
    var form = $('<form action="' + url + '" method="post">' +
        '<input style="display: none" type="text" name="archivoXML" value="' + archivoXML + '" />' +
        '</form>');
    $('body').append(form);
    form.submit();*/
}
function enviar(archivoXML, jugadorA, jugadorB, idJA, idJB) {
    var url = '/Home/Juego';
    
        var form = $('<form action="' + url + '" method="post">' +
            '<input style="display: none" type="text" name="archivoXML" value="' + archivoXML + '" />' +
            '<input style="display: none" type="text" name="jugadorA" value="' + jugadorA + '" />' +
            '<input style="display: none" type="text" name="jugadorB" value="' + jugadorB + '" />' +
            '<input style="display: none" type="text" name="idJA" value="' + idJA + '" />' +
            '<input style="display: none" type="text" name="idJB" value="' + idJB + '" />' +
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
    alert("Se guardara")
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
/*function zoom(ele) {
    var id = ele.id;
    let element = document.getElementById(id);
    //Descomentar el if para validar fichas

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
}*/