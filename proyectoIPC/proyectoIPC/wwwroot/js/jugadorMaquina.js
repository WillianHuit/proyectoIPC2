
document.addEventListener("DOMContentLoaded", function(event) {
   function knowIcanPlay() {
	   
	   try {
		   cargarIA();
	   } catch (error) {
		   // expected output: ReferenceError: nonExistentFunction is not defined
		   // Note - error messages will vary depending on browser
	   }
	}
	setInterval(knowIcanPlay, 3000);
  });

function cargarIA() {
	let stateIA = document.getElementById("onlyGame").value;
	let turnoIA = document.getElementById("turnoIA").value;
	if(stateIA=="activar"){
		jugarIA(turnoIA)
	}
}

function jugarIA(turnoIA){
	let posiciones = new Array();
	if(turnoIA == "negro"){
		turnoInt = 0;
	}else{
		turnoInt = 1;
	}
	if(turnoInt==turno){
		if(pasarTurno){
			pasarTurno = false;
		} else {
			lista = ["Ya he movido mi ficha", "Estoy a punto de ganar", "Buen movimiento, Que te parece este?", "No dejare que hagas eso", "Observa este movimiento","...","Mmmm...","Nada mal","Parece que estoy perdiendo?","Debo concentrarme mas","Uno mas y ganare","Ese movimiento es legal?","Ya se cual es tu siguiente movimiento","Tira mas rapido","Deprisa!","No se ve nada bien verdad?","Estoy calculando mi victoria"];
			iconos = ["https://media4.giphy.com/media/MXS4ZiO7fg6NNbYs0V/source.gif", "https://media2.giphy.com/media/KaoBnzxzuBhpZB9Jjr/source.gif", "https://media2.giphy.com/media/fW4faqOiyXyfqGyLH2/giphy.gif", "https://media2.giphy.com/media/MBfBKtomG4Y7EumyX6/giphy.gif","https://media1.giphy.com/media/LpLzM8cDGLPaKXnMWT/giphy.gif"]
			var y = Math.floor((Math.random() * lista.length));
			var z = Math.floor((Math.random() * iconos.length));
			posiciones = obtenerPosiciones();
			console.log(posiciones);
			var x = Math.floor((Math.random() * posiciones.length));
			envio = {id: posiciones[x]};
			zoom(envio);
			swal({
				text: lista[y],
				icon: iconos[z]
			});
		}
		
	}

}
function obtenerPosiciones(){
	let posiciones = new Array();
	var contador = 0;
	for (i = 0; i < 8; i++) {
        for (j = 0; j < 8; j++) {
            fila = i + 1;
            columna = j + 1;
            id_co = fila + "" + columna;
            //console.log(id)
            element2 = document.getElementById(id_co);
            if (element2.className == "btn btn-outline-secondary boton") {
                posiciones[contador] = id_co;
                contador++;
            } 
        }

  	}
  	return posiciones;
}