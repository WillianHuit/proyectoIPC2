let noContinue = false;
function contarTabla(tablero) {
	var blancas = 0;
	var negras = 0;
	var grises = 0;
	var espacios = 0;
	for(i=0;i<8;i++){
		for (j = 0; j < 8; j++) {
			if(tablero[i][j] == 1){
				negras++;
			}else if(tablero[i][j] == 2){
				blancas++;
			}else if(tablero[i][j] == 3){
				grises++;
			}else{
				espacios++;
			}
		}
	}
	let contadorBlancas = document.getElementById("JugadorBCount");
	let contadorNegras = document.getElementById("JugadorACount");
	contadorBlancas.innerHTML = blancas;
	contadorNegras.innerHTML = negras;
	if(grises==0 && espacios>0 && !noContinue){
		cambiarTurno(tablero);
	}else if(grises==0 && espacios==0){
		if(negras>blancas){
			ganador("Negras");
		}else if(negras<blancas){
			ganador("Blancas");
		}else{
			ganador("Empate");
		}
	}
}

function cambiarTurno(tablero) {
	if (repetir > 3) {
		elegirGanador(tablero);
	}
	alert("El siguiente jugador no tiene movimientos posibles, vuelve a colocar otra ficha.");
	repetir++;
	pasarTurno = true;
	if(turno==0){
            fichaVer.className = "boton btn btn-outline-dark";
            turno = 1;
            ficha = 1;
        }else{
            fichaVer.className = "boton bg-dark";
            turno = 0;
            ficha = 2;
        }
    tablero = validarMovimiento(tablero);
  	pintarTablero(tablero);
  	contarTabla(tablero);
}
function ganador(fichaGanador){
	if (fichaGanador == "Empate") {
		swal({
			title: "Juego Terminado",
			text: "Han Empatado",
			icon: "https://media0.giphy.com/media/TdFBF45RfGCeLPWKM1/giphy.gif"
		})
			.then(results => {
				guardarPartidaDB(2004);
			});
	} else if (fichaGanador == "Blancas") {
		swal({
			title: "Juego Terminado",
			text: "Las fichas BLANCAS han ganado",
			icon: "https://media0.giphy.com/media/TdFBF45RfGCeLPWKM1/giphy.gif"
		})
			.then(results => {
				idGanador = getIdGanador("blanco");
				guardarPartidaDB(idGanador);
			});
	} else if (fichaGanador == "Negras") {
		swal({
			title: "Juego Terminado",
			text: "Las fichas NEGRAS han ganado",
			icon: "https://media0.giphy.com/media/TdFBF45RfGCeLPWKM1/giphy.gif"
		})
			.then(results => {
				idGanador = getIdGanador("negro");
				guardarPartidaDB(idGanador);
			});
	}
}

function elegirGanador(tablero) {
	noContinue = true;
	var blancas = 0;
	var negras = 0;
	for (i = 0; i < 8; i++) {
		for (j = 0; j < 8; j++) {
			if (tablero[i][j] == 1) {
				negras++;
			} else if (tablero[i][j] == 2) {
				blancas++;
			}
		}
	}
	if (blancas > negras) {
		swal({
			title: "Juego Terminado",
			text: "Las fichas BLANCAS han ganado",
			icon: "https://media0.giphy.com/media/TdFBF45RfGCeLPWKM1/giphy.gif"
		}).then(results => {
			idGanador = getIdGanador("blanco");
			guardarPartidaDB(idGanador);
		});
	} else if (blancas < negras) {
		swal({
			title: "Juego Terminado",
			text: "Las fichas NEGRAS han ganado",
			icon: "https://media0.giphy.com/media/TdFBF45RfGCeLPWKM1/giphy.gif"
		}).then(results => {
			idGanador = getIdGanador("negro");
			guardarPartidaDB(idGanador);
		});
	} else if (blancas = negras) {
		swal({
			title: "Juego Terminado",
			text: "Han empatado",
			icon: "https://media0.giphy.com/media/TdFBF45RfGCeLPWKM1/giphy.gif"
		}).then(results => {
			guardarPartidaDB(2004);
		});
	}
	alert("finalizo");
	turno = 3;
	ficha = 3;
	turnoInt = 3;
	pasarTurno = false;
}

function getIdGanador(txtGanador) {
	var idGanador = -1;
	//A es el negro
	var element=2004;
	if (txtGanador == "negro") {
		element = document.getElementById("idJugadorA").value;
	} else {
		element = document.getElementById("idJugadorB").value;
	}
	idGanador = element;
	return idGanador;
}

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