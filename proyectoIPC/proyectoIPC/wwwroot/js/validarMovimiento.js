function validarMovimiento(tablero) {
	//tablero = vertical(fila,columna,tablero);
	console.log(tablero);
	posiciones = findPosiciones(tablero);
	for (var i = 0; i < posiciones.length; i++) {
		tablero = buscarCoincidencias(posiciones[i],tablero);
	}
	return tablero;
}

function buscarCoincidencias(posicion,tablero){
	posicion = posicion.split(",");
	var fila = parseInt(posicion[0]);
	var columna = parseInt(posicion[1]);
	tablero = verticalV(fila,columna,tablero);
	tablero = horizontalV(fila,columna,tablero);
	tablero = cruzado(fila,columna,tablero);
	return tablero;
}


function verticalV(fila,columna,tablero) {
	// ------------ Hacia abajo -----------------
	var abajo = fila;
	for(i = fila;i<8;i++){
		if(tablero[i][columna]==0 || tablero[fila][i]==3){
			break;
		}else if(tablero[i][columna]==ficha){
			abajo = i;
		}
	}
	if(abajo<7 && abajo!=fila && tablero[abajo+1][columna]!=1 && tablero[abajo+1][columna]!=2){
		if(tablero[abajo+1][columna] == 0){
			tablero[abajo+1][columna] = 3;
		}
	}
	//------------ Hacia arriba ------------------
	abajo = fila;
	for(i = fila;i>0;i--){
		if(tablero[i][columna]==0 || tablero[fila][i]==3){
			break;
		}else if(tablero[i][columna]==ficha){
			abajo = i;
		}
	}
	if(abajo>0 && abajo!=fila && tablero[abajo-1][columna]!=1 && tablero[abajo-1][columna]!=2){
		if(tablero[abajo-1][columna] ==0){
			tablero[abajo-1][columna] = 3;
		}
	}
	return tablero;
}
function horizontalV(fila,columna,tablero){
	// -------------------- Derecha ---------------------
	var derecha = columna;
	for(i = columna;i<8;i++){
		if(tablero[fila][i]==0 || tablero[fila][i]==3){
			break;
		}else if(tablero[fila][i]==ficha){
			derecha = i;
		}
	}
	if(derecha<7 && derecha!=columna){
		if(tablero[fila][derecha+1]==0){
			tablero[fila][derecha+1] = 3;
		}
	}
	// ------------------- Izquierda ---------------------
	var izquierda = columna;
	for(i = columna;i>0;i--){
		if(tablero[fila][i]==0 || tablero[fila][i]==3){
			break;
		}else if(tablero[fila][i]==ficha){
			izquierda = i;
		}
	}
	if(izquierda>0 && izquierda!=columna){
		if(tablero[fila][izquierda-1]==0){
			tablero[fila][izquierda-1] = 3;
		}
	}
	
	return tablero;
}
function cruzado(fila,columna,tablero){
	// ------------------Diagonal Derecha hacia abajo [\]-------------------
	var abajo = fila;
	var abajoB = fila;
	var contador = 0;
	for(i = fila;i<8;i++){
		if(tablero[i][columna+contador]==0 || tablero[i][columna+contador]==3){
			break;
		}else if(tablero[i][columna+contador]==ficha){
			abajo = i;
			abajoB = columna+contador;
		}
		contador++;
	}
	if(abajo<7 && abajo!=fila){
		if(tablero[abajo+1][abajoB+1] == 0){
			tablero[abajo+1][abajoB+1] = 3;
		}
	}	
	// ------------------Diagonal Derecha hacia arriba [\]-------------------
	var arriba = fila;
	var arribaB = fila;
	var contador = 0;
	for(i = fila;i>0;i--){
		if(tablero[i][columna-contador]==0 || tablero[i][columna-contador]==3){
			break;
		}else if(tablero[i][columna-contador]==ficha){
			arriba = i;
			arribaB = columna-contador;
		}
		contador++;
	}
	if(arriba>0 && arriba!=fila){
		if(tablero[arriba-1][arribaB-1] == 0){
			tablero[arriba-1][arribaB-1] = 3;
		}
	}
	// ------------------Diagonal Derecha hacia abajo [/]-------------------
	var abajo = fila;
	var abajoB = fila;
	var contador = 0;
	for(i = fila;i<8;i++){
		if(tablero[i][columna-contador]==0 || tablero[i][columna-contador]==3){
			break;
		}else if(tablero[i][columna-contador]==ficha){
			abajo = i;
			abajoB = columna-contador;
		}
		contador++;
	}
	if(abajo<7 && abajo!=fila){
		if(tablero[abajo+1][abajoB-1] == 0){
			tablero[abajo+1][abajoB-1] = 3;
		}
	}
	// ------------------Diagonal Derecha hacia arriba [/]-------------------
	var arriba = fila;
	var arribaB = fila;
	var contador = 0;
	for(i = fila;i>0;i--){
		if(tablero[i][columna+contador]==0 || tablero[i][columna+contador]==3){
			break;
		}else if(tablero[i][columna+contador]==ficha){
			arriba = i;
			arribaB = columna+contador;
		}
		contador++;
	}
	if(arriba>0 && arriba!=fila){
		if(tablero[arriba-1][arribaB+1] == 0){
			tablero[arriba-1][arribaB+1] = 3;
		}
	}	
	/*var abajo = fila;
	var abajoB = fila;
	var contador = 0;
	for(i = fila;i<8;i++){
		if(tablero[i][columna+contador]==0 || tablero[fila][i]==3){
			break;
		}else if(tablero[i][columna+contador]==ficha){
			abajo = i;
			abajoB = columna+contador;
		}
		contador++;
	}
	if(abajo<7 && abajo!=fila){
		tablero[abajo+1][abajoB+1] = 3;
	}*/

	return tablero
}
function findPosiciones(tablero){
	//ficha
	var nextFicha = 3;
	if(ficha==1){
		nextFicha = 2
	}else if(ficha==2){
		nextFicha = 1;
	}
	let posiciones = new Array();
	contador = 0;
	for(i=0;i<8;i++){
		for (j = 0; j < 8; j++) {
			if(tablero[i][j] == nextFicha){
				posiciones[contador] = i+","+j;
				contador++;
			}
		}
	}

	return posiciones;
}