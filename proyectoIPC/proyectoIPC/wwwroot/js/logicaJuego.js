var turno = 0;
var ficha = 3;
var turnoInt = 3;
var repetir = 0;
let pasarTurno = false;
//Turno = 0 -> negro
//Turno = 1 -> blanco
function zoom(ele) {
	console.log(ele);
	//Realiza el movimiento
    var id = ele.id;

    let element = document.getElementById(id);
    let fichaVer = document.getElementById("fichaTurno");
    if(element.className=="btn btn-outline-secondary boton"){
        if(turno==0){
            element.className = "boton bg-dark"
            fichaVer.className = "boton btn btn-outline-dark";
            turno = 1;
            ficha = 1;
        }else{
            element.className = "boton bg-light";
            fichaVer.className = "boton bg-dark";
            turno = 0;
            ficha = 2;
        }
      
    }else{
		swal({
			text: "No puedes realizar este movimiento!",
			icon: "error"
		});
    }
    //Obtiene el tablero actual
    let tablero_old = [[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]
	for (i = 0; i < 8; i++) {
        for (j = 0; j < 8; j++) {
            fila = i + 1;
            columna = j + 1;
            id_co = fila + "" + columna;
            //console.log(id)
            element2 = document.getElementById(id_co);
            if (element2.className == "boton bg-dark") {
                tablero_old[i][j]=1;
            } else if (element2.className == "boton bg-light") {
                tablero_old[i][j]=2;
            } else {
                tablero_old[i][j]=0;
            }
        }

  	}
  	fila = id[0]-1
  	columna = id[1]-1
  	tablero_old = vertical(fila,columna,tablero_old);
  	tablero_old = horizontal(fila,columna,tablero_old);
  	tablero_old = diagonalD(fila,columna,tablero_old);
  	tablero_old = diagonalI(fila,columna,tablero_old);
  	tablero_old = validarMovimiento(tablero_old);
  	pintarTablero(tablero_old);
  	contarTabla(tablero_old);
  	console.log(tablero_old);

}

function vertical(fila,columna,tablero) {
	// ------------ Hacia abajo -----------------
	abajo = fila;
	for(i = fila;i<8;i++){
		if(tablero[i][columna]==0){
			break;
		}else if(tablero[i][columna]==ficha){
			//console.log("valor:"+tablero[i][columna])
			abajo = i+1;
			//console.log("En donde:"+i+" Turno:"+turno)
		}
	}
	for(i = fila;i<abajo;i++){
		id = (i+1)+""+(columna+1);
		let element = document.getElementById(id);
		if(turno==1){
            //element.className = "boton bg-dark"
            tablero[i][columna] = 1
        }else{
            //element.className = "boton bg-light"
            tablero[i][columna] = 2
        }
	}
	//---------------Hacia arriba --------------------
	arriba = fila
	for(i = fila;i>-1;i--){
		if(tablero[i][columna]==0){
			break;
		}else if(tablero[i][columna]==ficha){
			//console.log("valor:"+tablero[i][columna])
			arriba = i+1;
			//console.log("En donde:"+i+" Turno:"+turno)
		}
	}
	for(i = arriba;i<fila;i++){
		id = (i+1)+""+(columna+1);
		let element = document.getElementById(id);
		if(turno==1){
            //element.className = "boton bg-dark"
            tablero[i][columna] = 1
        }else{
            //element.className = "boton bg-light"
            tablero[i][columna] = 2
        }
    }
	return tablero
   //return x * x;
}

function horizontal(fila,columna,tablero) {
// ------------------ Hacia la derecha ------------------
	derecha = columna;
	for(i = columna;i<8;i++){
		if(tablero[fila][i]==0){
			break;
		}else if(tablero[fila][i]==ficha){
			//console.log("valor:"+tablero[i][columna])
			derecha = i+1;
			//console.log("En donde:"+i+" Turno:"+turno)
		}
	}
	for(i = columna;i<derecha;i++){
		id = (fila+1)+""+(i+1);
		let element = document.getElementById(id);
		if(turno==1){
            //element.className = "boton bg-dark"
            tablero[fila][i] = 1
        }else{
            //element.className = "boton bg-light"
            tablero[fila][i] = 2
        }
	}
// ------------------ Hacia la izquierda ------------------
	izquierda = columna;
	for(i = columna;i>-1;i--){
		if(tablero[fila][i]==0){
			break;
		}else if(tablero[fila][i]==ficha){
			//console.log("valor:"+tablero[i][columna])
			derecha = i+1;
			//console.log("En donde:"+i+" Turno:"+turno)
		}
	}
	for(i = derecha;i<columna;i++){
		id = (fila+1)+""+(i+1);
		let element = document.getElementById(id);
		if(turno==1){
            //element.className = "boton bg-dark"
            tablero[fila][i] = 1
        }else{
            //element.className = "boton bg-light"
            tablero[fila][i] = 2
        }
	}
	return tablero
}
function diagonalD(fila,columna,tablero) {
	abajo = fila;
	contador = 0
	for(i = fila;i<8;i++){
		if(tablero[i][columna-contador]==0){
			break;
		}else if(tablero[i][columna-contador]==ficha){
			//console.log("valor:"+tablero[i][columna])
			//console.log("Encontro: "+i+""+(columna-contador))
			abajo = i;
			//console.log("En donde:"+i+" Turno:"+turno)
		}
		contador++;
	}
	//console.log("El: "+fila+""+columna+" finaliza: "+abajo)
	contador = 0;
	for(i = fila;i<abajo;i++){
		id = (i+1)+""+(columna-contador+1);
		let element = document.getElementById(id);
		if(turno==1){
            //element.className = "boton bg-dark"
            tablero[i][columna-contador] = 1
        }else{
            //element.className = "boton bg-light"
            tablero[i][columna-contador] = 2
        }
        contador++;
	}
	// ---------------------- Hacia Abajo -------------------
	arriba = fila;
	arribaB = columna
	contador = 0
	for(i = fila;i>-1;i--){
		//console.log("Busca: "+i+""+(columna+contador))
		if(tablero[i][columna+contador]==0){
			break;
		}else if(tablero[i][columna+contador]==ficha){
			//console.log("valor:"+tablero[i][columna])
			//console.log("Encontro: "+i+""+(columna+contador))
			arriba = i;
			arribaB = columna+contador
			//console.log("En donde:"+i+" Turno:"+turno)
		}
		contador++;
	}
	//console.log("El: "+fila+""+columna+" finaliza: "+arriba)
	contador = 0;
	for(i = arriba;i<fila;i++){
		id = (i+1)+""+(arribaB-contador+1);
		//console.log(id);
		let element = document.getElementById(id);
		//console.log(i+""+(arribaB-contador))
		if(turno==1){
            //element.className = "boton bg-dark"
            tablero[i][arribaB-contador] = 1
        }else{
            //element.className = "boton bg-light"
            tablero[i][arribaB-contador] = 2
        }
        contador++;
	}
	return tablero
}
function diagonalI(fila,columna,tablero) {
	abajo = fila;
	contador = 0
	for(i = fila;i<8;i++){
		if(tablero[i][columna+contador]==0){
			break;
		}else if(tablero[i][columna+contador]==ficha){
			//console.log("valor:"+tablero[i][columna])
			//console.log("Encontro: "+i+""+(columna-contador))
			abajo = i;
			//console.log("En donde:"+i+" Turno:"+turno)
		}
		contador++;
	}
	contador = 0;
	for(i = fila;i<abajo;i++){
		id = (i+1)+""+(columna+contador+1);
		let element = document.getElementById(id);
		if(turno==1){
            //element.className = "boton bg-dark"
            tablero[i][columna+contador] = 1
        }else{
            //element.className = "boton bg-light"
            tablero[i][columna+contador] = 2
        }
        contador++;
	}
	//-------------------------- Hacia abajo -------------------------
	arriba = fila;
	arribaB = columna;
	contador = 0;
	for(i = fila;i>-1;i--){
		if(tablero[i][columna-contador]==0){
			break;
		}else if(tablero[i][columna-contador]==ficha){
			//console.log("valor:"+tablero[i][columna])
			//console.log("Encontro: "+i+""+(columna-contador))
			arriba = i;
			arribaB	= columna-contador;
			//console.log("En donde:"+i+" Turno:"+turno)
		}
		contador++;
	}
	contador = 0;
	for(i = arriba;i<fila;i++){
		id = (i+1)+""+(arribaB+contador+1);
		let element = document.getElementById(id);
		if(turno==1){
            //element.className = "boton bg-dark"
            tablero[i][arribaB+contador] = 1
        }else{
            //element.className = "boton bg-light"
            tablero[i][arribaB+contador] = 2
        }
        contador++;
	}
	return tablero;
}

function pintarTablero(tablero_old){
	for (i = 0; i < 8; i++) {
        for (j = 0; j < 8; j++) {
            fila = i + 1;
            columna = j + 1;
            id_co = fila + "" + columna;
            //console.log(id)
            element2 = document.getElementById(id_co);
            if (tablero_old[i][j]==1) {
                element2.className = "boton bg-dark";
            } else if (tablero_old[i][j]==2) {
            	element2.className = "boton bg-light";
            }else if (tablero_old[i][j]==3) {
            	element2.className = "btn btn-outline-secondary boton";
            }else{
            	element2.className = "boton bg-success";
            }

        }

  	}
}

function validarPrimerMovimiento(){
	let tablero_old = [[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]
	for (i = 0; i < 8; i++) {
        for (j = 0; j < 8; j++) {
            fila = i + 1;
            columna = j + 1;
            id_co = fila + "" + columna;
            //console.log(id)
            element2 = document.getElementById(id_co);
            if (element2.className == "boton bg-dark") {
                tablero_old[i][j]=1;
            } else if (element2.className == "boton bg-light") {
                tablero_old[i][j]=2;
            } else {
                tablero_old[i][j]=0;
            }
        }

  	}
  	var txtTurno = "";
  	txtTurno = document.getElementById("turno").value;
  	console.log(txtTurno);
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
  	console.log(tablero_old);
  	tablero_old=validarMovimiento(tablero_old);
  	pintarTablero(tablero_old);
  	contarTabla(tablero_old); 	
}
 document.addEventListener("DOMContentLoaded", function(event) {
   validarPrimerMovimiento();
  });