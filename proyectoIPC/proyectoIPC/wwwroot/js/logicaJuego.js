var turno = 0;
var ficha = 3;
var turnoInt = 3;
var repetir = 0;
let pasarTurno = false;
var colorA;
var colorACount = 0;
var colorB;
var colorBCount = 0;
function zoom(ele) {
	//Realiza el movimiento
    var id = ele.id;

    let element = document.getElementById(id);
    let fichaVer = document.getElementById("fichaTurno");
    if (true) {
        if(turno==0){
            element.className = "boton btn " + colorA[colorACount];
            element.name = "A";
            fichaVer.className = "boton btn " + colorB[colorBCount];
            colorACount++;
            
            if (colorACount == colorA.length-1) {
                    colorACount = 0;
                }
            turno = 1;
            ficha = 1;
        }else{
            element.className = "boton btn " + colorB[colorBCount];
            element.name = "B";
            fichaVer.className = "boton btn " + colorA[colorACount];
            colorBCount++;
                if (colorBCount == colorB.length-1) {
                    colorBCount = 0;
                }
            turno = 0;
            ficha = 2;
        }
      
   
    //Obtiene el tablero actual
    
    var tamFila = document.getElementById("filaTabla").value;
    var tamColumna = document.getElementById("columnaTabla").value;
    var tableroStr = "";
    for (var i = 0; i < tamFila; i++) {
        for (var j = 0; j < tamColumna; j++) {
            var filaA = i + 1;
            var columnaA = j + 1;
            id_co = filaA + "" + columnaA;
            let element2 = document.getElementById(id_co);
            if (element2.name == "A") {
                tableroStr = tableroStr+"1,";
            } else if (element2.name == "B") {
                tableroStr = tableroStr+ "2,";
            } else {
                tableroStr = tableroStr+"0,";
            }
        }
    }
    fila = id[0] - 1;
    columna = id[1] - 1;
    var tiros = document.getElementById("tiros").value;
    var tipo = document.getElementById("tipo").value;
    validarServidor(tableroStr, fila, columna, tamFila, tamColumna, ficha, turno,tipo,tiros);
    document.getElementById("tiros").value=parseInt(tiros)+1

    } else {
        swal({
            text: "No puedes realizar este movimiento!",
            icon: "error"
        });
    }
}

function validarServidor(tableroStr, fila, columna, tamFila, tamColumna, fichaA, turnoA, tipo, tiros) {
    console.log(tableroStr);
    var record = {
        tableroOld: tableroStr,
        fila: fila,
        columna: columna,
        x: parseInt(tamFila),
        y: parseInt(tamColumna),
        ficha: fichaA,
        turno: turnoA,
        tipo: tipo,
        tiros: tiros
    };
    $.ajax({

        url: '/Movimientos/validarMovimiento',
        async: false,
        type: 'POST',
        data: record,
        beforeSend: function (xhr, opts) {
        },
        success: function (data) {
            if (data.status) {
                if (data.win) {
                    if (data.nextTurn) {
                        turno = data.sigT;
                        alert(data.message);
                    } 
                    alert("ganador: " + data.winName);
                }
                pintarTablero(data.tabla)
            } 
        },
        error: function (data) {
            alert("Opss! Ocurrio un error :(" + data)
        }
    })
}
function pintarTablero(tablero) {
    console.log(tablero);
    var tamFila = document.getElementById("filaTabla").value;
    var tamColumna = document.getElementById("columnaTabla").value;
    var contador = 0;
    for (i = 0; i < tamFila; i++) {
        for (j = 0; j < tamColumna; j++) {
            fila = i + 1;
            columna = j + 1;
            id_co = fila + "" + columna;
            //console.log(id)
            element2 = document.getElementById(id_co);
            if (tablero[contador]==1) {
                element2.className = "boton btn "+colorA[colorACount];
                element2.name = "A";
                colorACount++;
                    if (colorACount == colorA.length - 1) {
                        colorACount = 0;
                    }
            } else if (tablero[contador]==2) {
                element2.className = "boton btn " + colorB[colorBCount];
                element2.name = "B";
                colorBCount++;
                if (colorBCount == colorB.length - 1) {
                    colorBCount = 0;
                }
            } else if (tablero[contador]==3) {
                element2.className = "boton btn bg-disponible";
                element2.name = "C"
            }else{
                element2.className = "boton btn bg-fondo";
                element2.name = "D"
            }
            contador++;
        }

  	}
}

function cargar(){
	
  	txtTurno = document.getElementById("turno").value;
  	if(txtTurno=="negro"){
  		turno = 0;
  		ficha = 2;
  	}else{
  		turno = 1;
  		ficha = 1;
  	}
  	fichaVer = document.getElementById("fichaTurno");
  	if(turno==0){
  		fichaVer.className = "boton bg-dark";
  	}else if(turno==1){
  		fichaVer.className = "boton btn btn-outline-dark";
  	}
    var colorsA = document.getElementById("colorsA").value;
    var colorsB = document.getElementById("colorsB").value;
    colorA = colorsA.split(" ");
    colorB = colorsB.split(" ");
    console.log(colorA);
    console.log(colorB)
}
 document.addEventListener("DOMContentLoaded", function(event) {
     cargar();
 });


function guardarPartidaDB(idGanador) {
    var jugadorA = document.getElementById("idJugadorA").value;
    var jugadorB = document.getElementById("idJugadorB").value;
    var anfitrion = localStorage.getItem("idUsuario");
    console.log(jugadorA);
    console.log(jugadorB);
    console.log(anfitrion);
    console.log(idGanador);
    var record = {
        Anfitrion: anfitrion,
        JugadorA: jugadorA,
        JugadorB: jugadorB,
        Ganador: idGanador

    };
    $.ajax({

        url: '/Partidas/guardarPartida',
        async: false,
        type: 'POST',
        data: record,
        beforeSend: function (xhr, opts) {
        },
        success: function (data) {
            if (data.status == true) {
                document.location.href = '/Home/Inicio';
            } else if (data.status == false) {
                alert("Upss! Ocurrio un error.(DB)");
            }
        },
        error: function (data) {
            alert("Upss! Ocurrio un error.");
        }
    });
}